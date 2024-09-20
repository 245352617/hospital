using DotNetCore.CAP;
using YiJian.Job.BackgroundService.Contract;

namespace YiJian.Job.BackgroundService.RabbitMQ;

public class SynctoxicJobService: ISynctoxicJobService
{
    const string name = "job.masterdata.synctoxic";

    private readonly ICapPublisher _capBus;
    private readonly ILogger<SynctoxicJobService> _logger;

    public SynctoxicJobService(ICapPublisher capBus, ILogger<SynctoxicJobService> logger)
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
