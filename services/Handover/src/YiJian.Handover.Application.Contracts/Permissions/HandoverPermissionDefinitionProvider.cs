using YiJian.Handover.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace YiJian.Handover.Permissions
{
    public class HandoverPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(HandoverPermissions.GroupName, L("Permission:Handover"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<HandoverResource>(name);
        }
    }
}