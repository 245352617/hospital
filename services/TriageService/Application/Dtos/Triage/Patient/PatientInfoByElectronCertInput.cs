using System;

namespace TriageService.Application.Dtos.Triage.Patient
{
    /// <summary>
    /// 电子医保凭证入参
    /// </summary>
    public class PatientInfoByElectronCertInput
    {
        /// <summary>
        /// 电子医保凭证
        /// </summary>
        public string ElectronCertNo { get; set; }


        /// <summary>
        /// 参保地区域编码
        /// </summary>
        public string InsuplcAdmdvCode { get; set; }
    }


    /// <summary>
    /// 电子医保凭证返回信息
    /// </summary>
    public class PatientInfoByElectronCertDto
    {
        /// <summary>
        /// 电子医保凭证
        /// </summary>
        public string ElectronCertNo { get; set; }


        /// <summary>
        /// 参保地区域编码
        /// </summary>
        public string ExtraCode { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityNo { get; set; }


        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }


        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? Birthday { get; set; }


    }
}
