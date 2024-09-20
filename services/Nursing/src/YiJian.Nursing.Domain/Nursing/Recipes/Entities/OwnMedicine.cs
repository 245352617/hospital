using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Nursing.Recipes.Entities
{
    /// <summary>
    /// 描述：自备药
    /// 创建人： yangkai
    /// 创建时间：2022/10/27 16:41:24
    /// </summary>
    [Comment("自备药")]
    public class OwnMedicine : Entity<int>
    {
        /// <summary>
        /// 医生站的OwnMedicineId
        /// </summary>
        [Required]
        [Comment("医生站OwnMedicineId")]
        public int OwnMedicineId { get; set; }

        /// <summary>
        /// 系统标识: 0=急诊，1=院前
        /// </summary>
        [Comment("系统标识: 0=急诊，1=院前")]
        public EPlatformType PlatformType { get; set; }

        /// <summary>
        /// 患者唯一标识
        /// </summary>
        [Comment("患者唯一标识")]
        public Guid PIID { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary>
        [Comment("患者Id")]
        [StringLength(20)]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>
        [Comment("患者名称")]
        [StringLength(30)]
        public string PatientName { get; set; }

        /// <summary>
        /// 医嘱编码
        /// </summary>
        [Comment("医嘱编码")]
        [Required, StringLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 医嘱名称
        /// </summary>
        [Comment("医嘱名称")]
        [Required, StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 开嘱时间
        /// </summary>
        [Comment("开嘱时间")]
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 申请医生编码
        /// </summary>
        [Comment("申请医生编码")]
        [Required, StringLength(20)]
        public string ApplyDoctorCode { get; set; }

        /// <summary>
        /// 申请医生
        /// </summary>
        [Comment("申请医生")]
        [Required, StringLength(50)]
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary>
        [Comment("申请科室编码")]
        [StringLength(50)]
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请科室
        /// </summary>
        [Comment("申请科室")]
        [StringLength(50)]
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 领量(数量)
        /// </summary>
        [Comment("领量(数量)")]
        public decimal RecieveQty { get; set; }

        /// <summary>
        /// 领量单位
        /// </summary>
        [Comment("领量单位")]
        [StringLength(20)]
        public string RecieveUnit { get; set; }


        /// <summary>
        /// 用法编码
        /// </summary>
        [Comment("用法编码")]
        [Required, StringLength(20)]
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary>
        [Comment("用法名称")]
        [Required, StringLength(20)]
        public string UsageName { get; set; }

        /// <summary>
        /// 每次剂量
        /// </summary>
        [Comment("每次剂量")]
        [Required]
        public decimal DosageQty { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        [Comment("剂量单位")]
        [Required, StringLength(20)]
        public string DosageUnit { get; set; }

        /// <summary>
        /// 频次码
        /// </summary>
        [Comment("频次码")]
        [Required, StringLength(20)]
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        [Comment("频次")]
        [Required, StringLength(20)]
        public string FrequencyName { get; set; }

        /// <summary>
        /// 在一个周期内执行的次数
        /// </summary>
        [Comment("在一个周期内执行的次数")]
        public int? FrequencyTimes { get; set; }

        /// <summary>
        /// 医嘱说明
        /// </summary>
        [Comment("医嘱说明")]
        [StringLength(500)]
        public string Remarks { get; set; }
    }
}
