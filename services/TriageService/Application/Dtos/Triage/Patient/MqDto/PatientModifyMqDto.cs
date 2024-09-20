using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 修改档案同步
    /// </summary>
    public class PatientModifyMqDto
    {
        /// <summary>
        /// PatientInfo表Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 患者 ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string SexName { get; set; }
        
        /// <summary>
        /// 出生日期
        /// </summary>
        public string BirthDay { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        /// <example>18381847987</example>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 地址信息
        /// </summary>
        /// <example>广东-深圳-坪山区-马峦街道碧玲新榕路桂花巷5号</example>
        public string Address { get; set; }

        /// <summary>
        /// 证件类型编码
        /// </summary>
        public string IdTypeCode { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public string IdTypeName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityNo { get; set; }

        /// <summary>
        /// 门诊号码（就诊号）
        /// </summary>
        public string VisitNo { get; set; }
    }
}