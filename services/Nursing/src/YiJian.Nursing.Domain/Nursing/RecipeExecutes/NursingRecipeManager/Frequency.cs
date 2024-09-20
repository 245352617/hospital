using System;

namespace YiJian.Nursing.RecipeExecutes
{
    /// <summary>
    /// 频次信息
    /// </summary>
    public struct Frequency
    {
        /// <summary>
        /// 频次码
        /// </summary>
        public string FrequencyCode;

        /// <summary>
        /// 频次名称
        /// </summary>
        public string FrequencyName;

        /// <summary>
        /// 在一个周期内执行的次数
        /// </summary>
        public int? Times;

        /// <summary>
        /// 周期单位，比如1D表示每天，1W表示每周，2D隔天执行，1H每小时或者每半小时
        /// </summary>
        public string Unit;

        /// <summary>
        /// 一天内的执行时间，格式如："9","8-12-16","00:01,00:30","21:00:00.000","14:00,02:00","周日：08：00","周一：08：00；周四：08：00；"。
        /// 日时间点只有一个的时候，格式为：HH:mm:ss.fff。
        /// 日时间点多个的时候，格式为：HH:mm或者HH，以逗号（,）或者减号（-）分割。
        /// 周时间点一个到多个的时候，格式为：周[一 | 二 | 三 | 四 | 五 | 六 | 日 | 天]:HH:mm，以分号（;）分割。或者就是日时间点，则周几部分就是开嘱的日期对应的周几。
        /// </summary>
        public string ExecuteDayTime;

        /// <summary>
        /// 一天内的执行时间，并将中文符号替换为了英文符号，格式如："00:01,00:30","21:00:00.000","14:00,02:00","周日：08：00","周一：08：00；周四：08：00；"
        /// </summary>
        public string PreparedDayTime { get { return this.ExecuteDayTime == null ? "" : this.ExecuteDayTime.Replace("：", ":").Replace("；", ";").Replace("，", "，").Replace("。", ".").Replace(" ", ""); } }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime;

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime;


    }
}
