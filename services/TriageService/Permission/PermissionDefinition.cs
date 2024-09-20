namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 权限常量
    /// </summary>
    public class PermissionDefinition
    {
        /// <summary>
        /// 应用程序名
        /// </summary>
        public static string AppName = "PreHospital";

        /// <summary>
        /// 服务名
        /// </summary>
        public static string ServiceName = "Triage";
        
        /// <summary>
        /// 一个开关，是否启用授权权限
        /// </summary>
        public static bool IsEnabledPermission { get; set; } = false;

        /// <summary>
        /// 权限分割符
        /// </summary>
        public const string Separator = ".";

        /// <summary>
        /// 查询单条权限
        /// </summary>
        public const string Get = "Get";

        /// <summary>
        /// 保存权限
        /// </summary>
        public const string Save = "Save";

        /// <summary>
        /// 新增权限
        /// </summary>
        public const string Create = "Create";

        /// <summary>
        /// 更新权限
        /// </summary>
        public const string Update = "Update";

        /// <summary>
        /// 删除权限
        /// </summary>
        public const string Delete = "Delete";

        /// <summary>
        /// 浏览权限
        /// </summary>
        public const string View = "View";

        /// <summary>
        /// 审核权限
        /// </summary>
        public const string Audit = "Audit";

        /// <summary>
        /// 分页权限
        /// </summary>
        public const string Page = "Page";

        /// <summary>
        /// 列表权限
        /// </summary>
        public const string List = "List";
    }
}