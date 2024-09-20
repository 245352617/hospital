using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using YiJian.ECIS.ShareModel.Exceptions; 

namespace YiJian.MasterData.DictionariesTypes;

/// <summary>
/// 字典类型API
/// </summary>
[Authorize]
public class DictionariesTypeAppService : MasterDataAppService, IDictionariesTypeAppService
{
    private readonly DictionariesTypeManager _dictionariesTypeManager;
    private readonly IDictionariesTypeRepository _dictionariesTypeRepository;
    private readonly IDictionariesRepository _iDictionariesRepository;

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictionariesTypeRepository"></param>
    /// <param name="dictionariesTypeManager"></param>
    /// <param name="iDictionariesRepository"></param>
    public DictionariesTypeAppService(IDictionariesTypeRepository dictionariesTypeRepository,
        DictionariesTypeManager dictionariesTypeManager, IDictionariesRepository iDictionariesRepository)
    {
        _dictionariesTypeRepository = dictionariesTypeRepository;
        _dictionariesTypeManager = dictionariesTypeManager;
        _iDictionariesRepository = iDictionariesRepository;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<Guid> SaveAsync(DictionariesTypeUpdate input)
    {
        if (input.Id == Guid.Empty)
        {
            if (await (await _dictionariesTypeRepository.GetQueryableAsync()).AnyAsync(
                    x => x.DictionariesTypeCode == input.DictionariesTypeCode))
            {
                throw new EcisBusinessException(message: "编码已存在");
            }

            var dictionariesType = await _dictionariesTypeManager.CreateAsync(
                dictionariesTypeCode: input.DictionariesTypeCode, // 字典类型编码
                dictionariesTypeName: input.DictionariesTypeName, // 字典类型名称
                remark: input.Remark, // 备注
                status: input.Status
            );
            return dictionariesType.Id;
        }
        var typeModel = await _dictionariesTypeRepository.GetAsync(input.Id);
        if (typeModel == null)
            throw new EcisBusinessException(message: "数据不存在");
        typeModel.Modify(dictionariesTypeName: input.DictionariesTypeName, remark: input.Remark,
            status: input.Status);
        await _dictionariesTypeRepository.UpdateAsync(typeModel);
        return typeModel.Id;
    }

    #endregion Create

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DictionariesTypeData> GetAsync(Guid id)
    {
        var dictionariesType = await _dictionariesTypeRepository.GetAsync(id);

        return ObjectMapper.Map<DictionariesType, DictionariesTypeData>(dictionariesType);
    }

    #endregion Get

    #region 删除

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="EcisBusinessException"></exception>
    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var dto = await _dictionariesTypeRepository.GetAsync(id);
            if (dto == null)
            {
                throw new EcisBusinessException(message: "数据不存在");
            }

            if (await (await _iDictionariesRepository.GetQueryableAsync()).AnyAsync(x => dto.DictionariesTypeCode == x.DictionariesTypeCode))
            {
                throw new EcisBusinessException(message: "存在子集，无法删除");
            }

            await _dictionariesTypeRepository.DeleteAsync(id);
            return true;
        }
        catch (Exception e)
        {
            throw new EcisBusinessException(message: e.Message);
        }
    }

    #endregion 删除

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<DictionariesTypeData>> GetListAsync(
        string filter = null,
        string sorting = null)
    {
        var result = await _dictionariesTypeRepository.GetListAsync(filter, sorting);
        return new ListResultDto<DictionariesTypeData>(
            ObjectMapper.Map<List<DictionariesType>, List<DictionariesTypeData>>(result));
    }

    #endregion GetList

    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<DictionariesTypeData>> GetPagedListAsync(GetDictionariesTypeInput input)
    {
        var dictionariesTypes = await _dictionariesTypeRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Status,
            input.StartTime,
            input.EndTime,
            input.Sorting);
        var items = ObjectMapper.Map<List<DictionariesType>, List<DictionariesTypeData>>(dictionariesTypes);
        var totalCount = await _dictionariesTypeRepository.GetCountAsync(input.Filter, input.Status,
            input.StartTime,
            input.EndTime);
        var result = new PagedResultDto<DictionariesTypeData>(totalCount, items.AsReadOnly());
        return result;
    }

    #endregion GetPagedList
}