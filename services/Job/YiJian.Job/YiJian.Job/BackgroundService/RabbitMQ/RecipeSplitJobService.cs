using DotNetCore.CAP;
using YiJian.Job.BackgroundService.Contract;

namespace YiJian.Job.BackgroundService.RabbitMQ;

public class RecipeSplitJobService : IRecipeSplitJobService
{
    const string name = "job.recipe.split";

    private readonly ICapPublisher _capBus;
    private readonly ILogger<CheckPatientExistJobService> _logger;

    public RecipeSplitJobService(ICapPublisher capBus, ILogger<CheckPatientExistJobService> logger)
    {
        _capBus = capBus;
        _logger = logger;
    }

    /// <summary>
    /// 长嘱医嘱拆分
    /// </summary>
    public void Publish()
    {
        _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}消息队列触发:[ {name} ]");
        _capBus.Publish(name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}
