namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class CallCancelDto
    {  /// <summary>
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
