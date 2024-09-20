using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace YiJian.MasterData.ViewSettings;

/// <summary>
/// 视图配置 API
/// </summary>
[Authorize]
public class ViewSettingAppService : MasterDataAppService, IViewSettingAppService
{
    private readonly IViewSettingRepository _viewSettingRepository;

    #region constructor
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="viewSettingRepository"></param>
    public ViewSettingAppService(IViewSettingRepository viewSettingRepository)
    {
        _viewSettingRepository = viewSettingRepository;
    }
    #endregion constructor        

    #region Update
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    //[Authorize(MasterDataPermissions.ViewSettings.Update)]
    public async Task UpdateAsync(ViewSettingUpdate[] input)
    {
        foreach (var item in input)
        {
            var viewSetting = await _viewSettingRepository.GetAsync(item.Id);

            viewSetting.Modify(
                label: item.Label,             // 标头
                headerAlign: item.HeaderAlign, // 标头对齐
                align: item.Align,             // 对齐
                width: item.Width,             // 宽度
                minWidth: item.MinWidth,       // 最小宽度
                visible: item.Visible,         // 是否显示
                showTooltip: item.ShowTooltip, // 是否提示
                index: item.Index,             // 序号
                parentID: item.ParentID        // 父级ID
                );

            await _viewSettingRepository.UpdateAsync(viewSetting);
        }

    }
    #endregion Update

    #region GetList
    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    //[Authorize(MasterDataPermissions.ViewSettings.Default)]
    public async Task<ListResultDto<ViewSettingData>> GetListAsync(string view)
    {
        var result = await _viewSettingRepository.GetListAsync(view);

        return new ListResultDto<ViewSettingData>(
            ObjectMapper.Map<List<ViewSetting>, List<ViewSettingData>>(result));
    }

    #endregion GetList

    #region Reset
    public async Task<ListResultDto<ViewSettingData>> ResetAsync(string view)
    {
        await _viewSettingRepository.ResetAsync(view);

        await UnitOfWorkManager.Current.SaveChangesAsync();

        using (var uow = UnitOfWorkManager.Begin(new Volo.Abp.Uow.AbpUnitOfWorkOptions() { }, requiresNew: true))
        {
            return await GetListAsync(view);
        }
    }
    #endregion 
}

