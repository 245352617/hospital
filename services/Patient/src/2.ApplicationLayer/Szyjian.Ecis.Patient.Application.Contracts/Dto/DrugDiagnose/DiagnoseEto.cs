namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class DiagnoseEto
    {
        /// <summary>
        /// 1.西医诊断  2.中医诊断
        /// </summary>
        public int DiagType { get; set; }
        /// <summary>
        /// his内部配合diagType唯一识别疾病诊断
        /// </summary>
        public string HisCode { get; set; }
        /// <summary>
        /// 拼英代码
        /// </summary>
        public string SpellCode { get; set; }
        /// <summary>
        /// 诊断名称
        /// </summary>
        public string DiagName { get; set; }
        /// <summary>
        /// 标准icd10编码
        /// </summary>
        public string Icd10 { get; set; }

        /// <summary>
        /// 报卡类型
        /// </summary>
        public string CardrepType { get; set; }
    }
}