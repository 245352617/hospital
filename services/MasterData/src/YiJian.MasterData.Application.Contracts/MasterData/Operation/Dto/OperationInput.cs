using System;
using Volo.Abp.Application.Dtos;

namespace YiJian.MasterData;

[Serializable]
public class OperationInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}