
namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 建档、挂号出参
    /// </summary>
    public class PatientRespDto
    {
        /// <summary>
        /// 患者病历号
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// 监护人证件号码
        /// </summary>
        public string guardIdType { get; set; }

        ///// <summary>
        ///// 监护人姓名
        ///// </summary>
        //public string guardName { get; set; }

        ///// <summary>
        ///// 监护人证件号码
        ///// </summary>
        //public string guardIdNo { get; set; }

        /// <summary>
        ///诊疗卡号
        /// </summary>
        public string idCard { get; set; }

        /// <summary>
        ///住院号
        /// </summary>
        public string patientNo { get; set; }

        /// <summary>
        ///身份证号
        /// </summary>
        public string identifyNO { get; set; }

        /// <summary>
        ///患者姓名
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        ///出生日期
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string age { get; set; }

        /// <summary>
        ///性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        ///家庭地址
        /// </summary>
        public string homeAddress { get; set; }

        /// <summary>
        ///工作地址
        /// </summary>
        public string officeAddress { get; set; }

        /// <summary>
        /// 费别
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        ///户口地址
        /// </summary>
        public string nationaddress { get; set; }

        /// <summary>
        ///家庭电话号码
        /// </summary>
        public string phoneNumberHome { get; set; }

        /// <summary>
        ///工作电话号码
        /// </summary>
        public string phoneNumberBus { get; set; }

        /// <summary>
        ///婚姻状态
        /// </summary>
        public string maritalStatus { get; set; }

        /// <summary>
        ///患者社会保险号
        /// </summary>
        public string ssnNum { get; set; }

        /// <summary>
        ///民族
        /// </summary>
        public string ethnicGroup { get; set; }

        /// <summary>
        ///国籍
        /// </summary>
        public string nationality { get; set; }

        /// <summary>
        ///患者类别
        /// </summary>
        public string patientClass { get; set; }

        /// <summary>
        ///就诊号码
        /// </summary>
        public string visitNum { get; set; }

        /// <summary>
        ///社保流水号
        /// </summary>
        public string alternateVisitId { get; set; }

        /// <summary>
        ///预约流水号（对应龙岗中心的 regSerialNo）
        /// </summary>
        public string appointmentId { get; set; }

        /// <summary>
        /// 工作信息
        /// </summary>
        public string job { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string weight { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string contactName { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string contactPhone { get; set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string cardType { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string cardNo { get; set; }

        /// <summary>
        /// 看诊日期
        /// </summary>
        public string seeDate { get; set; }

        /// <summary>
        /// 挂号标识
        /// </summary>
        public string registerId { get; set; }

        /// <summary>
        /// 挂号顺序号
        /// </summary>
        public string registerSequence { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        public string registerDate { get; set; }

        /// <summary>
        /// 号别
        /// </summary>
        public string shift { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string deptId { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string @operator { get; set; }

        /// <summary>
        /// 再次就诊标识（就诊次数）
        /// </summary>
        public string visitNo { get; set; }

        /// <summary>
        /// 挂号医生编码
        /// </summary>
        public string doctorCode { get; set; }

        /// <summary>
        /// 是否已取消挂号
        /// 0未取消；2已取消
        /// </summary>
        public string isCancel { get; set; }
        /// <summary>
        /// 医生编码
        /// </summary>
        public string dcotorCode { get; set; }
        /// <summary>
        /// 挂号类型 1：普通号；2专家号；3名专家号
        /// </summary>
        public string regType { get; set; }

        /// <summary>
        /// 就诊类型 1:门诊，2:住院，3:体检
        /// </summary>
        public string patientKind { get; set; }

        /// <summary>
        /// 费用类别
        /// </summary>
        public string chargeType { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string deptName { get; set; }
        /// <summary>
        /// 午别/0：上午；1：下午；2：晚上
        /// </summary>
        public string timeInterval { get; set; }
        /// <summary>
        /// 诊结状态  1:未诊，2：诊结 
        /// </summary>
        public string diagnosis { get; set; }

        /// <summary>
        /// 是否退费  1:退费;0:未退费
        /// </summary>
        public string isRefund { get; set; }
    }
}