namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 查询Dto
    /// </summary>
    public class QueueAdmissionRecordInput
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        /// <example>1</example>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        /// <example>20</example>
        public int PageSize { get; set; }

        /// <summary>
        /// 分诊科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 分诊级别
        /// </summary>
        public string ActTriageLevel { get; set; }

    }
}