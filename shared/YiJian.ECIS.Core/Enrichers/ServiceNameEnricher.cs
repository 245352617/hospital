using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Serilog.Events;
using System;

namespace YiJian.ECIS.Core.Enrichers;

/// <summary>
/// 日志服务名增强
/// </summary>
public class ServiceNameEnricher : ILogEventEnricher
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ServiceNameEnricher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logEvent"></param>
    /// <param name="propertyFactory"></param>
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
        var appName = configuration["App:Name"];
        if (!string.IsNullOrEmpty(appName))
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ServiceName", appName));
        }
    }
}