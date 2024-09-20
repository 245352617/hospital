using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface ITriageConfigTypeDescriptionAppService : IApplicationService
    {
        /// <summary>
        /// 新增院前分诊设置类型描述
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> SaveTriageConfigTypeDescriptionAsync(CreateTriageConfigTypeDescriptionDto dto);
        /// <summary>
        /// 修改院前分诊设置类型描述
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> UpdateTriageConfigTypeDescriptionAsync(TriageConfigTypeDescriptionDto dto);
        /// <summary>
        /// 删除院前分诊设置类型描述
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteTriageConfigTypeDescriptionAsync(Guid id);
        /// <summary>
        /// 获取院前分诊设置类型描述集合
        /// </summary>
        /// <returns></returns>
        Task<JsonResult> GetTriageConfigTypeDescriptionListAsync();
        /// <summary>
        /// 获取院前分诊设置类型描述详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult> GetTriageConfigTypeDescriptionDetailAsync(Guid id);
    }
}
