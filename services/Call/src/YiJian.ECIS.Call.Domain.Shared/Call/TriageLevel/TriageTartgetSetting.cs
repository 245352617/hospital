using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call
{
    /// <summary>
    /// 分诊去向设置
    /// Author: ywlin
    /// Date: 2021-11-23
    /// </summary>
    public class TriageTartgetSetting
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否同步
        /// </summary>
        public bool IsSync { get; set; }
    }
}
