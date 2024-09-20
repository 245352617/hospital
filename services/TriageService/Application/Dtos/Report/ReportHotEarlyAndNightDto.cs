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
    /// 早八晚八发热统计报表
    /// </summary>
    public class ReportHotMorningAndNightDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [Description("日期")]
        public DateTime Date { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        [Description("科室编码")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        [Description("科室名称")]
        public string DeptName { get; set; }

        /// <summary>
        /// 科室 ID
        /// </summary>
        [Description("排序")]
        public int? Sort { get; set; }

        /// <summary>
        /// 早八统计人数
        /// </summary>
        [Description("早八统计人数")]
        public int MorningCount { get; set; }

        /// <summary>
        /// 晚八统计人数
        /// </summary>
        [Description("晚八统计人数")]
        public int NightCount { get; set; }

        /// <summary>
        /// 早八统计人数（修改）
        /// </summary>
        [Description("早八统计人数（修改）")]
        public int? MorningCountChanged { get; set; }

        /// <summary>
        /// 晚八统计人数（修改）
        /// </summary>
        [Description("晚八统计人数（修改）")]
        public int? NightCountChanged { get; set; }

        /// <summary>
        /// 早八最终统计人数
        /// </summary>
        public int MorningCountResult { get { return MorningCountChanged ?? MorningCount; } }

        /// <summary>
        /// 晚八最终统计人数
        /// </summary>
        public int NightCountResult { get { return NightCountChanged ?? NightCount; } }
    }
}
