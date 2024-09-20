using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 医保费别
    /// </summary>
    public class InsuranceFaberDto
    {
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }
    
        /// <summary>
        /// 费别列表
        /// </summary>
        public List<TriageConfigDto> Fabers { get; set; }

        /// <summary>
        /// 参保地代码
        /// </summary>
        public string InsuplcAdmdvCode { get; set; }

        /// <summary>
        /// 默认医保费别
        /// </summary>
        public string DefaultChargeType { get; set; }
    }
}
