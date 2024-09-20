using YiJian.ECIS.IMService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace YiJian.ECIS.IMService
{
    public abstract class IMServiceController : AbpController
    {
        protected IMServiceController()
        {
            LocalizationResource = typeof(IMServiceResource);
        }
    }
}
