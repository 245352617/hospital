using System.Collections.Generic;

namespace YiJian.Handover
{
    public class StatisticsData
    {
        /// <summary>
        /// 全部患者
        /// </summary>
        public int TotalPatient { get; set; }

        /// <summary>
        /// 已交班
        /// </summary>
        public int Already { get; set; }
        /// <summary>
        /// 未交班
        /// </summary>
        public int Not { get; set; }
        /// <summary>
        /// 一级
        /// </summary>
        public int I { get; set; }

        /// <summary>
        /// 二级
        /// </summary>
        public int II { get; set; }

        /// <summary>
        /// 三级
        /// </summary>
        public int III { get; set; }

        /// <summary>
        /// 四级
        /// </summary>
        public int IV { get; set; }

        /// <summary>
        /// 患者
        /// </summary>
        public List<NurseHandoverData> NursePatients { get; set; }
    }
}