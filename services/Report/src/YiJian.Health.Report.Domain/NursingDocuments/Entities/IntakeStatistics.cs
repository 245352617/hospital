using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Extensions;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingDocuments.Entities
{
    /// <summary>
    /// 入量出量统计
    /// </summary>
    [Comment("入量出量统计")]
    public class IntakeStatistics : Entity<Guid>
    {
        /// <summary>
        /// 统计开始时间
        /// </summary>
        [Comment("统计开始时间")]
        [Required]
        public DateTime Begintime { get; set; }

        /// <summary>
        /// 统计结束时间
        /// </summary>
        [Comment("统计结束时间")]
        [Required]
        public DateTime Endtime { get; set; }

        /// <summary>
        /// 入量总量
        /// </summary>
        [Comment("入量总量")]
        [StringLength(50)]
        public string InIntakesTotal { get; set; }

        /// <summary>
        /// 出量总量
        /// </summary>
        [Comment("出量总量")]
        [StringLength(50)]
        public string OutIntakesTotal { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary>
        [Comment("护理记录Id")]
        [Required]
        public Guid NursingDocumentId { get; set; }

        /// <summary>
        /// 护理记录单SheetIndex,第一页=0
        /// </summary>
        [Comment("护理记录单SheetIndex")]
        public int? SheetIndex { get; set; }

    }
}
