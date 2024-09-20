using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Domain.Shared.RabbitMq
{
    public class RabbitMqOption
    {
        /// <summary>
        /// rabbitmq地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// rabbitmq端口
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 虚拟路径，如果为空则默认“/”
        /// </summary>
        public string VirtualHost { get; set; }

        /// <summary>
        /// rabbitmq用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// rabbitmq密码
        /// </summary>
        public string Password { get; set; }
    }
}
