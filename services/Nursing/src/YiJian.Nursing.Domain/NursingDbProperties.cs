namespace YiJian.Nursing
{
    /// <summary>
    /// 数据库表配置
    /// </summary>
    public static class NursingDbProperties
    {
        /// <summary>
        /// 表名前缀
        /// </summary>
        public static string DbTablePrefix { get; set; } = "Nursing";

        /// <summary>
        /// 架构
        /// </summary>
        public static string DbSchema { get; set; } = null;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public const string ConnectionStringName = "Nursing";
    }
}
