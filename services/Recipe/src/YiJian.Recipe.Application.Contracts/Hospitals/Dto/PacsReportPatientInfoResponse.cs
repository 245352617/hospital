using System;
using YiJian.Hospitals.Enums;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 患者信息
    /// </summary>
    public class PacsReportPatientInfoResponse
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
        /// 住院次数 门诊病人则为空
        /// </summary>
        public string VisitNumber { get; set; }

        /// <summary>
        /// 就诊类型
        /// </summary>
        public EPatientType PatientType { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string PatientGender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? PatientBirthDay { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string PatientAge { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string WholeOrganizationId { get; set; }

        /// <summary>
        /// 诊断代码
        /// </summary>
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 病史
        /// </summary>
        public string MedicalHistory { get; set; }
    }

}