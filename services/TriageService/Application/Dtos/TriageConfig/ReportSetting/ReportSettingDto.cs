using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分诊报表Dto
    /// </summary>
    public class ReportSettingDto : EntityDto<Guid>
    {
        /// <summary>
        /// 报表名称
        /// </summary>
        public string ReportName { get; set; }

        /// <summary>
        /// 报表分类
        /// </summary>
        public string ReportTypeCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnabled { get; set; }

        /// <summary>
        /// 查询语句
        /// </summary>
        public string ReportSql { get; set; }

        /// <summary>
        /// 报表表头
        /// </summary>
        public string ReportHead { get; set; }

        /// <summary>
        /// 报表排序字段
        /// </summary>
        public string ReportSortFiled { get; set; }

        /// <summary>
        /// 排序类型； 0：升序； 1：降序；
        /// </summary>
        public int OrderType { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 数据库连接
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 报表设置查询项Dto
        /// </summary>
        public ICollection<ReportSettingQueryOptionDto> ReportSettingQueryOption { get; set; }
    }
}