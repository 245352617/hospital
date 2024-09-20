using Volo.Abp.Reflection;

namespace YiJian.Health.Report.Permissions
{
    /// <summary>
    /// 报表权限
    /// </summary>
    public class ReportPermissions
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public const string GroupName = "Report";

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(ReportPermissions));
        }

        /// <summary>
        /// 护理记录单
        /// </summary>
        public static class NursingDocuments
        {
            /// <summary>
            /// 默认
            /// </summary>
            public const string Default = GroupName + ".NursingDocuments";

            /// <summary>
            /// 列表
            /// </summary>
            public const string List = Default + ".List";

            /// <summary>
            /// 详情
            /// </summary>
            public const string Detail = Default + ".Detail";

            /// <summary>
            /// 修改
            /// </summary>
            public const string Modify = Default + ".Modify";

            /// <summary>
            /// 删除
            /// </summary>
            public const string Delete = Default + ".Delete";
        }

        /// <summary>
        /// 护理配置
        /// </summary>
        public static class NursingSettings
        {
            /// <summary>
            /// 默认
            /// </summary>
            public const string Default = GroupName + ".NursingSettings";

            /// <summary>
            /// 列表
            /// </summary>
            public const string List = Default + ".List";

            /// <summary>
            /// 详情
            /// </summary>
            public const string Detail = Default + ".Detail";

            /// <summary>
            /// 修改
            /// </summary>
            public const string Modify = Default + ".Modify";

            /// <summary>
            /// 删除
            /// </summary>
            public const string Delete = Default + ".Delete";
        }
    }
}