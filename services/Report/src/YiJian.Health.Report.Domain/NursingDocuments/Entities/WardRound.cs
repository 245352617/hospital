using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Health.Report.NursingDocuments
{
    /// <summary>
    /// 查房信息
    /// </summary>
    [Comment("查房信息")]
    public class WardRound : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 级别
        /// </summary> 
        [Comment("级别")]
        [StringLength(50, ErrorMessage = "级别需在50字内")]
        public string Level { get; set; }

        /// <summary>
        /// 签名
        /// </summary> 
        [Comment("签名")]
        public string Signature { get; set; } 

        /// <summary>
        /// 新建页索引
        /// </summary>
        [Comment("护理单记录索引")]
        [Required]
        public int SheetIndex { get; set; }
  
     
        /// <summary>
        /// 护理单Id
        /// </summary>
        [Comment("护理单Id(外键)")]
        [Required]
        public Guid NursingDocumentId { get; set; }

      
    }
}
