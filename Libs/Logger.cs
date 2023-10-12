using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouSlackingMonitor.Libs
{
    public static class Logger
    {
        public static string ProcessObserverString = "Process Observer".Pastel(Color.FromArgb(1, 162, 216));
        public static string IdleObserverString = "Idle Observer".Pastel(Color.FromArgb(252, 154, 3));
        public static string MatchedString = "Matched".Pastel(Color.FromArgb(24, 134, 1));
        public static string IgnoredString = "Ignored".Pastel(Color.FromArgb(101, 113, 124));
        public static string IdleString = "Idle".Pastel(Color.DarkGray);
        public static string AwakeString = "Awake".Pastel(Color.FromArgb(24, 134, 1));
        public static string MediaObserverString = "Media Observer".Pastel(Color.FromArgb(83, 201, 46));

        public static string MetaUpdateString = "MetaUpdate".Pastel(Color.DarkGray);
        public static string PlaybackUpdateString = "PlaybackUpdate".Pastel(Color.DarkGray);
        public static string TimelineUpdate = "TimelineUpdate".Pastel(Color.DarkGray);

        public static Color ValueStringColor = Color.FromArgb(201, 101, 196);

        public static void ProcessLogger(string processName, bool matched)
        {
            string status = matched ? MatchedString : IgnoredString;
            Console.WriteLine($"[{ProcessObserverString} | {status}] {processName}");
        }

        public static void IdleLogger(bool isIdle)
        {
            string idleString = isIdle ? IdleString : AwakeString;
            Console.WriteLine($"[{IdleObserverString}] {idleString}");
        }

        private static string WarpVal(object val)
        {
            return val.ToString().Pastel(ValueStringColor);
        }

        public static void MediaMetaLogger(string title, string artist)
        {
            Console.WriteLine($"[{MediaObserverString} | {MetaUpdateString}] Title: {{{WarpVal(title)}}} - Artist: {{{WarpVal(artist)}}}");
        }

        public static void MediaPlaybackLogger(bool isPlaying)
        {
            Console.WriteLine($"[{MediaObserverString} | {PlaybackUpdateString}] isPlaying: {{{WarpVal(isPlaying)}}}");
        }

        public static void MediaTimelineLogger(string currentTime, string duration)
        {
            Console.WriteLine($"[{MediaObserverString} | {TimelineUpdate}] currentTime: {{{WarpVal(currentTime)}}} - duration: {{{WarpVal(duration)}}}");
        }

        public static void HeartbeatLogger(string result)
        {
            Console.WriteLine($"[Heartbeat] Heartbeat({WarpVal(result)})".Pastel(Color.DarkGray));
        }

        public static void NetworkLogger(bool connected)
        {
            Console.WriteLine($"[Network] 连接状态：{WarpVal(connected)}".Pastel(Color.DarkGray));
        }
    }
}
