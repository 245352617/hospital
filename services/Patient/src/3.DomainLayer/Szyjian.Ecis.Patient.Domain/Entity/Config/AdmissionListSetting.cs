using FreeSql.DataAnnotations;
using System;
using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 患者列表设置
    /// </summary>
    [Table(Name = "Config_TableSetting")]
    public class AdmissionListSetting
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Description("主键Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// 列值
        /// </summary>
        [Description("列值")]
        [Column(DbType = "nvarchar(50)")]
        public string ColumnValue { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        [Description("列名")]
        [Column(DbType = "nvarchar(100)")]
        public string ColumnName { get; set; }

        /// <summary>
        /// 列宽
        /// </summary>
        [Description("列宽")]
        public int ColumnWidth { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [Description("序号")]
        public int SequenceNo { get; set; }

        /// <summary>
        /// 显示 0：不显示  1：显示
        /// </summary>
        [Description(" 显示 0：不显示  1：显示")]
        public bool Visible { get; set; }

        /// <summary>
        /// 单元格内文本是否换行 0：不换行，鼠标移上显示更多  1：换行，展示所有数据
        /// </summary>
        [Description("单元格内文本是否换行 0：不换行，鼠标移上显示更多  1：换行，展示所有数据")]
        public bool ShowOverflowTooltip { get; set; }

        /// <summary>
        /// 列名(默认值)
        /// </summary>
        [Description("列名(默认值)")]
        [Column(DbType = "nvarchar(100)")]
        public string DefaultColumnName { get; set; }

        /// <summary>
        /// 列宽(默认值)
        /// </summary>
        [Description("列宽(默认值)")]
        public int DefaultColumnWidth { get; set; }

        /// <summary>
        /// 序号(默认值)
        /// </summary>
        [Description("序号(默认值)")]
        public int DefaultSequenceNo { get; set; }

        /// <summary>
        /// 显示(默认值) 0：不显示  1：显示
        /// </summary>
        [Description("显示(默认值) 0：不显示  1：显示")]
        public bool DefaultVisible { get; set; }

        /// <summary>
        /// 单元格内文本是否换行(默认值) 0：不换行，鼠标移上显示更多  1：换行，展示所有数据
        /// </summary>
        [Description("单元格内文本是否换行(默认值) 0：不换行，鼠标移上显示更多  1：换行，展示所有数据")]
        public bool DefaultShowOverflowTooltip { get; set; }

        /// <summary>
        /// 列表类型
        /// </summary>
        [Description("列表类型")]
        [Column(DbType = "nvarchar(100)")]
        public string TableTypeCode { get; set; }
    }
}