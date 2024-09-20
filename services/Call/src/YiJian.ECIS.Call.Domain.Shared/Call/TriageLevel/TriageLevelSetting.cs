using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call
{
    /// <summary>
    /// 分诊等级设置
    /// Author: ywlin
    /// Date: 2021-11-23
    /// </summary>
    public class TriageLevelSetting
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否同步
        /// </summary>
        public bool IsSync { get; set; }
    }
}
