using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouSlackingMonitor.Models.MediaObserver
{
    public class MediaObserverTimeEventArgs
    {
        private string _totalTime;
        private string _currentTime;

        public string TotalTime { get { return _totalTime; } }
        public string CurrentTime { get { return _currentTime; } }
        public MediaObserverTimeEventArgs(string totalTime, string currentTime)
        {
            _totalTime = totalTime;
            _currentTime = currentTime;
        }
    }
}