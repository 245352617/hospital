using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IVitalSignExpressionAppService : IApplicationService
    {
        /// <summary>
        /// 获取生命体征表达式列表
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<List<VitalSignExpressionDto>>> GetVitalSignExpressionListAsync();

        /// <summary>
        /// 根据Id获取生命体征表达式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult<VitalSignExpressionDto>> GetVitalSignExpressionAsync(Guid id);

        /// <summary>
        /// 更新生命体征表达式
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> UpdateVitalSignExpressionAsync(Guid id, CreateOrUpdateVitalSignExpressionDto dto);

        /// <summary>
        /// 创建生命体征表达式
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> CreateVitalSignExpressionAsync(CreateOrUpdateVitalSignExpressionDto dto);

        /// <summary>
        /// 删除生命体征表达式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteVitalSignExpressionAsync(Guid id);
        
        /// <summary>
        /// 一键重置生命体征评分表达式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult> ResetAllVitalSignExpressionAsync(ResetVitalSignExpressionDto input);

        /// <summary>
        /// 单个重置生命体征评分表达式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult> ResetVitalSignExpressionAsync(ResetVitalSignExpressionDto input);

        /// <summary>
        /// 获取生命体征评分结果
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<VitalSignGradeResultDto>> GetGradeResultAsync(VitalSignGradeInput input);
    }
}