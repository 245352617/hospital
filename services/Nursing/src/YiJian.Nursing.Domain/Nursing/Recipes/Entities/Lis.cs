using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Nursing.Recipes.Entities
{
    /// <summary>
    /// 检验项
    /// </summary>
    [Comment("检验项")]
    public class Lis : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 医嘱ID
        /// </summary>
        [Comment("医嘱ID")]
        public Guid RecipeId { get; set; }

        /// <summary>
        /// 检验类别编码
        /// </summary>
        [Comment("检验类别编码")]
        [Required, StringLength(20)]
        public string CatalogCode { get; set; }

        /// <summary>
        /// 检验类别
        /// </summary>
        [Comment("检验类别")]
        [Required, StringLength(100)]
        public string CatalogName { get; set; }

        /// <summary>
        /// 临床症状
        /// </summary>
        [Comment("临床症状")]
        [StringLength(2000)]
        public string ClinicalSymptom { get; set; }

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

        #region 标本信息

        /// <summary>
        /// 标本编码
        /// </summary>
        [Comment("标本编码")]
        [StringLength(50)]
        public string SpecimenCode { get; set; }

        /// <summary>
        /// 标本名称
        /// </summary>
        [Comment("标本名称")]
        [StringLength(100)]
        public string SpecimenName { get; set; }

        /// <summary>
        /// 标本采集时间
        /// </summary>
        [Comment("标本采集时间")]
        public DateTime? SpecimenCollectDatetime { get; set; }

        /// <summary>
        /// 标本接收时间
        /// </summary>
        [Comment("标本接收时间")]
        public DateTime? SpecimenReceivedDatetime { get; set; }

        /// <summary>
        /// 标本采集部位编码
        /// </summary>
        [Comment("标本采集部位编码")]
        [StringLength(20)]
        public string SpecimenPartCode { get; set; }

        /// <summary>
        /// 标本采集部位
        /// </summary>
        [Comment("标本采集部位")]
        [StringLength(20)]
        public string SpecimenPartName { get; set; }

        /// <summary>
        /// 标本容器代码
        /// </summary>
        [Comment("标本容器代码")]
        [StringLength(20)]
        public string ContainerCode { get; set; }

        /// <summary>
        /// 标本容器
        /// </summary>
        [Comment("标本容器")]
        [StringLength(20)]
        public string ContainerName { get; set; }

        /// <summary>
        /// 标本容器颜色:0=红帽,1=蓝帽,2=紫帽
        /// </summary>
        [Comment("标本容器颜色:0=红帽,1=蓝帽,2=紫帽")]
        [StringLength(20)]
        public string ContainerColor { get; set; }

        /// <summary>
        /// 标本说明
        /// </summary>
        [Comment("标本说明")]
        [StringLength(200)]
        public string SpecimenDescription { get; set; }

        #endregion 标本信息

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
