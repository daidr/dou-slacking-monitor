using System;
using System.Drawing;
using System.Windows.Forms;
using DouSlackingMonitor.Libs;

namespace DouSlackingMonitor
{
    public partial class MainForm : Form
    {
        private readonly ConfigLoader configLoader;

        private string BlurSecret(string secret)
        {
            var repeat = "".ToString().PadLeft(secret.Length - 8, '*');
            return secret.Substring(0, 4) + repeat + secret.Substring(secret.Length - 4, 4);
        }

        public MainForm(ConfigLoader configLoader)
        {
            InitializeComponent();

            this.configLoader = configLoader;
        }

        private void SetNormalLabel(string text)
        {
            label1_out.Text = text;
            label1_out.ForeColor = SystemColors.ControlText;
        }

        private void SetWarningLabel(string text)
        {
            label1_out.Text = text;
            label1_out.ForeColor = Color.IndianRed;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void MinButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ReloadButton_Click(object sender, EventArgs e)
        {
            configLoader.Load();
        }

        private void ConfigLoader_OnLoad(object sender, EventArgs e)
        {
            if (configLoader.lastError != "")
            {
                SetWarningLabel(configLoader.lastError);
                label2_out.Text = "-";
                label3_out.Text = "-";
                label4_out.Text = "-";
                return;
            }
            else
            {
                SetNormalLabel("正常");
                label2_out.Text = configLoader.config.ApiEntrypoint;
                label3_out.Text = BlurSecret(configLoader.config.Secret);
                label4_out.Text = configLoader.config.ProcessWhitelist.Count.ToString();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigLoader_OnLoad(null, null);
            configLoader.ConfigLoaded += ConfigLoader_OnLoad;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            configLoader.ConfigLoaded -= ConfigLoader_OnLoad;
        }
    }
}
