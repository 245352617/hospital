namespace YiJian.MasterData.External.LongGang.ExamAndLab
{
    /// <summary>
    /// 描    述:获取检查附加药品和处置
    /// 创 建 人:杨凯
    /// 创建时间:2023/11/7 14:20:38
    /// </summary>
    public class ExamMedicineCodeEto
    {
        /// <summary>
        /// 检查编码
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 药品或处置编码
        /// </summary>
        public string MedicineCode { get; set; }

        /// <summary>
        /// 剂量或数量
        /// </summary>
        public float Qty { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
    }
}
