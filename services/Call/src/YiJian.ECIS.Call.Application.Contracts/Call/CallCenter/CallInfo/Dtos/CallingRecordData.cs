using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using YiJian.ECIS.Call.Domain.Converters;

namespace YiJian.ECIS.Call.CallCenter.Dtos
{
    /// <summary>
    /// 叫号记录
    /// </summary>
    public class CallingRecordData
    {
        /// <summary>
        /// 叫号记录 ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 排队号
        /// </summary>
        public string CallingSn { get; set; }

        /// <summary>
        /// 患者 ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 医生 ID
        /// </summary>
        public string DoctorId { get; set; }

        /// <summary>
        /// 医生姓名
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

        /// <summary>
        /// 叫号时间
        /// </summary>
        [JsonConverter(typeof(DateTimeStringJsonConverter))]
        public DateTime CallingTime { get; set; }
    }
}
