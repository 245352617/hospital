using System;


namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 入量出量统计
    /// </summary> 
    public class IntakeStatisticsDto
    {
        /// <summary>
        /// 统计类型（0=班次，1=日期）
        /// </summary> 
        public int StatisticsType { get; set; }

        /// <summary>
        /// 护理单ID
        /// </summary>
        public Guid NursingDocumentId { get; set; }

        /// <summary>
        /// 当前入院护理单的开始时间
        /// </summary>
        public DateTime Begintime { get; set; }

        /// <summary>
        /// 当前入院护理单的结束时间
        /// </summary>
        public DateTime Endtime { get; set; }

        /// <summary>
        /// 护理记录单SheetIndex,第一页=0
        /// </summary>
        public int? SheetIndex { get; set; }
    }
}


