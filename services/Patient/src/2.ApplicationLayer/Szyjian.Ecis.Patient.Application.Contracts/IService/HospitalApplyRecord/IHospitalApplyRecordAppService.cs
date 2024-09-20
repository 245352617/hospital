using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 住院申请API
    /// </summary>
    public interface IHospitalApplyRecordAppService : IApplicationService
    {
        /// <summary>
        /// 根据主键获取病人转住院申请记录
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<List<HospitalApplyRecordDto>>> GetHospitalApplyRecordAsync(
            HospitalApplyRecordWhereInput input, CancellationToken cancellationToken);

        /// <summary>
        /// 保存转住院申请
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> SaveHospitalApplyRecordAsync(CreateHospitalApplyRecordDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除住院申请
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<string>> DeleteHospitalApplyRecordAsync(HospitalApplyRecordWhereInput input,
            CancellationToken cancellationToken);

        /// <summary>
        /// 打印中心入院申请单调用
        /// </summary>
        /// <param name="PI_ID"></param>
        /// <returns></returns>
        Task<ReportDto> GetRecordByPrintAsync(Guid PI_ID);
    }
}