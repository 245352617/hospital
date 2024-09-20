using DotNetCore.CAP;
using YiJian.Job.BackgroundService.Contract;

namespace YiJian.Job.BackgroundService.RabbitMQ;

public class GatherMinioEmrJobService : IGatherMinioEmrJobService
{
    const string name = "job.check.patient.exist";

    private readonly ICapPublisher _capBus;
    private readonly ILogger<GatherMinioEmrJobService> _logger;

    public GatherMinioEmrJobService(ICapPublisher capBus, ILogger<GatherMinioEmrJobService> logger)
    {
        _capBus = capBus;
        _logger = logger;
    }

    /// <summary>
    /// 检查患者信息是否存在的作业
    /// </summary>
    public void Publish()
    { 
        _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}消息队列触发:[ {name} ]");
        _capBus.Publish(name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

    } 
     
}
