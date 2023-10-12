﻿using DouSlackingMonitor.Events;

namespace DouSlackingMonitor.Interfaces
{
    /// <summary>
    /// 观察者，用于监听应用程序获取焦点事件
    /// </summary>
    public interface IAppObserver
    {
        /// <summary>
        /// 启动观察
        /// </summary>
        void Start();
        /// <summary>
        /// 停止观察
        /// </summary>
        void Stop();

        /// <summary>
        /// 切换活跃APP时发生
        /// </summary>
        event AppObserverEventHandler OnAppActive;
    }
}
