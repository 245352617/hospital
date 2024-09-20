namespace SamJan.MicroService.PreHospital.TriageService
{
    public class PatientEvent
    {
        /// <summary>
        /// 事件类型10
        /// </summary>
        public string patientEvent { get; set; }

        /// <summary>
        /// 事件名称
        /// </summary>
        public string patientEventName { get; set; }

        /// <summary>
        /// 患者唯一主键
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// 患者类别（O:门诊，I：住院）
        /// </summary>
        public string patientClass { get; set; }

        /// <summary>
        /// 就诊号码
        /// </summary>
        public string visitNum { get; set; }

        /// <summary>
        /// 诊疗卡号
        /// </summary>
        public string idCard { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string patientNo { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string identifyNo { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 家庭电话号码
        /// </summary>
        public string phoneNumberHome { get; set; }

        /// <summary>
        /// 工作电话号码
        /// </summary>
        public string phoneNumberBus { get; set; }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string maritalStatus { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string ethnicGroup { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>
        public string nationality { get; set; }

        /// <summary>
        /// 病区
        /// </summary>
        public string pointCare { get; set; }

        /// <summary>
        /// 病房
        /// </summary>
        public string room { get; set; }

        /// <summary>
        /// 床位号
        /// </summary>
        public string bed { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string org { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string orgName { get; set; }

        /// <summary>
        /// 就诊次数
        /// </summary>
        public string reAdmissionIndicator { get; set; }

        /// <summary>
        /// 家庭地址
        /// </summary>
        public string homeAddress { get; set; }

        /// <summary>
        /// 工作地址
        /// </summary>
        public string officeAddress { get; set; }

        /// <summary>
        /// 户口地址
        /// </summary>
        public string nationAddress { get; set; }

        /// <summary>
        /// VIP标识
        /// </summary>
        public string vipIndicator { get; set; }

        /// <summary>
        /// 费用类别
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        /// 新生儿标识
        /// </summary>
        public string newbornBabyIndicator { get; set; }

        /// <summary>
        /// 医保类型
        /// </summary>
        public string productionClassCode { get; set; }

        /// <summary>
        /// 主治医生id
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        /// 主治医生姓名
        /// </summary>
        public string doctorName { get; set; }

        /// <summary>
        /// 责任护士id
        /// </summary>
        public string nurseId { get; set; }

        /// <summary>
        /// 责任护士姓名
        /// </summary>
        public string nurseName { get; set; }

        /// <summary>
        /// 最近一次诊断描述
        /// </summary>
        public string diagnosisDescription { get; set; }

        /// <summary>
        /// 入院（入科）时间
        /// </summary>
        public string admitDateTime { get; set; }

        /// <summary>
        /// 出院（出科）时间
        /// </summary>
        public string dischargeTime { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string contactName { get; set; }

        /// <summary>
        /// 联系人关系
        /// </summary>
        public string contactRelationship { get; set; }

        /// <summary>
        /// 联系人地址
        /// </summary>
        public string contactAddress { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string contactPhoneNum { get; set; }

        /// <summary>
        /// 联系人工作单位电话
        /// </summary>
        public string contactBusPhoneNum { get; set; }

        /// <summary>
        /// 入院途径
        /// </summary>
        public string admitSource { get; set; }
    }
}