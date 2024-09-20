using YiJian.EMR.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace YiJian.EMR
{
    public abstract class EMRController : AbpController
    {
        protected EMRController()
        {
            LocalizationResource = typeof(EMRResource);
        }
    }
}
