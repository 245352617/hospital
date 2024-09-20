using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 特护单--导管配置
    /// </summary>
    public class CanulaConfig
    {
        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 字段
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary> 
        public bool IsShow { get; set; }
    }
}
