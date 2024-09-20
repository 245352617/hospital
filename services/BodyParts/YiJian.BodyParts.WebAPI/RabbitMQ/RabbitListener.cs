using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace YiJian.BodyParts.WebAPI
{
 public class RabbitListener : IHostedService
    {
        private readonly IConnection connection;
        private readonly IModel channel;

        public RabbitListener(IOptions<AppConfiguration> options)
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory()
                {
                    HostName = options.Value.RabbitHost,
                    UserName = options.Value.RabbitUserName,
                    Password = options.Value.RabbitPassword
                };
                if (options.Value.RabbitPort > 0)
                {
                    factory.Port = options.Value.RabbitPort;
                }
                this.HostName = options.Value.RabbitHost;
                this.UserName = options.Value.RabbitUserName;
                this.Password = options.Value.RabbitPassword;
                this.Port = options.Value.RabbitPort;
                this.ExChange = options.Value.ExChangeName;

                this.connection = factory.CreateConnection();
                this.channel = connection.CreateModel();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Register();
            //SubscribeMq();
            return Task.CompletedTask;
        }

        public virtual void SubscribeMq()
        {
            
        }

        string HostName { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        int Port { get; set; }
        
        public string RouteKey { get; set; }
        public string QueueName{ get; set; }
        string ExChange { get; set; }

        public string ExChange1{ get; set; }

        // 处理消息的方法
        public virtual bool Process(string message)
        {
            return true;
        }

        // 注册消费者监听在这里
        public void Register()
        {
            Log.Information($"\n==>RabbitListener register \n\tHostName:{HostName}\n\tUserName:{UserName}\n\tPort:{Port}\n\tExChange：{ExChange}\n\tQueueName：{QueueName}\n\trouteKey:{RouteKey}");
            try
            {
                //交换机和队列绑定
                channel.QueueBind(QueueName, (ExChange1 == "" ? ExChange : ExChange1), RouteKey);
                //事件基本消费者
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                Log.Information("==>RabbitMQ连接成功!\n");

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var arr = body.ToArray();
                    var message = Encoding.UTF8.GetString(arr);
                    var result = Process(message);
                    if (result)
                    {
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                };
                channel.BasicConsume(queue: QueueName, consumer: consumer);
            }
            catch (Exception ex)
            {
                Log.Error(ex,ex.Message);
            }
            
        }

        public void DeRegister()
        {
            this.connection.Close();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.connection.Close();
            return Task.CompletedTask;
        }
    }
}