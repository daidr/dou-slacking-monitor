using DouSlackingMonitor.Models.AppObserver;
using DouSlackingMonitor.Models.MediaObserver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouSlackingMonitor.Events
{
    public delegate void AppObserverEventHandler(AppObserverEventArgs args);
    public delegate void MediaObserverEventHandler(MediaObserverEventArgs args);
    public delegate void MediaObserverTimeEventHandler(MediaObserverTimeEventArgs args);
    public delegate void MediaObserverPlaybackEventHandler(MediaObserverPlaybackEventArgs args);
}
