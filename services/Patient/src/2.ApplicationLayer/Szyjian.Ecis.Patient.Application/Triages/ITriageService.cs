using Abp.Application.Services.Dto;
using BeetleX.Http.Clients;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Triages
{
    /// <summary>
    /// 分诊服务
    /// </summary>
    [JsonFormater]
    public interface ITriageProxyService
    {
        /// <summary>
        /// 获取分诊科室配置信息
        /// </summary>
        /// <returns></returns> 
        [Get(Route = "api/ecis/TriageService/triageConfig/triageConfigPageList?TriageConfigType=1005&SkipCount=1&MaxResultCount=10000")]
        public Task<ResponseResult<PagedResultDto<DeptInfoDto>>> GetTriageConfigPageListAsync([Header("Authorization", "{Authorization}")] string Authorization);

    }
}
