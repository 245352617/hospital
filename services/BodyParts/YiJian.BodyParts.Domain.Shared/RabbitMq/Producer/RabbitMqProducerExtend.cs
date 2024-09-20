using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Domain.Shared.RabbitMq.Producer
{
    public static class RabbitMqProducerExtend
    {
        /// <summary>
        /// 注入Rabbitmq的生产者
        /// </summary>
        /// <param name="services"></param>
        /// <param name="option"></param>
        public static void ConfigureRabbitMqProducer(this IServiceCollection services, Action<RabbitMqOption> option)
        {
            RabbitMqOption opt = new RabbitMqOption();
            option.Invoke(opt);

            if (!VerifyRabbitMqOption(opt))
            {
                return;
            }

            var client = new ConnectionFactory();
            client.HostName = opt.Address;
            client.Port = int.Parse(opt.Port);
            client.VirtualHost = opt.VirtualHost;
            client.UserName = opt.UserName;
            client.Password = opt.Password;

            services.AddSingleton(client);
            services.AddSingleton<IRabbitMqProducer, RabbitMqProducer>();
            Log.Information($"Rabbitmq Producer 注入成功");
        }

        /// <summary>
        /// 校验rabbitmq参数
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        private static bool VerifyRabbitMqOption(RabbitMqOption option)
        {
            var bol = true;
            var errMsg = string.Empty;

            if (string.IsNullOrEmpty(option.Address))
            {
                bol = bol && false;
                errMsg += $"，RabbitMq地址{nameof(option.Address)}不能为空";
            }
            if (string.IsNullOrEmpty(option.Port) || !int.TryParse(option.Port, out int port))
            {
                bol = bol && false;
                errMsg += $"，RabbitMq地址{nameof(option.Port)}不能为空且必须是数值类型";
            }
            if (string.IsNullOrEmpty(option.UserName))
            {
                bol = bol && false;
                errMsg += $"，RabbitMq地址{nameof(option.UserName)}不能为空";
            }
            if (string.IsNullOrEmpty(option.Password))
            {
                bol = bol && false;
                errMsg += $"，RabbitMq地址{nameof(option.Password)}不能为空";
            }
            if (string.IsNullOrEmpty(option.VirtualHost))
            {
                option.VirtualHost = "/";
            }

            if (!bol)
            {
                Log.Error($"Rabbitmq Producer 注入失败{errMsg}");
            }

            return bol;
        }
    }
}
