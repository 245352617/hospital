using System.Collections.Generic;

namespace YiJian.Handover
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Volo.Abp.Validation;

    [Serializable]
    public class DoctorHandoverUpdate
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 交班日期
        /// </summary>
        [DynamicStringLength(typeof(DoctorHandoverConsts), nameof(DoctorHandoverConsts.MaxHandoverDateLength),
            ErrorMessage = "交班日期最大长度不能超过{1}!")]
        [Required(ErrorMessage = "交班日期必填")]
        public string HandoverDate { get; set; }

        /// <summary>
        /// 交班时间
        /// </summary>
        [Required(ErrorMessage = "交班时间必填")]
        public string HandoverTime { get; set; }

        /// <summary>
        /// 交班医生编码
        /// </summary>
        [DynamicStringLength(typeof(DoctorHandoverConsts), nameof(DoctorHandoverConsts.MaxHandoverDoctorCodeLength),
            ErrorMessage = "交班医生编码最大长度不能超过{1}!")]
        [Required(ErrorMessage = "交班医生编码必填")]
        public string HandoverDoctorCode { get; set; }

        /// <summary>
        /// 交班医生名称
        /// </summary>
        [DynamicStringLength(typeof(DoctorHandoverConsts), nameof(DoctorHandoverConsts.MaxHandoverDoctorNameLength),
            ErrorMessage = "交班医生名称最大长度不能超过{1}!")]
        [Required(ErrorMessage = "交班医生名称必填")]
        public string HandoverDoctorName { get; set; }

        /// <summary>
        /// 班次id
        /// </summary>
        [Required(ErrorMessage = "班次id必填")]
        public Guid ShiftSettingId { get; set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        [DynamicStringLength(typeof(DoctorHandoverConsts), nameof(DoctorHandoverConsts.MaxShiftSettingNameLength),
            ErrorMessage = "班次名称最大长度不能超过{1}!")]
        [Required(ErrorMessage = "班次名称必填")]
        public string ShiftSettingName { get; set; }

        /// <summary>
        /// 其他事项
        /// </summary>
        [DynamicStringLength(typeof(DoctorHandoverConsts), nameof(DoctorHandoverConsts.MaxOtherMattersLength),
            ErrorMessage = "其他事项最大长度不能超过{1}!")]
        public string OtherMatters { get; set; }
        /// <summary>
        /// 交班状态，0：未提交，1：已提交
        /// </summary>
        public int Status { get; set; } = 0;
        /// <summary>
        /// 统计
        /// </summary>

        public DoctorPatientStatisticUpdate PatientStatistics { get; set; }

        /// <summary>
        /// 交班患者
        /// </summary>

        public List<DoctorPatientsUpdate> DoctorPatients { get; set; }
    }
}