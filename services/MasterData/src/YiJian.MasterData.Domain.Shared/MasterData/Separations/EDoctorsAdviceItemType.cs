using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.MasterData.Separations;

/// <summary>
/// 医嘱各项分类: 0=药品,1=检查项,2=检验项,3=诊疗项
/// </summary>
public enum EDoctorsAdviceItemType : int
{
    /// <summary>
    /// 药品
    /// </summary>
    [Description("药品项")]
    Prescribe = 0,

    /// <summary>
    /// 检查项
    /// </summary>
    [Description("检查项")]
    Pacs = 1,

    /// <summary>
    /// 检验项
    /// </summary>
    [Description("检验项")]
    Lis = 2,

    /// <summary>
    /// 诊疗项
    /// </summary>
    [Description("诊疗项")]
    Treat = 3,

}
 
