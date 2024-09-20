using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IJudgmentAppService : IApplicationService
    {
        /// <summary>
        /// 获取判定依据列表
        /// </summary>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        Task<JsonResult<List<JudgmentTypeDto>>> GetJudgmentDetailListAsync(int isEnabled);

        /// <summary>
        /// 新增判定依据分类
        /// </summary>
        /// <returns></returns>
        Task<JsonResult> CreateJudgmentTypeAsync(List<CreateOrUpdateJudgmentTypeDto> dto);

        /// <summary>
        /// 根据Id删除判定依据分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteJudgmentTypeAsync(Guid id);

        /// <summary>
        /// 根据Id更新判定依据分类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> UpdateJudgmentTypeAsync(Guid id, CreateOrUpdateJudgmentTypeDto dto);

        /// <summary>
        /// 根据Id获取判定依据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult<JudgmentTypeDto>> GetJudgmentDetailAsync(Guid id);

        /// <summary>
        /// 新增判定依据主诉
        /// </summary>
        /// <returns></returns>
        Task<JsonResult> CreateJudgmentMasterAsync(List<CreateOrUpdateJudgmentMasterDto> dto);

        /// <summary>
        /// 根据Id删除判定依据主诉
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteJudgmentMasterAsync(Guid id);

        /// <summary>
        /// 根据Id更新判定依据主诉
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> UpdateJudgmentMasterAsync(Guid id, CreateOrUpdateJudgmentMasterDto dto);

        /// <summary>
        /// 新增判定依据项目
        /// </summary>
        /// <returns></returns>
        Task<JsonResult> CreateJudgmentItemAsync(List<CreateOrUpdateJudgmentItemDto> dto);

        /// <summary>
        /// 根据Id删除判定依据项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteJudgmentItemAsync(Guid id);

        /// <summary>
        /// 根据Id更新判定依据项目
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> UpdateJudgmentItemAsync(Guid id, CreateOrUpdateJudgmentItemDto dto);

        /// <summary>
        /// 操作判定依据Redis缓存
        /// </summary>
        /// <param name="flag">操作标识符；删除：0，1：更新，2：新增，3：查询</param>
        /// <param name="lambda">查询表达式</param>
        /// <param name="typeDtos">判定依据类型Dto</param>
        /// <param name="masterDtos">判定依据主诉Dto</param>
        /// <param name="itemDtos">判定依据项目Dto</param>
        Task<List<JudgmentTypeDto>> OperationJudgmentRedisCacheAsync(int flag,
            Func<JudgmentTypeDto, bool> lambda = null, List<JudgmentTypeDto> typeDtos = null,
            List<JudgmentMasterDto> masterDtos = null, List<JudgmentItemDto> itemDtos = null);
    }
}