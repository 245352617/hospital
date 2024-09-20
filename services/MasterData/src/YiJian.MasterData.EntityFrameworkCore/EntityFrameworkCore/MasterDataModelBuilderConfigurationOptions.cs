using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace YiJian.MasterData.EntityFrameworkCore;

public class MasterDataModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
{
    public MasterDataModelBuilderConfigurationOptions(
        [NotNull] string tablePrefix = "",
        [CanBeNull] string schema = null)
        : base(
            tablePrefix,
            schema)
    {

    }
}