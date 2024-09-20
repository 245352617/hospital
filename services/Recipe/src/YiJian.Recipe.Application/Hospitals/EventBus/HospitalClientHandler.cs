using DotNetCore.CAP;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;
using YiJian.ECIS.ShareModel.Extensions;
using YiJian.Hospitals.Eto;
using YiJian.Recipes.DoctorsAdvices.Contracts;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.MasterData.MasterData;

/// <summary>
/// 龙岗中心医院订阅处理类
/// </summary>
public class HospitalClientHandler : IDistributedEventHandler<HisChannelBillStatusHanderEto>,
    ITransientDependency
{
    private ICapPublisher _capPublisher;

    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IDoctorsAdviceRepository _doctorsAdviceRepository;
    private readonly ILogger<HospitalClientHandler> _logger;

    /// <summary>
    /// 龙岗中心医院订阅处理类
    /// </summary> 
    public HospitalClientHandler(
        ICapPublisher capPublisher,
        ILogger<HospitalClientHandler> logger,
        IConfiguration configuration,
        IPrescriptionRepository prescriptionRepository,
        IDoctorsAdviceRepository doctorsAdviceRepository)
    {

        _capPublisher = capPublisher;
        _logger = logger;
        _doctorsAdviceRepository = doctorsAdviceRepository;
        _prescriptionRepository = prescriptionRepository;
        _logger = logger;

    }

    /// <summary>
    /// MQ同步医嘱缴费状态
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    public async Task HandleEventAsync(HisChannelBillStatusHanderEto eventData)
    {
        try
        {
            _logger.LogInformation("更新医嘱状态Start：{@EventData}", eventData);
            if (string.IsNullOrWhiteSpace(eventData.VisSerialNo) || string.IsNullOrWhiteSpace(eventData.BillState) || string.IsNullOrWhiteSpace(eventData.MzBillId))
            {
                return;
            }

            var billState = 0;
            billState = int.Parse(eventData.BillState);
            var mzBillIds = eventData.MzBillId.Split(",").ToList();
            var prescriptionNos = new List<string>();
            mzBillIds.ForEach(x =>
            {
                prescriptionNos.Add(x);
            });

            var data = await _prescriptionRepository.GetPrescriptionsByBillNosAsync(eventData.VisSerialNo, prescriptionNos);
            if (!data.Any()) return;

            //再查询一遍，确认再请求http前拿最近的数据

            var adviceids = data.Select(s => s.DoctorsAdviceId).ToList();
            var advices = await _doctorsAdviceRepository.GetDoctorsAdvicesByIdsAsync(adviceids);

            var updatepartAdvices = new List<DoctorsAdvice>();
            var updateAdvices = new List<DoctorsAdvice>();

            foreach (var current in data)
            {
                if (billState == 0) continue;
                var currentAdvice = advices.FirstOrDefault(w => w.Id == current.DoctorsAdviceId);
                if (currentAdvice == null) continue;
                if (currentAdvice.Status == ERecipeStatus.Cancelled) continue;

                current.BillState = billState;
                var status = ERecipeStatus.Submitted;

                //当HIS上的状态和急诊的状态一致的时候不处理 
                //已缴费
                if (billState == 1 && currentAdvice.Status == ERecipeStatus.PayOff) continue;
                //已执行
                if (billState == 2 && currentAdvice.Status == ERecipeStatus.Executed) continue;
                //已退费
                if (billState == 3 && currentAdvice.Status == ERecipeStatus.ReturnPremium) continue;
                //已作废
                if (billState == -1 && currentAdvice.Status == ERecipeStatus.Cancelled) continue;

                _logger.LogInformation("[-] VisSerialNo={VisSerialNo},当前单有变化，" +
                    "MzBillId={MzBillId}状态从[{Status}]->BillState=[{BillState} , BillState=[1:已缴费,2:已执行,3:已退费,-1:已作废]] , " +
                    "发票号InvoiceNo={InvoiceNo}",
                    eventData.VisSerialNo, eventData.MzBillId, currentAdvice.Status.GetDescription(), billState, eventData.InvoiceNo);

                switch (billState)
                {
                    case 1: //已缴费
                            //缴费状态只有一下这几种情况才会更新
                        if (currentAdvice.Status == ERecipeStatus.Submitted ||
                            currentAdvice.Status == ERecipeStatus.Confirmed ||
                            currentAdvice.Status == ERecipeStatus.Executed ||
                            currentAdvice.Status == ERecipeStatus.PayOff)
                        {
                            status = ERecipeStatus.PayOff;
                            currentAdvice.PayStatus = EPayStatus.HavePaid;
                            currentAdvice.InvoiceNo = eventData.InvoiceNo;
                        }
                        break;
                    case 2: //已执行
                        if (currentAdvice.Status == ERecipeStatus.Submitted ||
                            currentAdvice.Status == ERecipeStatus.Confirmed ||
                            currentAdvice.Status == ERecipeStatus.PayOff ||
                            currentAdvice.Status == ERecipeStatus.Executed)
                        {
                            status = ERecipeStatus.Executed;
                        }
                        break;
                    case 3: //已退费 
                        status = ERecipeStatus.ReturnPremium;
                        currentAdvice.PayStatus = EPayStatus.HaveRefund;
                        break;
                    case -1: //已作废
                        status = ERecipeStatus.Cancelled;
                        break;
                    default:
                        break;
                }
                currentAdvice.Status = status;
                updatepartAdvices.Add(currentAdvice);
                updateAdvices.Add(currentAdvice);
            }

            if (updateAdvices.Any())
            {
                await _doctorsAdviceRepository.UpdateManyAsync(updatepartAdvices);
                _logger.LogDebug("推送HIS返回的状态到护理服务:：" + Newtonsoft.Json.JsonConvert.SerializeObject(updateAdvices));
                //推送HIS返回的状态到护理服务
                SendAdviceHisStatus(updateAdvices);
            }

            //更新单子的状态
            if (data.Any())
            {
                await _prescriptionRepository.UpdateManyAsync(data);
                return; //有状态更新并且成功
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"HIS传递过来的医嘱状态更新出现异常，就诊流水号：{eventData.VisSerialNo}, 订单单号：{eventData.MzBillId}, 订单状态：{eventData.BillState}");
            return;
        }
        return; //没有状态更新

    }


    /// <summary>
    /// 推送HIS返回的状态到护理服务
    /// </summary>
    /// <param name="updateAdvices"></param>
    private void SendAdviceHisStatus(List<DoctorsAdvice> updateAdvices)
    {
        try
        {
            var updateAdvicesGroup = updateAdvices.Where(w => w.PlatformType == EPlatformType.EmergencyTreatment)
                .GroupBy(g => g.PIID);
            foreach (var item in updateAdvicesGroup)
            {
                var values = item.Select(s => new DoctorsAdviceHisStatusEto
                {
                    Status = (EDoctorsAdviceStatus)s.Status,
                    PayStatus = s.PayStatus,
                    RecipeId = s.Id
                }).ToList();

                //推送HIS状态给nursing
                DoctorsAdviceHisEto eto = new DoctorsAdviceHisEto
                {
                    PlatformType = (int)EPlatformType.EmergencyTreatment,
                    PIID = item.Key,
                    DoctorsAdviceHisStatusList = values
                };
                _capPublisher.Publish("recipe.to.nursing.syncHisStatus", eto);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "推送HIS状态给Nursing异常：" + ex.Message);
        }
    }

}