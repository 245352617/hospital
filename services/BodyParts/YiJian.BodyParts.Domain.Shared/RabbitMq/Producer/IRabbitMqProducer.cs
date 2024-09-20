using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Domain.Shared.RabbitMq.Producer
{
    public interface IRabbitMqProducer
    {
        /// <summary>
        /// 推送消息到Mq，通过exchange+routingKey定位队列
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="routingKey"></param>
        /// <param name="t"></param>
        void Publish<T>(string exchange, string routingKey, T t);

        /// <summary>
        /// 推送消息到Mq，通过exchange+routingKey定位队列
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="routingKey"></param>
        /// <param name="msg"></param>
        void Publish(string exchange, string routingKey,string msg);

        /// <summary>
        /// 直接把消息推送到Queue
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="msg"></param>
        void Publish(string queueName,string msg);
    }
}
