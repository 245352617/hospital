using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call.CallCenter.Dtos
{
    /// <summary>
    /// 当前门诊医生
    /// Directory: output
    /// </summary>
    public class ConsultingDoctorDto
    {
        /// <summary>
        /// 科室编码
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 诊室编码
        /// </summary>
        public string ConsultingRoomCode { get; set; }

        /// <summary>
        /// 诊室名称
        /// </summary>
        public string ConsultingRoomName { get; set; }

        /// <summary>
        /// 医生ID
        /// </summary>
        public string DoctorId { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 今日看诊人数
        /// </summary>
        public int TodayConsultingCount { get; set; }
    }
}
