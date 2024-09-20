using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call.CallConfig.Dtos
{
    /// <summary>
    /// 列配置
    /// Directory: output
    /// </summary>
    public class RowConfigDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 列宽
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 是否换号
        /// </summary>
        public bool Wrap { get; set; }

        /// <summary>
        /// 排序（默认值）
        /// </summary>
        public uint DefaultOrder { get; set; }

        /// <summary>
        /// 列名（默认值）
        /// </summary>
        public string DefaultText { get; set; }

        /// <summary>
        /// 列宽（默认值）
        /// </summary>
        public int DefaultWidth { get; set; }

        /// <summary>
        /// 是否显示（默认值）
        /// </summary>
        public bool DefaultVisible { get; set; }

        /// <summary>
        /// 是否换号（默认值）
        /// </summary>
        public bool DefaultWrap { get; set; }
    }
}
