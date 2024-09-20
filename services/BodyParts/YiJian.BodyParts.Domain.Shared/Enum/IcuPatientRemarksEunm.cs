using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Domain.Shared.Enum
{

    /// <summary>
    /// 患者备注枚举类
    /// </summary>
    public enum IcuPatientRemarksEunm
    {
        [DbDescription("患者备注类型")]
        Remarks = 0,

        [DbDescription("特护单")]
        Special = 1,
    }
}
