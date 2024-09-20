using System.Collections.Generic;

namespace YiJian.MasterData.Exams
{
    /// <summary>
    /// 描    述:检查ExamProject更新模型只能更新ReservationPlace，ReservationTime，Note，TemplateId
    /// 创 建 人:杨凯
    /// 创建时间:2023/10/10 15:02:24
    /// </summary>
    public class ExamProjectUpdateDto
    {
        /// <summary>
        /// ExamProjectIds
        /// </summary>
        public List<int> Ids { get; set; }

        /// <summary>
        /// 预约地点
        /// </summary>
        public string ReservationPlace { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public string ReservationTime { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 打印模板Id
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// 附加药品编码(多个用','分隔)
        /// </summary>
        public string PrescribeCode { get; set; }

        /// <summary>
        /// 附加药品名称(多个用','分隔)
        /// </summary>
        public string PrescribeName { get; set; }

        /// <summary>
        /// 附加处置编码(多个用','分隔)
        /// </summary>
        public string TreatCode { get; set; }

        /// <summary>
        /// 附加处置名称(多个用','分隔)
        /// </summary>
        public string TreatName { get; set; }
    }
}
