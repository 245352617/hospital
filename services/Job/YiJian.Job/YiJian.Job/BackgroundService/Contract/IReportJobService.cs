namespace YiJian.Job.BackgroundService.Contract;

/// <summary>
/// 报表作业服务
/// </summary>
public interface IReportJobService
{
    /// <summary>
    /// 月度
    /// </summary>
    public void PublishMonth();

    /// <summary>
    /// 季度
    /// </summary>
    public void PublishQuarter();

    /// <summary>
    /// 年度
    /// </summary>
    public void PublishYear();

}
