using System;

namespace YiJian.Handover
{
    [Serializable]
    public class DoctorHandoverDetailsData
    {
        /// <summary>
        /// 交班日期
        /// </summary>
        public string HandoverTime { get; set; }

        /// <summary>
        /// 交班医生名称
        /// </summary>
        public string HandoverDoctorName { get; set; }

        /// <summary>
        /// 其他事项
        /// </summary>
        public string OtherMatters { get; set; }
    }
}