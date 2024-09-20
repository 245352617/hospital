using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.MasterData;

/// <summary>
/// 接收数据日志
/// </summary>
[Comment("接收数据日志")]
public class ReceivedLog : Entity<Guid>
{
    /// <summary>
    /// 路由键
    /// </summary>
    [StringLength(20)]
    [Comment("路由键")]
    public string RouteKey { get; set; }

    /// <summary>
    /// 队列
    /// </summary>
    [StringLength(20)]
    [Comment("队列")]
    public string Queue { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    [StringLength(int.MaxValue)]
    [Comment("内容")]
    public string Content { get; set; }

    /// <summary>
    /// 重试次数
    /// </summary>
    [Comment("重试次数")]
    public int Retries { get; set; }

    /// <summary>
    /// 时间
    /// </summary>
    [Comment("新增时间")]
    public DateTime Added { get; set; }

    /// <summary>
    /// 终止时间
    /// </summary>
    [Comment("终止时间")]
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [StringLength(20)]
    [Comment("状态")]
    public string StatusName { get; set; }
}
