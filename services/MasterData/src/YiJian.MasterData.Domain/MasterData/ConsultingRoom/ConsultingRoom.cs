using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.MasterData.Domain;

/// <summary>
/// 诊室
/// </summary>
[Comment("诊室")]
public class ConsultingRoom : AuditedAggregateRoot<Guid>
{
    /// <summary>
    /// 名称
    /// </summary>
    [Comment("名称")]
    public string Name { get; private set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Comment("编码")]
    public string Code { get; private set; }

    /// <summary>
    /// 诊室电脑 IP
    /// </summary>
    [Comment("诊室电脑 IP")]
    public string IP { get; private set; }

    /// <summary>
    /// 叫号屏 IP
    /// </summary>
    [Comment("叫号屏 IP")]
    public string CallScreenIp { get; set; }

    /// <summary>
    /// 是否使用
    /// </summary>
    [Comment("是否使用")]
    public bool IsActived { get; private set; }

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="code"></param>
    /// <param name="ip"></param>
    /// <param name="callScreenIp"></param>
    /// <param name="isActive"></param>
    public ConsultingRoom([NotNull] Guid id, [NotNull] string name, [NotNull] string code, string ip, string callScreenIp, bool isActive)
    {
        Id = Check.NotNull(id, nameof(Id));
        Name = Check.NotNull(name, nameof(Name));
        Code = Check.NotNull(code, nameof(Code));
        IP = ip;
        CallScreenIp = callScreenIp;
        IsActived = isActive;
    }

    /// <summary>
    /// 创建诊室
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="code"></param>
    /// <param name="ip"></param>
    /// <param name="callScreenIp"></param>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public static ConsultingRoom Create([NotNull] Guid id, [NotNull] string name, [NotNull] string code, string ip, string callScreenIp, bool isActive)
    {
        return new ConsultingRoom(id, name, code, ip, callScreenIp, isActive);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="name"></param>
    /// <param name="code"></param>
    /// <param name="ip"></param>
    /// <param name="callScreenIp"></param>
    /// <param name="isActive"></param>
    public void Edit([NotNull] string name, [NotNull] string code, string ip, string callScreenIp, bool isActive)
    {
        Name = Check.NotNull(name, nameof(Name));
        Code = Check.NotNull(code, nameof(Code));
        IP = ip;
        CallScreenIp = callScreenIp;
        IsActived = isActive;
    }

    private ConsultingRoom()
    {
    }
}
