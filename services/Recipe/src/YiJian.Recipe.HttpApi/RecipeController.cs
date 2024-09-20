using Volo.Abp.AspNetCore.Mvc;
using YiJian.Recipe.Localization;

namespace YiJian.Recipe
{
    public abstract class RecipeController : AbpController
    {
        protected RecipeController()
        {
            LocalizationResource = typeof(RecipeResource);
        }
    }
}
