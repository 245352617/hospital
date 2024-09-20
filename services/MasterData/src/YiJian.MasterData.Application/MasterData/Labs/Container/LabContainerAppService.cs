using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.MasterData.Permissions;

namespace YiJian.MasterData.Labs.Container;

/// <summary>
/// 容器编码 API
/// </summary>
[Authorize]
public class LabContainerAppService : MasterDataAppService, ILabContainerAppService
{
    private readonly ILabContainerRepository _labContainerRepository;

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="labContainerRepository"></param>
    public LabContainerAppService(ILabContainerRepository labContainerRepository)
    {
        _labContainerRepository = labContainerRepository;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> CreateAsync(LabContainerCreation input)
    {
        if (await _labContainerRepository.AnyAsync(a => a.ContainerCode == input.ContainerCode))
        {
            throw new EcisBusinessException(message: "编码已存在");
        }

        var labContainer = await _labContainerRepository.InsertAsync(new LabContainer(
            containerCode: input.ContainerCode, containerName: input.ContainerName, // 容器名称
            containerColor: input.ContainerColor, // 容器颜色
            isActive: input.IsActive
        ));

        return labContainer.Id;
    }

    #endregion Create

    #region Update

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task UpdateAsync(LabContainerUpdate input)
    {
        var labContainer = await _labContainerRepository.GetAsync(input.Id);
        if (labContainer == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        labContainer.Modify(containerName: input.ContainerName, // 容器名称
            containerColor: input.ContainerColor, // 容器颜色
            isActive: input.IsActive
        );

        await _labContainerRepository.UpdateAsync(labContainer);
    }

    #endregion Update

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(MasterDataPermissions.LabContainers.Default)]
    public async Task<LabContainerData> GetAsync(int id)
    {
        var labContainer = await _labContainerRepository.GetAsync(id);

        return ObjectMapper.Map<LabContainer, LabContainerData>(labContainer);
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
        if (!await _labContainerRepository.AnyAsync(a => a.Id == id))
        {
            throw new EcisBusinessException(message: "数据不存在");
        }
        await _labContainerRepository.DeleteAsync(id);
    }

    #endregion Delete

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    //[Authorize(MasterDataPermissions.LabContainers.Default)]
    public async Task<ListResultDto<LabContainerData>> GetListAsync(
        string filter = null,
        string sorting = null)
    {
        var result = await _labContainerRepository.GetListAsync(filter, sorting);

        return new ListResultDto<LabContainerData>(
            ObjectMapper.Map<List<LabContainer>, List<LabContainerData>>(result));
    }

    #endregion GetList

    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    //[Authorize(MasterDataPermissions.LabContainers.Default)]
    public async Task<PagedResultDto<LabContainerData>> GetPagedListAsync(GetLabContainerPagedInput input)
    {
        var labContainers = await _labContainerRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Sorting);

        var items = ObjectMapper.Map<List<LabContainer>, List<LabContainerData>>(labContainers);

        var totalCount = await _labContainerRepository.GetCountAsync(input.Filter);

        var result = new PagedResultDto<LabContainerData>(totalCount, items.AsReadOnly());

        return result;
    }

    #endregion GetPagedList
}