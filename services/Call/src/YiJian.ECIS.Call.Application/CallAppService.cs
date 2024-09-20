using YiJian.ECIS.Call.Localization;
using Volo.Abp.Application.Services;

namespace YiJian.ECIS.Call
{
    public abstract class CallAppService : ApplicationService
    {
        protected CallAppService()
        {
            LocalizationResource = typeof(CallResource);
            ObjectMapperContext = typeof(CallApplicationModule);
        }
    }
}
