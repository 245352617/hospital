using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 通过门诊病人二维码获取门诊病人信息
    /// 龙岗中心设备对接生命体征接口
    /// </summary>
    public class MZPatientInfo
    {
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string SEX { get; set; }

        /// <summary>
        /// 门诊号码
        /// </summary>
        public string OPID { get; set; }

        /// <summary>
        /// 时间
        /// 2020/07/31 17:48:00
        /// format: yyyy/MM/dd HH:mm:ss
        /// </summary>
        public DateTime TIME { get; set; }
    }
}
