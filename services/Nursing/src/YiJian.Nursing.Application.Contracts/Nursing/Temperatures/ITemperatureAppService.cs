using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.Nursing.Temperatures.Dtos;

namespace YiJian.Nursing.Temperatures
{
    /// <summary>
    /// 描述：体温单服务接口
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 10:53:38
    /// </summary>
    public interface ITemperatureAppService : IApplicationService
    {
        /// <summary>
        /// 保存临床事件
        /// </summary>
        /// <param name="clinicalEventInput"></param>
        /// <returns></returns>
        Task SaveClinicalEventAsync(ClinicalEventDto clinicalEventInput);

        /// <summary>
        /// 获取临床事件
        /// </summary>
        /// <param name="pi_id"></param>
        /// <returns></returns>
        Task<ClinicalEventDto> GetClinicalEventsAsync(Guid pi_id);

        /// <summary>
        /// 删除临床事件
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeleteClinicalEventsAsync(List<Guid> ids);

        /// <summary>
        /// 获取体温单
        /// </summary>
        /// <param name="pi_id"></param>
        /// <param name="measureDate"></param>
        /// <returns></returns>
        Task<TemperatureDto> GetTemperatureAsync(Guid pi_id, DateTime measureDate);

        /// <summary>
        /// 更新体温记录
        /// </summary>
        /// <param name="temperatureRecordDtos"></param>
        /// <returns></returns>
        Task<bool> UpdateTemperatureRecordsAsync(List<TemperatureRecordDto> temperatureRecordDtos);

        /// <summary>
        /// 获取体温单明细信息
        /// </summary>
        /// <param name="pi_id"></param>
        /// <returns></returns>
        Task<List<TemperatureDetailDto>> GetTemperatureDetailsAsync(Guid pi_id);

        /// <summary>
        /// 获取体温报表数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TemperatureReportDto> GetTemperatureReportAsync(TemperatureReportInput input);
    }
}
