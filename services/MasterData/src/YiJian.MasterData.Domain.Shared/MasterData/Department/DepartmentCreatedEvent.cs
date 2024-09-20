using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EventBus;

namespace YiJian.MasterData;

/// <summary>
/// 接诊事件
/// </summary>
[EventName("ECIS.MasterData.Department.Created")]
public class DepartmentCreatedEvent
{
    /// <summary>
    /// 科室 ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 科室名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 科室编码
    /// </summary>
    public string Code { get; set; }
}
