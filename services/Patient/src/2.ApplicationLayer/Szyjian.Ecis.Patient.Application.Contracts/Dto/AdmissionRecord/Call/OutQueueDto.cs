using System.ComponentModel.DataAnnotations;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 移除队列
    /// Directory: input
    /// </summary>
    public class OutQueueDto
    {
        /// <summary>
        /// 医生 ID
        /// </summary>
        [Required]
        public string DoctorId { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 诊室编码
        /// </summary>
        [Required]
        public string ConsultingRoomCode { get; set; }
        /// <summary>
        /// Ip 诊室固定模式根据Ip 反查诊室编码
        /// </summary>
        public string IpAddr { get; set; }
    }
}