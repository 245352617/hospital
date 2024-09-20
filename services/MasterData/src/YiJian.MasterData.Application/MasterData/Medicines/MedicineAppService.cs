using DotNetCore.CAP;
using MasterDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Volo.Abp.Application.Dtos;
using YiJian.Apis;
using YiJian.ECIS.DDP;
using YiJian.ECIS.ShareModel.DDPs;
using YiJian.ECIS.ShareModel.DDPs.Requests;
using YiJian.ECIS.ShareModel.DDPs.Responses;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.MasterData.AllItems;
using YiJian.MasterData.MasterData;

namespace YiJian.MasterData.Medicines;

/// <summary>
/// 药品字典 API
/// </summary>
[Authorize]
public partial class MedicineAppService : MasterDataAppService, IMedicineAppService, ICapSubscribe
{
    private readonly ICapPublisher _capPublisher;
    private readonly IMedicineRepository _medicineRepository;
    private readonly IAllItemByThreePartyAppService _allItemAppService;
    private readonly IMedicineFrequencyRepository _medicineFrequencyRepository;
    private readonly ILogger<MedicineAppService> _logger;
    public IHISMedicineRepository HisMedicineRepository { get; set; }

    //DDP
    private readonly IOptionsMonitor<DdpHospital> _ddpHospitalOptionsMonitor;
    private readonly DdpHospital _ddpHospital;
    private readonly DdpSwitch _ddpSwitch;
    private readonly IDdpApiService _ddpApiService;

