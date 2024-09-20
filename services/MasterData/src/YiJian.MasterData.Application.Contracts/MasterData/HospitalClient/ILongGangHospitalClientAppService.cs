using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.MasterData.External.LongGang.Medicines;
using YiJian.MasterData.MasterData.HospitalClient.LabAndExamHis;

namespace YiJian.MasterData.MasterData.HospitalClient;

public interface ILongGangHospitalClientAppService : IApplicationService
{
    /// <summary>
    /// 同步检验数据
    /// </summary>
    Task SyncAllLabInfoAsync(List<LabAndExamEto> model);

    /// <summary>
    /// 同步检查相关数据
    /// </summary>
    Task SyncAllExamInfoAsync(List<LabAndExamEto> model);

    /// <summary>
    /// 同步诊疗信息
    /// </summary>
    Task SyncAllTreatsInfoAsync(List<TreatsEto> treatHis);

    /// <summary>
    /// 同步药品用法
    /// </summary>
    Task SyncAllUsagesAsync(List<UsagesEto> usagesHis);

    /// <summary>
    /// 同步药品频次
    /// </summary>
    Task SyncFrequencyAsync(List<FrequencyEto> frequencyHis);

    /// <summary>
    /// 同步药品信息
    /// </summary>
    Task SyncMedicineAsync(List<MedicinesEto> paramsList);

    /// <summary>
    /// 全量同步龙岗字典信息
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    Task HandleEventAsync(FullHandlerEto eventData);
}