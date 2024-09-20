using System.Collections.Generic;
using System.Collections.Immutable;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class ApplicationPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        /// <summary>
        /// 系统权限树,从配置中获取，没有取取默认值
        /// 服务名称
        /// </summary>
        public static List<string> PermissionTreeDefinition = new List<string>
        {
            "FastTrackRegisterInfo", // 快速通道登记接口权限
            "FastTrackSetting", // 快速通道设置接口权限
            "GroupInjury", // 群伤事件接口权限
            "Judgment", // 分诊判定依据接口权限
            "LevelTriageRelationDirection", // 分诊级别关联分诊去向和其他去向接口权限
            "PatientInfo", // 建档患者信息接口权限
            "PatientRegister", // 患者退挂号接口权限
            "PermissionManagement", // 权限管理接口权限
            "ReportSetting", // 分诊报表设置接口权限
            "ScoreManage", // 评分管理接口权限
            "TableSetting", // 分诊记录显示字段接口权限
            "TriageConfig", // 分诊设置接口权限
            "TriageConfigTypeDescription", // 分诊设置类型接口权限
            "TriageDevice", // 设备信息接口权限
            "VitalSignExpression", // 生命体征评分表达式接口权限
        };

        /// <summary>
        /// 默认子权限
        /// </summary>
        private static readonly List<string> Child = new List<string>
        {
            PermissionDefinition.Create, PermissionDefinition.Update, PermissionDefinition.Delete,
            PermissionDefinition.View, PermissionDefinition.Audit, PermissionDefinition.Page,
            PermissionDefinition.List, PermissionDefinition.Get, PermissionDefinition.Save
        };

        //定义简单权限树
        //后台再加一些其他业务角色
        //IHospital.Admin
        private static IEnumerable<string> Trees => PermissionTreeDefinition;

        //权限声明
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(PermissionDefinition.AppName + "." + PermissionDefinition.ServiceName,
                L($"{PermissionDefinition.ServiceName}"));

            foreach (var r in Trees)
            {
                var permission = group.AddPermission(
                    PermissionDefinition.AppName + PermissionDefinition.Separator + PermissionDefinition.ServiceName +
                    PermissionDefinition.Separator + r,
                    L($"{PermissionDefinition.Separator}{r}"));

                foreach (var item in Child)
                    permission.AddChild(
                        PermissionDefinition.AppName + PermissionDefinition.Separator + PermissionDefinition.ServiceName +
                        PermissionDefinition.Separator + r + PermissionDefinition.Separator + item,
                        L($"{r}{PermissionDefinition.Separator}{item}"));
            }
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PermissionDefinition>(name);
        }
    }
}