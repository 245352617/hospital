using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;
using YiJian.MasterData.External.LongGang.Frequency;

namespace YiJian.MasterData.External.LongGang;

public class MedicineFrequencyEventHandler : MasterDataAppService, ILongGangHospitalEventHandler, IDistributedEventHandler<FrequencyEto>,
              ITransientDependency
{
    public ILogger<MedicineUsageEventHandler> _logger { get; set; }
    public IMedicineFrequencyRepository MedicineFrequencyRepository { get; set; }
    public async Task HandleEventAsync(FrequencyEto eventData)
    {
        var uow = UnitOfWorkManager.Begin();

        try
        {
            if (eventData == null) return;
            _logger.LogInformation("RabbitMQ药品频次信息内容:{0}", JsonConvert.SerializeObject(eventData));
            var selectFrequency = await MedicineFrequencyRepository.FindAsync(x => x.FrequencyCode == eventData.DrugfrequencyCode);
            //1.新增
            if (selectFrequency == null)
            {
                var frequency = new MedicineFrequency()
                {
                    ThirdPartyId = eventData.DrugfrequencyId,
                    FrequencyCode = eventData.DrugfrequencyCode,
                    FrequencyName = eventData.DrugfrequencyName,
                    FullName = eventData.DrugfrequencyName,
                    Times = int.Parse(eventData.DailyFrequency == "" ? "0" : eventData.DailyFrequency),
                    ExecDayTimes = eventData.ExecutionTime,
                    Sort = int.Parse(eventData.ArrangementOrder == "" ? "0" : eventData.ArrangementOrder)
                };
                await MedicineFrequencyRepository.InsertAsync(frequency);

                _logger.LogInformation($"{ DateTime.Now}新增药品频次成功{JsonConvert.SerializeObject(frequency)}");
            }

            else
            {

                //2.修改 
                selectFrequency.FrequencyName = eventData.DrugfrequencyName;
                selectFrequency.FullName = eventData.DrugfrequencyName;
                selectFrequency.Times = int.Parse(eventData.DailyFrequency == "" ? "0" : eventData.DailyFrequency);
                selectFrequency.ExecDayTimes = eventData.ExecutionTime;
                selectFrequency.Sort = int.Parse(eventData.ArrangementOrder == "" ? "0" : eventData.ArrangementOrder);
                await MedicineFrequencyRepository.UpdateAsync(selectFrequency);
                _logger.LogInformation($"{ DateTime.Now}更新药品频次成功{JsonConvert.SerializeObject(selectFrequency)}");


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
