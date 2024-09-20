namespace YiJian.ECIS.Core.Redis
{
    /// <summary>
    /// Redis配置类
    /// </summary>
    public class RedisOptions
    {
        public string Connection { get; set; }

        public string InstanceName { get; set; }

        public int DefaultDb { get; set; }

        public string DefaultKeyPrefix { get; set; }

        public RedisOptions Value => this;


    }
}
