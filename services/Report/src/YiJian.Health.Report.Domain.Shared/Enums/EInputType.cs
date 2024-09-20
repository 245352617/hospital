using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace YiJian.Health.Report.Enums
{
    /// <summary>
    /// 表单域类型（0=普通文本域,1=单选按钮,2=复选框,3=下拉框）
    /// </summary>
    public enum EInputType : int
    {

        /// <summary>
        /// 普通文本域
        /// </summary>
        [Description("普通文本域")] 
        Text = 0,

        /// <summary>
        /// 单选按钮
        /// </summary>
        [Description("单选按钮")]
        Radio = 1,

        /// <summary>
        /// 复选框
        /// </summary>
        [Description("复选框")]
        Checkbox = 2,

        /// <summary>
        /// 下拉框
        /// </summary>
        [Description("下拉框")]
        Select = 3,
         
    }
}
