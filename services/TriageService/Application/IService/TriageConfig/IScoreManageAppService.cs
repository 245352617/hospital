using SamJan.MicroService.PreHospital.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IScoreManageAppService : IApplicationService
    {
        /// <summary>
        /// 新增评分管理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> CreateScoreManageAsync(CreateOrUpdateScoreManageDto dto);

        /// <summary>
        /// 修改评分管理
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> UpdateScoreManageAsync(Guid Id, CreateOrUpdateScoreManageDto dto);

        /// <summary>
        /// 评分启用禁用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult> EnableScoreManageAsync(ScoreManageInput input);

        /// <summary>
        /// 获取评分管理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult<ScoreManageDto>> GetScoreManageAsync(Guid id);

        /// <summary>
        /// 获取评分管理列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<List<ScoreManageDto>>> GetScoreManageListAsync(ScoreManageWhereInput input);
    }
}