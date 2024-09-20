using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.ShareModel.IMServiceEto;
using YiJian.MasterData.Departments;
using YiJian.MasterData.Domain;

namespace YiJian.MasterData;

/// <summary>
/// 科室应用服务
/// </summary>
[Authorize]
public class DepartmentAppService : MasterDataAppService, IDepartmentAppService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ICapPublisher _capPublisher;
    private readonly IExecuteDepRuleDicRepository _executeDepRuleDicRepository;

    /// <summary>
    /// 科室应用服务
    /// </summary> 
    public DepartmentAppService(
        IDepartmentRepository departmentRepository,
        ICapPublisher capPublisher,
        IExecuteDepRuleDicRepository executeDepRuleDicRepository)
    {
        _departmentRepository = departmentRepository;
        this._capPublisher = capPublisher;
        _executeDepRuleDicRepository = executeDepRuleDicRepository;
    }

    /// <summary>
    /// 创建部门
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<DepartmentData> CreateAsync(DepartmentCreation input)
    {
        if (await _departmentRepository.AnyAsync(p => !p.IsDeleted && p.Name == input.Name))
        {
            throw new EcisBusinessException(message: "科室名称已存在");
        }

        if (await _departmentRepository.AnyAsync(p => !p.IsDeleted && p.Code == input.Code))
        {
            throw new EcisBusinessException(message: "科室编码已存在");
        }

        var department = await _departmentRepository.InsertAsync(
            new Department(
                GuidGenerator.Create(),
                input.Name,
                input.Code,
                input.RegisterCode,
                input.IsActived
            )
        );

        // 发布科室变化消息
        await this._capPublisher.PublishAsync("masterdata.department.changed", new DefaultBroadcastEto());

        return ObjectMapper.Map<Department, DepartmentData>(department);
    }

    /// <summary>
    /// 删除部门
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Guid> DeleteAsync(Guid id)
    {
        if (!await _departmentRepository.AnyAsync(c => !c.IsDeleted && c.Id == id))
        {
            throw new EcisBusinessException(message: "科室不存在");
        }

        await _departmentRepository.DeleteAsync(id);

        // 发布科室变化消息
        await this._capPublisher.PublishAsync("masterdata.department.changed", new DefaultBroadcastEto());

        return id;
    }

    /// <summary>
    /// 获取部门信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DepartmentData> GetAsync(Guid id)
    {
        var department = await _departmentRepository.GetAsync(id);
        return ObjectMapper.Map<Department, DepartmentData>(department);
    }

    /// <summary>
    /// 获取部门列表
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<DepartmentData>> GetListAsync()
    {
        var result = await (await _departmentRepository.GetQueryableAsync())
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .Include(x => x.ConsultingRooms)
            .OrderByDescending(x => x.CreationTime)
            .ToListAsync();
        return new ListResultDto<DepartmentData>(
            ObjectMapper.Map<List<Department>, List<DepartmentData>>(result));
    }

    /// <summary>
    /// 更新科室信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<DepartmentData> UpdateAsync(Guid id, DepartmentUpdate input)
    {
        var department = await _departmentRepository.GetAsync(id);

        department.Edit(input.Name, input.Code, input.RegisterCode, input.IsActived);

        await _departmentRepository.UpdateAsync(department);

        // 发布科室变化消息
        await this._capPublisher.PublishAsync("im.call.department.changed", new DefaultBroadcastEto());

        return ObjectMapper.Map<Department, DepartmentData>(department);
    }

    /// <summary>
    /// 科室查询分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<DepartmentData>> GetPagedListAsync(GetDepartmentInput input)
    {
        var departments = await (await _departmentRepository.GetQueryableAsync())
            .AsNoTracking()
            .Include(x => x.ConsultingRooms)
            .Where(x => !x.IsDeleted)
            .OrderBy(nameof(Department.Name))
            .PageBy(input.SkipCount, input.Size)
            .ToListAsync();

        var items = ObjectMapper.Map<List<Department>, List<DepartmentData>>(departments);

        var totalCount = await (await _departmentRepository.GetQueryableAsync()).Where(x => !x.IsDeleted).CountAsync();

        var result = new PagedResultDto<DepartmentData>(totalCount, items.AsReadOnly());
        return result;
    }

    /// <summary>
    /// 根据规则id获取执行科室字段信息
    /// </summary>
    /// <param name="RuleId"></param>
    /// <returns></returns>
    public async Task<List<ExecuteDepRuleDicDto>> GetExecuteDepRuleDicAsync(int RuleId)
    {
        var list = await _executeDepRuleDicRepository.GetListByRuleIdAsync(RuleId);
        return ObjectMapper.Map<List<ExecuteDepRuleDic>, List<ExecuteDepRuleDicDto>>(list);
    }

    /// <summary>
    /// 关键字查询执行科室字段信息, 支持 执行科室编码，执行科室名称，拼音
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="rid"></param>
    /// <returns></returns>
    public async Task<List<ExecuteDepRuleDicDto>> GetExecuteDepRuleDicByKeyAsync([FromQuery] string keyword, [FromQuery] int? rid)
    {
        //不輸入不給查詢，避免資源浪費
        if (keyword.IsNullOrEmpty()) return new List<ExecuteDepRuleDicDto>();
        //输入all 字符串是查询全部
        if (keyword.ToLower() == "all") keyword = "";
        var list = await _executeDepRuleDicRepository.SearchListByRuleIdAsync(keyword, rid); 
        return ObjectMapper.Map<List<ExecuteDepRuleDic>, List<ExecuteDepRuleDicDto>>(list);
    }

}