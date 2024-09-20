namespace YiJian.Job.Models;

/// <summary>
/// hangfire作业配置
/// </summary>
public class HangfireJob
{
    /// <summary>
    /// 作业key
    /// </summary>
    public const string Jobs = "Jobs";

    /// <summary>
    /// 作业的Id 
    /// </summary>
    public string RecurringJobId { get; set; }

    /// <summary>
    /// cron表达式
    /// </summary>
    public string Cron { get; set; }

    /// <summary>
    /// 推送消息队列的路由名称（cap模式）
    /// </summary>
    public string Name { get; set; }
}
