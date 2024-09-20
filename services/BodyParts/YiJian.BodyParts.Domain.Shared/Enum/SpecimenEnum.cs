using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.BodyParts
{
    /// <summary>
    /// 标本类型枚举
    /// </summary>
    public enum SpecimenEnum
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        All = 0,
        /// <summary>
        /// 动脉血
        /// </summary>
        [Description("动脉血")]
        A = 10,
        /// <summary>
        /// 静脉血
        /// </summary>
        [Description("静脉血")]
        V = 20,
        /// <summary>
        /// 毛细管
        /// </summary>
        [Description("毛细管")]
        C = 30,
        /// <summary>
        /// 混合静脉
        /// </summary>
        [Description("混合静脉")]
        M = 40,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        O = 50,
        /// <summary>
        /// 动脉-微量样品
        /// </summary>
        [Description("动脉-微量样品")]
        AM = 60,
        /// <summary>
        /// 静脉-微量样品
        /// </summary>
        [Description("静脉-微量样品")]
        VM = 70,
        /// <summary>
        /// 毛细管-微量样品
        /// </summary>
        [Description("毛细管-微量样品")]
        CM = 80,
        /// <summary>
        /// 混合静脉-微量样品
        /// </summary>
        [Description("混合静脉-微量样品")]
        MM = 90,
        /// <summary>
        /// 其他-微量样品
        /// </summary>
        [Description("其他-微量样品")]
        OM = 100,
        /// <summary>
        /// 全血
        /// </summary>
        [Description("全血")]
        WholeBlood = 110,
        /// <summary>
        /// 动脉
        /// </summary>
        [Description("动脉")]
        Arterial = 120,
        /// <summary>
        /// 静脉
        /// </summary>
        [Description("静脉")]
        Venous = 130,
        /// <summary>
        /// 混合静脉
        /// </summary>
        [Description("混合静脉")]
        MixedVenous = 140,
        /// <summary>
        /// 毛细管
        /// </summary>
        [Description("毛细管")]
        Capillary = 150,
        /// <summary>
        /// 水性
        /// </summary>
        [Description("水性")]
        Aqueous = 160,
        /// <summary>
        /// CPB
        /// </summary>
        [Description("CPB")]
        CPB = 170

    }


}
