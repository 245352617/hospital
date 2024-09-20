using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using YiJian.Recipe.Localization;

namespace YiJian.Recipe.Permissions
{
    public class RecipePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(RecipePermissions.GroupName, L("Permission:Recipe"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<RecipeResource>(name);
        }
    }
}