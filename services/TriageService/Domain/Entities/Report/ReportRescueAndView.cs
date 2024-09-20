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
    /// 抢救区、留观区统计报表
    /// </summary>
    public class ReportRescueAndView : Entity<Guid>
    {
        public ReportRescueAndView SetId(Guid id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 区域
        /// </summary>
        [Description("区域")]
        public string AreaCode { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [Description("区域")]
        public string AreaName { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        [Description("项目")]
        [StringLength(60)]
        public string ItemName { get; set; }

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
        public int Count { get; set; }

        /// <summary>
        /// 手动统计人数
        /// </summary>
        [Description("手动统计人数")]
        public int? CountChanged { get; set; }
    }
}
