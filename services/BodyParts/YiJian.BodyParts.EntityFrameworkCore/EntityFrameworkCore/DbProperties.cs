namespace YiJian.BodyParts.EntityFrameworkCore
{
    public static class DbProperties
    {
        /// <summary>
        /// 表名前缀
        /// </summary>
        public static string TablePrefix { get; set; } = string.Empty;

        /// <summary>
        /// 归属架构
        /// </summary>
        public static string Schema { get; set; } = null;

        /// <summary>
        /// 默认连接地址
        /// </summary>
        public const string ConnectionStringName = "Default";
    }
}
