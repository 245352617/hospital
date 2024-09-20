using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Services;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 报卡插件设置
    /// </summary>
    public interface IReportCardPluginSettingAppService : IApplicationService
    {
        /// <summary>
        /// 获取报卡插件设置
        /// </summary>
        Task<ResponseResult<ReportCardPluginSettingDto>> GetReportCardPluginSettingAsync(bool isDefault, CancellationToken cancellationToken = default);

        /// <summary>
        /// 修改报卡插件设置
        /// </summary>
        Task<ResponseResult<string>> UpdateReportCardPluginSettingAsync(ReportCardPluginSettingDto dto, CancellationToken cancellationToken = default);
    }
}
