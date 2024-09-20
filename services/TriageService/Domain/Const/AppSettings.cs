namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// App 设置常量
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// 默认连接字符串
        /// </summary>
        public static string DefaultConnectionString = "DefaultConnection";

        /// <summary>
        /// 调度服务
        /// </summary>
        public const string EmrService = "EmrService";

        /// <summary>
        /// 聚合服务
        /// </summary>
        public const string AggregateService = "AggregateService";

        /// <summary>
        /// 地图服务
        /// </summary>
        public const string MapService = "MapService";

        /// <summary>
        /// Iot服务
        /// </summary>
        public const string IotService = "IotService";

        /// <summary>
        /// 分诊服务
        /// </summary>
        public const string TriageService = "TriageService";

        /// <summary>
        /// 都昌病历服务
        /// </summary>
        public const string EmrEditorService = "EmrEditorService";

        public const string ScoreDictRedisKey = ":ScoreDict";

        /// <summary>
        /// 任务单剩余距离 Redis 缓存 Key Name
        /// </summary>
        public const string TaskSurplusDistanceRedisKey = ":TaskSurplusDistance:";

        /// <summary>
        /// 任务单预计到达时间 Redis 缓存 Key Name
        /// </summary>
        public const string TaskExpectTimeRedisKey = ":TaskExpectTime:";
    }
}