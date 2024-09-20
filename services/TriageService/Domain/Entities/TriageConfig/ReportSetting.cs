using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.PreHospital.Core.BaseEntities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分诊报表设置
    /// </summary>
    public class ReportSetting : BaseEntity<Guid>
    {
        public ReportSetting SetId(Guid id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 报表名称
        /// </summary>
        [Description("报表名称")]
        [StringLength(50)]
        public string ReportName { get; set; }

        /// <summary>
        /// 报表分类
        /// </summary>
        [Description("报表分类")]
        [StringLength(20)]
        public string ReportTypeCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Description("是否启用")]
        public int IsEnabled { get; set; }

        /// <summary>
        /// 查询语句
        /// </summary>
        [Description("查询语句")]
        public string ReportSql { get; set; }

        /// <summary>
        /// 报表表头
        /// </summary>
        [Description("报表表头")]
        public string ReportHead { get; set; }

        /// <summary>
        /// 报表排序字段
        /// </summary>
        [Description("报表排序字段")]
        [StringLength(50)]
        public string ReportSortFiled { get; set; }

        /// <summary>
        /// 排序类型； 0：升序； 1：降序；
        /// </summary>
        [Description("排序类型； 0：升序； 1：降序；")]
        public int OrderType { get; set; }
        /// <summary>
        /// 数据库连接
        /// </summary>
        [Description("数据库连接")]
        [StringLength(100)]
        public string ConnectionString { get; set; }
        /// <summary>
        /// 分诊报表查询选项
        /// </summary>
        [IsNeedComment(IsNeed = false)]
        public ICollection<ReportSettingQueryOption> ReportSettingQueryOption { get; set; }

    }
}