using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;
using YiJian.MasterData.External.LongGang.Teat;
using YiJian.MasterData.Treats;

namespace YiJian.MasterData.External.LongGang;

public class TreatEventHandler : MasterDataAppService, ILongGangHospitalEventHandler, IDistributedEventHandler<TreatsEto>,
        ITransientDependency
{
    public ILogger<TreatEventHandler> _logger { get; set; }
    public ITreatRepository TreatRepository { get; set; }
    public async Task HandleEventAsync(TreatsEto eventData)
    {

        var uow = UnitOfWorkManager.Begin();

        _logger.LogInformation("RabbitMQ诊疗材料内容:{0}", JsonConvert.SerializeObject(eventData));
        try
        {
            if (eventData == null) return;
            var selectTreat = await TreatRepository.FirstOrDefaultAsync(x => x.TreatCode == eventData.ProjectId && x.CategoryCode == eventData.ProjectType);

            //1.新增
            if (selectTreat == null)
            {
                var treat = new Treat()
                {
                    TreatCode = eventData.ProjectId,
                    TreatName = eventData.ProjectName,
                    CategoryName = eventData.ProjectTypeName,
                    CategoryCode = eventData.ProjectType,
                    Price = (decimal)(eventData.Price ?? 0),
                    OtherPrice =eventData.Additional=="1"? decimal.Parse(eventData.ChargeAmount.ToString("f2")):0,
                    Additional = eventData.Additional=="1",
                    PyCode = eventData.SpellCode,
                    Unit = eventData.Unit,
                    WbCode = eventData.ProjectName.FirstLetterWB(),
                    ProjectMerge = eventData.ProjectMerge,
                    YBInneCode = eventData.YBInneCode,
                    MeducalInsuranceCode = eventData.MeducalInsuranceCode,
                    DepExecutionType = eventData.DepExecutionType,
                    DepExecutionRules = eventData.DepExecutionRules,
                };
                await TreatRepository.InsertAsync(treat);

                _logger.LogInformation($"{ DateTime.Now}新增治疗材料成功{JsonConvert.SerializeObject(treat)}");
            }

            else
            {
                if (eventData.UseFlag != (selectTreat.IsDeleted == true ? "1" : "0"))
                {
                    selectTreat.IsDeleted = true;
                    await TreatRepository.UpdateAsync(selectTreat);
                    _logger.LogInformation($"{ DateTime.Now}删除治疗材料成功{JsonConvert.SerializeObject(selectTreat)}");
                }
                else
                {

                    //2.修改 
                    selectTreat.TreatName = eventData.ProjectName;
                    selectTreat.CategoryName = eventData.ProjectTypeName;
                    selectTreat.Price = (decimal)(eventData.Price ?? 0);
                    selectTreat.OtherPrice = eventData.Additional == "1"
                        ? decimal.Parse(eventData.ChargeAmount.ToString("f2"))
                        : 0;
                    selectTreat.Additional = eventData.Additional == "1";
                    selectTreat.PyCode = eventData.SpellCode;
                    selectTreat.Unit = eventData.Unit;
                    selectTreat.WbCode = eventData.ProjectName.FirstLetterWB();
                    selectTreat.ProjectMerge = eventData.ProjectMerge;
                    if (!string.IsNullOrEmpty(eventData.MeducalInsuranceCode)) {
                        selectTreat.MeducalInsuranceCode = eventData.MeducalInsuranceCode;
                    }
                    if (!string.IsNullOrEmpty(eventData.YBInneCode))
                    {
                        selectTreat.YBInneCode = eventData.YBInneCode;
                    }
                    await TreatRepository.UpdateAsync(selectTreat);
                    _logger.LogInformation($"{ DateTime.Now}更新治疗材料成功{JsonConvert.SerializeObject(selectTreat)}");
                }
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
