using DotNetCore.CAP;
using Google.Protobuf.Collections;
using MasterDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Uow;
using YiJian.Common;
using YiJian.DoctorsAdvices.Contracts;
using YiJian.DoctorsAdvices.Dto;
using YiJian.DoctorsAdvices.Entities;
using YiJian.DoctorsAdvices.Enums;
using YiJian.Documents.Dto;
using YiJian.Dosages;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.Hospitals;
using YiJian.Hospitals.Dto;
using YiJian.Hospitals.Enums;
using YiJian.Preferences.Dto;
using YiJian.Recipe.Preferences.Contracts;
using YiJian.Recipes.DoctorsAdvices.Contracts;
using YiJian.Recipes.DoctorsAdvices.Entities;
using YiJian.Recipes.Preferences.Entities;
using YiJian.Sequences.Contracts;

namespace YiJian.Preferences;

/// <summary>
/// 快速开嘱内容配置（个人偏好设置）
/// </summary>
[Authorize]
public partial class QuickStartAppService : ApplicationService, IQuickStartAppService, ICapSubscribe
{
    private readonly IQuickStartCatalogueRepository _quickStartCatalogueRepository;
    private readonly IQuickStartAdviceRepository _quickStartAdviceRepository;
    private readonly IQuickStartMedicineRepository _quickStartMedicineRepository;
    private readonly IDoctorsAdviceRepository _doctorsAdviceRepository;
    private readonly IPrescribeRepository _prescribeRepository;
    private readonly IMySequenceRepository _mySequenceRepository;
    private readonly IDoctorsAdviceMapRepository _doctorsAdviceMapRepository;
    private readonly IHospitalClientAppService _hospitalClientAppService;
    private readonly IDrugStockQueryRepository _drugStockQueryRepository;
    private readonly IMedicalTechnologyMapRepository _medicalTechnologyMapRepository;
    private readonly ITreatRepository _treatRepository;
    private readonly IImmunizationRecordRepository _iImmunizationRecordRepository;
    private readonly IToxicRepository _toxicRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<QuickStartAppService> _logger;
    private readonly ICapPublisher _capPublisher;
    private readonly IDosageAppService _dosageAppService;
    private readonly RemoteServices _remoteServices;
    private readonly GrpcMasterData.GrpcMasterDataClient _grpcMasterDataClient;
    private readonly PatientAppService _patientAppService;
    private const string _detailId = "DetailId";
    private static readonly SemaphoreSlim _mutex = new SemaphoreSlim(1);

    /// <summary>
    /// 快速开嘱内容配置（个人偏好设置）
    /// </summary> 
    public QuickStartAppService(
        IQuickStartCatalogueRepository quickStartCatalogueRepository,
        IQuickStartAdviceRepository quickStartAdviceRepository,
        IQuickStartMedicineRepository quickStartMedicineRepository,
        IDoctorsAdviceRepository doctorsAdviceRepository,
        IPrescribeRepository prescribeRepository,
        IMySequenceRepository mySequenceRepository,
        IHospitalClientAppService hospitalClientAppService,
        IDrugStockQueryRepository drugStockQueryRepository,
        IMedicalTechnologyMapRepository medicalTechnologyMapRepository,
        ITreatRepository treatRepository,
        IImmunizationRecordRepository iImmunizationRecordRepository,
        IToxicRepository toxicRepository,
        IConfiguration configuration,
        ILogger<QuickStartAppService> logger,
        ICapPublisher capPublisher,
        IDosageAppService dosageAppService,
        IOptionsMonitor<RemoteServices> remoteServices,
        GrpcMasterData.GrpcMasterDataClient grpcMasterDataClient,
        IDoctorsAdviceMapRepository doctorsAdviceMapRepository,
        PatientAppService patientAppService)
    {
        _quickStartCatalogueRepository = quickStartCatalogueRepository;
        _quickStartAdviceRepository = quickStartAdviceRepository;
        _quickStartMedicineRepository = quickStartMedicineRepository;
        _doctorsAdviceRepository = doctorsAdviceRepository;
        _prescribeRepository = prescribeRepository;
        _mySequenceRepository = mySequenceRepository;
        _hospitalClientAppService = hospitalClientAppService;
        _drugStockQueryRepository = drugStockQueryRepository;
        _medicalTechnologyMapRepository = medicalTechnologyMapRepository;
        _treatRepository = treatRepository;
        _iImmunizationRecordRepository = iImmunizationRecordRepository;
        _toxicRepository = toxicRepository;
        _configuration = configuration;
        _logger = logger;
        _capPublisher = capPublisher;
        _dosageAppService = dosageAppService;
        _remoteServices = remoteServices.CurrentValue;
        _grpcMasterDataClient = grpcMasterDataClient;
        _doctorsAdviceMapRepository = doctorsAdviceMapRepository;
        _patientAppService = patientAppService;
    }

    /// <summary>
    /// 获取快速开嘱列表信息
    /// </summary> 
    /// <param name="model"></param>
    /// <returns></returns> 
    public async Task<List<QuickStartDto>> GetAsync(InitCatalogueDto model)
    {
        List<QuickStartDto> ret = new();

        var catalogues = (await _quickStartCatalogueRepository.GetQueryableAsync())
            .Include(i => i.QuickStartAdvices)
            .ThenInclude(i => i.Medicine)
            .Where(w => w.PlatformType == model.PlatformType && w.DoctorCode == model.DoctorCode);

        if (catalogues.Any())
        {
            foreach (var catalogue in catalogues)
            {
                var quickStart = new QuickStartDto { Title = catalogue.Title, Sort = catalogue.Sort };
                if (!catalogue.QuickStartAdvices.Any())
                {
                    ret.Add(quickStart);
                    continue;
                }

                List<QuickStartSampleDto> medicines = new();
                var advices = new List<QuickStartAdvice>();

                if (catalogue.CanModify)
                {
                    //非常用药排序
                    advices = catalogue.QuickStartAdvices.OrderBy(o => o.Sort).Take(100).ToList();
                }
                else
                {
                    //常用药排序
                    advices = catalogue.QuickStartAdvices.OrderByDescending(o => o.UsageCount).ThenBy(t => t.Sort).Take(100).ToList();
                }

                advices.ForEach(x =>
                {
                    if (x.Medicine != null)
                    {
                        var model = ObjectMapper.Map<QuickStartMedicine, QuickStartSampleDto>(x.Medicine);
                        medicines.Add(model);
                    }
                });

                quickStart.Medicines = medicines;
                ret.Add(quickStart);
            }

            IEnumerable<GetMedicineInvInfoRquest> MedicineInfos = ret.SelectMany(x => x.Medicines.Select(x => new GetMedicineInvInfoRquest { Code = x.MedicineCode, FactoryCode = x.FactoryCode, PharmacyCode = x.PharmacyCode, Specification = x.Specification }));

            CombinationRequest combinationRequest = new CombinationRequest();
            combinationRequest.MedicineInfo.AddRange(MedicineInfos);
            // GRPC 获取药品信息  (已改为HIS药品视图内容)
            var combinationResponse = _grpcMasterDataClient.GetCombinationRecipeDetails(combinationRequest);
            if (combinationResponse == null)
            {
                combinationResponse = new CombinationResponse();
            }
            var medicineDetails = combinationResponse.Details.Where(x => x.Category == "Medicine");
            ret.SelectMany(x => x.Medicines).ForEach(x =>
            {
                var medicineDetail = medicineDetails.FirstOrDefault(y => y.MedicineCode == x.MedicineCode && y.FactoryCode == x.FactoryCode && y.PharmacyCode == x.PharmacyCode && y.Specification == x.Specification);
                if (medicineDetail == null)
                {
                    x.IsActive = false;
                }
                else
                {
                    x.IsActive = medicineDetail.IsActive;
                }
            });

            return ret.OrderBy(o => o.Sort).ToList();
        }

        //查找不到记录，初始化，并且返回空记录
        List<QuickStartDto> emptyList = new();
        var initret = await InitCatalogueAsync(model);
        initret.ForEach(i =>
        {
            emptyList.Add(new QuickStartDto { Title = i.Title, Sort = i.Sort, Medicines = new List<QuickStartSampleDto>() });
        });
        return emptyList;
    }

