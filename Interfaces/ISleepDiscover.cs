﻿using DouSlackingMonitor.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouSlackingMonitor.Interfaces
{
    public interface ISleepDiscover
    {
        /// <summary>
        /// 启动
        /// </summary>
        void Start();

        /// <summary>
        /// 休眠状态发生更改
        /// </summary>
        event SleepDiscoverEventHandler SleepStatusChanged;
    }
}
