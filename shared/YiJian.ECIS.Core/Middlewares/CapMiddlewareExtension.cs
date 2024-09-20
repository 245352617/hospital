using DotNetCore.CAP;
using DotNetCore.CAP.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Volo.Abp.Modularity;

namespace YiJian.ECIS.Core.Middlewares;

public static class CapMiddlewareExtension
{
    public static CapBuilder AddEcisDefaultCap(this IServiceCollection services)
    {
        var configuration = services.GetConfiguration();
        var connectionStringName = configuration["RabbitMQ:EventBus:ConnectionStringName"];
        var builder = services.AddCap(capOptions =>
         {
             Console.WriteLine("[-] AddCap");
             capOptions.UseSqlServer(opt =>
             {
                 opt.ConnectionString = configuration[$"ConnectionStrings:{connectionStringName}"];
                 Console.WriteLine($"[-] UseSqlServer , ConnectionString ={opt.ConnectionString}");
             });
         });
        return builder;
    }

    public static ServiceConfigurationContext AddEcisDefaultCapEventBus(this ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var serviceConfigurationContext = context.AddCapEventBus(capOptions =>
          {
              capOptions.ProducerThreadCount = Environment.ProcessorCount;
              capOptions.ConsumerThreadCount = Environment.ProcessorCount;
              capOptions.DefaultGroupName = configuration["RabbitMQ:EventBus:DefaultGroupName"];
              capOptions.FailedThresholdCallback = (failed) =>
              {
                  var logger = failed.ServiceProvider.GetService<ILogger<ECISCoreModule>>();
                  logger.LogError($@"消息类型 {failed.MessageType} 失败，重试次数 {capOptions.FailedRetryCount} , 
                        请手动处理. 消息名: {failed.Message.GetName()}");
              };

              capOptions.UseRabbitMQ(options =>
              {
                  options.ExchangeName = configuration["RabbitMQ:EventBus:ExchangeName"];
                  options.UserName = configuration["RabbitMQ:Connections:Default:UserName"];
                  options.Password = configuration["RabbitMQ:Connections:Default:Password"];
                  options.HostName = configuration["RabbitMQ:Connections:Default:HostName"];
                  options.Port = int.Parse(configuration["RabbitMQ:Connections:Default:Port"] ?? "5672");
                  Console.WriteLine($"[-] UseRabbitMQ\n " +
                      $"ExchangeName={options.ExchangeName}\n " +
                      $"UserName={options.UserName}\n " +
                      $"Password={options.Password}\n " +
                      $"HostName={options.HostName}\n " +
                      $"Port={options.Port}");
              });
              // Configure the host of RabbitMQ
              capOptions.UseDashboard();
          });

        return serviceConfigurationContext;
    }
}
