using System;
using System.ComponentModel.DataAnnotations;

namespace YiJian.OwnMediciness.Dto
{
    /// <summary>
    /// 自备药
    /// </summary>
    public class OwnMedicineDto
    {
        // /// <summary>
        // /// 系统标识: 0=急诊，1=院前
        // /// </summary> 
        // public EPlatformType PlatformType { get; set; }
        //
        // /// <summary>
        // /// 患者唯一标识
        // /// </summary> 
        // public Guid PIID { get; set; }
        //
        // /// <summary>
        // /// 患者Id
        // /// </summary> 
        // [StringLength(20)]
        // public string PatientId { get; set; }
        //
        // /// <summary>
        // /// 患者名称
        // /// </summary> 
        // [StringLength(30)]
        // public string PatientName { get; set; }


        /// <summary>
        /// 前端定位索引，自己录入自己定位
        /// </summary> 
        public int Index { get; set; }

        /// <summary>
        /// 医嘱编码
        /// </summary> 
        [StringLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 医嘱名称
        /// </summary> 
        [Required(ErrorMessage = "药品名称必填"), StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 开嘱时间
        /// </summary> 
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 申请医生编码
        /// </summary> 
        [Required(ErrorMessage = "申请医生编码必填"), StringLength(20)]
        public string ApplyDoctorCode { get; set; }

        /// <summary>
        /// 申请医生
        /// </summary> 
        [Required(ErrorMessage = "申请医生必填"), StringLength(50)]
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary> 
        [StringLength(50)]
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请科室
        /// </summary> 
        [StringLength(50)]
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 领量(数量)
        /// </summary> 
        public decimal? RecieveQty { get; set; }

        /// <summary>
        /// 领量单位
        /// </summary> 
        [StringLength(20)]
        public string RecieveUnit { get; set; }


        /// <summary>
        /// 用法编码
        /// </summary> 
        [StringLength(20)]
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary> 
        [StringLength(20)]
        public string UsageName { get; set; }

        /// <summary>
        /// 每次剂量
        /// </summary> 
        public decimal? DosageQty { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary> 
        [StringLength(20)]
        public string DosageUnit { get; set; }

        /// <summary>
        /// 频次码
        /// </summary> 
        [StringLength(20)]
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 频次
        /// </summary> 
        [StringLength(20)]
        public string FrequencyName { get; set; }

        /// <summary>
        /// 在一个周期内执行的次数
        /// </summary> 
        public int? FrequencyTimes { get; set; }

        /// <summary>
        /// 天数
        /// </summary>
        public int? LongDays { get; set; } = 1;

        /// <summary>
        /// 医嘱说明
        /// </summary> 
        [StringLength(500)]
        public string Remarks { get; set; }
    }
}
