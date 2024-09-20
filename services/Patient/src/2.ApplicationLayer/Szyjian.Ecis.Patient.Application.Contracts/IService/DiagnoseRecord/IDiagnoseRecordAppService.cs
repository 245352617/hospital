using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 诊断记录应用服务接口
    /// </summary>
    public interface IDiagnoseRecordAppService : IApplicationService
    {
        /// <summary>
        /// 开立诊断-1 
        /// </summary>
        /// <param name="dtoList"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> SaveAsync(List<CreateDiagnoseRecordDto> dtoList,
            Guid pI_ID,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 收藏诊断
        /// </summary>
        /// <param name="dtoList"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> SaveCollectionDiagnoseListAsync(List<CreateCollectionDiagnoseDto> dtoList,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 保存收藏诊断排序
        /// </summary>
        /// <param name="dtoList"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> SaveCollectionDiagnoseSortAsync(List<UpdateCollectionDiagnoseSortDto> dtoList,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除诊断、取消收藏
        /// </summary>
        /// <param name="dto">删除Dto</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<string>> DeleteDiagnoseRecordAsync(DeleteDiagnoseDto dto,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据输入项查询诊断列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResponseResult<IEnumerable<DiagnoseRecordDto>>> GetDiagnoseRecordListAsync(DiagnoseRecordInput input,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新诊断被电子病历引用的标记
        /// </summary>
        /// <returns></returns>  
        Task<ResponseResult<bool>> ModifyDiagnoseRecordEmrUsedAsync(IList<int> pdid);

        /// <summary>
        /// 查询快速诊断列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="type">类型  -1:全部  0：常用  1：收藏   2：最近历史</param>
        /// <returns></returns>
        Task<ResponseResult<Dictionary<string, IEnumerable<FastDiagnoseDto>>>> GetFastDiagnoseListAsync(string filter,
            FastDiagnoseType type);

        /// <summary>
        /// 获取历史诊断
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="pI_ID"></param>
        /// <returns></returns>
        Task<ResponseResult<IEnumerable<DiagnoseWithDeptDto>>>
            GetHistoryDiagnoseListAsync(string patientId, Guid pI_ID);

    }
}