using Abp.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 分诊服务
    /// </summary>
    public interface ITriageAppService : IApplicationService
    {
        /// <summary>
        /// 获取分诊科室配置信息
        /// </summary>
        /// <param name="triageConfigCodes">科室代码</param>
        /// <param name="authorization"></param>
        /// <returns>院内的HIS科室</returns>
        public Task<List<DeptInfoDto>> GetTriageConfigPageListAsync(List<string> triageConfigCodes, string authorization);
    }
}
