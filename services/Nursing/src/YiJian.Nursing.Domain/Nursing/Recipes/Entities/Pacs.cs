using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Nursing.Recipes.Entities
{
    /// <summary>
    /// 检查项
    /// </summary>
    [Comment("检查项")]
    public class Pacs : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 医嘱ID
        /// </summary>
        [Comment("医嘱ID")]
        public Guid RecipeId { get; set; }

        /// <summary>
        /// 检查目录编码
        /// </summary>
        [Comment("检查类别编码")]
        [Required, StringLength(20)]
        public string CatalogCode { get; set; }

        /// <summary>
        /// 检查目录名称
        /// </summary>
        [Comment("检查类别")]
        [Required, StringLength(100)]
        public string CatalogName { get; set; }

        /// <summary>
        /// 临床症状
        /// </summary>
        [Comment("临床症状")]
        [StringLength(2000)]
        public string ClinicalSymptom { get; set; }

        /// <summary>
        /// 病史简要
        /// </summary>
        [Comment("病史简要")]
        [StringLength(2000)]
        public string MedicalHistory { get; set; }

        /// <summary>
        /// 检查部位编码
        /// </summary>
        [Comment("检查部位编码")]
        [StringLength(20)]
        public string PartCode { get; set; }

        /// <summary>
        /// 检查部位
        /// </summary>
        [Comment("检查部位")]
        [StringLength(50)]
        public string PartName { get; set; }

        /// <summary>
        /// 目录描述名称 例如心电图申请单、超声申请单
        /// </summary>
        [Comment("分类类型名称 例如心电图申请单、超声申请单")]
        [StringLength(100)]
        public string CatalogDisplayName { get; set; }

        /// <summary>
        /// 是否紧急
        /// </summary>
        [Comment("是否紧急")]
        public bool IsEmergency { get; set; }

        /// <summary>
        /// 是否在床旁
        /// </summary>
        [Comment("是否在床旁")]
        public bool IsBedSide { get; set; }


        #region 报告信息

        /// <summary>
        /// 报告标识
        /// </summary>
        [Comment("报告标识")]
        public bool HasReport { get; set; }

        /// <summary>
        /// 出报告时间
        /// </summary>
        [Comment("出报告时间")]
        public DateTime? ReportTime { get; set; }

        /// <summary>
        /// 确认报告医生编码
        /// </summary>
        [Comment("确认报告医生编码")]
        [StringLength(20)]
        public string ReportDoctorCode { get; set; }

        /// <summary>
        /// 确认报告医生
        /// </summary>
        [Comment("确认报告医生")]
        [StringLength(50)]
        public string ReportDoctorName { get; set; }

        #endregion 报告信息

    }
}
