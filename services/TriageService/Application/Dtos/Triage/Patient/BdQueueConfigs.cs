using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 北大队列配置（对应科室、对应叫号）
    /// </summary>
    public class BdQueueConfigs : List<BdQueueConfigItem>
    {
        public BdQueueConfigItem this[string queueId]
        {
            get
            {
                return this.FirstOrDefault(x => x.QueueId == queueId);
            }
        }
    }

    public class BdQueueConfigItem
    {
        /// <summary>
        /// 队列 ID
        /// </summary>
        public string QueueId { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 排队号前缀
        /// </summary>
        public string SnPrefix { get; set; }
    }
}
