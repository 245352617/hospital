using Volo.Abp.Application.Services;
using YiJian.ECIS.Authorization;
using YiJian.Recipe.Localization;

namespace YiJian.Recipe
{
    /// <summary>
    /// RecipeAppService
    /// </summary>
    public abstract class RecipeAppService : ApplicationService
    {
        /// <summary>
        /// RecipeAppService
        /// </summary>
        protected RecipeAppService()
        {
            LocalizationResource = typeof(RecipeResource);
            ObjectMapperContext = typeof(RecipeApplicationModule);
        }

        /// <summary>
        /// 当前用户信息
        /// </summary>
        protected new IEcisCurrentUser CurrentUser => LazyServiceProvider.LazyGetRequiredService<IEcisCurrentUser>();
    }
}
