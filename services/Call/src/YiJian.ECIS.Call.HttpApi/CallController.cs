using YiJian.ECIS.Call.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace YiJian.ECIS.Call
{
    public abstract class CallController : AbpController
    {
        protected CallController()
        {
            LocalizationResource = typeof(CallResource);
        }
    }
}
