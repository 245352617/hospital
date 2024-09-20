using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace YiJian.MasterData;


public interface IDepartmentAppService : IApplicationService
{
    Task<DepartmentData> GetAsync(Guid id);
    Task<DepartmentData> CreateAsync(DepartmentCreation input);
    Task<DepartmentData> UpdateAsync(Guid id, DepartmentUpdate input);
    Task<Guid> DeleteAsync(Guid id);
    Task<ListResultDto<DepartmentData>> GetListAsync();
    Task<PagedResultDto<DepartmentData>> GetPagedListAsync(GetDepartmentInput input);
    
    /// <summary>
    /// 根据规则id获取执行科室字段信息
    /// </summary>
    /// <param name="ruleId"></param>
    /// <returns></returns>
    public Task<List<ExecuteDepRuleDicDto>> GetExecuteDepRuleDicAsync(int ruleId);


    /// <summary>
    /// 根据规则id获取执行科室字段信息
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="rid"></param>
    /// <returns></returns>
    public Task<List<ExecuteDepRuleDicDto>> GetExecuteDepRuleDicByKeyAsync(string keyword,int? rid);
}
