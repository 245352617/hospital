using System.Security.Permissions;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 患者建档入参Dto
    /// </summary>
    public class CreatePatientInfoRepDto
    {
        /// <summary>
        /// 证件号码
        /// </summary>
        public string IDNumber { get; set; }
        
        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }
        
        /// <summary>
        /// 男/女/未知
        /// </summary>
        public string Sex { get; set; }
        
        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birthday { get; set; }
        
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }
        
        /// <summary>
        /// 费别
        /// </summary>
        public string ChargeType { get; set; }
        
        /// <summary>
        /// 个人电话
        /// </summary>
        public string PhoneNum { get; set; }
        
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactPerson { get; set; }
        
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactPhone { get; set; }
        
        /// <summary>
        /// 家庭地址
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// 操作员
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 卡类型（就诊卡、医保卡）
        /// </summary>
        public string CardType { get; set; }
        
        /// <summary>
        /// 卡号（就诊卡号、医保卡号-电脑号）
        /// </summary>
        public string CardNo { get; set; }
        
        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }
    }
}