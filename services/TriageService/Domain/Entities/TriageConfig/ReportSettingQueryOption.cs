using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SamJan.MicroService.PreHospital.Core.BaseEntities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分诊报表查询选项
    /// </summary>
    public class ReportSettingQueryOption : BaseEntity<Guid>
    {
        public ReportSettingQueryOption SetId(Guid id)
        {
            this.Id = id;
            return this;
        }

        /// <summary>
        /// 报表设置表主键Id
        /// </summary>
        [Description("报表设置表主键Id")]
        public Guid ReportSettingId { get; set; }

        /// <summary>
        /// 查询名称
        /// </summary>
        [Description("查询名称")]
        [StringLength(50)]
        public string QueryName { get; set; }

        /// <summary>
        /// 查询字段
        /// </summary>
        [Description("查询字段")]
        public string QueryFiled { get; set; }

        /// <summary>
        /// 查询类型
        /// </summary>
        [Description("查询类型")]
        public string QueryType { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        [Description("数据源")]
        public string DataSource { get; set; }

        /// <summary>
        /// 数据源标识：0.sql  1.json字符串
        /// </summary>
        [Description("数据源标识：0.sql  1.json字符串")]
        public int DataSourceFlag { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Description("显示名称")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [Description("默认值")]
        [StringLength(100)]
        public string DefaultValue { get; set; }
    }
}