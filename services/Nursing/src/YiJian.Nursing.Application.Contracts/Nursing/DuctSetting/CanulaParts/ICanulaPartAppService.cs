using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.Nursing
{
    /// <summary>
    /// 表:人体图-编号字典 API Interface
    /// </summary>   
    public interface ICanulaPartAppService : IApplicationService
    {
        /// <summary>
        /// 获取人体图部位分布列表
        /// </summary>
        /// <param name="bodyType"></param>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        Task<JsonResult<List<CanulaPartData>>> CanulaPartListAsync(int bodyType,
            string moduleCode);

        /// <summary>
        /// 新增或编辑部位
        /// </summary>
        /// <param name="dictCanula"></param>
        /// <returns></returns>
        Task<JsonResult> CreateCanulaPartInfoAsync(CanulaPartData dictCanula);

        /// <summary>
        /// 删除一条人体图部位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteNoticeInfoAsync(Guid id);
    }
}