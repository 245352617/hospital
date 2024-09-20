using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 就诊区域统计(就诊区,留观区,抢救区)
    /// </summary>
    [Comment("就诊区域统计")]
    public class StatisticsArea : Entity<int>
    {
        /// <summary>
        /// 格式输出
        /// </summary>
        [NotMapped]
        public string VisitDateFormat { get; set; }


        /// <summary>
        /// 排序
        /// </summary>
        [NotMapped]
        public int Sort { get; set; }

        /// <summary>
        /// 就诊日期
        /// </summary>
        [Comment("就诊日期")]
        public DateTime VisitDate { get; set; }

        /// <summary>
        /// 就诊区域
        /// </summary>
        [Comment("就诊区域")]
        [StringLength(50)]
        public string AreaName { get; set; }

        /// <summary>
        /// 就诊区域编码
        /// </summary>
        [Comment("就诊区域编码")]
        [StringLength(50)]
        public string AreaCode { get; set; }

        /// <summary>
        /// 就诊数量
        /// </summary>
        [Comment("就诊数量")]
        public int VisitCount { get; set; }


        /// <summary>
        /// 同步时间
        /// </summary>
        [Comment("同步时间")]
        public DateTime SyncDate { get; set; }
    }
}