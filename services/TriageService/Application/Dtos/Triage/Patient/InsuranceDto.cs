using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 医保信息
    /// </summary>
    public class InsuranceDto
    {
        /// <summary>
        /// 医保人员编码
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 就诊凭证编号
        /// </summary>
        public string mdtrt_cert_no { get; set; }

        /// <summary>
        /// 医保人员名称
        /// </summary>
        public string psn_name { get; set; }

        /// <summary>
        /// 人员证件类型
        /// </summary>
        public string psn_cert_type { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string certno { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string gend { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string naty { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? brdy { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public decimal age { get; set; }

        /// <summary>
        /// 参保地医保区划
        /// </summary>
        public string insuplc_admdvs { get; set; }

        /// <summary>
        /// 缴费档次
        /// </summary>
        public List<InsuranceType> InsuranceTypes { get; set; }
    }

    /// <summary>
    /// 医保档次信息
    /// </summary>
    public class InsuranceType
    {
        /// <summary>
        /// 缴费档次，目前对应[Ecis_Triage].[dbo].[Dict_TriageConfig]的extracode
        /// </summary>
        public string clctstd_crtf_rule_codg { get; set; }

        /// <summary>
        /// 险种类型
        /// </summary>
        public string insutype { get; set; }
    }
}
