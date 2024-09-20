using System.Collections.Generic;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 快速诊断仓储接口
    /// </summary>
    public interface IDiagnoseRecordRepository
    {
        /// <summary>
        /// 批量插入快速诊断
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<bool> AddRangeAsync(IEnumerable<DiagnoseRecord> entities);

        /// <summary>
        /// 删除再新增患者诊断
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task HandleDiagnoseAsync(List<GetDiagnoseRecordBySocket> list);
    }
}