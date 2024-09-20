using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call.CallCenter.Dtos
{
    /// <summary>
    /// 取消呼叫
    /// Directory: input
    /// </summary>
    public class CallCancelDto
    {
        /// <summary>
        /// 医生 ID
        /// </summary>
        public string DoctorId { get; set; }

        /// <summary>
        /// 诊室编码
        /// </summary>
        public string ConsultingRoomCode { get; set; }
        /// <summary>
        /// Ip 诊室固定模式根据Ip 反查诊室编码
        /// </summary>
        public string IpAddr { get; set; }
    }
}
