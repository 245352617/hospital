using System;
using YiJian.Hospitals.Enums;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 患者信息
    /// </summary>
    public class PatientInfoResponse
    {
        /// <summary>
        /// 病人ID HIS的病人ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号 门诊号/住院号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊类型
        /// </summary>
        public EVisitType PatientType { get; set; }

        /// <summary>
        /// 就诊类型
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string PatientAge { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string WholeOrganizationId { get; set; }

    }

}