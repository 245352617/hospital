using Volo.Abp.Application.Services;
using YiJian.MasterData.Localization;

namespace YiJian.MasterData;

public abstract class MasterDataAppService : ApplicationService
{
    protected MasterDataAppService()
    {
        LocalizationResource = typeof(MasterDataResource);
        ObjectMapperContext = typeof(MasterDataApplicationModule);
    }
    
}
