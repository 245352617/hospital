using YiJian.Health.Report.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace YiJian.Health.Report
{
    public abstract class ReportController : AbpController
    {
        protected ReportController()
        {
            LocalizationResource = typeof(ReportResource);
        }
    }
}
