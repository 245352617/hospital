using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.MasterData.DictionariesMultitypes;

/// <summary>
/// 字典多类型API
/// </summary>
[Authorize]
public class DictionariesMultitypeAppService : MasterDataAppService, IDictionariesMultitypeAppService
{
    private readonly IDictionariesMultitypeRepository _DictionariesMultitypeRepository;

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="DictionariesMultitypeRepository"></param>
    public DictionariesMultitypeAppService(IDictionariesMultitypeRepository DictionariesMultitypeRepository)
    {
        _DictionariesMultitypeRepository = DictionariesMultitypeRepository;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增/修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<Guid> SaveAsync(DictionariesMultitypeDto input)
    {
        var dictionariesMultitype = ObjectMapper.Map<DictionariesMultitypeDto, DictionariesMultitype> (input);
        if (input.Id == Guid.Empty)
        {
            if (await (await _DictionariesMultitypeRepository.GetQueryableAsync()).AnyAsync(
                    x => x.Code == input.Code))
            {
                Oh.Error("编码已存在"); 
            }
            var result =  await _DictionariesMultitypeRepository.InsertAsync(dictionariesMultitype);
            return result.Id;
        }
        else
        {
            var typeModel = await _DictionariesMultitypeRepository.FindAsync(input.Id);
            if (typeModel == null)
                Oh.Error("数据不存在");
            typeModel.Modify(dictionariesMultitype);
            var result = await _DictionariesMultitypeRepository.UpdateAsync(typeModel);
            return result.Id;
        }
   
    }

    #endregion Create

    #region Get

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DictionariesMultitypeDto> GetAsync(Guid id)
    {
        var DictionariesMultitype = await _DictionariesMultitypeRepository.GetAsync(id);
        return ObjectMapper.Map<DictionariesMultitype, DictionariesMultitypeDto>(DictionariesMultitype);
    }

    #endregion Get

    #region delete

    /// <summary>
    /// delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(Guid id)
    {
        var dto = await _DictionariesMultitypeRepository.GetAsync(id);
        if (dto == null) Oh.Error("数据不存在");
        await _DictionariesMultitypeRepository.DeleteAsync(id);
    }

    #endregion 删除

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<DictionariesMultitypeGroupDto>> GetListAsync(
        string filter = null,
        string sorting = null)
    {
        var data = new List<DictionariesMultitypeGroupDto>();
        var result = await (await _DictionariesMultitypeRepository.GetQueryableAsync())
            .Where(p=>p.Status)
            .WhereIf(!string.IsNullOrWhiteSpace(filter), p => p.Name.Contains(filter))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "Sort" : sorting).ToListAsync();

        var mapData =
            ObjectMapper.Map<List<DictionariesMultitype>, List<DictionariesMultitypeDto>>(result);
        var groupList = mapData.GroupBy(p => new { GroupName = p.GroupName, GroupCode = p.GroupCode }).ToList();
        foreach (var item in groupList)
        {
            data.Add(new DictionariesMultitypeGroupDto() { 
             GroupName =item.Key.GroupName,
             GroupCode =item.Key.GroupCode,
             dictionariesMultitypeDtos=item.ToList()
            });;
        }
        return data;
    }

    #endregion GetList

    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<DictionariesMultitypeDto>> GetPagedListAsync(GetDictionariesMultitypeInput input)
    {
        var DictionariesMultitypes = await _DictionariesMultitypeRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Status,
            input.StartTime,
            input.EndTime,
            input.Sorting);
        var items = ObjectMapper.Map<List<DictionariesMultitype>, List<DictionariesMultitypeDto>>(DictionariesMultitypes);
        var totalCount = await _DictionariesMultitypeRepository.GetCountAsync(input.Filter, input.Status,
            input.StartTime,
            input.EndTime);
        var result = new PagedResultDto<DictionariesMultitypeDto>(totalCount, items.AsReadOnly());
        return result;
    }

    #endregion GetPagedList
}