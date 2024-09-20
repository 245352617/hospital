using System;

namespace YiJian.Handover
{
    public class NurseHandoverDataList
    {
        /// <summary>
        /// 交班日期
        /// </summary>
        public string HandoverDate { get; set; }

        /// <summary>
        /// 班次id
        /// </summary>
        public Guid ShiftSettingId { get; set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        public string ShiftSettingName { get; set; }

        /// <summary>
        /// 交班护士名称
        /// </summary>
        public string HandoverNurseName { get; set; }

        /// <summary>
        /// 接班护士名称
        /// </summary>
        public string SuccessionNurseName { get; set; }

        /// <summary>
        /// 抢救总人数
        /// </summary>
        public int RescueTotal { get; set; }

        /// <summary>
        /// 抢救一级
        /// </summary>
        public int RescueI { get; set; }

        /// <summary>
        /// 抢救二级级
        /// </summary>
        public int RescueII { get; set; }

        /// <summary>
        /// 抢救三级
        /// </summary>
        public int RescueIII { get; set; }

        /// <summary>
        /// 抢救四级
        /// </summary>
        public int RescueIV { get; set; }
        
        /// <summary>
        /// 留观总人数
        /// </summary>
        public int ObservationTotal { get; set; }

        /// <summary>
        /// 留观一级
        /// </summary>
        public int ObservationI { get; set; }

        /// <summary>
        /// 留观二级级
        /// </summary>
        public int ObservationII { get; set; }

        /// <summary>
        /// 留观三级
        /// </summary>
        public int ObservationIII { get; set; }

        /// <summary>
        /// 留观四级
        /// </summary>
        public int ObservationIV { get; set; }
    }
}