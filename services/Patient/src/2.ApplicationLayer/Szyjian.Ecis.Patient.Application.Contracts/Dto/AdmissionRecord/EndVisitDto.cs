namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 结束就诊参数
    /// </summary>
    public class EndVisitDto
    {
        /// <summary>
        /// 最终去向代码
        /// </summary>
        public string LastDirectionCode { get; set; }

        /// <summary>
        /// 最终去向名称
        /// </summary>
        public string LastDirectionName { get; set; }

        /// <summary>
        /// 重点病种
        /// </summary>
        public string KeyDiseasesCode { set; get; }
        /// <summary>
        /// 重点病种名称
        /// </summary>
        public string KeyDiseasesName { set; get; }
    }
}
