using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.Nursing
{
    /// <summary>
    /// 表:护理项目表 API Interface
    /// </summary>   
    public interface IParaItemAppService : IApplicationService
    {
        /// <summary>
        /// 新增或修改系统项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult> SaveParaItemInfoAsync(ParaItemUpdate input);

        /// <summary>
        /// 根据条件获取参数项目列表
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <param name="moduleType"></param>
        /// <param name="query"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        Task<JsonResult<List<ParaItemData>>> GetParaItemListAsync(string moduleCode, string moduleType,
            string query, string deptCode);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteParaItemInfoAsync(Guid guid);

        /// <summary>
        /// 批量导入参数
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <param name="groupName"></param>
        /// <param name="paraCodes"></param>
        /// <returns></returns>
        Task<JsonResult> SaveParaItemListAsync(string moduleCode,
            string groupName, List<string> paraCodes);
    }
}