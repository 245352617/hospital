using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData;

/// <summary>
/// 手术字典API
/// </summary>
[Authorize]
public class OperationAppService:MasterDataAppService,IOperationAppService
{
    private readonly IOperationRepository _repository;

    public OperationAppService(IOperationRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 手术字典查询分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<OperationDto>> GetOperationPagedListAsync(OperationInput input)
    {
        var operations = await _repository.GetPageListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter);
        var items = ObjectMapper.Map<List<Operation>, List<OperationDto>>(operations);

        //这个是不合理的，应该放在 _repository.GetPageListAsync 方法里返回
        var totalCount = await ( await _repository.GetQueryableAsync()).WhereIf(!input.Filter.IsNullOrEmpty(), d=> d.OperationCode.Contains(input.Filter) || d.OperationName.Contains(input.Filter) || input.Filter.Contains(d.OperationCode) || input.Filter.Contains(d.OperationName)).CountAsync();
         
        //var totalCount = AbpEnumerableExtensions.WhereIf(_repository, !input.Filter.IsNullOrEmpty(),
        //    d =>
        //        d.OperationCode.Contains(input.Filter) || d.OperationName.Contains(input.Filter)||input.Filter.Contains(d.OperationCode)||input.Filter.Contains(d.OperationName)
        //).Count();

        var result = new PagedResultDto<OperationDto>(totalCount, items.AsReadOnly());
        return result;
    }
}