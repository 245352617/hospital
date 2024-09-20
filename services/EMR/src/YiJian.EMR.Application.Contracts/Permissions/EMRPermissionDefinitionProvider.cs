using YiJian.EMR.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace YiJian.EMR.Permissions
{
    public class EMRPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(EMRPermissions.GroupName, L("Permission:EMR"));

            var cataloguesPermission = group.AddPermission(EMRPermissions.Catalogues.Default, L("Permission:Catalogues"));
            cataloguesPermission.AddChild(EMRPermissions.Catalogues.List, L("Permission:Catalogues.List"));
            cataloguesPermission.AddChild(EMRPermissions.Catalogues.Detail, L("Permission:Catalogues.Detail"));
            cataloguesPermission.AddChild(EMRPermissions.Catalogues.Modify, L("Permission:Catalogues.Modify"));
            cataloguesPermission.AddChild(EMRPermissions.Catalogues.Delete, L("Permission:Catalogues.Delete"));

            var templateCataloguesPermission = group.AddPermission(EMRPermissions.TemplateCatalogues.Default, L("Permission:TemplateCatalogues"));
            templateCataloguesPermission.AddChild(EMRPermissions.TemplateCatalogues.List, L("Permission:TemplateCatalogues.List"));
            templateCataloguesPermission.AddChild(EMRPermissions.TemplateCatalogues.Detail, L("Permission:TemplateCatalogues.Detail"));
            templateCataloguesPermission.AddChild(EMRPermissions.TemplateCatalogues.Modify, L("Permission:TemplateCatalogues.Modify"));
            templateCataloguesPermission.AddChild(EMRPermissions.TemplateCatalogues.Delete, L("Permission:TemplateCatalogues.Delete"));

            var writesPermission = group.AddPermission(EMRPermissions.Writes.Default, L("Permission:Writes"));
            writesPermission.AddChild(EMRPermissions.Writes.List, L("Permission:Writes.List"));
            writesPermission.AddChild(EMRPermissions.Writes.Detail, L("Permission:Writes.Detail"));
            writesPermission.AddChild(EMRPermissions.Writes.Modify, L("Permission:Writes.Modify"));
            writesPermission.AddChild(EMRPermissions.Writes.Delete, L("Permission:Writes.Delete"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EMRResource>(name);
        }
    }
}