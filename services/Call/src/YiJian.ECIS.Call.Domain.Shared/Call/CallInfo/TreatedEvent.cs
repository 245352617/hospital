using System;
using Volo.Abp.EventBus;

namespace YiJian.ECIS.Call
{
    /// <summary>
    /// 接诊事件
    /// </summary>
    [EventName("ECIS.Call.CallCenter.Treated")]
    public class TreatedEvent
    {
        /// <summary>
        /// 叫号信息 ID
        /// </summary>
        public Guid CallInfoId { get; set; }

        /// <summary>
        /// 医生 ID
        /// </summary>
        public string DoctorId { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 诊室代码
        /// </summary>
        public string ConsultingRoomCode { get; set; }

        /// <summary>
        /// 诊室名称
        /// </summary>
        public string ConsultingRoomName { get; set; }
    }
}
