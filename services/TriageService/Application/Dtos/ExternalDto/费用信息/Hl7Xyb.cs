namespace SamJan.MicroService.PreHospital.TriageService
{
    /**
     * 新医保信息段(Hl7Xyb)实体类
     *
     * @author cjd
     * @since 2020-09-01 16:10:28
    */
    public class Hl7Xyb
    {
        /// <summary>
        ///医疗类别
        /// </summary>
        public string medicalCategory {get;set;}
        
        /// <summary>
        ///科室编码
        /// </summary>
        public string dept {get;set;}
        
        /// <summary>
        ///挂号类别
        /// </summary>
        public string registrationCategory {get;set;}
        
        /// <summary>
        ///社保目录编码
        /// </summary>
        public string socialSecurityDirectoryCode {get;set;}
        
        /// <summary>
        ///医疗内部诊疗目录编码
        /// </summary>
        public string internalDiagnosticDirectoryCode {get;set;}
        
        /// <summary>
        ///医疗内部诊疗目录名称
        /// </summary>
        public string internalDiagnosticDirectoryName {get;set;}
        
        /// <summary>
        ///医疗内部诊疗目录金额
        /// </summary>
        public string internalDiagnosticDirectoryMoney {get;set;}
        
        /// <summary>
        ///交易验证码
        /// </summary>
        public string transactionVerificationCode {get;set;}
        
        /// <summary>
        ///社保协议医师编码
        /// </summary>
        public string physiciansCoding {get;set;}
        
        /// <summary>
        ///诊断医生姓名
        /// </summary>
        public string diagnosisDoctorName {get;set;}
        
        /// <summary>
        ///结算类别
        /// </summary>
        public string settlementType {get;set;}
        
        /// <summary>
        ///医药机构结算业务序列号
        /// </summary>
        public string settlementBusinessSerialNumber {get;set;}
        
        /// <summary>
        ///医疗证号
        /// </summary>
        public string medicalCertificateNumber {get;set;}
    }
}