    /// <summary>
    /// 获取当前医生的类型目录数据[后台，可以当作获取目录使用]
    /// </summary>
    /// <returns></returns>
    public async Task<List<CataloguesDto>> GetCataloguesAsync(InitCatalogueDto model)
    {
        var catalogues = await (await _quickStartCatalogueRepository.GetQueryableAsync()).AsNoTracking()
            .Where(w => w.PlatformType == model.PlatformType && w.DoctorCode == model.DoctorCode)
            .Select(s => new CataloguesDto
            {
                Id = s.Id,
                CanModify = s.CanModify,
                Title = s.Title,
                Sort = s.Sort
            })
            .OrderBy(o => o.Sort)
            .ToListAsync();
        if (catalogues.Any()) return catalogues;
        return await InitCatalogueAsync(model);
    }

    /// <summary>
    /// 获取所有后台数据[后台-整合]
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    public async Task<List<AllMedicineDto>> GetAdminMedicinesAsync(InitCatalogueDto model)
    {
        List<AllMedicineDto> data = new();
        var catalogues = await GetCataloguesAsync(model);

        foreach (var item in catalogues)
        {
            var query = await GetQueryAsync(item.Id);
            data.Add(new AllMedicineDto { Id = item.Id, Title = item.Title, CanModify = item.CanModify, Medicines = query });
        }
        return data;
    }

    /// <summary>
    /// 获取指定目录下的药品信息集合
    /// </summary>
    /// <param name="catalogueId">目录id</param>
    /// <returns></returns> 
    public async Task<List<QuickStartQueryDto>> GetQueryAsync(Guid catalogueId)
    {
        var catalogue = await (await _quickStartCatalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == catalogueId);
        if (catalogue == null) Oh.Error("当前目录无效");

        List<QuickStartQueryDto> list = new();

        var query = from a in (await _quickStartAdviceRepository.GetQueryableAsync())
                    join m in (await _quickStartMedicineRepository.GetQueryableAsync())
                    on a.Id equals m.QuickStartAdviceId
                    where a.QuickStartCatalogueId == catalogueId
                    select new QuickStartQueryDto
                    {
                        #region QueryData
                        Id = a.Id,
                        Qty = m.Qty,
                        Sort = a.Sort,
                        UsageCount = a.UsageCount,
                        DosageForm = m.DosageForm,
                        FrequencyCode = m.FrequencyCode,
                        FrequencyName = m.FrequencyName,
                        FrequencyTimes = m.FrequencyTimes,
                        FrequencyUnit = m.FrequencyUnit,
                        DailyFrequency = m.DailyFrequency,
                        MedicalInsuranceCode = m.MedicalInsuranceCode,
                        IsSkinTest = m.IsSkinTest,
                        MedicineId = m.MedicineId,
                        MedicineCode = m.MedicineCode,
                        MedicineName = m.MedicineName,
                        PharmacyCode = m.PharmacyCode,
                        PharmacyName = m.PharmacyName,
                        Price = m.Price,
                        Remark = m.Remark,
                        Specification = m.Specification,
                        Unit = m.Unit,
                        FactoryCode = m.FactoryCode,
                        FactoryName = m.FactoryName,

                        ActualDays = m.ActualDays,
                        ApplyDoctorCode = m.ApplyDoctorCode,
                        ApplyDoctorName = m.ApplyDoctorName,
                        ApplyTime = m.ApplyTime,
                        BaseFlag = m.BaseFlag,
                        BigPackFactor = m.BigPackFactor,
                        BigPackPrice = m.BigPackPrice,
                        BigPackUnit = m.BigPackUnit,
                        CategoryCode = m.CategoryCode,
                        CategoryName = m.CategoryName,
                        ChargeCode = m.ChargeCode,
                        ChargeName = m.ChargeName,
                        ChildrenPrice = m.ChildrenPrice,
                        DefaultDosage = m.DefaultDosage,
                        DefaultDosageQty = m.DefaultDosageQty,
                        DefaultDosageUnit = m.DefaultDosageUnit,
                        DosageQty = m.DosageQty,
                        DosageUnit = m.DosageUnit,
                        FixPrice = m.FixPrice,
                        LongDays = m.LongDays,
                        MedicineProperty = m.MedicineProperty,
                        PayType = m.PayType,
                        PayTypeCode = m.PayTypeCode,
                        QtyPerTimes = m.QtyPerTimes,
                        QuickStartAdviceId = m.QuickStartAdviceId,
                        RecieveQty = m.RecieveQty,
                        RecieveUnit = m.RecieveUnit,
                        Remarks = m.Remarks,
                        RetPrice = m.RetPrice,
                        ScientificName = m.ScientificName,
                        SkinTestResult = m.SkinTestResult,
                        SmallPackFactor = m.SmallPackFactor,
                        SmallPackPrice = m.SmallPackPrice,
                        SmallPackUnit = m.SmallPackUnit,
                        Stock = m.Stock,
                        ToxicProperty = m.ToxicProperty,
                        Unpack = m.Unpack,
                        UsageCode = m.UsageCode,
                        UsageName = m.UsageName,
                        IsFirstAid = m.IsFirstAid,
                        LimitType = m.IsLimited.HasValue ? (m.IsLimited.Value ? 1 : 2) : null,
                        IsCriticalPrescription = m.IsCriticalPrescription,
                        HisUnit = m.HisUnit,
                        HisDosageUnit = m.HisDosageUnit,
                        HisDosageQty = m.HisDosageQty,
                        #endregion
                    };

        //只有常用药物是不可修改的
        if (catalogue.CanModify) return await query.OrderBy(o => o.Sort).ToListAsync();

        var commonDrugs = await query.OrderByDescending(o => o.UsageCount).ToListAsync();

        //回填sort,仅仅是给前端顺序拍个序号，不参与业务
        if (commonDrugs.Any())
        {
            int counter = 1;
            foreach (var drug in commonDrugs)
            {
                drug.Sort = counter++;
            }
        }
        return commonDrugs;
    }

