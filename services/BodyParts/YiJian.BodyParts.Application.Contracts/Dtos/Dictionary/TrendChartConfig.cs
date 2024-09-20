using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 特护单--趋势图配置
    /// </summary>
    public class TrendChartConfig
    {
        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 项目代码
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary> 
        public bool IsShow { get; set; }

        /// <summary>
        /// 显示图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 显示颜色
        /// </summary>
        public string Color { get; set; }
    }
}
