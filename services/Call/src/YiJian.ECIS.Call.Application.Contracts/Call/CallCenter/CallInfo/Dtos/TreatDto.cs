using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call.CallCenter.Dtos
{
    /// <summary>
    /// 接诊
    /// Directory: input
    /// </summary>
    public class TreatDto
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
        public string ConsultingRoomCode { get; set; }

        /// <summary>
        /// 诊室名称
        /// </summary>
        public string ConsultingRoomName { get; set; }
    }
}
