using System.Collections.Generic;

namespace YiJian.Handover
{
    using System;

    [Serializable]
    public class DoctorHandoverStatisticDataList
    {
        /// <summary>
        /// 交班日期
        /// </summary>
        public string HandoverDate { get; set; }

        /// <summary>
        /// 交班医生名称
        /// </summary>
        public string HandoverDoctorName { get; set; }

        /// <summary>
        /// 班次id
        /// </summary>
        public Guid ShiftSettingId { get; set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        public string ShiftSettingName { get; set; }

        /// <summary>
        /// 接诊总人数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// I级  (病危人数)
        /// </summary>
        public int ClassI { get; set; }

        /// <summary>
        /// II级  (病重人数)
        /// </summary>
        public int ClassII { get; set; }

        /// <summary>
        /// III级
        /// </summary>
        public int ClassIII { get; set; }

        /// <summary>
        /// IV级
        /// </summary>
        public int ClassIV { get; set; }

        /// <summary>
        /// 预术人数
        /// </summary>
        public int PreOperation { get; set; }

        /// <summary>
        /// 现有病人数
        /// </summary>
        public int ExistingDisease { get; set; }

        /// <summary>
        /// 出科人数
        /// </summary>
        public int OutDept { get; set; }

        /// <summary>
        /// 抢救人数
        /// </summary>
        public int Rescue { get; set; }

        /// <summary>
        /// 出诊人数
        /// </summary>
        public int Visit { get; set; }

        /// <summary>
        /// 死亡人数
        /// </summary>
        public int Death { get; set; }

        /// <summary>
        /// 心肺复苏人数
        /// </summary>
        public int CPR { get; set; }

        /// <summary>
        /// 收住院人数
        /// </summary>
        public int Admission { get; set; }

        public List<HandoverHistoryPatientsDataList> DoctorPatients { get; set; }

        public List<DoctorHandoverDetailsData> DoctorHandover { get; set; }
    }
}