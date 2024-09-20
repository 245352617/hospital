using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.ViewSettings;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;

/// <summary>
/// 视图配置 仓储实现
/// </summary> 
public class ViewSettingRepository : MasterDataRepositoryBase<ViewSetting, int>, IViewSettingRepository
{
    #region constructor
    public ViewSettingRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
    #endregion        

    #region GetList
    /// <summary>
    /// 获取列表
    /// </summary>
    ///  <param name="view">视图名称</param>
    /// <returns></returns>
    public async Task<List<ViewSetting>> GetListAsync(string view = null)
    {
        var result = await (await GetDbSetAsync())
            .AsNoTracking()
            .Where(v => v.IsActive && v.View == view)
            .ToListAsync();

        GetDefaultView(result);

        return result;
    }
    #endregion

    #region Reset

    /// <summary>
    /// 重置
    /// </summary>
    /// <param name="view">视图名称</param>
    /// <returns></returns>
    public async Task ResetAsync(string view)
    {
        var result = await (await GetDbSetAsync()).Where(v => v.IsActive && v.View == view).ToListAsync();

        foreach (var item in result)
        {
            item.Reset();
        }
    }
    #endregion Reset  

    #region ChildrenSetting
    private void GetDefaultView(List<ViewSetting> viewSettings)
    {
        var childenIds = new List<int>();

        // children
        foreach (var item in viewSettings)
        {
            item.Children = viewSettings.Where(v => v.ParentID == item.Id).ToList();

            if (item.Children.Any())
            {
                item.Children.Sort();
                childenIds.AddRange(item.Children.Select(x => x.Id));
            }
        }

        foreach (var id in childenIds)
        {
            var index = viewSettings.FindIndex(x => x.Id == id);
            viewSettings.RemoveAt(index);
        }

        viewSettings.Sort();
    }
    #endregion
}

