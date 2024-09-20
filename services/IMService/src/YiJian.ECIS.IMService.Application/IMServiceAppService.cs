using YiJian.ECIS.IMService.Localization;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.ECIS.IMService
{    
    public abstract class IMServiceAppService : ApplicationService
    {
        protected IMServiceAppService()
        {
            LocalizationResource = typeof(IMServiceResource);
            ObjectMapperContext = typeof(IMServiceApplicationModule);
        }
        
    }
}
