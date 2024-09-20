using System.Collections.Generic;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 时间轴应用服务接口
    /// </summary>
    public interface ITimeAxisRecordAppService : IApplicationService
    {
        /// <summary>
        /// 保存时间轴节点数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ResponseResult<int>> SaveTimeAxisRecordAsync(TimeAxisRecordDto dto);

        /// <summary>
        /// 获取病患时间轴记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ResponseResult<IEnumerable<TimeAxisRecordDto>>> GetTimeAxisRecordListAsync(TimeAxisRecordInput input);
    }
}