using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using YiJian.Health.Report.Localization;

namespace YiJian.Health.Report.Permissions
{
    /// <summary>
    /// 权限配置
    /// </summary>
    public class ReportPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        /// <summary>
        /// 定义
        /// </summary>
        /// <param name="context"></param>
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(ReportPermissions.GroupName, L("Permission:Report"));

            var documentPermission = group.AddPermission(ReportPermissions.NursingDocuments.Default, L("Permission:NursingDocuments"));
            documentPermission.AddChild(ReportPermissions.NursingDocuments.List, L("Permission:NursingDocuments.List"));
            documentPermission.AddChild(ReportPermissions.NursingDocuments.Detail, L("Permission:NursingDocuments.Detail"));
            documentPermission.AddChild(ReportPermissions.NursingDocuments.Modify, L("Permission:NursingDocuments.Modify"));
            documentPermission.AddChild(ReportPermissions.NursingDocuments.Delete, L("Permission:NursingDocuments.Delete"));

            var settingPermission = group.AddPermission(ReportPermissions.NursingSettings.Default, L("Permission:NursingSettings"));
            settingPermission.AddChild(ReportPermissions.NursingSettings.List, L("Permission:NursingSettings.List"));
            settingPermission.AddChild(ReportPermissions.NursingSettings.Detail, L("Permission:NursingSettings.Detail"));
            settingPermission.AddChild(ReportPermissions.NursingSettings.Modify, L("Permission:NursingSettings.Modify"));
            settingPermission.AddChild(ReportPermissions.NursingSettings.Delete, L("Permission:NursingSettings.Delete"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ReportResource>(name);
        }
    }
}