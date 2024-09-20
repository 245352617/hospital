using YiJian.BodyParts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.BodyParts.IService
{
    /// <summary>
    /// 患者字典设置服务
    /// </summary>
    public interface IIcuPatientDicAppService : IApplicationService
    {
        /// <summary>
        /// 根据流水号查询当前班次信息，班次列表
        /// </summary>
        /// <param name="PI_ID">患者患者id</param>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        Task<JsonResult<ScheduleDto>> SelectDeptScheduleList(string PI_ID, DateTime currentTime);
    }
}