using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 描    述:危急值查询Dto
    /// 创 建 人:杨凯
    /// 创建时间:2023/9/26 16:21:18
    /// </summary>
    public class QueryCrisisDto
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 搜索条件
        /// </summary>
        public string QueryParam { get; set; }

        /// <summary>
        /// 处理情况
        /// </summary>
        public bool? IsHandle { get; set; }

        /// <summary>
        /// 处理措施
        /// </summary>
        public string HandleCode { get; set; }

        /// <summary>
        /// 患者PI_ID
        /// </summary>
        public Guid? PI_ID { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int Index { get; set; } = 1;

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; } = 30;
    }
}
