using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface ITriageDeviceAppService : IApplicationService
    {
        /// <summary>
        /// 新建分诊设备
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> CreateTriageDeviceAsync(CreateOrUpdateTriageDeviceDto dto);

        /// <summary>
        /// 更新分诊设备信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> UpdateTriageDeviceAsync(Guid id, CreateOrUpdateTriageDeviceDto dto);

        /// <summary>
        /// 根据Id获取分诊设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult<TriageDeviceDto>> GetTriageDeviceAsync(Guid id);

        /// <summary>
        /// 获取分诊设备列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<List<TriageDeviceDto>>> GetTriageDeviceListAsync(TriageDeviceWhereInput input);

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<List<IotDeviceInfoDto>>> GetDeviceInfoByIot();

        /// <summary>
        /// 根据设备编码获取生命体征数据
        /// </summary>
        /// <param name="deviceCode"></param>
        /// <returns></returns>
        Task<string> GetIotVitalSignsInfo(string deviceCode);
        
        /// <summary>
        /// 根据车牌号获取生命体征数据
        /// </summary>
        /// <returns></returns>
        Task<JsonResult> GetVitalSignInfoByCarNumAsync(string carNum);

        /// <summary>
        /// 获取生命体征信息-金湾
        /// </summary>
        /// <param name="deviceCode"></param>
        /// <returns></returns>
        [Auth("TriageDevice" + PermissionDefinition.Separator + PermissionDefinition.Get)]
         Task<JsonResult<object>> GetVitalSignsInfoJinWanAsync(string deviceCode);

    }
}