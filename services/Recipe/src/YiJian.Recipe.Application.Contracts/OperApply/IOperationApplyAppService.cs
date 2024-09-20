using System.Collections.Generic;

namespace YiJian.Recipe
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Volo.Abp.Application.Services;

    /// <summary>
    /// 分诊患者id API Interface
    /// </summary>   
    public interface IOperationApplyAppService : IApplicationService
    {
        Task<Guid> CreateOperationApplyAsync(OperationApplyCreation input);
        Task<OperationApplyData> GetOperationApplyAsync(Guid id);
        Task DeleteOperationApplyAsync(Guid id);
        Task<List<OperationApplyDataList>> GetOperationApplyListAsync(Guid pI_ID);
        Task SubmitOperationApplyAsync(Guid id, int operationStatus);
        Task<List<OperationApplyDataList>> GetOperationApplyDataListAsync(OperationApplyInput input);
        Task SyncApplyOperationStatusAsync(OperationApplyStatusSync input);

        /// <summary>
        /// 全景视图-手术申请列表
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<OperationApplyDataList>> GetAllViewOperApplyListAsync(Guid PID, DateTime? StartTime, DateTime? EndTime, CancellationToken cancellationToken = default);
    }
}