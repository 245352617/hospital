using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 评分字典应用服务层接口
    /// </summary>
    public interface IScoreDictAppService : IApplicationService
    {
        /// <summary>
        /// 获取评分字典树
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<List<ScoreDictDto>>> GetTreeAsync(ScoreDictInput input);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> SaveAsync(ScoreDictDto dto);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteAsync(Guid id);
    }
}