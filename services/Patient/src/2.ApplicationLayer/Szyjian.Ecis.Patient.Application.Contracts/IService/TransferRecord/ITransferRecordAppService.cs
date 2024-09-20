using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public interface ITransferRecordAppService : IApplicationService
    {
        /// <summary>
        /// 根据分诊人id获取流转记录
        /// </summary>
        /// <param name="whereInput"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<IEnumerable<TransferRecordDto>>> GetTransferRecordListAsync(
            TransferRecordWhereInput whereInput,
            CancellationToken cancellationToken);

        Task<ResponseResult<Dictionary<string, object>>> GetTransferRecordPrintAsync(
            TransferRecordWhereInput whereInput,
            CancellationToken cancellationToken);

        /// <summary>
        /// 新增流转记录
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> CreateTransferRecordAsync(CreateTransferRecordDro dto,
            CancellationToken cancellationToken);

        /// <summary>
        /// 刪除流转记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> DeleteTransferRecordAsync(TransferType transferType, Guid pI_ID);
        /// <summary>
        /// 新增流转记录
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> CreateTransferRecordInfoAsync(CreateTransferRecordDro dto, CancellationToken cancellationToken);
    }
}