    /// <summary>
    /// 初始化目录(医生第一次进来的时候需要初始化一个目录)
    /// </summary>
    /// <param name="model">快速开嘱目录信息</param> 
    /// <returns></returns>
    public async Task<List<CataloguesDto>> InitCatalogueAsync(InitCatalogueDto model)
    {
        var any = await (await _quickStartCatalogueRepository.GetQueryableAsync()).AnyAsync(w => w.DoctorCode == model.DoctorCode);
        if (any) Oh.Error("已经存在不需要再添加");

        //默认目录（目录名称，是否可以修改） 
        var defaultCatalogues = new List<CataloguesDto>{
             new CataloguesDto(GuidGenerator.Create(),"常用药物",false,1),
             new CataloguesDto(GuidGenerator.Create(),"抗生素",true,2),
             new CataloguesDto(GuidGenerator.Create(),"液体",true,3),
             new CataloguesDto(GuidGenerator.Create(),"心脑血管",true,4),
             new CataloguesDto(GuidGenerator.Create(),"解毒",true,5),
        };

        var catalogues = new List<QuickStartCatalogue>();
        foreach (var item in defaultCatalogues)
        {
            catalogues.Add(new QuickStartCatalogue(
                id: GuidGenerator.Create(),
                title: item.Title,
                doctorCode: model.DoctorCode,
                doctorName: model.DoctorName,
                canModify: item.CanModify,
                sort: item.Sort,
                model.PlatformType));
        }
        await _quickStartCatalogueRepository.InsertManyAsync(catalogues);
        return defaultCatalogues;
    }

    /// <summary>
    /// 更新快速开始的类型目录
    /// </summary>
    /// <param name="model">快速开嘱对象</param>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<Guid> PutCatalogueAsync(PutQuickStartCatalogueDto model)
    {
        var entity = await (await _quickStartCatalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.Id);
        if (entity == null) Oh.Error("查无此目录");
        if (!entity.CanModify) Oh.Error($"{entity.Title} 不可修改");
        entity.ModifyTitle(model.Title);
        _ = await _quickStartCatalogueRepository.UpdateAsync(entity);
        return model.Id;
    }

