using DotNetCore.CAP;
using YiJian.Job.BackgroundService.Contract;

namespace YiJian.Job.BackgroundService.RabbitMQ
{
    public class PatientStatusFromHisService : IPatientStatusFromHisService
    {
        const string name = "job.update.patientstatus.fromhis";

        private readonly ICapPublisher _capBus;
        private readonly ILogger<PatientStatusFromHisService> _logger;

        public PatientStatusFromHisService(ICapPublisher capBus, ILogger<PatientStatusFromHisService> logger)
        {
            _capBus = capBus;
            _logger = logger;
        }

        /// <summary>
        /// 同步His患者状态
        /// </summary>
        public void Publish()
        {
            _logger.LogInformation($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}消息队列触发:[ {name} ]");
            _capBus.Publish(name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        }

    }
}