    private readonly IMemoryCache _cache;

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary> 
    public MedicineAppService(
        ICapPublisher capPublisher,
        IMedicineRepository medicineRepository,
        IMedicineFrequencyRepository medicineFrequencyRepository, IAllItemByThreePartyAppService allItemAppService,
        ILogger<MedicineAppService> logger,
        IOptionsMonitor<DdpHospital> ddpHospitalOptionsMonitor,
        DdpSwitch ddpSwitch,
        IMemoryCache cache)
    {
        this._capPublisher = capPublisher;
        _medicineRepository = medicineRepository;
        _medicineFrequencyRepository = medicineFrequencyRepository;
        _allItemAppService = allItemAppService;
        _logger = logger;

        _ddpHospitalOptionsMonitor = ddpHospitalOptionsMonitor;
        _ddpHospital = _ddpHospitalOptionsMonitor.CurrentValue;
        _ddpSwitch = ddpSwitch;
        _ddpApiService = _ddpSwitch.CreateService(_ddpHospital);
        _cache = cache;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> CreateAsync(MedicineCreation input)
    {
        var model = ObjectMapper.Map<MedicineCreation, Medicine>(input);
        var result = await _medicineRepository.InsertAsync(model);

        if (result != null && input.PlatformType == PlatformType.PreHospital)
        {
            await _allItemAppService.SaveAsync(new AllItemCreation
            {
                Code = input.MedicineCode,
                Name = input.MedicineName,
                CategoryCode = "Medicine",
                CategoryName = "药品",
                Price = input.Price,
                Unit = input.Unit,
                TypeCode = "PreHospital",
                TypeName = "院前急救"
            });

            return result.Id;
        }

        // 发布药品同步消息
        var eto = ObjectMapper.Map<Medicine, GrpcMedicineModel>(result);
        await this._capPublisher.PublishAsync("sync.masterdata.medicine", eto);

        return -1;
    }

    #endregion Create

    #region Update

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> UpdateAsync(MedicineUpdate input)
    {
        var medicine = await (await _medicineRepository.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == input.Id);
        if (medicine == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        //拿取旧是否急救，旧的是true，新的是否急救为false，就删除该记录
        var oldAid = medicine.PlatformType;
        medicine.Modify(input.Name, // 药品名称
            input.Alias, // 别名
            input.MedicineProperty,
            input.Price, // 基本单位价格
            input.Remark, // 备注
            input.IsFirstAid, // 是否急救药
            input.UsageCode, // 药物用途
            input.PlatformType, input.Unpack);
        await _medicineRepository.UpdateAsync(medicine);
        var result = await _medicineRepository.UpdateAsync(medicine);
        if (input.PlatformType == PlatformType.PreHospital)
        {
            await _allItemAppService.SaveAsync(new AllItemCreation
            {
                Code = input.Id.ToString(), //药品编码有重复，使用编码
                Name = medicine.MedicineName,
                CategoryCode = "Medicine",
                CategoryName = "药品",
                Price = input.Price,
                Unit = medicine.Unit,
                TypeCode = "PreHospital",
                TypeName = "院前急救",
            });
        }
        else if (oldAid == PlatformType.PreHospital && input.PlatformType != PlatformType.PreHospital)
        {
            await _allItemAppService.DeleteAsync(medicine.Id.ToString(), "Medicine", "PreHospital");
        }

        // 发布药品同步消息
        var eto = ObjectMapper.Map<Medicine, GrpcMedicineModel>(result);
        await this._capPublisher.PublishAsync("sync.masterdata.medicine", eto);
        return result.Id;
    }

    #endregion Update

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<MedicineData> GetAsync(int id)
    {
        var medicine = await _medicineRepository.GetAsync(id);
        if (medicine == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        var result = ObjectMapper.Map<Medicine, MedicineData>(medicine);
        if (!result.FrequencyCode.IsNullOrWhiteSpace())
        {
            var frequency =
                await (await _medicineFrequencyRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                    x.FrequencyCode == result.FrequencyCode);
            result.FrequencyTimes = frequency.Times;
            result.FrequencyUnit = frequency.Unit;
            result.FrequencyExecDayTimes = frequency.ExecDayTimes;
            result.DailyFrequency = frequency.ThirdPartyId; // HIS频次编码
        }

        return result;
    }

    /// <summary>
    /// 查询his药品医保信息
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<List<PKUQueryHisYbInfoResponse>> GetYbInfoAsync(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new EcisBusinessException(message: "请求参数为空");
        }

        var ddpResponse = await _ddpApiService.GetHisYbInfoAsync(name);
        if (ddpResponse.Code != 200)
        {
            _logger.LogError($"查询his药品医保信息异常:[{ddpResponse.Code}],{ddpResponse.Msg}");
            throw new EcisBusinessException(message: "查询his药品医保信息异常");
        }

        return ddpResponse.Data;
    }

    /// <summary>
    /// 获取药品信息
    /// </summary>
    /// <param name="code">药品代码</param>
    /// <param name="specification">规格</param>
    /// <param name="pharmacyCode">药方代码</param>
    /// <param name="factoryCode">厂家代码</param>
    /// <returns></returns>
    [Obsolete]
    public async Task<MedicineData> GetAsync(string code, string specification, string pharmacyCode, string factoryCode)
    {
        specification = HttpUtility.UrlDecode(specification);

        _logger.LogInformation("获取药品信息=> specification = " + specification);

        var medicine = await (await _medicineRepository.GetQueryableAsync()).FirstOrDefaultAsync(
            x => x.MedicineCode == code && x.Specification == specification
            && x.PharmacyCode == pharmacyCode && x.FactoryCode == factoryCode);
        if (medicine == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        var result = ObjectMapper.Map<Medicine, MedicineData>(medicine);
        if (!result.FrequencyCode.IsNullOrWhiteSpace())
        {
            var frequency =
                await (await _medicineFrequencyRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                    x.FrequencyCode == result.FrequencyCode);
            result.FrequencyTimes = frequency.Times;
            result.FrequencyUnit = frequency.Unit;
            result.FrequencyExecDayTimes = frequency.ExecDayTimes;
        }

        return result;
    }

    /// <summary>
    /// 根据药品代码，规格，药房代码和厂家代码获取药品信息
    /// </summary>
    /// <param name="code">药品代码</param>
    /// <param name="specification">规格</param>
    /// <param name="pharmacyCode">药房代码</param>
    /// <param name="factoryCode">厂家代码</param>
    /// <returns></returns>
    public async Task<MedicineData> GetMedicineInfoAsync(string code, string specification, string pharmacyCode, string factoryCode)
    {
        try
        {
            //调用DDP模式
            if (_ddpHospital.DdpSwitch)
            {
                return await GetMedicineInfoByDdpAsync(code, specification, pharmacyCode, factoryCode);
            }
            else
            {
                return await GetMedicineInfoByLGZXAsync(code, specification, pharmacyCode, factoryCode);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"根据药品代码，规格，药房代码和厂家代码获取药品信息：{ex.Message}");
            return null; //TODO 
        }
    }

    /// <summary>
    /// 清除HIS DDP过来的药品缓存数据，方便处理紧急问题
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    public bool RemoveMedicinesCache()
    {
        try
        {
            _cache.Remove(DdpDCModel.DDP_HIS_MEDICINES_KEY);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// 通过Ddp获取药品的信息
    /// </summary>
    /// <param name="code"></param>
    /// <param name="specification"></param>
    /// <param name="pharmacyCode"></param>
    /// <param name="factoryCode"></param>
    /// <returns></returns>
    private async Task<MedicineData> GetMedicineInfoByDdpAsync(string code, string specification, string pharmacyCode, string factoryCode)
    {
        var request = new DdpHisMedicineByPropRequest
        {
            MedicineCode = code,
            Specification = specification,
            FactoryCode = factoryCode,
        };
        var ddpResponse = await _ddpApiService.GetHisMedicineAsync(request);
        if (ddpResponse.Code != 200)
        {
            _logger.LogError($"获取药品信息异常:[{ddpResponse.Code}],{ddpResponse.Msg}");
            throw new EcisBusinessException(message: "获取药品信息异常");
        }
        if (ddpResponse.Data == null || !ddpResponse.Data.Any(w => w.PharmacyCode == w.PharmacyCode))
        {
            throw new EcisBusinessException(message: "数据不存在");
        }
        var data = ddpResponse.Data.FirstOrDefault(w => w.PharmacyCode == w.PharmacyCode);

        return new MedicineData
        {
            Alias = data.Alias,
            AliasPyCode = data.AliasPyCode,
            AliasWbCode = data.AliasWbCode,
            AntibioticLevel = (int?)data.AntibioticLevel,
            AntibioticPermission = data.AntibioticPermission,
            BaseFlag = data.BaseFlag,
            BigPackFactor = data.BigPackFactor,
            BigPackPrice = data.BigPackPrice,
            BigPackUnit = data.BigPackUnit,
            CategoryCode = data.CategoryCode,
            CategoryName = data.CategoryName,
            ChildrenPrice = data.ChildrenPrice,
            Code = data.Code,
            DailyFrequency = data.DailyFrequency,
            DefaultDosage = data.DefaultDosage,
            DosageForm = data.DosageForm,
            DosageQty = data.DosageQty,
            DosageUnit = data.DosageUnit,
            EmergencySign = data.EmergencySign,
            ExecDeptCode = data.ExecDeptCode,
            ExecDeptName = data.ExecDeptName,
            FactoryCode = data.FactoryCode,
            FactoryName = data.FactoryName,
            FixPrice = data.FixPrice,
            FrequencyCode = data.FrequencyCode,
            FrequencyExecDayTimes = data.FrequencyExecDayTimes,
            FrequencyName = data.FrequencyName,
            FrequencyTimes = data.FrequencyTimes,
            FrequencyUnit = data.FrequencyUnit,
            IsFirstAid = data.IsFirstAid,
            Id = data.Id,
            IndexNo = data.IndexNo,
            InsuranceCode = data.InsuranceCode,
            InsurancePayRate = data.InsurancePayRate,
            InsuranceType = data.InsuranceType,
            IsActive = data.IsActive.HasValue ? data.IsActive.Value : false,
            IsAllergyTest = data.IsAllergyTest,
            IsAnaleptic = data.IsAnaleptic,
            IsCompound = data.IsCompound,
            IsDrunk = data.IsDrunk,
            IsHighRisk = data.IsHighRisk,
            IsInsulin = data.IsInsulin,
            IsLimited = data.IsLimited,
            IsPrecious = data.IsPrecious,
            IsRefrigerated = data.IsRefrigerated,
            IsSkinTest = data.IsSkinTest,
            IsTumour = data.IsTumour,
            LimitedNote = data.LimitedNote,
            MedicalInsuranceCode = data.MedicalInsuranceCode,
            YBInneCode = data.YBInneCode,
            MedicineProperty = data.MedicineProperty,
            Name = data.Name,
            PharmacyCode = data.PharmacyCode,
            PharmacyName = data.PharmacyName,
            PlatformType = data.PlatformType,
            PrescriptionPermission = data.PrescriptionPermission,
            Price = data.Price,
            PyCode = data.PyCode,
            Remark = data.Remark,
            RetPrice = data.RetPrice,
            ScientificName = data.ScientificName,
            SmallPackFactor = data.SmallPackFactor,
            SmallPackPrice = data.SmallPackPrice,
            SmallPackUnit = data.SmallPackUnit,
            Specification = data.Specification,
            ToxicLevel = data.ToxicLevel,
            Unit = data.Unit,
            Unpack = data.Unpack,
            UsageCode = data.UsageCode,
            UsageName = data.UsageName,
            Volume = data.Volume,
            VolumeUnit = data.VolumeUnit,
            WbCode = data.WbCode,
            Weight = data.Weight,
            WeightUnit = data.WeightUnit,
        };
    }

    /// <summary>
    /// 龙岗中心从数据库中获取药品的信息
    /// </summary>
    /// <param name="code"></param>
    /// <param name="specification"></param>
    /// <param name="pharmacyCode"></param>
    /// <param name="factoryCode"></param>
    /// <returns></returns> 
    private async Task<MedicineData> GetMedicineInfoByLGZXAsync(string code, string specification, string pharmacyCode, string factoryCode)
    {
        specification = HttpUtility.UrlDecode(specification);

        _logger.LogInformation("获取药品信息=> specification = " + specification);

        var medicine = await (await HisMedicineRepository.GetQueryableAsync()).FirstOrDefaultAsync(
             x => Convert.ToString(x.MedicineCode) == code && x.Specification == specification
             && x.PharmacyCode == pharmacyCode && Convert.ToString(x.FactoryCode) == factoryCode);
        if (medicine == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        var result = ObjectMapper.Map<HISMedicine, MedicineData>(medicine);
        if (!result.FrequencyCode.IsNullOrWhiteSpace())
        {
            var frequency =
                await (await _medicineFrequencyRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                    x.FrequencyCode == result.FrequencyCode);
            if (frequency == null) Oh.Error("找不到数据");
            result.FrequencyTimes = frequency.Times;
            result.FrequencyUnit = frequency.Unit;
            result.FrequencyExecDayTimes = frequency.ExecDayTimes;
            result.DailyFrequency = frequency.ThirdPartyId; // HIS频次编码
        }
        return result;
    }

    #endregion Get

    #region Details

    /// <summary>
    /// 根据编码获取
    /// </summary>
    /// <param name="code"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="EcisBusinessException"></exception>
    public async Task<MedicineData> GetDetailsAsync(string code, PlatformType type = PlatformType.EmergencyTreatment)
    {
        var medicine = await (await _medicineRepository.GetQueryableAsync()).FirstOrDefaultAsync(f => f.Id == int.Parse(code));
        if (medicine == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        var result = ObjectMapper.Map<Medicine, MedicineData>(medicine);
        if (!result.FrequencyCode.IsNullOrWhiteSpace())
        {
            var frequency =
                await (await _medicineFrequencyRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                    x.FrequencyCode == result.FrequencyCode);
            result.FrequencyTimes = frequency.Times;
            result.FrequencyUnit = frequency.Unit;
            result.FrequencyExecDayTimes = frequency.ExecDayTimes;
            result.DailyFrequency = frequency.ThirdPartyId; // HIS频次编码
        }

        if (type == PlatformType.PreHospital)
        {
            //院前CategoryCode和字典PreHospitalCategory保持一致
            result.CategoryCode = "Medicine";
            result.CategoryName = "药品";
        }

        return result;
    }

    #endregion

    #region Delete

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(int id)
    {
        var medicine = await _medicineRepository.GetAsync(id);
        if (medicine == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        await _medicineRepository.DeleteAsync(id);
        if (medicine.PlatformType == PlatformType.PreHospital)
        {
            await _allItemAppService.DeleteAsync(medicine.Id.ToString(), "Medicine", "PreHospital");
        }
    }

    #endregion Delete


    /// <summary>
    /// 合理用药调用，只是用来模拟
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public Task<bool> CheckRationalUseAsync(string code)
    {
        //每家医院合理用药逻辑不同，此处只做调用操作
        return Task.FromResult(true);
    }

    #region Categories

    /// <summary>
    /// 获取目录列表
    /// </summary>
    /// <returns></returns>
    public async Task<string[]> GetCategoriesAsync()
    {
        return await _medicineRepository.GetCategoriesAsync();
    }

    #endregion Categories

    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<MedicineData>> GetPagedListAsync(GetMedicinePagedInput input)
    {
        var medicines = await _medicineRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Category,
            input.IsEmergency, input.PlatformType, input.PharmacyCode, input.ToxicLevel, input.IsActive);

        var items = ObjectMapper.Map<List<Medicine>, List<MedicineData>>(medicines);
        var frequencyList = await _medicineFrequencyRepository.GetListAsync();
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
        var totalCount = await _medicineRepository.GetCountAsync(input.Filter,
            input.Category,
            input.IsEmergency, input.PlatformType, input.PharmacyCode, input.ToxicLevel, input.IsActive);

        var result = new PagedResultDto<MedicineData>(totalCount, items.AsReadOnly());

        return result;
    }

    #endregion GetPagedList

    #region 修改药品的默认频次，用法

    /// <summary>
    /// 修改药品的默认频次，用法
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> UpdateFrequencyUsageAsync(UpdateFrequencyUsageDto input)
    {
        var medicine = await (await _medicineRepository.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == input.Id);

        if (medicine == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        medicine.FrequencyCode = input.FrequencyCode;
        medicine.FrequencyName = input.FrequencyName;
        medicine.UsageCode = input.UsageCode;
        medicine.UsageName = input.UsageName;

        await _medicineRepository.UpdateAsync(medicine);

        var result = await _medicineRepository.UpdateAsync(medicine);

        // 发布药品同步消息
        var eto = ObjectMapper.Map<Medicine, GrpcMedicineModel>(result);

        await this._capPublisher.PublishAsync("sync.masterdata.medicine", eto);

        return result.Id;
    }

    #endregion
}