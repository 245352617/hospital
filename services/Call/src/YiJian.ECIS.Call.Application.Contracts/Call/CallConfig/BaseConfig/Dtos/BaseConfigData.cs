using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call.CallConfig.Dtos
{
    /// <summary>
    /// 叫号设置【基础设置】查询 DTO
    /// direction：output
    /// </summary>
    [Serializable]
    public class BaseConfigData
    {
        /// <summary>
        /// 当前叫号模式
        /// 1: 诊室固定
        /// 2: 医生变动
        /// </summary>
        public CallMode TomorrowCallMode { get; set; }

        /// <summary>
        /// 模式生效时间
        /// 0: 立即生效（默认）
        /// 1: 次日生效
        /// </summary>
        public RegularEffectTime RegularEffectTime { get; set; }

        /// <summary>
        /// 每日更新号码时间（小时）（0-23）
        /// </summary>
        public ushort TomorrowUpdateNoHour { get; set; }

        /// <summary>
        /// 每日更新号码时间（分钟）（0-59）
        /// </summary>
        public ushort TomorrowUpdateNoMinute { get; set; }

        /// <summary>
        /// 每日更新号码时间
        /// </summary>
        public string TomorrowUpdateTime => $"{TomorrowUpdateNoHour:d2}:{TomorrowUpdateNoMinute:d2}";

        /// <summary>
        /// 当前叫号模式
        /// 1: 诊室固定
        /// 2: 医生变动
        /// </summary>
        public CallMode CurrentCallMode { get; set; }

        /// <summary>
        /// 当前的 每日更新号码时间（小时）（0-23）
        /// </summary>
        public ushort CurrentUpdateNoHour { get; set; }

        /// <summary>
        /// 当前的 每日更新号码时间（分钟）（0-59）
        /// </summary>
        public ushort CurrentUpdateNoMinute { get; set; }

        /// <summary>
        /// 当前的 每日更新号码时间
        /// </summary>
        public string CurrentUpdateTime => $"{CurrentUpdateNoHour:d2}:{CurrentUpdateNoMinute:d2}";

        /// <summary>
        /// 下次更新时间
        /// </summary>
        public string NextUpdateTime
        {
            get
            {
                if (DateTime.Now.Hour < TomorrowUpdateNoHour || DateTime.Now.Minute < TomorrowUpdateNoMinute)
                {
                    return DateTime.Today.ToString("yyyy-MM-dd ") + $"{CurrentUpdateNoHour:d2}:{CurrentUpdateNoMinute:d2}";
                }

                return DateTime.Today.AddDays(1).ToString("yyyy-MM-dd ") + $"{CurrentUpdateNoHour:d2}:{CurrentUpdateNoMinute:d2}";
            }
        }
    }
}
