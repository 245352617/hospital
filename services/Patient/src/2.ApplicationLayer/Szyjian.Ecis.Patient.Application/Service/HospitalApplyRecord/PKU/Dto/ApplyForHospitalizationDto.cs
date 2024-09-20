using System;
using System.Collections.Generic;

namespace Patient.Application.Service.HospitalApplyRecord.PKU.Dto
{
    // 急诊转住院入参（急诊与ddp间定义的入参）
    public class ApplyForHospitalizationDto
    {
        #region 病人基本信息、医保、费别

        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 挂号序号
        /// </summary>
        public string RegisterSerialNo { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        public string Native { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// （现居住）住址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 工作单位联系电话
        /// </summary>
        public string CompanyPhone { get; set; }

        /// <summary>
        /// 工作单位地址
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string MaritalStatus { get; set; }

        /// <summary>
        /// 血型
        /// </summary>
        public string BlooodType { get; set; }

        /// <summary>
        /// （紧急）联系人
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// （紧急）联系人电话
        /// </summary>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// （紧急）联系人地址
        /// </summary>
        public string ContactsAddress { get; set; }

        /// <summary>
        /// 医保卡号
        /// </summary>
        public string MedicalCard { get; set; }

        /// <summary>
        /// 费别
        /// </summary>
        public string FeeType { get; set; }

        #endregion


        #region 诊断信息、过敏信息、病情、绿通标识等信息

        /// <summary>
        /// 诊断列表
        /// </summary>
        public List<Diagnose> Diagnoses { get; set; }

        /// <summary>
        /// 过敏史
        /// </summary>
        public string AllergyHistory { get; set; }

        /// <summary>
        /// 病情（如：危、重、一般；具体根各医院情况来定）
        /// </summary>
        public int Condition { get; set; }

        /// <summary>
        /// 绿通标识
        /// </summary>
        public string GreenChannelIdentification { get; set; }

        #endregion


        #region 申请医生及科室信息

        /// <summary>
        /// 申请医生编码（工号）
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 申请医生姓名
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 申请科室名称
        /// </summary>
        public string DeptName { get; set; }

        #endregion


        #region 申请住院相关信息

        /// <summary>
        /// 申请入院日期
        /// </summary>
        public DateTime DateOfAdmission { get; set; }

        /// <summary>
        /// 入院方式编码
        /// </summary>
        public int AdmissionModeCode { get; set; }

        /// <summary>
        /// 入院方式
        /// </summary>
        public string AdmissionModeName { get; set; }

        /// <summary>
        /// 住院科室编码
        /// </summary>
        public string InpatientDepartmentCode { get; set; }

        /// <summary>
        /// 住院科室名称
        /// </summary>
        public string InpatientDepartmentName { get; set; }

        /// <summary>
        /// 病区收治编码
        /// </summary>
        public string AdmissionTypeCode { get; set; }

        /// <summary>
        /// 病区收治名称
        /// </summary>
        public string AdmissionTypeName { get; set; }

        /// <summary>
        /// 预交金
        /// </summary>
        public decimal AdvancePaymentAmount { get; set; }

        /// <summary>
        /// 预交金付款方式
        /// </summary>
        public string AdvancePayWay { get; set; }

        /// <summary>
        /// 是否十日内再入院
        /// </summary>
        public bool TenDaysAdmission { get; set; }

        /// <summary>
        /// 特殊病人（ 0 -正常， 1 -日间手术， 2 -抢救通道， 3 -绿色通道， 4-eras 通道， 5 -发热留观， 6 -急诊留观）
        /// </summary>
        public string SpecialPatient { get; set; }

        /// <summary>
        /// 门急诊标志
        /// </summary>
        public int EmergencySign { get; set; }

        #endregion


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 扩展字段，存储院方有特殊要求的字段
        /// </summary>
        public Dictionary<string, object> ExtendedFields { get; set; } = new Dictionary<string, object>();
    }

    /// <summary>
    /// 诊断类
    /// </summary>
    public class Diagnose
    {
        /// <summary>
        /// 诊断编码（ICD10）
        /// </summary>
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        public string DiagnoseName { get; set; }
    }
}