    /// <summary>
    /// 添加快速医嘱信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    [UnitOfWork]
    //[AllowAnonymous]
    public async Task<bool> AddAdviceAsync(AddQuickStartAdviceDto model)
    {
        if (model.QuickStartAdvices.Count() > 100) Oh.Error("一个分类下最多只支持100条配置项！");

        try
        {
            await _mutex.WaitAsync();

            if (!model.QuickStartAdvices.Any()) Oh.Error("添加的内容不能为空");
            var catalogue = await (await _quickStartCatalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.QuickStartCatalogueId);
            if (catalogue == null) Oh.Error("快速开嘱的目录不能随便填写，查无此记录");

            var medicineId = model.QuickStartAdvices.Select(s => s.MedicineId).ToList();
            var request = new GetListMedicinesRequest();
            request.MedicineIds.AddRange(medicineId);
            // GRPC 获取药品信息  (已改为HIS药品视图内容)
            var grpcData = await _grpcMasterDataClient.GetMedicineListByCodesAsync(request);
            if (grpcData == null || !grpcData.Medicines.Any()) Oh.Error("没有查询到任何药品信息");
            var grpcMedicines = grpcData.Medicines;
            await ModifyQuickStartAdviceAsync(model, catalogue, medicineId, grpcMedicines);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"添加快速医嘱信息异常：{ex.Message}");
            return false;
        }
        finally
        {
            _mutex.Release();
        }
    }

    /// <summary>
    /// 新增快速开嘱数据
    /// </summary>
    /// <param name="model"></param>
    /// <param name="catalogue"></param>
    /// <param name="medicineId"></param>
    /// <param name="grpcMedicines"></param>
    /// <returns></returns>
    private async Task ModifyQuickStartAdviceAsync(AddQuickStartAdviceDto model, QuickStartCatalogue catalogue, List<int> medicineId, RepeatedField<GrpcMedicineModel> grpcMedicines)
    {
        List<QuickStartAdvice> advices = new();
        List<QuickStartAdvice> updateAdvices = new();
        List<QuickStartMedicine> medicines = new();
        List<QuickStartMedicine> updateMedicines = new();

        var adviceids = model.QuickStartAdvices.Select(s => s.QuickStartAdviceId).ToList(); //前端传过来的所有药品信息

        //在库的快速开嘱的主表信息
        var inStockAdvice = await (await _quickStartAdviceRepository.GetQueryableAsync()).AsNoTracking().Where(w => adviceids.Contains(w.Id)).ToListAsync();
        //在库的快速药品信息
        var inStockMedicines = await (await _quickStartMedicineRepository.GetQueryableAsync()).AsNoTracking().Where(w => medicineId.Contains(w.MedicineId)).ToListAsync();

        var request = new GetAllFrequenciesRequest();
        var allFrequenciesResponse = await _grpcMasterDataClient.GetAllFrequenciesAsync(request);
        var allFrequencies = allFrequenciesResponse.Frequencies;
        //清除没有提交的药品信息（删除）


        //添加或更新药品信息
        foreach (QuickStartSampleAdviceDto item in model.QuickStartAdvices)
        {
            var medicine = grpcMedicines.FirstOrDefault(w => w.Id == item.MedicineId);
            if (medicine == null) continue;

            //更新
            if (item.QuickStartAdviceId.HasValue && item.Id.HasValue)
            {
                var advice = inStockAdvice.FirstOrDefault(w => w.Id == item.QuickStartAdviceId);
                advice.Sort = item.Sort > 0 ? item.Sort : advice.Sort; //更新排序 
                updateAdvices.Add(advice);
                var frequency = allFrequencies.FirstOrDefault(w => w.FrequencyCode == item.FrequencyCode);
                var updateEntity = inStockMedicines.FirstOrDefault(w => /* w.MedicineCode == item.MedicineCode &&*/ w.QuickStartAdviceId == item.Id);

                //找不到就是药品是新增的
                if (updateEntity == null) updateEntity = await (await _quickStartMedicineRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.QuickStartAdviceId == item.QuickStartAdviceId);

                //更新旧记录
                updateEntity.Update(
                   medicineId: item.MedicineId,
                   medicineCode: item.MedicineCode,
                   medicineName: item.MedicineName,
                   dosageQty: item.DosageQty,
                   dosageUnit: item.DosageUnit,
                   dosageForm: item.DosageForm,
                   qty: item.Qty,
                   unit: item.Unit,
                   bigPackFactor: item.BigPackFactor,
                   bigPackPrice: item.BigPackPrice,
                   bigPackUnit: item.BigPackUnit,
                   smallPackPrice: item.SmallPackPrice,
                   smallPackUnit: item.SmallPackUnit,
                   smallPackFactor: item.SmallPackFactor,
                   hisUnit: item.HisUnit,
                   usageCode: item.UsageCode,
                   usageName: item.UsageName,
                   frequencyCode: item.FrequencyCode,
                   frequencyName: item.FrequencyName,
                   dailyFrequency: frequency == null ? "01" : frequency.DailyFrequency,
                   medicalInsuranceCode: item.MedicalInsuranceCode,
                   isSkinTest: item.IsSkinTest,
                   remark: item.Remark ?? "",
                   quickStartAdviceId: item.QuickStartAdviceId.Value);

                updateEntity.UpdateAdvicePart(
                    recieveQty: item.RecieveQty,
                    recieveUnit: item.RecieveUnit,
                    toxicProperty: item.ToxicProperty,
                    longDays: item.LongDays,
                    actualDays: item.ActualDays,
                    defaultDosageQty: item.DefaultDosageQty,
                    qtyPerTimes: item.QtyPerTimes,
                    defaultDosageUnit: item.DefaultDosageUnit,
                    skinTestResult: item.SkinTestResult,
                    materialPrice: item.MaterialPrice,
                    isBindingTreat: item.IsBindingTreat,
                    isAmendedMark: item.IsAmendedMark,
                    isAdaptationDisease: item.IsAdaptationDisease,
                    speed: item.Speed,
                    restrictedDrugs: item.RestrictedDrugs,
                    isCriticalPrescription: item.IsCriticalPrescription);

                updateEntity.UpdateMedicineInfo(medicineId: medicine.MedicineId, specification: medicine.Specification, pharmacyCode: medicine.PharmacyCode, pharmacyName: medicine.PharmacyName, factoryCode: medicine.FactoryCode, factoryName: medicine.FactoryName);
                updateEntity.UpdateFrequency(frequency.FrequencyCode, frequency.FrequencyName, frequency.Times, frequency.Unit, frequency.ExecDayTimes, frequency.DailyFrequency);
                updateEntity.SetHisDosageQty();
                //updateEntity.ComputeQty();

                updateMedicines.Add(updateEntity);
            }
            //新增
            else
            {
                var adviceId = GuidGenerator.Create();
                var advice = new QuickStartAdvice(adviceId, item.Sort, model.QuickStartCatalogueId);
                advices.Add(advice);
                var frequency = allFrequencies.FirstOrDefault(w => w.FrequencyCode == item.FrequencyCode);

                var medicineEntity = new QuickStartMedicine(GuidGenerator.Create());
                medicineEntity = ObjectMapper.Map<GrpcMedicineModel, QuickStartMedicine>(medicine);

                if (item.FrequencyCode.IsNullOrEmpty()) Oh.Error($"【{medicine.MedicineId},{medicine.Name}】频次信息必填");

                //填充快速开嘱药品信息
                medicineEntity.Update(
                    item.MedicineId,
                    item.MedicineCode,
                    item.MedicineName,
                    dosageForm: item.DosageForm,
                    dosageQty: item.DosageQty,
                    dosageUnit: item.DosageUnit,
                    qty: item.Qty,
                    unit: item.Unit,
                    bigPackPrice: item.BigPackPrice,
                    bigPackFactor: item.BigPackFactor,
                    bigPackUnit: item.BigPackUnit,
                    smallPackPrice: item.SmallPackPrice,
                    smallPackUnit: item.SmallPackUnit,
                    smallPackFactor: item.SmallPackFactor,
                    hisUnit: item.HisUnit,
                    usageCode: item.UsageCode,
                    usageName: item.UsageName,
                    frequencyCode: item.FrequencyCode,
                    frequencyName: item.FrequencyName,
                    dailyFrequency: frequency == null ? "01" : frequency.DailyFrequency,
                    medicalInsuranceCode: item.MedicalInsuranceCode,
                    isSkinTest: item.IsSkinTest,
                    remark: item.Remark,
                    adviceId);

                medicineEntity.UpdateAdvicePart(
                    recieveQty: item.RecieveQty,
                    recieveUnit: item.RecieveUnit,
                    toxicProperty: item.ToxicProperty,
                    longDays: item.LongDays,
                    actualDays: item.ActualDays,
                    defaultDosageQty: item.DefaultDosageQty,
                    qtyPerTimes: item.QtyPerTimes,
                    defaultDosageUnit: item.DefaultDosageUnit,
                    skinTestResult: item.SkinTestResult,
                    materialPrice: item.MaterialPrice,
                    isBindingTreat: item.IsBindingTreat,
                    isAmendedMark: item.IsAmendedMark,
                    isAdaptationDisease: item.IsAdaptationDisease,
                    speed: item.Speed,
                    restrictedDrugs: item.RestrictedDrugs,
                    isCriticalPrescription: item.IsCriticalPrescription);
                //更新频次
                medicineEntity.UpdateFrequency(
                    frequencyCode: frequency.FrequencyCode,
                    frequencyName: frequency.FrequencyName,
                    frequencyTimes: frequency.Times,
                    frequencyUnit: frequency.Unit,
                    frequencyExecDayTimes: frequency.ExecDayTimes,
                    dailyFrequency: frequency.DailyFrequency);

                medicineEntity.SetHisDosageQty();
                //medicineEntity.ComputeQty();

                medicines.Add(medicineEntity);
            }
        }

        if (advices.Any()) await _quickStartAdviceRepository.InsertManyAsync(advices);
        if (medicines.Any()) await _quickStartMedicineRepository.InsertManyAsync(medicines);
        if (updateAdvices.Any()) await _quickStartAdviceRepository.UpdateManyAsync(updateAdvices);
        if (updateMedicines.Any()) await _quickStartMedicineRepository.UpdateManyAsync(updateMedicines);
    }

    /// <summary>
    /// 用户换了药品，这里只能重新替换
    /// </summary>
    /// <param name="item"></param>
    /// <param name="advice"></param>
    /// <param name="updateEntity"></param>
    /// <param name="drug"></param>
    /// <returns></returns>
    private async Task<QuickStartMedicine> ChangeNewDrugAsync(QuickStartSampleAdviceDto item, QuickStartAdvice advice, QuickStartMedicine updateEntity, GrpcMedicineModel drug)
    {
        //找不到就是药品是新增的
        if (updateEntity == null)
        {
            updateEntity = await (await _quickStartMedicineRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.QuickStartAdviceId == item.QuickStartAdviceId);
            //医保目录:0=自费,1=甲类,2=乙类,3=丙
            var insuranceType = drug.InsuranceType == "甲" ? 1 : (drug.InsuranceType == "乙" ? 2 : (drug.InsuranceType == "自费" ? 0 : 3));

            updateEntity.ChangeDrug(categoryCode: drug.CategoryCode, categoryName: drug.CategoryName, chargeCode: drug.ChargeCode, chargeName: drug.ChargeName,
                scientificName: drug.ScientificName, alias: drug.Alias, aliasPyCode: drug.AliasPyCode, aliasWbCode: drug.AliasWbCode, pyCode: drug.PyCode, wbCode: drug.WbCode,
                medicineProperty: drug.MedicineProperty, defaultDosage: drug.DefaultDosage, dosageQty: (decimal)drug.DosageQty, price: (decimal)drug.Price, bigPackPrice: (decimal)drug.BigPackPrice, bigPackFactor: drug.BigPackFactor,
                bigPackUnit: drug.BigPackUnit, smallPackPrice: (decimal)drug.SmallPackPrice, smallPackUnit: drug.SmallPackUnit, smallPackFactor: drug.SmallPackFactor, insuranceCode: drug.MedicalInsuranceCode,
                insuranceType: (EInsuranceCatalog)insuranceType, factoryName: drug.FactoryName, factoryCode: drug.FactoryCode, batchNo: drug.BatchNo, volumeUnit: drug.VolumeUnit, limitedNote: drug.LimitedNote,
                specification: drug.Specification, dosageForm: drug.DosageForm, pharmacyCode: drug.PharmacyCode, pharmacyName: drug.PharmacyName, antibioticPermission: drug.AntibioticPermission,
                prescriptionPermission: drug.PrescriptionPermission, baseFlag: drug.BaseFlag, unpack: (EMedicineUnPack)drug.Unpack, childrenPrice: (decimal)drug.ChildrenPrice, fixPrice: (decimal)drug.FixPrice,
                retPrice: (decimal)drug.RetPrice, insurancePayRate: drug.InsurancePayRate, weight: drug.Weight, weightUnit: drug.WeightUnit, volume: drug.Volume, isCompound: drug.IsCompound,
                isDrunk: drug.IsDrunk, toxicLevel: drug.ToxicLevel, isHighRisk: drug.IsHighRisk, isRefrigerated: drug.IsRefrigerated, isTumour: drug.IsTumour, antibioticLevel: drug.AntibioticLevel ? 1 : 0,
                isPrecious: drug.IsPrecious, isInsulin: drug.IsInsulin, isAnaleptic: drug.IsAnaleptic, isAllergyTest: drug.IsAllergyTest, isLimited: drug.IsLimited, isFirstAid: drug.IsFirstAid,
                hisUnit: drug.Unit, hisDosageUnit: drug.DosageUnit, hisDosageQty: (decimal)drug.DosageQty);

            advice.ResetUsageCount();
        }

        return updateEntity;
    }

    /// <summary>
    /// 删除快速开嘱信息(支持多条)
    /// </summary>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<bool> DeleteAdviceAsync(List<Guid> ids)
    {
        if (!ids.Any()) Oh.Error("医嘱Ids必传");
        var advices = await (await _quickStartAdviceRepository.GetQueryableAsync()).Where(w => ids.Contains(w.Id)).ToListAsync();
        if (!advices.Any()) Oh.Error("查询的快速医嘱无效，请认真操作");
        var adviceIds = advices.Select(s => s.Id).ToList();
        var medicines = (await _quickStartMedicineRepository.GetQueryableAsync()).Where(w => adviceIds.Contains(w.QuickStartAdviceId)).ToList();
        await _quickStartMedicineRepository.DeleteManyAsync(medicines);
        await _quickStartAdviceRepository.DeleteManyAsync(ids);
        return true;
    }

    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    [UnitOfWork]
    public async Task<bool> SortAsync(List<SortDto> models)
    {
        var ids = models.Select(s => s.QuickStartAdviceDtoId);
        if (!ids.Any()) Oh.Error("没有需要更新的排序");
        var advices = await (await _quickStartAdviceRepository.GetQueryableAsync()).Where(w => ids.Contains(w.Id)).ToListAsync();
        if (!advices.Any()) Oh.Error("请提交正确的排序数据");
        advices.ForEach(e =>
        {
            var model = models.FirstOrDefault(w => e.Id == w.QuickStartAdviceDtoId);
            e.Sort = model == null ? 1 : model.Sort;
        });
        await _quickStartAdviceRepository.UpdateManyAsync(advices);
        return true;
    }

    /// <summary>
    /// 提交快速开嘱
    /// </summary> 
    /// <param name="model"></param>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<Guid> SubmitQuickStartAdviceAsync(SubmitQuickStartAdvice model)
    {

        var quickMedicine = await (await _quickStartMedicineRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.Id);
        if (quickMedicine == null) Oh.Error("你的快速开嘱记录被删除了,或快速医嘱记录未找到。");

        var task = _patientAppService.GetPatientInfoAsync(model.PIID);
        var medicineIds = new GetListMedicinesRequest();
        medicineIds.MedicineIds.Add(quickMedicine.MedicineId);
        var response = await _grpcMasterDataClient.GetMedicineListByCodesAsync(medicineIds);
        var medicine = response.Medicines.FirstOrDefault();
        if (medicine == null)
        {
            var request = new GetMedicinesRequest()
            {
                MedicineCode = int.Parse(quickMedicine.MedicineCode),
                FactoryCode = int.Parse(quickMedicine.FactoryCode),
                Specification = quickMedicine.Specification
            };
            response = await _grpcMasterDataClient.GetMedicinesAsync(request);
            if (!response.Medicines.Any()) Oh.Error($"找不到你要的药品信息,MedicineCode={request.MedicineCode}, FactoryCode={request.FactoryCode}, Specification={request.Specification}");
            medicine = response.Medicines.OrderByDescending(o => o.Qty).FirstOrDefault(); //获取库存最多的第一条记录  
            var toxic = await (await _toxicRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.MedicineId == medicine.MedicineId);
            if (toxic == null)
            {
                var toxicEntity = new Toxic(
                   id: GuidGenerator.Create(),
                   medicineId: medicine.MedicineId,
                   isSkinTest: medicine.IsSkinTest,
                   isCompound: medicine.IsCompound,
                   isDrunk: medicine.IsDrunk,
                   toxicLevel: medicine.ToxicLevel,
                   isHighRisk: medicine.IsHighRisk,
                   isRefrigerated: medicine.IsRefrigerated,
                   isTumour: medicine.IsTumour,
                   antibioticLevel: medicine.AntibioticLevel ? 1 : 0,
                   isPrecious: medicine.IsPrecious,
                   isInsulin: medicine.IsInsulin,
                   isAnaleptic: medicine.IsAnaleptic,
                   isAllergyTest: medicine.IsAllergyTest,
                   isLimited: medicine.IsLimited,
                   limitedNote: medicine.LimitedNote);
                _ = await _toxicRepository.InsertAsync(toxicEntity, true);
            }
        }

        if (medicine.Qty < (double)quickMedicine.Qty) Oh.Error($"药品【{medicine.Name}】库存不足！");

        var id = GuidGenerator.Create();
        if (model.PlatformType == EPlatformType.EmergencyTreatment)
        {
            var drugStockQueryRequest = new DrugStockQueryRequest
            {
                QueryType = 2,
                QueryCode = medicine.Code,
                Storage = int.Parse(quickMedicine.PharmacyCode)
            };

            var drugStocks = await _hospitalClientAppService.QueryHisDrugStockAsync(drugStockQueryRequest);
            if (drugStocks == null) Oh.Error($"药品【{medicine.Name}】药房目前没有库存");
            if (drugStocks != null && drugStocks.Any())
            {
                var drugStock = drugStocks.FirstOrDefault();
                var entity = ObjectMapper.Map<DrugStockQueryResponse, DrugStockQuery>(drugStock);
                entity.SetDoctorsAdviceId(id);
                await _drugStockQueryRepository.InsertAsync(entity);
            }
            else
            {
                Oh.Error($"药品【{medicine.Name}】药房目前没有库存");
            }
        }

        var recipeNo = (await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "RecipeNo")).ToString();
        var detailId = await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), _detailId);

        //构建医嘱信息
        #region var advice = new DoctorsAdvice();

        var advice = new DoctorsAdvice(
                id: id,
                detailId: detailId,
                platformType: model.PlatformType,
                piid: model.PIID,
                patientId: model.PatientId,
                patientName: model.PatientName,
                code: medicine.Code,
                name: medicine.Name,
                categoryCode: "Medicine",//medicine.CategoryCode, //快速开嘱，开的是药品
                categoryName: "药物",//medicine.CategoryName, //快速开嘱，开的是药品
                isBackTracking: quickMedicine.IsBackTracking,
                prescriptionNo: "",
                recipeNo: recipeNo,
                recipeGroupNo: 1,
                applyTime: DateTime.Now,
                applyDoctorCode: model.ApplyDoctorCode,
                applyDoctorName: model.ApplyDoctorName,
                applyDeptCode: model.ApplyDeptCode,
                applyDeptName: model.ApplyDeptName,
                traineeCode: "",
                traineeName: "",
                payTypeCode: quickMedicine.PayTypeCode ?? "",
                payType: quickMedicine.PayType,
                price: (decimal)medicine.Price,
                unit: medicine.Unit,
                amount: 0, //TODO 试算出来的
                insuranceCode: medicine.InsuranceCode.ToString(),
                insuranceType: medicine.InsuranceType.IsNullOrEmpty() ? EInsuranceCatalog.Self : medicine.InsuranceType.Parse(),
                isChronicDisease: model.IsChronicDisease,
                isRecipePrinted: false, //第一次开嘱，默认未打印
                hisOrderNo: "", //HIS单号需要对接才有
                diagnosis: model.Diagnosis,
                execDeptCode: model.ApplyDeptCode, //medicine.ExecDeptCode,
                execDeptName: model.ApplyDeptName, //medicine.ExecDeptName,
                positionCode: "", //药品暂且没有位置信息
                positionName: "",
                remarks: quickMedicine.Remarks,
                chargeCode: medicine.ChargeCode,
                chargeName: medicine.ChargeName,
                prescribeTypeCode: model.PrescribeTypeCode,
                prescribeTypeName: model.PrescribeTypeName,
                startTime: DateTime.Now,
                endTime: null,
                recieveQty: quickMedicine.RecieveQty,
                recieveUnit: quickMedicine.RecieveUnit,
                pyCode: medicine.PyCode,
                wbCode: medicine.WbCode,
                itemType: EDoctorsAdviceItemType.Prescribe,
                sourceType: 3);
        advice.MeducalInsuranceCode = medicine.MedicalInsuranceCode;
        advice.YBInneCode = medicine.YBInneCode;
        advice.MeducalInsuranceName = medicine.MeducalInsuranceName;
        #endregion

        //构建药品信息
        #region var prescribe = new Prescribe();
        var prescribe = new Prescribe(
                id: GuidGenerator.Create(),
                medicineId: medicine.MedicineId,
                isOutDrug: false,
                medicineProperty: quickMedicine.MedicineProperty,
                toxicProperty: medicine.ToxicProperty,
                usageCode: quickMedicine.UsageCode,
                usageName: quickMedicine.UsageName,
                speed: quickMedicine.Speed, //无数据
                longDays: quickMedicine.LongDays,
                actualDays: quickMedicine.ActualDays,
                dosageForm: quickMedicine.DosageForm,
                dosageQty: quickMedicine.DosageQty,
                defaultDosageQty: quickMedicine.DefaultDosageQty,
                qtyPerTimes: quickMedicine.QtyPerTimes,
                dosageUnit: quickMedicine.DosageUnit,
                defaultDosageUnit: quickMedicine.DefaultDosageUnit,
                unpack: (EMedicineUnPack)quickMedicine.Unpack,
                bigPackPrice: (decimal)quickMedicine.BigPackPrice,
                bigPackFactor: quickMedicine.BigPackFactor,
                bigPackUnit: quickMedicine.BigPackUnit,
                smallPackPrice: (decimal)quickMedicine.SmallPackPrice,
                smallPackUnit: quickMedicine.SmallPackUnit,
                smallPackFactor: quickMedicine.SmallPackFactor,
                frequencyCode: quickMedicine.FrequencyCode,
                medicalInsuranceCode: quickMedicine.MedicalInsuranceCode,
                frequencyName: quickMedicine.FrequencyName,
                frequencyTimes: quickMedicine.FrequencyTimes,
                frequencyUnit: quickMedicine.FrequencyUnit,
                frequencyExecDayTimes: quickMedicine.FrequencyExecDayTimes,
                dailyFrequency: quickMedicine.DailyFrequency,
                pharmacyCode: quickMedicine.PharmacyCode,
                pharmacyName: quickMedicine.PharmacyName,
                factoryName: quickMedicine.FactoryName,
                factoryCode: quickMedicine.FactoryCode,
                batchNo: quickMedicine.BatchNo,
                expirDate: quickMedicine.ExpireDate,
                isSkinTest: quickMedicine.IsSkinTest,
                skinTestResult: null,
                skinTestSignChoseResult: model.SkinTestSignChoseResult,
                materialPrice: quickMedicine.MaterialPrice,
                isBindingTreat: quickMedicine.IsBindingTreat,
                isAmendedMark: quickMedicine.IsAmendedMark,
                isAdaptationDisease: quickMedicine.IsAdaptationDisease,
                isFirstAid: quickMedicine.IsFirstAid,
                antibioticPermission: quickMedicine.AntibioticPermission,
                prescriptionPermission: medicine.PrescriptionPermission,
                specification: medicine.Specification,
                childrenPrice: quickMedicine.ChildrenPrice,
                fixPrice: (decimal)quickMedicine.FixPrice,
                retPrice: (decimal)quickMedicine.RetPrice,
                restrictedDrugs: model.RestrictedDrugs,
                limitType: medicine.IsLimited ? 1 : 2, //限制用药标记 “1.限制用药 2.非限制用药” 出现1.限制用药需要弹出 限制费用提示消息
                doctorsAdviceId: advice.Id,
                isCriticalPrescription: quickMedicine.IsCriticalPrescription,
                hisUnit: medicine.Unit,
                hisDosageUnit: medicine.DosageUnit,
                hisDosageQty: (decimal)medicine.DosageQty);

        prescribe.SetHisDosageQty();

        //自定义一次剂量处理 TODO
        var dosageList = await _dosageAppService.GetAllCustomDosagesAsync();
        var dosage = dosageList.FirstOrDefault(w => w.Code == medicine.Code);
        if (dosage != null)
        {
            var mydosageQty = dosage.GetHisDosageQty(quickMedicine.DosageQty, quickMedicine.DosageUnit);
            prescribe.CommitHisDosageQty = mydosageQty;
        }

        #endregion

        //advice.ComputeQty(prescribe);
        advice.Amount = advice.Price * advice.RecieveQty;

        //附加项
        await TreatItemsAsync(model, advice, prescribe);

        //设置主要药品医嘱的状态
        advice.UpdateStatus(ERecipeStatus.Saved);

        var immunizationRecord = model.ImmunizationRecord;

        await CreateImmunizationRecordAsync(advice, prescribe, immunizationRecord); //疫苗接种记录
        AdmissionRecordDto admissionRecordDto = await task;
        if (admissionRecordDto != null)
        {
            advice.AreaCode = admissionRecordDto.AreaCode;
        }

        _ = await _doctorsAdviceRepository.InsertAsync(advice);
        _ = await _prescribeRepository.InsertAsync(prescribe);
        _ = await _doctorsAdviceMapRepository.InsertAsync(new DoctorsAdviceMap(advice.Id));

        await _capPublisher.PublishAsync("recipe.quickStart.usageCount.increase", quickMedicine.Id);
        return advice.Id;

    }

    /// <summary>
    /// 创建一个疫苗接种记录
    /// </summary>
    /// <param name="advice"></param>
    /// <param name="prescribe"></param>
    /// <param name="immunizationRecord"></param>
    /// <returns></returns>
    private async Task CreateImmunizationRecordAsync(DoctorsAdvice advice, Prescribe prescribe, ImmunizationRecordDto immunizationRecord)
    {
        if (immunizationRecord == null) return;
        var request = new MedicineIdsRequest();
        request.MedicineIds.Add(advice.Code);
        var response = await _grpcMasterDataClient.GetToxicListByMedicineIdsAsync(request);
        if (!response.ToxicList.Any())
        {
            _logger.LogInformation("GRPC查不到改药品的信息");
            return;
        }
        if (response.ToxicList.FirstOrDefault() == null || response.ToxicList.FirstOrDefault().ToxicLevel != 7)
        {
            _logger.LogInformation("需要查询的药品的药理toxicLevel!=7");
            return;
        }
        var immunizationRecordEntity = new ImmunizationRecord(
           id: GuidGenerator.Create(),
           acupunctureManipulation: immunizationRecord.AcupunctureManipulation,
           times: immunizationRecord.Times,
           DateTime.Now,
           confirmed: false,
           doctorAdviceId: advice.Id,
           medicineId: prescribe.MedicineId,
           patientId: advice.PatientId);
        await _iImmunizationRecordRepository.AddRecordAsync(immunizationRecordEntity);
    }

    #region 附加项

    /// <summary>
    /// 添加附加项
    /// </summary>
    /// <param name="model"></param>
    /// <param name="advice"></param>
    /// <param name="prescribe"></param>
    /// <returns></returns>
    private async Task TreatItemsAsync(SubmitQuickStartAdvice model, DoctorsAdvice advice, Prescribe prescribe)
    {
        if (advice.PlatformType == EPlatformType.EmergencyTreatment)
        {
            //添加附加项目 
            await AppendItemAsync(advice, prescribe, model);
            if (model.SkinTestSignChoseResult is ESkinTestSignChoseResult.Yes)
            {
                //添加皮试附加项目
                await AppendSkinItemAsync(advice, prescribe, model);
            }

            if (prescribe.UsageCode == _configuration["UsageIntravenousDripCode"] &&
                (prescribe.FrequencyCode.ToUpper() is "BID" or "TID"))
            {
                await AppendFrequencyItemAsync(advice, prescribe, model);

            }
        }
    }

    /// <summary>
    /// 添加附加处置项
    /// </summary> 
    private async Task AppendItemAsync(DoctorsAdvice advice, Prescribe prescribe, SubmitQuickStartAdvice model)
    {
        //查询药品用法是否有关联处置，有则默认开一条处置单
        var request = new GetTreatByUsageCodeRequest() { UsageCode = prescribe.UsageCode };
        var treat = await _grpcMasterDataClient.GetTreatByUsageAsync(request);

        if (!treat.TreatCode.IsNullOrEmpty())
        {
            var qty = (prescribe.FrequencyTimes ?? 1) * prescribe.LongDays;
            if (prescribe.UsageCode == _configuration["UsageIntravenousDripCode"] &&
                (prescribe.FrequencyCode.ToUpper() is "BID" or "TID"))
            {
                qty = prescribe.LongDays;
            }
            var newEntity = await AddTreatItemAsync(advice, prescribe, model, treat, EAdditionalItemType.UsageAdditional, qty);
            var newAdvice = newEntity.Item1;
            newAdvice.Additional = 1;
            var newTreat = newEntity.Item2;

            await InsertTreatAsync(newAdvice, newTreat);  //提交
        }
    }

    /// <summary>
    /// 添加皮试附加项
    /// </summary> 
    private async Task AppendSkinItemAsync(DoctorsAdvice advice, Prescribe prescribe, SubmitQuickStartAdvice model)
    {
        //查询药品用法是否有关联处置，有则默认开一条处置单
        var request = new GetTreatProjectByCodeRequest { Code = _configuration["SkinTreatCode"] };
        var treatResponse = await _grpcMasterDataClient.GetTreatProjectByCodeAsync(request);
        var treat = treatResponse.TreatProject;

        if (treat != null)
        {
            var newEntity = await AddTreatItemAsync(advice, prescribe, model, treat, EAdditionalItemType.SkinAdditional, 1);
            var newAdvice = newEntity.Item1;
            newAdvice.Additional = 1;
            var newTreat = newEntity.Item2;
            newTreat.AdditionalItemsType = EAdditionalItemType.SkinAdditional;
            await InsertTreatAsync(newAdvice, newTreat);  //提交 
        }
    }
    /// <summary>
    /// 添加频次附加项
    /// </summary> 
    private async Task AppendFrequencyItemAsync(DoctorsAdvice advice, Prescribe prescribe, SubmitQuickStartAdvice model)
    {
        //查询药品用法是否有关联处置，有则默认开一条处置单
        var request = new GetTreatProjectByCodeRequest { Code = _configuration["ContinuousIntravenousInfusionCode"] };
        var treatResponse = await _grpcMasterDataClient.GetTreatProjectByCodeAsync(request);
        var treat = treatResponse.TreatProject;

        if (treat != null)
        {
            var qty = (prescribe.FrequencyCode.ToUpper() is "BID" ? 1 : 2) * prescribe.LongDays;
            var newEntity = await AddTreatItemAsync(advice, prescribe, model, treat, EAdditionalItemType.FrequencyAdditional, qty);
            var newAdvice = newEntity.Item1;
            newAdvice.Additional = 1;
            var newTreat = newEntity.Item2;
            newTreat.AdditionalItemsType = EAdditionalItemType.FrequencyAdditional;
            await InsertTreatAsync(newAdvice, newTreat);  //提交 
        }
    }

    /// <summary>
    /// 添加附加处置项
    /// </summary>
    /// <param name="advice"></param>
    /// <param name="prescribe"></param>
    /// <param name="model"></param>
    /// <param name="treat"></param> 
    /// <param name="type"></param>
    /// <param name="recieveQty"></param>
    /// <returns></returns>
    private async Task<(DoctorsAdvice, Treat)> AddTreatItemAsync(DoctorsAdvice advice, Prescribe prescribe, SubmitQuickStartAdvice model, GrpcTreatProjectModel treat, EAdditionalItemType type, int recieveQty)
    {
        var recipeNo = (await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "RecipeNo")).ToString();
        var detailId = await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), _detailId);
        // var recieveQty = (type == EAdditionalItemType.SkinAdditional) ? 1:  (prescribe.FrequencyTimes ?? 1) * prescribe.LongDays; 
        var isAdditionalPrice = false;
        var otherPrice = decimal.Parse(treat.OtherPrice.ToString("f3"));
        decimal amount = GetAmount((decimal)treat.Price, recieveQty, model.PatientInfo.IsChildren(), treat.Additional, out isAdditionalPrice, otherPrice);

        var treatAdvice = new DoctorsAdvice(
            detailId: detailId,
            id: GuidGenerator.Create(),
            platformType: advice.PlatformType,
            piid: advice.PIID,
            patientId: model.PatientId,
            patientName: model.PatientName,
            code: treat.TreatCode,
            name: treat.TreatName,
            categoryCode: treat.DictionaryCode,
            categoryName: treat.DictionaryName,
            isBackTracking: advice.IsBackTracking,
            prescriptionNo: advice.PrescriptionNo,
            recipeNo: recipeNo,
            recipeGroupNo: 1,
            applyTime: DateTime.Now,
            applyDoctorCode: model.ApplyDoctorCode,
            applyDoctorName: model.ApplyDoctorName,
            applyDeptCode: model.ApplyDeptCode,
            applyDeptName: model.ApplyDeptName,
            traineeCode: advice.TraineeCode,
            traineeName: advice.TraineeName,
            payTypeCode: advice.PayTypeCode,
            payType: advice.PayType,
            price: decimal.Parse(treat.Price.ToString("f3")),
            unit: treat.Unit,
            amount: amount,
            insuranceCode: advice.InsuranceCode,
            insuranceType: advice.InsuranceType,
            isChronicDisease: advice.IsChronicDisease,
            isRecipePrinted: false,
            hisOrderNo: advice.HisOrderNo,
            diagnosis: model.Diagnosis,
            execDeptCode: advice.ExecDeptCode,
            execDeptName: advice.ExecDeptName,
            positionCode: advice.PositionCode,
            positionName: advice.PositionName,
            remarks: advice.Remarks,
            chargeCode: treat.ChargeCode,
            chargeName: treat.ChargeName,
            prescribeTypeCode: advice.PrescribeTypeCode,
            prescribeTypeName: advice.PrescribeTypeName,
            startTime: advice.StartTime,
            endTime: advice.EndTime,
            recieveQty: recieveQty,//advice.RecieveQty,
            recieveUnit: treat.Unit,
            pyCode: advice.PyCode,
            wbCode: advice.WbCode,
            itemType: EDoctorsAdviceItemType.Treat,
            isAdditionalPrice: isAdditionalPrice,
            sourceType: 3);
        treatAdvice.MeducalInsuranceCode = treat.MeducalInsuranceCode;
        treatAdvice.YBInneCode = treat.YBInneCode;

        //1 添加附加处置项
        var treatEntity = new Treat(
            id: GuidGenerator.Create(),
            specification: treat.Specification,
            frequencyCode: "",
            frequencyName: "",
            feeTypeMainCode: treat.FeeTypeMainCode,
            feeTypeSubCode: treat.FeeTypeSubCode,
            doctorsAdviceId: treatAdvice.Id,
            otherPrice: otherPrice,
            additional: treat.Additional,
            usageCode: "",
            usageName: "",
            longDays: 1,
            projectType: treat.CategoryCode,
            projectName: treat.CategoryName,
            projectMerge: treat.ProjectMerge,
            treatId: treat.Id,
            additionalItemsType: type,
            additionalItemsId: advice.Id);

        treatAdvice.UpdateStatus(ERecipeStatus.Saved);
        treatAdvice.ItemType = EDoctorsAdviceItemType.Treat;
        if (treatEntity.AdditionalItemsType == EAdditionalItemType.UsageAdditional && treatAdvice.Code == "300151")
        {
            treatAdvice.Additional = 1;
        }

        return (treatAdvice, treatEntity);
    }

    /// <summary>
    /// 同时提交附加项的内容
    /// </summary>
    /// <param name="advice">处置项的医嘱信息</param>
    /// <param name="treat">处置项的详细信息</param>
    /// <returns></returns>
    private async Task InsertTreatAsync(DoctorsAdvice advice, Treat treat)
    {
        AdmissionRecordDto admissionRecordDto = await _patientAppService.GetPatientInfoAsync(advice.PIID);
        if (admissionRecordDto != null)
        {
            advice.AreaCode = admissionRecordDto.AreaCode;
        }

        _ = await _doctorsAdviceRepository.InsertAsync(advice);
        _ = await _treatRepository.InsertAsync(treat);
        _ = await _medicalTechnologyMapRepository.InsertAsync(new MedicalTechnologyMap(treat.Id, EDoctorsAdviceItemType.Treat));
        _ = await _doctorsAdviceMapRepository.InsertAsync(new DoctorsAdviceMap(advice.Id));
    }

    /// <summary>
    /// 获取试算价格
    /// </summary> 
    /// <returns></returns>
    private decimal GetAmount(decimal Price, decimal RecieveQty, bool isChildren, bool Additional, out bool isAdditionalPrice, decimal? OtherPrice)
    {
        if (isChildren && Additional && OtherPrice.HasValue && OtherPrice.Value > 0)
        {
            isAdditionalPrice = true;
            return OtherPrice.Value * RecieveQty;
        }
        isAdditionalPrice = false;
        return (Price * RecieveQty);
    }

    #endregion
}
