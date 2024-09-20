using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace YiJian.MasterData.AllItems;


/// <summary>
/// 诊疗检查检验药品项目合集 API Interface
/// </summary>   
public interface IAllItemByThreePartyAppService : IApplicationService
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> SaveAsync(AllItemCreation input);

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<AllItemData> GetAsync(int id);


    Task DeleteAsync(string code, string cateCode, string typeCode);

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<AllItemData>> GetPagedListAsync(GetAllItemPagedInput input);


    /// <summary>
    /// 获取所有医嘱项目分页记录-院前app
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<List<AllItemDataPreHospitalDto>> GetPagedListAppAsync(GetAllItemPagedInput input);
}