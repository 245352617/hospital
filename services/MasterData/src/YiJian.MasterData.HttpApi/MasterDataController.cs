using YiJian.MasterData.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace YiJian.MasterData;

public abstract class MasterDataController : AbpController
{
    protected MasterDataController()
    {
        LocalizationResource = typeof(MasterDataResource);
    }
}
