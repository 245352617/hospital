using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace YiJian.Recipe.EntityFrameworkCore
{
    public class RecipeModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public RecipeModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}