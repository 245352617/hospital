using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using YiJian.Apis;
using YiJian.ECIS.DDP;
using YiJian.ECIS.ShareModel.DDPs;
using YiJian.ECIS.ShareModel.DDPs.Requests;
using YiJian.ECIS.ShareModel.DDPs.Responses;
using YiJian.MasterData.External.LongGang.Medicines;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData.External.LongGang;

/// <summary>
/// HIS药品服务
/// </summary>
public class HisMedicineAppService : ApplicationService, IHisMedicineAppService
{
    private readonly IHISMedicineRepository _hisMedicineRepository;
    private readonly IMedicineFrequencyRepository _medicineFrequencyRepository;

    private readonly IOptionsMonitor<DdpHospital> _ddpHospitalOptionsMonitor;
    private readonly DdpHospital _ddpHospital;
    private readonly DdpSwitch _ddpSwitch;
    private readonly IDdpApiService _ddpApiService;

    /// <summary>
    /// HIS药品服务
    /// </summary> 
    public HisMedicineAppService(
        IHISMedicineRepository hisMedicineRepository,
        IMedicineFrequencyRepository medicineFrequencyRepository,
        IOptionsMonitor<DdpHospital> ddpHospitalOptionsMonitor,
        DdpSwitch ddpSwitch)
    {
        _hisMedicineRepository = hisMedicineRepository;
        _medicineFrequencyRepository = medicineFrequencyRepository;
        _ddpHospitalOptionsMonitor = ddpHospitalOptionsMonitor;
        _ddpHospital = _ddpHospitalOptionsMonitor.CurrentValue;
        _ddpSwitch = ddpSwitch;
        _ddpApiService = _ddpSwitch.CreateService(_ddpHospital);
    }

    /// <summary>
    /// 获取药品记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<PagedResultDto<MedicineData>> GetHisPagedListAsync(GetMedicinePagedInput input)
    {
        //开启DDP模式
        if (_ddpHospital.DdpSwitch)
        {
            return await GetHisPagedListByDdpAsync(input);
        }

        //龙岗中心医院（历史版本）
        return await GetHisPageListLGZXAsync(input);
    }

    /// <summary>
    /// 龙岗中心医院的库存药品
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private async Task<PagedResultDto<MedicineData>> GetHisPageListLGZXAsync(GetMedicinePagedInput input)
    {
        var list = new List<MedicineData>();

        var data = await _hisMedicineRepository.GetPagedListAsync(
             input.SkipCount,
             input.Size,
             input.Filter,
             input.Category,
             Convert.ToInt32(input.IsEmergency),
             input.PlatformType,
             input.PharmacyCode,
             input.EmergencySign);

        var items = ObjectMapper.Map<List<HISMedicine>, List<MedicineData>>(data.Item1);
        var frequencyList = await _medicineFrequencyRepository.GetListAsync();

        //填充频次信息
        items.ForEach(f =>
        {
            if (!f.FrequencyCode.IsNullOrWhiteSpace())
            {
                var frequency = frequencyList.FirstOrDefault(x => x.FrequencyCode == f.FrequencyCode);
                if (frequency != null)
                {
                    f.FrequencyTimes = frequency.Times;
                    f.FrequencyUnit = frequency.Unit;
                    f.FrequencyExecDayTimes = frequency.ExecDayTimes;
                    f.DailyFrequency = frequency.ThirdPartyId; // HIS频次编码
                }
            }
        });

        var result = new PagedResultDto<MedicineData>(totalCount: data.Item2, items.ToList());
        return result;
    }

    /// <summary>
    /// 开启DDP模式调用DDP
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private async Task<PagedResultDto<MedicineData>> GetHisPagedListByDdpAsync(GetMedicinePagedInput input)
    {
        //构造请求参数
        var request = new DdpHisMedicineSearchRequest
        {
            Index = 1,
            Size = 50,
            EmergencySign = input.EmergencySign,
            NameOrPyCode = input.Filter,
            PharmacyCode = input.PharmacyCode,
        };

        var ddpResponse = await _ddpApiService.GetHisMedicinesPageAsync(request);
        List<MedicineData> drugs = ObjectMapper.Map<List<DdpHisMedicineResponse>, List<MedicineData>>(ddpResponse.Data);

        drugs = drugs.Distinct(new MedicineData()).ToList();
        if (!string.IsNullOrEmpty(input.Filter))
            drugs = drugs.OrderByDescending(x => x.PyCode.StartsWith(input.Filter.ToUpper()) || x.Name.StartsWith(input.Filter) ? 1 : 0).ToList();

        return new PagedResultDto<MedicineData>(50, drugs);
    }

}
