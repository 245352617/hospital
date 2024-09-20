using System;
using Volo.Abp.Application.Dtos;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 报表设置查询项Dto
    /// </summary>
    public class ReportSettingQueryOptionDto : EntityDto<Guid>
    {
        /// <summary>
        /// 报表设置表Id
        /// </summary>
        public Guid ReportSettingId { get; set; }

        /// <summary>
        /// 查询名称
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// 查询字段 
        /// </summary>
        public string QueryFiled { get; set; }

        /// <summary>
        /// 查询类型 0:普通文本 1:日期   2:单选  3:多选 
        /// </summary>
        public string QueryType { get; set; }

        /// <summary>
        /// 数据源：SQL
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 数据源标识：0.sql  1.json字符串
        /// </summary>
        public int DataSourceFlag { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 数据库连接
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
    }
}