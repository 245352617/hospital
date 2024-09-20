using YiJian.BodyParts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.BodyParts.IService
{
    /// <summary>
    /// 表:护理记录表
    /// </summary>
    public interface IIcuNursingEventAppService : IApplicationService
    {
        #region 服务接口定义
        /// <summary>
        /// 根据条件查询护理记录
        /// </summary>
        /// <param name="PI_ID">患者id</param>
        /// <param name="ScheduleCode">班次代码</param>
        /// <param name="ScheduleTime">班次日期</param>
        /// <returns></returns>
        Task<JsonResult<List<IcuNursingEventDto>>> SelectIcuNursingEventList(string PI_ID);

        /// <summary>
        /// 新增护理记录
        /// </summary>
        /// <param name="icuNursingEventDto"></param>
        /// <returns></returns>
        Task<JsonResult> CreateIcuNursingEventInfo(CreateUpdateIcuNursingEventDto icuNursingEventDto);

        /// <summary>
        /// 审核护理记录
        /// </summary>
        /// <param name="icuNursingEventDtos"></param>
        /// <returns></returns>
        Task<JsonResult> UpdateIcuNursingEventList(List<CreateUpdateIcuNursingEventDto> icuNursingEventDtos);

        /// <summary>
        /// 修改护理记录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult<string>> UpdateNursingEvent(CreateUpdateIcuNursingEventDto dto);

        /// <summary>
        /// 删除一条护理记录
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteIcuNursingEventInfo(Guid guid);

        #endregion
    }
}
