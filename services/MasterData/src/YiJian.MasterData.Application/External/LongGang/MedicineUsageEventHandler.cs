using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;
using YiJian.MasterData.External.LongGang.Usage;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData.External.LongGang;

public class MedicineUsageEventHandler : MasterDataAppService, ILongGangHospitalEventHandler, IDistributedEventHandler<UsagesEto>,
            ITransientDependency
{
    public ILogger<MedicineUsageEventHandler> _logger { get; set; }
    public IMedicineUsageRepository MedicineUsageRepository { get; set; }
    public async Task HandleEventAsync(UsagesEto eventData)
    {
        var uow = UnitOfWorkManager.Begin();

        try
        {
            if (eventData == null) return;
            _logger.LogInformation("RabbitMQ药品用法信息内容:{0}", JsonConvert.SerializeObject(eventData));
            var selectUsage = await MedicineUsageRepository.FindAsync(x => x.UsageCode == eventData.DrugUsageId);
            //1.新增
            if (selectUsage == null)
            {
                var usage = new MedicineUsage()
                {
                    UsageCode = eventData.DrugUsageId,
                    UsageName = eventData.DrugUsageName,
                    FullName = eventData.DrugUsageName,
                    PyCode = eventData.SpellCode,
                    WbCode = eventData.DrugUsageName.FirstLetterWB(),
                    AddCard = eventData.AddCard,
                    TreatCode = eventData.Project
                };
                await MedicineUsageRepository.InsertAsync(usage);

                _logger.LogInformation($"{ DateTime.Now}新增药品用法成功{JsonConvert.SerializeObject(usage)}");
            }

            else
            {
                //2.修改 
                selectUsage.UsageName = eventData.DrugUsageName;
                selectUsage.FullName = eventData.DrugUsageName;
                selectUsage.PyCode = eventData.SpellCode;
                selectUsage.WbCode = eventData.DrugUsageName.FirstLetterWB();
                selectUsage.AddCard = eventData.AddCard;
                selectUsage.TreatCode = eventData.Project;
                await MedicineUsageRepository.UpdateAsync(selectUsage);
                _logger.LogInformation($"{ DateTime.Now}更新药品用法成功{JsonConvert.SerializeObject(selectUsage)}");


            }
            await uow.CompleteAsync();
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
            await uow.RollbackAsync();
        }
    }
}
