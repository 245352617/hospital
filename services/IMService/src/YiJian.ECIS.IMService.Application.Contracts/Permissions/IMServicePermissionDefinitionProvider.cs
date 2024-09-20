using YiJian.ECIS.IMService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace YiJian.ECIS.IMService.Permissions
{
    public class IMServicePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(IMServicePermissions.GroupName, L("Permission:IMService"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<IMServiceResource>(name);
        }
    }
}