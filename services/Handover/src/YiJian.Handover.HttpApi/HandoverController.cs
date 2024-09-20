using YiJian.Handover.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace YiJian.Handover
{
    public abstract class HandoverController : AbpController
    {
        protected HandoverController()
        {
            LocalizationResource = typeof(HandoverResource);
        }
    }
}
