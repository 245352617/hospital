using System;
using System.Drawing;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Newtonsoft.Json;
using YiJian.BodyParts.Service;

namespace YiJian.BodyParts.WebAPI
{
    public class EmplEventsListener : RabbitListener
    {
        public void WriteLine(string msg)
        {
            Console.WriteLine(msg, Color.Green);
        }

        private readonly ILogger<RabbitListener> _logger;

        // 因为Process函数是委托回调,直接将其他Service注入的话两者不在一个scope,
        // 这里要调用其他的Service实例只能用IServiceProvider CreateScope后获取实例对象
        private readonly IServiceProvider _services;

        public EmplEventsListener(IServiceProvider services, IOptions<AppConfiguration> options,
            ILogger<RabbitListener> logger) : base(options)
        {
            _logger = logger;
            _services = services;
            this.QueueName = "PatientQueue2";
            this.RouteKey = "EmplEventsEvents";
        }

        public override bool Process(string message)
        {
            Log.Information($"==>收到rabbitmq 信息：{message}");
            try
            {
                using (var scope = _services.CreateScope())
                {
                    //获取服务
                    //var userAppService = scope.ServiceProvider.GetRequiredService<UserAppService>();
                    //var processResult = userAppService.ProcessUserEvent(message);
                    //Log.Information("======>rabbit 传入过来的数据");
                    //Log.Information("======>" + message);
                    //Log.Information("======>处理结果");
                    //Log.Information("==>" + processResult);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Process fail,error:{ex.Message},stackTrace:{ex.StackTrace},message:{message}");
                _logger.LogError(-1, ex, "Process fail");
                return false;
            }
        }
    }
}