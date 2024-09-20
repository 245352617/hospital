using YiJian.EMR.Localization;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.EMR
{    
    public abstract class EMRAppService : ApplicationService
    {
        protected EMRAppService()
        {
            LocalizationResource = typeof(EMRResource);
            ObjectMapperContext = typeof(EMRApplicationModule);
        }
        
    }
}
