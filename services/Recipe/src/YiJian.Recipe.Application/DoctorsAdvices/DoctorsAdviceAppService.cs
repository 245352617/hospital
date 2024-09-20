using DotNetCore.CAP;
using Google.Protobuf.Collections;
using MasterDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using Volo.Abp.Users;
using YiJian.Apis;
using YiJian.Cases.Contracts;
using YiJian.Common;
using YiJian.DoctorsAdvices.Contracts;
using YiJian.DoctorsAdvices.Dto;
using YiJian.DoctorsAdvices.Entities;
using YiJian.DoctorsAdvices.Enums;
using YiJian.Documents.Dto;
using YiJian.Dosages;
using YiJian.ECIS.Core.FastReport;
using YiJian.ECIS.DDP;
using YiJian.ECIS.ShareModel.DDPs;
using YiJian.ECIS.ShareModel.DDPs.Requests;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.ShareModel.Extensions;
using YiJian.ECIS.ShareModel.HisDto;
using YiJian.ECIS.ShareModel.Utils;
using YiJian.Hospitals;
using YiJian.Hospitals.Dto;
using YiJian.Hospitals.Enums;
using YiJian.OwnMedicines.Contracts;
using YiJian.OwnMedicines.Entities;
using YiJian.Preferences;
using YiJian.Recipe;
using YiJian.Recipe.Packages;
using YiJian.Recipe.Preferences.Contracts;
using YiJian.Recipes.Basic;
using YiJian.Recipes.DoctorsAdvices.Contracts;
using YiJian.Recipes.DoctorsAdvices.Entities;
using YiJian.Sequences.Contracts;
namespace YiJian.DoctorsAdvices;

/// <summary>
/// 医嘱
/// </summary>
[Authorize]
public partial class DoctorsAdviceAppService : RecipeAppService, IDoctorsAdviceAppService, ICapSubscribe
{
    private readonly IDoctorsAdviceRepository _doctorsAdviceRepository;
    private readonly IPrescribeRepository _prescribeRepository;
    private readonly ILisRepository _lisRepository;
    private readonly ILisItemRepository _lisItemRepository;
    private readonly IPacsRepository _pacsRepository;
    private readonly IPacsItemRepository _pacsItemRepository;
    private readonly IPacsPathologyItemRepository _pacsPathologyItemRepository;
    private readonly IPacsPathologyItemNoRepository _pacsPathologyItemNoRepository;
    private readonly ITreatRepository _treatRepository;
    private readonly IPrintInfoRepository _printInfoRepository;
    private readonly IDoctorsAdviceAuditRepository _doctorsAdviceAuditRepository;
    private readonly IToxicRepository _toxicRepository;
    private readonly IMySequenceRepository _mySequenceRepository;
    private readonly IPackageRepository _packageRepository;
    private readonly IMedDetailResultRepository _medDetailResultRepository;
    private readonly IDrugStockQueryRepository _drugStockQueryRepository;
    private readonly IDoctorsAdviceMapRepository _doctorsAdviceMapRepository;
    private readonly IOwnMedicineRepository _ownMedicineRepository;
    private readonly IImmunizationRecordRepository _iImmunizationRecordRepository;
    private readonly IUserSettingRepository _personSettingRepository;
    private readonly IPrescribeCustomRuleRepository _prescribeCustomRuleRepository;

    private readonly IDataFilter _dataFilter;
    private readonly ICapPublisher _capPublisher;
    private readonly ILogger<DoctorsAdviceAppService> _logger;

    private readonly GrpcMasterData.GrpcMasterDataClient _grpcMasterDataClient;
    private readonly IQuickStartCatalogueRepository _quickStartCatalogueRepository;
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IQuickStartAppService _quickStartAppService;
    private readonly IHospitalClientAppService _hospitalClientAppService;
    private readonly IOptionsMonitor<RemoteServices> _remoteServices;

    private readonly PatientAppService _patientAppService;
    private readonly IDosageAppService _dosageAppService;
    private readonly IMedicalTechnologyMapRepository _medicalTechnologyMapRepository;
    private readonly IPatientCaseRepository _patientCaseRepository;
    private readonly IConfiguration _configuration;

    private readonly IOptionsMonitor<DdpHospital> _ddpHospitalOptionsMonitor;
    private readonly DdpHospital _ddpHospital;
    private readonly DdpSwitch _ddpSwitch;
    private readonly IDdpApiService _ddpApiService;

    private const string DETAILID = "DetailId";
    private IPUKHISClientAppService _hisService;

    /// <summary>
    /// 医嘱
    /// </summary> 
    public DoctorsAdviceAppService(
        IDoctorsAdviceRepository doctorsAdviceRepository,
        IPrescribeRepository prescribeRepository,
        ILisRepository lisRepository,
        ILisItemRepository lisItemRepository,
        IPacsRepository pacsRepository,
        IPacsItemRepository pacsItemRepository,
        IPacsPathologyItemRepository pacsPathologyItemRepository,
        IPacsPathologyItemNoRepository pacsPathologyItemNoRepository,
        ITreatRepository treatRepository,
        IPrintInfoRepository printInfoRepository,
        IDoctorsAdviceAuditRepository doctorsAdviceAuditRepository,
        IToxicRepository toxicRepository,
        IMySequenceRepository mySequenceRepository,
        IPackageRepository packageRepository,
        IMedDetailResultRepository medDetailResultRepository,
        IDrugStockQueryRepository drugStockQueryRepository,
        IDoctorsAdviceMapRepository doctorsAdviceMapRepository,
        IOwnMedicineRepository ownMedicineRepository,
        IImmunizationRecordRepository iImmunizationRecordRepository,
        ICapPublisher capPublisher,
        ILogger<DoctorsAdviceAppService> logger,
        GrpcMasterData.GrpcMasterDataClient grpcMasterDataClient,
        IQuickStartAppService quickStartAppService,
        IHospitalClientAppService hospitalClientAppService,
        IQuickStartCatalogueRepository quickStartCatalogueRepository,
        IPrescriptionRepository prescriptionRepository,
        IOptionsMonitor<RemoteServices> remoteServices,
        PatientAppService patientAppService,
        IDosageAppService dosageAppService,
        IDataFilter dataFilter,
        IMedicalTechnologyMapRepository medicalTechnologyMapRepository,
        IPatientCaseRepository patientCaseRepository,
        IConfiguration configuration,
        IUserSettingRepository personSettingRepository,
        IPrescribeCustomRuleRepository prescribeCustomRuleRepository,
        IOptionsMonitor<DdpHospital> ddpHospitalOptionsMonitor,
        IPUKHISClientAppService pUKHISClientAppService,
        DdpSwitch ddpSwitch)
    {
        _doctorsAdviceRepository = doctorsAdviceRepository;
        _prescribeRepository = prescribeRepository;
        _lisRepository = lisRepository;
        _lisItemRepository = lisItemRepository;
        _pacsRepository = pacsRepository;
        _pacsItemRepository = pacsItemRepository;
        _pacsPathologyItemRepository = pacsPathologyItemRepository;
        _pacsPathologyItemNoRepository = pacsPathologyItemNoRepository;
        _treatRepository = treatRepository;
        _printInfoRepository = printInfoRepository;
        _doctorsAdviceAuditRepository = doctorsAdviceAuditRepository;
        _toxicRepository = toxicRepository;
        _mySequenceRepository = mySequenceRepository;
        _packageRepository = packageRepository;
        _medDetailResultRepository = medDetailResultRepository;
        _drugStockQueryRepository = drugStockQueryRepository;
        _doctorsAdviceMapRepository = doctorsAdviceMapRepository;
        _ownMedicineRepository = ownMedicineRepository;
        _iImmunizationRecordRepository = iImmunizationRecordRepository;
        _capPublisher = capPublisher;
        _logger = logger;
        _grpcMasterDataClient = grpcMasterDataClient;
        _quickStartAppService = quickStartAppService;
        _quickStartCatalogueRepository = quickStartCatalogueRepository;
        _prescriptionRepository = prescriptionRepository;
        _hospitalClientAppService = hospitalClientAppService;
        _remoteServices = remoteServices;
        _patientAppService = patientAppService;
        _dosageAppService = dosageAppService;
        _dataFilter = dataFilter;
        _medicalTechnologyMapRepository = medicalTechnologyMapRepository;
        _patientCaseRepository = patientCaseRepository;
        _configuration = configuration;
        _personSettingRepository = personSettingRepository;
        _prescribeCustomRuleRepository = prescribeCustomRuleRepository;

        _ddpHospitalOptionsMonitor = ddpHospitalOptionsMonitor;
        _ddpHospital = _ddpHospitalOptionsMonitor.CurrentValue;
        _ddpSwitch = ddpSwitch;
        _ddpApiService = _ddpSwitch.CreateService(_ddpHospital);
        //配置 configuration["HisUrl:PUKHospital"]
        //_hisService = new PUKHISClientAppService(configuration);
        _hisService = pUKHISClientAppService;
    }

    /// <summary>
    /// 获取医嘱列表集合
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    public async Task<List<DoctorsAdvicesDto>> QueryDoctorsAdvicesAsync(QueryDoctorsAdviceDto model)
    {
        var status = new List<ERecipeStatus>
        {
            ERecipeStatus.Saved,
            ERecipeStatus.Submitted,
            ERecipeStatus.Confirmed,
            ERecipeStatus.Stopped,
            ERecipeStatus.Rejected,
            ERecipeStatus.Executed,
            ERecipeStatus.PayOff,
            ERecipeStatus.ReturnPremium, ERecipeStatus.Cancelled
        };
        var setting =
            await (await _personSettingRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                x.Code == "Nullify" && x.GroupCode == "Recipe" && x.UserName ==
                (CurrentUser.FindClaimValue("name") != null ? CurrentUser.FindClaimValue("name") : CurrentUser.UserName));
        //ABP版本问题 CurrentUser.FindClaimValue("name") 取不到值

        //医生配置不显示作废医嘱以及未查询状态，需要移除掉作废状态查询
        if (setting is { Value: "false" } && !model.Status.HasValue)
        {
            status.RemoveAll(x => x == ERecipeStatus.Cancelled);
        }

        UserSetting closeTreatSetting = await _personSettingRepository.FindAsync(x => x.UserName == "system" && x.Code == "CloseAdditionalTreat");
        if (closeTreatSetting is { Value: "true" })
        {
            model.CloseAdditionalTreat = false;
        }

        var itemTypes = new List<EDoctorsAdviceItemType>
        {
            EDoctorsAdviceItemType.Prescribe,
            EDoctorsAdviceItemType.Pacs,
            EDoctorsAdviceItemType.Lis,
            EDoctorsAdviceItemType.Treat
        };

        var inItemTypes = model.ItemType.HasValue && itemTypes.Exists(t => t == model.ItemType);

        DateTime? begintime = null;
        if (model.ApplyType.HasValue) begintime = model.GetApplyBeginTime();

        var query = from d in (await _doctorsAdviceRepository.GetQueryableAsync())
                .Where(w => w.PIID == model.PIID && w.PlatformType == model.PlatformType &&
                            status.Contains(w.Status))
                .WhereIf(model.Status.HasValue && status.Contains(model.Status.Value),
                    w => w.Status == model.Status.Value)
                .WhereIf(begintime.HasValue, w => w.ApplyTime >= begintime.Value)
                .WhereIf(!model.CategoryCode.IsNullOrEmpty(), w => w.CategoryCode == model.CategoryCode.Trim())
                .WhereIf(!model.Keyword.IsNullOrEmpty(),
                    w => w.Name.Trim().Contains(model.Keyword.Trim()) ||
                         w.ApplyDoctorName.Trim().Contains(model.Keyword.Trim()) ||
                         w.PyCode.Contains(model.Keyword.Trim()))
                .WhereIf(!model.PrescribeTypeCode.IsNullOrEmpty(),
                    w => w.PrescribeTypeCode == model.PrescribeTypeCode.Trim())
                .WhereIf(inItemTypes, w => w.ItemType == model.ItemType)
                    join pr in (await _prescribeRepository.GetQueryableAsync()) on d.Id equals pr.DoctorsAdviceId into temp
                    from p in temp.DefaultIfEmpty()
                    join t in (await _treatRepository.GetQueryableAsync()) on d.Id equals t.DoctorsAdviceId into temp2
                    from t2 in temp2.DefaultIfEmpty()
                    join pa in (await _pacsRepository.GetQueryableAsync()) on d.Id equals pa.DoctorsAdviceId into temp3
                    from t3 in temp3.DefaultIfEmpty()
                    join li in (await _lisRepository.GetQueryableAsync()) on d.Id equals li.DoctorsAdviceId into temp4
                    from t4 in temp4.DefaultIfEmpty()
                    orderby d.RecipeNo ascending, d.RecipeGroupNo ascending
                    select new DoctorsAdvicesDto
                    {
                        #region DoctorsAdvicesDto

                        Id = d.Id,
                        PlatformType = d.PlatformType,
                        PIID = d.PIID,
                        PatientId = d.PatientId,
                        PatientName = d.PatientName,
                        Code = d.Code,
                        Name = d.Name,
                        CategoryCode = d.CategoryCode,
                        CategoryName = d.CategoryName,
                        IsBackTracking = d.IsBackTracking,
                        PrescriptionNo = d.PrescriptionNo,
                        RecipeNo = d.RecipeNo,
                        RecipeGroupNo = d.RecipeGroupNo,
                        ApplyTime = d.ApplyTime,
                        ApplyDoctorCode = d.ApplyDoctorCode,
                        ApplyDoctorName = d.ApplyDoctorName,
                        ApplyDeptCode = d.ApplyDeptCode,
                        ApplyDeptName = d.ApplyDeptName,
                        TraineeCode = d.TraineeCode,
                        TraineeName = d.TraineeName,
                        ExecDeptCode = d.ExecDeptCode,
                        ExecDeptName = d.ExecDeptName,
                        ExecutorCode = d.ExecutorCode,
                        ExecutorName = d.ExecutorName,
                        ExecTime = d.ExecTime,
                        StopDoctorCode = d.StopDoctorCode,
                        StopDoctorName = d.StopDoctorName,
                        StopDateTime = d.StopDateTime,
                        Status = d.Status,
                        PayTypeCode = d.PayTypeCode,
                        PayType = d.PayType,
                        PayStatus = d.PayStatus,
                        Price = d.Price,
                        Unit = d.Unit,
                        Amount = d.Amount,
                        InsuranceCode = d.InsuranceCode,
                        InsuranceType = d.InsuranceType,
                        IsChronicDisease = d.IsChronicDisease,
                        IsRecipePrinted = d.IsRecipePrinted,
                        HisOrderNo = d.HisOrderNo,
                        Diagnosis = d.Diagnosis,
                        PositionCode = d.PositionCode,
                        PositionName = d.PositionName,
                        ItemType = d.ItemType,
                        Remarks = d.Remarks,
                        ChargeCode = d.ChargeCode,
                        ChargeName = d.ChargeName,
                        MeducalInsuranceCode = d.MeducalInsuranceCode,
                        YBInneCode = d.YBInneCode,
                        MeducalInsuranceName = d.MeducalInsuranceName,
                        PrescribeTypeCode = d.PrescribeTypeCode,
                        PrescribeTypeName = d.PrescribeTypeName,
                        StartTime = d.StartTime,
                        EndTime = d.EndTime,
                        RecieveQty = d.RecieveQty,
                        RecieveUnit = d.RecieveUnit,
                        CommitSerialNo = d.CommitSerialNo,
                        CreationTime = d.CreationTime,
                        IsPrint = d.IsPrint,
                        PrescribeId = p != null ? p.Id : null,
                        IsOutDrug = p != null && p.IsOutDrug,
                        MedicineProperty = p != null ? p.MedicineProperty : "",
                        ToxicProperty = p != null ? p.ToxicProperty : "",
                        //UsageCode = p != null ? p.UsageCode : "",
                        //UsageName = p != null ? p.UsageName : "", 
                        UsageCode = d.ItemType == EDoctorsAdviceItemType.Prescribe
                            ? (p != null ? p.UsageCode : "")
                            : (d.ItemType == EDoctorsAdviceItemType.Treat ? (t2 != null ? t2.UsageCode : "") : ""),
                        UsageName = d.ItemType == EDoctorsAdviceItemType.Prescribe
                            ? (p != null ? p.UsageName : "")
                            : (d.ItemType == EDoctorsAdviceItemType.Treat ? (t2 != null ? t2.UsageName : "") : ""),
                        Speed = p != null ? p.Speed : "",
                        //LongDays = p != null ? p.LongDays : 0,
                        LongDays = d.ItemType == EDoctorsAdviceItemType.Prescribe ? (p != null ? p.LongDays : 1) : null,
                        ActualDays = p != null ? p.ActualDays : 0,
                        DosageForm = p != null ? p.DosageForm : string.Empty,
                        DosageQty = p != null ? p.DosageQty : 0,
                        DefaultDosageQty = p != null ? p.DefaultDosageQty : 0,
                        QtyPerTimes = p != null ? p.QtyPerTimes : 0,
                        DosageUnit = p != null ? p.DosageUnit : "",
                        DefaultDosageUnit = p != null ? p.DefaultDosageUnit : "",
                        Unpack = p != null ? p.Unpack : 0,
                        BigPackPrice = p != null ? p.BigPackPrice : 0,
                        BigPackFactor = p != null ? p.BigPackFactor : 0,
                        BigPackUnit = p != null ? p.BigPackUnit : "",
                        SmallPackPrice = p != null ? p.SmallPackPrice : 0,
                        SmallPackUnit = p != null ? p.SmallPackUnit : "",
                        SmallPackFactor = p != null ? p.SmallPackFactor : 0,
                        //FrequencyCode = p != null ? p.FrequencyCode : "",
                        //FrequencyName = p != null ? p.FrequencyName : "", 
                        FrequencyCode = (d.ItemType == EDoctorsAdviceItemType.Prescribe && p != null)
                            ? p.FrequencyCode
                            : ((d.ItemType == EDoctorsAdviceItemType.Treat && t2 != null) ? t2.FrequencyCode : ""),
                        FrequencyName = (d.ItemType == EDoctorsAdviceItemType.Prescribe && p != null)
                            ? p.FrequencyName
                            : ((d.ItemType == EDoctorsAdviceItemType.Treat && t2 != null) ? t2.FrequencyName : ""),
                        MedicalInsuranceCode = (d.ItemType == EDoctorsAdviceItemType.Prescribe && p != null)
                            ? p.MedicalInsuranceCode
                            : string.Empty,
                        FrequencyTimes = p != null ? p.FrequencyTimes : null,
                        FrequencyUnit = p != null ? p.FrequencyUnit : "",
                        FrequencyExecDayTimes = p != null ? p.FrequencyExecDayTimes : null,
                        PharmacyCode = p != null ? p.PharmacyCode : "",
                        PharmacyName = p != null ? p.PharmacyName : "",
                        FactoryName = p != null ? p.FactoryName : "",
                        FactoryCode = p != null ? p.FactoryCode : "",
                        BatchNo = p != null ? p.BatchNo : "",
                        ExpirDate = p != null ? p.ExpirDate : null,
                        IsSkinTest = p != null ? p.IsSkinTest : null,
                        SkinTestResult = p != null ? p.SkinTestResult : null,
                        SkinTestSignChoseResult = p != null ? p.SkinTestSignChoseResult : null,
                        MaterialPrice = p != null ? p.MaterialPrice : null,
                        IsBindingTreat = p != null ? p.IsBindingTreat : null,
                        IsAmendedMark = p != null ? p.IsAmendedMark : null,
                        IsAdaptationDisease = p != null ? p.IsAdaptationDisease : null,
                        IsFirstAid = p != null ? p.IsFirstAid : null,
                        AntibioticPermission = p != null ? p.AntibioticPermission : 0,
                        PrescriptionPermission = p != null ? p.PrescriptionPermission : 0,
                        Specification = (d.ItemType == EDoctorsAdviceItemType.Prescribe && p != null)
                            ? p.Specification
                            : ((d.ItemType == EDoctorsAdviceItemType.Treat && t2 != null)
                                ? t2.Specification
                                : ""), //药品 诊疗都有
                        LimitType = p != null ? p.LimitType : null,
                        RestrictedDrugs = p != null ? p.RestrictedDrugs : null,
                        DailyFrequency = p != null ? p.DailyFrequency : null,
                        MedicineId = p != null ? p.MedicineId : null,
                        IsCriticalPrescription = p != null ? p.IsCriticalPrescription : false,
                        HisDosageQty = p != null && p.HisDosageQty > 0 ? p.HisDosageQty : 0,
                        HisDosageUnit = p != null ? p.HisDosageUnit : "",
                        HisUnit = p != null ? p.HisUnit : "",

                        //诊疗
                        TreatId = t2 != null ? t2.Id : null,
                        intTreatId = t2 != null ? t2.TreatId : 0,
                        OtherPrice = t2 != null ? t2.OtherPrice : null,
                        FeeTypeMainCode = t2 != null ? t2.FeeTypeMainCode : "",
                        FeeTypeSubCode = t2 != null ? t2.FeeTypeSubCode : "",
                        ProjectMerge = t2 != null ? t2.ProjectMerge : "",
                        ProjectType = t2 != null ? t2.ProjectType : "",
                        ProjectName = t2 != null ? t2.ProjectName : "",
                        AdditionalItemsType = t2 != null ? t2.AdditionalItemsType : EAdditionalItemType.No,
                        AdditionalItemsId = t2 != null ? t2.AdditionalItemsId : Guid.Empty,
                        Additional = t2 != null && t2.Additional,
                        PacsId = t3 != null ? t3.Id : null,
                        PartCode = t3 != null ? t3.PartCode : "",
                        PartName = t3 != null ? t3.PartName : "",
                        CatalogCode = (d.ItemType == EDoctorsAdviceItemType.Pacs && t3 != null)
                            ? t3.CatalogCode
                            : ((d.ItemType == EDoctorsAdviceItemType.Lis && t4 != null) ? t4.CatalogCode : ""),
                        CatalogName = (d.ItemType == EDoctorsAdviceItemType.Pacs && t3 != null)
                            ? t3.CatalogName
                            : ((d.ItemType == EDoctorsAdviceItemType.Lis && t4 != null) ? t4.CatalogName : ""),
                        ClinicalSymptom = (d.ItemType == EDoctorsAdviceItemType.Pacs && t3 != null)
                            ? t3.ClinicalSymptom
                            : ((d.ItemType == EDoctorsAdviceItemType.Lis && t4 != null) ? t4.ClinicalSymptom : ""),
                        MedicalHistory = t3 != null ? t3.MedicalHistory : "",
                        CatalogDisplayName = t3 != null ? t3.CatalogDisplayName : "",
                        IsEmergency = (d.ItemType == EDoctorsAdviceItemType.Pacs && t3 != null)
                            ? t3.IsEmergency
                            : ((d.ItemType == EDoctorsAdviceItemType.Lis && t4 != null) ? t4.IsEmergency : null),
                        IsBedSide = (d.ItemType == EDoctorsAdviceItemType.Pacs && t3 != null)
                            ? t3.IsBedSide
                            : ((d.ItemType == EDoctorsAdviceItemType.Lis && t4 != null) ? t4.IsBedSide : null),

                        LisId = t4 != null ? t4.Id : null,
                        SpecimenCode = t4 != null ? t4.SpecimenCode : "",
                        SpecimenName = t4 != null ? t4.SpecimenName : "",
                        SpecimenPartCode = t4 != null ? t4.SpecimenPartCode : "",
                        SpecimenPartName = t4 != null ? t4.SpecimenPartName : "",
                        ContainerCode = t4 != null ? t4.ContainerCode : "",
                        ContainerName = t4 != null ? t4.ContainerName : "",
                        ContainerColor = t4 != null ? t4.ContainerColor : "",
                        SpecimenDescription = t4 != null ? t4.SpecimenDescription : "",
                        SpecimenCollectDatetime = t4 != null ? t4.SpecimenCollectDatetime : null,
                        SpecimenReceivedDatetime = t4 != null ? t4.SpecimenReceivedDatetime : null,

                        #endregion
                    };

        //var sql = query.ToQueryString();
        //Console.WriteLine("=======================================");
        //Console.WriteLine(sql);
        //Console.WriteLine("=======================================");

        List<DoctorsAdvicesDto> doctorsAdvicesDtoList = await query.OrderBy(x => x.CreationTime).ThenBy(o => o.RecipeNo).ThenBy(t => t.RecipeGroupNo).ThenBy(t => t.ApplyTime).ToListAsync();
        List<Guid> pacsIds = new List<Guid>();
        List<Guid> lisIds = new List<Guid>();
        foreach (DoctorsAdvicesDto item in doctorsAdvicesDtoList)
        {
            if (item.PacsId.HasValue)
            {
                pacsIds.Add(item.PacsId.Value);
            }

            if (item.LisId.HasValue)
            {
                lisIds.Add(item.LisId.Value);
            }
        }
        if (pacsIds.Any())
        {
            List<PacsPathologyItem> pacsPathologyItems = await _pacsPathologyItemRepository.GetListAsync(x => pacsIds.Contains(x.PacsId));
            List<PacsItem> pacsItems = await _pacsItemRepository.GetListAsync(x => pacsIds.Contains(x.PacsId));

            foreach (DoctorsAdvicesDto item in doctorsAdvicesDtoList)
            {
                if (item.PacsId.HasValue)
                {
                    PacsPathologyItem pacsPathologyItem = pacsPathologyItems.FirstOrDefault(x => x.PacsId == item.PacsId.Value);
                    if (pacsPathologyItem is not null)
                    {
                        PacsPathologyItemDto pacsPathologyItemDto = new PacsPathologyItemDto();
                        pacsPathologyItemDto.PacsId = pacsPathologyItem.PacsId;
                        pacsPathologyItemDto.Specimen = pacsPathologyItem.Specimen;
                        pacsPathologyItemDto.DrawMaterialsPart = pacsPathologyItem.DrawMaterialsPart;
                        pacsPathologyItemDto.SpecimenQty = pacsPathologyItem.SpecimenQty;
                        pacsPathologyItemDto.LeaveTime = pacsPathologyItem.LeaveTime;
                        pacsPathologyItemDto.RegularTime = pacsPathologyItem.RegularTime;
                        pacsPathologyItemDto.SpecificityInfect = pacsPathologyItem.SpecificityInfect;
                        pacsPathologyItemDto.ApplyForObjective = pacsPathologyItem.ApplyForObjective;
                        pacsPathologyItemDto.Symptom = pacsPathologyItem.Symptom;
                        item.pacsPathologyItemDto = pacsPathologyItemDto;
                    }

                    List<PacsItem> pacsItemList = pacsItems.Where(x => x.PacsId == item.PacsId.Value).ToList();
                    item.pacsItemDtos = ObjectMapper.Map<List<PacsItem>, List<PacsItemDto>>(pacsItemList);
                }
            }
        }

        if (lisIds.Any())
        {
            List<LisItem> lisItems = await _lisItemRepository.GetListAsync(x => lisIds.Contains(x.LisId));
            foreach (DoctorsAdvicesDto item in doctorsAdvicesDtoList)
            {
                if (item.LisId.HasValue)
                {
                    List<LisItem> lisItemList = lisItems.Where(x => x.LisId == item.LisId.Value).ToList();
                    item.lisItemDtos = ObjectMapper.Map<List<LisItem>, List<LisItemDto>>(lisItemList);
                }
            }
        }
        #region 药理相关的内容

        var medicineIds = doctorsAdvicesDtoList.Where(w => w.ItemType == EDoctorsAdviceItemType.Prescribe && w.MedicineId.HasValue)
            .Select(s => s.MedicineId.Value).ToList();

        if (medicineIds.Any())
        {
            var meds = await (await _toxicRepository.GetQueryableAsync()).Where(w => medicineIds.Contains(w.MedicineId))
                .ToListAsync();

            var IsChildren = model.PatientInfo.IsChildren();

            foreach (var item in doctorsAdvicesDtoList)
            {
                if (item.ItemType == EDoctorsAdviceItemType.Prescribe)
                {
                    var toxic = meds.FirstOrDefault(w => w.MedicineId == item.MedicineId);
                    if (toxic != null)
                    {
                        item.ToxicLevel = toxic.ToxicLevel;
                        item.AntibioticLevel = toxic.AntibioticLevel;
                    }
                }
                else if (item.ItemType == EDoctorsAdviceItemType.Treat)
                {
                    bool isAdditionalPrice = false;
                    item.Amount = item.GetAmount(IsChildren, out isAdditionalPrice);
                    item.IsAdditionalPrice = isAdditionalPrice;
                }
            }

        }

        #endregion

        #region 附加处置跟在所属药品后面

        var doctorList = new List<DoctorsAdvicesDto>();
        var noList = doctorsAdvicesDtoList.Where(x => x.AdditionalItemsType == EAdditionalItemType.No).GroupBy(g => g.RecipeNo);

        var isChildren = model.PatientInfo.IsChildren();
        foreach (var t in noList)
        {
            var doctorAdviceList = doctorsAdvicesDtoList.Where(x => x.RecipeNo == t.Key).ToList();
            doctorList.AddRange(doctorAdviceList);

            //添加控制开关
            if (!model.CloseAdditionalTreat.HasValue || !model.CloseAdditionalTreat.Value)
            {
                var items = doctorsAdvicesDtoList.Where(x =>
                x.AdditionalItemsType != EAdditionalItemType.No &&
                doctorAdviceList.Select(s => s.Id).ToList().Contains(x.AdditionalItemsId)).ToList();
                if (items.Any())
                {
                    doctorList.AddRange(items);
                }
            }
        }

        #endregion

        return doctorList;
    }

    /// <summary>
    /// 获取医嘱列表集合
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<List<DoctorsAdvicesDto>> QueryDoctorsAdvicesV2Async(QueryDoctorsAdviceDto model)
    {
        return await QueryDoctorsAdvicesAsync(model);
    }

    /// <summary>
    /// 获取医嘱打印数据
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<List<DoctorsAdvicesPrintDto>> PrintDoctorsAdvicesAsync(QueryDoctorsAdviceDto model)
    {
        model.ApplyType = EApplyType.All;
        List<DoctorsAdvicesDto> doctorsAdvicesDtos = (await QueryDoctorsAdvicesAsync(model))
            // 打印数据不显示“已作废”、“未提交”的医嘱
            .Where((x => x.Status != ERecipeStatus.Cancelled && x.Status != ERecipeStatus.Saved))
            .ToList();
        List<string> doctorCodes = doctorsAdvicesDtos.Select(x => x.ApplyDoctorCode).Where(x => !string.IsNullOrEmpty(x)).ToList();
        List<string> executorCodes = doctorsAdvicesDtos.Select(x => x.ExecutorCode).Where(x => !string.IsNullOrEmpty(x)).ToList();
        doctorCodes.AddRange(executorCodes);
        doctorCodes = doctorCodes.Distinct().ToList();
        Dictionary<string, string> dic = new Dictionary<string, string>();
        foreach (string doctorCode in doctorCodes)
        {
            string stampBase = await _hospitalClientAppService.QueryStampBaseAsync(doctorCode);
            if (!string.IsNullOrEmpty(stampBase))
            {
                dic.Add(doctorCode, stampBase);
            }
        }

        foreach (DoctorsAdvicesDto doctorsAdvicesDto in doctorsAdvicesDtos)
        {
            if (!string.IsNullOrEmpty(doctorsAdvicesDto.ApplyDoctorCode) && dic.ContainsKey(doctorsAdvicesDto.ApplyDoctorCode))
            {
                doctorsAdvicesDto.ApplyDoctorSignature = dic[doctorsAdvicesDto.ApplyDoctorCode];
            }

            if (!string.IsNullOrEmpty(doctorsAdvicesDto.ExecutorCode) && dic.ContainsKey(doctorsAdvicesDto.ExecutorCode))
            {
                doctorsAdvicesDto.ExecutorSignature = dic[doctorsAdvicesDto.ExecutorCode];
            }
        }

        var groups = doctorsAdvicesDtos.GroupBy(x => x.ApplyDate);
        List<DoctorsAdvicesPrintDto> doctorsAdvicesPrintDtos = new List<DoctorsAdvicesPrintDto>();
        foreach (var item in groups)
        {
            DoctorsAdvicesPrintDto doctorsAdvicesPrintDto = new DoctorsAdvicesPrintDto()
            {
                Date = item.Key,
                doctorsAdvicesDtos = item.ToList()
            };
            doctorsAdvicesPrintDtos.Add(doctorsAdvicesPrintDto);
        }

        return doctorsAdvicesPrintDtos;
    }

    /// <summary>
    /// 获取选中的打印数据
    /// </summary>
    /// <param name="piid"></param>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    [NonUnify]
    public async Task<RecipesPrintDto> GetPrintRecipesAsync(Guid piid, List<Guid> ids)
    {
        try
        {
            AdmissionRecordDto admissionRecordDto = await _patientAppService.GetPatientInfoAsync(piid);
            QueryDoctorsAdviceDto queryDoctorsAdviceDto = new QueryDoctorsAdviceDto()
            {
                ApplyType = EApplyType.All,
                PIID = piid,
                PatientInfo = new PatientInfoDto() { PatientIDCard = admissionRecordDto?.IDNo }
            };

            List<DoctorsAdvicesDto> doctorsAdvicesDtos = (await QueryDoctorsAdvicesAsync(queryDoctorsAdviceDto))
                // 打印数据不显示“已作废”、“未提交”的医嘱
                .Where((x => x.Status != ERecipeStatus.Cancelled && x.Status != ERecipeStatus.Saved))
                .ToList();
            doctorsAdvicesDtos = doctorsAdvicesDtos.Where(x => ids.Contains(x.Id)).ToList();

            List<string> doctorCodes = doctorsAdvicesDtos.Select(x => x.ApplyDoctorCode).Where(x => !string.IsNullOrEmpty(x)).ToList();
            List<string> executorCodes = doctorsAdvicesDtos.Select(x => x.ExecutorCode).Where(x => !string.IsNullOrEmpty(x)).ToList();
            doctorCodes.AddRange(executorCodes);
            doctorCodes = doctorCodes.Distinct().ToList();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (string doctorCode in doctorCodes)
            {
                string stampBase = await _hospitalClientAppService.QueryStampBaseAsync(doctorCode);
                if (!string.IsNullOrEmpty(stampBase))
                {
                    dic.Add(doctorCode, stampBase);
                }
            }

            foreach (DoctorsAdvicesDto doctorsAdvicesDto in doctorsAdvicesDtos)
            {
                if (!string.IsNullOrEmpty(doctorsAdvicesDto.ApplyDoctorCode) && dic.ContainsKey(doctorsAdvicesDto.ApplyDoctorCode))
                {
                    doctorsAdvicesDto.ApplyDoctorSignature = dic[doctorsAdvicesDto.ApplyDoctorCode];
                }

                if (!string.IsNullOrEmpty(doctorsAdvicesDto.ExecutorCode) && dic.ContainsKey(doctorsAdvicesDto.ExecutorCode))
                {
                    doctorsAdvicesDto.ExecutorSignature = dic[doctorsAdvicesDto.ExecutorCode];
                }
            }

            RecipesPrintDto recipesPrintDto = new RecipesPrintDto();
            recipesPrintDto.AdmissionRecords.Add(admissionRecordDto);
            recipesPrintDto.DoctorsAdvicesDtos = doctorsAdvicesDtos;
            foreach (var item in recipesPrintDto.DoctorsAdvicesDtos)
            {
                item.pacsItemDtos = null;
                item.lisItemDtos = null;
            }
            return recipesPrintDto;

        }
        catch (Exception ex)
        {
            _logger.LogError("获取选中的打印数据异常：" + ex.Message);
            throw Oh.Error("获取选中的打印数据异常：" + ex.Message);
        }
    }

    /// <summary>
    /// 获取打印的xml
    /// </summary>
    /// <param name="piid"></param>
    /// <param name="ids"></param>
    /// <returns></returns>
    [NonUnify]
    [AllowAnonymous]
    public async Task<string> GetPrintRecipesXmlAsync(Guid piid, List<Guid> ids)
    {
        try
        {
            RecipesPrintDto data = await GetPrintRecipesAsync(piid, ids);
            string xsdAndXmlString = FastReportUtils.GetXmlSchemalAndDataString(data);

            return xsdAndXmlString;
        }
        catch (Exception ex)
        {
            _logger.LogError("获取打印xml数据异常：" + ex.Message);
            throw Oh.Error("获取打印xml数据异常：" + ex.Message);
        }
    }

    /// <summary>
    /// 更新是否已打
    /// </summary>
    /// <param name="doctorsAdvicePrintDtos"></param>
    /// <returns></returns>
    public async Task<bool> UpdateIsPrintAsync(List<DoctorsAdvicePrintDto> doctorsAdvicePrintDtos)
    {
        IEnumerable<Guid> ids = doctorsAdvicePrintDtos.Select(x => x.Id);
        List<DoctorsAdvice> doctorsAdvices = await _doctorsAdviceRepository.GetListAsync(x => ids.Contains(x.Id));
        foreach (DoctorsAdvice item in doctorsAdvices)
        {
            DoctorsAdvicePrintDto doctorsAdvicePrintDto = doctorsAdvicePrintDtos.FirstOrDefault(x => x.Id == item.Id);
            if (doctorsAdvicePrintDto is not null)
            {
                item.IsPrint = doctorsAdvicePrintDto.IsPrint;
            }
        }

        if (doctorsAdvices.Any()) await _doctorsAdviceRepository.UpdateManyAsync(doctorsAdvices);
        return true;
    }

    /// <summary>
    /// 添加医嘱（合集，包括 药方，检查，检验，诊疗）-- 院前急救使用
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [UnitOfWork]
    public async Task<bool> AddFullAdviceAsync(AddFullAdviceDto model)
    {
        //添加医嘱检查
        if (model.AddPacsListes.Any())
        {
            foreach (var pacs in model.AddPacsListes)
            {
                var addpacs = new AddPacsDto()
                { DoctorsAdvice = pacs.DoctorsAdvice, Items = new List<PacsDto>() { pacs.Items } }; //兼容操作
                await AddPacsAsync(addpacs);
            }
        }

        //添加医嘱检验
        if (model.AddLises.Any())
        {
            foreach (var lis in model.AddLises)
            {
                var addlis = new AddLisDto()
                { DoctorsAdvice = lis.DoctorsAdvice, Items = new List<LisDto>() { lis.Items } }; //兼容操作
                await AddLisAsync(addlis);
            }
        }

        //添加医嘱药方
        if (model.AddPrescribeses.Any())
        {
            foreach (var prescribes in model.AddPrescribeses)
            {
                await AddPrescribeAsync(prescribes);
            }
        }

        //添加医嘱诊疗
        if (model.AddTreats.Any())
        {
            foreach (var treat in model.AddTreats)
            {
                await AddTreatAsync(treat);
            }
        }

        return true;
    }


    /// <summary>
    /// 添加套餐（合集，包括 药方，检查，检验，诊疗）-- 急诊使用 (改方法不支持疫苗接种记录操作)
    /// </summary> 
    /// <param name="model"></param>
    /// <returns></returns> 
    //[AllowAnonymous]
    [UnitOfWork]
    public async Task<bool> AddSetMealAdviceAsync(SetMealAdviceDto model)
    {
        SetMealAdviceV2Dto dto = new()
        {
            PatientInfo = model.PatientInfo,
            DoctorsAdvice = model.DoctorsAdvice,
            PackageId = model.PackageId,
            Entries = model.EntryIds.Select(x => new SetMealAdviceEntry
            {
                EntryId = x,
                SkinTestSignChoseResult = null, // 默认值
                LimitType = 0, // 默认值
                RestrictedDrugs = null,
            }).ToList()
        };

        return await this.AddSetMealAdviceV2Async(dto);
    }

    /// <summary>
    /// 套餐开医嘱（合集，包括 药方，检查，检验，诊疗）-- 急诊使用
    /// </summary> 
    /// <param name="model"></param>
    /// <returns></returns> 
    [UnitOfWork]
    //[AllowAnonymous]
    public async Task<bool> AddSetMealAdviceV2Async(SetMealAdviceV2Dto model)
    {
        if (model.Entries != null && !model.Entries.Any())
        {
            Oh.Error("项目内容不能为空");
        }

        var isChildren = false;
        if (model.PatientInfo != null && model.PatientInfo.IsChildren())
        {
            isChildren = true;
        }

        var task = _patientAppService.GetPatientInfoAsync(model.DoctorsAdvice.PIID);

        var package = await (await _packageRepository.GetQueryableAsync())
            .Include(x => x.Projects.OrderBy(x => x.EntryId))
            .ThenInclude(x => x.Project)
            .ThenInclude(x => x.MedicineProp)
            .Include(x => x.Projects.OrderBy(x => x.EntryId))
            .ThenInclude(x => x.Project)
            .ThenInclude(x => x.LabProp)
            .Include(x => x.Projects.OrderBy(x => x.EntryId))
            .ThenInclude(x => x.Project)
            .ThenInclude(x => x.ExamProp)
            .Include(x => x.Projects.OrderBy(x => x.EntryId))
            .ThenInclude(x => x.Project)
            .ThenInclude(x => x.TreatProp)
            .FirstOrDefaultAsync(x => x.Id == model.PackageId);

        if (package == null) Oh.Error("查无此医嘱套餐");

        var projects = package.Projects.Where(w => model.Entries.Select(x => x.EntryId).Contains(w.EntryId))
            .ToList();

        var list = new List<DoctorsAdvice>();

        var doctorsAdvices = new List<DoctorsAdvice>();
        var prescribes = new List<Prescribe>();
        var pacses = new List<Pacs>();
        var lises = new List<Lis>();
        var pacsesItem = new List<PacsItem>();
        var lisesItem = new List<LisItem>();
        var treats = new List<Treat>();


        //GRPC 查询费别的信息
        var medicineIds = projects.Select(s => s.Project.Code).Distinct().ToList();
        var codes = projects.Select(s => s.Project.SourceId.ToString()).Distinct().ToList();

        RepeatedField<ChargeInfoModel> charges = null;

        #region 获取费别信息

        try
        {
            var request = new GetChargeInfoRequest();
            request.Codes.AddRange(codes);
            var grpcCharges = await _grpcMasterDataClient.GetChargeInfosAsync(request);
            charges = grpcCharges.ChargeInfos;
        }
        catch (Exception ex)
        {
            _logger.LogError(message: "获取费别信息异常(GRPC)：{ex}", ex);
        }

        #endregion

        List<GrpcMedicineModel> medicines = new List<GrpcMedicineModel>();

        #region 获取药品集合

        try
        {
            var medicinesRequest = new GetListMedicinesByCodeRequest();
            medicinesRequest.MedicineCode.AddRange(medicineIds);
            MedicinesResponse grpcMedicines = await _grpcMasterDataClient.GetMedicineListByMedicineCodesAsync(medicinesRequest);
            medicines = grpcMedicines.Medicines.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError("获取药品信息异常(GRPC)：{ex}", ex);
        }

        #endregion

        foreach (var item in projects)
        {
            var advice = model.DoctorsAdvice;
            // 医嘱套餐提交的明细信息
            var entryInfo = model.Entries.First(x => x.EntryId == item.EntryId);

            #region 构建用户公共传过来的

            var detailId = await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), DETAILID);

            var newDoctorsAdvice = new DoctorsAdvice(
                id: GuidGenerator.Create(),
                detailId: detailId,
                piid: advice.PIID,
                platformType: advice.PlatformType,
                patientId: advice.PatientId,
                patientName: advice.PatientName,
                applyDeptCode: advice.ApplyDeptCode,
                applyDeptName: advice.ApplyDeptName,
                applyDoctorCode: advice.ApplyDoctorCode,
                applyDoctorName: advice.ApplyDoctorName,
                diagnosis: advice.Diagnosis,
                traineeCode: advice.TraineeCode,
                traineeName: advice.TraineeName,
                isChronicDisease: advice.IsChronicDisease,
                recieveQty: item.RecieveQty, //item.LongDays,
                recieveUnit: item.RecieveUnit,
                prescribeTypeCode: item.PrescribeTypeCode,
                prescribeTypeName: item.PrescribeTypeName);
            newDoctorsAdvice.YBInneCode = advice.YBInneCode;
            newDoctorsAdvice.MeducalInsuranceCode = advice.MeducalInsuranceCode;
            newDoctorsAdvice.MeducalInsuranceName = advice.MeducalInsuranceName;
            #endregion

            #region 构建套装项目
            GrpcMedicineModel medicine = null;
            RecipeProject project = item.Project;

            if (medicines.Any() && project.CategoryCode.ToLower() == "medicine")
            {
                //grpc 使用 code 查询之后，需要使用药品编码，规格，药房编码 和 厂家编码查询定位 
                medicine = medicines.FirstOrDefault(w => w.Code == project.Code && w.Specification == project.Specification && w.PharmacyCode == project.MedicineProp.PharmacyCode && w.FactoryCode == project.MedicineProp.FactoryCode);
                if (medicine == null)
                {
                    _logger.LogInformation($"从HIS提供的视图中无法获取【{project!.Name}】药品信息，急诊查询的参数是：MedicineCode={project!.Code}, Specification={project!.Specification}, PharmacyName={project!.MedicineProp!.PharmacyName}, FactoryCode={project!.MedicineProp!.FactoryCode}");
                    Oh.Error($"您引用的药品【{project!.Name}】暂无库存！");
                }
            }


            string chargeCode = item.Project.ChargeCode;
            string chargeName = item.Project.ChargeName;

            if (charges != null)
            {
                ChargeInfoModel charge = charges.FirstOrDefault(w => w.Code == project.SourceId.ToString());
                if (charge != null)
                {
                    chargeCode = charge.ChargeCode;
                    chargeName = charge.ChargeName;
                }
            }

            newDoctorsAdvice.BuildProject(
                code: project.Code,
                name: project.Name,
                categoryCode: project.CategoryCode,
                categoryName: project.CategoryName,
                isBackTracking: false, //TODO 目前没有
                prescriptionNo: "", //TODO 目前没有 
                payTypeCode: "", //TODO 目前没有
                payType: ERecipePayType.Self, //TODO 目前没有
                price: (project.CategoryCode.ToLower() == "medicine" && medicine != null) ? (decimal)medicine.Price : project.Price, //project.Price,
                unit: project.Unit,
                insuranceCode: "", //TODO 目前没有
                insuranceType: EInsuranceCatalog.Self,
                hisOrderNo: "",
                executorCode: "", //TODO 目前没有
                executorName: "", //TODO 目前没有 
                remarks: item.Remarks ?? "",
                chargeCode: chargeCode,
                chargeName: chargeName);

            #endregion

            string categoryCode = item.Project.CategoryCode.ToLower();
            //创建套餐时检验检查项目保存有科室，开立医嘱时使用检验检查的保存的科室并保存在数据库中
            if ("lab".Equals(categoryCode) || "exam".Equals(categoryCode))
            {
                newDoctorsAdvice.ExecDeptCode = string.IsNullOrEmpty(project.ExecDeptCode) ? item.ExecDeptCode : project.ExecDeptCode;
                newDoctorsAdvice.ExecDeptName = string.IsNullOrEmpty(project.ExecDeptName) ? item.ExecDeptName : project.ExecDeptName;
            }

            //创建套餐时处置没有保存科室字段，开立医嘱时使用接诊科室并保存在数据库中
            //创建套餐时药品没有保存科室字段，开立医嘱时使用接诊科室并保存在数据库中
            if ("medicine".Equals(categoryCode) || "treat".Equals(categoryCode))
            {
                newDoctorsAdvice.ExecDeptCode = entryInfo.ExecDeptCode;
                newDoctorsAdvice.ExecDeptName = entryInfo.ExecDeptName;
            }

            var immunizationRecord = entryInfo.ImmunizationRecord;

            switch (item.Project.CategoryCode.ToLower())
            {
                case "lab": //检验
                    if (!isChildren && item.SpecimenName == "指血")
                    {
                        Oh.Error("检验标本为手指血的项目，>=6岁的患者不能开");
                    }
                    AddSetMealLis(lises, item, newDoctorsAdvice);
                    break;
                case "exam": //检查 
                    await AddSetMealPacsAsync(pacses, item, newDoctorsAdvice, model.PatientInfo);
                    break;
                case "medicine": //药品    
                    await AddSetMealPrescribeAsync(prescribes, item, medicines, newDoctorsAdvice, immunizationRecord,
                        entryInfo.SkinTestSignChoseResult, entryInfo.RestrictedDrugs, entryInfo.LimitType);
                    break;
                default: //诊疗   
                    await AddSetMealTreat(treats, item, newDoctorsAdvice, model.PatientInfo.IsChildren());
                    break;
            }

            list.Add(newDoctorsAdvice);
            if (newDoctorsAdvice.PlatformType == EPlatformType.EmergencyTreatment &&
                item.Project.CategoryCode.ToLower() == "medicine")
            {
                if (list.All(a => a.RecipeNo != "usage_" + item.RecipeNo))
                {
                    //查询药品用法是否有关联处置，有则默认开一条处置单
                    var treatGrpc = await _grpcMasterDataClient.GetTreatByUsageAsync(
                        new GetTreatByUsageCodeRequest()
                        { UsageCode = item.UsageCode });
                    if (!string.IsNullOrEmpty(treatGrpc.TreatCode))
                    {
                        bool isAdditionalPrice = false;
                        var qty = item.UsageCode == _configuration["UsageIntravenousDripCode"] &&
                                  (item.FrequencyCode.ToUpper() is "BID" or "TID")
                            ? item.LongDays
                            : item.FrequencyTimes * item.LongDays;
                        var amount = decimal.Parse(treatGrpc.Price.ToString("f3")) * qty;
                        if (isChildren && treatGrpc.Additional)
                        {
                            amount = decimal.Parse(treatGrpc.OtherPrice.ToString("f3")) * qty;
                            isAdditionalPrice = true;
                        }

                        DoctorsAdvice treatDoctorsAdvice = new DoctorsAdvice(
                            id: GuidGenerator.Create(),
                            detailId: await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice),
                                DETAILID),
                            platformType: advice.PlatformType,
                            piid: advice.PIID,
                            patientId: advice.PatientId,
                            patientName: advice.PatientName,
                            code: treatGrpc.TreatCode,
                            name: treatGrpc.TreatName,
                            categoryCode: treatGrpc.DictionaryCode,
                            categoryName: treatGrpc.DictionaryName,
                            isBackTracking: false,
                            prescriptionNo: "", //TODO 目前没有 
                            recipeNo: "usage_" + item.RecipeNo,
                            recipeGroupNo: 1,
                            applyTime: DateTime.Now,
                            applyDeptCode: advice.ApplyDeptCode,
                            applyDeptName: advice.ApplyDeptName,
                            applyDoctorCode: advice.ApplyDoctorCode,
                            applyDoctorName: advice.ApplyDoctorName,
                            traineeCode: advice.TraineeCode,
                            traineeName: advice.TraineeName,
                            payTypeCode: "", //TODO 目前没有
                            payType: ERecipePayType.Self,
                            price: decimal.Parse(treatGrpc.Price.ToString("f3")), //project.Price,
                            unit: treatGrpc.Unit,
                            amount: amount,
                            insuranceCode: "", //TODO 目前没有
                            insuranceType: EInsuranceCatalog.Self,
                            isChronicDisease: advice.IsChronicDisease,
                            isRecipePrinted: false,
                            hisOrderNo: "",
                            diagnosis: advice.Diagnosis,
                            execDeptCode: entryInfo.ExecDeptCode,
                            execDeptName: entryInfo.ExecDeptName,
                            positionCode: "",
                            positionName: "",
                            remarks: "",
                            chargeCode: chargeCode,
                            chargeName: chargeName,
                            prescribeTypeCode: item.PrescribeTypeCode,
                            prescribeTypeName: item.PrescribeTypeName,
                            startTime: null,
                            endTime: null,
                            recieveQty: qty,
                            recieveUnit: treatGrpc.Unit,//item.RecieveUnit,
                            pyCode: treatGrpc.PyCode,
                            wbCode: treatGrpc.WbCode,
                            itemType: EDoctorsAdviceItemType.Treat,
                            isAdditionalPrice: isAdditionalPrice);
                        treatDoctorsAdvice.MeducalInsuranceCode = treatGrpc.MeducalInsuranceCode;
                        treatDoctorsAdvice.YBInneCode = treatGrpc.YBInneCode;

                        Treat treat = new Treat(GuidGenerator.Create())
                        {
                            FeeTypeMainCode = treatGrpc.FrequencyCode,
                            FeeTypeSubCode = treatGrpc.FeeTypeSubCode,
                            FrequencyCode = "",
                            OtherPrice = decimal.Parse(treatGrpc.OtherPrice.ToString("f3")),
                            Additional = treatGrpc.Additional,
                            Specification = treatGrpc.Specification,
                            ProjectMerge = treatGrpc.ProjectMerge,
                            DoctorsAdviceId = treatDoctorsAdvice.Id,
                            FrequencyName = "",
                            LongDays = 1,
                            ProjectType = treatGrpc.CategoryCode,
                            ProjectName = treatGrpc.CategoryName,
                            TreatId = treatGrpc.Id,
                            AdditionalItemsId = newDoctorsAdvice.Id,
                            AdditionalItemsType = EAdditionalItemType.UsageAdditional
                        };
                        treatDoctorsAdvice.Additional = 1;

                        treats.Add(treat);
                        list.Add(treatDoctorsAdvice);
                    }
                }

                //药品是否需要开皮试附加处置
                if (entryInfo.SkinTestSignChoseResult.HasValue &&
                    entryInfo.SkinTestSignChoseResult == ESkinTestSignChoseResult.Yes &&
                    list.All(a => a.RecipeNo != "skin_" + item.RecipeNo))
                {
                    var treatResponse = await _grpcMasterDataClient.GetTreatProjectByCodeAsync(
                        new GetTreatProjectByCodeRequest
                        { Code = _configuration["SkinTreatCode"] });
                    var treatByCode = treatResponse.TreatProject;
                    if (treatByCode != null)
                    {
                        bool isAdditionalPrice = false;
                        var amount = decimal.Parse(treatByCode.Price.ToString("f3")) * 1;
                        if (isChildren && treatByCode.Additional)
                        {
                            amount = decimal.Parse(treatByCode.OtherPrice.ToString("f3")) * 1;
                            isAdditionalPrice = true;
                        }

                        var treatDoctorsAdvice = new DoctorsAdvice(
                            id: GuidGenerator.Create(),
                            detailId: await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice),
                                DETAILID),
                            platformType: advice.PlatformType,
                            piid: advice.PIID,
                            patientId: advice.PatientId,
                            patientName: advice.PatientName,
                            code: treatByCode.TreatCode,
                            name: treatByCode.TreatName,
                            categoryCode: treatByCode.DictionaryCode,
                            categoryName: treatByCode.DictionaryName,
                            isBackTracking: false,
                            prescriptionNo: "", //TODO 目前没有 
                            recipeNo: "skin_" + item.RecipeNo,
                            recipeGroupNo: 1,
                            applyTime: DateTime.Now,
                            applyDeptCode: advice.ApplyDeptCode,
                            applyDeptName: advice.ApplyDeptName,
                            applyDoctorCode: advice.ApplyDoctorCode,
                            applyDoctorName: advice.ApplyDoctorName,
                            traineeCode: advice.TraineeCode,
                            traineeName: advice.TraineeName,
                            payTypeCode: "", //TODO 目前没有
                            payType: ERecipePayType.Self, //TODO 目前没有
                            price: decimal.Parse(treatByCode.Price.ToString("f3")), //project.Price,
                            unit: treatByCode.Unit,
                            amount: amount,
                            insuranceCode: "", //TODO 目前没有
                            insuranceType: EInsuranceCatalog.Self,
                            isChronicDisease: advice.IsChronicDisease,
                            isRecipePrinted: false,
                            hisOrderNo: "",
                            diagnosis: advice.Diagnosis,
                            execDeptCode: entryInfo.ExecDeptCode,
                            execDeptName: entryInfo.ExecDeptName,
                            positionCode: "",
                            positionName: "",
                            remarks: "",
                            chargeCode: chargeCode,
                            chargeName: chargeName,
                            prescribeTypeCode: item.PrescribeTypeCode,
                            prescribeTypeName: item.PrescribeTypeName,
                            startTime: null,
                            endTime: null,
                            recieveQty: 1,
                            recieveUnit: treatByCode.Unit,
                            pyCode: treatByCode.PyCode,
                            wbCode: treatByCode.WbCode,
                            itemType: EDoctorsAdviceItemType.Treat, isAdditionalPrice: isAdditionalPrice);
                        treatDoctorsAdvice.MeducalInsuranceCode = treatByCode.MeducalInsuranceCode;
                        treatDoctorsAdvice.YBInneCode = treatByCode.YBInneCode;

                        var treat = new Treat(GuidGenerator.Create())
                        {
                            #region 添加诊疗

                            FeeTypeMainCode = treatByCode.FrequencyCode, //TODO 目前没有
                            FeeTypeSubCode = treatByCode.FeeTypeSubCode, //TODO 目前没有
                            FrequencyCode = "",
                            OtherPrice = decimal.Parse(treatByCode.OtherPrice.ToString("f3")),
                            Additional = treatByCode.Additional,
                            Specification = treatByCode.Specification,
                            ProjectMerge = treatByCode.ProjectMerge,
                            DoctorsAdviceId = treatDoctorsAdvice.Id,
                            FrequencyName = "",
                            LongDays = 1,
                            ProjectType = treatByCode.CategoryCode, //TODO
                            ProjectName = treatByCode.CategoryName, //TODO
                            TreatId = treatByCode.Id,
                            AdditionalItemsId = newDoctorsAdvice.Id,
                            AdditionalItemsType = EAdditionalItemType.SkinAdditional

                            #endregion
                        };
                        treatDoctorsAdvice.Additional = 1;
                        treats.Add(treat);
                        list.Add(treatDoctorsAdvice);
                    }
                }

                if (item.UsageCode == _configuration["UsageIntravenousDripCode"] &&
                    (item.FrequencyCode.ToUpper() is "BID" or "TID") &&
                    list.All(a => a.RecipeNo != "frequency_" + item.RecipeNo))
                {
                    var treatResponse = await _grpcMasterDataClient.GetTreatProjectByCodeAsync(
                        new GetTreatProjectByCodeRequest
                        { Code = _configuration["ContinuousIntravenousInfusionCode"] });
                    var treatFrequency = treatResponse.TreatProject;
                    if (treatFrequency != null)
                    {
                        bool isAdditionalPrice = false;
                        var recieveQty = (item.FrequencyCode.ToUpper() is "BID" ? 1 : 2) * item.LongDays;
                        var amount = decimal.Parse(treatFrequency.Price.ToString("f3")) * recieveQty;
                        if (isChildren && treatFrequency.Additional)
                        {
                            amount = decimal.Parse(treatFrequency.OtherPrice.ToString("f3")) * recieveQty;
                            isAdditionalPrice = true;
                        }

                        var treatDoctorsAdvice = new DoctorsAdvice(
                            id: GuidGenerator.Create(),
                            detailId: await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice),
                                DETAILID),
                            platformType: advice.PlatformType,
                            piid: advice.PIID,
                            patientId: advice.PatientId,
                            patientName: advice.PatientName,
                            code: treatFrequency.TreatCode,
                            name: treatFrequency.TreatName,
                            categoryCode: treatFrequency.DictionaryCode,
                            categoryName: treatFrequency.DictionaryName,
                            isBackTracking: false,
                            prescriptionNo: "", //TODO 目前没有 
                            recipeNo: "frequency_" + item.RecipeNo,
                            recipeGroupNo: 1,
                            applyTime: DateTime.Now,
                            applyDeptCode: advice.ApplyDeptCode,
                            applyDeptName: advice.ApplyDeptName,
                            applyDoctorCode: advice.ApplyDoctorCode,
                            applyDoctorName: advice.ApplyDoctorName,
                            traineeCode: advice.TraineeCode,
                            traineeName: advice.TraineeName,
                            payTypeCode: "", //TODO 目前没有
                            payType: ERecipePayType.Self, //TODO 目前没有
                            price: decimal.Parse(treatFrequency.Price.ToString("f3")), //project.Price,
                            unit: treatFrequency.Unit,
                            amount: amount,
                            insuranceCode: "", //TODO 目前没有
                            insuranceType: EInsuranceCatalog.Self,
                            isChronicDisease: advice.IsChronicDisease,
                            isRecipePrinted: false,
                            hisOrderNo: "",
                            diagnosis: advice.Diagnosis,
                            execDeptCode: entryInfo.ExecDeptCode,
                            execDeptName: entryInfo.ExecDeptName,
                            positionCode: "",
                            positionName: "",
                            remarks: "",
                            chargeCode: chargeCode,
                            chargeName: chargeName,
                            prescribeTypeCode: item.PrescribeTypeCode,
                            prescribeTypeName: item.PrescribeTypeName,
                            startTime: null,
                            endTime: null,
                            recieveQty: recieveQty,
                            recieveUnit: treatFrequency.Unit,
                            pyCode: treatFrequency.PyCode,
                            wbCode: treatFrequency.WbCode,
                            itemType: EDoctorsAdviceItemType.Treat, isAdditionalPrice: isAdditionalPrice);
                        treatDoctorsAdvice.MeducalInsuranceCode = treatFrequency.MeducalInsuranceCode;
                        treatDoctorsAdvice.YBInneCode = treatFrequency.YBInneCode;

                        var treat = new Treat(GuidGenerator.Create())
                        {
                            #region 添加诊疗

                            FeeTypeMainCode = treatFrequency.FrequencyCode, //TODO 目前没有
                            FeeTypeSubCode = treatFrequency.FeeTypeSubCode, //TODO 目前没有
                            FrequencyCode = "",
                            OtherPrice = decimal.Parse(treatFrequency.OtherPrice.ToString("f3")),
                            Additional = treatFrequency.Additional,
                            Specification = treatFrequency.Specification,
                            ProjectMerge = treatFrequency.ProjectMerge,
                            DoctorsAdviceId = treatDoctorsAdvice.Id,
                            FrequencyName = "",
                            LongDays = 1,
                            ProjectType = treatFrequency.CategoryCode, //TODO
                            ProjectName = treatFrequency.CategoryName, //TODO
                            TreatId = treatFrequency.Id,
                            AdditionalItemsId = newDoctorsAdvice.Id,
                            AdditionalItemsType = EAdditionalItemType.FrequencyAdditional

                            #endregion
                        };
                        treatDoctorsAdvice.Additional = 1;
                        treats.Add(treat);
                        list.Add(treatDoctorsAdvice);
                    }
                }
            }
        }

        await SetMealRecipeNoAsync(list, doctorsAdvices);

        var mtm = new List<MedicalTechnologyMap>();

        if (treats.Any())
        {
            foreach (var item in treats)
            {
                mtm.Add(new MedicalTechnologyMap(item.Id, EDoctorsAdviceItemType.Treat));
            }

            await _treatRepository.InsertManyAsync(treats);
        }

        if (pacses.Any())
        {
            var request = new ExamCodeRequest();
            request.Code.AddRange(pacses.Select(s => s.ProjectCode));
            ExamTargetsReponse examtargets = await _grpcMasterDataClient.GetExamTargetsByCodeAsync(request);

            var pacsItems = new List<PacsItem>();

            foreach (var item in pacses)
            {
                var targets = examtargets.Targets.Where(w => w.ProjectCode == item.ProjectCode);

                var exam = doctorsAdvices.Find(x => x.CategoryCode == "Exam" && x.Id == item.DoctorsAdviceId);
                exam.Amount = (decimal)targets.Sum(s => s.Price * s.Qty);

                foreach (var target in targets)
                {
                    GetTreatByCodeRequest getTreatProjectByCodeRequest = new GetTreatByCodeRequest() { Code = target.TargetCode };
                    GetTreatByCodeResponse treatRsp = await _grpcMasterDataClient.GetTreatByCodeAsync(getTreatProjectByCodeRequest);

                    var entity = new PacsItem(
                        id: GuidGenerator.Create(),
                        targetCode: target.TargetCode,
                        targetName: target.TargetName,
                        targetUnit: target.TargetUnit,
                        price: (decimal)target.Price,
                        qty: (decimal)target.Qty,
                        insuranceCode: "", //TODO
                        insuranceType: (EInsuranceCatalog)target.InsuranceType,
                        projectCode: target.ProjectCode,
                        otherPrice: (decimal)target.OtherPrice,
                        specification: target.Specification,
                        sort: target.Sort,
                        pyCode: target.PyCode,
                        wbCode: target.WbCode,
                        specialFlag: target.SpecialFlag,
                        isActive: target.IsActive,
                        projectType: target.ProjectType,
                        projectMerge: target.ProjectMerge,
                        item.Id);
                    entity.MeducalInsuranceCode = treatRsp?.MeducalInsuranceCode;
                    entity.YBInneCode = treatRsp?.YBInneCode;

                    mtm.Add(new MedicalTechnologyMap(entity.Id, EDoctorsAdviceItemType.Pacs));
                    pacsItems.Add(entity);
                }
            }
            var pacsDoctorsAdviceEntities = doctorsAdvices.Where(x => x.ItemType == EDoctorsAdviceItemType.Pacs).ToList();
            if (pacsDoctorsAdviceEntities.Any())
            {
                var docadvi = pacsDoctorsAdviceEntities.First();

                //查询4小时内重复的历史数据，按0元开单
                DateTime before4HourTime = DateTime.Now.AddHours(-4);
                //查询出四小时之前的医嘱
                var daList = await _doctorsAdviceRepository.GetListAsync(c => c.PIID == docadvi.PIID && c.ItemType == EDoctorsAdviceItemType.Pacs && c.PlatformType == 0 && c.Status == ERecipeStatus.Saved && c.ApplyTime >= before4HourTime);
                var daIds = daList.Select(c => c.Id);
                var pacsIds = (await _pacsRepository.GetListAsync(c => daIds.Contains(c.DoctorsAdviceId))).Select(c => c.Id);
                var itemList = await _pacsItemRepository.GetListAsync(c => pacsIds.Contains(c.PacsId));
                itemList.AddRange(pacsItems);

                int pid = int.Parse(docadvi.PatientId);
                CheckPacsXmcfRequestDto checkLisXmcfRequestDto = new CheckPacsXmcfRequestDto()
                {
                    mzzy = 1,
                    brid = pid,
                };
                checkLisXmcfRequestDto.xmlist = new List<RequestXmlistItem>();
                if (itemList.Any())
                {
                    foreach (var item in itemList)
                    {
                        checkLisXmcfRequestDto.xmlist.Add(new RequestXmlistItem()
                        {
                            jlwyz = item.Id.ToString(),
                            ztxh = item.ProjectCode,
                            fydj = item.Price,
                            fymc = item.TargetName,
                            fysl = (int)item.Qty,
                            fyxh = item.TargetCode
                        });
                    }
                }

                // 需要重新计算价格 的 DA 医嘱 和处置类型 
                List<AddPacsHisResponse> hisResponses = new List<AddPacsHisResponse>();
                try
                {
                    CheckPacsXmcfResponseDto checkPacsXmcfResponseDto = await _hisService.CheckPacsXmcfAsync(checkLisXmcfRequestDto);
                    if (checkPacsXmcfResponseDto != null && checkPacsXmcfResponseDto.Data != null)
                    {
                        var xmlist = checkPacsXmcfResponseDto.Data.xmlist;

                        foreach (var item in xmlist)
                        {
                            AddPacsHisResponse entity = new AddPacsHisResponse()
                            {
                                PacsItemId = Guid.Parse(item.jlwyz),
                                NewTargetCode = item.newfyxh.ToString(),
                                NewTargetName = item.newfymc,
                                NewPrice = item.newfydj,
                                NewQty = item.newfysl,
                                ProjectCode = item.ztxh.ToString(),
                                Qty = item.fysl,
                                Price = item.fydj,
                                TargetName = item.fymc,
                                TargetCode = item.fyxh.ToString()
                            };
                            var add = true;
                            switch (item.czff)
                            {
                                case "0元开单":
                                    entity.CzffEnum = CzffEnum.ZeroAllow;
                                    // 将当前这一单置为0 
                                    foreach (var pacItem in pacsItems)
                                    {
                                        if (pacItem.Id.ToString() == item.jlwyz)
                                        {
                                            // 标记这一单为0元开单 
                                            pacItem.Price = 0;
                                            foreach (var pac in pacses)
                                            {
                                                if (pacItem.PacsId == pac.Id)
                                                {
                                                    entity.DoctorsAdviceId = pac.DoctorsAdviceId;
                                                    foreach (var doctorsAdvice in pacsDoctorsAdviceEntities)
                                                    {
                                                        if (doctorsAdvice.Id == pac.DoctorsAdviceId)
                                                        {
                                                            entity.ProjectName = doctorsAdvice.Name;
                                                            doctorsAdvice.Remarks += pacItem.TargetName + "当日4小时内重复，按0元开单; ";
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                        if (pacsItems.Count() - 1 == pacsItems.IndexOf(pacItem))
                                            add = false;
                                    }
                                    if (add)
                                        hisResponses.Add(entity);
                                    break;

                                case "不允许开单":
                                    entity.CzffEnum = CzffEnum.NotAllow;
                                    foreach (var pacItem in pacsItems)
                                    {
                                        if (pacItem.Id.ToString() == item.jlwyz)
                                        {
                                            foreach (var pac in pacses)
                                            {
                                                if (pacItem.PacsId == pac.Id)
                                                {
                                                    entity.DoctorsAdviceId = pac.DoctorsAdviceId;
                                                    foreach (var doctorsAdvice in pacsDoctorsAdviceEntities)
                                                    {
                                                        if (doctorsAdvice.Id == pac.DoctorsAdviceId)
                                                        {
                                                            entity.ProjectName = doctorsAdvice.Name;
                                                            doctorsAdvice.Remarks += pacItem.TargetName + "不允许开单; ";
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                            //删除当前记录提示不允许开单
                                            pacsItems.Remove(pacItem);
                                            break;
                                        }
                                        if (pacsItems.Count() - 1 == pacsItems.IndexOf(pacItem))
                                            add = false;
                                    }
                                    if (add)
                                        hisResponses.Add(entity);
                                    break;

                                case "项目置换":
                                    entity.CzffEnum = CzffEnum.Replacement;
                                    foreach (var pacItem in pacsItems)
                                    {
                                        if (pacItem.Id.ToString() == item.jlwyz)
                                        {
                                            //entity.TargetCode = pacItem.TargetCode;
                                            //entity.TargetName = pacItem.TargetName;
                                            //entity.Price = pacItem.Price;
                                            //entity.Qty = pacItem.Qty;
                                            //entity.PacsItemId = pacItem.PacsId;

                                            //进行项目置换  
                                            pacItem.Price = entity.NewPrice;
                                            pacItem.Qty = entity.NewQty;
                                            pacItem.TargetName = entity.NewTargetName;
                                            pacItem.TargetCode = entity.NewTargetCode;

                                            foreach (var pac in pacses)
                                            {
                                                if (pacItem.PacsId == pac.Id)
                                                {
                                                    entity.DoctorsAdviceId = pac.DoctorsAdviceId;
                                                    foreach (var doctorsAdvice in pacsDoctorsAdviceEntities)
                                                    {
                                                        if (doctorsAdvice.Id == pac.DoctorsAdviceId)
                                                        {
                                                            entity.ProjectName = doctorsAdvice.Name;
                                                            doctorsAdvice.Remarks += pacItem.TargetName + "项目置换成" + entity.NewTargetName + "; ";
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        //最后一条都没找到的话丢弃 表示不是本次添加的数据
                                        if (pacsItems.Count() - 1 == pacsItems.IndexOf(pacItem))
                                            add = false;
                                    }
                                    if (add)
                                        hisResponses.Add(entity);
                                    break;

                                default: break;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    Oh.Error("His 接口未正常返回数据！");
                }

                // 重新计算project 价格 
                foreach (var item in hisResponses)
                {
                    foreach (var doctorsAdvice in pacsDoctorsAdviceEntities)
                    {
                        if (doctorsAdvice.Id == item.DoctorsAdviceId)
                        {
                            //重新计算价格
                            var pacs = pacses.Where(c => c.DoctorsAdviceId == doctorsAdvice.Id).Select(c => c.Id);
                            doctorsAdvice.Amount = pacsItems.Where(c => pacs.Contains(c.PacsId)).Sum(s => s.Price * s.Qty) * doctorsAdvice.RecieveQty;
                            break;
                        }
                    }
                }
            }

            await _pacsItemRepository.InsertManyAsync(pacsItems);
            await _pacsRepository.InsertManyAsync(pacses);
        }

        if (lises.Any())
        {
            var request = new LabCodeRequest();
            request.Code.AddRange(lises.Select(s => s.ProjectCode));
            var labtargets = await _grpcMasterDataClient.GetLabTargetsByCodeAsync(request);

            var lisItems = new List<LisItem>();

            foreach (var item in lises)
            {
                var targets = labtargets.Targets.Where(w => w.ProjectCode == item.ProjectCode);
                // 重新计算价格
                var lab = doctorsAdvices.Find(x => x.CategoryCode == "Lab" && x.Id == item.DoctorsAdviceId);
                lab.Price = (decimal)targets.Sum(s => s.Price * s.Qty);
                lab.Amount = lab.Price * lab.RecieveQty;

                foreach (var target in targets)
                {
                    GetTreatByCodeRequest getTreatProjectByCodeRequest = new GetTreatByCodeRequest() { Code = target.TargetCode };
                    GetTreatByCodeResponse treatRsp = await _grpcMasterDataClient.GetTreatByCodeAsync(getTreatProjectByCodeRequest);

                    var entity = new LisItem(
                        id: GuidGenerator.Create(),
                        targetCode: target.TargetCode,
                        targetName: target.TargetName,
                        targetUnit: target.TargetUnit,
                        price: (decimal)target.Price,
                        qty: (decimal)target.Qty,
                        insuranceCode: "", //TODO
                        insuranceType: (EInsuranceCatalog)target.InsuranceType,
                        projectCode: target.ProjectCode,
                        otherPrice: 0,
                        specification: "", //TODO
                        sort: target.Sort,
                        pyCode: target.PyCode,
                        wbCode: target.WbCode,
                        specialFlag: "", //TODO
                        isActive: target.IsActive,
                        projectType: target.ProjectType,
                        projectMerge: target.ProjectMerge,
                        item.Id);

                    entity.MeducalInsuranceCode = treatRsp?.MeducalInsuranceCode;
                    entity.YBInneCode = treatRsp?.YBInneCode;

                    mtm.Add(new MedicalTechnologyMap(entity.Id, EDoctorsAdviceItemType.Lis));
                    lisItems.Add(entity);
                }
            }

            await _lisItemRepository.InsertManyAsync(lisItems);
            await _lisRepository.InsertManyAsync(lises);
        }

        if (mtm.Any()) await _medicalTechnologyMapRepository.InsertManyAsync(mtm);
        if (prescribes.Any()) await _prescribeRepository.InsertManyAsync(prescribes);

        var maps = new List<DoctorsAdviceMap>();
        doctorsAdvices.ForEach(x => maps.Add(new DoctorsAdviceMap(x.Id)));
        if (maps.Any()) await _doctorsAdviceMapRepository.InsertManyAsync(maps);
        AdmissionRecordDto admissionRecordDto = await task;
        if (admissionRecordDto != null)
        {
            foreach (var item in doctorsAdvices)
            {
                item.AreaCode = admissionRecordDto.AreaCode;
                if (item.CategoryCode == "Entrust")
                {
                    await UpdatePatientInfoAsync(item.Name, item.PIID);
                }
            }
        }
        await _doctorsAdviceRepository.InsertManyAsync(doctorsAdvices);

        return true;
    }

    /// <summary>
    /// 设置套餐医嘱号，医嘱子号
    /// </summary>
    /// <param name="list"></param>
    /// <param name="doctorsAdvices"></param>
    /// <returns></returns>
    private async Task SetMealRecipeNoAsync(List<DoctorsAdvice> list, List<DoctorsAdvice> doctorsAdvices)
    {
        //设置医嘱号和子号 
        var adviceGroup = list.GroupBy(g => g.RecipeNo);
        foreach (var group in adviceGroup)
        {
            int counter = 1;
            var recipeNo = "";
            foreach (var item in group)
            {
                if (counter == 1)
                {
                    recipeNo = (await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "RecipeNo"))
                        .ToString();
                }

                item.RecipeNo = recipeNo;
                item.RecipeGroupNo = counter++;
                doctorsAdvices.Add(item);
            }
        }
    }

    #region 构建套餐对象

    /// <summary>
    /// 添加套餐检验
    /// </summary>
    /// <param name="lises"></param>
    /// <param name="item"></param>
    /// <param name="newDoctorsAdvice"></param>  
    private void AddSetMealLis(List<Lis> lises, PackageProject item, DoctorsAdvice newDoctorsAdvice)
    {
        var project = item.Project;

        newDoctorsAdvice.BuildSetMeal(
            itemType: EDoctorsAdviceItemType.Lis,
            amount: project.Price, //套餐的检验没有子项所以总价的单价
            recipeNo: item.RecipeNo.ToString(), //DOTO 临时用置空，等下处理
            recipeGroupNo: item.RecipeGroupNo, //DOTO  等下处理  
            positionCode: item.PositionCode,
            positionName: item.PositionName,
            recieveQty: 1,
            recieveUnit: "");

        lises.Add(new Lis(GuidGenerator.Create())
        {
            #region 添加检验

            CatalogCode = project.LabProp.CatalogCode,
            CatalogName = project.LabProp.CatalogName,
            ClinicalSymptom = "", //TODO 目前没有
            ContainerCode = item.ContainerCode,
            ContainerColor = "",
            ContainerName = item.ContainerName,
            DoctorsAdviceId = newDoctorsAdvice.Id,
            IsBedSide = false, //TODO 目前没有
            IsEmergency = false, //TODO 目前没有
            LisItems = new List<LisItem>(),
            ReportDoctorCode = "", //TODO 目前没有
            HasReportName = false, //TODO 目前没有
            ReportDoctorName = "", //TODO 目前没有
            ReportTime = null, //TODO 目前没有
            SpecimenCode = item.SpecimenCode,
            SpecimenName = item.SpecimenName,
            SpecimenCollectDatetime = null, //TODO 目前没有
            SpecimenDescription = "", //TODO 目前没有
            SpecimenPartCode = item.PartCode,
            SpecimenPartName = item.PartName,
            SpecimenReceivedDatetime = null, //TODO 目前没有
            ProjectCode = project.Code,
            AddCard = project.LabProp.AddCard,
            GuideCode = project.LabProp.GuideCode,
            GuideName = project.LabProp.GuideName,
            GuideCatelogName = project.LabProp.GuideCatelogName,
            #endregion
        });
    }

    /// <summary>
    /// 添加套餐检查 
    /// </summary>
    /// <param name="pacses"></param>
    /// <param name="item"></param>
    /// <param name="newDoctorsAdvice"></param>
    /// <param name="patientInfo"></param> 
    private async Task AddSetMealPacsAsync(List<Pacs> pacses, PackageProject item, DoctorsAdvice newDoctorsAdvice, PatientInfoDto patientInfo)
    {
        var project = item.Project;
        newDoctorsAdvice.BuildSetMeal(
            itemType: EDoctorsAdviceItemType.Pacs,
            amount: project.Price, //检查没有子项，所以总价=单价
            recipeNo: item.RecipeNo.ToString(), //DOTO 临时用置空，等下处理
            recipeGroupNo: item.RecipeGroupNo, //DOTO  等下处理  
            positionCode: item.PositionCode,
            positionName: item.PositionName,
            recieveQty: 1,
            recieveUnit: "");

        pacses.Add(new Pacs(GuidGenerator.Create())
        {
            #region 添加检查

            CatalogCode = project.ExamProp.CatalogCode,
            CatalogName = project.ExamProp.CatalogName,
            FirstCatalogCode = project.ExamProp.FirstCatalogCode,
            FirstCatalogName = project.ExamProp.FirstCatalogName,
            CatalogDisplayName = project.ExamProp.CatalogName, //TODO 目前没有
            ClinicalSymptom = "", //TODO 目前没有
            DoctorsAdviceId = newDoctorsAdvice.Id,
            HasReport = false, //TODO 目前没有
            IsBedSide = false, //TODO 目前没有
            IsEmergency = false,
            MedicalHistory = "", //TODO 目前没有 
            PacsItems = new List<PacsItem>(),
            PartCode = item.PartCode,
            PartName = item.PartName,
            ProjectCode = project.Code,
            ReportDoctorCode = "", //TODO 目前没有
            ReportDoctorName = "", //TODO 目前没有
            ReportTime = null, //TODO 目前没有 
            AddCard = project.ExamProp.AddCard,
            GuideCode = project.ExamProp.GuideCode,
            GuideName = project.ExamProp.GuideName,
            ExamTitle = project.ExamProp.ExamTitle,
            ReservationPlace = project.ExamProp.ReservationPlace,
            TemplateId = project.ExamProp.TemplateId
            #endregion
        });

        //添加附加项
        string projectCode = newDoctorsAdvice.Code;
        ExamCodeRequest examCodeRequest = new ExamCodeRequest();
        examCodeRequest.Code.Add(projectCode);
        ExamAttachItemReponse examAttachItemReponse = await _grpcMasterDataClient.GetExamAttachItemsAsync(examCodeRequest);
        if (examAttachItemReponse.ExamAttachItems.Any())
        {
            AddPacsDto addPacsDto = new AddPacsDto();
            addPacsDto.Items = new List<PacsDto>();
            foreach (Pacs pacs in pacses)
            {
                PacsDto pacsDto = ObjectMapper.Map<Pacs, PacsDto>(pacs);
                pacsDto.Code = newDoctorsAdvice.Code;
                pacsDto.InsuranceCode = newDoctorsAdvice.InsuranceCode;
                pacsDto.InsuranceType = newDoctorsAdvice.InsuranceType;
                pacsDto.PayTypeCode = newDoctorsAdvice.PayTypeCode;
                pacsDto.PayType = newDoctorsAdvice.PayType;
                pacsDto.ApplyTime = newDoctorsAdvice.ApplyTime;
                pacsDto.ExecDeptCode = newDoctorsAdvice.ExecDeptCode;
                pacsDto.ExecDeptName = newDoctorsAdvice.ExecDeptName;
                pacsDto.PrescribeTypeCode = newDoctorsAdvice.PrescribeTypeCode;
                pacsDto.PrescribeTypeName = newDoctorsAdvice.PrescribeTypeName;
                pacsDto.StartTime = newDoctorsAdvice.StartTime;
                pacsDto.EndTime = newDoctorsAdvice.EndTime;
                addPacsDto.Items.Add(pacsDto);
            }
            addPacsDto.DoctorsAdvice = ObjectMapper.Map<DoctorsAdvice, ModifyDoctorsAdviceBaseDto>(newDoctorsAdvice);
            addPacsDto.PatientInfo = patientInfo;

            await AddPacsTreatAsync(addPacsDto, examAttachItemReponse);
            await AddPacsPrescribeAsync(addPacsDto, examAttachItemReponse);
        }
    }

    /// <summary>
    /// 添加套餐药品
    /// </summary>
    /// <param name="prescribes"></param>
    /// <param name="item"></param>
    /// <param name="medicines"></param>
    /// <param name="newDoctorsAdvice"></param> 
    /// <param name="immunizationRecord"></param>
    /// <param name="skinTestSignChoseResult"></param>  
    /// <param name="limitType"></param> 
    /// <param name="restrictedDrugs"></param>
    private async Task AddSetMealPrescribeAsync(
        List<Prescribe> prescribes,
        PackageProject item,
        List<GrpcMedicineModel> medicines,
        DoctorsAdvice newDoctorsAdvice,
        ImmunizationRecordDto immunizationRecord,
        ESkinTestSignChoseResult? skinTestSignChoseResult = null,
        ERestrictedDrugs? restrictedDrugs = ERestrictedDrugs.Default, int limitType = 0)
    {
        var project = item.Project;
        var medicine = medicines.FirstOrDefault(w => project.Code == w.Code && project.Specification == w.Specification && project.MedicineProp.FactoryCode == w.FactoryCode && project.MedicineProp.PharmacyCode == w.PharmacyCode);

        if (medicine == null || (decimal)medicine.Qty < newDoctorsAdvice.RecieveQty) Oh.Error($"药品【{project.Name}】库存不足！");

        decimal amount;
        amount = Convert.ToDecimal(medicine.Price) * item.RecieveQty;

        newDoctorsAdvice.BuildSetMeal(
            itemType: EDoctorsAdviceItemType.Prescribe,
            amount: amount, //TODO 待测试
            recipeNo: item.RecipeNo.ToString(), //DOTO 临时用置空，等下处理
            recipeGroupNo: item.RecipeGroupNo, //DOTO  等下处理  
            positionCode: "",
            positionName: "",
            recieveQty: item.RecieveQty,
            recieveUnit: item.RecieveUnit);
        newDoctorsAdvice.MeducalInsuranceCode = medicine.MedicalInsuranceCode;
        newDoctorsAdvice.YBInneCode = medicine.YBInneCode;
        newDoctorsAdvice.MeducalInsuranceName = medicine.MeducalInsuranceName;

        var newPrescribe = new Prescribe(GuidGenerator.Create())
        {
            #region 添加药品

            ActualDays = null, //TODO 目前没有   
            AntibioticPermission = medicine.AntibioticPermission, //TODO 目前没有
            BatchNo = medicine.BatchNo, //project.MedicineProp.BatchNo, //TODO 目前没有
            BigPackFactor = medicine.BigPackFactor, //project.MedicineProp.BigPackFactor,
            BigPackPrice = (decimal)medicine.BigPackPrice, //project.MedicineProp.BigPackPrice,
            BigPackUnit = medicine.BigPackUnit, //project.MedicineProp.BigPackUnit,
            DosageForm = medicine.DosageForm,
            DosageQty = (decimal)item.DosageQty, //item.DosageQty.HasValue ? item.DosageQty.Value : 0,
            DosageUnit = item.DosageUnit, // item.DosageUnit,
            ExpirDate = (DateTime?)null, //project.MedicineProp.ExpireDate,
            FactoryCode = medicine.FactoryCode, //project.MedicineProp.FactoryCode,
            FactoryName = medicine.FactoryName, //project.MedicineProp.FactoryName,
            FrequencyCode = item.FrequencyCode, //project.MedicineProp.FrequencyCode,
            FrequencyExecDayTimes = item.FrequencyExecDayTimes,
            DailyFrequency = item.DailyFrequency,
            FrequencyName = item.FrequencyName, //project.MedicineProp.FrequencyName,
            MedicalInsuranceCode = medicine.MedicalInsuranceCode,
            FrequencyTimes = item.FrequencyTimes,
            FrequencyUnit = item.FrequencyUnit,
            IsAdaptationDisease = null, //TODO 目前没有   
            IsAmendedMark = null, //TODO 目前没有   
            IsBindingTreat = null, //TODO 目前没有   
            IsFirstAid = medicine.IsFirstAid, //project.CanUseInFirstAid, //TODO 目前没有   
            IsOutDrug = false, // DOTO 目前没有
            IsSkinTest = item.IsSkinTest, //project.MedicineProp.IsSkinTest,
            LongDays = item.LongDays, // DOTO 目前没有
            MaterialPrice = 0, // DOTO 目前没有
            MedicineProperty = medicine.MedicineProperty, //project.MedicineProp.MedicineProperty,
            PharmacyCode = medicine.PharmacyCode, //project.MedicineProp.PharmacyCode,
            PharmacyName = medicine.PharmacyName, //project.MedicineProp.PharmacyName,
            PrescriptionPermission = medicine.PrescriptionPermission, // DOTO 目前没有
            QtyPerTimes = null, //TODO每次用量
            SkinTestResult = item.IsSkinTest, //TODO 目前没有    
            SmallPackFactor = medicine.SmallPackFactor, //project.MedicineProp.SmallPackFactor,
            SmallPackPrice = (decimal)medicine.SmallPackPrice, //project.MedicineProp.SmallPackPrice,
            SmallPackUnit = medicine.SmallPackUnit, //project.MedicineProp.SmallPackUnit,
            Speed = "", //TODO 目前没有    
            ToxicProperty = medicine.ToxicProperty, //project.MedicineProp.ToxicProperty, //TODO 目前没有   
            Unpack = (EMedicineUnPack)medicine.Unpack, //EMedicineUnPack.RoundByMinUnitAmount,
            UsageCode = item.UsageCode, //project.MedicineProp.UsageCode,
            UsageName = item.UsageName, //project.MedicineProp.UsageName,
            Specification = medicine.Specification, //project.Specification,
            DoctorsAdviceId = newDoctorsAdvice.Id,
            DefaultDosageQty = (decimal)medicine.DosageQty, //project.MedicineProp.DosageQty,
            DefaultDosageUnit = medicine.DosageUnit, //project.MedicineProp.DosageUnit,
            SkinTestSignChoseResult = skinTestSignChoseResult,
            LimitType = limitType,
            RestrictedDrugs = restrictedDrugs,
            MedicineId = project.SourceId,
            IsCriticalPrescription = item.IsCriticalPrescription,
            HisUnit = medicine.Unit,
            HisDosageUnit = medicine.DosageUnit,
            HisDosageQty = (decimal)medicine.DosageQty,
            #endregion
        };
        newPrescribe.SetHisDosageQty();

        //自定义一次剂量处理 TODO
        var dosageList = await _dosageAppService.GetAllCustomDosagesAsync();
        var dosage = dosageList.FirstOrDefault(w => w.Code == newDoctorsAdvice.Code);
        if (dosage != null)
        {
            var mydosageQty = dosage.GetHisDosageQty(newPrescribe.DosageQty, newPrescribe.DosageUnit);
            newPrescribe.CommitHisDosageQty = mydosageQty;
        }

        prescribes.Add(newPrescribe);

        //套餐添加疫苗接种记录
        if (immunizationRecord != null && medicine.ToxicLevel == 7)
        {
            var immunizationRecordEntity = new ImmunizationRecord(
                id: GuidGenerator.Create(),
                acupunctureManipulation: immunizationRecord.AcupunctureManipulation,
                times: immunizationRecord.Times,
                recordTime: DateTime.Now,
                confirmed: false,
                doctorAdviceId: newDoctorsAdvice.Id,
                medicineId: project.SourceId,
                patientId: newDoctorsAdvice.PatientId);
            await _iImmunizationRecordRepository.AddRecordAsync(immunizationRecordEntity);
        }
    }

    /// <summary>
    /// 添加套餐诊疗
    /// </summary>
    /// <param name="treats"></param>
    /// <param name="item"></param>
    /// <param name="newDoctorsAdvice"></param> 
    /// <param name="isChildren"></param>
    private async Task AddSetMealTreat(List<Treat> treats, PackageProject item, DoctorsAdvice newDoctorsAdvice,
        bool isChildren = false)
    {

        var project = item.Project;
        GetTreatByCodeRequest getTreatProjectByCodeRequest = new GetTreatByCodeRequest() { Code = project.Code };
        GetTreatByCodeResponse treatRsp = await _grpcMasterDataClient.GetTreatByCodeAsync(getTreatProjectByCodeRequest);
        var price = treatRsp != null ? decimal.Parse(treatRsp.Price.ToString("f3")) : project.Price;
        var amount =
            price *
            item.RecieveQty; //(Price * RecieveQty) + markUpPrice; 默认 recieveQty = 1 , markUpPrice = 0 
        bool isAdditionalPrice = false;

        //如果是儿童,并且有加收标记，那么用加售后的金额
        if (isChildren && project.Additional)
        {
            amount = project.OtherPrice * item.RecieveQty;
            isAdditionalPrice = true;
        }

        newDoctorsAdvice.BuildSetMeal(
            itemType: EDoctorsAdviceItemType.Treat,
            amount: amount,
            recipeNo: item.RecipeNo.ToString(), //DOTO 临时用置空，等下处理
            recipeGroupNo: item.RecipeGroupNo, //DOTO  等下处理  
            positionCode: "",
            positionName: "",
            recieveQty: item.RecieveQty,
            recieveUnit: item.RecieveUnit,
            isAdditionalPrice: isAdditionalPrice,
            price: price);

        newDoctorsAdvice.MeducalInsuranceCode = treatRsp?.MeducalInsuranceCode;
        newDoctorsAdvice.YBInneCode = treatRsp?.YBInneCode;

        Treat treat = new Treat(GuidGenerator.Create())
        {
            FeeTypeMainCode = "", //TODO 目前没有
            FeeTypeSubCode = "", //TODO 目前没有
            FrequencyCode = item.FrequencyCode,
            OtherPrice = project.OtherPrice,
            Additional = project.Additional,
            Specification = project.Specification,
            ProjectMerge = project.TreatProp?.ProjectMerge,
            DoctorsAdviceId = newDoctorsAdvice.Id,
            FrequencyName = "",
            LongDays = 1,
            ProjectType = project.TreatProp?.ProjectType,
            ProjectName = project.TreatProp?.ProjectName,
            TreatId = project.SourceId,
        };

        treats.Add(treat);
    }

    #endregion

    /// <summary>
    /// 新增同组药品信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<List<Guid>> AddGroupPrescribeAsync(AddGroupPrescribesDto model)
    {
        var codesCount = model.PrescribeItems.Select(s => s.Items).GroupBy(g => g.Code).Count();
        var itemsCount = model.PrescribeItems.Count();
        if (codesCount != itemsCount) Oh.Error("药品有重复，请检查药品和数量问题");

        var count = model.PrescribeItems.Select(s => s.Items)
            .GroupBy(g => new { g.UsageCode, g.LongDays, g.ExecDeptCode, g.FrequencyCode }).Count();
        if (count > 1) Oh.Error("药品同组异常，请查看同组的用药方法、天数、执行科室和频次是否一致");
        var recipeNo = (await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "RecipeNo")).ToString();
        int counter = 1;

        var ret = new List<Guid>();
        var closeSkin = false;
        var closeUsage = false;
        foreach (AddPrescribeDto item in model.PrescribeItems)
        {
            GetMedicineInvInfoRquest req = new GetMedicineInvInfoRquest()
            {
                Code = item.Items.Code,
                FactoryCode = item.Items.FactoryCode,
                Specification = item.Items.Specification,
                PharmacyCode = "1"
            };
            MedicineResponse resp = await _grpcMasterDataClient.GetMedicineInvInfoAsync(req);

            if (resp != null && resp.Medicine != null)
            {
                item.DoctorsAdvice.YBInneCode = resp.Medicine.YBInneCode;
                item.DoctorsAdvice.MeducalInsuranceCode = resp.Medicine.MedicalInsuranceCode;
                item.DoctorsAdvice.MeducalInsuranceName = resp.Medicine.MeducalInsuranceName;
            }

            item.Items.SetRecipeNo(recipeNo, counter);
            var res = await CreatePrescribeAsync(item, closeSkin: closeSkin, closeUsage: closeUsage, source: EAddPrescribeSource.Group);
            closeSkin = res.CloseSkin;
            closeUsage = res.CloseUsage;
            ret.Add(res.Id);
            counter += 1;
        }

        return ret;
    }

    /// <summary>
    /// 新增药方
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<Guid> AddPrescribeAsync(AddPrescribeDto model)
    {
        var prescribe = await CreatePrescribeAsync(model);
        return prescribe.Id;
    }

    /// <summary>
    /// 获取频次信息
    /// </summary>
    /// <returns></returns>
    private List<Frequency> GetAllFrequencies()
    {
        GetAllFrequenciesRequest request = new GetAllFrequenciesRequest();
        FrequenciesResponse response = _grpcMasterDataClient.GetAllFrequencies(request);
        List<Frequency> list = new List<Frequency>();
        foreach (GrpcFrequencyModel fre in response.Frequencies)
        {
            Frequency pf = new Frequency()
            {
                FrequencyCode = fre.FrequencyCode,
                FrequencyName = fre.FrequencyName,
                Unit = fre.Unit,
                Times = fre.Times,
                ExecuteDayTime = fre.ExecDayTimes
            };
            list.Add(pf);
        }

        return list;
    }

    /// <summary>
    /// 创建药品
    /// </summary>
    /// <param name="model"></param>
    /// <param name="closeSkin">关闭皮试附加项，默认是不关闭</param>
    /// <param name="closeUsage">关闭用法附加项，默认是不关闭</param>  
    /// <param name="source">添加的药品来源</param>
    /// <returns></returns>
    private async Task<PrescribeResponseDto> CreatePrescribeAsync(AddPrescribeDto model, bool closeSkin = false, bool closeUsage = false, EAddPrescribeSource source = EAddPrescribeSource.Single)
    {
        var id = GuidGenerator.Create();
        var prescribeId = GuidGenerator.Create();
        PrescribeDto pre = model.Items;

        List<Frequency> frequencies = GetAllFrequencies();
        if (frequencies.Exists(p => p.FrequencyCode == pre.FrequencyCode))
        {
            Frequency freq = frequencies.Find(w => w.FrequencyCode == pre.FrequencyCode);
            pre.FrequencyName = freq.FrequencyName;
            pre.FrequencyUnit = freq.Unit;
            pre.FrequencyTimes = freq.Times;
            pre.FrequencyExecDayTimes = freq.PreparedDayTime;
        }

        #region Create Prescribe

        var prescribe = new Prescribe(
            id: prescribeId,
            medicineId: model.Items.MedicineId,
            isOutDrug: pre.IsOutDrug,
            medicineProperty: pre.MedicineProperty,
            toxicProperty: pre.ToxicProperty,
            usageCode: pre.UsageCode,
            usageName: pre.UsageName,
            speed: pre.Speed,
            longDays: pre.LongDays,
            actualDays: pre.ActualDays,
            dosageForm: pre.DosageForm,
            dosageQty: pre.DosageQty,
            defaultDosageQty: pre.DefaultDosageQty,
            qtyPerTimes: pre.QtyPerTimes,
            dosageUnit: pre.DosageUnit,
            defaultDosageUnit: pre.DefaultDosageUnit,
            unpack: pre.Unpack,
            bigPackPrice: pre.BigPackPrice,
            bigPackFactor: pre.BigPackFactor,
            bigPackUnit: pre.BigPackUnit,
            smallPackPrice: pre.SmallPackPrice,
            smallPackUnit: pre.SmallPackUnit,
            smallPackFactor: pre.SmallPackFactor,
            frequencyCode: pre.FrequencyCode,
            frequencyName: pre.FrequencyName,
            medicalInsuranceCode: pre.MedicalInsuranceCode,
            frequencyTimes: pre.FrequencyTimes,
            frequencyUnit: pre.FrequencyUnit,
            frequencyExecDayTimes: pre.FrequencyExecDayTimes,
            dailyFrequency: pre.DailyFrequency,
            pharmacyCode: pre.PharmacyCode,
            pharmacyName: pre.PharmacyName,
            factoryName: pre.FactoryName,
            factoryCode: pre.FactoryCode,
            batchNo: pre.BatchNo,
            expirDate: pre.ExpirDate,
            isSkinTest: pre.IsSkinTest,
            skinTestResult: pre.SkinTestResult,
            skinTestSignChoseResult: pre.SkinTestSignChoseResult,
            materialPrice: pre.MaterialPrice,
            isBindingTreat: pre.IsBindingTreat,
            isAmendedMark: pre.IsAmendedMark,
            isAdaptationDisease: pre.IsAdaptationDisease,
            isFirstAid: pre.IsFirstAid,
            antibioticPermission: pre.AntibioticPermission,
            prescriptionPermission: pre.PrescriptionPermission,
            specification: pre.Specification,
            childrenPrice: pre.ChildrenPrice,
            fixPrice: pre.FixPrice,
            retPrice: pre.RetPrice,
            restrictedDrugs: pre.RestrictedDrugs,
            limitType: pre.LimitType,
            doctorsAdviceId: id,
            isCriticalPrescription: pre.IsCriticalPrescription,
            hisUnit: pre.HisUnit,
            hisDosageUnit: pre.HisDosageUnit,
            hisDosageQty: pre.HisDosageQty);

        prescribe.SetHisDosageQty();

        //自定义一次剂量处理 TODO
        var dosageList = await _dosageAppService.GetAllCustomDosagesAsync();
        var dosage = dosageList.FirstOrDefault(w => w.Code == pre.Code);
        if (dosage != null)
        {
            var mydosageQty = dosage.GetHisDosageQty(pre.DosageQty, pre.DosageUnit);
            prescribe.CommitHisDosageQty = mydosageQty;
        }

        #endregion

        _ = await _prescribeRepository.InsertAsync(prescribe);
        var sub = new ModifySubItemDto(
            code: pre.Code,
            name: pre.Name,
            unit: pre.Unit,
            price: pre.Price,
            amount: pre.GetAmount(), //TODO 待测试
            insuranceCode: pre.InsuranceCode,
            insuranceType: pre.InsuranceType,
            payTypeCode: pre.PayTypeCode,
            payType: pre.PayType,
            prescriptionNo: pre.PrescriptionNo,
            recipeGroupNo: pre.RecipeGroupNo,
            recipeNo: pre.RecipeNo,
            applyTime: pre.ApplyTime.HasValue ? pre.ApplyTime.Value : DateTime.Now,
            recipeCategoryCode: pre.CategoryCode,
            recipeCategoryName: pre.CategoryName,
            isBackTracking: pre.IsBackTracking,
            isRecipePrinted: pre.IsRecipePrinted,
            hisOrderNo: pre.HisOrderNo,
            positionCode: pre.PositionCode,
            positionName: pre.PositionName,
            execDeptCode: pre.ExecDeptCode,
            execDeptName: pre.ExecDeptName,
            remarks: pre.Remarks,
            chargeCode: pre.ChargeCode,
            chargeName: pre.ChargeName,
            prescribeTypeCode: pre.PrescribeTypeCode,
            prescribeTypeName: pre.PrescribeTypeName,
            startTime: pre.StartTime,
            endTime: pre.EndTime,
            recieveQty: pre.RecieveQty,
            recieveUnit: pre.RecieveUnit,
            pyCode: pre.PyCode,
            wbCode: pre.WbCode);

        //前端传过来的医嘱号
        var requestRecipeNo = pre.RecipeNo;

        //构建医嘱实体
        DoctorsAdvice entity = await BuildDoctorsAdviceAsync(model.DoctorsAdvice, id, sub, prescribe, source);
        var admissionRecordTask = _patientAppService.GetPatientInfoAsync(entity.PIID);
        entity.MeducalInsuranceCode = model.DoctorsAdvice.MeducalInsuranceCode;
        entity.YBInneCode = model.DoctorsAdvice.YBInneCode;
        entity.MeducalInsuranceName = model.DoctorsAdvice.MeducalInsuranceName;
        entity.PrescriptionFlag = model.DoctorsAdvice.PrescriptionFlag;
        entity.UpdateStatus(ERecipeStatus.Saved);
        entity.ItemType = EDoctorsAdviceItemType.Prescribe;

        entity.SetExtendedAttributes(
            pyCode: pre.PyCode,
            wbCode: pre.WbCode,
            scientificName: pre.ScientificName,
            alias: pre.Alias,
            aliasPyCode: pre.AliasPyCode,
            aliasWbCode: pre.AliasWbCode);

        //急诊暂时不处理，院前因为医嘱号用户可以指定，而子号默认出现多个医嘱子号为1
        if (entity.PlatformType == EPlatformType.PreHospital)
        {
            var das = await (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => w.RecipeNo == model.Items.RecipeNo)
                .Select(s => new { s.Id, s.RecipeNo, s.RecipeGroupNo }).ToListAsync();
            if (das.Any()) entity.RecipeGroupNo = das.Max(s => s.RecipeGroupNo) + 1; //获取最大的一条+1
        }

        //院前不需要做库存校验，他们自己带的药
        if (model.DoctorsAdvice.PlatformType == EPlatformType.EmergencyTreatment)
        {
            var drugStock = await _hospitalClientAppService.QueryHisDrugStockAsync(new DrugStockQueryRequest
            { QueryType = 2, QueryCode = entity.Code, Storage = int.Parse(prescribe.PharmacyCode) });
            if (drugStock.Count > 0)
            {
                //存储库存校验信息
                var drugstock =
                    ObjectMapper.Map<DrugStockQueryResponse, DrugStockQuery>(drugStock.FirstOrDefault());
                drugstock.SetDoctorsAdviceId(entity.Id);
                await _drugStockQueryRepository.InsertAsync(drugstock);
            }
        }
        int counter = 1;
        //处理可能有多个同组的医嘱子号问题
        var adviceList = await (await _doctorsAdviceRepository.GetQueryableAsync())
            .Where(w => w.RecipeNo == entity.RecipeNo)
            .OrderBy(o => o.CreationTime)
            .ToListAsync();
        if (adviceList.Any())
        {
            List<DoctorsAdvice> updateGroupAdvices = new();
            foreach (var item in adviceList)
            {
                if (item.RecipeGroupNo != counter)
                {
                    item.RecipeGroupNo = counter;
                    updateGroupAdvices.Add(item);
                }
                counter++;
            }
            if (updateGroupAdvices.Any()) await _doctorsAdviceRepository.UpdateManyAsync(updateGroupAdvices);
        }

        entity.RecipeGroupNo = counter;
        AdmissionRecordDto admissionRecordDto = await admissionRecordTask;
        if (admissionRecordDto != null)
        {
            entity.AreaCode = admissionRecordDto.AreaCode;
        }

        _ = await _doctorsAdviceRepository.InsertAsync(entity);
        _ = await _doctorsAdviceMapRepository.InsertAsync(new DoctorsAdviceMap(entity.Id)); //添加映射 

        await AddImmunizationRecordAsync(model, pre, entity); //添加疫苗针次记录

        //await AppendQuickStartAsync(model); 
        if (entity.PlatformType == EPlatformType.EmergencyTreatment)
        {
            var adviceIds = await (await _doctorsAdviceRepository.GetQueryableAsync())
                                .Where(w => w.RecipeNo == entity.RecipeNo)
                                .Select(s => s.Id)
                                .ToListAsync();
            var treatList = await (from t in (await _treatRepository.GetQueryableAsync())
                                   join a in (await _doctorsAdviceRepository.GetQueryableAsync())
                                   on t.DoctorsAdviceId equals a.Id
                                   where adviceIds.Contains(t.AdditionalItemsId)
                                   select new
                                   {
                                       a.Id,
                                       a.Code,
                                       a.Name,
                                       t.AdditionalItemsType,
                                       t.AdditionalItemsId
                                   }).ToListAsync();

            //添加附加项目
            if (!closeUsage)
            {
                //没有记录的时候需要添加
                if (!treatList.Any())
                {
                    //判断是否添加过一模一样的 
                    closeUsage = await AppendItemAsync(model, id);
                }
                else
                {
                    var request = new GetTreatByUsageCodeRequest() { UsageCode = model.Items.UsageCode };
                    //查询药品用法是否有关联处置，有则默认开一条处置单
                    var treat = await _grpcMasterDataClient.GetTreatByUsageAsync(request);
                    var existsAny = treatList.Where(w => w.Code == treat.TreatCode).Any();
                    if (!existsAny)
                    {
                        //判断是否添加过一模一样的 
                        closeUsage = await AppendItemAsync(model, id);
                    }
                }
            }

            var skinAdditionalAny = treatList.Where(w => w.AdditionalItemsType == EAdditionalItemType.SkinAdditional).Any();
            if (!skinAdditionalAny)
            {
                if (pre.SkinTestSignChoseResult is ESkinTestSignChoseResult.Yes && !closeSkin)
                {
                    //添加皮试附加项目
                    closeSkin = await AppendOtherItemAsync(model, id, _configuration["SkinTreatCode"], 1,
                        EAdditionalItemType.SkinAdditional);
                }
            }
        }

        return new PrescribeResponseDto()
        {
            Id = id,
            CloseUsage = closeUsage,
            CloseSkin = closeSkin
        };
    }

    /// <summary>
    /// 添加疫苗针次记录
    /// </summary>
    /// <param name="model"></param>
    /// <param name="pre"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    private async Task AddImmunizationRecordAsync(AddPrescribeDto model, PrescribeDto pre, DoctorsAdvice entity)
    {
        if (model.ImmunizationRecord == null) return;

        //如果有针次信息，这需要记录
        var ir = model.ImmunizationRecord;
        var toxic = await (await _toxicRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.MedicineId == pre.MedicineId);
        if (toxic != null && toxic.ToxicLevel.HasValue && toxic.ToxicLevel.Value == 7)
        {
            var immunizationRecord = new ImmunizationRecord(
                id: GuidGenerator.Create(),
                acupunctureManipulation: ir.AcupunctureManipulation,
                times: ir.Times,
                recordTime: DateTime.Now,
                confirmed: false,
                doctorAdviceId: entity.Id,
                pre.MedicineId,
                entity.PatientId);
            await _iImmunizationRecordRepository.AddRecordAsync(immunizationRecord);
        }
    }

    /// <summary>
    /// 添加附加处置项
    /// </summary>
    /// <param name="model"></param>
    /// <param name="id"></param>
    private async Task<bool> AppendItemAsync(AddPrescribeDto model, Guid id)
    {
        var pre = model.Items;
        //查询药品用法是否有关联处置，有则默认开一条处置单
        GrpcTreatProjectModel treat = await _grpcMasterDataClient.GetTreatByUsageAsync(new GetTreatByUsageCodeRequest()
        { UsageCode = pre.UsageCode });
        if (!string.IsNullOrWhiteSpace(treat.TreatCode))
        {
            var addTreatDto = new AddTreatDto()
            {
                PatientInfo = model.PatientInfo,
                DoctorsAdvice = model.DoctorsAdvice.CloneJson(),
                IsAddition = true,
                Items = new TreatDto()
                {
                    Additional = treat.Additional,
                    Code = treat.TreatCode,
                    Name = treat.TreatName,
                    Unit = treat.Unit,
                    Price = decimal.Parse(treat.Price.ToString("f3")),
                    OtherPrice = decimal.Parse(treat.OtherPrice.ToString("f3")),
                    Specification = treat.Specification,
                    FeeTypeMainCode = treat.FeeTypeMainCode,
                    FeeTypeSubCode = treat.FeeTypeSubCode,
                    InsuranceCode = pre.InsuranceCode,
                    InsuranceType = pre.InsuranceType,
                    PayTypeCode = pre.PayTypeCode,
                    PayType = pre.PayType,
                    ApplyTime = pre.ApplyTime,
                    CategoryCode = treat.DictionaryCode,
                    CategoryName = treat.DictionaryName,
                    ExecDeptCode = pre.ExecDeptCode, //treat.ExecDeptCode,
                    ExecDeptName = pre.ExecDeptName, //treat.ExecDeptName,
                    ChargeCode = treat.ChargeCode,
                    ChargeName = treat.ChargeName,
                    PrescribeTypeCode = pre.PrescribeTypeCode,
                    PrescribeTypeName = pre.PrescribeTypeName,
                    StartTime = pre.StartTime,
                    EndTime = pre.EndTime,
                    RecipeNo = "",
                    RecieveQty =
                        (pre.UsageCode == _configuration["UsageIntravenousDripCode"] &&
                         (model.Items.FrequencyCode.ToUpper() is "BID" or "TID"))
                            ? pre.LongDays
                            : (pre.FrequencyTimes ?? 1) * pre.LongDays,
                    RecieveUnit = treat.Unit,
                    ProjectType = treat.CategoryCode,
                    ProjectName = treat.CategoryName,
                    ProjectMerge = treat.ProjectMerge,
                    PyCode = treat.PyCode,
                    WbCode = treat.WbCode,
                    UsageCode = "",
                    UsageName = "",
                    intTreatId = treat.Id,
                    AdditionalItemsType = EAdditionalItemType.UsageAdditional,
                    AdditionalItemsId = id,
                }
            };
            addTreatDto.DoctorsAdvice.MeducalInsuranceCode = treat.MeducalInsuranceCode;
            addTreatDto.DoctorsAdvice.YBInneCode = treat.YBInneCode;
            await AddTreatAsync(addTreatDto);
        }

        //如果药品的途径是静脉滴注，需要根据频次来添加连续输液
        if (pre.UsageCode == _configuration["UsageIntravenousDripCode"] &&
            (model.Items.FrequencyCode.ToUpper() is "BID" or "TID"))
        {
            var qty = pre.LongDays;
            if (model.Items.FrequencyCode.ToUpper() == "TID")
            {
                qty = 2 * pre.LongDays;
            }

            await AppendOtherItemAsync(model, id, _configuration["ContinuousIntravenousInfusionCode"], qty,
                EAdditionalItemType.FrequencyAdditional);
        }

        return true;
    }

    /// <summary>
    /// 添加皮试附加项
    /// </summary>
    /// <param name="model"></param>
    /// <param name="id"></param>
    /// <param name="code"></param>
    /// <param name="qty"></param>
    /// <param name="additionalItemsType"></param>
    private async Task<bool> AppendOtherItemAsync(AddPrescribeDto model, Guid id, string code = "", int qty = 1,
        EAdditionalItemType additionalItemsType = EAdditionalItemType.SkinAdditional)
    {
        var pre = model.Items;
        //查询药品用法是否有关联处置，有则默认开一条处置单
        var treatResponse = await _grpcMasterDataClient.GetTreatProjectByCodeAsync(new GetTreatProjectByCodeRequest
        { Code = code });
        var treatByCode = treatResponse.TreatProject;
        if (treatByCode != null)
        {
            var addTreatDto = new AddTreatDto()
            {
                PatientInfo = model.PatientInfo,
                DoctorsAdvice = model.DoctorsAdvice.CloneJson(),
                IsAddition = true,
                Items = new TreatDto()
                {
                    Code = treatByCode.TreatCode,
                    Name = treatByCode.TreatName,
                    Unit = treatByCode.Unit,
                    Price = decimal.Parse(treatByCode.Price.ToString("f3")),
                    OtherPrice = decimal.Parse(treatByCode.OtherPrice.ToString("f3")),
                    Specification = treatByCode.Specification,
                    FeeTypeMainCode = treatByCode.FeeTypeMainCode,
                    FeeTypeSubCode = treatByCode.FeeTypeSubCode,
                    InsuranceCode = pre.InsuranceCode,
                    InsuranceType = pre.InsuranceType,
                    PayTypeCode = pre.PayTypeCode,
                    PayType = pre.PayType,
                    ApplyTime = pre.ApplyTime,
                    CategoryCode = treatByCode.DictionaryCode,
                    CategoryName = treatByCode.DictionaryName,
                    ExecDeptCode = pre.ExecDeptCode,
                    ExecDeptName = pre.ExecDeptName,
                    ChargeCode = treatByCode.ChargeCode,
                    ChargeName = treatByCode.ChargeName,
                    PrescribeTypeCode = pre.PrescribeTypeCode,
                    PrescribeTypeName = pre.PrescribeTypeName,
                    StartTime = pre.StartTime,
                    EndTime = pre.EndTime,
                    RecipeNo = "",
                    RecieveQty = qty,
                    RecieveUnit = treatByCode.Unit,
                    ProjectType = treatByCode.CategoryCode,
                    ProjectName = treatByCode.CategoryName,
                    ProjectMerge = treatByCode.ProjectMerge,
                    PyCode = treatByCode.PyCode,
                    WbCode = treatByCode.WbCode,
                    UsageCode = "",
                    UsageName = "",
                    intTreatId = treatByCode.Id,
                    Additional = treatByCode.Additional,
                    AdditionalItemsType = additionalItemsType,
                    AdditionalItemsId = id
                }
            };

            addTreatDto.DoctorsAdvice.MeducalInsuranceCode = treatByCode.MeducalInsuranceCode;
            addTreatDto.DoctorsAdvice.YBInneCode = treatByCode.YBInneCode;

            await AddTreatAsync(addTreatDto);


            return true;
        }

        return false;
    }

    /// <summary>
    ///  新增检验
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<List<Guid>> AddLisAsync(AddLisDto model)
    {
        var lisList = model.Items;
        if (lisList.Count == 0) Oh.Error("新增检验项必须包含检验项内容");

        if (!model.PatientInfo.IsChildren())
        {
            foreach (var item in lisList)
            {
                if (item.SpecimenName == "指血")
                {
                    Oh.Error("检验标本为手指血的项目，>=6岁的患者不能开");
                }
            }
        }

        var task = _patientAppService.GetPatientInfoAsync(model.DoctorsAdvice.PIID);

        var entities = new List<DoctorsAdvice>();
        var lisEntities = new List<Lis>();
        var lisItemEntities = new List<LisItem>();
        var mtm = new List<MedicalTechnologyMap>();
        var main = model.DoctorsAdvice;
        var isPreHospital = main.PlatformType == EPlatformType.PreHospital;

        foreach (var lis in lisList)
        {
            var id = GuidGenerator.Create(); //医嘱Id
            var lisId = GuidGenerator.Create(); //检验项Id 
            lis.SetPyWbCode();

            foreach (var item in lis.Items)
            {
                //检验小项Entity
                var itemEntity = new LisItem(
                    id: GuidGenerator.Create(),
                    targetCode: item.TargetCode,
                    targetName: item.TargetName,
                    targetUnit: item.TargetUnit,
                    price: item.Price,
                    qty: item.Qty,
                    insuranceCode: item.InsuranceCode,
                    insuranceType: item.InsuranceType,
                    projectCode: item.ProjectCode,
                    otherPrice: item.OtherPrice,
                    specification: item.Specification,
                    sort: item.Sort,
                    pyCode: item.PyCode,
                    wbCode: item.WbCode,
                    specialFlag: item.SpecialFlag,
                    isActive: item.IsActive,
                    projectType: item.ProjectType,
                    projectMerge: item.ProjectMerge,
                    lisId);
                itemEntity.YBInneCode = item.YBInneCode;
                itemEntity.MeducalInsuranceCode = item.MeducalInsuranceCode;
                mtm.Add(new MedicalTechnologyMap(itemEntity.Id, EDoctorsAdviceItemType.Lis));
                lisItemEntities.Add(itemEntity);
            }

            #region LisEntity

            //检验Entity
            var lisEntity = new Lis(
                id: lisId,
                catalogCode: lis.CatalogCode,
                catalogName: lis.CatalogName,
                clinicalSymptom: lis.ClinicalSymptom,
                specimenCode: lis.SpecimenCode,
                specimenName: lis.SpecimenName,
                specimenPartCode: lis.SpecimenPartCode,
                specimenPartName: lis.SpecimenPartName,
                containerCode: lis.ContainerCode,
                containerName: lis.ContainerName,
                containerColor: lis.ContainerColor,
                specimenDescription: lis.SpecimenDescription,
                specimenCollectDatetime: lis.SpecimenCollectDatetime,
                specimenReceivedDatetime: lis.SpecimenReceivedDatetime,
                reportTime: lis.ReportTime,
                reportDoctorCode: lis.ReportDoctorCode,
                reportDoctorName: lis.ReportDoctorName,
                hasReportName: lis.HasReportName,
                isEmergency: lis.IsEmergency,
                isBedSide: lis.IsBedSide,
                doctorsAdviceId: id,
                addCard: lis.AddCard,
                guideCode: lis.GuideCode,
                guideName: lis.GuideName,
                guideCatelogName: lis.GuideCatelogName);

            #endregion

            lisEntities.Add(lisEntity);

            //var amount = lis.Items.Select(s => s.Price * s.Qty).Sum();
            var sub = new ModifySubItemDto(
                code: lis.Code,
                name: lis.Name,
                unit: lis.Unit,
                price: lis.Price,
                amount: lis.GetAmount(), //TODO 待测试
                insuranceCode: lis.InsuranceCode,
                insuranceType: lis.InsuranceType,
                payTypeCode: lis.PayTypeCode,
                payType: lis.PayType,
                prescriptionNo: lis.PrescriptionNo,
                recipeGroupNo: lis.RecipeGroupNo,
                recipeNo: lis.RecipeNo,
                applyTime: lis.ApplyTime.HasValue ? lis.ApplyTime.Value : DateTime.Now,
                recipeCategoryCode: lis.CategoryCode,
                recipeCategoryName: lis.CategoryName,
                isBackTracking: lis.IsBackTracking,
                isRecipePrinted: lis.IsRecipePrinted,
                hisOrderNo: lis.HisOrderNo,
                positionCode: lis.PositionCode,
                positionName: lis.PositionName,
                execDeptCode: lis.ExecDeptCode,
                execDeptName: lis.ExecDeptName,
                remarks: lis.Remarks,
                chargeCode: lis.ChargeCode,
                chargeName: lis.ChargeName,
                prescribeTypeCode: lis.PrescribeTypeCode,
                prescribeTypeName: lis.PrescribeTypeName,
                startTime: lis.StartTime,
                endTime: lis.EndTime,
                recieveQty: lis.RecieveQty,
                recieveUnit: lis.RecieveUnit,
                pyCode: lis.PyCode,
                wbCode: lis.WbCode);
            var entity = await BuildDoctorsAdviceAsync(main, id, sub);
            entity.YBInneCode = main.YBInneCode;
            entity.MeducalInsuranceCode = main.MeducalInsuranceCode;
            entity.MeducalInsuranceName = main.MeducalInsuranceName;
            entity.UpdateStatus(ERecipeStatus.Saved);
            entity.ItemType = EDoctorsAdviceItemType.Lis;
            entities.Add(entity);
        }

        //处理院前医嘱号手工填写和子号默认值的问题
        if (isPreHospital)
        {
            var recipeNos = entities.Select(w => w.RecipeNo).Distinct().ToList();
            var existsEntities = await (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => recipeNos.Contains(w.RecipeNo))
                .Select(s => new { s.Id, s.RecipeNo, s.RecipeGroupNo }).ToListAsync();

            if (existsEntities.Any())
            {
                //院前一次只有一个检验，所以不需要做递增操作
                foreach (var item in entities)
                {
                    var recipeGroupNo = existsEntities.Where(w => w.RecipeNo == item.RecipeNo)
                        .Max(s => s.RecipeGroupNo);
                    if (recipeGroupNo > 0) item.RecipeGroupNo = recipeGroupNo + 1;
                }
            }
        }

        if (mtm.Any()) await _medicalTechnologyMapRepository.InsertManyAsync(mtm);
        if (lisItemEntities.Any()) await _lisItemRepository.InsertManyAsync(lisItemEntities);
        if (lisEntities.Any()) await _lisRepository.InsertManyAsync(lisEntities);
        if (entities.Any())
        {
            AdmissionRecordDto admissionRecordDto = await task;
            if (admissionRecordDto != null)
            {
                foreach (var item in entities)
                {
                    item.AreaCode = admissionRecordDto.AreaCode;
                }
            }
            await _doctorsAdviceRepository.InsertManyAsync(entities);
            var maps = new List<DoctorsAdviceMap>();
            entities.ForEach(x => maps.Add(new DoctorsAdviceMap(x.Id)));
            await _doctorsAdviceMapRepository.InsertManyAsync(maps);
        }

        //添加附加处置
        await AddLisTreatAsync(model, entities);

        return await Task.FromResult(entities.Select(s => s.Id).ToList());
    }

    /// <summary>
    /// 添加检验附加处置
    /// </summary>
    /// <param name="addLisDto"></param>
    /// <param name="doctorsAdvices"></param>
    /// <returns></returns>
    private async Task AddLisTreatAsync(AddLisDto addLisDto, List<DoctorsAdvice> doctorsAdvices)
    {
        List<LisDto> lisDtos = addLisDto.Items;
        List<string> treatCodes = new List<string>();
        foreach (LisDto lisDto in lisDtos)
        {
            if (!string.IsNullOrEmpty(lisDto.TreatCode))
            {
                string[] codes = lisDto.TreatCode.Split(",", StringSplitOptions.RemoveEmptyEntries);
                treatCodes.AddRange(codes);
            }
        }
        if (!treatCodes.Any()) return;

        treatCodes = treatCodes.Distinct().ToList();
        Guid piId = addLisDto.DoctorsAdvice.PIID;
        List<DoctorsAdvice> advices = await _doctorsAdviceRepository.GetListAsync(x => x.PIID == piId && x.Status != ERecipeStatus.Cancelled);
        List<string> exitCodes = advices.Select(x => x.Code).ToList();
        treatCodes = treatCodes.Except(exitCodes).ToList();
        foreach (string treatCode in treatCodes)
        {
            GetTreatByCodeRequest getTreatProjectByCodeRequest = new GetTreatByCodeRequest() { Code = treatCode };
            GetTreatByCodeResponse treat = await _grpcMasterDataClient.GetTreatByCodeAsync(getTreatProjectByCodeRequest);
            LisDto pre = lisDtos.First(x => x.TreatCode.Contains(treatCode));

            if (!string.IsNullOrWhiteSpace(treat.Code))
            {
                await AddTreatAsync(new AddTreatDto()
                {
                    PatientInfo = addLisDto.PatientInfo,
                    DoctorsAdvice = addLisDto.DoctorsAdvice,
                    IsAddition = false,
                    Items = new TreatDto()
                    {
                        Additional = treat.Additional,
                        Code = treat.Code,
                        Name = treat.Name,
                        Unit = treat.Unit,
                        Price = decimal.Parse(treat.Price.ToString("f3")),
                        OtherPrice = decimal.Parse(treat.OtherPrice.ToString("f3")),
                        Specification = treat.Specification,
                        FeeTypeMainCode = treat.FeeTypeMainCode,
                        FeeTypeSubCode = treat.FeeTypeSubCode,
                        InsuranceCode = pre.InsuranceCode,
                        InsuranceType = pre.InsuranceType,
                        PayTypeCode = pre.PayTypeCode,
                        PayType = pre.PayType,
                        ApplyTime = pre.ApplyTime,
                        CategoryCode = treat.DictionaryCode,
                        CategoryName = treat.DictionaryName,
                        ExecDeptCode = pre.ExecDeptCode, //treat.ExecDeptCode,
                        ExecDeptName = pre.ExecDeptName, //treat.ExecDeptName,
                        ChargeCode = string.Empty,
                        ChargeName = string.Empty,
                        PrescribeTypeCode = pre.PrescribeTypeCode,
                        PrescribeTypeName = pre.PrescribeTypeName,
                        StartTime = pre.StartTime,
                        EndTime = pre.EndTime,
                        RecipeNo = "",
                        RecieveQty = 1,
                        RecieveUnit = treat.Unit,
                        ProjectType = treat.CategoryCode,
                        ProjectName = treat.CategoryName,
                        ProjectMerge = treat.ProjectMerge,
                        PyCode = treat.PyCode,
                        WbCode = treat.WbCode,
                        UsageCode = "",
                        UsageName = "",
                        intTreatId = treat.Id,
                        AdditionalItemsType = EAdditionalItemType.LisAdditional,
                        AdditionalItemsId = addLisDto.DoctorsAdvice.Id ?? Guid.Empty,
                    }
                });
            }
        }
        return;
    }

    /// <summary>
    ///  新增检查
    /// </summary>
    /// <param name="addPacsDto"></param>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<AddPacsResponseDto> AddPacsAsync(AddPacsDto addPacsDto)
    {
        List<PacsDto> pacsDtoList = addPacsDto.Items;
        if (pacsDtoList.Count == 0) Oh.Error("新增检查项必须包含检查项内容");
        var task = _patientAppService.GetPatientInfoAsync(addPacsDto.DoctorsAdvice.PIID);

        List<DoctorsAdvice> doctorsAdviceEntities = new List<DoctorsAdvice>();
        List<Pacs> pacsEntities = new List<Pacs>();
        List<PacsItem> pacsItemEntities = new List<PacsItem>();
        List<PacsPathologyItem> pacsPathologyItemEntities = new List<PacsPathologyItem>();
        List<PacsPathologyItemNo> pacsPathologyItemNoEntities = new List<PacsPathologyItemNo>();
        List<MedicalTechnologyMap> mtm = new List<MedicalTechnologyMap>();

        ModifyDoctorsAdviceBaseDto main = addPacsDto.DoctorsAdvice;
        int pid = default;
        try
        {
            pid = int.Parse(main.PatientId);
        }
        catch
        {
            Oh.Error("请传入正确的科室代码，或者患者编码。");
        }
        bool isPreHospital = main.PlatformType == EPlatformType.PreHospital;

        //查询4小时内重复的历史数据，按0元开单
        DateTime before4HourTime = DateTime.Now.AddHours(-4);
        //查询出四小时之前的医嘱
        var daList = await _doctorsAdviceRepository.GetListAsync(c => c.PIID == addPacsDto.DoctorsAdvice.PIID && c.ItemType == EDoctorsAdviceItemType.Pacs && c.PlatformType == 0 && c.Status == ERecipeStatus.Saved && c.ApplyTime >= before4HourTime);
        var daIds = daList.Select(c => c.Id);
        var pacsIds = (await _pacsRepository.GetListAsync(c => daIds.Contains(c.DoctorsAdviceId))).Select(c => c.Id);
        var itemList = await _pacsItemRepository.GetListAsync(c => pacsIds.Contains(c.PacsId));

        CheckPacsXmcfRequestDto checkLisXmcfRequestDto = new CheckPacsXmcfRequestDto()
        {
            mzzy = 1,
            brid = pid,
        };
        checkLisXmcfRequestDto.xmlist = new List<RequestXmlistItem>();
        if (daList.Count > 0)
        {
            foreach (var item in itemList)
            {
                checkLisXmcfRequestDto.xmlist.Add(new RequestXmlistItem()
                {
                    jlwyz = item.Id.ToString(),
                    ztxh = item.ProjectCode,
                    fydj = item.Price,
                    fymc = item.TargetName,
                    fysl = (int)item.Qty,
                    fyxh = item.TargetCode
                });
            }
        }

        foreach (PacsDto pacsDto in pacsDtoList)
        {
            Guid id = GuidGenerator.Create(); //医嘱Id
            Guid pacsId = GuidGenerator.Create(); //检查项Id
            pacsDto.SetPyWbCode();

            foreach (PacsItemDto item in pacsDto.Items)
            {
                PacsItem pacsItem = new PacsItem(
                    id: GuidGenerator.Create(),
                    targetCode: item.TargetCode,
                    targetName: item.TargetName,
                    targetUnit: item.TargetUnit,
                    price: item.Price,
                    qty: item.Qty,
                    insuranceCode: item.InsuranceCode,
                    insuranceType: item.InsuranceType,
                    projectCode: item.ProjectCode,
                    otherPrice: item.OtherPrice,
                    specification: item.Specification,
                    sort: item.Sort,
                    pyCode: item.PyCode,
                    wbCode: item.WbCode,
                    specialFlag: item.SpecialFlag,
                    isActive: item.IsActive,
                    projectType: item.ProjectType,
                    projectMerge: item.ProjectMerge,
                    pacsId);
                pacsItem.YBInneCode = item.YBInneCode;
                pacsItem.MeducalInsuranceCode = item.MeducalInsuranceCode;

                mtm.Add(new MedicalTechnologyMap(pacsItem.Id, EDoctorsAdviceItemType.Pacs));

                pacsItemEntities.Add(pacsItem);
                checkLisXmcfRequestDto.xmlist.Add(new RequestXmlistItem()
                {
                    jlwyz = pacsItem.Id.ToString(),
                    ztxh = item.ProjectCode,
                    fydj = item.Price,
                    fymc = item.TargetName,
                    fysl = (int)item.Qty,
                    fyxh = item.TargetCode
                });
            }

            pacsEntities.Add(new Pacs(pacsId, pacsDto.CatalogCode, pacsDto.CatalogName, pacsDto.FirstCatalogCode,
                pacsDto.FirstCatalogName, pacsDto.ClinicalSymptom,
                pacsDto.MedicalHistory,
                pacsDto.PartCode, pacsDto.PartName, pacsDto.CatalogDisplayName, pacsDto.ReportTime, pacsDto.ReportDoctorCode,
                pacsDto.ReportDoctorName,
                pacsDto.HasReport, pacsDto.IsEmergency, pacsDto.IsBedSide, id, pacsDto.AddCard, pacsDto.GuideCode,
                pacsDto.GuideName, pacsDto.ExamTitle, pacsDto.ReservationPlace, pacsDto.TemplateId));

            if (pacsDto.PacsPathologyItemDto != null)
            {
                PacsPathologyItem pacsPathologyItem = new PacsPathologyItem(GuidGenerator.Create());
                pacsPathologyItem.PacsId = pacsId;
                pacsPathologyItem.Specimen = pacsDto.PacsPathologyItemDto.Specimen;
                pacsPathologyItem.DrawMaterialsPart = pacsDto.PacsPathologyItemDto.DrawMaterialsPart;
                pacsPathologyItem.SpecimenQty = pacsDto.PacsPathologyItemDto.SpecimenQty;
                pacsPathologyItem.LeaveTime = pacsDto.PacsPathologyItemDto.LeaveTime;
                pacsPathologyItem.RegularTime = pacsDto.PacsPathologyItemDto.RegularTime;
                pacsPathologyItem.SpecificityInfect = pacsDto.PacsPathologyItemDto.SpecificityInfect;
                pacsPathologyItem.ApplyForObjective = pacsDto.PacsPathologyItemDto.ApplyForObjective;
                pacsPathologyItem.Symptom = pacsDto.PacsPathologyItemDto.Symptom;
                pacsPathologyItemEntities.Add(pacsPathologyItem);

                if (!string.IsNullOrEmpty(pacsDto.PacsPathologyItemDto.Specimen))
                {
                    string[] specimenNames = pacsDto.PacsPathologyItemDto.Specimen.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    if (specimenNames.Any())
                    {
                        foreach (string item in specimenNames)
                        {
                            PacsPathologyItemNo pacsPathologyItemNo = new PacsPathologyItemNo()
                            {
                                PacsId = pacsId,
                                SpecimenName = item
                            };
                            pacsPathologyItemNoEntities.Add(pacsPathologyItemNo);
                        }
                    }
                }
            }

            //var amount = pacs.Items.Select(s => s.Price * s.Qty).Sum();
            //var IsAdditionPrice = false;
            // 北京大学深圳医院 儿童附加收费计算金额 
            var amount = pacsDto.GetAmount(addPacsDto.PatientInfo.IsChildren());

            var sub = new ModifySubItemDto(
                code: pacsDto.Code,
                name: pacsDto.Name,
                unit: pacsDto.Unit,
                price: pacsDto.Price,
                amount: amount, //TODO 待测试
                insuranceCode: pacsDto.InsuranceCode,
                insuranceType: pacsDto.InsuranceType,
                payTypeCode: pacsDto.PayTypeCode,
                payType: pacsDto.PayType,
                prescriptionNo: pacsDto.PrescriptionNo,
                recipeGroupNo: pacsDto.RecipeGroupNo,
                recipeNo: pacsDto.RecipeNo,
                applyTime: pacsDto.ApplyTime.HasValue ? pacsDto.ApplyTime.Value : DateTime.Now,
                recipeCategoryCode: pacsDto.CategoryCode,
                recipeCategoryName: pacsDto.CategoryName,
                isBackTracking: pacsDto.IsBackTracking,
                isRecipePrinted: pacsDto.IsRecipePrinted,
                hisOrderNo: pacsDto.HisOrderNo,
                positionCode: pacsDto.PositionCode,
                positionName: pacsDto.PositionName,
                execDeptCode: pacsDto.ExecDeptCode,
                execDeptName: pacsDto.ExecDeptName,
                remarks: pacsDto.Remarks,
                chargeCode: pacsDto.ChargeCode,
                chargeName: pacsDto.ChargeName,
                prescribeTypeCode: pacsDto.PrescribeTypeCode,
                prescribeTypeName: pacsDto.PrescribeTypeName,
                startTime: pacsDto.StartTime,
                endTime: pacsDto.EndTime,
                recieveQty: pacsDto.RecieveQty,
                recieveUnit: pacsDto.RecieveUnit,
                pyCode: pacsDto.PyCode,
                wbCode: pacsDto.WbCode);
            //构建医嘱实体
            var entity = await BuildDoctorsAdviceAsync(main, id, sub);
            entity.YBInneCode = main.YBInneCode;
            entity.MeducalInsuranceCode = main.MeducalInsuranceCode;
            entity.MeducalInsuranceName = main.MeducalInsuranceName;
            entity.UpdateStatus(ERecipeStatus.Saved);
            entity.ItemType = EDoctorsAdviceItemType.Pacs;
            doctorsAdviceEntities.Add(entity);
        }

        // 需要重新计算价格 的 DA 医嘱 和处置类型 
        List<AddPacsHisResponse> hisResponses = new List<AddPacsHisResponse>();
        try
        {
            CheckPacsXmcfResponseDto checkPacsXmcfResponseDto = await _hisService.CheckPacsXmcfAsync(checkLisXmcfRequestDto);
            if (checkPacsXmcfResponseDto != null && checkPacsXmcfResponseDto.Data != null)
            {
                var list = checkPacsXmcfResponseDto.Data.xmlist;

                foreach (var item in list)
                {
                    AddPacsHisResponse entity = new AddPacsHisResponse()
                    {
                        PacsItemId = Guid.Parse(item.jlwyz),
                        NewTargetCode = item.newfyxh.ToString(),
                        NewTargetName = item.newfymc,
                        NewPrice = item.newfydj,
                        NewQty = item.newfysl,
                        ProjectCode = item.ztxh.ToString(),
                        Qty = item.fysl,
                        Price = item.fydj,
                        TargetName = item.fymc,
                        TargetCode = item.fyxh.ToString()
                    };
                    var add = true;
                    switch (item.czff)
                    {
                        case "0元开单":
                            entity.CzffEnum = CzffEnum.ZeroAllow;
                            // 将当前这一单置为0 
                            foreach (var pacItem in pacsItemEntities)
                            {
                                if (pacItem.Id.ToString() == item.jlwyz)
                                {
                                    // 标记这一单为0元开单 
                                    pacItem.Price = 0;
                                    foreach (var pac in pacsEntities)
                                    {
                                        if (pacItem.PacsId == pac.Id)
                                        {
                                            entity.DoctorsAdviceId = pac.DoctorsAdviceId;
                                            foreach (var doctorsAdvice in doctorsAdviceEntities)
                                            {
                                                if (doctorsAdvice.Id == pac.DoctorsAdviceId)
                                                {
                                                    entity.ProjectName = doctorsAdvice.Name;
                                                    doctorsAdvice.Remarks += pacItem.TargetName + "当日4小时内重复，按0元开单; ";
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                    }
                                    break;
                                }
                                if (pacsItemEntities.Count() - 1 == pacsItemEntities.IndexOf(pacItem))
                                    add = false;
                            }
                            if (add)
                                hisResponses.Add(entity);
                            break;

                        case "不允许开单":
                            entity.CzffEnum = CzffEnum.NotAllow;
                            foreach (var pacItem in pacsItemEntities)
                            {
                                if (pacItem.Id.ToString() == item.jlwyz)
                                {
                                    foreach (var pac in pacsEntities)
                                    {
                                        if (pacItem.PacsId == pac.Id)
                                        {
                                            entity.DoctorsAdviceId = pac.DoctorsAdviceId;
                                            foreach (var doctorsAdvice in doctorsAdviceEntities)
                                            {
                                                if (doctorsAdvice.Id == pac.DoctorsAdviceId)
                                                {
                                                    entity.ProjectName = doctorsAdvice.Name;
                                                    doctorsAdvice.Remarks += pacItem.TargetName + "不允许开单; ";
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    //删除当前记录提示不允许开单
                                    pacsItemEntities.Remove(pacItem);
                                    break;
                                }
                                if (pacsItemEntities.Count() - 1 == pacsItemEntities.IndexOf(pacItem))
                                    add = false;
                            }
                            if (add)
                                hisResponses.Add(entity);
                            break;

                        case "项目置换":
                            entity.CzffEnum = CzffEnum.Replacement;
                            foreach (var pacItem in pacsItemEntities)
                            {
                                if (pacItem.Id.ToString() == item.jlwyz)
                                {
                                    //entity.TargetCode = pacItem.TargetCode;
                                    //entity.TargetName = pacItem.TargetName;
                                    //entity.Price = pacItem.Price;
                                    //entity.Qty = pacItem.Qty;
                                    //entity.PacsItemId = pacItem.PacsId;

                                    //进行项目置换  
                                    pacItem.Price = entity.NewPrice;
                                    pacItem.Qty = entity.NewQty;
                                    pacItem.TargetName = entity.NewTargetName;
                                    pacItem.TargetCode = entity.NewTargetCode;

                                    foreach (var pac in pacsEntities)
                                    {
                                        if (pacItem.PacsId == pac.Id)
                                        {
                                            entity.DoctorsAdviceId = pac.DoctorsAdviceId;
                                            foreach (var doctorsAdvice in doctorsAdviceEntities)
                                            {
                                                if (doctorsAdvice.Id == pac.DoctorsAdviceId)
                                                {
                                                    entity.ProjectName = doctorsAdvice.Name;
                                                    doctorsAdvice.Remarks += pacItem.TargetName + "项目置换成" + entity.NewTargetName + "; ";
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                //最后一条都没找到的话丢弃 表示不是本次添加的数据
                                if (pacsItemEntities.Count() - 1 == pacsItemEntities.IndexOf(pacItem))
                                    add = false;
                            }
                            if (add)
                                hisResponses.Add(entity);
                            break;

                        default: break;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            Oh.Error("His 接口未正常返回数据！");
        }

        // 重新计算project 价格 
        foreach (var item in hisResponses)
        {
            foreach (var doctorsAdvice in doctorsAdviceEntities)
            {
                if (doctorsAdvice.Id == item.DoctorsAdviceId)
                {
                    //重新计算价格
                    var pacs = pacsEntities.Where(c => c.DoctorsAdviceId == doctorsAdvice.Id).Select(c => c.Id);
                    doctorsAdvice.Amount = pacsItemEntities.Where(c => pacs.Contains(c.PacsId)).Sum(s => s.Price * s.Qty) * doctorsAdvice.RecieveQty;
                    break;
                }
            }
        }

        //处理院前医嘱号手工填写和子号默认值的问题
        if (isPreHospital)
        {
            var recipeNos = doctorsAdviceEntities.Select(w => w.RecipeNo).Distinct().ToList();
            var existsEntities = await (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => recipeNos.Contains(w.RecipeNo))
                .Select(s => new { s.Id, s.RecipeNo, s.RecipeGroupNo }).ToListAsync();

            if (existsEntities.Any())
            {
                //院前一次只有一个检验，所以不需要做递增操作
                foreach (var item in doctorsAdviceEntities)
                {
                    var recipeGroupNo = existsEntities.Where(w => w.RecipeNo == item.RecipeNo)
                        .Max(s => s.RecipeGroupNo);
                    if (recipeGroupNo > 0) item.RecipeGroupNo = recipeGroupNo + 1;
                }
            }
        }

        if (mtm.Any()) await _medicalTechnologyMapRepository.InsertManyAsync(mtm);
        if (pacsItemEntities.Any()) await _pacsItemRepository.InsertManyAsync(pacsItemEntities);
        if (pacsEntities.Any()) await _pacsRepository.InsertManyAsync(pacsEntities);
        if (pacsPathologyItemEntities.Any()) await _pacsPathologyItemRepository.InsertManyAsync(pacsPathologyItemEntities);
        if (pacsPathologyItemNoEntities.Any()) await _pacsPathologyItemNoRepository.InsertManyAsync(pacsPathologyItemNoEntities);

        if (doctorsAdviceEntities.Any())
        {
            AdmissionRecordDto admissionRecordDto = await task;
            if (admissionRecordDto != null)
            {
                foreach (var item in doctorsAdviceEntities)
                {
                    item.AreaCode = admissionRecordDto.AreaCode;
                }
            }
            await _doctorsAdviceRepository.InsertManyAsync(doctorsAdviceEntities);
            var maps = new List<DoctorsAdviceMap>();
            doctorsAdviceEntities.ForEach(x => maps.Add(new DoctorsAdviceMap(x.Id)));
            await _doctorsAdviceMapRepository.InsertManyAsync(maps);
        }
        AddPacsResponseDto addPacsResponse = new AddPacsResponseDto();

        //添加附加项
        IEnumerable<string> projectCodes = addPacsDto.Items.Select(x => x.Code);
        ExamCodeRequest examCodeRequest = new ExamCodeRequest();
        examCodeRequest.Code.AddRange(projectCodes);
        ExamAttachItemReponse examAttachItemReponse = await _grpcMasterDataClient.GetExamAttachItemsAsync(examCodeRequest);
        if (examAttachItemReponse.ExamAttachItems.Any())
        {
            await AddPacsTreatAsync(addPacsDto, examAttachItemReponse);
            await AddPacsPrescribeAsync(addPacsDto, examAttachItemReponse);
        }

        addPacsResponse.GuidList = await Task.FromResult(doctorsAdviceEntities.Select(s => s.Id).ToList());
        addPacsResponse.AddPacsHisResponses = hisResponses;
        return addPacsResponse;
    }

    /// <summary>
    /// 添加检查附加处置
    /// </summary>
    /// <param name="addPacsDto"></param>
    /// <param name="examAttachItemReponse"></param>
    /// <returns></returns>
    private async Task AddPacsTreatAsync(AddPacsDto addPacsDto, ExamAttachItemReponse examAttachItemReponse)
    {
        List<PacsDto> pacsDtos = addPacsDto.Items;
        List<string> treatCodes = examAttachItemReponse.ExamAttachItems.Where(x => x.Type == "Treat").Select(x => x.MedicineCode).ToList();
        if (!treatCodes.Any()) return;

        foreach (string treatCode in treatCodes)
        {
            ExamAttachItemModel examAttachItem = examAttachItemReponse.ExamAttachItems.First(x => x.MedicineCode == treatCode);

            GetTreatByCodeRequest getTreatProjectByCodeRequest = new GetTreatByCodeRequest() { Code = treatCode };
            GetTreatByCodeResponse treat = await _grpcMasterDataClient.GetTreatByCodeAsync(getTreatProjectByCodeRequest);
            PacsDto pre = pacsDtos.First(x => x.Code == examAttachItem.ProjectCode);

            if (!string.IsNullOrWhiteSpace(treat.Code))
            {
                var treatDto = new AddTreatDto()
                {
                    PatientInfo = addPacsDto.PatientInfo,
                    DoctorsAdvice = addPacsDto.DoctorsAdvice.CloneJson(),
                    IsAddition = false,
                    Items = new TreatDto()
                    {
                        Additional = treat.Additional,
                        Code = treat.Code,
                        Name = treat.Name,
                        Unit = treat.Unit,
                        Price = decimal.Parse(treat.Price.ToString("f3")),
                        OtherPrice = decimal.Parse(treat.OtherPrice.ToString("f3")),
                        Specification = treat.Specification,
                        FeeTypeMainCode = treat.FeeTypeMainCode,
                        FeeTypeSubCode = treat.FeeTypeSubCode,
                        InsuranceCode = pre.InsuranceCode,
                        InsuranceType = pre.InsuranceType,
                        PayTypeCode = pre.PayTypeCode,
                        PayType = pre.PayType,
                        ApplyTime = pre.ApplyTime,
                        CategoryCode = treat.DictionaryCode,
                        CategoryName = treat.DictionaryName,
                        ExecDeptCode = pre.ExecDeptCode, //treat.ExecDeptCode,
                        ExecDeptName = pre.ExecDeptName, //treat.ExecDeptName,
                        ChargeCode = string.Empty,
                        ChargeName = string.Empty,
                        PrescribeTypeCode = pre.PrescribeTypeCode,
                        PrescribeTypeName = pre.PrescribeTypeName,
                        StartTime = pre.StartTime,
                        EndTime = pre.EndTime,
                        RecipeNo = "",
                        RecieveQty = (decimal)examAttachItem.Qty,
                        RecieveUnit = treat.Unit,
                        ProjectType = treat.CategoryCode,
                        ProjectName = treat.CategoryName,
                        ProjectMerge = treat.ProjectMerge,
                        PyCode = treat.PyCode,
                        WbCode = treat.WbCode,
                        UsageCode = "",
                        UsageName = "",
                        Remarks = "检查自动带出",
                        intTreatId = treat.Id,
                        AdditionalItemsType = EAdditionalItemType.No,
                        AdditionalItemsId = Guid.Empty,
                    }
                };
                treatDto.DoctorsAdvice.SourceType = 1;
                treatDto.DoctorsAdvice.ItemType = EDoctorsAdviceItemType.Treat;
                treatDto.DoctorsAdvice.MeducalInsuranceCode = treat.MeducalInsuranceCode;
                treatDto.DoctorsAdvice.YBInneCode = treat.YBInneCode;


                await AddTreatAsync(treatDto);
            }
        }
        return;
    }

    /// <summary>
    /// 添加检查附加药品
    /// </summary>
    /// <param name="addPacsDto"></param>
    /// <param name="examAttachItemReponse"></param>
    /// <returns></returns>
    private async Task AddPacsPrescribeAsync(AddPacsDto addPacsDto, ExamAttachItemReponse examAttachItemReponse)
    {
        List<PacsDto> pacsDtos = addPacsDto.Items;
        List<string> prescribeCodes = examAttachItemReponse.ExamAttachItems.Where(x => x.Type == "Medicine").Select(x => x.MedicineCode).ToList();
        if (!prescribeCodes.Any()) return;

        if (prescribeCodes.Any())
        {
            var projectCodes = examAttachItemReponse.ExamAttachItems.Select(x => x.ProjectCode).Distinct();
            foreach (var projectCode in projectCodes)
            {
                Guid id = Guid.NewGuid();
                var examProjectResponse = await this._grpcMasterDataClient.GetExamProjectByCodeAsync(new GetExamProjectByCodeRequest()
                {
                    Code = projectCode
                });
                var examProject = examProjectResponse.ExamProject;
                int sourceType = 1;
                if (examProject != null)
                {
                    if (examProject.ProjectName.Contains("CT") && examProject.ProjectName.Contains("增强"))
                    {
                        sourceType = 8;
                    }
                }

                List<string> currentCodes = examAttachItemReponse.ExamAttachItems.Where(x => x.Type == "Medicine" && x.ProjectCode == projectCode).Select(x => x.MedicineCode).ToList();
                //currentCodes = prescribeCodes.Where(x => currentCodes.Contains(x)).ToList();
                foreach (string prescribeCode in currentCodes)
                {
                    GetMedicineInvInfoRquest req = new GetMedicineInvInfoRquest()
                    {
                        Code = prescribeCode,
                        FactoryCode = string.Empty,
                        Specification = string.Empty,
                        PharmacyCode = "1"
                    };
                    MedicineResponse resp = await _grpcMasterDataClient.GetMedicineInvInfoAsync(req);
                    if (resp == null || resp.Medicine == null || string.IsNullOrEmpty(resp.Medicine.Code)) continue;

                    GrpcMedicineModel medicine = resp.Medicine;
                    ExamAttachItemModel examAttachItem = examAttachItemReponse.ExamAttachItems.First(x => x.MedicineCode == prescribeCode && x.ProjectCode == projectCode);
                    double recieveQty = Math.Ceiling(examAttachItem.Qty / medicine.DefaultDosage);
                    PrescribeDto prescribeDto = new PrescribeDto()
                    {
                        MedicineId = medicine.Id,
                        Code = medicine.Code,
                        Name = medicine.Name,
                        IsOutDrug = false,
                        MedicineProperty = "1",
                        ToxicProperty = string.Empty,
                        PrescribeTypeCode = "PrescribeTemp",
                        PrescribeTypeName = "临",
                        UsageCode = medicine.UsageCode,
                        UsageName = medicine.UsageName,
                        LongDays = 1,
                        ActualDays = 1,
                        DosageQty = (decimal)medicine.DosageQty,
                        DefaultDosageQty = (decimal)medicine.DefaultDosage,
                        QtyPerTimes = 0,
                        DosageUnit = medicine.DosageUnit,
                        DefaultDosageUnit = medicine.DosageUnit,
                        HisUnit = medicine.Unit,
                        HisDosageUnit = medicine.DosageUnit,
                        HisDosageQty = (decimal)medicine.DefaultDosage,
                        RecieveQty = (decimal)recieveQty,
                        RecieveUnit = medicine.Unit,
                        Specification = medicine.Specification,
                        Unpack = EMedicineUnPack.RoundByMinUnitAmount,
                        BigPackPrice = (decimal)medicine.BigPackPrice,
                        BigPackFactor = medicine.BigPackFactor,
                        BigPackUnit = medicine.BigPackUnit,
                        SmallPackPrice = (decimal)medicine.SmallPackPrice,
                        SmallPackFactor = medicine.SmallPackFactor,
                        SmallPackUnit = medicine.SmallPackUnit,
                        RecipeNo = "0",
                        FrequencyCode = "st",
                        FrequencyName = "立即执行",
                        MedicalInsuranceCode = medicine.MedicalInsuranceCode,
                        FrequencyTimes = 1,
                        FrequencyUnit = string.Empty,
                        FrequencyExecDayTimes = string.Empty,
                        DailyFrequency = "1",
                        PharmacyCode = medicine.PharmacyCode,
                        PharmacyName = medicine.PharmacyName,
                        FactoryCode = medicine.FactoryCode,
                        FactoryName = medicine.FactoryName,
                        BatchNo = string.Empty,
                        Unit = medicine.Unit,
                        Price = (decimal)medicine.Price,
                        InsuranceCode = medicine.InsuranceCode.ToString(),
                        InsuranceType = EInsuranceCatalog.Self,
                        PayTypeCode = string.Empty,
                        PayType = ERecipePayType.Self,
                        ApplyTime = DateTime.Now,
                        CategoryCode = "Medicine",
                        CategoryName = "药物",
                        ExecDeptCode = medicine.ExecDeptCode,
                        ExecDeptName = medicine.ExecDeptName,
                        Remarks = "检查自动带出",
                        LimitType = 2,
                        PyCode = medicine.PyCode,
                        WbCode = medicine.WbCode,
                        IsCriticalPrescription = true
                    };
                    AddPrescribeDto addPrescribeDto = new AddPrescribeDto()
                    {
                        DoctorsAdvice = addPacsDto.DoctorsAdvice.CloneJson(),
                        PatientInfo = addPacsDto.PatientInfo,
                        Items = prescribeDto
                    };
                    addPrescribeDto.DoctorsAdvice.ItemType = EDoctorsAdviceItemType.Prescribe;
                    addPrescribeDto.DoctorsAdvice.SourceType = sourceType;
                    addPrescribeDto.DoctorsAdvice.MeducalInsuranceCode = medicine.MedicalInsuranceCode;
                    addPrescribeDto.DoctorsAdvice.YBInneCode = medicine.YBInneCode;
                    addPrescribeDto.DoctorsAdvice.MeducalInsuranceName = medicine.MeducalInsuranceName;
                    addPrescribeDto.DoctorsAdvice.PrescriptionFlag = id.ToString("N");
                    PrescribeResponseDto prescribe = await CreatePrescribeAsync(addPrescribeDto);
                }
            }
        }

        return;
    }

    /// <summary>
    /// 全景视图-检验列表
    /// </summary>
    /// <param name="PID"></param>
    /// <param name="StartTime"></param>
    /// <param name="EndTime"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<LisListDto>> GetAllViewLisListAsync(Guid PID, DateTime? StartTime, DateTime? EndTime,
        CancellationToken cancellationToken = default)
    {
        var lisList = await (from d in (await this._doctorsAdviceRepository.GetQueryableAsync()).AsNoTracking()
                    .Where(x => x.PIID == PID)
                    .WhereIf(StartTime.HasValue, w => w.ApplyTime >= StartTime.Value)
                    .WhereIf(EndTime.HasValue, w => w.ApplyTime <= EndTime.Value)
                    .OrderBy(x => x.ApplyTime)
                             join l in (await _lisRepository.GetQueryableAsync()) on d.Id equals l.DoctorsAdviceId
                             select new LisListDto
                             {
                                 Id = l.Id,
                                 CatalogCode = l.CatalogCode,
                                 CatalogName = l.CatalogName,
                                 ClinicalSymptom = l.ClinicalSymptom,
                                 SpecimenCode = l.SpecimenCode,
                                 SpecimenName = l.SpecimenName,
                                 SpecimenPartCode = l.SpecimenPartCode,
                                 SpecimenPartName = l.SpecimenPartName,
                                 ContainerCode = l.ContainerCode,
                                 ContainerName = l.ContainerName,
                                 ContainerColor = l.ContainerColor,
                                 SpecimenDescription = l.SpecimenDescription,
                                 SpecimenCollectDatetime = l.SpecimenCollectDatetime,
                                 SpecimenReceivedDatetime = l.SpecimenReceivedDatetime,
                                 ReportTime = l.ReportTime,
                                 ReportDoctorCode = l.ReportDoctorCode,
                                 ReportDoctorName = l.ReportDoctorName,
                                 HasReportName = l.HasReportName,
                                 IsEmergency = l.IsEmergency,
                                 IsBedSide = l.IsBedSide,
                                 AddCard = l.AddCard,
                                 DoctorsAdviceId = l.DoctorsAdviceId,
                                 ApplyTime = d.ApplyTime
                             })
            .ToListAsync();
        return lisList;
    }

    /// <summary>
    /// 全景视图-检查列表
    /// </summary>
    /// <param name="PID"></param>
    /// <param name="StartTime"></param>
    /// <param name="EndTime"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<PacsListDto>> GetAllViewPacsListAsync(Guid PID, DateTime? StartTime, DateTime? EndTime,
        CancellationToken cancellationToken = default)
    {
        var pacsList = await (from d in ((await this._doctorsAdviceRepository.GetQueryableAsync()).AsNoTracking()
                    .Where(x => x.PIID == PID)
                    .WhereIf(StartTime.HasValue, w => w.ApplyTime >= StartTime.Value)
                    .WhereIf(EndTime.HasValue, w => w.ApplyTime <= EndTime.Value)
                    .OrderBy(x => x.ApplyTime))
                              join p in (await _pacsRepository.GetQueryableAsync()) on d.Id equals p.DoctorsAdviceId
                              select new PacsListDto
                              {
                                  Id = p.Id,
                                  CatalogCode = p.CatalogCode,
                                  CatalogName = p.CatalogName,
                                  ClinicalSymptom = p.ClinicalSymptom,
                                  MedicalHistory = p.MedicalHistory,
                                  PartCode = p.PartCode,
                                  PartName = p.PartName,
                                  CatalogDisplayName = p.CatalogDisplayName,
                                  ReportTime = p.ReportTime,
                                  ReportDoctorCode = p.ReportDoctorCode,
                                  ReportDoctorName = p.ReportDoctorName,
                                  HasReport = p.HasReport,
                                  IsEmergency = p.IsEmergency,
                                  IsBedSide = p.IsBedSide,
                                  AddCard = p.AddCard,
                                  DoctorsAdviceId = p.DoctorsAdviceId,
                                  ApplyTime = d.ApplyTime
                              })
            .ToListAsync();
        return pacsList;
    }

    /// <summary>
    ///  新增诊疗项
    /// </summary>
    /// <param name="model"></param> 
    /// <returns></returns>  
    [UnitOfWork]
    public async Task<Guid> AddTreatAsync(AddTreatDto model)
    {
        try
        {
            Guid id = GuidGenerator.Create();
            TreatDto treatDto = model.Items;
            treatDto.SetPyWbCode();
            bool isAdditionalPrice = false;

            decimal amount = treatDto.GetAmount(model.PatientInfo.IsChildren(), out isAdditionalPrice);

            ModifySubItemDto modifySubItemDto = new ModifySubItemDto(
                code: treatDto.Code,
                name: treatDto.Name,
                unit: treatDto.Unit.IsNullOrEmpty() ? "次" : treatDto.Unit,
                price: treatDto.Price,
                amount: amount,
                insuranceCode: treatDto.InsuranceCode,
                insuranceType: treatDto.InsuranceType,
                payTypeCode: treatDto.PayTypeCode,
                payType: treatDto.PayType,
                prescriptionNo: treatDto.PrescriptionNo,
                recipeGroupNo: 1,
                recipeNo: treatDto.RecipeNo,
                applyTime: treatDto.ApplyTime ?? DateTime.Now,
                recipeCategoryCode: treatDto.CategoryCode,
                recipeCategoryName: treatDto.CategoryName,
                isBackTracking: treatDto.IsBackTracking,
                isRecipePrinted: treatDto.IsRecipePrinted,
                hisOrderNo: treatDto.HisOrderNo,
                positionCode: treatDto.PositionCode,
                positionName: treatDto.PositionName,
                execDeptCode: treatDto.ExecDeptCode,
                execDeptName: treatDto.ExecDeptName,
                remarks: treatDto.Remarks,
                chargeCode: treatDto.ChargeCode,
                chargeName: treatDto.ChargeName,
                prescribeTypeCode: treatDto.PrescribeTypeCode,
                prescribeTypeName: treatDto.PrescribeTypeName,
                startTime: treatDto.StartTime,
                endTime: treatDto.EndTime,
                recieveQty: treatDto.RecieveQty,
                recieveUnit: treatDto.RecieveUnit,
                pyCode: treatDto.PyCode,
                wbCode: treatDto.WbCode,
                isAdditionalPrice: isAdditionalPrice);
            //构建医嘱实体 
            DoctorsAdvice doctorsAdvice = await BuildDoctorsAdviceAsync(model.DoctorsAdvice, id, modifySubItemDto);
            var admissionRecordTask = _patientAppService.GetPatientInfoAsync(doctorsAdvice.PIID);
            doctorsAdvice.YBInneCode = model.DoctorsAdvice.YBInneCode;
            doctorsAdvice.MeducalInsuranceCode = model.DoctorsAdvice.MeducalInsuranceCode;
            doctorsAdvice.MeducalInsuranceName = model.DoctorsAdvice.MeducalInsuranceName;
            doctorsAdvice.UpdateStatus(ERecipeStatus.Saved);
            doctorsAdvice.ItemType = EDoctorsAdviceItemType.Treat;

            string frequencyCode = string.Empty;
            string frequencyName = string.Empty;
            if (treatDto.CategoryCode == "Entrust")
            {
                frequencyCode = treatDto.FrequencyCode;
                frequencyName = treatDto.FrequencyName;
            }

            if (treatDto.AdditionalItemsType != EAdditionalItemType.No)
            {
                doctorsAdvice.Additional = 1;
            }

            AdmissionRecordDto admissionRecordDto = await admissionRecordTask;
            if (admissionRecordDto != null)
            {
                doctorsAdvice.AreaCode = admissionRecordDto.AreaCode;
            }
            if (doctorsAdvice.CategoryCode == "Entrust")
            {
                await UpdatePatientInfoAsync(doctorsAdvice.Name, doctorsAdvice.PIID);
            }
            _ = await _doctorsAdviceRepository.InsertAsync(doctorsAdvice);

            Treat treatEntity = new Treat(
                id: GuidGenerator.Create(),
                specification: treatDto.Specification,
                frequencyCode: frequencyCode,
                frequencyName: frequencyName,
                feeTypeMainCode: treatDto.FeeTypeMainCode,
                feeTypeSubCode: treatDto.FeeTypeSubCode,
                doctorsAdviceId: id,
                otherPrice: treatDto.OtherPrice,
                additional: treatDto.Additional,
                usageCode: string.Empty,
                usageName: string.Empty,
                longDays: treatDto.LongDays,
                projectType: treatDto.ProjectType,
                projectName: treatDto.ProjectName,
                projectMerge: treatDto.ProjectMerge,
                treatId: treatDto.intTreatId,
                additionalItemsType: treatDto.AdditionalItemsType,
                additionalItemsId: treatDto.AdditionalItemsId);

            _ = await _treatRepository.InsertAsync(treatEntity);
            _ = await _medicalTechnologyMapRepository.InsertAsync(new MedicalTechnologyMap(treatEntity.Id, EDoctorsAdviceItemType.Treat));
            _ = await _doctorsAdviceMapRepository.InsertAsync(new DoctorsAdviceMap(doctorsAdvice.Id));

            return await Task.FromResult(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    /// <summary>
    /// 构建医嘱实体Entity
    /// </summary>
    /// <param name="main"></param>
    /// <param name="id"></param> 
    /// <param name="sub"></param>
    /// <param name="prescribe">如果是药品的情况传递过来</param>  
    /// <param name="source"></param>
    /// <returns></returns>
    private async Task<DoctorsAdvice> BuildDoctorsAdviceAsync(
        ModifyDoctorsAdviceBaseDto main,
        Guid id,
        ModifySubItemDto sub,
        Prescribe prescribe = null, EAddPrescribeSource source = EAddPrescribeSource.Single)
    {

        var recipeNo = sub.RecipeNo;

    newRecipe:
        if (sub.RecipeNo.Trim() == "" || sub.RecipeNo.Trim() == "0" || sub.RecipeNo.Trim().ToLower() == "auto")
        {
            recipeNo = (await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "RecipeNo")).ToString();
        }
        else
        {
            if (main.ItemType == EDoctorsAdviceItemType.Prescribe)
            {
                //如果设置了有内容的都是希望同组的，所以需要校验同组条件

                var groupRecipes = await (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => w.RecipeNo == sub.RecipeNo.Trim()).ToListAsync();

                //0. 找不到上一条记录，则没有办法同组 单条添加的情况需要清掉从新生成新的医嘱号，添加一组的时候过来的不需要 
                if (!groupRecipes.Any())
                {
                    _logger.LogInformation("[ 0 ]. 找不到上一条记录，则没有办法同组");
                    if (source == EAddPrescribeSource.Group)
                    {
                        //goup recipe
                        goto newAutoRecipe;
                    }
                    else
                    {
                        //single recipe
                        sub.RecipeNo = "";
                        goto newRecipe;
                    }
                }

                //1. 同组药品不能大于5条 
                if (groupRecipes.Count >= 5)
                {
                    _logger.LogInformation("[ 1 ]. 同组药品不能大于5条 ,大于5要不能再同组");
                    sub.RecipeNo = "";
                    goto newRecipe;
                }

                var lastEntity = groupRecipes.OrderByDescending(o => o.CreationTime).FirstOrDefault();

                //2. 状态必须一致并且在提交前
                if (lastEntity.Status > ERecipeStatus.Saved)
                {
                    _logger.LogInformation("[ 2 ]. 状态必须一致并且在提交前,已提交之后的所有状态都不能再同组");
                    sub.RecipeNo = "";
                    goto newRecipe;
                }
                //3. 精神药物不能合组
                if (prescribe != null && prescribe.MedicineId > 0)
                {
                    var toxicAny = await (await _toxicRepository.GetQueryableAsync()).Where(w => w.MedicineId == prescribe.MedicineId && w.ToxicLevel > 0).AnyAsync();
                    if (toxicAny)
                    {
                        //查找之前的同组药品是否有精神药物的  
                        var daids = await (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => w.RecipeNo == sub.RecipeNo).Select(s => s.Id).ToListAsync();
                        var medicineIds = await (await _prescribeRepository.GetQueryableAsync()).Where(w => daids.Contains(w.DoctorsAdviceId)).Select(s => s.MedicineId).ToListAsync();
                        var queryToxicAny = await (await _toxicRepository.GetQueryableAsync()).Where(w => medicineIds.Contains(w.MedicineId) && w.ToxicLevel > 0).AnyAsync();

                        if (queryToxicAny)
                        {
                            _logger.LogInformation("[ 3 ]. 精神药物和精神药物不能合组");
                            sub.RecipeNo = "";
                            goto newRecipe;
                        }
                    }
                }
                //4. 合组的药品必须保证【用法,频次,天数，药房】保存一致（计划时间改为最新一个） 
                // UsageCode,  FrequencyCode,  LongDays,  PharmacyCode,  IsCriticalPrescription

                var lastPrescribe = await _prescribeRepository.FirstOrDefaultAsync(w => w.DoctorsAdviceId == lastEntity.Id);

                if (prescribe.UsageCode != lastPrescribe.UsageCode
                    || prescribe.FrequencyCode != lastPrescribe.FrequencyCode
                    || prescribe.LongDays != lastPrescribe.LongDays
                    || prescribe.PharmacyCode != lastPrescribe.PharmacyCode
                    || prescribe.IsCriticalPrescription != lastPrescribe.IsCriticalPrescription)
                {
                    _logger.LogInformation("[ 3 ]. 4. 合组的药品必须保证【用法,频次,天数，药房 (UsageCode,  FrequencyCode,  LongDays,  PharmacyCode,  IsCriticalPrescription) 不一致】 不能合组");
                    sub.RecipeNo = "";
                    goto newRecipe;
                }

            }
        }

    newAutoRecipe:
        var recipeGroupNo = sub.RecipeGroupNo;
        if (recipeGroupNo == 0)
        {
            recipeGroupNo = await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), recipeNo.ToString());
        }

        var detailId = await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), DETAILID);

        return new DoctorsAdvice(
            detailId: detailId,
            id: id,
            platformType: main.PlatformType,
            piid: main.PIID,
            patientId: main.PatientId,
            patientName: main.PatientName,
            code: sub.Code,
            name: sub.Name,
            categoryCode: sub.RecipeCategoryCode,
            categoryName: sub.RecipeCategoryName,
            isBackTracking: sub.IsBackTracking,
            prescriptionNo: sub.PrescriptionNo,
            recipeNo: recipeNo.ToString(),
            recipeGroupNo: recipeGroupNo,
            applyTime: sub.ApplyTime.HasValue ? sub.ApplyTime.Value : DateTime.Now,
            applyDoctorCode: main.ApplyDoctorCode,
            applyDoctorName: main.ApplyDoctorName,
            applyDeptCode: main.ApplyDeptCode,
            applyDeptName: main.ApplyDeptName,
            traineeCode: main.TraineeCode,
            traineeName: main.TraineeName,
            payTypeCode: sub.PayTypeCode,
            payType: sub.PayType,
            price: sub.Price,
            unit: sub.Unit,
            amount: sub.Amount,
            insuranceCode: sub.InsuranceCode,
            insuranceType: sub.InsuranceType,
            isChronicDisease: main.IsChronicDisease,
            isRecipePrinted: sub.IsRecipePrinted,
            hisOrderNo: sub.HisOrderNo,
            diagnosis: main.Diagnosis,
            execDeptCode: sub.ExecDeptCode,
            execDeptName: sub.ExecDeptName,
            positionCode: sub.PositionCode,
            positionName: sub.PositionName,
            remarks: sub.Remarks,
            chargeCode: sub.ChargeCode,
            chargeName: sub.ChargeName,
            prescribeTypeCode: sub.PrescribeTypeCode,
            prescribeTypeName: sub.PrescribeTypeName,
            startTime: sub.StartTime,
            endTime: sub.EndTime,
            recieveQty: sub.RecieveQty,
            recieveUnit: sub.RecieveUnit,
            pyCode: sub.PyCode,
            wbCode: sub.WbCode,
            itemType: main.ItemType,
            isAdditionalPrice: sub.IsAdditionalPrice,
            sourceType: main.SourceType);
    }

    /// <summary>
    /// 保存记录[可以保存一组医嘱](目前需求只修改医嘱记录和开方的记录)
    /// </summary>
    /// <param name="doctorsAdvicesDto"></param>
    /// <returns></returns>
    [UnitOfWork]
    public async Task<Guid> SaveRecoredAsync(DoctorsAdvicesDto doctorsAdvicesDto)
    {
        if (doctorsAdvicesDto.ItemType == EDoctorsAdviceItemType.Prescribe && doctorsAdvicesDto.RecieveQty <= 0)
            Oh.Error("药品领量不合理，领量必须大于 0");
        if (doctorsAdvicesDto.ItemType == EDoctorsAdviceItemType.Treat && doctorsAdvicesDto.RecieveQty <= 0) Oh.Error("数量不能小于或等于 0");

        List<ERecipeStatus> status = new List<ERecipeStatus> { ERecipeStatus.Saved, ERecipeStatus.Rejected };
        DoctorsAdvice doctorsAdvice = await _doctorsAdviceRepository.FindAsync(x => x.Id == doctorsAdvicesDto.Id && status.Contains(x.Status));
        if (doctorsAdvice == null) Oh.Error("未查到需要修改的医嘱信息");

        List<DoctorsAdvice> syncAdvices = new List<DoctorsAdvice>();
        #region 更新主表内容
        //如果子号改变，这交换子号信息
        if (doctorsAdvice.RecipeGroupNo != doctorsAdvicesDto.RecipeGroupNo)
        {
            //希望交换的医嘱信息
            DoctorsAdvice otherAdvice = await _doctorsAdviceRepository.FindAsync(w => w.RecipeNo == doctorsAdvice.RecipeNo && w.RecipeGroupNo == doctorsAdvicesDto.RecipeGroupNo);
            if (otherAdvice != null)
            {
                otherAdvice.RecipeGroupNo = doctorsAdvice.RecipeGroupNo;
                _ = await _doctorsAdviceRepository.UpdateAsync(otherAdvice);
            }
        }

        decimal amount = doctorsAdvicesDto.GetAmount(doctorsAdvicesDto.PatientInfo.IsChildren(), out bool isAdditionalPrice);
        doctorsAdvice.Update(
            code: doctorsAdvicesDto.Code,
            name: doctorsAdvicesDto.Name,
            categoryCode: doctorsAdvicesDto.CategoryCode,
            categoryName: doctorsAdvicesDto.CategoryName,
            isBackTracking: doctorsAdvicesDto.IsBackTracking,
            prescriptionNo: doctorsAdvicesDto.PrescriptionNo,
            applyTime: doctorsAdvicesDto.ApplyTime,
            applyDoctorCode: doctorsAdvicesDto.ApplyDoctorCode,
            applyDoctorName: doctorsAdvicesDto.ApplyDoctorName,
            applyDeptCode: doctorsAdvicesDto.ApplyDeptCode,
            applyDeptName: doctorsAdvicesDto.ApplyDeptName,
            traineeCode: doctorsAdvicesDto.TraineeCode,
            traineeName: doctorsAdvicesDto.TraineeName,
            payTypeCode: doctorsAdvicesDto.PayTypeCode,
            payType: doctorsAdvicesDto.PayType,
            price: doctorsAdvicesDto.Price,
            amount: amount, //model.Amount,
            insuranceCode: doctorsAdvicesDto.InsuranceCode,
            insuranceType: doctorsAdvicesDto.InsuranceType,
            isChronicDisease: doctorsAdvicesDto.IsChronicDisease,
            isRecipePrinted: doctorsAdvicesDto.IsRecipePrinted,
            hisOrderNo: doctorsAdvicesDto.HisOrderNo,
            diagnosis: doctorsAdvicesDto.Diagnosis,
            positionCode: doctorsAdvicesDto.PositionCode,
            positionName: doctorsAdvicesDto.PositionName,
            remarks: doctorsAdvicesDto.Remarks,
            chargeCode: doctorsAdvicesDto.ChargeCode,
            chargeName: doctorsAdvicesDto.ChargeName,
            prescribeTypeCode: doctorsAdvicesDto.PrescribeTypeCode,
            prescribeTypeName: doctorsAdvicesDto.PrescribeTypeName,
            startTime: doctorsAdvicesDto.StartTime,
            endTime: doctorsAdvicesDto.EndTime,
            recieveQty: doctorsAdvicesDto.RecieveQty,
            recieveUnit: doctorsAdvicesDto.RecieveUnit,
            execDeptCode: doctorsAdvicesDto.ExecDeptCode,
            execDeptName: doctorsAdvicesDto.ExecDeptName,
            isAdditionalPrice: isAdditionalPrice);

        //重置医嘱号
        doctorsAdvice.RecipeGroupNo = doctorsAdvicesDto.RecipeGroupNo;
        doctorsAdvice.PrescriptionNo = string.Empty;
        syncAdvices.Add(doctorsAdvice);
        #endregion

        //组相关的医嘱集合
        List<DoctorsAdvice> groupList = await (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => w.RecipeNo == doctorsAdvicesDto.RecipeNo && w.Id != doctorsAdvicesDto.Id)
            .ToListAsync();

        var groupFirst = groupList.FirstOrDefault();

        //药方项
        if (doctorsAdvicesDto.ItemType == EDoctorsAdviceItemType.Prescribe)
        {
            var prescribe = await (await _prescribeRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.DoctorsAdviceId == doctorsAdvicesDto.Id);
            if (prescribe != null)
            {
                if (doctorsAdvicesDto.IsCriticalPrescription != prescribe.IsCriticalPrescription) Oh.Error("急诊药和非急诊药不能成组,请仔细开药");
                var qty = (prescribe.FrequencyTimes ?? 1) * prescribe.LongDays;
                var usageCode = prescribe.UsageCode;
                var skinTestSignChoseResult = prescribe.SkinTestSignChoseResult ?? ESkinTestSignChoseResult.No;
                var frequencyCode = prescribe.FrequencyCode;
                var longdays = prescribe.LongDays;
                //更新开方内容
                var syncPrescribe = new List<Prescribe>();

                #region prescribe.Update

                prescribe.Update(
                    isOutDrug: doctorsAdvicesDto.IsOutDrug,
                    medicineProperty: doctorsAdvicesDto.MedicineProperty,
                    toxicProperty: doctorsAdvicesDto.ToxicProperty,
                    usageCode: doctorsAdvicesDto.UsageCode,
                    usageName: doctorsAdvicesDto.UsageName,
                    speed: doctorsAdvicesDto.Speed,
                    longDays: doctorsAdvicesDto.LongDays ?? 1,
                    actualDays: doctorsAdvicesDto.ActualDays,
                    dosageForm: doctorsAdvicesDto.DosageForm,
                    dosageQty: doctorsAdvicesDto.DosageQty,
                    defaultDosageQty: doctorsAdvicesDto.DefaultDosageQty,
                    qtyPerTimes: doctorsAdvicesDto.QtyPerTimes,
                    dosageUnit: doctorsAdvicesDto.DosageUnit,
                    defaultDosageUnit: doctorsAdvicesDto.DefaultDosageUnit,
                    specification: doctorsAdvicesDto.Specification,
                    unpack: doctorsAdvicesDto.Unpack,
                    bigPackPrice: doctorsAdvicesDto.BigPackPrice,
                    bigPackFactor: doctorsAdvicesDto.BigPackFactor,
                    bigPackUnit: doctorsAdvicesDto.BigPackUnit,
                    smallPackPrice: doctorsAdvicesDto.SmallPackPrice,
                    smallPackUnit: doctorsAdvicesDto.SmallPackUnit,
                    smallPackFactor: doctorsAdvicesDto.SmallPackFactor,
                    frequencyCode: doctorsAdvicesDto.FrequencyCode,
                    frequencyName: doctorsAdvicesDto.FrequencyName,
                    medicalInsuranceCode: doctorsAdvicesDto.MedicalInsuranceCode,
                    frequencyTimes: doctorsAdvicesDto.FrequencyTimes,
                    frequencyUnit: doctorsAdvicesDto.FrequencyUnit,
                    frequencyExecDayTimes: doctorsAdvicesDto.FrequencyExecDayTimes,
                    dailyFrequency: doctorsAdvicesDto.DailyFrequency,
                    pharmacyCode: doctorsAdvicesDto.PharmacyCode,
                    pharmacyName: doctorsAdvicesDto.PharmacyName,
                    factoryName: doctorsAdvicesDto.FactoryName,
                    factoryCode: doctorsAdvicesDto.FactoryCode,
                    batchNo: doctorsAdvicesDto.BatchNo,
                    expirDate: doctorsAdvicesDto.ExpirDate,
                    isSkinTest: doctorsAdvicesDto.IsSkinTest,
                    skinTestResult: doctorsAdvicesDto.SkinTestResult,
                    skinTestSignChoseResult: doctorsAdvicesDto.SkinTestSignChoseResult,
                    materialPrice: doctorsAdvicesDto.MaterialPrice,
                    isBindingTreat: doctorsAdvicesDto.IsBindingTreat,
                    isAmendedMark: doctorsAdvicesDto.IsAmendedMark,
                    isAdaptationDisease: doctorsAdvicesDto.IsAdaptationDisease,
                    isFirstAid: doctorsAdvicesDto.IsFirstAid,
                    antibioticPermission: doctorsAdvicesDto.AntibioticPermission,
                    prescriptionPermission: doctorsAdvicesDto.PrescriptionPermission,
                    isCriticalPrescription: doctorsAdvicesDto.IsCriticalPrescription,
                    hisUnit: doctorsAdvicesDto.HisUnit,
                    hisDosageUnit: doctorsAdvicesDto.HisDosageUnit,
                    hisDosageQty: doctorsAdvicesDto.HisDosageQty);
                prescribe.MedicineId = doctorsAdvicesDto.MedicineId ?? 0;
                prescribe.DrugsLimit(prescribe.LimitType, prescribe.RestrictedDrugs);
                prescribe.SetHisDosageQty();

                //自定义一次剂量处理 TODO
                var dosageList = await _dosageAppService.GetAllCustomDosagesAsync();
                var dosage = dosageList.FirstOrDefault(w => w.Code == doctorsAdvicesDto.Code);
                if (dosage != null)
                {
                    var mydosageQty = dosage.GetHisDosageQty(doctorsAdvicesDto.DosageQty, doctorsAdvicesDto.DosageUnit);
                    prescribe.CommitHisDosageQty = mydosageQty;
                }

                #endregion

                syncPrescribe.Add(prescribe);
                if (groupList.Any())
                {
                    var groupAdviceIds = groupList.Select(w => w.Id).ToList();
                    var prescribes = await (await _prescribeRepository.GetQueryableAsync())
                        .Where(w => groupAdviceIds.Contains(w.DoctorsAdviceId)).ToListAsync();
                    foreach (var item in prescribes)
                    {
                        var oldLongDays = item.LongDays;
                        var oldFrequencyTimes = item.FrequencyTimes;

                        item.UpdateGroupAdviceProp(prescribe.FrequencyCode, prescribe.FrequencyName,
                            prescribe.FrequencyTimes,
                            prescribe.UsageCode, prescribe.UsageName, prescribe.LongDays, prescribe.ActualDays,
                            prescribe.MedicalInsuranceCode, prescribe.FrequencyExecDayTimes, prescribe.FrequencyUnit, doctorsAdvicesDto.DailyFrequency);
                        syncPrescribe.Add(item);

                        var newAdvice = groupList.FirstOrDefault(w => w.Id == item.DoctorsAdviceId);

                        newAdvice.UpdateGroupAdviceProp(
                            prescribeTypeCode: doctorsAdvicesDto.PrescribeTypeCode,
                            prescribeTypeName: doctorsAdvicesDto.PrescribeTypeName,
                            execDeptCode: doctorsAdvicesDto.ExecDeptCode,
                            execDeptName: doctorsAdvicesDto.ExecDeptName);

                        //当频次和天数改变时，触发
                        if (oldLongDays != doctorsAdvicesDto.LongDays || oldFrequencyTimes != doctorsAdvicesDto.FrequencyTimes)
                        {
                            newAdvice.ComputeQty(item); //重新换算领量的数量
                        }

                        syncAdvices.Add(newAdvice);
                    }
                }

                await _prescribeRepository.UpdateManyAsync(syncPrescribe);
                if (doctorsAdvice.PlatformType == EPlatformType.EmergencyTreatment)
                {
                    var modelIds = new List<Guid>() { doctorsAdvicesDto.Id };
                    //合组时处置和皮试是合并的，查询出来删除就可以，不用管皮试的上级是哪个药品
                    if (groupList.Any())
                    {
                        modelIds.AddRange(groupList.Select(s => s.Id).ToList());
                    }

                    var newQty = (doctorsAdvicesDto.FrequencyTimes ?? 1) * (doctorsAdvicesDto.LongDays ?? 1);

                    #region 用法附加项

                    if (usageCode != doctorsAdvicesDto.UsageCode || qty != newQty)
                    {
                        await DeleteAdditionalItemAsync(modelIds, EAdditionalItemType.UsageAdditional);
                        //查询药品用法是否有关联处置，有则默认开一条处置单
                        var treat = await _grpcMasterDataClient.GetTreatByUsageAsync(
                            new GetTreatByUsageCodeRequest()
                            { UsageCode = doctorsAdvicesDto.UsageCode });
                        if (!string.IsNullOrWhiteSpace(treat.TreatCode))
                        {
                            var recieveQty = (doctorsAdvicesDto.UsageCode == _configuration["UsageIntravenousDripCode"] &&
                                         (doctorsAdvicesDto.FrequencyCode.ToUpper() is "BID" or "TID"))
                                         ? (decimal)doctorsAdvicesDto.LongDays // BID/TID的 静脉输液按天数（每天一次），静脉接瓶按频次减一次再乘以天数
                                         : newQty;
                            await AdditionalTreatAsync(EAdditionalItemType.UsageAdditional, doctorsAdvicesDto, doctorsAdvice, treat, (int)recieveQty, (int)doctorsAdvicesDto.LongDays);
                        }
                    }

                    #endregion

                    #region 皮试附加项



                    //判断是否需要删除之前的皮试附加处置
                    if (skinTestSignChoseResult == ESkinTestSignChoseResult.Yes ||
                        doctorsAdvicesDto.SkinTestSignChoseResult == ESkinTestSignChoseResult.No)
                    {
                        await DeleteAdditionalItemAsync(modelIds, EAdditionalItemType.SkinAdditional);
                    }

                    //当医嘱时皮试时需要添加皮试附加处置
                    if (doctorsAdvicesDto.SkinTestSignChoseResult == ESkinTestSignChoseResult.Yes)
                    {
                        //查询药品用法是否有关联处置，有则默认开一条处置单
                        var treatResponse = await _grpcMasterDataClient.GetTreatProjectByCodeAsync(
                            new GetTreatProjectByCodeRequest
                            { Code = _configuration["SkinTreatCode"] });
                        var treatByCode = treatResponse.TreatProject;
                        if (treatByCode != null)
                        {
                            var doctorAdvices =
                                ObjectMapper.Map<DoctorsAdvice, ModifyDoctorsAdviceBaseDto>(doctorsAdvice);
                            doctorAdvices.ItemType = EDoctorsAdviceItemType.Treat;

                            _ = await AddTreatAsync(new AddTreatDto()
                            {
                                PatientInfo = doctorsAdvicesDto.PatientInfo,
                                DoctorsAdvice = doctorAdvices,
                                IsAddition = true,
                                Items = new TreatDto()
                                {
                                    Code = treatByCode.TreatCode,
                                    Name = treatByCode.TreatName,
                                    Unit = treatByCode.Unit,
                                    Price = decimal.Parse(treatByCode.Price.ToString("f3")),
                                    OtherPrice = decimal.Parse(treatByCode.OtherPrice.ToString("f3")),
                                    Specification = treatByCode.Specification,
                                    FeeTypeMainCode = treatByCode.FeeTypeMainCode,
                                    FeeTypeSubCode = treatByCode.FeeTypeSubCode,
                                    InsuranceCode = doctorsAdvicesDto.InsuranceCode,
                                    InsuranceType = doctorsAdvicesDto.InsuranceType,
                                    PayTypeCode = doctorsAdvicesDto.PayTypeCode,
                                    PayType = doctorsAdvicesDto.PayType,
                                    ApplyTime = doctorsAdvicesDto.ApplyTime,
                                    CategoryCode = treatByCode.DictionaryCode,
                                    CategoryName = treatByCode.DictionaryName,
                                    ExecDeptCode = doctorsAdvicesDto.ExecDeptCode,
                                    ExecDeptName = doctorsAdvicesDto.ExecDeptName,
                                    ChargeCode = treatByCode.ChargeCode,
                                    ChargeName = treatByCode.ChargeName,
                                    PrescribeTypeCode = doctorsAdvicesDto.PrescribeTypeCode,
                                    PrescribeTypeName = doctorsAdvicesDto.PrescribeTypeName,
                                    StartTime = doctorsAdvicesDto.StartTime,
                                    EndTime = doctorsAdvicesDto.EndTime,
                                    RecipeNo = "",
                                    RecieveQty = 1,
                                    RecieveUnit = treatByCode.Unit,
                                    ProjectType = treatByCode.CategoryCode,
                                    ProjectName = treatByCode.CategoryName,
                                    ProjectMerge = treatByCode.ProjectMerge,
                                    PyCode = treatByCode.PyCode,
                                    WbCode = treatByCode.WbCode,
                                    intTreatId = treatByCode.Id,
                                    AdditionalItemsType = EAdditionalItemType.SkinAdditional,
                                    UsageCode = "",
                                    UsageName = "",
                                    AdditionalItemsId = doctorsAdvicesDto.Id,
                                    Additional = treatByCode.Additional,
                                }
                            });
                        }
                    }

                    #endregion

                    #region 静脉滴注附加项

                    //判断当药品的用法不相等要删除附加处置
                    if (usageCode != doctorsAdvicesDto.UsageCode || frequencyCode != doctorsAdvicesDto.FrequencyCode || longdays != doctorsAdvicesDto.LongDays)
                    {
                        await DeleteAdditionalItemAsync(modelIds, EAdditionalItemType.FrequencyAdditional);
                        // 门诊静脉接瓶
                        await AdditionalIntravenousInfusionAsync(doctorsAdvicesDto, doctorsAdvice);
                    }

                    #endregion



                }
            }

            //院前不需要做药品库存校验，他们自己车上带有
            if (doctorsAdvicesDto.PlatformType == EPlatformType.EmergencyTreatment)
            {
                var drugStock = await _hospitalClientAppService.QueryHisDrugStockAsync(new DrugStockQueryRequest
                { QueryType = 2, QueryCode = doctorsAdvicesDto.Code, Storage = int.Parse(doctorsAdvicesDto.PharmacyCode) });
                var drugStockEntity =
                    await (await _drugStockQueryRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.DoctorsAdviceId == doctorsAdvice.Id);
                if (drugStockEntity != null && drugStock.Count > 0)
                {
                    var drugStockQuery =
                        ObjectMapper.Map<DrugStockQueryResponse, DrugStockQuery>(drugStock.FirstOrDefault());
                    drugStockQuery.SetDoctorsAdviceId(doctorsAdvice.Id);
                    drugStockEntity.Update(drugStockQuery);
                    await _drugStockQueryRepository.UpdateAsync(drugStockEntity);
                }
            }
        }

        //诊疗项
        if (doctorsAdvicesDto.ItemType == EDoctorsAdviceItemType.Treat)
        {
            var treat = await (await _treatRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.DoctorsAdviceId == doctorsAdvicesDto.Id);
            if (treat != null)
            {
                doctorsAdvicesDto.SetTreanFrequency();
                treat.Update(
                    specification: doctorsAdvicesDto.Specification,
                    frequencyCode: doctorsAdvicesDto.FrequencyCode,
                    frequencyName: doctorsAdvicesDto.FrequencyName,
                    feeTypeMainCode: doctorsAdvicesDto.FeeTypeMainCode,
                    feeTypeSubCode: doctorsAdvicesDto.FeeTypeSubCode,
                    otherPrice: doctorsAdvicesDto.OtherPrice,
                    additional: doctorsAdvicesDto.Additional,
                    usageCode: doctorsAdvicesDto.UsageCode,
                    usageName: doctorsAdvicesDto.UsageName,
                    longDays: doctorsAdvicesDto.LongDays ?? 1,
                    projectType: doctorsAdvicesDto.ProjectType,
                    projectName: doctorsAdvicesDto.ProjectName,
                    projectMerge: doctorsAdvicesDto.ProjectMerge,
                    additionalItemsType: doctorsAdvicesDto.AdditionalItemsType,
                    additionalItemsId: doctorsAdvicesDto.AdditionalItemsId,
                    treatId: doctorsAdvicesDto.intTreatId);
                _ = await _treatRepository.UpdateAsync(treat);
            }
        }

        //await _doctorsAdviceRepository.UpdateAsync(entity); 
        await _doctorsAdviceRepository.UpdateManyAsync(syncAdvices);
        return await Task.FromResult(doctorsAdvice.Id);
    }

    /// <summary>
    /// 静滴（静默注射）附加“门诊静脉接瓶”处置
    /// </summary>
    /// <param name="doctorsAdvicesDto"></param>
    /// <param name="doctorsAdvice"></param>
    /// <returns></returns>
    async Task AdditionalIntravenousInfusionAsync(DoctorsAdvicesDto doctorsAdvicesDto, DoctorsAdvice doctorsAdvice)
    {
        if (doctorsAdvicesDto.UsageCode == _configuration["UsageIntravenousDripCode"] &&
            doctorsAdvicesDto.FrequencyCode.ToUpper() is "BID" or "TID")
        {
            //查询药品用法是否有关联处置，有则默认开一条处置单
            var treatResponse = await _grpcMasterDataClient.GetTreatProjectByCodeAsync(
                new GetTreatProjectByCodeRequest
                { Code = _configuration["ContinuousIntravenousInfusionCode"] });
            var treatByCode = treatResponse.TreatProject;
            var recieveQty = (doctorsAdvicesDto.FrequencyCode.ToUpper() is "BID" ? 1 : 2) * (int)doctorsAdvicesDto.LongDays; // BID/TID的 静脉输液按天数（每天一次），静脉接瓶按频次减一次再乘以天数
            await AdditionalTreatAsync(EAdditionalItemType.FrequencyAdditional, doctorsAdvicesDto, doctorsAdvice, treatByCode, recieveQty, (int)doctorsAdvicesDto.LongDays);
        }
    }

    /// <summary>
    /// 添加附加处置项目
    /// </summary>
    /// <param name="frequencyAdditional"></param>
    /// <param name="doctorsAdvicesDto"></param>
    /// <param name="doctorsAdvice"></param>
    /// <param name="treatByCode"></param>
    /// <param name="recieveQty"></param>
    /// <param name="longday"></param>
    /// <returns></returns>
    async Task AdditionalTreatAsync(EAdditionalItemType frequencyAdditional, DoctorsAdvicesDto doctorsAdvicesDto, DoctorsAdvice doctorsAdvice, GrpcTreatProjectModel treatByCode, int recieveQty, int longday)
    {
        if (treatByCode != null)
        {
            var doctorAdvices =
                ObjectMapper.Map<DoctorsAdvice, ModifyDoctorsAdviceBaseDto>(doctorsAdvice);
            doctorAdvices.ItemType = EDoctorsAdviceItemType.Treat;

            _ = await AddTreatAsync(new AddTreatDto()
            {
                PatientInfo = doctorsAdvicesDto.PatientInfo,
                DoctorsAdvice = doctorAdvices,
                IsAddition = true,
                Items = new TreatDto()
                {
                    Code = treatByCode.TreatCode,
                    Name = treatByCode.TreatName,
                    Unit = treatByCode.Unit,
                    Price = decimal.Parse(treatByCode.Price.ToString("f3")),
                    OtherPrice = decimal.Parse(treatByCode.OtherPrice.ToString("f3")),
                    Specification = treatByCode.Specification,
                    FeeTypeMainCode = treatByCode.FeeTypeMainCode,
                    FeeTypeSubCode = treatByCode.FeeTypeSubCode,
                    InsuranceCode = doctorsAdvicesDto.InsuranceCode,
                    InsuranceType = doctorsAdvicesDto.InsuranceType,
                    PayTypeCode = doctorsAdvicesDto.PayTypeCode,
                    PayType = doctorsAdvicesDto.PayType,
                    ApplyTime = doctorsAdvicesDto.ApplyTime,
                    CategoryCode = treatByCode.DictionaryCode,
                    CategoryName = treatByCode.DictionaryName,
                    ExecDeptCode = doctorsAdvicesDto.ExecDeptCode,
                    ExecDeptName = doctorsAdvicesDto.ExecDeptName,
                    ChargeCode = treatByCode.ChargeCode,
                    ChargeName = treatByCode.ChargeName,
                    PrescribeTypeCode = doctorsAdvicesDto.PrescribeTypeCode,
                    PrescribeTypeName = doctorsAdvicesDto.PrescribeTypeName,
                    StartTime = doctorsAdvicesDto.StartTime,
                    EndTime = doctorsAdvicesDto.EndTime,
                    RecipeNo = "",
                    RecieveQty = recieveQty,
                    RecieveUnit = treatByCode.Unit,
                    ProjectType = treatByCode.CategoryCode,
                    ProjectName = treatByCode.CategoryName,
                    ProjectMerge = treatByCode.ProjectMerge,
                    PyCode = treatByCode.PyCode,
                    WbCode = treatByCode.WbCode,
                    intTreatId = treatByCode.Id,
                    AdditionalItemsType = frequencyAdditional,
                    UsageCode = "",
                    UsageName = "",
                    LongDays = longday,
                    AdditionalItemsId = doctorsAdvicesDto.Id,
                    Additional = treatByCode.Additional,
                }
            });
        }
    }

    /// <summary>
    /// 删除附加处置
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="type"></param>
    private async Task DeleteAdditionalItemAsync(List<Guid> ids, EAdditionalItemType type)
    {
        var treat =
            await (await _treatRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                ids.Contains(x.AdditionalItemsId) &&
                x.AdditionalItemsType == type);
        if (treat != null)
        {
            var treatDoctorAdvice =
                await (await _doctorsAdviceRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                    x.Id == treat.DoctorsAdviceId);
            await _treatRepository.DeleteAsync(treat);
            if (treatDoctorAdvice != null)
                await _doctorsAdviceRepository.DeleteAsync(treatDoctorAdvice);
        }
    }

    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<SubmitDoctorsAdviceResponse> SubmitAsync(SubmitDoctorsAdviceDto model)
    {
        SubmitDoctorsAdviceResponse response = new SubmitDoctorsAdviceResponse();
        if (AbpStringExtensions.IsNullOrWhiteSpace(model.VisSerialNo) &&
            model.PlatformType == EPlatformType.EmergencyTreatment)
            Oh.Error("没有就诊流水号，请重新保存诊断");

        List<ERecipeStatus> status = new List<ERecipeStatus> { ERecipeStatus.Saved, ERecipeStatus.Rejected };
        //查询提交的医嘱是否有附加处置
        List<Treat> treatList = await _treatRepository.GetListAsync(x => x.AdditionalItemsType != EAdditionalItemType.No && model.Ids.Contains(x.AdditionalItemsId));
        if (treatList.Any())
        {
            List<Guid> ids = treatList.Select(s => s.DoctorsAdviceId).ToList();
            model.Ids.AddRange(ids);
        }

        // 提交的时候如果有组的医嘱，需要将整组一起提交△
        IEnumerable<string> recipeNos = (await _doctorsAdviceRepository.GetListAsync(x => model.Ids.Contains(x.Id) && status.Contains(x.Status)))
            .Select(s => s.RecipeNo)
            .Distinct();
        if (!recipeNos.Any()) Oh.Error("没有提交数据");

        List<DoctorsAdvice> updateEntities = await _doctorsAdviceRepository.GetListAsync(w => w.PlatformType == model.PlatformType && recipeNos.Contains(w.RecipeNo));
        IEnumerable<Guid> updateIds = updateEntities.Select(x => x.Id);
        model.Ids.AddRange(updateIds);
        model.Ids = model.Ids.Distinct().ToList();

        List<EDoctorsAdviceItemType> itemtypes = new List<EDoctorsAdviceItemType>()
            { EDoctorsAdviceItemType.Prescribe, EDoctorsAdviceItemType.Lis, EDoctorsAdviceItemType.Pacs };
        //int updateEntitiesCount = updateEntities.Where(w => itemtypes.Contains(w.ItemType)).Count();

        // 更新药理信息 （直接更新）
        await ModifyToxicAsync(updateEntities);

        //分方 
        await SplitPrescriptionAsync(updateEntities, model);
        //return new List<Guid>();  //分方测试跳过下面的内容

        string doctor = CurrentUser.UserName;
        if (doctor.IsNullOrEmpty()) Oh.Error("未能获取医生信息");

        int count = updateEntities.Where(w => w.ApplyDoctorCode == doctor).Count();
        if (updateEntities.Count > count) Oh.Error("不能提交其他医生开立的医嘱");

        //生成一个序列号
        var commitSerialNo = await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "commitSerialNo");

        updateEntities.ForEach(x =>
        {
            x.UpdateStatus(ERecipeStatus.Submitted);
            x.CreateCommitSerialNo(commitSerialNo);
            if (x.CategoryCode == "Entrust")
            {
                x.PayStatus = EPayStatus.HavePaid;
            }
        }); //全部状态设为提交状态

        var prescriptionNos = updateEntities.Select(x => x.PrescriptionNo).Distinct();
        foreach (var prescriptionNo in prescriptionNos)
        {
            var advices = updateEntities.Where(x => x.PrescriptionNo == prescriptionNo).GroupBy(x => x.RecipeNo);
            int counter = 1;
            foreach (var item in advices)
            {
                item.ToList().ForEach(x => x.SequenceNo = counter);
                counter++;
            }
        }

        await _doctorsAdviceRepository.UpdateManyAsync(updateEntities);
        List<Guid> doctorsAdviceIds = updateEntities.Select(s => s.Id).ToList();

        List<Prescribe> prescribes = await _prescribeRepository.GetListAsync(x => doctorsAdviceIds.Contains(x.DoctorsAdviceId));
        List<Treat> treats = await _treatRepository.GetListAsync(x => doctorsAdviceIds.Contains(x.DoctorsAdviceId));
        if (treats.Any(x => x.AdditionalItemsType != EAdditionalItemType.No && !model.Ids.Distinct().Contains(x.AdditionalItemsId)))
        {
            Oh.Error(message: "药品附加处置项不能单独提交");
        }

        List<Pacs> pacses = await (await _pacsRepository.GetQueryableAsync()).Include(x => x.PacsItems).Where(w => doctorsAdviceIds.Contains(w.DoctorsAdviceId))
            .ToListAsync();
        List<Lis> lises = await (await _lisRepository.GetQueryableAsync()).Include(x => x.LisItems).Where(w => doctorsAdviceIds.Contains(w.DoctorsAdviceId))
            .ToListAsync();

        var eto = new List<SubmitDoctorsAdviceEto>();

        foreach (var item in updateEntities)
        {
            var submit = new SubmitDoctorsAdviceEto
            {
                DoctorsAdvice = ObjectMapper.Map<DoctorsAdvice, DoctorsAdviceEto>(item)
            };

            switch (item.ItemType)
            {
                case EDoctorsAdviceItemType.Prescribe: //处方
                    var prescribe = prescribes.FirstOrDefault(w => w.DoctorsAdviceId == item.Id);
                    if (prescribe != null) submit.Prescribe = ObjectMapper.Map<Prescribe, PrescribeEto>(prescribe);
                    break;
                case EDoctorsAdviceItemType.Pacs: //检查
                    var pacs = pacses.FirstOrDefault(w => w.DoctorsAdviceId == item.Id);
                    if (pacs != null) submit.Pacs = ObjectMapper.Map<Pacs, PacsEto>(pacs);
                    break;
                case EDoctorsAdviceItemType.Lis: //检验
                    var lis = lises.FirstOrDefault(w => w.DoctorsAdviceId == item.Id);
                    if (lis != null) submit.Lis = ObjectMapper.Map<Lis, LisEto>(lis);
                    break;
                case EDoctorsAdviceItemType.Treat: //诊疗
                    var treat = treats.FirstOrDefault(w => w.DoctorsAdviceId == item.Id);
                    if (treat != null) submit.Treat = ObjectMapper.Map<Treat, TreatEto>(treat);
                    break;
                default:
                    break;
            }

            eto.Add(submit);
        }

        //1.提交医嘱推送消息
        await PushSubmitAdviceMessageAsync(eto);
        //2.自备药
        await PushOwnMedicineAsync(model);

        //时间节点记录（包含已删除数据）
        await TimeAxisRecoredAsync(model, updateEntities);

        await UnitOfWorkManager.Current.SaveChangesAsync();

        #region 发送医嘱信息给医院

        if (model.PlatformType == EPlatformType.EmergencyTreatment)
        {
            var isChildren = false;
            try
            {
                isChildren = model.PatientInfo.IsChildren();
            }
            catch
            {
                isChildren = false;
                var msg = model.PatientInfo == null ? "没有传身份证" : $"身份证 {model.PatientInfo.PatientIDCard} 不合法";
                _logger.LogError($"获取身份证信息异常:" + msg);
            }

            //发送医嘱信息给医院
            var sendMedical = await SendMedicalInfoAsync(model, updateEntities, isChildren);

            response.Orders = sendMedical;
        }

        #endregion

        response.Ids = doctorsAdviceIds;
        return response;
    }

    /// <summary>
    /// 更新药理信息
    /// </summary>
    /// <param name="doctorsAdvices"></param>
    /// <returns></returns> 
    private async Task ModifyToxicAsync(List<DoctorsAdvice> doctorsAdvices)
    {
        List<string> medicineCodes = doctorsAdvices.Where(x => x.ItemType == EDoctorsAdviceItemType.Prescribe).Select(x => x.Code).ToList();
        if (medicineCodes.Any())
        {
            MedicineIdsRequest medicineIdsRequest = new MedicineIdsRequest();
            medicineIdsRequest.MedicineIds.AddRange(medicineCodes);
            ToxicListResponse toxicResponse = await _grpcMasterDataClient.GetToxicListByMedicineIdsAsync(medicineIdsRequest);
            var toxics = toxicResponse.ToxicList;

            if (toxics.Any())
            {
                List<int> medicineIds = toxics.Select(s => s.MedicineId).ToList();
                List<Toxic> existsToxic = await _toxicRepository.GetListAsync(x => medicineIds.Contains(x.MedicineId)); //查询出已经存在的药理信息

                IEnumerable<Guid> doctorsAdviceIds = doctorsAdvices.Select(x => x.Id);
                List<Prescribe> prescribes = await _prescribeRepository.GetListAsync(x => doctorsAdviceIds.Contains(x.DoctorsAdviceId));

                List<Prescribe> updatePrescribes = new List<Prescribe>();
                List<Toxic> newToxics = new List<Toxic>();
                foreach (ToxicListModel toxic in toxics)
                {
                    bool exists = existsToxic.Exists(w => w.MedicineId == toxic.MedicineId);
                    //如果不存在则新增一条
                    if (!exists)
                    {
                        Toxic toxicEntity = new Toxic(
                            id: GuidGenerator.Create(),
                            medicineId: toxic.MedicineId,
                            isSkinTest: toxic.IsSkinTest,
                            isCompound: toxic.IsCompound,
                            isDrunk: toxic.IsDrunk,
                            toxicLevel: toxic.ToxicLevel,
                            isHighRisk: toxic.IsHighRisk,
                            isRefrigerated: toxic.IsRefrigerated,
                            isTumour: toxic.IsTumour,
                            antibioticLevel: toxic.AntibioticLevel,
                            isPrecious: toxic.IsPrecious,
                            isInsulin: toxic.IsInsulin,
                            isAnaleptic: toxic.IsAnaleptic,
                            isAllergyTest: toxic.IsAllergyTest,
                            isLimited: toxic.IsLimited,
                            limitedNote: toxic.LimitedNote);
                        newToxics.Add(toxicEntity);
                    }

                    DoctorsAdvice doctorsAdvice = doctorsAdvices.FirstOrDefault(x => x.Code == toxic.Code);
                    if (doctorsAdvice == null) continue;
                    Prescribe prescribe = prescribes.FirstOrDefault(x => x.DoctorsAdviceId == doctorsAdvice.Id);
                    if (prescribe == null) continue;
                    if (prescribe.MedicineId != toxic.MedicineId)
                    {
                        prescribe.MedicineId = toxic.MedicineId;
                    }
                }

                if (newToxics.Any()) await _toxicRepository.InsertManyAsync(newToxics, autoSave: true);

                if (prescribes.Any()) await _prescribeRepository.UpdateManyAsync(prescribes, autoSave: true);
            }
        }
    }

    /// <summary>
    /// 分方算法
    /// </summary>
    /// <param name="updateEntities"></param>
    /// <param name="model"></param>
    /// <returns></returns> 
    private async Task SplitPrescriptionAsync(List<DoctorsAdvice> updateEntities, SubmitDoctorsAdviceDto model)
    {
        string columnName = "myPrescriptionNo"; //列名称

        /*
        * 0.四大分类分处方单，检查单，检验单，诊疗单四大类
        * 1.处方单成组同方，一单最多五条，同组不能超过5条，未分组可以堆叠在一张单上
        * 2.检查，检验单打印到一起
        * 3.诊疗
        * 
        */
        var dic = new Dictionary<int, List<DoctorsAdvice>>();

        //Prescribe 药品
        var medicines = updateEntities.Where(w => w.ItemType == EDoctorsAdviceItemType.Prescribe && w.SourceType != 1 && w.SourceType != 8).ToList();

        if (medicines.Any())
        {
            var ids = medicines.Select(s => s.Id).Distinct().ToList();
            var prescribes = await (await _prescribeRepository.GetQueryableAsync()).Where(w => ids.Contains(w.DoctorsAdviceId)).ToListAsync();

            var medicineIds = prescribes.Select(s => s.MedicineId).ToList();

            var toxicMedicine = await (from p in (await _prescribeRepository.GetQueryableAsync())
                                       join t in (await _toxicRepository.GetQueryableAsync())
                                           on p.MedicineId equals t.MedicineId
                                       where t.ToxicLevel > 0 && medicineIds.Contains(p.MedicineId) && ids.Contains(p.DoctorsAdviceId)
                                       select new
                                       {
                                           p.Id,
                                           p.DoctorsAdviceId,
                                           p.MedicineId,
                                           t.ToxicLevel
                                       }).ToListAsync();

            var drugs = prescribes;
            if (toxicMedicine.Any())
            {
                //药理分方的药品信息
                var doctorsAdviceIds = toxicMedicine.Select(s => s.DoctorsAdviceId).ToList();
                //获取毒理麻药成组的数据
                var toxicAdvices =
                    await (from a in (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => doctorsAdviceIds.Contains(w.Id))
                           join b in (await _doctorsAdviceRepository.GetQueryableAsync())
                               on a.RecipeNo equals b.RecipeNo
                           select a).ToListAsync();

                if (toxicAdvices.Any())
                {
                    var toxicAdvicesIds = toxicAdvices.Select(x => x.Id);
                    var toxicAdvicesPrescribes = prescribes.Where(x => toxicAdvicesIds.Contains(x.DoctorsAdviceId));
                    var insuranceIds = toxicAdvicesPrescribes.Where(x => x.RestrictedDrugs > 0).Select(x => x.DoctorsAdviceId);
                    var insuranceToxicAdvices = toxicAdvices.Where(x => insuranceIds.Contains(x.Id));
                    var recipes = insuranceToxicAdvices.GroupBy(g => g.RecipeNo).ToList();
                    var unInsuranceToxicAdvices = toxicAdvices.Where(x => !insuranceIds.Contains(x.Id));
                    var recipes1 = unInsuranceToxicAdvices.GroupBy(g => g.RecipeNo);

                    recipes.AddRange(recipes1);

                    foreach (var item in recipes)
                    {
                        var myPrescriptionNo =
                            await _mySequenceRepository.GetSequenceAsync(nameof(Prescription), columnName); //生成一个序列
                        dic.Add(myPrescriptionNo, item.ToList());
                    }

                    var drugsid = toxicAdvices.Select(s => s.Id).ToList();

                    //如果有毒理麻醉药之类的重新真理普通处方单集合
                    drugs = prescribes.Where(w => !drugsid.Contains(w.DoctorsAdviceId)).ToList();
                }
            }

            var insuranceDrugs = drugs.Where(x => (int?)x.RestrictedDrugs > 0);
            var unInsuranceDrugs = drugs.Where(x => (int?)x.RestrictedDrugs < 1 || x.RestrictedDrugs is null);

            var pharmayGroup = insuranceDrugs.GroupBy(g => new { g.PharmacyCode, g.IsCriticalPrescription, g.MedicineProperty }).ToList(); //分方:药房->危急 ->medicineproperty (北大需求)
            var pharmayGroup1 = unInsuranceDrugs.GroupBy(g => new { g.PharmacyCode, g.IsCriticalPrescription, g.MedicineProperty });
            pharmayGroup.AddRange(pharmayGroup1);

            //先根据药房分方
            foreach (var group in pharmayGroup)
            {
                var list = group.ToList();

                var partPrescribe = list.Select(s => s.DoctorsAdviceId).ToList();
                int groupCount = medicines.Where(x => partPrescribe.Contains(x.Id)).Select(x => x.RecipeNo).Distinct().Count();
                if (groupCount <= 5 && list.Count <= 8)
                {
                    //五个，不管是分组还是散的都分在一个药方
                    var myPrescriptionNo =
                        await _mySequenceRepository.GetSequenceAsync(nameof(Prescription), columnName); //生成一个序列  
                    List<DoctorsAdvice> currentMedicines = medicines.Where(w => partPrescribe.Contains(w.Id)).ToList();
                    dic.Add(myPrescriptionNo, currentMedicines);
                }
                else
                {
                    //大于5个需要根据 毒精麻 毒理分组，根据用药途径分组
                    //全部认为是非毒理药物

                    var partAdviceids = medicines.Where(w => partPrescribe.Contains(w.Id)).ToList();

                    var groupPartAdvice =
                        partAdviceids.GroupBy(g => g.RecipeNo).OrderBy(o => o.Count()); //从小到大排序，方便入队的时候小的压在下面

                    var attenuator = 8; //衰减计数器，默认5个，当5个都完全没有符合的情况，就衰减到4个，4个也无法满足就衰减到3个，以此类推 
                    int groupAttenuator = 5;
                    var needAttenuator = false;

                    Stack stack = new Stack();
                    Queue queue = new Queue();

                    foreach (var gpa in groupPartAdvice)
                    {
                        stack.Push(gpa);
                    }

                    var onePrescription = new List<DoctorsAdvice>(); //一个方子
                    bool fromStack = true; //默认从栈中获取

                    while (true)
                    {
                        if (stack.Count == 0 && queue.Count == 0) break;
                        if (stack.Count == 0) fromStack = false;

                        //从队列中获取，否则从队列中获取
                        if (fromStack)
                        {
                            if (needAttenuator && onePrescription.Count() == attenuator)
                            {
                                needAttenuator = false;
                                var myPrescriptionNo =
                                    await _mySequenceRepository.GetSequenceAsync(nameof(Prescription),
                                        columnName); //生成一个序列
                                dic.Add(myPrescriptionNo, onePrescription);
                                onePrescription = new List<DoctorsAdvice>(); //置空 
                            }

                            var adviceQ1 = stack.Pop() as IGrouping<string, DoctorsAdvice>;
                            var count = onePrescription.Count() + adviceQ1.Count();
                            var groupCounts = onePrescription.Select(x => x.RecipeNo).Distinct().Count() + 1;

                            if (count < attenuator && groupCounts < groupAttenuator)
                            {
                                foreach (var item in adviceQ1)
                                {
                                    onePrescription.Add(item);
                                }

                                if (stack.Count == 0)
                                {
                                    var myPrescriptionNo =
                                        await _mySequenceRepository.GetSequenceAsync(nameof(Prescription),
                                            columnName); //生成一个序列
                                    dic.Add(myPrescriptionNo, onePrescription);
                                    onePrescription = new List<DoctorsAdvice>(); //置空
                                    fromStack = false; //切换到队列
                                }
                            }
                            else if (count == attenuator || groupCounts == groupAttenuator)
                            {
                                foreach (var item in adviceQ1)
                                {
                                    onePrescription.Add(item);
                                }

                                var myPrescriptionNo =
                                    await _mySequenceRepository.GetSequenceAsync(nameof(Prescription),
                                        columnName); //生成一个序列
                                dic.Add(myPrescriptionNo, onePrescription);
                                onePrescription = new List<DoctorsAdvice>(); //置空 
                                if (stack.Count == 0)
                                {
                                    fromStack = false; //切换到队列 
                                }
                                else
                                {
                                    if (queue.Count > 0)
                                    {
                                        fromStack = false; //切换到队列 
                                    }
                                }
                            }
                            else
                            {
                                queue.Enqueue(adviceQ1);
                                continue;
                            }
                        }
                        else
                        {
                            if (stack.Count == 0)
                            {
                                attenuator -= 1;
                                needAttenuator = true;
                            }

                            var count = queue.Count;
                            for (int i = 0; i < count; i++)
                            {
                                var q = queue.Dequeue() as IGrouping<string, DoctorsAdvice>;
                                stack.Push(q);
                            }

                            fromStack = true;
                        }
                    }
                }
            }
        }

        var medicines1 = updateEntities.Where(w => w.ItemType == EDoctorsAdviceItemType.Prescribe && (w.SourceType == 1 || w.SourceType == 8)).GroupBy(x => x.PrescriptionFlag);
        foreach (var item in medicines1)
        {
            var myPrescriptionNo =
                await _mySequenceRepository.GetSequenceAsync(nameof(Prescription), columnName); //生成一个序列  
            dic.Add(myPrescriptionNo, item.ToList());
        }

        //Lis 检验
        var lises = updateEntities.Where(w => w.ItemType == EDoctorsAdviceItemType.Lis).ToList();

        if (lises.Count() > 0)
        {
            GetLabReportInfoCodes labReportInfoCodes = new GetLabReportInfoCodes();
            labReportInfoCodes.Code.AddRange(lises.Select(s => s.Code));
            LabReportInfos labReportInfos = _grpcMasterDataClient.GetLabReportInfo(labReportInfoCodes);

            IEnumerable<LabReportInfoModel> labReportInfoModels1 = labReportInfos.LabReportInfolist.Where(x => x.ExecDeptName == "遗传组");
            foreach (var item in labReportInfoModels1)
            {
                var lises1 = lises.Where(x => x.Code == item.Code).ToList();
                if (!lises1.Any()) continue;
                var myPrescriptionNo = await _mySequenceRepository.GetSequenceAsync(nameof(Prescription), columnName);
                dic.Add(myPrescriptionNo, lises1);
                lises.RemoveAll(lises1);
            }

            IEnumerable<LabReportInfoModel> labReportInfoModels2 = labReportInfos.LabReportInfolist.Where(x => x.CatelogName.Contains("采血"));
            var codes = labReportInfoModels2.Select(x => x.Code);
            var lises2 = lises.Where(x => codes.Contains(x.Code)).ToList();
            if (lises2.Any())
            {
                var myPrescriptionNo1 = await _mySequenceRepository.GetSequenceAsync(nameof(Prescription), columnName);
                dic.Add(myPrescriptionNo1, lises2);
                lises.RemoveAll(lises2);
            }

            if (lises.Any())
            {
                var myPrescriptionNo2 = await _mySequenceRepository.GetSequenceAsync(nameof(Prescription), columnName);
                dic.Add(myPrescriptionNo2, lises);
            }
        }

        //Pacs 检查
        var pacses = updateEntities.Where(w => w.ItemType == EDoctorsAdviceItemType.Pacs).ToList();

        if (pacses.Count() > 0)
        {
            List<DoctorsAdvice> cts = pacses.Where(x => x.Name.Contains("CT") && x.Name.Contains("三维重建")).ToList();
            if (cts.Any())
            {
                pacses.RemoveAll(cts);
                DoctorsAdvice ct = pacses.FirstOrDefault(x => x.Name.Contains("CT"));
                if (ct != null)
                {
                    pacses.Remove(ct);
                    cts.Add(ct);
                }
                int myPrescriptionNo = await _mySequenceRepository.GetSequenceAsync(nameof(Prescription), columnName); //生成一个序列
                dic.Add(myPrescriptionNo, cts);
            }

            foreach (var item in pacses)
            {
                var myPrescriptionNo = await _mySequenceRepository.GetSequenceAsync(nameof(Prescription), columnName); //生成一个序列
                dic.Add(myPrescriptionNo, new List<DoctorsAdvice>() { item });
            }
        }

        var treats = updateEntities.Where(w => w.ItemType == EDoctorsAdviceItemType.Treat).ToList();

        //诊疗分方
        if (treats.Count() > 0)
        {
            var ids = treats.Select(x => x.Id);
            var trs = await _treatRepository.GetListAsync(x => ids.Contains(x.DoctorsAdviceId));

            //治疗收费单
            var consultationCode = _configuration["Consultation"];
            var consultations = treats.Where(w => w.Code == consultationCode).GroupBy(g => g.Id);       //急诊诊查费
            var exitIds = new List<Guid>();
            foreach (var item in consultations)
            {
                var myPrescriptionNo = await _mySequenceRepository.GetSequenceAsync(nameof(Prescription), columnName); //生成一个序列
                dic.Add(myPrescriptionNo, item.ToList());
                exitIds.Add(item.Key);
            }

            var attachTreats = treats.Where(x => x.SourceType == 1).GroupBy(x => x.Id);
            foreach (var item in attachTreats)
            {
                var myPrescriptionNo = await _mySequenceRepository.GetSequenceAsync(nameof(Prescription), columnName); //生成一个序列
                dic.Add(myPrescriptionNo, item.ToList());
                exitIds.Add(item.Key);
            }

            trs = trs.Where(x => !exitIds.Contains(x.DoctorsAdviceId)).ToList();
            var trGroups = trs.GroupBy(x => x.AdditionalItemsId);

            foreach (var trGroup in trGroups)
            {
                var docIds = trGroup.ToList().Select(x => x.DoctorsAdviceId);
                //治疗收费单之外的单
                var groupTreats = treats.Where(x => docIds.Contains(x.Id));
                var group = groupTreats.GroupBy(g => g.ExecDeptCode);
                foreach (var item in group)
                {
                    var myPrescriptionNo = await _mySequenceRepository.GetSequenceAsync(nameof(Prescription), columnName); //生成一个序列
                    dic.Add(myPrescriptionNo, item.ToList());
                }
            }
        }

        var prescriptions = new List<Prescription>();

        foreach (var item in dic)
        {
            foreach (var advice in item.Value)
            {
                //var myPrescriptionNo = advice.ItemType == EDoctorsAdviceItemType.Prescribe
                //    ? $"C{item.Key.ToString("00000000")}"
                //    : $"Y{item.Key.ToString("00000000")}";

                var medType = advice.ItemType == EDoctorsAdviceItemType.Prescribe ? "C" : "Y";

                var entity = new Prescription(
                    id: GuidGenerator.Create(),
                    doctorsAdviceId: advice.Id,
                    medType: medType,
                    myPrescriptionNo: item.Key.ToString(),
                    prescriptionNo: item.Key.ToString(),
                    model.VisSerialNo,
                    model.PatientId,
                    model.DeptCode,
                    model.OperatorCode
                );
                prescriptions.Add(entity);
            }
        }

        updateEntities.ForEach(x =>
        {
            x.PrescriptionNo = prescriptions.FirstOrDefault(w => w.DoctorsAdviceId == x.Id)?.MyPrescriptionNo;
        });

        if (prescriptions.Count() > 0) await _prescriptionRepository.InsertManyAsync(prescriptions, true);
    }


    /// <summary>
    /// 医嘱信息回传
    /// </summary>
    /// <param name="model"></param>
    /// <param name="list"></param>
    /// <param name="isChildren"></param>
    /// <returns></returns>
    [NonAction]
    public async Task<List<PushDoctorsAdviceModel>> SendMedicalInfoAsync(SubmitDoctorsAdviceDto model, List<DoctorsAdvice> list, bool isChildren = false)
    {
        var group = list.GroupBy(g => g.PrescriptionNo);
        var adviceGroup = new Dictionary<string, List<SendAdviceInfoEto>>();
        foreach (var item in group)
        {
            if (item.Any())
            {
                adviceGroup.Add(item.Key, item.Select(s => new SendAdviceInfoEto
                {
                    Id = s.Id,
                    ItemType = s.ItemType,
                    PlatformType = s.PlatformType,
                }).ToList());
            }
        }

        var eto = new SendMedicalInfoEto { BaseInfo = model, AdviceGroup = adviceGroup };
        _logger.LogInformation($"医嘱推送内容：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
        //交递给消息队列处理，走一个异步，避免卡在提交的操作上
        //改为同步操作
        //await _capPublisher.PublishAsync("self.call.sendMedicalInfo", eto);

        //开启DDP调用模式
        if (_ddpHospital.DdpSwitch)
        {
            //DDP调用
            return await CallDdpSendMedicalInfoAsync(eto, isChildren);
        }
        //龙岗的模式，直接调用
        else
        {
            //发送医嘱信息给医院
            return await CallSendMedicalInfoAsync(eto, isChildren);
        }
    }

    /// <summary>
    /// 时间节点记录
    /// </summary>
    /// <param name="model"></param>
    /// <param name="updateEntities"></param>
    /// <returns></returns>
    private async Task TimeAxisRecoredAsync(SubmitDoctorsAdviceDto model, List<DoctorsAdvice> updateEntities)
    {
        using (_dataFilter.Disable<ISoftDelete>())
        {
            var piid = updateEntities.FirstOrDefault()?.PIID;
            if (piid.HasValue)
            {
                try
                {
                    //判断患者是否第一次开检验，是则推送时间节点消息
                    var lis = await (await _doctorsAdviceRepository.GetQueryableAsync()).AnyAsync(x =>
                        x.PIID == piid && x.ItemType == EDoctorsAdviceItemType.Lis &&
                        x.Status != ERecipeStatus.Saved
                        && !model.Ids.Contains(x.Id));

                    if (updateEntities.Any(x => x.ItemType == EDoctorsAdviceItemType.Lis) && !lis)
                    {
                        await _capPublisher.PublishAsync("sync.timeAxisRecord.to.patient",
                            new CreateTimeAxisRecordDto()
                            {
                                TimePointCode = 23,
                                Time = DateTime.Now,
                                PI_ID = piid.Value
                            });
                    }

                    //判断患者是否第一次开药方，是则推送时间节点消息,不包含此次的提交
                    var prescribe = await (await _doctorsAdviceRepository.GetQueryableAsync()).AnyAsync(x =>
                        x.PIID == piid && x.ItemType == EDoctorsAdviceItemType.Prescribe &&
                        x.Status != ERecipeStatus.Saved && !model.Ids.Contains(x.Id));
                    if (updateEntities.Any(x => x.ItemType == EDoctorsAdviceItemType.Prescribe) && !prescribe)
                    {
                        await _capPublisher.PublishAsync("sync.timeAxisRecord.to.patient",
                            new CreateTimeAxisRecordDto()
                            {
                                TimePointCode = 25,
                                Time = DateTime.Now,
                                PI_ID = piid.Value
                            });
                    }

                    //判断患者是否第一次开检查，是则推送时间节点消息
                    var pacs = await (await _doctorsAdviceRepository.GetQueryableAsync()).AnyAsync(x =>
                        x.PIID == piid && x.ItemType == EDoctorsAdviceItemType.Pacs &&
                        x.Status != ERecipeStatus.Saved && !model.Ids.Contains(x.Id));
                    if (updateEntities.Any(x => x.ItemType == EDoctorsAdviceItemType.Pacs) && !pacs)
                    {
                        await _capPublisher.PublishAsync("sync.timeAxisRecord.to.patient",
                            new CreateTimeAxisRecordDto()
                            {
                                TimePointCode = 24,
                                Time = DateTime.Now,
                                PI_ID = piid.Value
                            });
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"推送时间节点记录异常：{ex.Message}");
                }
            }
        }
    }

    /// <summary>
    /// 提交医嘱信息推送医嘱数据
    /// </summary>
    /// <param name="eto"></param>
    /// <returns></returns>
    private async Task PushSubmitAdviceMessageAsync(List<SubmitDoctorsAdviceEto> eto)
    {
        if (!eto.Any()) return;
        try
        {
            //1.推送到护理服务，让护士服务处理
            _logger.LogInformation("推送提交记录：" + System.Text.Json.JsonSerializer.Serialize(eto));
            //_capPublisher.Publish("submitadvice.recipeservice.to.nursingservice", eto);
            await _capPublisher.PublishAsync("submitadvice.recipeservice.to.nursingservice", eto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"提交医嘱到护理服务（submitadvice.recipeservice.to.nursingservice）异常： {ex.Message}");
        }
    }

    /// <summary>
    /// 推送自备药
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    private async Task PushOwnMedicineAsync(SubmitDoctorsAdviceDto model)
    {
        try
        {
            var ownMedicines = await _ownMedicineRepository.GetMyAllAsync(model.Piid);
            if (!ownMedicines.Any()) return;
            var notPushMedicines = ownMedicines.Where(w => !w.IsPush).ToList();
            if (!notPushMedicines.Any()) return;
            var ownMedicineEto = ObjectMapper.Map<List<OwnMedicine>, List<OwnMedicineEto>>(notPushMedicines);
            _capPublisher.Publish("ownmedicine.recipeservice.to.nursingservice", ownMedicineEto);
            var ids = notPushMedicines.Select(s => s.Id);
            await _ownMedicineRepository.SetPushAsync(ids);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"推送自备药（ownmedicine.recipeservice.to.nursingservice）异常： {ex.Message}");
        }
    }

    /// <summary>
    /// 拷贝-v2
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    [UnitOfWork]
    //[AllowAnonymous]
    public async Task<List<Guid>> CloneV2Async(CloneV2RequestWrap model)
    {
        return await CloneAdviceAsync(model, true);
    }

    /// <summary>
    /// 拷贝
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<List<Guid>> CloneAsync(List<Guid> ids)
    {
        var models = new List<CloneV2Request>();
        foreach (var id in ids)
        {
            models.Add(new CloneV2Request(id));
        }

        CloneV2RequestWrap cloneV2RequestWrap = new CloneV2RequestWrap();
        cloneV2RequestWrap.CloneV2Requests = models;
        return await CloneAdviceAsync(cloneV2RequestWrap, false);
    }


    /// <summary>
    /// 克隆医嘱记录
    /// </summary>
    /// <param name="cloneV2RequestWrap"></param>
    /// <param name="hasOtherAttr"></param>
    /// <returns></returns>
    private async Task<List<Guid>> CloneAdviceAsync(CloneV2RequestWrap cloneV2RequestWrap,
        bool hasOtherAttr = false)
    {
        List<CloneV2Request> models = cloneV2RequestWrap.CloneV2Requests;

        List<Guid> requestIds = models.Select(s => s.Id).ToList();
        //查询存附加项，并且排除掉
        var additionalItemsTreats = await (await _treatRepository.GetQueryableAsync())
            .Where(w => w.AdditionalItemsType > EAdditionalItemType.No && requestIds.Contains(w.DoctorsAdviceId))
            .Select(s => s.DoctorsAdviceId)
            .ToListAsync();

        //遍历传入的医嘱数据，如果是附加的处置，则过滤掉
        models = cloneV2RequestWrap.CloneV2Requests.Where(w => !additionalItemsTreats.Contains(w.Id)).ToList();

        List<Guid> ids = models.Select(s => s.Id).ToList();
        var treats = await (await _treatRepository.GetQueryableAsync()).Where(w => ids.Contains(w.DoctorsAdviceId)).ToListAsync();
        if (treats.Any(x => x.AdditionalItemsType != EAdditionalItemType.No && !ids.Contains(x.AdditionalItemsId)))
        {
            Oh.Error(message: "药品附加处置项不能单独复制");
        }

        //被拷贝的医嘱（不能被变更）
        var entities = await (await _doctorsAdviceRepository.GetQueryableAsync()).AsNoTracking().Where(w => ids.Contains(w.Id)).ToListAsync();

        if (!entities.Any()) return new List<Guid>();

        var patientInfo = await _patientAppService.GetPatientInfoAsync(cloneV2RequestWrap.PIID.Value);
        if (patientInfo == null)
        {
            Oh.Error(message: "未查询到患者信息");
        }

        //if (patientInfo.VisitStatus == EVisitStatus.已就诊 || patientInfo.VisitStatus == EVisitStatus.过号)
        //{
        //    Oh.Error(message: "已经就诊或过号的患者，不能复制医嘱");
        //}


        var ret = entities.Select(w => w.Id).ToList();
        var prescribes = await (await _prescribeRepository.GetQueryableAsync()).Where(w => ret.Contains(w.DoctorsAdviceId)).ToListAsync();

        //存在药品复制时，需要判断是否有附加处置
        if (prescribes.Any())
        {
            var treatItem = await (await _treatRepository.GetQueryableAsync())
                .Where(w => w.AdditionalItemsType != EAdditionalItemType.No && ret.Contains(w.AdditionalItemsId))
                .ToListAsync();

            //查询已处置项和药品的关联查询数据
            var recipeList = from t in treatItem
                             join p in entities.Where(x => x.ItemType == EDoctorsAdviceItemType.Prescribe)
                             on t.AdditionalItemsId equals p.Id
                             select new
                             {
                                 RecipeNo = p.RecipeNo,
                                 AdditionalItemsType = t.AdditionalItemsType
                             };

            entities.AddRange(await (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => treatItem.Select(s => s.DoctorsAdviceId).ToList().Contains(w.Id)).ToListAsync());
            //entities.ForEach(x=>x.UpdateStatus(ERecipeStatus.Saved));
            treats.AddRange(treatItem.Where(t => !ret.Contains(t.DoctorsAdviceId)));

            //查询皮试
            var treatResponse = await _grpcMasterDataClient.GetTreatProjectByCodeAsync(new GetTreatProjectByCodeRequest { Code = _configuration["SkinTreatCode"] });
            var treatByCode = treatResponse.TreatProject;
            //查询频次关联的附加处置数据
            var treatFrequencyResponse = await _grpcMasterDataClient
                .GetTreatProjectByCodeAsync(new GetTreatProjectByCodeRequest { Code = _configuration["ContinuousIntravenousInfusionCode"] });
            var treatFrequency = treatFrequencyResponse.TreatProject;

            var medicine = entities.Where(t => t.ItemType == EDoctorsAdviceItemType.Prescribe).ToList();
            var medicineIds = medicine.Select(s => s.Id).ToList();
            var group = medicine.GroupBy(g => g.RecipeNo).Select(s => s.ToList().FirstOrDefault()?.Id).ToList();
            foreach (var id in group)
            {

                var entity = prescribes.FirstOrDefault(w => w.DoctorsAdviceId == id);
                var data = models.FirstOrDefault(x => x.Id == id);
                var m = medicine.FirstOrDefault(f => f.Id == id);

                if (hasOtherAttr)
                {
                    //复制的药品选择未选择皮试，而复制出来的药品选择了皮试，需要开一条皮试   或者   复制药品选择和皮试，而被复制药品皮试项已删除则需要新增
                    var recipeListSkinAdditional = recipeList.Any(a => a.RecipeNo == m.RecipeNo && a.AdditionalItemsType == EAdditionalItemType.SkinAdditional);

                    //var dataSkinTestSignChoseResultYes = models.Any(w=> w.SkinTestSignChoseResult== ESkinTestSignChoseResult.Yes);
                    //var dataSkinTestSignChoseResultNo = models.Any(w => w.SkinTestSignChoseResult != ESkinTestSignChoseResult.Yes);

                    var dataSkinTestSignChoseResultYes = (from a in models
                                                          join b in medicine
                                                          on a.Id equals b.Id
                                                          where b.RecipeNo == m.RecipeNo && a.SkinTestSignChoseResult == ESkinTestSignChoseResult.Yes
                                                          select new { a.Id, b.RecipeNo }).Any();

                    var dataSkinTestSignChoseResultNo = (from a in models
                                                         join b in medicine
                                                         on a.Id equals b.Id
                                                         where b.RecipeNo == m.RecipeNo && a.SkinTestSignChoseResult != ESkinTestSignChoseResult.Yes
                                                         select new { a.Id, b.RecipeNo }).Any();

                    //var entitySkinTestSignChoseResultNoYes = prescribes.Any(w=> w.SkinTestSignChoseResult != ESkinTestSignChoseResult.Yes && (w.IsSkinTest.HasValue && w.IsSkinTest.Value));
                    //var entitySkinTestSignChoseResultYes = prescribes.Any(w => w.SkinTestSignChoseResult == ESkinTestSignChoseResult.Yes);

                    var entitySkinTestSignChoseResultNo = (from a in medicine
                                                           join p in prescribes
                                                           on a.Id equals p.DoctorsAdviceId
                                                           where a.RecipeNo == m.RecipeNo && p.SkinTestSignChoseResult != ESkinTestSignChoseResult.Yes && (p.IsSkinTest.HasValue && p.IsSkinTest.Value == true)
                                                           select new { a.Id, a.RecipeNo, p.IsSkinTest, p.SkinTestSignChoseResult }).Any();

                    var entitySkinTestSignChoseResultYes = (from a in medicine
                                                            join p in prescribes
                                                            on a.Id equals p.DoctorsAdviceId
                                                            where a.RecipeNo == m.RecipeNo && p.SkinTestSignChoseResult == ESkinTestSignChoseResult.Yes && (p.IsSkinTest.HasValue && p.IsSkinTest.Value == true)
                                                            select new { a.Id, a.RecipeNo, p.IsSkinTest, p.SkinTestSignChoseResult }).Any();

                    //if ((data?.SkinTestSignChoseResult == ESkinTestSignChoseResult.Yes && entity?.SkinTestSignChoseResult != ESkinTestSignChoseResult.Yes)
                    //     || (data?.SkinTestSignChoseResult == ESkinTestSignChoseResult.Yes && entity?.SkinTestSignChoseResult == ESkinTestSignChoseResult.Yes && !recipeListSkinAdditional))
                    if ((dataSkinTestSignChoseResultYes && entitySkinTestSignChoseResultNo)
                         || (dataSkinTestSignChoseResultYes && entitySkinTestSignChoseResultYes && !recipeListSkinAdditional))
                    {
                        if (treatByCode != null)
                        {
                            var treatDoctorsAdvice = new DoctorsAdvice(
                                id: GuidGenerator.Create(),
                                detailId: await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice),
                                    DETAILID),
                                platformType: m.PlatformType,
                                piid: cloneV2RequestWrap.PIID ?? m.PIID,
                                patientId: m.PatientId,
                                patientName: m.PatientName,
                                code: treatByCode.TreatCode,
                                name: treatByCode.TreatName,
                                categoryCode: treatByCode.DictionaryCode,
                                categoryName: treatByCode.DictionaryName,
                                isBackTracking: false,
                                prescriptionNo: "", //TODO 目前没有 
                                recipeNo: "",
                                recipeGroupNo: 1,
                                applyTime: DateTime.Now,
                                applyDeptCode: cloneV2RequestWrap.ApplyDeptCode, //m.ApplyDeptCode,
                                applyDeptName: cloneV2RequestWrap.ApplyDeptName, //m.ApplyDeptName,
                                applyDoctorCode: cloneV2RequestWrap.ApplyDoctorCode, //m.ApplyDoctorCode,
                                applyDoctorName: cloneV2RequestWrap.ApplyDoctorName, //m.ApplyDoctorName,
                                traineeCode: m.TraineeCode,
                                traineeName: m.TraineeName,
                                payTypeCode: "", //TODO 目前没有
                                payType: ERecipePayType.Self, //TODO 目前没有
                                price: decimal.Parse(treatByCode.Price.ToString("f3")), //project.Price,
                                unit: treatByCode.Unit,
                                amount: decimal.Parse(treatByCode.Price.ToString("f3")) * 1,
                                insuranceCode: "", //TODO 目前没有
                                insuranceType: EInsuranceCatalog.Self,
                                isChronicDisease: m.IsChronicDisease,
                                isRecipePrinted: false,
                                hisOrderNo: "",
                                diagnosis: m.Diagnosis,
                                execDeptCode: cloneV2RequestWrap.ExecDeptCode, //treatByCode.ExecDeptCode,
                                execDeptName: cloneV2RequestWrap.ExecDeptName, //treatByCode.ExecDeptName,
                                positionCode: "",
                                positionName: "",
                                remarks: "",
                                chargeCode: m.ChargeCode,
                                chargeName: m.ChargeCode,
                                prescribeTypeCode: m.PrescribeTypeCode,
                                prescribeTypeName: m.PrescribeTypeName,
                                startTime: null,
                                endTime: null,
                                recieveQty: 1,
                                recieveUnit: treatByCode.Unit,
                                pyCode: treatByCode.PyCode,
                                wbCode: treatByCode.WbCode,
                                itemType: EDoctorsAdviceItemType.Treat);
                            treatDoctorsAdvice.Additional = 1;
                            //treatDoctorsAdvice.UpdateStatus(ERecipeStatus.Saved);

                            var treat = new Treat(GuidGenerator.Create())
                            {
                                #region 添加诊疗

                                FeeTypeMainCode = treatByCode.FrequencyCode, //TODO 目前没有
                                FeeTypeSubCode = treatByCode.FeeTypeSubCode, //TODO 目前没有
                                FrequencyCode = "",
                                OtherPrice = decimal.Parse(treatByCode.OtherPrice.ToString("f3")),
                                Additional = treatByCode.Additional,
                                Specification = treatByCode.Specification,
                                ProjectMerge = treatByCode.ProjectMerge,
                                DoctorsAdviceId = treatDoctorsAdvice.Id,
                                FrequencyName = "",
                                LongDays = 1,
                                ProjectType = treatByCode.CategoryCode, //TODO
                                ProjectName = treatByCode.CategoryName, //TODO
                                TreatId = treatByCode.Id,
                                AdditionalItemsId = m.Id,
                                AdditionalItemsType = EAdditionalItemType.SkinAdditional

                                #endregion
                            };
                            treats.Add(treat);
                            entities.Add(treatDoctorsAdvice);
                        }
                    }

                    //复制的药品选择皮试，而复制出来的药品未选择了皮试，不需要添加皮试,去掉上面添加的皮试 
                    //if (data?.SkinTestSignChoseResult != ESkinTestSignChoseResult.Yes &&
                    //    entity?.SkinTestSignChoseResult == ESkinTestSignChoseResult.Yes) 
                    if (dataSkinTestSignChoseResultNo && entitySkinTestSignChoseResultYes)
                    {
                        var removeIds = treatItem
                            .Where(x => x.AdditionalItemsId == m.Id && x.AdditionalItemsType == EAdditionalItemType.SkinAdditional)
                            .Select(s => s.DoctorsAdviceId).ToList();
                        if (removeIds.Any())
                        {
                            entities.RemoveAll(x => removeIds.Contains(x.Id));
                            treats.RemoveAll(x => removeIds.Contains(x.DoctorsAdviceId));
                        }
                    }

                    //判断复制的药品附加处置是否存在，不存在如果用法有关联处置，需要新增处置
                    if (!recipeList.Any(a =>
                            a.RecipeNo == m.RecipeNo &&
                            a.AdditionalItemsType == EAdditionalItemType.UsageAdditional))
                    {
                        //查询药品用法是否有关联处置，有则默认开一条处置单
                        var treatByUsage = await _grpcMasterDataClient.GetTreatByUsageAsync(
                            new GetTreatByUsageCodeRequest()
                            { UsageCode = entity.UsageCode });
                        if (!string.IsNullOrEmpty(treatByUsage.TreatCode))
                        {
                            var recieveQty = (entity.UsageCode == _configuration["UsageIntravenousDripCode"] &&
                                             entity.FrequencyCode.ToUpper() is "BID" or "TID")
                                    ? entity.LongDays
                                    : (entity.FrequencyTimes ?? 1) * entity.LongDays;
                            var amount = decimal.Parse(treatByUsage.Price.ToString("f3")) * recieveQty;
                            var treatDoctorsAdvice = new DoctorsAdvice(
                                id: GuidGenerator.Create(),
                                detailId: await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice),
                                    DETAILID),
                                platformType: m.PlatformType,
                                piid: cloneV2RequestWrap.PIID ?? m.PIID,
                                patientId: m.PatientId,
                                patientName: m.PatientName,
                                code: treatByUsage.TreatCode,
                                name: treatByUsage.TreatName,
                                categoryCode: treatByUsage.DictionaryCode,
                                categoryName: treatByUsage.DictionaryName,
                                isBackTracking: false,
                                prescriptionNo: "", //TODO 目前没有 
                                recipeNo: "auto",
                                recipeGroupNo: 1,
                                applyTime: DateTime.Now,
                                applyDeptCode: cloneV2RequestWrap.ApplyDeptCode, //m.ApplyDeptCode,
                                applyDeptName: cloneV2RequestWrap.ApplyDeptName, //m.ApplyDeptName,
                                applyDoctorCode: cloneV2RequestWrap.ApplyDoctorCode, //m.ApplyDoctorCode,
                                applyDoctorName: cloneV2RequestWrap.ApplyDoctorName, //m.ApplyDoctorName,
                                traineeCode: m.TraineeCode,
                                traineeName: m.TraineeName,
                                payTypeCode: "", //TODO 目前没有
                                payType: ERecipePayType.Self,
                                price: decimal.Parse(treatByUsage.Price.ToString("f3")), //project.Price,
                                unit: treatByUsage.Unit,
                                amount: amount,
                                insuranceCode: "", //TODO 目前没有
                                insuranceType: EInsuranceCatalog.Self,
                                isChronicDisease: m.IsChronicDisease,
                                isRecipePrinted: false,
                                hisOrderNo: "",
                                diagnosis: m.Diagnosis,
                                execDeptCode: cloneV2RequestWrap.ExecDeptCode, //treatByUsage.ExecDeptCode,
                                execDeptName: cloneV2RequestWrap.ExecDeptName, //treatByUsage.ExecDeptName,
                                positionCode: "",
                                positionName: "",
                                remarks: "",
                                chargeCode: m.ChargeCode,
                                chargeName: m.ChargeCode,
                                prescribeTypeCode: m.PrescribeTypeCode,
                                prescribeTypeName: m.PrescribeTypeName,
                                startTime: null,
                                endTime: null,
                                recieveQty: recieveQty,
                                recieveUnit: treatByUsage.Unit,
                                pyCode: treatByUsage.PyCode,
                                wbCode: treatByUsage.WbCode,
                                itemType: EDoctorsAdviceItemType.Treat);
                            treatDoctorsAdvice.Additional = 1;

                            //treatDoctorsAdvice.UpdateStatus(ERecipeStatus.Saved);

                            var treat = new Treat(GuidGenerator.Create())
                            {
                                #region 添加诊疗

                                FeeTypeMainCode = treatByUsage.FrequencyCode,
                                FeeTypeSubCode = treatByUsage.FeeTypeSubCode,
                                FrequencyCode = "",
                                OtherPrice = decimal.Parse(treatByUsage.OtherPrice.ToString("f3")),
                                Additional = treatByUsage.Additional,
                                Specification = treatByUsage.Specification,
                                ProjectMerge = treatByUsage.ProjectMerge,
                                DoctorsAdviceId = treatDoctorsAdvice.Id,
                                FrequencyName = "",
                                LongDays = entity.LongDays,
                                ProjectType = treatByUsage.CategoryCode,
                                ProjectName = treatByUsage.CategoryName,
                                TreatId = treatByUsage.Id,
                                AdditionalItemsId = m.Id,
                                AdditionalItemsType = EAdditionalItemType.UsageAdditional

                                #endregion
                            };
                            treats.Add(treat);
                            entities.Add(treatDoctorsAdvice);
                        }
                    }

                    //判断复制的药品频次附加处置是否存在，不存在如果用法有关联处置，需要新增处置
                    if (entity.UsageCode == _configuration["UsageIntravenousDripCode"] &&
                        entity.FrequencyCode.ToUpper() is "BID" or "TID" && !recipeList.Any(a =>
                            a.RecipeNo == m.RecipeNo &&
                            a.AdditionalItemsType == EAdditionalItemType.FrequencyAdditional))
                    {
                        if (treatFrequency != null)
                        {
                            var recieveQty = (entity.FrequencyCode.ToUpper() is "BID" ? 1 : 2) * entity.LongDays;
                            var amount = decimal.Parse(treatFrequency.Price.ToString("f3")) * recieveQty;
                            var treatDoctorsAdvice = new DoctorsAdvice(
                                id: GuidGenerator.Create(),
                                detailId: await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice),
                                    DETAILID),
                                platformType: m.PlatformType,
                                piid: cloneV2RequestWrap.PIID ?? m.PIID,
                                patientId: m.PatientId,
                                patientName: m.PatientName,
                                code: treatFrequency.TreatCode,
                                name: treatFrequency.TreatName,
                                categoryCode: treatFrequency.DictionaryCode,
                                categoryName: treatFrequency.DictionaryName,
                                isBackTracking: false,
                                prescriptionNo: "", //TODO 目前没有 
                                recipeNo: "",
                                recipeGroupNo: 1,
                                applyTime: DateTime.Now,
                                applyDeptCode: cloneV2RequestWrap.ApplyDeptCode, //m.ApplyDeptCode,
                                applyDeptName: cloneV2RequestWrap.ApplyDeptName, //m.ApplyDeptName,
                                applyDoctorCode: cloneV2RequestWrap.ApplyDoctorCode, //m.ApplyDoctorCode,
                                applyDoctorName: cloneV2RequestWrap.ApplyDoctorName, //m.ApplyDoctorName,
                                traineeCode: m.TraineeCode,
                                traineeName: m.TraineeName,
                                payTypeCode: "", //TODO 目前没有
                                payType: ERecipePayType.Self, //TODO 目前没有
                                price: decimal.Parse(treatFrequency.Price.ToString("f3")), //project.Price,
                                unit: treatFrequency.Unit,
                                amount: amount,
                                insuranceCode: "", //TODO 目前没有
                                insuranceType: EInsuranceCatalog.Self,
                                isChronicDisease: m.IsChronicDisease,
                                isRecipePrinted: false,
                                hisOrderNo: "",
                                diagnosis: m.Diagnosis,
                                execDeptCode: cloneV2RequestWrap.ExecDeptCode, //treatByCode.ExecDeptCode,
                                execDeptName: cloneV2RequestWrap.ExecDeptName, //treatByCode.ExecDeptName,
                                positionCode: "",
                                positionName: "",
                                remarks: "",
                                chargeCode: m.ChargeCode,
                                chargeName: m.ChargeCode,
                                prescribeTypeCode: m.PrescribeTypeCode,
                                prescribeTypeName: m.PrescribeTypeName,
                                startTime: null,
                                endTime: null,
                                recieveQty: recieveQty,
                                recieveUnit: treatFrequency.Unit,
                                pyCode: treatFrequency.PyCode,
                                wbCode: treatFrequency.WbCode,
                                itemType: EDoctorsAdviceItemType.Treat);
                            treatDoctorsAdvice.Additional = 1;
                            //treatDoctorsAdvice.UpdateStatus(ERecipeStatus.Saved);

                            var treat = new Treat(GuidGenerator.Create())
                            {
                                #region 添加诊疗

                                FeeTypeMainCode = treatFrequency.FrequencyCode, //TODO 目前没有
                                FeeTypeSubCode = treatFrequency.FeeTypeSubCode, //TODO 目前没有
                                FrequencyCode = "",
                                OtherPrice = decimal.Parse(treatFrequency.OtherPrice.ToString("f3")),
                                Additional = treatFrequency.Additional,
                                Specification = treatFrequency.Specification,
                                ProjectMerge = treatFrequency.ProjectMerge,
                                DoctorsAdviceId = treatDoctorsAdvice.Id,
                                FrequencyName = "",
                                LongDays = 1,
                                ProjectType = treatFrequency.CategoryCode, //TODO
                                ProjectName = treatFrequency.CategoryName, //TODO
                                TreatId = treatFrequency.Id,
                                AdditionalItemsId = m.Id,
                                AdditionalItemsType = EAdditionalItemType.FrequencyAdditional

                                #endregion
                            };
                            treats.Add(treat);
                            entities.Add(treatDoctorsAdvice);
                        }
                    }
                }
            }

        }

        var pacses = await (await _pacsRepository.GetQueryableAsync()).Include(x => x.PacsItems).Where(w => ret.Contains(w.DoctorsAdviceId))
            .ToListAsync();
        var lises = await (await _lisRepository.GetQueryableAsync()).Include(x => x.LisItems).Where(w => ret.Contains(w.DoctorsAdviceId))
            .ToListAsync();

        var doctorsAdviceEntities = new List<DoctorsAdvice>();
        var prescribeEntities = new List<Prescribe>();
        var pacsEntities = new List<Pacs>();
        var pacsItemsEntities = new List<PacsItem>();
        var lisEntities = new List<Lis>();
        var lisItemsEntities = new List<LisItem>();
        var treatEntities = new List<Treat>();
        var drugStockQueries = new List<DrugStockQuery>();
        var mtm = new List<MedicalTechnologyMap>();

        Dictionary<string, string> recipeNoDic = new();
        Dictionary<string, int> INCR = new();


        var department = Newtonsoft.Json.JsonConvert.DeserializeObject<DepartmentSampleDto>(CurrentUser.FindClaim("Department").Value);
        var applyDeptCode = department.DeptCode;
        var applyDeptName = department.DeptName;
        //var applyDoctorCode = CurrentUser.FindClaim("name").Value;
        var applyDoctorCode = CurrentUser.UserName;
        var applyDoctorName = CurrentUser.FindClaim("fullName").Value;

        //克隆药品库存返回的信息，懒得校验了
        //var doctorsAdviceIds = entities.Select(s => s.Id).ToList();
        //var drugStocks = await (await _drugStockQueryRepository.GetQueryableAsync()).Where(w => doctorsAdviceIds.Contains(w.DoctorsAdviceId))
        //    .ToListAsync();

        //var drugStocks = await ( from d in (await _drugStockQueryRepository.GetQueryableAsync()).Where(w => doctorsAdviceIds.Contains(w.DoctorsAdviceId))
        //                 select d).ToListAsync();

        var dictPreIds = new Dictionary<Guid, Guid>();
        foreach (var item in entities.Distinct())
        {
            var id = GuidGenerator.Create();
            //复制历史医嘱
            if (cloneV2RequestWrap.PIID.HasValue)
            {
                item.PIID = cloneV2RequestWrap.PIID.Value;
            }

            //药方
            if (item.ItemType == EDoctorsAdviceItemType.Prescribe)
            {
                var entity = prescribes.Where(w => w.DoctorsAdviceId == item.Id).ToList();
                if (entity.Any())
                {
                    var cloneEntity = CloneUtil.CloneJson(entity);
                    foreach (var data in cloneEntity)
                    {
                        data.ResetId(GuidGenerator.Create(), id); //重置id并且清理跟踪对象

                        //拷贝弹框带过来的内容 
                        if (hasOtherAttr)
                        {
                            var attr = models.FirstOrDefault(w => w.Id == data.OldDoctorsAdviceId);
                            if (attr != null)
                            {
                                if (attr.SkinTestSignChoseResult.HasValue)
                                {
                                    data.SkinTestSignChoseResult = attr.SkinTestSignChoseResult.Value;
                                }

                                if (attr.LimitType != null && attr.RestrictedDrugs != null)
                                {
                                    data.DrugsLimit(attr.LimitType.Value, attr.RestrictedDrugs);
                                }
                            }
                        }

                        data.SetHisDosageQty();

                        //自定义一次剂量处理 TODO
                        var dosageList = await _dosageAppService.GetAllCustomDosagesAsync();
                        var dosage = dosageList.FirstOrDefault(w => w.Code == item.Code);
                        if (dosage != null)
                        {
                            var mydosageQty = dosage.GetHisDosageQty(data.DosageQty, data.DosageUnit);
                            data.CommitHisDosageQty = mydosageQty;
                        }

                        prescribeEntities.Add(data);

                        #region 库存校验

                        if (item.PlatformType == EPlatformType.EmergencyTreatment)
                        {
                            //查库存
                            var model = new DrugStockQueryRequest
                            {
                                QueryType = 2,
                                QueryCode = item.Code,
                                Storage = int.Parse(data.PharmacyCode),
                            };
                            var newDrugStocks = await _hospitalClientAppService.QueryHisDrugStockAsync(model);
                            if (newDrugStocks == null) Oh.Error($"药品【{item.Name}】药房目前没有库存");
                            if (newDrugStocks != null && newDrugStocks.Any())
                            {
                                var _drugStocks = newDrugStocks.FirstOrDefault();
                                var newEntity =
                                    ObjectMapper.Map<DrugStockQueryResponse, DrugStockQuery>(_drugStocks);
                                newEntity.SetDoctorsAdviceId(data.DoctorsAdviceId);
                                drugStockQueries.Add(newEntity);
                                //await _drugStockQueryRepository.InsertAsync(newEntity);
                            }
                            else
                            {
                                Oh.Error($"药品【{item.Name}】药房目前没有库存");
                            }
                        }

                        #endregion
                    }

                    dictPreIds.Add(item.Id, id);
                }
            }

            //检查
            if (item.ItemType == EDoctorsAdviceItemType.Pacs)
            {
                var entity = pacses.Where(w => w.DoctorsAdviceId == item.Id).ToList();
                if (entity.Any())
                {
                    var cloneEntity = CloneUtil.CloneJson(entity);
                    foreach (var data in cloneEntity)
                    {
                        var pacsId = GuidGenerator.Create();

                        foreach (var subData in data.PacsItems)
                        {
                            var cloneSubEntity = CloneUtil.CloneJson(subData);
                            cloneSubEntity.ResetId(GuidGenerator.Create(), pacsId); //重置id并且清除跟踪对象
                            pacsItemsEntities.Add(cloneSubEntity);
                            mtm.Add(new MedicalTechnologyMap(cloneSubEntity.Id, EDoctorsAdviceItemType.Pacs));
                        }

                        data.ResetId(pacsId, id); //重置id并且清除跟踪对象 
                        pacsEntities.Add(data);
                    }
                }
            }

            //检验
            if (item.ItemType == EDoctorsAdviceItemType.Lis)
            {
                var entity = lises.Where(w => w.DoctorsAdviceId == item.Id).ToList();
                if (entity.Any())
                {
                    var cloneEntity = CloneUtil.CloneJson(entity);
                    foreach (var data in cloneEntity)
                    {
                        var lisId = GuidGenerator.Create();
                        foreach (var subData in data.LisItems)
                        {
                            var cloneSubEntity = CloneUtil.CloneJson(subData);
                            cloneSubEntity.ResetId(GuidGenerator.Create(), lisId);
                            lisItemsEntities.Add(cloneSubEntity);
                            mtm.Add(new MedicalTechnologyMap(cloneSubEntity.Id, EDoctorsAdviceItemType.Lis));
                        }

                        if (!string.IsNullOrEmpty(patientInfo.IDNo))
                        {
                            var idcard = IDCard.IDCard.Verify(patientInfo.IDNo);
                            if (idcard.IsVerifyPass && !idcard.DayOfBirth.IsChildren() && data.SpecimenName == "指血")
                            {
                                Oh.Error("检验标本为手指血的项目，>=6岁的患者不能开");
                            }
                        }

                        data.ResetId(lisId, id);
                        lisEntities.Add(data);
                    }
                }
            }

            //诊疗
            if (item.ItemType == EDoctorsAdviceItemType.Treat)
            {
                var entity = treats.Where(w => w.DoctorsAdviceId == item.Id).ToList();
                if (entity.Any())
                {
                    var cloneEntity = CloneUtil.CloneJson(entity);
                    foreach (var data in cloneEntity)
                    {
                        data.ResetId(GuidGenerator.Create(), id);
                        foreach (var d in dictPreIds)
                        {
                            if (data.AdditionalItemsId == d.Key)
                            {
                                data.AdditionalItemsId = d.Value;
                            }
                        }
                        treatEntities.Add(data);
                        mtm.Add(new MedicalTechnologyMap(data.Id, EDoctorsAdviceItemType.Treat));
                    }
                }
            }

            var detailId = await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), DETAILID);
            var clone = CloneUtil.CloneJson(item);
            clone.AfterCloneAndResetProp(id, applyDeptCode, applyDeptName, applyDoctorCode, applyDoctorName,
                detailId);

            if (!recipeNoDic.ContainsKey(item.RecipeNo))
            {
                var recipeNo = (await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "RecipeNo"))
                    .ToString();
                recipeNoDic.Add(item.RecipeNo, recipeNo); // key 是久的recipeNo ，value 是新的recipeNo 
                clone.ReSetRecipeNo(recipeNo, 1, CurrentUser.Id); //重置医嘱号
                INCR.Add(recipeNo, 1);
            }
            else
            {
                var newRecipeNo = recipeNoDic[item.RecipeNo];
                var recipeGroupNo = INCR[newRecipeNo] + 1;
                clone.ReSetRecipeNo(newRecipeNo, recipeGroupNo, CurrentUser.Id); //重置医嘱号
                INCR[newRecipeNo] = recipeGroupNo;
            }

            clone.UpdateStatus(ERecipeStatus.Saved);
            clone.CleanCommitInfo();
            //诊疗和药方复制的时候执行科室改为当前的接诊科室
            if (item.ItemType == EDoctorsAdviceItemType.Prescribe || item.ItemType == EDoctorsAdviceItemType.Treat)
            {
                clone.ExecDeptCode = cloneV2RequestWrap.ExecDeptCode;
                clone.ExecDeptName = cloneV2RequestWrap.ExecDeptName;
            }

            doctorsAdviceEntities.Add(clone);
        }

        //选择不皮试
        var skinTestSignChoseResultNo = cloneV2RequestWrap.CloneV2Requests
            .Where(w => w.SkinTestSignChoseResult == ESkinTestSignChoseResult.No || w.SkinTestSignChoseResult == ESkinTestSignChoseResult.KeepUp)
            .Any();

        if (skinTestSignChoseResultNo)
        {
            var removeTreat = treatEntities.FirstOrDefault(w => w.AdditionalItemsType == EAdditionalItemType.SkinAdditional);
            if (removeTreat != null)
            {
                treatEntities.Remove(removeTreat);
                var removeAdvice = doctorsAdviceEntities.FirstOrDefault(w => w.Id == removeTreat.DoctorsAdviceId);
                doctorsAdviceEntities.Remove(removeAdvice);
            }
        }
        Dictionary<Guid, dynamic> updateAdvice = new Dictionary<Guid, dynamic>();

        if (prescribeEntities.Any())
        {
            #region 更新药品中药理的信息
            var drugs = (from a in doctorsAdviceEntities
                         join p in prescribeEntities
                         on a.Id equals p.DoctorsAdviceId
                         select new
                         {
                             Code = a.Code,
                             DoctorsAdviceId = p.DoctorsAdviceId,
                             Name = a.Name
                         }).ToList();


            List<GrpcMedicineModel> medicines = null;
            try
            {
                var medicinesRequest = new GetListMedicinesByCodeRequest();
                medicinesRequest.MedicineCode.AddRange(drugs.Select(s => s.Code).ToList());
                var grpcMedicines = await _grpcMasterDataClient.GetMedicineListByMedicineCodesAsync(medicinesRequest);
                medicines = grpcMedicines.Medicines.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("获取药品信息异常(GRPC)：{ex}", ex);
            }


            foreach (var prescribe in prescribeEntities)
            {
                var drug = drugs.FirstOrDefault(w => w.DoctorsAdviceId == prescribe.DoctorsAdviceId);
                if (drug != null)
                {
                    var medicine = medicines.FirstOrDefault(w => drug.Code == w.Code && prescribe.Specification == w.Specification && prescribe.PharmacyCode == w.PharmacyCode && prescribe.FactoryCode == w.FactoryCode);

                    if (medicine != null)
                    {
                        //更新HIS过来的部分关键信息
                        prescribe.UpdatePartProp(
                            unpack: (EMedicineUnPack)medicine.Unpack,
                            bigPackPrice: (decimal)medicine.BigPackPrice,
                            bigPackFactor: medicine.BigPackFactor,
                            bigPackUnit: medicine.BigPackUnit,
                            smallPackPrice: (decimal)medicine.SmallPackPrice,
                            smallPackUnit: medicine.SmallPackUnit,
                            smallPackFactor: medicine.SmallPackFactor,
                            antibioticPermission: medicine.AntibioticPermission,
                            prescriptionPermission: medicine.PrescriptionPermission,
                            medicineId: medicine.MedicineId);

                        //更新药品中药理部分的数据
                        //prescribe.UpdateToxic(
                        //          isSkinTest: medicine.IsSkinTest,
                        //          isCompound: medicine.IsCompound,
                        //          isDrunk: medicine.IsDrunk,
                        //          toxicLevel: medicine.ToxicLevel,
                        //          isHighRisk: medicine.IsHighRisk,
                        //          isRefrigerated: medicine.IsRefrigerated,
                        //          isTumour: medicine.IsTumour,
                        //          antibioticLevel: medicine.AntibioticLevel,
                        //          isPrecious: medicine.IsPrecious,
                        //          isInsulin: medicine.IsInsulin,
                        //          isAnaleptic: medicine.IsAnaleptic,
                        //          isAllergyTest: medicine.IsAllergyTest,
                        //          isLimited: medicine.IsLimited,
                        //          limitedNote: medicine.LimitedNote);

                        if (!updateAdvice.ContainsKey(prescribe.DoctorsAdviceId))
                        {
                            updateAdvice.Add(prescribe.DoctorsAdviceId, new
                            {
                                Price = (decimal)medicine.Price,
                                InsuranceCode = medicine.InsuranceCode,
                                InsuranceType = medicine.InsuranceType
                            });
                        }

                    }
                    else
                    {
                        _logger.LogInformation($"药品Code：{drug.Code} , 药品名称： {drug.Name} 在库存中查找不到信息");
                        Oh.Error($"药品【{drug.Name}】没有库存");
                    }
                }
                //重置时间
                prescribe.CreationTime = DateTime.Now;
                prescribe.LastModificationTime = null;
            }
            #endregion

            await _prescribeRepository.InsertManyAsync(prescribeEntities);
        }

        if (doctorsAdviceEntities.Any() && pacsEntities.Any())
        {
            List<DoctorsAdvice> pacsDoctorsAdviceEntities = doctorsAdviceEntities.Where(x => x.ItemType == EDoctorsAdviceItemType.Pacs).ToList();
            foreach (DoctorsAdvice pacsDoctorsAdviceEntity in pacsDoctorsAdviceEntities)
            {
                //添加附加项
                string projectCode = pacsDoctorsAdviceEntity.Code;
                ExamCodeRequest examCodeRequest = new ExamCodeRequest();
                examCodeRequest.Code.Add(projectCode);
                ExamAttachItemReponse examAttachItemReponse = await _grpcMasterDataClient.GetExamAttachItemsAsync(examCodeRequest);
                if (examAttachItemReponse.ExamAttachItems.Any())
                {
                    AddPacsDto addPacsDto = new AddPacsDto();
                    addPacsDto.Items = new List<PacsDto>();
                    IEnumerable<Pacs> currentItems = pacsEntities.Where(x => x.DoctorsAdviceId == pacsDoctorsAdviceEntity.Id);
                    foreach (Pacs pacs in currentItems)
                    {
                        PacsDto pacsDto = ObjectMapper.Map<Pacs, PacsDto>(pacs);
                        pacsDto.Code = pacsDoctorsAdviceEntity.Code;
                        pacsDto.InsuranceCode = pacsDoctorsAdviceEntity.InsuranceCode;
                        pacsDto.InsuranceType = pacsDoctorsAdviceEntity.InsuranceType;
                        pacsDto.PayTypeCode = pacsDoctorsAdviceEntity.PayTypeCode;
                        pacsDto.PayType = pacsDoctorsAdviceEntity.PayType;
                        pacsDto.ApplyTime = pacsDoctorsAdviceEntity.ApplyTime;
                        pacsDto.ExecDeptCode = pacsDoctorsAdviceEntity.ExecDeptCode;
                        pacsDto.ExecDeptName = pacsDoctorsAdviceEntity.ExecDeptName;
                        pacsDto.PrescribeTypeCode = pacsDoctorsAdviceEntity.PrescribeTypeCode;
                        pacsDto.PrescribeTypeName = pacsDoctorsAdviceEntity.PrescribeTypeName;
                        pacsDto.StartTime = pacsDoctorsAdviceEntity.StartTime;
                        pacsDto.EndTime = pacsDoctorsAdviceEntity.EndTime;
                        addPacsDto.Items.Add(pacsDto);
                    }
                    addPacsDto.DoctorsAdvice = ObjectMapper.Map<DoctorsAdvice, ModifyDoctorsAdviceBaseDto>(pacsDoctorsAdviceEntity);
                    addPacsDto.PatientInfo = new PatientInfoDto()
                    {
                        PatientIDCard = patientInfo.IDNo
                    };

                    await AddPacsTreatAsync(addPacsDto, examAttachItemReponse);
                    await AddPacsPrescribeAsync(addPacsDto, examAttachItemReponse);
                }
            }

            if (pacsDoctorsAdviceEntities.Any())
            {
                var docadvi = pacsDoctorsAdviceEntities.First();

                //查询4小时内重复的历史数据，按0元开单
                DateTime before4HourTime = DateTime.Now.AddHours(-4);
                //查询出四小时之前的医嘱
                var daList = await _doctorsAdviceRepository.GetListAsync(c => c.PIID == docadvi.PIID && c.ItemType == EDoctorsAdviceItemType.Pacs && c.PlatformType == 0 && c.Status == ERecipeStatus.Saved && c.ApplyTime >= before4HourTime);
                var daIds = daList.Select(c => c.Id);
                var pacsIds = (await _pacsRepository.GetListAsync(c => daIds.Contains(c.DoctorsAdviceId))).Select(c => c.Id);
                var itemList = await _pacsItemRepository.GetListAsync(c => pacsIds.Contains(c.PacsId));
                itemList.AddRange(pacsItemsEntities);

                int pid = int.Parse(docadvi.PatientId);
                CheckPacsXmcfRequestDto checkLisXmcfRequestDto = new CheckPacsXmcfRequestDto()
                {
                    mzzy = 1,
                    brid = pid,
                };
                checkLisXmcfRequestDto.xmlist = new List<RequestXmlistItem>();
                if (itemList.Any())
                {
                    foreach (var item in itemList)
                    {
                        checkLisXmcfRequestDto.xmlist.Add(new RequestXmlistItem()
                        {
                            jlwyz = item.Id.ToString(),
                            ztxh = item.ProjectCode,
                            fydj = item.Price,
                            fymc = item.TargetName,
                            fysl = (int)item.Qty,
                            fyxh = item.TargetCode
                        });
                    }
                }

                // 需要重新计算价格 的 DA 医嘱 和处置类型 
                List<AddPacsHisResponse> hisResponses = new List<AddPacsHisResponse>();
                try
                {
                    CheckPacsXmcfResponseDto checkPacsXmcfResponseDto = await _hisService.CheckPacsXmcfAsync(checkLisXmcfRequestDto);
                    if (checkPacsXmcfResponseDto != null && checkPacsXmcfResponseDto.Data != null)
                    {
                        var list = checkPacsXmcfResponseDto.Data.xmlist;

                        foreach (var item in list)
                        {
                            AddPacsHisResponse entity = new AddPacsHisResponse()
                            {
                                PacsItemId = Guid.Parse(item.jlwyz),
                                NewTargetCode = item.newfyxh.ToString(),
                                NewTargetName = item.newfymc,
                                NewPrice = item.newfydj,
                                NewQty = item.newfysl,
                                ProjectCode = item.ztxh.ToString(),
                                Qty = item.fysl,
                                Price = item.fydj,
                                TargetName = item.fymc,
                                TargetCode = item.fyxh.ToString()
                            };
                            var add = true;
                            switch (item.czff)
                            {
                                case "0元开单":
                                    entity.CzffEnum = CzffEnum.ZeroAllow;
                                    // 将当前这一单置为0 
                                    foreach (var pacItem in pacsItemsEntities)
                                    {
                                        if (pacItem.Id.ToString() == item.jlwyz)
                                        {
                                            // 标记这一单为0元开单 
                                            pacItem.Price = 0;
                                            foreach (var pac in pacsEntities)
                                            {
                                                if (pacItem.PacsId == pac.Id)
                                                {
                                                    entity.DoctorsAdviceId = pac.DoctorsAdviceId;
                                                    foreach (var doctorsAdvice in pacsDoctorsAdviceEntities)
                                                    {
                                                        if (doctorsAdvice.Id == pac.DoctorsAdviceId)
                                                        {
                                                            entity.ProjectName = doctorsAdvice.Name;
                                                            doctorsAdvice.Remarks += pacItem.TargetName + "当日4小时内重复，按0元开单; ";
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                            break;
                                        }
                                        if (pacsItemsEntities.Count() - 1 == pacsItemsEntities.IndexOf(pacItem))
                                            add = false;
                                    }
                                    if (add)
                                        hisResponses.Add(entity);
                                    break;

                                case "不允许开单":
                                    entity.CzffEnum = CzffEnum.NotAllow;
                                    foreach (var pacItem in pacsItemsEntities)
                                    {
                                        if (pacItem.Id.ToString() == item.jlwyz)
                                        {
                                            foreach (var pac in pacsEntities)
                                            {
                                                if (pacItem.PacsId == pac.Id)
                                                {
                                                    entity.DoctorsAdviceId = pac.DoctorsAdviceId;
                                                    foreach (var doctorsAdvice in pacsDoctorsAdviceEntities)
                                                    {
                                                        if (doctorsAdvice.Id == pac.DoctorsAdviceId)
                                                        {
                                                            entity.ProjectName = doctorsAdvice.Name;
                                                            doctorsAdvice.Remarks += pacItem.TargetName + "不允许开单; ";
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                            //删除当前记录提示不允许开单
                                            pacsItemsEntities.Remove(pacItem);
                                            break;
                                        }
                                        if (pacsItemsEntities.Count() - 1 == pacsItemsEntities.IndexOf(pacItem))
                                            add = false;
                                    }
                                    if (add)
                                        hisResponses.Add(entity);
                                    break;

                                case "项目置换":
                                    entity.CzffEnum = CzffEnum.Replacement;
                                    foreach (var pacItem in pacsItemsEntities)
                                    {
                                        if (pacItem.Id.ToString() == item.jlwyz)
                                        {
                                            //entity.TargetCode = pacItem.TargetCode;
                                            //entity.TargetName = pacItem.TargetName;
                                            //entity.Price = pacItem.Price;
                                            //entity.Qty = pacItem.Qty;
                                            //entity.PacsItemId = pacItem.PacsId;

                                            //进行项目置换  
                                            pacItem.Price = entity.NewPrice;
                                            pacItem.Qty = entity.NewQty;
                                            pacItem.TargetName = entity.NewTargetName;
                                            pacItem.TargetCode = entity.NewTargetCode;

                                            foreach (var pac in pacsEntities)
                                            {
                                                if (pacItem.PacsId == pac.Id)
                                                {
                                                    entity.DoctorsAdviceId = pac.DoctorsAdviceId;
                                                    foreach (var doctorsAdvice in pacsDoctorsAdviceEntities)
                                                    {
                                                        if (doctorsAdvice.Id == pac.DoctorsAdviceId)
                                                        {
                                                            entity.ProjectName = doctorsAdvice.Name;
                                                            doctorsAdvice.Remarks += pacItem.TargetName + "项目置换成" + entity.NewTargetName + "; ";
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        //最后一条都没找到的话丢弃 表示不是本次添加的数据
                                        if (pacsItemsEntities.Count() - 1 == pacsItemsEntities.IndexOf(pacItem))
                                            add = false;
                                    }
                                    if (add)
                                        hisResponses.Add(entity);
                                    break;

                                default: break;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    Oh.Error("His 接口未正常返回数据！");
                }

                // 重新计算project 价格 
                foreach (var item in hisResponses)
                {
                    foreach (var doctorsAdvice in pacsDoctorsAdviceEntities)
                    {
                        if (doctorsAdvice.Id == item.DoctorsAdviceId)
                        {
                            //重新计算价格
                            var pacs = pacsEntities.Where(c => c.DoctorsAdviceId == doctorsAdvice.Id).Select(c => c.Id);
                            doctorsAdvice.Amount = pacsItemsEntities.Where(c => pacs.Contains(c.PacsId)).Sum(s => s.Price * s.Qty) * doctorsAdvice.RecieveQty;
                            break;
                        }
                    }
                }
            }
        }
        //if (prescribeEntities.Any()) await _prescribeRepository.InsertManyAsync(prescribeEntities);
        if (treatEntities.Any()) await _treatRepository.InsertManyAsync(treatEntities);
        if (lisItemsEntities.Any()) await _lisItemRepository.InsertManyAsync(lisItemsEntities);
        if (lisEntities.Any()) await _lisRepository.InsertManyAsync(lisEntities);
        if (pacsItemsEntities.Any()) await _pacsItemRepository.InsertManyAsync(pacsItemsEntities);
        if (pacsEntities.Any()) await _pacsRepository.InsertManyAsync(pacsEntities);
        if (doctorsAdviceEntities.Any())
        {
            foreach (var item in doctorsAdviceEntities)
            {
                item.AreaCode = patientInfo.AreaCode;
                if (item.CategoryCode == "Entrust")
                {
                    await UpdatePatientInfoAsync(item.Name, item.PIID);
                }
            }
            await _doctorsAdviceRepository.InsertManyAsync(doctorsAdviceEntities);
            List<DoctorsAdviceMap> maps = new();
            doctorsAdviceEntities.ForEach(x => maps.Add(new DoctorsAdviceMap(x.Id)));
            await _doctorsAdviceMapRepository.InsertManyAsync(maps);
        }

        if (drugStockQueries.Any()) await _drugStockQueryRepository.InsertManyAsync(drugStockQueries);
        if (mtm.Any()) await _medicalTechnologyMapRepository.InsertManyAsync(mtm);

        return ret;
    }

    /// <summary>
    /// 停嘱
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<List<Guid>> StopAsync(StopDoctorsAdviceDto model)
    {
        var status = new List<ERecipeStatus>
            { ERecipeStatus.Submitted, ERecipeStatus.Confirmed, ERecipeStatus.Executed };
        var treats = await (await _treatRepository.GetQueryableAsync()).Where(w => model.Ids.Contains(w.DoctorsAdviceId)).ToListAsync();
        if (treats.Any(x =>
                x.AdditionalItemsType != EAdditionalItemType.No &&
                !model.Ids.Distinct().Contains(x.AdditionalItemsId)))
        {
            Oh.Error(message: "药品附加处置项不能单独停嘱");
        }

        //查询提交的医嘱是否有附加处置
        var treatList = await (await _treatRepository.GetQueryableAsync()).Where(
                x => x.AdditionalItemsType != EAdditionalItemType.No && model.Ids.Contains(x.AdditionalItemsId))
            .ToListAsync();
        if (treatList.Any())
        {
            var ids = treatList.Select(s => s.DoctorsAdviceId).ToList();
            model.Ids.AddRange(ids);
        }

        var recipeNos = await (await _doctorsAdviceRepository.GetQueryableAsync())
            .Where(w => model.Ids.Contains(w.Id) && status.Contains(w.Status))
            .Select(s => s.RecipeNo)
            .Distinct()
            .ToListAsync();
        if (!recipeNos.Any()) Oh.Error("查找不到需要停嘱的医嘱信息");

        var stopEntities = await (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => recipeNos.Contains(w.RecipeNo)).ToListAsync();
        foreach (var obs in stopEntities)
        {
            obs.UpdateStopInfo(model.OperatorCode, model.OperatorName, model.StopTime);
        }

        await _doctorsAdviceRepository.UpdateManyAsync(stopEntities);
        var stopIds = stopEntities.Select(s => s.Id).ToList();

        var eto = new DoctorsAdviceStopEto()
        {
            PIID = stopEntities.FirstOrDefault().PIID,
            RecipeIds = stopIds,
            Operation = EDoctorsAdviceOperation.Stop,
            OperatorCode = model.OperatorCode,
            OperatorName = model.OperatorName,
            Optime = DateTime.Now,
            Status = EDoctorsAdviceStatus.Stopped,
            StopTime = model.StopTime,
            PlatformType = (int)stopEntities.FirstOrDefault().PlatformType
        };
        _logger.LogInformation("推送停嘱记录：" + System.Text.Json.JsonSerializer.Serialize(eto));
        _capPublisher.Publish("stopadvice.recipeservice.to.nursingservice", eto);
        //停嘱响应 reply.stopadvice.nursingservice.to.recipeservice

        /* 停嘱不需要给HIS推送状态
        try
        {
            var recordinfos = new List<Recordinfo>();

            //药品的停嘱
            var chargeDetails = stopEntities.Where(w => w.ItemType == EDoctorsAdviceItemType.Prescribe).ToList();
            var cdids = chargeDetails.Select(s=>s.Id).ToList();
              
            foreach (var item in stopEntities)
            { 
                recordinfos.Add(new Recordinfo {
                    OperatorDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    RecordType = ERecordType.DoctorsAdvice, //3=医嘱
                    RecordState = 3, //作废  处方、治疗项目作废 （recordType=3）  检验、检查组套作废（recordType = 3）

                });
            }
             
            var request = new UpdateRecordStatusRequest
            {
                DeptCode = model.DeptCode,
                OperatorCode = model.OperatorCode,
                PatientId = model.PatientId,
                VisSerialNo = model.VisSerialNo, 
                Recordinfo = recordinfos
            };

            //TODO 处理医院返回的数据逻辑
            var ret = await _hospitalClientAppService.UpdateRecordStatusAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"停嘱调用医院接口修改状态异常：{ex.Message}");
        }
        */

        return stopIds;
    }

    /// <summary>
    /// 作废-院前(已提交可作废)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<List<Guid>> ObsPreAsync(ObsPreDoctorsAdviceDto model)
    {
        var input = new ObsDoctorsAdviceDto
        {
            Ids = model.Ids,
            OperatorCode = model.OperatorCode,
            OperatorName = model.OperatorName,
            DeptCode = "-", //院前没有该信息
            PatientId = "-", //院前没有该信息
            VisSerialNo = "-", //院前没有该信息
        };
        //院前不需要分页部分提交
        return await ObsAdviceAsync(input, true);
    }

    /// <summary>
    /// 作废(已提交可作废)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    //[UnitOfWork] //取消最外面的工作单元，写到分页提交的方法里
    public async Task<List<Guid>> ObsAsync(ObsDoctorsAdviceDto model)
    {
        var advices = await (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => model.Ids.Contains(w.Id)).ToListAsync();
        return await ObsAdviceAsync(model, false);
    }

    /// <summary>
    /// 作废医嘱
    /// </summary>
    /// <param name="model"></param>
    /// <param name="isPreHospital">是否是院前接口</param>
    /// <returns></returns>
    [UnitOfWork]
    private async Task<List<Guid>> ObsAdviceAsync(ObsDoctorsAdviceDto model, bool isPreHospital = false)
    {
        //诊疗
        var treats = await (await _treatRepository.GetQueryableAsync()).Where(w => model.Ids.Contains(w.DoctorsAdviceId)).ToListAsync();
        var treatsAny = treats.Any(x => x.AdditionalItemsType != EAdditionalItemType.No && !model.Ids.Distinct().Contains(x.AdditionalItemsId));

        if (treatsAny) Oh.Error(message: "药品附加处置项不能单独作废");

        //查询提交的医嘱是否有附加处置
        var treatList = await (await _treatRepository.GetQueryableAsync())
            .Where(x => x.AdditionalItemsType != EAdditionalItemType.No && model.Ids.Contains(x.AdditionalItemsId))
            .ToListAsync();

        if (treatList.Any())
        {
            var ids = treatList.Select(s => s.DoctorsAdviceId).ToList();
            model.Ids.AddRange(ids);
        }

        var recipeNos = await (await _doctorsAdviceRepository.GetQueryableAsync())
            .Where(w => model.Ids.Contains(w.Id) && w.Status == ERecipeStatus.Submitted)
            .Select(s => s.RecipeNo)
            .Distinct()
            .ToListAsync();

        if (!recipeNos.Any()) return new List<Guid>();

        List<DoctorsAdvice> obsEntities = new();
        var obsDrugEntities = await (await _doctorsAdviceRepository.GetQueryableAsync())
            .Where(w => recipeNos.Contains(w.RecipeNo))
            .ToListAsync();
        obsEntities.AddRange(obsDrugEntities);

        var additionObsIds = obsDrugEntities.Select(s => s.Id).ToList();
        var additionObsEntities = await (from a in (await _doctorsAdviceRepository.GetQueryableAsync())
                                         join t in (await _treatRepository.GetQueryableAsync())
                                         on a.Id equals t.DoctorsAdviceId
                                         where additionObsIds.Contains(t.AdditionalItemsId)
                                         select a).Distinct().ToListAsync();

        foreach (var item in additionObsEntities)
        {
            if (!obsEntities.Any(w => w.Id == item.Id))
            {
                obsEntities.Add(item);
            }
        }

        foreach (var obs in obsEntities)
        {
            obs.UpdateStatus(ERecipeStatus.Cancelled);
        }

        await _doctorsAdviceRepository.UpdateManyAsync(obsEntities);

        var obsIds = obsEntities.Select(s => s.Id).ToList();

        var eto = new DoctorsAdviceStatusEto()
        {
            PIID = obsEntities.FirstOrDefault().PIID,
            RecipeIds = obsIds,
            Operation = EDoctorsAdviceOperation.Cancel,
            OperatorCode = model.OperatorCode,
            OperatorName = model.OperatorName,
            Optime = DateTime.Now,
            Status = EDoctorsAdviceStatus.Cancelled,
            PlatformType = (int)obsEntities.FirstOrDefault().PlatformType
        };
        //作废推送到护理服务医嘱执行
        _logger.LogInformation("推送作废记录：" + System.Text.Json.JsonSerializer.Serialize(eto));
        _capPublisher.Publish("canceladvice.recipeservice.to.nursingservice", eto);
        //reply.canceladvice.nursingservice.to.recipeservice 作废响应

        //作废且提交到HIS
        if (!isPreHospital) await ObsToHisAsync(model, obsEntities);

        return obsIds;
    }

    /// <summary>
    /// 作废且提交到HIS
    /// </summary>
    /// <param name="model"></param>
    /// <param name="obsEntities"></param>
    /// <returns></returns>
    private async Task ObsToHisAsync(ObsDoctorsAdviceDto model, List<DoctorsAdvice> obsEntities)
    {
        //推送作废记录到HIS
        try
        {
            //开启DDP调用模式
            if (_ddpHospital.DdpSwitch)
            {
                List<PKUObsAdviceRequest> requests = new List<PKUObsAdviceRequest>();
                //药品的作废
                List<DoctorsAdvice> chargeDetails = obsEntities.Where(w => w.ItemType == EDoctorsAdviceItemType.Prescribe).ToList();
                chargeDetails = chargeDetails.DistinctBy(x => x.RecipeNo).ToList();
                foreach (DoctorsAdvice item in chargeDetails)
                {
                    PKUObsAdviceRequest request = new PKUObsAdviceRequest();
                    request.HisNumber = item.HisOrderNo;
                    request.Manufacturer = item.PrescriptionNo;
                    request.Type = "CF";
                    requests.Add(request);
                }

                //医技的作废
                List<DoctorsAdvice> yjDetails = obsEntities.Where(w => w.ItemType != EDoctorsAdviceItemType.Prescribe).ToList();
                foreach (var item in yjDetails)
                {
                    PKUObsAdviceRequest request = new PKUObsAdviceRequest();
                    request.HisNumber = item.HisOrderNo;
                    request.Manufacturer = item.PrescriptionNo;
                    request.Type = "YJ";
                    requests.Add(request);
                }
                IEnumerable<Task<DdpBaseResponse<object>>> tasks = requests.Select(x => _ddpApiService.ObsAdviceAsync(x));
                var tasksResult = tasks.ToList();
                while (tasksResult.Any())
                {
                    Task<DdpBaseResponse<object>> finishedTask = await Task.WhenAny(tasksResult);
                    tasksResult.Remove(finishedTask);
                }
            }
            //龙岗的模式，直接调用
            else
            {
                //发送医嘱信息给医院
                var recordinfos = new List<Recordinfo>();

                //药品的作废
                var chargeDetails = obsEntities.Where(w => w.ItemType == EDoctorsAdviceItemType.Prescribe).Distinct().ToList();
                if (chargeDetails.Any())
                {
                    var cdids = chargeDetails.Select(s => s.Id).ToList();
                    var stopPrescription = await (await _prescriptionRepository.GetQueryableAsync()).Where(w => cdids.Contains(w.DoctorsAdviceId))
                        .ToListAsync();
                    if (stopPrescription.Any())
                    {
                        foreach (var item in chargeDetails)
                        {
                            var myPrescription = stopPrescription.FirstOrDefault(w => w.DoctorsAdviceId == item.Id);

                            recordinfos.Add(new Recordinfo
                            {
                                OperatorDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                RecordType = ERecordType.DoctorsAdvice, //3=医嘱
                                RecordState = 2, //作废  处方、治疗项目作废 （recordType=3）  检验、检查组套作废（recordType = 3）
                                RecordNo = myPrescription == null ? item.PrescriptionNo : myPrescription.MyPrescriptionNo,
                                RecordItemNo = item.DetailId.ToString()
                            });
                        }
                    }

                }

                //检查 
                var pacsProjectDetail = obsEntities.Where(w => w.ItemType == EDoctorsAdviceItemType.Pacs).ToList();
                if (pacsProjectDetail.Any())
                {
                    var ppdid = pacsProjectDetail.Select(s => s.Id).ToList();
                    var pacsQuery = await (from a in (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => ppdid.Contains(w.Id))
                                           join pt in (await _prescriptionRepository.GetQueryableAsync()).Where(w => ppdid.Contains(w.DoctorsAdviceId))
                                               on a.Id equals pt.DoctorsAdviceId
                                           join p in (await _pacsRepository.GetQueryableAsync()).Where(w => ppdid.Contains(w.DoctorsAdviceId))
                                               on a.Id equals p.DoctorsAdviceId
                                           join i in (await _pacsItemRepository.GetQueryableAsync())
                                               on p.Id equals i.PacsId
                                           join m in (await _medicalTechnologyMapRepository.GetQueryableAsync())
                                               on i.Id equals m.LPTId
                                           where m.ItemType == EDoctorsAdviceItemType.Pacs
                                           select new
                                           {
                                               RecordNo = pt.MyPrescriptionNo,
                                               RecordItemNo = a.Code //m.Id.ToString(),
                                           })
                                           .Distinct()
                                           .ToListAsync();

                    foreach (var item in pacsQuery)
                    {
                        recordinfos.Add(new Recordinfo
                        {
                            OperatorDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            RecordType = ERecordType.DoctorsAdvice, //3=医嘱
                            RecordState = 3, //作废  处方、治疗项目作废 （recordType=3）  检验、检查组套作废（recordType = 3）
                            RecordNo = item.RecordNo,
                            RecordItemNo = item.RecordItemNo,
                        });
                    }
                }

                //检验
                var lisProjectDetail = obsEntities.Where(w => w.ItemType == EDoctorsAdviceItemType.Lis).ToList();
                if (lisProjectDetail.Any())
                {
                    var lpdid = lisProjectDetail.Select(s => s.Id).ToList();
                    var lisQuery = await (from a in (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => lpdid.Contains(w.Id))
                                          join pt in (await _prescriptionRepository.GetQueryableAsync()).Where(w => lpdid.Contains(w.DoctorsAdviceId))
                                              on a.Id equals pt.DoctorsAdviceId
                                          join p in (await _lisRepository.GetQueryableAsync()).Where(w => lpdid.Contains(w.DoctorsAdviceId))
                                              on a.Id equals p.DoctorsAdviceId
                                          join i in (await _lisItemRepository.GetQueryableAsync())
                                              on p.Id equals i.LisId
                                          join m in (await _medicalTechnologyMapRepository.GetQueryableAsync())
                                              on i.Id equals m.LPTId
                                          where m.ItemType == EDoctorsAdviceItemType.Lis
                                          select new
                                          {
                                              RecordNo = pt.MyPrescriptionNo,
                                              RecordItemNo = a.Code //m.Id.ToString()
                                          })
                                          .Distinct()
                                          .ToListAsync();

                    foreach (var item in lisQuery)
                    {
                        recordinfos.Add(new Recordinfo
                        {
                            OperatorDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            RecordType = ERecordType.DoctorsAdvice, //3=医嘱
                            RecordState = 3, //作废  处方、治疗项目作废 （recordType=3）  检验、检查组套作废（recordType = 3）
                            RecordNo = item.RecordNo,
                            RecordItemNo = item.RecordItemNo,
                        });
                    }
                }

                //诊疗  
                var treatProjectDetail = obsEntities.Where(w => w.ItemType == EDoctorsAdviceItemType.Treat).ToList();
                if (treatProjectDetail.Any())
                {
                    var tpdid = treatProjectDetail.Select(s => s.Id).ToList();
                    var treatQuery = await (from a in (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => tpdid.Contains(w.Id))
                                            join t in (await _treatRepository.GetQueryableAsync()).Where(w => tpdid.Contains(w.DoctorsAdviceId))
                                                on a.Id equals t.DoctorsAdviceId
                                            join pt in (await _prescriptionRepository.GetQueryableAsync()).Where(w => tpdid.Contains(w.DoctorsAdviceId))
                                                on a.Id equals pt.DoctorsAdviceId
                                            join m in (await _medicalTechnologyMapRepository.GetQueryableAsync())
                                                on t.Id equals m.LPTId
                                            select new
                                            {
                                                RecordNo = pt.MyPrescriptionNo,
                                                RecordItemNo = m.Id,
                                            })
                                            .Distinct()
                                            .ToListAsync();

                    foreach (var item in treatQuery)
                    {
                        recordinfos.Add(new Recordinfo
                        {
                            OperatorDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            RecordType = ERecordType.DoctorsAdvice, //3=医嘱
                            RecordState = 2, //作废  处方、治疗项目作废 （recordType=3）  检验、检查组套作废（recordType = 3）
                            RecordNo = item.RecordNo,
                            RecordItemNo = item.RecordItemNo.ToString(),
                        });
                    }
                }

                if (recordinfos.Any())
                {
                    var request = new UpdateRecordStatusRequest
                    {
                        DeptCode = model.DeptCode,
                        OperatorCode = model.OperatorCode,
                        PatientId = model.PatientId,
                        VisSerialNo = model.VisSerialNo,
                        Recordinfo = recordinfos
                    };
                    var ret = await _hospitalClientAppService.UpdateRecordStatusAsync(request);
                    if (ret.Code != 0) Oh.Error(ret.Msg);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"作废调用医院接口修改状态异常：{ex.Message}");
            Oh.Error(ex.Message);
        }
    }

    /// <summary>
    /// 删除(只能删除未提交前的记录,如果是院前)
    /// </summary>
    /// <param name="ids">删除为提交的几率</param>
    /// <returns></returns> 
    [UnitOfWork]
    //[AllowAnonymous]
    [HttpPost("/api/ecis/recipe/doctors-advice"), HttpDelete("/api/ecis/recipe/doctors-advice")]
    public async Task<List<Guid>> DeleteAsync([FromQuery] List<Guid> ids)
    {
        var entities = await (await _doctorsAdviceRepository.GetQueryableAsync())
            .Where(w => ids.Contains(w.Id) && w.Status == ERecipeStatus.Saved).ToListAsync();
        if (!entities.Any()) return new List<Guid>();
        var ret = entities.Select(w => w.Id).ToList();

        var prescribes = await (await _prescribeRepository.GetQueryableAsync()).Where(w => ret.Contains(w.DoctorsAdviceId)).ToListAsync();
        var pacses = await (await _pacsRepository.GetQueryableAsync()).Include(x => x.PacsItems).Where(w => ret.Contains(w.DoctorsAdviceId))
            .ToListAsync();
        var lises = await (await _lisRepository.GetQueryableAsync()).Include(x => x.LisItems).Where(w => ret.Contains(w.DoctorsAdviceId))
            .ToListAsync();
        var treats = await (await _treatRepository.GetQueryableAsync()).Where(w => ret.Contains(w.DoctorsAdviceId)).ToListAsync();

        if (prescribes.Any())
        {
            await _prescribeRepository.DeleteManyAsync(prescribes);
        }

        if (pacses.Any())
        {
            var pacsItems = new List<PacsItem>();
            pacses.ForEach(x => pacsItems.AddRange(x.PacsItems));
            await _pacsItemRepository.DeleteManyAsync(pacsItems);
            await _pacsRepository.DeleteManyAsync(pacses);
        }

        if (lises.Any())
        {
            var lisItems = new List<LisItem>();
            lises.ForEach(x => lisItems.AddRange(x.LisItems));
            await _lisItemRepository.DeleteManyAsync(lisItems);
            await _lisRepository.DeleteManyAsync(lises);
        }

        if (treats.Any())
        {
            await _treatRepository.DeleteManyAsync(treats);
        }

        //删除组医嘱时，需要更新子号 
        var updateList = new List<DoctorsAdvice>();
        var recipeNos = entities.Select(s => s.RecipeNo).Distinct().ToList();
        var otherAdvice = await (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => recipeNos.Contains(w.RecipeNo)).ToListAsync();

        var group = entities.GroupBy(g => g.RecipeNo);

        foreach (var item in group)
        {
            var models = otherAdvice.Where(w => w.RecipeNo == item.Key).ToList();
            if (models.Count > item.Count())
            {
                var groupNos = item.Select(s => s.RecipeGroupNo).Distinct().ToList();
                var updateEntities = models.Where(w => !groupNos.Contains(w.RecipeGroupNo))
                    .OrderBy(o => o.RecipeGroupNo).ToList();
                if (updateEntities.Count > 0)
                {
                    var counter = 1;
                    foreach (var entity in updateEntities)
                    {
                        entity.RecipeGroupNo = counter++;
                    }

                    updateList.AddRange(updateEntities);
                }
            }
        }

        if (updateList.Any()) await _doctorsAdviceRepository.UpdateManyAsync(updateList);
        if (entities.Any()) await _doctorsAdviceRepository.DeleteManyAsync(entities);
        //判断药品医嘱删除是否包含处置
        if (prescribes.Any())
        {
            var preId = prescribes.Select(w => w.DoctorsAdviceId).ToList();
            //查询关联的处置
            var treatList = await (await _treatRepository.GetQueryableAsync()).Where(w =>
                    w.AdditionalItemsType != EAdditionalItemType.No && preId.Contains(w.AdditionalItemsId))
                .ToListAsync();
            if (treatList.Any())
            {
                var treatIds = treatList.Select(s => s.DoctorsAdviceId).ToList();
                //调用删除方法删除处置
                await DeleteAsync(treatIds);
            }
        }

        return ret;
    }

    /// <summary>
    /// 组合
    /// <![CDATA[医嘱号为最小医嘱号，子号从1开始自动增长]]>
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<List<Guid>> GroupAsync(List<Guid> ids)
    {
        List<DoctorsAdvice> doctorsAdvices = await _doctorsAdviceRepository.GetListAsync(x => ids.Contains(x.Id) && x.Status == ERecipeStatus.Saved);
        if (!doctorsAdvices.Any()) Oh.Error("找不到需要拆组的医嘱记录");

        string doctor = CurrentUser.UserName;
        if (doctor.IsNullOrEmpty()) Oh.Error("未能获取医生信息");

        int count = doctorsAdvices.Count(w => w.ApplyDoctorCode == doctor);
        if (doctorsAdvices.Count > count) Oh.Error("不应该将别人的医嘱和自己的归组在一起");

        if (doctorsAdvices.Count > 5) Oh.Error("合组数量不能大于5个");

        int canGroupCount = doctorsAdvices.Count(w =>
            w.ItemType == EDoctorsAdviceItemType.Lis || w.ItemType == EDoctorsAdviceItemType.Pacs);
        if (canGroupCount > 0) Oh.Error("不能将检查，检验的和药品诊疗合组");

        if (doctorsAdvices.GroupBy(g => g.PrescribeTypeCode).Select(s => s.Key).Count() > 1) Oh.Error("不允许将长嘱和临嘱合并成组");

        //验证同组的频次，用法，规格都必须相同（计划时间改为最新一个）
        List<Guid> prescribeAdviceIds = doctorsAdvices.Where(x => x.ItemType == EDoctorsAdviceItemType.Prescribe).Select(x => x.Id).ToList();
        List<Prescribe> prescribes = await _prescribeRepository.GetListAsync(x => prescribeAdviceIds.Contains(x.DoctorsAdviceId));

        int sampleGroup = prescribes.GroupBy(g => new { g.UsageCode, g.FrequencyCode, g.LongDays, g.PharmacyCode, g.IsCriticalPrescription, g.MedicineProperty }).Count();
        if (sampleGroup > 1) Oh.Error("合组的药品必须保证【用法,频次,天数,药房,药品类型】保持一致");

        var query = await (from a in (await _doctorsAdviceRepository.GetQueryableAsync())
                           join p in (await _prescribeRepository.GetQueryableAsync())
                               on a.Id equals p.DoctorsAdviceId
                           join t in (await _toxicRepository.GetQueryableAsync())
                               on p.MedicineId equals t.MedicineId
                           where ids.Contains(a.Id) && t.ToxicLevel > 0
                           select new
                           {
                               a.Code,
                               a.Name,
                               t.MedicineId,
                               t.ToxicLevel
                           }).ToListAsync();

        if (query.Any() && query.Count() > 1)
            Oh.Error($"麻药，精神药品 【{string.Join(',', query.Select(s => s.Name).ToList())}】 之间不能相互成组");

        Prescribe prescribe = prescribes.FirstOrDefault();
        int frequencyTimes = prescribe != null ? prescribe.FrequencyTimes.Value : 1;
        int longDays = prescribe != null ? prescribe.LongDays : 1;
        string recipeNo = (await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "RecipeNo")).ToString();

        int recipeGroupNo = 1;
        DateTime applyTime = doctorsAdvices.Max(s => s.ApplyTime);
        DateTime? startTime = doctorsAdvices.Max(s => s.StartTime);
        doctorsAdvices = doctorsAdvices.OrderBy(x => x.CreationTime).ToList();
        foreach (DoctorsAdvice doctorsAdvice in doctorsAdvices)
        {
            doctorsAdvice.RecipeNo = recipeNo;
            doctorsAdvice.RecipeGroupNo = recipeGroupNo;
            recipeGroupNo += 1;
            doctorsAdvice.ApplyTime = applyTime; //计划时间改为最新一个
            doctorsAdvice.StartTime = startTime;
        }

        //判断组合的药品是否有附加项，有则需要判断是否去掉多余的附加项
        var treatList = await (await _treatRepository.GetQueryableAsync()).Where(s =>
                s.AdditionalItemsType != EAdditionalItemType.No && ids.Contains(s.AdditionalItemsId))
            .OrderBy(o => o.AdditionalItemsId).ToListAsync();
        if (treatList.Any())
        {
            var deleteIds = new List<Guid>();
            var usageTreat =
                treatList.FirstOrDefault(x => x.AdditionalItemsType == EAdditionalItemType.UsageAdditional);
            //用法附加项修改医嘱号及时间
            if (usageTreat != null)
            {
                var advice =
                    await (await _doctorsAdviceRepository.GetQueryableAsync()).FirstOrDefaultAsync(x => x.Id == usageTreat.DoctorsAdviceId);
                if (advice != null)
                {
                    advice.RecipeNo =
                        (await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "RecipeNo"))
                        .ToString();
                    advice.ApplyTime = applyTime; //计划时间改为最新一个
                    advice.StartTime = startTime;
                    advice.RecieveQty =
                        (prescribe.UsageCode == _configuration["UsageIntravenousDripCode"] &&
                         prescribe.FrequencyCode.ToUpper() is "BID" or "TID")
                            // BID/TID的 静脉输液按天数（每天一次），静脉接瓶按频次减一次再乘以天数
                            ? longDays
                            : (frequencyTimes * longDays);
                    doctorsAdvices.Add(advice);
                }
            }

            var skinTreat =
                treatList.FirstOrDefault(x => x.AdditionalItemsType == EAdditionalItemType.SkinAdditional);
            //皮试附加项修改医嘱号及时间
            if (skinTreat != null)
            {
                var advice =
                    await (await _doctorsAdviceRepository.GetQueryableAsync()).FirstOrDefaultAsync(x => x.Id == skinTreat.DoctorsAdviceId);
                if (advice != null)
                {
                    advice.RecipeNo =
                        (await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "RecipeNo"))
                        .ToString();
                    advice.ApplyTime = applyTime; //计划时间改为最新一个
                    advice.StartTime = startTime;
                    advice.RecieveQty = 1;
                    doctorsAdvices.Add(advice);
                }
            }

            var frequencyTreat =
                treatList.FirstOrDefault(x => x.AdditionalItemsType == EAdditionalItemType.FrequencyAdditional);
            //皮试附加项修改医嘱号及时间
            if (frequencyTreat != null)
            {
                var advice =
                    await (await _doctorsAdviceRepository.GetQueryableAsync()).FirstOrDefaultAsync(x => x.Id == frequencyTreat.DoctorsAdviceId);
                if (advice != null)
                {
                    advice.RecipeNo =
                        (await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "RecipeNo"))
                        .ToString();
                    advice.ApplyTime = applyTime; //计划时间改为最新一个
                    advice.StartTime = startTime;
                    doctorsAdvices.Add(advice);
                }
            }

            //查询用法附加处置是否大于1，大于1则删除多余的
            if (treatList.Count(x => x.AdditionalItemsType == EAdditionalItemType.UsageAdditional) > 1)
            {
                deleteIds.AddRange(treatList
                    .Where(x => x.Id != usageTreat.Id &&
                                x.AdditionalItemsType == EAdditionalItemType.UsageAdditional)
                    .Select(s => s.DoctorsAdviceId)
                    .ToList());
            }

            if (treatList.Count(x => x.AdditionalItemsType == EAdditionalItemType.SkinAdditional) > 1)
            {
                deleteIds.AddRange(treatList
                    .Where(x => x.Id != skinTreat.Id && x.AdditionalItemsType == EAdditionalItemType.SkinAdditional)
                    .Select(s => s.DoctorsAdviceId)
                    .ToList());
            }

            if (treatList.Count(x => x.AdditionalItemsType == EAdditionalItemType.FrequencyAdditional) > 1)
            {
                deleteIds.AddRange(treatList
                    .Where(x => x.Id != frequencyTreat.Id &&
                                x.AdditionalItemsType == EAdditionalItemType.FrequencyAdditional)
                    .Select(s => s.DoctorsAdviceId)
                    .ToList());
            }

            if (deleteIds.Any())
            {
                await DeleteAsync(deleteIds);
            }
        }

        await _doctorsAdviceRepository.UpdateManyAsync(doctorsAdvices);

        return await Task.FromResult(doctorsAdvices.Select(w => w.Id).ToList());
    }

    /// <summary>
    /// 医嘱拆组
    /// </summary>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<List<Guid>> TakeApartGroupAsync(TakeApartGroupDto model)
    {
        var entities = await (await _doctorsAdviceRepository.GetQueryableAsync())
            .Where(w => model.Ids.Contains(w.Id) && w.Status == ERecipeStatus.Saved).ToListAsync();

        if (!entities.Any()) Oh.Error("找不到需要拆组的医嘱记录");

        var count = entities.Select(s => s.RecipeNo).Distinct().Count();
        if (count > 1) Oh.Error("无法拆不同组的医嘱记录");

        var task = _patientAppService.GetPatientInfoAsync(entities.First().PIID);

        List<DoctorsAdvice> listDoctorAdvie = new List<DoctorsAdvice>();
        var recipeNo = entities.FirstOrDefault()?.RecipeNo;
        var listTreat = new List<Treat>();
        var mtm = new List<MedicalTechnologyMap>();
        var allEntities = await (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => w.RecipeNo == recipeNo).ToListAsync();
        var otherEntities = allEntities.Where(w => !entities.Select(s => s.Id).ToList().Contains(w.Id)).ToList();
        if (otherEntities.Any())
        {
            entities.AddRange(otherEntities);
        }

        var entitiesIds = entities.Select(s => s.Id).ToList();
        var prescribes = await (await _prescribeRepository.GetQueryableAsync()).Where(w => entitiesIds.Contains(w.DoctorsAdviceId))
            .ToListAsync();
        foreach (var entity in entities)
        {
            entity.RecipeNo = (await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "RecipeNo"))
                .ToString();
            entity.RecipeGroupNo = 1;

            #region 附加项添加

            var prescribe = prescribes.FirstOrDefault(x => x.DoctorsAdviceId == entity.Id);
            if (prescribe == null)
            {
                continue;
            }

            var isChild = model.Patientinfo.IsChildren();

            //查询药品用法是否有关联处置，有则默认开一条处置单
            var treatGrpc = await _grpcMasterDataClient.GetTreatByUsageAsync(
                new GetTreatByUsageCodeRequest()
                { UsageCode = prescribe.UsageCode });
            if (!string.IsNullOrEmpty(treatGrpc.TreatCode))
            {
                bool isAdditionalPrice = false;
                var recieveQty = prescribe.UsageCode == _configuration["UsageIntravenousDripCode"] &&
                                         prescribe.FrequencyCode.ToUpper() is "BID" or "TID"
                                         // BID/TID的 静脉输液按天数（每天一次），静脉接瓶按频次减一次再乘以天数
                                         ? prescribe.LongDays
                                         : ((prescribe.FrequencyTimes ?? 1) * prescribe.LongDays);

                var amount = decimal.Parse(treatGrpc.Price.ToString("f3")) * recieveQty;

                var detailId = await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), DETAILID);

                var treatDoctorsAdvice = new DoctorsAdvice(
                    id: GuidGenerator.Create(),
                    detailId: detailId,
                    platformType: entity.PlatformType,
                    piid: entity.PIID,
                    patientId: entity.PatientId,
                    patientName: entity.PatientName,
                    code: treatGrpc.TreatCode,
                    name: treatGrpc.TreatName,
                    categoryCode: treatGrpc.DictionaryCode,
                    categoryName: treatGrpc.DictionaryName,
                    isBackTracking: false,
                    prescriptionNo: "", //TODO 目前没有 
                    recipeNo: (await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "RecipeNo"))
                    .ToString(),
                    recipeGroupNo: 1,
                    applyTime: DateTime.Now,
                    applyDeptCode: entity.ApplyDeptCode,
                    applyDeptName: entity.ApplyDeptName,
                    applyDoctorCode: entity.ApplyDoctorCode,
                    applyDoctorName: entity.ApplyDoctorName,
                    traineeCode: entity.TraineeCode,
                    traineeName: entity.TraineeName,
                    payTypeCode: "",
                    payType: ERecipePayType.Self,
                    price: decimal.Parse(treatGrpc.Price.ToString("f3")), //project.Price,
                    unit: treatGrpc.Unit,
                    amount: amount,
                    insuranceCode: "", //TODO 目前没有
                    insuranceType: EInsuranceCatalog.Self,
                    isChronicDisease: entity.IsChronicDisease,
                    isRecipePrinted: false,
                    hisOrderNo: "",
                    diagnosis: entity.Diagnosis,
                    execDeptCode: entity.ExecDeptCode, //treatGrpc.ExecDeptCode,
                    execDeptName: entity.ExecDeptName, //treatGrpc.ExecDeptName,
                    positionCode: "",
                    positionName: "",
                    remarks: "",
                    chargeCode: entity.ChargeCode,
                    chargeName: entity.ChargeName,
                    prescribeTypeCode: entity.PrescribeTypeCode,
                    prescribeTypeName: entity.PrescribeTypeName,
                    startTime: null,
                    endTime: null,
                    recieveQty: recieveQty,
                    recieveUnit: treatGrpc.Unit,
                    pyCode: treatGrpc.PyCode,
                    wbCode: treatGrpc.WbCode,
                    itemType: EDoctorsAdviceItemType.Treat, isAdditionalPrice: isAdditionalPrice);
                treatDoctorsAdvice.Additional = 1;

                var treat = new Treat(GuidGenerator.Create())
                {
                    #region 添加诊疗

                    FeeTypeMainCode = treatGrpc.FrequencyCode,
                    FeeTypeSubCode = treatGrpc.FeeTypeSubCode,
                    FrequencyCode = "",
                    OtherPrice = decimal.Parse(treatGrpc.OtherPrice.ToString("f3")),
                    Additional = treatGrpc.Additional,
                    Specification = treatGrpc.Specification,
                    ProjectMerge = treatGrpc.ProjectMerge,
                    DoctorsAdviceId = treatDoctorsAdvice.Id,
                    FrequencyName = "",
                    LongDays = prescribe.LongDays,
                    ProjectType = treatGrpc.CategoryCode,
                    ProjectName = treatGrpc.CategoryName,
                    TreatId = treatGrpc.Id,
                    AdditionalItemsId = entity.Id,
                    AdditionalItemsType = EAdditionalItemType.UsageAdditional

                    #endregion
                };
                if (isChild && treatGrpc.Additional && treatGrpc.OtherPrice > 0)
                {
                    treat.DoctorsAdvice = treatDoctorsAdvice; //手动复制，处理价格更新问题
                    amount = treat.GetAmount(model.Patientinfo.IsChildren(), out isAdditionalPrice);
                    if (amount > 0) treatDoctorsAdvice.UpdateAmount(amount, isAdditionalPrice);
                }

                mtm.Add(new MedicalTechnologyMap(treat.Id, EDoctorsAdviceItemType.Treat));
                listTreat.Add(treat);
                listDoctorAdvie.Add(treatDoctorsAdvice);
            }

            if (prescribe.SkinTestSignChoseResult == ESkinTestSignChoseResult.Yes)
            {
                var treatResponse = await _grpcMasterDataClient.GetTreatProjectByCodeAsync(
                    new GetTreatProjectByCodeRequest
                    { Code = _configuration["SkinTreatCode"] });
                var treatByCode = treatResponse.TreatProject;
                if (treatByCode != null)
                {
                    bool isAdditionalPrice = false;
                    var amount = decimal.Parse(treatByCode.Price.ToString("f3")) * 1;
                    var detailId = await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), DETAILID);

                    var treatDoctorsAdvice = new DoctorsAdvice(
                        id: GuidGenerator.Create(),
                        detailId: detailId,
                        platformType: entity.PlatformType,
                        piid: entity.PIID,
                        patientId: entity.PatientId,
                        patientName: entity.PatientName,
                        code: treatByCode.TreatCode,
                        name: treatByCode.TreatName,
                        categoryCode: treatByCode.DictionaryCode,
                        categoryName: treatByCode.DictionaryName,
                        isBackTracking: false,
                        prescriptionNo: "", //TODO 目前没有 
                        recipeNo: (await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "RecipeNo"))
                        .ToString(), //DOTO 临时用置空，等下处理
                        recipeGroupNo: 1, //DOTO  等下处理  
                        applyTime: DateTime.Now,
                        applyDeptCode: entity.ApplyDeptCode,
                        applyDeptName: entity.ApplyDeptName,
                        applyDoctorCode: entity.ApplyDoctorCode,
                        applyDoctorName: entity.ApplyDoctorName,
                        traineeCode: entity.TraineeCode,
                        traineeName: entity.TraineeName,
                        payTypeCode: "", //TODO 目前没有
                        payType: ERecipePayType.Self, //TODO 目前没有
                        price: decimal.Parse(treatByCode.Price.ToString("f3")), //project.Price,
                        unit: treatByCode.Unit,
                        amount: amount,
                        insuranceCode: "", //TODO 目前没有
                        insuranceType: EInsuranceCatalog.Self,
                        isChronicDisease: entity.IsChronicDisease,
                        isRecipePrinted: false,
                        hisOrderNo: "",
                        diagnosis: entity.Diagnosis,
                        execDeptCode: entity.ExecDeptCode, //treatByCode.ExecDeptCode,
                        execDeptName: entity.ExecDeptName, //treatByCode.ExecDeptName,
                        positionCode: "",
                        positionName: "",
                        remarks: "",
                        chargeCode: entity.ChargeCode,
                        chargeName: entity.ChargeName,
                        prescribeTypeCode: entity.PrescribeTypeCode,
                        prescribeTypeName: entity.PrescribeTypeName,
                        startTime: null,
                        endTime: null,
                        recieveQty: 1,
                        recieveUnit: treatByCode.Unit,
                        pyCode: treatByCode.PyCode,
                        wbCode: treatByCode.WbCode,
                        itemType: EDoctorsAdviceItemType.Treat, isAdditionalPrice: isAdditionalPrice);

                    var treat = new Treat(GuidGenerator.Create())
                    {
                        #region 添加诊疗

                        FeeTypeMainCode = treatByCode.FrequencyCode, //TODO 目前没有
                        FeeTypeSubCode = treatByCode.FeeTypeSubCode, //TODO 目前没有
                        FrequencyCode = "",
                        OtherPrice = decimal.Parse(treatByCode.OtherPrice.ToString("f3")),
                        Additional = treatByCode.Additional,
                        Specification = treatByCode.Specification,
                        ProjectMerge = treatByCode.ProjectMerge,
                        DoctorsAdviceId = treatDoctorsAdvice.Id,
                        FrequencyName = "",
                        LongDays = 1,
                        ProjectType = treatByCode.CategoryCode, //TODO
                        ProjectName = treatByCode.CategoryName, //TODO
                        TreatId = treatByCode.Id,
                        AdditionalItemsId = entity.Id,
                        AdditionalItemsType = EAdditionalItemType.SkinAdditional

                        #endregion
                    };

                    if (isChild && treatByCode.Additional && treatByCode.OtherPrice > 0)
                    {
                        //isAdditionalPrice = true;
                        //amount = decimal.Parse(treatByCode.OtherPrice.ToString("f3")) * recieveQty;

                        treat.DoctorsAdvice = treatDoctorsAdvice; //手动复制，处理价格更新问题
                        amount = treat.GetAmount(model.Patientinfo.IsChildren(), out isAdditionalPrice);
                        if (amount > 0) treatDoctorsAdvice.UpdateAmount(amount, isAdditionalPrice);

                    }

                    mtm.Add(new MedicalTechnologyMap(treat.Id, EDoctorsAdviceItemType.Treat));
                    listTreat.Add(treat);
                    listDoctorAdvie.Add(treatDoctorsAdvice);
                }
            }

            if (prescribe.UsageCode == _configuration["UsageIntravenousDripCode"] &&
                prescribe.FrequencyCode.ToUpper() is "BID" or "TID")
            {

                var treatResponse = await _grpcMasterDataClient.GetTreatProjectByCodeAsync(
                    new GetTreatProjectByCodeRequest
                    { Code = _configuration["ContinuousIntravenousInfusionCode"] });
                var treatFrequency = treatResponse.TreatProject;
                if (treatFrequency != null)
                {
                    bool isAdditionalPrice = false;
                    var recieveQty = (prescribe.FrequencyCode.ToUpper() is "BID" ? 1 : 2) * prescribe.LongDays;
                    var amount = decimal.Parse(treatFrequency.Price.ToString("f3")) * recieveQty;
                    var detailId = await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), DETAILID);

                    var treatDoctorsAdvice = new DoctorsAdvice(
                        id: GuidGenerator.Create(),
                        detailId: detailId,
                        platformType: entity.PlatformType,
                        piid: entity.PIID,
                        patientId: entity.PatientId,
                        patientName: entity.PatientName,
                        code: treatFrequency.TreatCode,
                        name: treatFrequency.TreatName,
                        categoryCode: treatFrequency.DictionaryCode,
                        categoryName: treatFrequency.DictionaryName,
                        isBackTracking: false,
                        prescriptionNo: "", //TODO 目前没有 
                        recipeNo: (await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "RecipeNo"))
                        .ToString(), //DOTO 临时用置空，等下处理
                        recipeGroupNo: 1, //DOTO  等下处理  
                        applyTime: DateTime.Now,
                        applyDeptCode: entity.ApplyDeptCode,
                        applyDeptName: entity.ApplyDeptName,
                        applyDoctorCode: entity.ApplyDoctorCode,
                        applyDoctorName: entity.ApplyDoctorName,
                        traineeCode: entity.TraineeCode,
                        traineeName: entity.TraineeName,
                        payTypeCode: "", //TODO 目前没有
                        payType: ERecipePayType.Self, //TODO 目前没有
                        price: decimal.Parse(treatFrequency.Price.ToString("f3")), //project.Price,
                        unit: treatFrequency.Unit,
                        amount: amount,
                        insuranceCode: "", //TODO 目前没有
                        insuranceType: EInsuranceCatalog.Self,
                        isChronicDisease: entity.IsChronicDisease,
                        isRecipePrinted: false,
                        hisOrderNo: "",
                        diagnosis: entity.Diagnosis,
                        execDeptCode: entity.ExecDeptCode, //treatByCode.ExecDeptCode,
                        execDeptName: entity.ExecDeptName, //treatByCode.ExecDeptName,
                        positionCode: "",
                        positionName: "",
                        remarks: "",
                        chargeCode: entity.ChargeCode,
                        chargeName: entity.ChargeName,
                        prescribeTypeCode: entity.PrescribeTypeCode,
                        prescribeTypeName: entity.PrescribeTypeName,
                        startTime: null,
                        endTime: null,
                        recieveQty: recieveQty,
                        recieveUnit: treatFrequency.Unit,
                        pyCode: treatFrequency.PyCode,
                        wbCode: treatFrequency.WbCode,
                        itemType: EDoctorsAdviceItemType.Treat, isAdditionalPrice: isAdditionalPrice);

                    var treat = new Treat(GuidGenerator.Create())
                    {
                        #region 添加诊疗

                        FeeTypeMainCode = treatFrequency.FrequencyCode, //TODO 目前没有
                        FeeTypeSubCode = treatFrequency.FeeTypeSubCode, //TODO 目前没有
                        FrequencyCode = "",
                        OtherPrice = decimal.Parse(treatFrequency.OtherPrice.ToString("f3")),
                        Additional = treatFrequency.Additional,
                        Specification = treatFrequency.Specification,
                        ProjectMerge = treatFrequency.ProjectMerge,
                        DoctorsAdviceId = treatDoctorsAdvice.Id,
                        FrequencyName = "",
                        LongDays = 1,
                        ProjectType = treatFrequency.CategoryCode, //TODO
                        ProjectName = treatFrequency.CategoryName, //TODO
                        TreatId = treatFrequency.Id,
                        AdditionalItemsId = entity.Id,
                        AdditionalItemsType = EAdditionalItemType.FrequencyAdditional

                        #endregion
                    };

                    if (isChild && treatFrequency.Additional && treatFrequency.OtherPrice > 0)
                    {
                        treat.DoctorsAdvice = treatDoctorsAdvice; //手动复制，处理价格更新问题
                        amount = treat.GetAmount(model.Patientinfo.IsChildren(), out isAdditionalPrice);
                        if (amount > 0) treatDoctorsAdvice.UpdateAmount(amount, isAdditionalPrice);
                    }

                    mtm.Add(new MedicalTechnologyMap(treat.Id, EDoctorsAdviceItemType.Treat));
                    listTreat.Add(treat);
                    listDoctorAdvie.Add(treatDoctorsAdvice);
                }
            }

            #endregion
        }

        await _doctorsAdviceRepository.UpdateManyAsync(entities);
        //查询附加处置项
        var treats = await (await _treatRepository.GetQueryableAsync()).Where(x =>
            x.AdditionalItemsType != EAdditionalItemType.No &&
            entitiesIds.Contains(x.AdditionalItemsId)).ToListAsync();
        //删除附加处置
        if (treats.Any()) await DeleteAsync(treats.Select(s => s.DoctorsAdviceId).ToList());
        if (listDoctorAdvie.Any())
        {
            AdmissionRecordDto admissionRecordDto = await task;
            if (admissionRecordDto != null)
            {
                foreach (var item in listDoctorAdvie)
                {
                    item.AreaCode = admissionRecordDto.AreaCode;
                }
            }
            await _doctorsAdviceRepository.InsertManyAsync(listDoctorAdvie);
            var maps = new List<DoctorsAdviceMap>();
            listDoctorAdvie.ForEach(x => maps.Add(new DoctorsAdviceMap(x.Id)));
            await _doctorsAdviceMapRepository.InsertManyAsync(maps);
        }

        if (listTreat.Any()) await _treatRepository.InsertManyAsync(listTreat);
        if (mtm.Any()) await _medicalTechnologyMapRepository.InsertManyAsync(mtm);

        return await Task.FromResult(entities.Select(s => s.Id).ToList());
    }

    /// <summary>
    /// 根据Id获取医嘱详细信息
    /// </summary>
    /// <param name="id">医嘱id</param>
    /// <returns></returns> 
    public async Task<AdviceDetailDto> GetDetailAsync(Guid id)
    {
        var advice = await (await _doctorsAdviceRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == id);
        if (advice == null) Oh.Error("未能获取该医嘱信息");

        var data = new AdviceDetailDto
        {
            DoctorsAdvice = ObjectMapper.Map<DoctorsAdvice, ModifyDoctorsAdviceBaseDto>(advice)
        };
        var fildata = ObjectMapper.Map<DoctorsAdvice, DoctorsAdvicePartialDto>(advice);

        if (advice.ItemType == EDoctorsAdviceItemType.Pacs)
        {
            var pacs = await (await _pacsRepository.GetQueryableAsync()).Include(x => x.PacsItems)
                .FirstOrDefaultAsync(w => w.DoctorsAdviceId == advice.Id);
            if (pacs != null)
            {
                PacsPathologyItem pacsPathologyItem = await _pacsPathologyItemRepository.FindAsync(x => x.PacsId == pacs.Id);
                PacsDto model = ObjectMapper.Map<Pacs, PacsDto>(pacs);
                if (pacsPathologyItem != null)
                {
                    model.PacsPathologyItemDto = new PacsPathologyItemDto();
                    model.PacsPathologyItemDto.Specimen = pacsPathologyItem.Specimen;
                    model.PacsPathologyItemDto.DrawMaterialsPart = pacsPathologyItem.DrawMaterialsPart;
                    model.PacsPathologyItemDto.SpecimenQty = pacsPathologyItem.SpecimenQty;
                    model.PacsPathologyItemDto.LeaveTime = pacsPathologyItem.LeaveTime;
                    model.PacsPathologyItemDto.RegularTime = pacsPathologyItem.RegularTime;
                    model.PacsPathologyItemDto.SpecificityInfect = pacsPathologyItem.SpecificityInfect;
                    model.PacsPathologyItemDto.ApplyForObjective = pacsPathologyItem.ApplyForObjective;
                    model.PacsPathologyItemDto.Symptom = pacsPathologyItem.Symptom;
                }
                model.FillData(fildata);
                data.Pacs = model;
            }
        }
        else if (advice.ItemType == EDoctorsAdviceItemType.Lis)
        {
            var lis = await (await _lisRepository.GetQueryableAsync()).Include(x => x.LisItems)
                .FirstOrDefaultAsync(w => w.DoctorsAdviceId == advice.Id);
            if (lis != null)
            {
                var model = ObjectMapper.Map<Lis, LisDto>(lis);
                model.FillData(fildata);
                data.Lis = model;
            }
        }
        else if (advice.ItemType == EDoctorsAdviceItemType.Prescribe)
        {
            var prescribe = await (await _prescribeRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.DoctorsAdviceId == advice.Id);
            if (prescribe != null)
            {
                var model = ObjectMapper.Map<Prescribe, PrescribeDto>(prescribe);
                model.FillData(fildata);
                data.Prescribe = model;
            }
        }
        else if (advice.ItemType == EDoctorsAdviceItemType.Treat)
        {
            var treat = await (await _treatRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.DoctorsAdviceId == advice.Id);
            if (treat != null)
            {
                var model = ObjectMapper.Map<Treat, TreatDto>(treat);
                model.FillData(fildata);
                data.Treat = model;
            }
        }

        return data;
    }

    /// <summary>
    /// 更新医嘱详细内容
    /// </summary>
    /// <see cref="AdviceDetailDto"/>
    /// <returns></returns> 
    [UnitOfWork]
    public async Task<Guid> ModifyDetailAsync(AdviceDetailDto model)
    {
        var doctorsAdvices = new List<DoctorsAdvice>();
        var advice = model.DoctorsAdvice;
        if (!advice.Id.HasValue) Oh.Error("更改医嘱信息需要先确定医嘱Id不能为空");
        var doctorsAdviceId = advice.Id.Value;
        var entity = await (await _doctorsAdviceRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == doctorsAdviceId);
        var recipeNo = entity.RecipeNo;

        entity.UpdatePartial1(
            applyDoctorCode: advice.ApplyDoctorCode,
            applyDoctorName: advice.ApplyDoctorName,
            applyDeptCode: advice.ApplyDeptCode,
            applyDeptName: advice.ApplyDeptName,
            traineeCode: advice.TraineeCode,
            traineeName: advice.TraineeName,
            isChronicDisease: advice.IsChronicDisease,
            diagnosis: advice.Diagnosis);

        if (entity.ItemType == EDoctorsAdviceItemType.Pacs)
        {
            if (!model.Pacs.Id.HasValue) Oh.Error("检查项没有指定Id，无法做修改操作");

            Pacs pacs = await (await _pacsRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.Pacs.Id.Value);

            entity.ExecDeptCode = model.Pacs.ExecDeptCode;
            entity.ExecDeptName = model.Pacs.ExecDeptName;
            pacs.IsBedSide = model.Pacs.IsBedSide;
            pacs.IsEmergency = model.Pacs.IsEmergency;
            pacs.PartCode = model.Pacs.PartCode;
            pacs.PartName = model.Pacs.PartName;
            entity.Remarks = model.Pacs.Remarks;
            await _pacsRepository.UpdateAsync(pacs);

            //await ModifyPacsAsync(model.Pacs);
            //var advicePartial = ObjectMapper.Map<PacsDto, DoctorsAdvicePartial>(model.Pacs);
            //entity.UpdatePartial2(advicePartial);
            //entity.ReSetAmount(model.Pacs.GetAmount());
        }
        else if (entity.ItemType == EDoctorsAdviceItemType.Lis)
        {
            await ModifyLisAsync(model.Lis);
            var advicePartial = ObjectMapper.Map<LisDto, DoctorsAdvicePartial>(model.Lis);
            entity.UpdatePartial2(advicePartial);
            entity.ReSetAmount(model.Lis.GetAmount());
        }
        else if (entity.ItemType == EDoctorsAdviceItemType.Prescribe)
        {
            await ModifyPrescribeAsync(model.Prescribe);
            var advicePartial = ObjectMapper.Map<PrescribeDto, DoctorsAdvicePartial>(model.Prescribe);
            entity.UpdatePartial2(advicePartial);
            entity.ReSetAmount(model.Prescribe.GetAmount());
        }
        else if (entity.ItemType == EDoctorsAdviceItemType.Treat)
        {
            model.SetTreatFrequencyNull();
            await ModifyTreatAsync(model.Treat);
            var advicePartial = ObjectMapper.Map<TreatDto, DoctorsAdvicePartial>(model.Treat);
            entity.UpdatePartial2(advicePartial);
            bool isAdditionalPrice = false;
            entity.ReSetAmount(model.Treat.GetAmount(model.PatientInfo.IsChildren(), out isAdditionalPrice));
            entity.IsAdditionalPrice = isAdditionalPrice;
        }

        #region 校验频次，用法和计划时间

        //只处理院前的药品合组问题
        if (entity.PlatformType == EPlatformType.PreHospital &&
            model.DoctorsAdvice.ItemType == EDoctorsAdviceItemType.Prescribe && !entity.RecipeNo.IsNullOrEmpty())
        {
            var groupAdvice = await (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w =>
                    w.ItemType == EDoctorsAdviceItemType.Prescribe && w.RecipeNo == recipeNo &&
                    w.Id != doctorsAdviceId)
                .ToListAsync();
            if (groupAdvice.Any()) //如果存在相同的需要合组，且做合组校验
            {
                var firstAdvice = groupAdvice.FirstOrDefault();
                var prescribe =
                    await (await _prescribeRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.DoctorsAdviceId == firstAdvice.Id);
                if (prescribe.UsageCode != model.Prescribe.UsageCode ||
                    prescribe.FrequencyCode != model.Prescribe.FrequencyCode) Oh.Error("医嘱合组时，频次和用法必须一致");

                entity.ApplyTime = firstAdvice.ApplyTime; //同步集合开始时间
                entity.StartTime = firstAdvice.StartTime;

                if (!entity.RecipeNo.IsNullOrEmpty())
                {
                    doctorsAdvices.Add(entity);
                    foreach (var item in groupAdvice)
                    {
                        doctorsAdvices.Add(item);
                    }

                    var counter = 1;
                    foreach (var item in doctorsAdvices)
                    {
                        item.RecipeGroupNo = counter++;
                    }
                }
            }
        }

        if (!doctorsAdvices.Exists(w => w.Id == entity.Id)) doctorsAdvices.Add(entity);

        #endregion

        #region 拆组，重新排组

        if (entity.RecipeNo.Trim() == "" || entity.RecipeNo.Trim() == "0" ||
            entity.RecipeNo.Trim().ToLower() == "auto")
        {
            entity.RecipeNo = (await _mySequenceRepository.GetSequenceAsync(nameof(DoctorsAdvice), "RecipeNo"))
                .ToString();
            entity.RecipeGroupNo = 1;

            var oldAdvices = await (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => w.RecipeNo == recipeNo && w.Id != entity.Id)
                .OrderBy(o => o.RecipeGroupNo).ToListAsync();

            if (oldAdvices.Any())
            {
                var applyTime = oldAdvices.Max(s => s.ApplyTime);
                var startTime = oldAdvices.Max(s => s.StartTime);
                var counter = 1;
                foreach (var oldAdvice in oldAdvices)
                {
                    oldAdvice.RecipeGroupNo = counter++;
                    oldAdvice.ApplyTime = applyTime;
                    oldAdvice.StartTime = startTime;

                    if (!doctorsAdvices.Exists(w => w.Id == oldAdvice.Id)) doctorsAdvices.Add(oldAdvice);
                }
            }
        }

        #endregion

        await _doctorsAdviceRepository.UpdateManyAsync(doctorsAdvices);

        return await Task.FromResult(entity.Id);
    }

    /// <summary>
    /// 修改检查
    /// </summary>
    /// <returns></returns>um/
    private async Task ModifyPacsAsync(PacsDto pacsDto)
    {
        if (!pacsDto.Id.HasValue) Oh.Error("检查项没有指定Id，无法做修改操作");

        Pacs pacs = await (await _pacsRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == pacsDto.Id.Value);
        Pacs updateEntity = ObjectMapper.Map<PacsDto, Pacs>(pacsDto);
        pacs.Update(updateEntity);
        Pacs data = await _pacsRepository.UpdateAsync(pacs);

        List<PacsItem> oldPacsItems = await (await _pacsItemRepository.GetQueryableAsync()).Where(w => w.PacsId == pacs.Id).ToListAsync();
        await _pacsItemRepository.DeleteManyAsync(oldPacsItems);

        List<PacsItem> newPacsItems = new List<PacsItem>();
        List<MedicalTechnologyMap> mtm = new List<MedicalTechnologyMap>();
        foreach (PacsItemDto pacsItemDto in pacsDto.Items)
        {
            PacsItem pacsItem = new PacsItem(
                id: GuidGenerator.Create(),
                targetCode: pacsItemDto.TargetCode,
                targetName: pacsItemDto.TargetName,
                targetUnit: pacsItemDto.TargetUnit,
                price: pacsItemDto.Price,
                qty: pacsItemDto.Qty,
                insuranceCode: pacsItemDto.InsuranceCode,
                insuranceType: pacsItemDto.InsuranceType,
                projectCode: pacsItemDto.ProjectCode,
                otherPrice: pacsItemDto.OtherPrice,
                specification: pacsItemDto.Specification,
                sort: pacsItemDto.Sort,
                pyCode: pacsItemDto.PyCode,
                wbCode: pacsItemDto.WbCode,
                specialFlag: pacsItemDto.SpecialFlag,
                isActive: pacsItemDto.IsActive,
                projectType: pacsItemDto.ProjectType,
                projectMerge: pacsItemDto.ProjectMerge,
                pacsId: pacs.Id);

            mtm.Add(new MedicalTechnologyMap(pacsItem.Id, EDoctorsAdviceItemType.Pacs));
            newPacsItems.Add(pacsItem);
        }
        if (mtm.Any()) await _medicalTechnologyMapRepository.InsertManyAsync(mtm);
        if (newPacsItems.Count > 0) await _pacsItemRepository.InsertManyAsync(newPacsItems);

        if (pacsDto.PacsPathologyItemDto != null)
        {
            PacsPathologyItem pacsPathologyItem = await _pacsPathologyItemRepository.FindAsync(x => x.PacsId == pacs.Id);
            if (pacsPathologyItem == null)
            {
                pacsPathologyItem = new PacsPathologyItem(GuidGenerator.Create());
                pacsPathologyItem.PacsId = pacs.Id;
                pacsPathologyItem.Specimen = pacsDto.PacsPathologyItemDto.Specimen;
                pacsPathologyItem.DrawMaterialsPart = pacsDto.PacsPathologyItemDto.DrawMaterialsPart;
                pacsPathologyItem.SpecimenQty = pacsDto.PacsPathologyItemDto.SpecimenQty;
                pacsPathologyItem.LeaveTime = pacsDto.PacsPathologyItemDto.LeaveTime;
                pacsPathologyItem.RegularTime = pacsDto.PacsPathologyItemDto.RegularTime;
                pacsPathologyItem.SpecificityInfect = pacsDto.PacsPathologyItemDto.SpecificityInfect;
                pacsPathologyItem.ApplyForObjective = pacsDto.PacsPathologyItemDto.ApplyForObjective;
                pacsPathologyItem.Symptom = pacsDto.PacsPathologyItemDto.Symptom;
                await _pacsPathologyItemRepository.InsertAsync(pacsPathologyItem);
            }
            else
            {
                pacsPathologyItem.Specimen = pacsDto.PacsPathologyItemDto.Specimen;
                pacsPathologyItem.DrawMaterialsPart = pacsDto.PacsPathologyItemDto.DrawMaterialsPart;
                pacsPathologyItem.SpecimenQty = pacsDto.PacsPathologyItemDto.SpecimenQty;
                pacsPathologyItem.LeaveTime = pacsDto.PacsPathologyItemDto.LeaveTime;
                pacsPathologyItem.RegularTime = pacsDto.PacsPathologyItemDto.RegularTime;
                pacsPathologyItem.SpecificityInfect = pacsDto.PacsPathologyItemDto.SpecificityInfect;
                pacsPathologyItem.ApplyForObjective = pacsDto.PacsPathologyItemDto.ApplyForObjective;
                pacsPathologyItem.Symptom = pacsDto.PacsPathologyItemDto.Symptom;
                await _pacsPathologyItemRepository.UpdateAsync(pacsPathologyItem);
            }

            await _pacsPathologyItemNoRepository.DeleteAsync(x => x.PacsId == pacs.Id);
            if (!string.IsNullOrEmpty(pacsDto.PacsPathologyItemDto.Specimen))
            {
                string[] specimenNames = pacsDto.PacsPathologyItemDto.Specimen.Split(",", StringSplitOptions.RemoveEmptyEntries);
                if (specimenNames.Any())
                {
                    List<PacsPathologyItemNo> pacsPathologyItemNos = new List<PacsPathologyItemNo>();
                    foreach (string item in specimenNames)
                    {
                        PacsPathologyItemNo pacsPathologyItemNo = new PacsPathologyItemNo()
                        {
                            PacsId = pacs.Id,
                            SpecimenName = item
                        };
                        pacsPathologyItemNos.Add(pacsPathologyItemNo);
                    }
                    await _pacsPathologyItemNoRepository.InsertManyAsync(pacsPathologyItemNos);
                }
            }
        }
    }

    /// <summary>
    /// 修改检验
    /// </summary>
    /// <returns></returns>
    private async Task ModifyLisAsync(LisDto lis)
    {
        if (!lis.Id.HasValue) Oh.Error("检验项没有指定Id，无法做修改操作");

        var entity = await (await _lisRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == lis.Id.Value);

        var updateEntity = ObjectMapper.Map<LisDto, Lis>(lis);
        entity.Update(updateEntity);

        var data = await _lisRepository.UpdateAsync(entity);
        var items = await (await _lisItemRepository.GetQueryableAsync()).Where(w => w.LisId == entity.Id).ToListAsync();
        await _lisItemRepository.DeleteManyAsync(items);

        var newItems = new List<LisItem>();
        var mtm = new List<MedicalTechnologyMap>();

        foreach (var item in lis.Items)
        {
            var listarget = new LisItem(
                id: GuidGenerator.Create(),
                targetCode: item.TargetCode,
                targetName: item.TargetName,
                targetUnit: item.TargetUnit,
                price: item.Price,
                qty: item.Qty,
                insuranceCode: item.InsuranceCode,
                insuranceType: item.InsuranceType,
                projectCode: item.ProjectCode,
                otherPrice: item.OtherPrice,
                specification: item.Specification,
                sort: item.Sort,
                pyCode: item.PyCode,
                wbCode: item.WbCode,
                specialFlag: item.SpecialFlag,
                isActive: item.IsActive,
                projectType: item.ProjectType,
                projectMerge: item.ProjectMerge,
                lisId: entity.Id);

            mtm.Add(new MedicalTechnologyMap(listarget.Id, EDoctorsAdviceItemType.Lis));
            newItems.Add(listarget);
        }

        if (mtm.Any()) await _medicalTechnologyMapRepository.InsertManyAsync(mtm);
        if (newItems.Count > 0) await _lisItemRepository.InsertManyAsync(newItems);
    }

    /// <summary>
    /// 修改药方
    /// </summary>
    /// <returns></returns>
    private async Task ModifyPrescribeAsync(PrescribeDto prescribe)
    {
        if (!prescribe.Id.HasValue) Oh.Error("药方项没有指定Id，无法做修改操作");
        var entity = await (await _prescribeRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == prescribe.Id.Value);
        var updateEntity = ObjectMapper.Map<PrescribeDto, Prescribe>(prescribe);
        updateEntity.SetHisDosageQty();

        //自定义一次剂量处理 TODO
        var dosageList = await _dosageAppService.GetAllCustomDosagesAsync();
        var dosage = dosageList.FirstOrDefault(w => w.Code == prescribe.Code);
        if (dosage != null)
        {
            var mydosageQty = dosage.GetHisDosageQty(prescribe.DosageQty, prescribe.DosageUnit);
            updateEntity.CommitHisDosageQty = mydosageQty;
        }

        entity.Update(updateEntity);
        var data = await _prescribeRepository.UpdateAsync(entity);
    }

    /// <summary>
    /// 修改诊疗
    /// </summary>
    /// <returns></returns>
    private async Task ModifyTreatAsync(TreatDto treat)
    {
        if (!treat.Id.HasValue) Oh.Error("药方项没有指定Id，无法做修改操作");
        var entity = await (await _treatRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == treat.Id.Value);
        var updateEntity = ObjectMapper.Map<TreatDto, Treat>(treat);
        entity.Update(updateEntity);
        var data = await _treatRepository.UpdateAsync(entity);
    }

    /// <summary>
    /// 判断患者是否已开医嘱，能否流转到就诊区
    /// </summary>
    /// <param name="pI_ID"></param>
    /// <returns></returns>
    public async Task<bool> GetIsTransferByRecipeAsync(Guid pI_ID)
    {
        if (await (await _doctorsAdviceRepository.GetQueryableAsync()).AnyAsync(w => w.PIID == pI_ID))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 查询医嘱状态并更新相关操作
    /// </summary>
    /// <returns></returns> 
    [AllowAnonymous]
    [NonAction]
    public async Task QueryAndOptMedicalInfoAsync()
    {
        //更新普通的医嘱状态
        try
        {
            var hours72 = GetCofHours();
            List<DoctorsAdvice> doctorsAdvices = await _doctorsAdviceRepository.GetListAsync(x => x.Status == ERecipeStatus.Submitted && (x.PayStatus == EPayStatus.NoPayment || string.IsNullOrEmpty(x.InvoiceNo)) && !string.IsNullOrEmpty(x.HisOrderNo) && x.CreationTime > hours72);
            if (!doctorsAdvices.Any())
            {
                return;
            }

            await UpdateRecipeBillStatusAsync(doctorsAdvices);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"查询医嘱状态功能异常：{ex.Message}");
        }
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

    /// <summary>
    /// 获取医嘱状态查询配置时间范围，如果配置错误默认72小时
    /// </summary>
    /// <returns></returns>
    private DateTime GetCofHours()
    {
        var hours = 72;
        int.TryParse(_configuration.GetSection("QueryMedicalInfo:TimeRange").Value, out hours);
        return DateTime.Now.AddHours(-hours);
    }

    /// <summary>
    /// 验证当前患者状态（页面停留与实际状态不一致）
    /// </summary>
    /// <param name="PIID"></param>
    /// <returns></returns>
    public async Task CheckPatientStatusAsync(Guid PIID)
    {
        try
        {
            if (PIID != Guid.Empty)
            {
                var patientInfo = await _patientAppService.GetPatientInfoAsync(PIID);
                if (patientInfo == null)
                {
                    Oh.Error(message: "未查询到患者信息");
                }

                if (patientInfo.VisitStatus == EVisitStatus.出科 || patientInfo.VisitStatus == EVisitStatus.已退号 || patientInfo.VisitStatus == EVisitStatus.过号 || patientInfo.VisitStatus == EVisitStatus.已就诊)
                {
                    var status = Enum.GetName(typeof(EVisitStatus), patientInfo.VisitStatus);
                    Oh.Error(message: $"当前患者就诊已结束,就诊状态为【{status}】,请刷新或返回到患者列表！");
                }
            }
        }
        catch (EcisBusinessException)
        {
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine($"验证当前患者状态:{e}");
        }
    }

    /// <summary>
    /// 同步患者医嘱状态
    /// </summary>
    /// <param name="pi_Id">患者Id</param>
    /// <returns></returns>
    public async Task<bool> GetChangeBillStatusAsync(Guid pi_Id)
    {
        try
        {
            List<DoctorsAdvice> doctorsAdvices = await _doctorsAdviceRepository.GetListAsync(x => x.PIID == pi_Id && !string.IsNullOrEmpty(x.HisOrderNo));
            if (!doctorsAdvices.Any())
            {
                return true;
            }

            await UpdateRecipeBillStatusAsync(doctorsAdvices);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"更新医嘱状态异常： {ex.Message}");
            Oh.Error($"更新异常，{ex.Message}");
        }
        return true;
    }

    /// <summary>
    /// HIS医嘱状态变更推送或调用接口(HIS直接提供状态变化的数据)
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<int> HisChangeBillStatusAsync(HisChannelBillStatusDto dto)
    {
        try
        {
            var mzBillIds = dto.MzBillId.Split(",").ToList();
            var prescriptionNos = new List<string>();
            mzBillIds.ForEach(x =>
            {
                prescriptionNos.Add(Regex.Match(x, "[a-zA-Z]+").Value.Substring(0, 1) + Regex.Match(x, "[0-9]+").Value);
            });

            var data = await _prescriptionRepository.GetPrescriptionsByBillNosAsync(dto.VisSerialNo, prescriptionNos);
            if (!data.Any()) return 0;

            //再查询一遍，确认再请求http前拿最近的数据

            var adviceids = data.Select(s => s.DoctorsAdviceId).ToList();
            var advices = await _doctorsAdviceRepository.GetDoctorsAdvicesByIdsAsync(adviceids);

            var updatepartAdvices = new List<DoctorsAdvice>();
            var updateAdvices = new List<DoctorsAdvice>();

            foreach (var current in data)
            {
                if (dto.BillState == 0) continue;
                var currentAdvice = advices.FirstOrDefault(w => w.Id == current.DoctorsAdviceId);
                if (currentAdvice == null) continue;
                if (currentAdvice.Status == ERecipeStatus.Cancelled) continue;

                current.BillState = dto.BillState;
                var status = ERecipeStatus.Submitted;

                //当HIS上的状态和急诊的状态一致的时候不处理 
                //已缴费
                if (dto.BillState == 1 && currentAdvice.Status == ERecipeStatus.PayOff) continue;
                //已执行
                if (dto.BillState == 2 && currentAdvice.Status == ERecipeStatus.Executed) continue;
                //已退费
                if (dto.BillState == 3 && currentAdvice.Status == ERecipeStatus.ReturnPremium) continue;
                //已作废
                if (dto.BillState == -1 && currentAdvice.Status == ERecipeStatus.Cancelled) continue;

                _logger.LogInformation($"[-] VisSerialNo={dto.VisSerialNo},当前单有变化，状态从[{currentAdvice.Status.GetDescription()}]->BillState=[{dto.BillState} , BillState=[1:已缴费,2:已执行,3:已退费,-1:已作废]]");

                switch (dto.BillState)
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
                return 1; //有状态更新并且成功
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"HIS传递过来的医嘱状态更新出现异常，就诊流水号：{dto.VisSerialNo}, 订单单号：{dto.MzBillId}");
            Oh.Error($"更新医嘱状态异常:{ex.Message}");
        }
        return 0; //没有状态更新
    }

    /// <summary>
    /// 根据Pacs /Lis Id 获取对应的Item数据
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    public async Task<List<LisOrPacsItemDto>> GetLisItemAsync(List<GetLisOrPacsItemDto> ids)
    {
        if (ids is null || ids.Count == 0)
            return default;
        var lisIds = ids.Where(c => c.ItemType == EDoctorsAdviceItemType.Lis).Select(c => c.Id).ToList();
        var pacsIds = ids.Where(c => c.ItemType == EDoctorsAdviceItemType.Pacs).Select(c => c.Id).ToList();
        var lisList = await _lisItemRepository.GetListAsync(c => lisIds.Contains(c.LisId));
        var pacsList = await _pacsItemRepository.GetListAsync(c => pacsIds.Contains(c.PacsId));
        var returnList = ids.Select(c => new LisOrPacsItemDto()
        {
            ItemType = c.ItemType,
            LisItemList = c.ItemType == EDoctorsAdviceItemType.Lis ? lisList?.Where(lis => lis.LisId == c.Id).Select(lis => ObjectMapper.Map<LisItem, LisItemDto>(lis)).ToList() : default,
            PacsItemList = c.ItemType == EDoctorsAdviceItemType.Pacs ? pacsList?.Where(pac => pac.PacsId == c.Id).Select(pac => ObjectMapper.Map<PacsItem, PacsItemDto>(pac)).ToList() : default
        }).ToList();
        return returnList;
    }
}