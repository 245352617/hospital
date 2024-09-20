using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 更新患者基本信息Dto
    /// </summary>
    public class UpdatePatientInfoDto
    {
        /// <summary>
        /// 自增Id
        /// </summary>
        public int AR_ID { get; set; }

        /// <summary>
        /// 患者分诊基本信息Id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别编码
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 性别名称
        /// </summary>
        public string SexName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNo { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        public string HomeAddress { get; set; }

        /// <summary>
        /// 是否流感
        /// </summary>
        public bool FluFlag { get; set; }

        /// <summary>
        /// 是否分诊错误
        /// </summary>
        public bool TriageErrorFlag { get; set; }

        /// <summary>
        /// 是否咳嗽
        /// </summary>
        public bool CoughFlag { get; set; }

        /// <summary>
        /// 是否咽痛
        /// </summary>
        public bool ChestFlag { get; set; }

        /// <summary>
        /// 就诊类型编码
        /// </summary>
        [MaxLength(50)]
        public string TypeOfVisitCode { get; set; }

        /// <summary>
        /// 就诊类型编码
        /// </summary>
        [MaxLength(100)]
        public string TypeOfVisitName { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        public string PastMedicalHistory { get; set; }

        /// <summary>
        /// 过敏史
        /// </summary>
        public string AllergyHistory { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        [MaxLength(50)]
        public string ContactsPerson { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        [MaxLength(20)]
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 主诉编码
        /// </summary>
        public string NarrationCode { get; set; }

        /// <summary>
        /// 主诉名称
        /// </summary>
        public string NarrationName { get; set; }

        /// <summary>
        /// 责任医生编码
        /// </summary>
        public string DutyDoctorCode { get; set; }

        /// <summary>
        /// 责任医生名称
        /// </summary>
        public string DutyDoctorName { get; set; }

        /// <summary>
        /// 责任护士编码
        /// </summary>
        public string DutyNurseCode { get; set; }

        /// <summary>
        /// 责任护士名称
        /// </summary>
        public string DutyNurseName { get; set; }
        /// <summary>
        /// RFID
        /// </summary>
        public string RFID { get; set; }

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        public string GuardianIdTypeCode { get; set; }

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        public string GuardianIdTypeName { get; set; }
        /// <summary>
        /// 证件类型（默认居民身份证）
        /// </summary>
        public string IdTypeCode { get; set; }

        /// <summary>
        /// 证件类型（默认居民身份证）
        /// </summary>
        public string IdTypeName { get; set; }
        /// <summary>
        /// 来院方式Code
        /// </summary>
        public string ToHospitalWayCode { get; set; }

        /// <summary>
        /// 来院方式名称
        /// </summary>
        public string ToHospitalWayName { get; set; }
        /// <summary>
        /// 人群编码
        /// </summary>
        public string CrowdCode { get; set; }

        /// <summary>
        /// 人群
        /// </summary>
        public string CrowdName { get; set; }
        /// <summary>
        /// 就诊原因编码
        /// </summary>
        public string VisitReasonCode { get; set; }

        /// <summary>
        /// 就诊原因
        /// </summary>
        public string VisitReasonName { get; set; }
        /// <summary>
        /// 监护人身份证号码
        /// </summary>
        public string GuardianIdCardNo { get; set; }
        /// <summary>
        /// 重点病种编码
        /// </summary>
        public string KeyDiseasesCode { set; get; }

        /// <summary>
        /// 重点病种名称
        /// </summary>
        public string KeyDiseasesName { set; get; }
        /// <summary>
        /// 修改级别编码
        /// </summary>
        public string ModifyLevelCode { get; set; }

        /// <summary>
        /// 修改级别名称
        /// </summary>
        public string ModifyLevelName { get; set; }
        /// <summary>
        /// 监护人/联系人电话
        /// </summary>
        [StringLength(20)]
        public string GuardianPhone { get; set; }

        /// <summary>
        /// 绿色通道编码
        /// </summary>
        public string GreenRoadCode { get; set; }

        /// <summary>
        /// 绿色通道名称
        /// </summary>
        public string GreenRoadName { get; set; }

        public bool IsOpenGreenChannl { get; set; }
    }
}