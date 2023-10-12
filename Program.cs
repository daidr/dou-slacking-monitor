using DouSlackingMonitor.Enums;
using DouSlackingMonitor.Interfaces;
using DouSlackingMonitor.Libs;
using DouSlackingMonitor.Models;
using DouSlackingMonitor.Models.AppObserver;
using DouSlackingMonitor.Models.MediaObserver;
using Pastel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;
using Windows.Networking.Connectivity;

namespace DouSlackingMonitor
{
    internal static class Program
    {

        private static ConfigLoader configLoader;
        private static MainForm mainForm;

        private static NotifyIcon notifyIcon;
        private static ContextMenuStrip contextMenuStrip;
        private static ToolStripMenuItem exitMenuItem;
        private static ToolStripMenuItem networkMenuItem;
        private static ToolStripMenuItem showFormMenuItem;
        private static ToolStripMenuItem showConsoleMenuItem;
        private static ToolStripMenuItem reloadConfigMenuItem;

        private static DouSlackingAPI api;
        private static bool networkConnected = false;
        private static System.Threading.Thread heartbeatThread;

        private static IAppObserver appObserver;
        private static ISleepDiscover sleepDiscover;
        private static IMediaObserver mediaObserver;


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // avoid multiple instances
            if (System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("已经有一个DouSlacking Monitor正在运行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Win32API.AllocConsole();
            var handle = Win32API.GetConsoleWindow();
            var menus = Win32API.GetSystemMenu(handle, IntPtr.Zero);
            // remove close button and maximize button
            Win32API.RemoveMenu(menus, Win32API.SC_CLOSE, Win32API.MF_BYCOMMAND);
            Win32API.RemoveMenu(menus, Win32API.SC_MAXIMIZE, Win32API.MF_BYCOMMAND);
            Win32API.SetConsoleCtrlHandler(null, true);

            // hidden console window
            Win32API.ShowWindow(handle, Win32API.SW_HIDE);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // load config
            configLoader = new ConfigLoader("./general.toml");
            configLoader.ConfigLoaded += ConfigLoader_OnLoad;
            configLoader.Load();

            // add tray icon
            notifyIcon = new NotifyIcon
            {
                Icon = Properties.Resources.MainIcon,
                Text = "DouSlacking Monitor",
                Visible = true
            };
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
            contextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip = contextMenuStrip;

            exitMenuItem = new ToolStripMenuItem
            {
                Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134),
                ForeColor = Color.IndianRed,
                Text = "完全退出",
            };
            exitMenuItem.Click += (sender, e) => Exit();

            networkMenuItem = new ToolStripMenuItem
            {
                Enabled = false,
                Size = new Size(136, 22),
                Text = "网络状态：-"
            };

            showFormMenuItem = new ToolStripMenuItem
            {
                Text = "显示配置窗口"
            };
            showFormMenuItem.Click += ShowFormMenuItem_Click;

            reloadConfigMenuItem = new ToolStripMenuItem
            {
                Text = "重新加载配置"
            };
            reloadConfigMenuItem.Click += (sender, e) =>
            {
                configLoader.Load();
            };

            showConsoleMenuItem = new ToolStripMenuItem
            {
                Text = "显示 Console"
            };
            showConsoleMenuItem.Click += (sender, e) =>
            {
                ToggleConsoleWindow();
            };

            contextMenuStrip.Items.AddRange(new ToolStripItem[]
            {
                networkMenuItem,
                new ToolStripSeparator(),
                showFormMenuItem,
                showConsoleMenuItem,
                reloadConfigMenuItem,
                new ToolStripSeparator(),
                exitMenuItem
            });

            appObserver = new AppObserver();
            appObserver.OnAppActive += AppObserver_OnAppActive;


            sleepDiscover = new SleepDiscover();
            sleepDiscover.SleepStatusChanged += SleepDiscover_SleepStatusChanged;


            mediaObserver = new MediaObserver();
            mediaObserver.OnMediaUpdate += MediaObserver_OnMediaUpdate;
            mediaObserver.OnPlaybackUpdate += MediaObserver_OnPlaybackUpdate;
            mediaObserver.OnTimeUpdate += MediaObserver_OnTimeUpdate;

            // start observers
            appObserver.Start();
            sleepDiscover.Start();
            mediaObserver.Start();

            // start heartbeat
            heartbeatThread = new System.Threading.Thread(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(1000 * 10);
                    DoHeartbeat();
                }
            });

            heartbeatThread.Start();


            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
            NetworkInformation_NetworkStatusChanged(null);

