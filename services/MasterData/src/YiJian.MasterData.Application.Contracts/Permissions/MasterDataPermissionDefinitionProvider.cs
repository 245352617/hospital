using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using YiJian.MasterData.Localization;

namespace YiJian.MasterData.Permissions;

public class MasterDataPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MasterDataPermissions.GroupName, L("Permission:MasterData"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MasterDataResource>(name);
    }
}