using YiJian.Handover.Localization;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.Handover
{    
    public abstract class HandoverAppService : ApplicationService
    {
        protected HandoverAppService()
        {
            LocalizationResource = typeof(HandoverResource);
            ObjectMapperContext = typeof(HandoverApplicationModule);
        }
        
    }
}
