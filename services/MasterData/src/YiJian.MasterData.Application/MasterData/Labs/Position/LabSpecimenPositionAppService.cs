using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.MasterData.Labs.Position;

/// <summary>
/// 检验标本采集部位 API
/// </summary>
[Authorize]
public class LabSpecimenPositionAppService : MasterDataAppService, ILabSpecimenPositionAppService
{
    private readonly ILabSpecimenPositionRepository _labSpecimenPositionRepository;

    #region constructor
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="labSpecimenPositionRepository"></param>
    public LabSpecimenPositionAppService(ILabSpecimenPositionRepository labSpecimenPositionRepository)
    {
        _labSpecimenPositionRepository = labSpecimenPositionRepository;
    }
    #endregion constructor

    #region Create
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> CreateAsync(LabSpecimenPositionCreation input)
    {
        if (await _labSpecimenPositionRepository.AnyAsync(a => a.SpecimenPartCode == input.SpecimenPartCode))
        {
            throw new EcisBusinessException(message: "编码已存在");
        }
        var labSpecimenPosition = await _labSpecimenPositionRepository.InsertAsync(new LabSpecimenPosition(specimenCode: input.SpecimenCode,// 标本编码
            specimenName: input.SpecimenName,// 标本名称
            positionCode: input.SpecimenPartCode,// 采集部位编码
            positionName: input.SpecimenPartName,// 采集部位名称
            sort: input.Sort,         // 排序号
            isActive: input.IsActive
        ));
        return labSpecimenPosition.Id;
    }
    #endregion Create

    #region Update
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task UpdateAsync(LabSpecimenPositionUpdate input)
    {
        var labSpecimenPosition = await _labSpecimenPositionRepository.GetAsync(input.Id);
        if (labSpecimenPosition == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }
        labSpecimenPosition.Modify(specimenName: input.SpecimenName,// 标本名称
            positionCode: input.SpecimenPartCode,// 采集部位编码
            positionName: input.SpecimenPartName,// 采集部位名称
            sort: input.Sort,         // 排序号
            isActive: input.IsActive
            );

        await _labSpecimenPositionRepository.UpdateAsync(labSpecimenPosition);
    }
    #endregion Update

    #region Get
    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<LabSpecimenPositionData> GetAsync(int id)
    {
        var labSpecimenPosition = await _labSpecimenPositionRepository.GetAsync(id);

        return ObjectMapper.Map<LabSpecimenPosition, LabSpecimenPositionData>(labSpecimenPosition);
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
        if (!await _labSpecimenPositionRepository.AnyAsync(a => a.Id == id))
        {
            throw new EcisBusinessException(message: "数据不存在");
        }
        await _labSpecimenPositionRepository.DeleteAsync(id);
    }
    #endregion Delete

    #region GetList
    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<LabSpecimenPositionData>> GetListAsync(string positionCode,
        string filter = null,
        string sorting = null)
    {
        var result = await _labSpecimenPositionRepository.GetListAsync(positionCode, filter, sorting);

        return new ListResultDto<LabSpecimenPositionData>(
            ObjectMapper.Map<List<LabSpecimenPosition>, List<LabSpecimenPositionData>>(result));
    }
    #endregion GetList

    #region GetPagedList
    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<LabSpecimenPositionData>> GetPagedListAsync(GetLabSpecimenPositionPagedInput input)
    {
        var labSpecimenPositions = await _labSpecimenPositionRepository.GetPagedListAsync(input.PositionCode,
                input.SkipCount,
                input.Size,
                input.Filter,
                input.Sorting);

        var items = ObjectMapper.Map<List<LabSpecimenPosition>, List<LabSpecimenPositionData>>(labSpecimenPositions);

        var totalCount = await _labSpecimenPositionRepository.GetCountAsync(input.PositionCode, input.Filter);

        var result = new PagedResultDto<LabSpecimenPositionData>(totalCount, items.AsReadOnly());

        return result;
    }
    #endregion GetPagedList

}
