using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.Nursing.Settings
{
    /// <summary>
    /// 表:模块参数 API Interface
    /// </summary>   
    public interface IParaModuleAppService : IApplicationService
    {
        /// <summary>
        /// 新增或修改模块参数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult> SaveParaModuleInfoAsync(ParaModuleUpdate input);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ParaModuleData> GetAsync(Guid id);

        /// <summary>
        /// 删除模块参数(仅用于删除科室项目模块)
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteParaModuleInfoAsync(string moduleCode);

        /// <summary>
        /// 根据条件查询模块参数
        /// </summary>
        /// <param name="moduleType"></param>
        /// <param name="query"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        Task<JsonResult<List<ParaModuleData>>> GetParaModuleListAsync(string moduleType, string query, int isEnabled = -1);

        /// <summary>
        /// 查询批量导入参数
        /// </summary>
        /// <param name="moduleType"></param>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        Task<JsonResult<List<ParaItemListDto>>> SelectModuleListAsync(string moduleType, string moduleCode);
    }
}