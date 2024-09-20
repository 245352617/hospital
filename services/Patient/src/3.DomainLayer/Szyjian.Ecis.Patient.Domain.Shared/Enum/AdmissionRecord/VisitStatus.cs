using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 就诊状态枚举：-1=全部、0=未挂号、1=待就诊、2=过号、3=已退号、4=正在就诊、5=已就诊、6=出科
    /// </summary>
    public enum VisitStatus
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        全部 = -1,

        /// <summary>
        /// 未挂号
        /// </summary>
        [Description("未挂号")]
        未挂号 = 0,

        /// <summary>
        /// 已挂号、未入科
        /// </summary>
        [Description("待就诊")]
        待就诊 = 1,

        /// <summary>
        /// 排队叫号过号
        /// </summary>
        [Description("过号")]
        过号 = 2,

        /// <summary>
        /// 挂号已退号
        /// </summary>
        [Description("已退号")]
        已退号 = 3,

        /// <summary>
        /// 正在就诊
        /// </summary>
        [Description("正在就诊")]
        正在就诊 = 4,

        /// <summary>
        /// 可看作结束就诊
        /// </summary>
        [Description("已就诊")]
        已就诊 = 5,

        /// <summary>
        /// 针对留观区、抢救区患者
        /// </summary>
        [Description("出科")]
        出科 = 6,
    }
}