using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class NursingOrderListDto
    {
        /// <summary>
        /// 护理时间
        /// </summary>
        /// <example></example>
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 药速
        /// </summary>
        /// <example></example>
        public decimal? DrugSpeed { get; set; }

        /// <summary>
        /// 药速单位
        /// </summary>
        public string DrugSpeedUnit { get; set; }

        /// <summary>
        /// 流速
        /// </summary>
        /// <example></example>
        public decimal? Speed { get; set; }

        /// <summary>
        /// 加推
        /// </summary>
        /// <example></example>
        public decimal? Push { get; set; }

        /// <summary>
        /// 输液量
        /// </summary>
        public decimal? DrugDosage { get; set; }

        /// <summary>
        /// 剩余量
        /// </summary>
        public decimal? RestDosage { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        /// <example></example>
        public string DrugState { get; set; }
    }
}
