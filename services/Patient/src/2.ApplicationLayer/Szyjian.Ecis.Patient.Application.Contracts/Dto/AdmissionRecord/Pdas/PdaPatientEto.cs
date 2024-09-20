using System;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.EventBus;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 描述：pda患者信息同步eto
    /// 创建人： yangkai
    /// 创建时间：2022/11/28 10:10:53
    /// </summary>
    [EventName("CommonEvents")]
    public class PdaPatientEto
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public EPatientEventType PatientEvent { get; set; }

        /// <summary>
        /// 事件名称
        /// </summary>
        public string PatientEventName { get; set; }

        /// <summary>
        /// 患者唯一标识
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 患者类别 紧急(急诊固定传E)
        /// </summary>
        public string PatientClass { get; set; } = "E";

        /// <summary>
        /// 就诊号码
        /// </summary>
        public string VisitNum { get; set; }

        /// <summary>
        /// 诊疗卡号
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string PatientNo { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentifyNo { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 家庭电话号码
        /// </summary>
        public string PhoneNumberHome { get; set; }

        /// <summary>
        /// 工作电话号码
        /// </summary>
        public string PhoneNumberBus { get; set; }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string MaritalStatus { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string EthnicGroup { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// 病区
        /// </summary>
        public string PointCare { get; set; }

        /// <summary>
        /// 病房
        /// </summary>
        public string Room { get; set; }

        /// <summary>
        /// 床位号
        /// </summary>
        public string Bed { get; set; }

        /// <summary>
        /// 床位排序
        /// </summary>
        public int BedOrderId { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string Org { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 再次入院标识符(就诊次数)
        /// </summary>
        public string ReAdmissionIndicator { get; set; }

        /// <summary>
        /// 家庭地址
        /// </summary>
        public string HomeAddress { get; set; }

        /// <summary>
        /// 工作地址
        /// </summary>
        public string OfficeAddress { get; set; }

        /// <summary>
        /// 户口地址
        /// </summary>
        public string NationAddress { get; set; }

        /// <summary>
        /// VIP标识
        /// </summary>
        public string VipIndicator { get; set; }

        /// <summary>
        /// 费用类别 01：自费 02：医保 03：公费
        /// </summary>
        public string PatientType { get; set; }

        /// <summary>
        /// 新生儿标识
        /// </summary>
        public string NewbornBabyIndicator { get; set; }

        /// <summary>
        /// 医保类型
        /// </summary>
        public string ProductionClassCode { get; set; }

        /// <summary>
        /// 主治医生id
        /// </summary>
        public string DoctorId { get; set; }

        /// <summary>
        /// 主治医生姓名
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 责任护士id
        /// </summary>
        public string NurseId { get; set; }

        /// <summary>
        /// 责任护士姓名
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 最近一次诊断描述
        /// </summary>
        public string DiagnosisDescription { get; set; }

        /// <summary>
        /// 死亡日期和时间
        /// </summary>
        public DateTime? DeathDate { get; set; }

        /// <summary>
        /// 死亡标识
        /// </summary>
        public string DeathIndicator { get; set; }

        /// <summary>
        /// 入院、入科时间
        /// </summary>
        public DateTime? AdmitDateTime { get; set; }

        /// <summary>
        /// 出院、出科时间
        /// </summary>
        public DateTime? DischargeTime { get; set; }

        /// <summary>
        /// 绿通标识 1:绿通 0:不是绿通
        /// </summary>
        public int GreenPassFlag { get; set; }

        /// <summary>
        /// 分诊级别
        /// </summary>
        public string TriageLevelStr { get; set; }

        /// <summary>
        /// 床头贴
        /// </summary>
        public string HeadboardSticker { get; set; }

        /// <summary>
        /// 拉入病床时间
        /// </summary>
        public DateTime? IndwellingBaseTime { get; set; }
    }

    /// <summary>
    /// 性别（Java使用的枚举 这边使用常量类处理）
    /// </summary>
    public class PdaSex
    {
        /// <summary>
        /// 男
        /// </summary>
        public const string MALE = "M";

        /// <summary>
        /// 女
        /// </summary>
        public const string FEMALE = "F";

        /// <summary>
        /// 其他
        /// </summary>
        public const string OTHER = "O";

        /// <summary>
        /// 不知道
        /// </summary>
        public const string UNKNOWN = "U";

        /// <summary>
        /// 不明确的
        /// </summary>
        public const string AMBIGUOUS = "A";

        /// <summary>
        /// 不适用
        /// </summary>
        public const string NOTAPPLICABLE = "N";
    }
}
