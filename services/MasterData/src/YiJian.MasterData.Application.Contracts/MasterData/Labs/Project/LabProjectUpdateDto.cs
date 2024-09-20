using System.Collections.Generic;

namespace YiJian.MasterData.Labs
{
    /// <summary>
    /// 描    述:检查ExamProject更新模型
    /// 创 建 人:杨凯
    /// 创建时间:2023/10/20 15:01:54
    /// </summary>
    public class LabProjectUpdateDto
    {
        /// <summary>
        /// LabProjectIds
        /// </summary>
        public List<int> Ids { get; set; }

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
