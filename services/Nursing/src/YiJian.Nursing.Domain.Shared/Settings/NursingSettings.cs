using System;

namespace YiJian.Nursing
{
    /// <summary>
    /// 医嘱执行配置
    /// </summary>
    public class NursingSettings
    {
        /// <summary>
        /// 药品医嘱是否拆顿
        /// </summary>
        public bool NeedSplit { get; set; }

        /// <summary>
        /// 临嘱的执行时间以开嘱时间延后X分钟作为执行时间
        /// </summary>
        public int TempOffsetMinutes { get; set; }

        /// <summary>
        /// 提前几天进行医嘱拆顿，默认0，表示拆顿当天，1表示拆顿明天
        /// </summary>
        public int WorkDayOffset { get; set; }

        /// <summary>
        /// 早上8点开始
        /// </summary>
        public TimeSpan WorkStartDayTime { get; set; }

        /// <summary>
        /// 第二天早上8点结束
        /// </summary>
        public TimeSpan WorkSendDayTime { get; set; }
    }
}