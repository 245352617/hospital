using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 报卡
    /// </summary>
    public interface ICardReportingAppService : IApplicationService
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<Guid>> SaveAsync(CardReportingSaveDto dto);

        /// <summary>
        /// 查询患者已上报报卡
        /// </summary>
        /// <param name="piid"></param>
        /// <returns></returns>
        Task<ResponseResult<List<CardReportingDto>>> GetCardListAsync(Guid piid);

        /// <summary>
        /// 获取报卡信息
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<CardReportingDto>> GetAsync(Guid piid, ECardReportingType cardReportingType);
    }
}