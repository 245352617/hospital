using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.MasterData.Labs.Position;

namespace YiJian.MasterData.Labs;

/// <summary>
/// 检验标本 API
/// </summary>
[Authorize]
public class LabSpecimenAppService : MasterDataAppService, ILabSpecimenAppService
{
    private readonly ILabSpecimenRepository _labSpecimenRepository;
    private readonly ILabSpecimenPositionRepository _labSpecimenPositionRepository;

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="labSpecimenRepository"></param>
    /// <param name="labSpecimenPositionRepository"></param>
    public LabSpecimenAppService(ILabSpecimenRepository labSpecimenRepository,
        ILabSpecimenPositionRepository labSpecimenPositionRepository)
    {
        _labSpecimenRepository = labSpecimenRepository;
        _labSpecimenPositionRepository = labSpecimenPositionRepository;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> CreateAsync(LabSpecimenCreation input)
    {
        if (await _labSpecimenRepository.AnyAsync(a => a.SpecimenCode == input.SpecimenCode))
        {
            throw new EcisBusinessException(message: "编码已存在");
        }

        var model = ObjectMapper.Map<LabSpecimenCreation, LabSpecimen>(input);
        var result = await _labSpecimenRepository.InsertAsync(model);
        return result.Id;
    }

    #endregion Create

    #region Update

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task UpdateAsync(LabSpecimenUpdate input)
    {
        var labSpecimen = await _labSpecimenRepository.GetAsync(input.Id);
        if (labSpecimen == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        labSpecimen.Modify(name: input.SpecimenName, // 标本名称
            sort: input.Sort, // 排序号
            isActive: input.IsActive // 是否启用
        );
        await _labSpecimenRepository.UpdateAsync(labSpecimen);
    }

    #endregion Update

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<LabSpecimenData> GetAsync(int id)
    {
        var labSpecimen = await _labSpecimenRepository.GetAsync(id);

        return ObjectMapper.Map<LabSpecimen, LabSpecimenData>(labSpecimen);
    }

    #endregion Get

    #region Delete

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(int id)
    {
        if (!await _labSpecimenRepository.AnyAsync(a => a.Id == id))
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        await _labSpecimenRepository.DeleteAsync(id);
    }

    #endregion Delete

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<LabSpecimenData>> GetListAsync(
        string filter = null,
        string sorting = null)
    {
        var result = await _labSpecimenRepository.GetListAsync(filter, sorting);

        var map = ObjectMapper.Map<List<LabSpecimen>, List<LabSpecimenData>>(result);
        var partList = await (await _labSpecimenPositionRepository.GetQueryableAsync()).Where(x =>
            string.Join(",", map.Select(s => s.SpecimenCode).ToList()).Contains(x.SpecimenCode)).ToListAsync();
        map.ForEach(x =>
        {
            x.SpecimenPartCode = partList.FirstOrDefault(p => p.SpecimenCode == x.SpecimenCode)?.SpecimenPartCode;
            x.SpecimenPartName = partList.FirstOrDefault(p => p.SpecimenCode == x.SpecimenCode)?.SpecimenPartName;
        });
        return new ListResultDto<LabSpecimenData>(map);
    }

    #endregion GetList

    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<LabSpecimenData>> GetPagedListAsync(GetLabSpecimenPagedInput input)
    {
        var labSpecimens = await _labSpecimenRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Sorting);

        var items = ObjectMapper.Map<List<LabSpecimen>, List<LabSpecimenData>>(labSpecimens);
        var partList = await (await _labSpecimenPositionRepository.GetQueryableAsync()).Where(x =>
            string.Join(",", items.Select(s => s.SpecimenCode).ToList()).Contains(x.SpecimenCode)).ToListAsync();
        items.ForEach(x =>
        {
            x.SpecimenPartCode = partList.FirstOrDefault(p => p.SpecimenCode == x.SpecimenCode)?.SpecimenPartCode;
            x.SpecimenPartName = partList.FirstOrDefault(p => p.SpecimenCode == x.SpecimenCode)?.SpecimenPartName;
        });
        var totalCount = await _labSpecimenRepository.GetCountAsync(input.Filter);

        var result = new PagedResultDto<LabSpecimenData>(totalCount, items.AsReadOnly());

        return result;
    }

    #endregion GetPagedList
}