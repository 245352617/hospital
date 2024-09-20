using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 预约确认结果
    /// </summary>
    public class ReservationConfirmResultDto
    {
        /// <summary>
        /// 预约流水号
        /// </summary>
        public string SeqNumber { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public string RegDateTime { get; set; }

        /// <summary>
        /// 就诊地点
        /// </summary>
        public string AdmitAddress { get; set; }

        /// <summary>
        /// 预约单内容
        /// </summary>
        public string OrderContent { get; set; }
    }
}
