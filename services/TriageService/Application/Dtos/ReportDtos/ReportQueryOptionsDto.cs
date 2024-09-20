using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 报表查询Dto
    /// </summary>
    public class ReportQueryOptionsDto
    {
        /// <summary>
        /// 跳过页数
        /// </summary>
        /// <example>0</example>
        public int SkipCount { get; set; }

        /// <summary>
        /// 最大条数
        /// </summary>
        /// <example>15</example>
        public int MaxResultCount { get; set; }

        /// <summary>
        /// 要查询报表数据的Id
        /// </summary>
        public Guid ReportId { get; set; }

        /// <summary>
        /// 是否需要分页
        /// </summary>
        public bool IsNeedPaged { get; set; } = true;

        /// <summary>
        /// 查询条件数据（注：传入json字符串，例：{“triageDT”:"2021-07-01~2021-07-02"}）日期数据使用~分割，其它类型数据则正常传值
        /// </summary>
        public string QueryData { get; set; }
    }
}