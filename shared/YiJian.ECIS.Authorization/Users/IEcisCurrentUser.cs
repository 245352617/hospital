using System.Collections.Generic;
using Volo.Abp.Users;

namespace YiJian.ECIS.Authorization;

/// <summary>
/// 急危重症一体化用户信息，扩展Abp用户接口
/// </summary>
public interface IEcisCurrentUser : ICurrentUser
{
    /// <summary>
    /// 姓名
    /// </summary>
    public string FullName { get; }

    /// <summary>
    /// 用户所属科室
    /// </summary>
    public SzyjDepartment? Dept { get; }

    /// <summary>
    /// 已授权的科室
    /// </summary>
    public IEnumerable<SzyjDepartment>? AuthDept { get; }
}