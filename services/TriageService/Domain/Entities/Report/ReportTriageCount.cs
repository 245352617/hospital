using SamJan.MicroService.PreHospital.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 单日人数统计报表
    /// </summary>
    public class ReportTriageCount : Entity<Guid>
    {
        public ReportTriageCount SetId(Guid id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 科室名称
        /// </summary>
        [Description("科室名称")]
        [StringLength(60)]
        public string DeptName { get; set; }

        /// <summary>
        /// 分诊日期
        /// </summary>
        [Description("分诊日期")]
        public DateTime TriageDate { get; set; }

        /// <summary>
        /// 科室 ID
        /// </summary>
        [Description("排序")]
        public int? Sort { get; set; }

        /// <summary>
        /// 系统统计人数
        /// </summary>
        [Description("系统统计人数")]
        public int TriageCount { get; set; }

        /// <summary>
        /// 手动修改人数
        /// </summary>
        [Description("手动修改人数")]
        public int? TriageCountChanged { get; set; }
    }
}