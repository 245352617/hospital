using SamJan.MicroService.PreHospital.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface ILevelTriageRelationDirectionAppService : IApplicationService
    {
        /// <summary>
        /// 修改分诊级别关联关联去向
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> UpdateLevelTriageRelationDirectionAsync(List<LevelTriageRelationDirectionDto> dto);
        
        /// <summary>
        /// 获取分诊级别关联去向
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<List<LevelTriageRelationDirectionDto>>> SelectLevelTriageRelationDirectionAsync();
    }
}
