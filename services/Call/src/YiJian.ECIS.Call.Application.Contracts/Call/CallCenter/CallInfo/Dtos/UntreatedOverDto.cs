using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.ECIS.Call.CallCenter.Dtos
{
    /// <summary>
    /// 过号
    /// Directory: input
    /// </summary>
    public class UntreatedOverDto
    {
        /// <summary>
        /// 医生 ID
        /// </summary>
        public string DoctorId { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 诊室编码
        /// </summary>
        [Required(ErrorMessage = "诊室编码必传")]
        public string ConsultingRoomCode { get; set; }

        /// <summary>
        /// 诊室名称
        /// </summary>
        public string ConsultingRoomName { get; set; }

        /// <summary>
        /// Ip 诊室固定模式根据Ip 反查诊室编码
        /// </summary>
        public string IpAddr { get; set; }
    }
}
