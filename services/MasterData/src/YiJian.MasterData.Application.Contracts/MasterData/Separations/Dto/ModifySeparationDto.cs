using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace YiJian.MasterData.MasterData.Separations.Dto;

/// <summary>
/// 分方途径分类
/// </summary> 
public class ModifySeparationDto : EntityDto<Guid>
{
    /// <summary>
    /// 分方途径分类编码，唯一
    /// </summary>
    public int Code { get;set;}

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 用药途经
    /// </summary>
    public virtual List<UsageDto> Usages { get; set; } = new List<UsageDto>();

    /// <summary>
    /// 打印模板Id
    /// </summary>
    public Guid? PrintSettingId { get; set; }

    /// <summary>
    /// 打印模板名称
    /// </summary> 
    public string PrintSettingName { get; set; }

}
