using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call.CallConfig.TableConfig.Dtos
{
    /// <summary>
    /// 列配置更新
    /// Directory: input
    /// </summary>
    public class RowConfigUpdateItem
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public uint Order { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 列宽
        /// </summary>
        public uint Width { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 是否换号
        /// </summary>
        public bool Wrap { get; set; }
    }
}
