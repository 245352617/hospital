namespace YiJian.ECIS.ShareModel.DDPs.Responses
{
    /// <summary>
    /// 描    述:获取检验报告
    /// 创 建 人:杨凯
    /// 创建时间:2023/12/28 14:12:11
    /// </summary>
    public class PKULisReportResponse
    {
        /// <summary>
        /// 报告详情
        /// </summary>
        public List<ReportDetail> Lis { get; set; }
    }

    /// <summary>
    /// 报告详情
    /// </summary>
    public class ReportDetail
    {
        /// <summary>
        /// 报告
        /// </summary>
        public string Report { get; set; }
    }
}
