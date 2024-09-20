using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace YiJian.BodyParts.Domain.Shared.Enum
{

    /// <summary>
    /// 异常体征查询枚举
    /// </summary>
    public enum ParaItemNameEnum
    {

        [Description("体温")]
        体温 = 1,

        [Description("心率")]
        心率 = 2,

        [Description("呼吸")]
        呼吸 = 3,

        [Description("无创收缩压")]
        无创收缩压 = 4,

        [Description("无创舒张压")]
        无创舒张压 = 5,

        [Description("血氧饱和度")]
        血氧饱和度 = 6,

        [Description("有创平均压")]
        有创平均压 = 10,
    }
}
