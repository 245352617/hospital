using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.Health.Report.NursingDocuments.Entities;

namespace YiJian.Health.Report.NursingDocuments
{
    /// <summary>
    /// 护理单
    /// </summary>
    [Comment("护理单")]
    public class NursingDocument : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 护理单
        /// </summary>
        private NursingDocument()
        {

        }

        /// <summary>
        /// 护理单
        /// </summary>
        public NursingDocument(
            Guid id,
            Guid PiId,
            [NotNull] string title,
            string cardNo,
            [NotNull] string patientId,
            [NotNull] string patient,
            string idCard,
            string gender,
            DateTime? dayOfBirth,
            string bedNumber,
            string age,
            DateTime admissionTime,
            string diagnose,
            string deptCode,
            string deptName,
            string emergencyWay,
            string nursingCode = "")
        {
            Id = id;
            PI_ID = PiId;
            Title = title;
            CardNo = cardNo;
            PatientId = patientId;
            Patient = patient;
            IDCard = idCard;
            Gender = gender;
            DayOfBirth = dayOfBirth;
            BedNumber = bedNumber;
            Age = age;
            AdmissionTime = admissionTime;
            Diagnose = diagnose;
            DeptCode = deptCode;
            DeptName = deptName;
            EmergencyWay = emergencyWay;
            NursingCode = nursingCode;
        }

        /// <summary>
        /// 单据标题
        /// </summary>
        [Comment("单据标题")]
        [Required, StringLength(200, ErrorMessage = "单据标题需在200字内")]
        public string Title { get; set; }

        /// <summary>
        /// 就诊卡号
        /// </summary>
        [Comment("就诊卡号")]
        public string CardNo { get; set; }

        /// <summary>
        /// 护理单编码(eg: NS-ED-A009)
        /// </summary>
        [Comment("护理单编码")]
        [Required, StringLength(200, ErrorMessage = "护理单编码需在32字内")]
        public string NursingCode { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary>
        [Comment("患者Id")]
        [Required, StringLength(32, ErrorMessage = "患者Id需在32字内")]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>
        [Comment("患者名称")]
        [Required, StringLength(50, ErrorMessage = "患者名称需在50字内")]
        public string Patient { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [Comment("身份证")]
        [StringLength(18, ErrorMessage = "身份证编号需在18字内")]
        public string IDCard { get; set; }

        /// <summary>
        /// 性别，如果没有身份证需要手工填写性别
        /// </summary>
        [Comment("性别")]
        [StringLength(10)]
        public string Gender { get; set; }

        /// <summary>
        /// 入科当时的年龄
        /// </summary>
        [Comment("入科当时的年龄")]
        [StringLength(10)]
        public string Age { get; set; }

        /// <summary>
        /// 出生日期，如果没有身份证需要手工填写
        /// </summary>
        [Comment("出生日期")]
        public DateTime? DayOfBirth { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        [Comment("床号")]
        [StringLength(32, ErrorMessage = "床号需在32字内")]
        public string BedNumber { get; set; }

        /// <summary>
        /// 入院时间
        /// </summary>
        [Comment("入院时间")]
        public DateTime AdmissionTime { get; set; }

        /// <summary>
        /// 科室编号
        /// </summary>
        [Comment("科室编号")]
        [StringLength(32, ErrorMessage = "科室编号描述需在32字内")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        [Comment("科室名称")]
        [StringLength(50, ErrorMessage = "科室名称描述需在50字内")]
        public string DeptName { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        [Comment("诊断")]
        [StringLength(4000, ErrorMessage = "诊断描述需在4000字内")]
        public string Diagnose { get; set; }

        /// <summary>
        /// 入抢救室的方式【eg: 步行,扶行,抱入,轮椅,平车,救护车】 （The way into the emergency room）
        /// </summary>
        [Comment("入抢救室的方式")]
        [StringLength(20, ErrorMessage = "入抢救室的方式20字内")]
        public string EmergencyWay { get; set; }

        /// <summary>
        /// 全过程唯一ID
        /// </summary>
        [Comment("PI_ID")]
        [Required]
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 病危
        /// </summary>
        [Comment("病危")]
        public bool? IsCriticallyIll { get; set; }

        /// <summary>
        /// 病重
        /// </summary>
        [Comment("病重")]
        public bool? IsSeriouslyIll { get; set; }

        /// <summary>
        /// 病危病重时间
        /// </summary>
        [Comment("病危病重时间")]
        public DateTime? SeriouslyTime { get; set; }

        /// <summary>
        /// 绿通时间
        /// </summary>
        [Comment("绿通时间")]
        public DateTime? GreenTime { get; set; }

        /// <summary>
        /// 绿色通道
        /// </summary>
        [Comment("绿色通道")]
        public bool IsGreen { get; set; }

        /// <summary>
        /// 查房信息
        /// </summary>
        public virtual List<WardRound> WardRounds { get; set; }

        /// <summary>
        /// 动态字段
        /// </summary>
        public virtual List<DynamicField> DynamicFields { get; set; }

        /// <summary>
        /// 护理记录
        /// </summary>
        public virtual List<NursingRecord> NursingRecords { get; set; }

        /// <summary>
        /// 病危病重记录
        /// </summary>
        public virtual List<CriticalIllness> CriticalIllnesses { get; set; }

        /// <summary>
        /// 更新患者信息
        /// </summary> 
        /// <param name="cardNo"></param> 
        /// <param name="idCard"></param>
        /// <param name="gender"></param>
        /// <param name="dayOfBirth"></param>
        /// <param name="bedNumber"></param> 
        /// <param name="diagnose"></param> 
        /// <param name="emergencyWay"></param>
        public void UpdatePatientInfo(
           string cardNo,
           string idCard,
           string gender,
           DateTime? dayOfBirth,
           string bedNumber,
           string diagnose,
           string emergencyWay)
        {
            CardNo = cardNo;
            IDCard = idCard;
            Gender = gender;
            DayOfBirth = dayOfBirth;
            BedNumber = bedNumber;
            Diagnose = diagnose;
            EmergencyWay = emergencyWay;
        }
    }
}
