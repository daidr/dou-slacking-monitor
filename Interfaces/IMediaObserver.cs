using DouSlackingMonitor.Events;

namespace DouSlackingMonitor.Interfaces
{
    /// <summary>
    /// 观察者，用于监听媒体事件
    /// </summary>
    public interface IMediaObserver
    {
        /// <summary>
        /// 启动观察
        /// </summary>
        void Start();

        /// <summary>
        /// 媒体信息改变时发生
        /// </summary>
        event MediaObserverEventHandler OnMediaUpdate;

        /// <summary>
        /// 播放状态改变时发生
        /// </summary>
        event MediaObserverPlaybackEventHandler OnPlaybackUpdate;

        /// <summary>
        /// 播放时间改变时发生
        /// </summary>
        event MediaObserverTimeEventHandler OnTimeUpdate;
    }
}
