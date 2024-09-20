using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace YiJian.MasterData.MasterData.Separations.Dto;

/// <summary>
/// 分方配置实体
/// </summary> 
public class SeparationDto : EntityDto<Guid?>
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 打印模板Id
    /// </summary>
    public Guid? PrintSettingId { get; set; }

    /// <summary>
    /// 打印模板名称
    /// </summary> 
    public string PrintSettingName { get; set; }

    /// <summary>
    /// 分方单分类编码，0=注射单，1=输液单，2=雾化单...
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// 用药途经
    /// </summary>
    public virtual List<UsageDto> Usages { get; set; } = new List<UsageDto>();
}