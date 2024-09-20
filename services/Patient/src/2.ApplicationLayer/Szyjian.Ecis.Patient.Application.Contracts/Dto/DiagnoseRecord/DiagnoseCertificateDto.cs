namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 诊断说明书
    /// </summary>
    public class DiagnoseCertificateDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string GenderName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 地址/单位
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string RegisterNo { get; set; }

        /// <summary>
        /// 诊断信息
        /// </summary>
        public string Diagnose { get; set; }

        /// <summary>
        /// 医生建议
        /// </summary>
        public string DoctorAdvice { get; set; }

        /// <summary>
        /// 建议天数
        /// </summary>
        public int AdvicedRestDays { get; set; }

        /// <summary>
        /// 建议休息开始时间
        /// </summary>
        public string RestDateBegin { get; set; }


        /// <summary>
        /// 建议休息结束时间
        /// </summary>
        public string RestDateEnd { get; set; }


        /// <summary>
        /// 医生签名
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        ///医生盖章
        /// </summary>
        public string DoctorSign { get; set; }

    }


    /// <summary>
    /// 诊断信息
    /// </summary>
    public class DiagnoseInfoDto
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        public string DiagnoseName { get; set; }

    }
}