            Application.Run();
            Win32API.FreeConsole();
        }

        private static void ConfigLoader_OnLoad(object sender, EventArgs e)
        {
            try
            {

            
            if (configLoader.lastError != "")
            {
                MessageBox.Show("配置加载失败：" + configLoader.lastError, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (api != null)
            {
                api.UpdateOptions(configLoader.config.ApiEntrypoint, configLoader.config.Secret);
                MessageBox.Show("配置已重载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                api = new DouSlackingAPI(configLoader.config.ApiEntrypoint, configLoader.config.Secret);
            }
            } catch (Exception ex)
            {
                configLoader.lastError = ex.Message;
                MessageBox.Show("配置加载失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void ToggleConsoleWindow()
        {
            if (showConsoleMenuItem.Checked)
            {
                Win32API.ShowWindow(Win32API.GetConsoleWindow(), Win32API.SW_HIDE);
                showConsoleMenuItem.Checked = false;
            }
            else
            {
                Win32API.ShowWindow(Win32API.GetConsoleWindow(), Win32API.SW_SHOW);
                showConsoleMenuItem.Checked = true;
            }
        }

        private static void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (mainForm == null)
            {
                CreateForm();
            }
            else
            {
                mainForm.Activate();
            }
        }

        private static void ShowFormMenuItem_Click(object sender, EventArgs e)
        {
            if (mainForm == null)
            {
                CreateForm();
            }
            else
            {
                mainForm.Close();
            }
        }



        private static void MainForm_Closed(object sender, EventArgs e)
        {
            mainForm = null;
            showFormMenuItem.Checked = false;
        }

        private static void MainForm_Shown(object sender, EventArgs e)
        {
            showFormMenuItem.Checked = true;
        }

        private static void CreateForm()
        {
            if (mainForm != null)
            {
                return;
            }
            mainForm = new MainForm(configLoader);
            mainForm.FormClosed += MainForm_Closed;
            mainForm.Shown += MainForm_Shown;

            mainForm.Show();
        }

        private static void Exit()
        {
            Application.Exit();
        }

        private static void SetNetworkConnected(bool status)
        {
            networkConnected = status;
            networkMenuItem.Text = status ? "网络状态：已连接" : "网络状态：无连接";
        }

        private static void NetworkInformation_NetworkStatusChanged(object sender)
        {

            var profile = NetworkInformation.GetInternetConnectionProfile();
            if (profile == null)
            {
                SetNetworkConnected(false);
                Logger.NetworkLogger(false);
            }
            else
            {
                SetNetworkConnected(true);
                Logger.NetworkLogger(true);
            }
        }

        private static void DoHeartbeat()
        {
            if (configLoader.lastError != "" || !networkConnected) return;
            var result = api.Heartbeat();
            Logger.HeartbeatLogger(result);
        }

        private static void AppObserver_OnAppActive(AppObserverEventArgs e)
        {
            if (configLoader.lastError != "" || !networkConnected) return;

            if (e.ProcessName != "")
            {
                if (configLoader.config.ProcessWhitelist.Contains(e.ProcessName))
                {
                    Logger.ProcessLogger(e.ProcessName, true);
                    new System.Threading.Thread(() =>
                    {
                        api.UploadProcess(e.ProcessName);
                    }).Start();
                }
                else
                {
                    Logger.ProcessLogger(e.ProcessName, false);
                }
            }
        }

        private static void SleepDiscover_SleepStatusChanged(SleepStatus sleepStatus)
        {
            if (configLoader.lastError != "" || !networkConnected) return;
            if (sleepStatus == SleepStatus.Sleep)
            {
                Logger.IdleLogger(true);
                new System.Threading.Thread(() =>
                {
                    api.UpdateIdle(true);
                }).Start();
            }
            else if (sleepStatus == SleepStatus.Wake)
            {
                Logger.IdleLogger(false);
                new System.Threading.Thread(() =>
                {
                    api.UpdateIdle(false);
                }).Start();
            }
        }

        private static void MediaObserver_OnMediaUpdate(MediaObserverEventArgs e)
        {
            if (configLoader.lastError != "" || !networkConnected)  return; 
            if (e.Title != "")
            {
                Logger.MediaTimelineLogger(e.Title, e.Artist);
                new System.Threading.Thread(() =>
                {
                    api.UploadMedia(e.Thumbnail, e.Title, e.Artist);
                }).Start();
            }
        }

        private static void MediaObserver_OnPlaybackUpdate(MediaObserverPlaybackEventArgs e)
        {
            if (configLoader.lastError != "" || !networkConnected)  return; 
            Logger.MediaPlaybackLogger(e.isPlaying);
            new System.Threading.Thread(() =>
            {
                api.UploadMediaPlayback(e.isPlaying);
            }).Start();
        }

        static string prevCurrentTime = "";
        static string prevTotalTime = "";

        private static void MediaObserver_OnTimeUpdate(MediaObserverTimeEventArgs e)
        {
            if (configLoader.lastError != "" || !networkConnected) return; 
            if (e.CurrentTime == prevCurrentTime && e.TotalTime == prevTotalTime) return;
            prevCurrentTime = e.CurrentTime;
            prevTotalTime = e.TotalTime;
            Logger.MediaTimelineLogger(e.CurrentTime, e.TotalTime);
            new System.Threading.Thread(() =>
            {
                api.UploadMediaTimeline(e.CurrentTime, e.TotalTime);
            }).Start();
        }
    }
}
