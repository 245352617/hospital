using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 描述：门口屏叫号Dto API/Treatment/PatientRegistration
    /// </summary>
    public class TerminalCallToHisDto
    {
        /// <summary>
        /// 医生ID
        /// </summary>
        [JsonProperty("doctorID")]
        public string DoctorId { get; set; }

        /// <summary>
        /// 诊室电脑 IP
        /// 龙岗中心测试调用 "192.168.12.220"
        /// </summary>
        [Required]
        [JsonProperty("ipAddress")]
        public string IpAddress { get; set; }

        /// <summary>
        /// 诊室名称
        /// </summary>
        [JsonProperty("clinicName")]
        public string ClinicName { get; set; }

        /// <summary>
        /// 患者ID
        /// </summary>
        [JsonProperty("patientID")]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [JsonProperty("patientName")]
        public string PatientName { get; set; }
    }
}
