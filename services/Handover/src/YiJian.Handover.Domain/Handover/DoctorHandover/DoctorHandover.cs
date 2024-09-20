using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Extensions;

namespace YiJian.Handover
{
    public class DoctorHandover : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 交班日期
        /// </summary>
        [Required]
        public DateTime HandoverDate { get; private set; }

        /// <summary>
        /// 交班时间
        /// </summary>
        [Description("交班时间")]
        public DateTime HandoverTime { get; private set; }

        /// <summary>
        /// 交班医生编码
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Description("交班医生编码")]
        public string HandoverDoctorCode { get; private set; }

        /// <summary>
        /// 交班医生名称
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [Description("交班医生名称")]
        public string HandoverDoctorName { get; private set; }

        /// <summary>
        /// 班次id
        /// </summary>
        [Required]
        [Description("班次id")]
        public Guid ShiftSettingId { get; private set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [Description("班次名称")]
        public string ShiftSettingName { get; private set; }

        /// <summary>
        /// 其他事项
        /// </summary>
        [Description("其他事项")]
        [Column(TypeName = "nvarchar(max)")]
        public string OtherMatters { get; private set; }

        /// <summary>
        /// 交班状态，0：未提交，1：提交交班
        /// </summary>
        [Description("交班状态")]
        public int Status { get; private set; } = 0;

        /// <summary>
        /// 统计
        /// </summary>
        public DoctorPatientStatistic PatientStatistics { get; private set; }

        /// <summary>
        /// 交班患者
        /// </summary>
        public ICollection<DoctorPatients> DoctorPatients { get; private set; }

        #region constructor

        public DoctorHandover(Guid id, string handoverDate, DateTime handoverTime, string handoverDoctorCode,
            string handoverDoctorName, Guid shiftSettingId, string shiftSettingName, string otherMatters,
            DoctorPatientStatistic patientStatistics, List<DoctorPatients> doctorPatients, int status) : base(id)
        {
            HandoverDate = DateTime.Parse(handoverDate);

            PatientStatistics = patientStatistics;
            DoctorPatients = doctorPatients;
            Modify(handoverTime, handoverDoctorCode, handoverDoctorName, shiftSettingId, shiftSettingName,
                otherMatters, status);
        }

        #endregion

        #region Modify

        public void Modify(DateTime handoverTime, string handoverDoctorCode, string handoverDoctorName,
            Guid shiftSettingId, string shiftSettingName, string otherMatters, int status)
        {
            HandoverTime = handoverTime;
            HandoverDoctorCode = handoverDoctorCode;
            HandoverDoctorName = handoverDoctorName;
            ShiftSettingId = shiftSettingId;
            ShiftSettingName = shiftSettingName;
            OtherMatters = otherMatters;
            Status = status;
        }

        #endregion

        #region constructor

        private DoctorHandover()
        {
            // for EFCore
        }

        #endregion
    }
}