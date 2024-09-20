namespace YiJian.BodyParts.WebAPI
{
    public class MQPreConfigOption
    {
        /// <summary>
        /// 启用mq
        /// </summary>
        public bool IsStartRabbitMQ { get; set; }
        
        /// <summary>
        /// 是否开启后台作业
        /// </summary>
        public bool IsStartBackgroundWorker { get; set; }
    }
}