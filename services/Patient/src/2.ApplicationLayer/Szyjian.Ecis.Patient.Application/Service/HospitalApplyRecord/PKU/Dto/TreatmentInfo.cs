namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 描    述 ：
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/7/25 15:12:29
    /// </summary>
    public class TreatmentInfo
    {
        /// <summary>
        /// 小组序号
        /// </summary>
        public string XZXH { get; set; }

        /// <summary>
        /// 小组名称
        /// </summary>
        public string XZMC { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string KSDM { get; set; }

        /// <summary>
        /// 拼音代码
        /// </summary>
        public string PYDM { get; set; }

        /// <summary>
        /// 小组负责人工号
        /// </summary>
        public string XZFZR { get; set; }
    }
}
