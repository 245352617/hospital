using Volo.Abp.Application.Services;
using YiJian.Health.Report.Localization;

namespace YiJian.Health.Report
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ReportAppService : ApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        protected ReportAppService()
        {
            LocalizationResource = typeof(ReportResource);
            ObjectMapperContext = typeof(ReportApplicationModule);
        }

    }
}
