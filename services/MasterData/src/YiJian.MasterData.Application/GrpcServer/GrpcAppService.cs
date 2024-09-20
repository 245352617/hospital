using Grpc.Core;
using MasterDataService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUglify.Helpers;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using YiJian.Apis;
using YiJian.ECIS.DDP;
using YiJian.ECIS.Grpc;
using YiJian.ECIS.ShareModel.DDPs;
using YiJian.ECIS.ShareModel.DDPs.Requests;
using YiJian.ECIS.ShareModel.DDPs.Responses;
using YiJian.MasterData.AllItems;
using YiJian.MasterData.DictionariesMultitypes;
using YiJian.MasterData.Domain;
using YiJian.MasterData.Exams;
using YiJian.MasterData.Labs;
using YiJian.MasterData.Labs.Position;
using YiJian.MasterData.MasterData.Treats;
using YiJian.MasterData.Medicines;
using YiJian.MasterData.Separations.Contracts;
using YiJian.MasterData.Separations.Entities;
using YiJian.MasterData.Treats;

namespace YiJian.MasterData.MasterData.Medicines;

/// <summary>
/// Rpc服务
/// </summary>
public class GrpcAppService : GrpcMasterData.GrpcMasterDataBase, IGrpcAppService
{

    private readonly IMedicineRepository _medicineRepository;
    private readonly IHISMedicineRepository _hISMedicineRepository;
    private readonly ILabProjectRepository _labProjectRepository;
    private readonly IExamProjectRepository _examProjectRepository;
    private readonly IExamCatalogRepository _examCatalogRepository;
    private readonly ITreatRepository _treatRepository;
    private readonly IMedicineFrequencyRepository _medicineFrequencieRepository;
    private readonly IAllItemRepository _allItemRepository;
    private readonly ISeparationRepository _separationRepository;
    private readonly IUsageRepository _usageRepository;
    private readonly ILabSpecimenRepository _labSpecimenRepository;
    private readonly ILabSpecimenPositionRepository _labSpecimenPositionRepository;
    private readonly IExamPartRepository _examPartRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IConsultingRoomRepository _consultingRoomRepository;
    private readonly ILabTargetRepository _labTargetRepository;
    private readonly ILabReportInfoRepository _labReportInfoRepository;
    private readonly IExamTargetRepository _examTargetRepository;
    private readonly IMedicineUsageRepository _medicineUsageRepository;
    private readonly ITreatGroupRepository _treatGroupRepository;
    private readonly ILogger<GrpcAppService> _log;
    private readonly IExamNoteRepository _examNoteRepository;
    private readonly IDictionariesMultitypeRepository _dictionariesMultitypeRepository;
    private readonly IDictionariesRepository _dictionariesRepository;
    private readonly INursingRecipeTypeRepository _nursingRecipeTypeRepository;
    private readonly IExamAttachItemsRepository _examAttachItemsRepository;
    private readonly IOptionsMonitor<DdpHospital> _ddpHospitalOptionsMonitor;
    private readonly DdpHospital _ddpHospital;
    private readonly DdpSwitch _ddpSwitch;
    private readonly IDdpApiService _ddpApiService;
    private readonly IObjectMapper ObjectMapper;
    private readonly IUnitOfWorkManager UnitOfWorkManager;
    private readonly List<ExamExecDeptConfig> _examExecDeptConfigs;
    private readonly IMemoryCache _cache;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="_objectMapper"></param>
    /// <param name="_unitOfWorkManager"></param>
    /// <param name="medicineRepository"></param>
    /// <param name="hISMedicineRepository"></param>
    /// <param name="labProjectRepository"></param>
    /// <param name="examProjectRepository"></param>
    /// <param name="treatRepository"></param>
    /// <param name="medicineFrequencieRepository"></param>
    /// <param name="allItemRepository"></param>
    /// <param name="separationRepository"></param>
    /// <param name="usageRepository"></param>
    /// <param name="labSpecimenRepository"></param>
    /// <param name="labSpecimenPositionRepository"></param>
    /// <param name="examPartRepository"></param>
    /// <param name="departmentRepository"></param>
    /// <param name="consultingRoomRepository"></param>
    /// <param name="labTargetRepository"></param>
    /// <param name="examTargetRepository"></param>
    /// <param name="medicineUsageRepository"></param>
    /// <param name="nursingRecipeTypeRepository"></param>
    /// <param name="treatGroupRepository"></param>
    /// <param name="log"></param>
    /// <param name="examCatalogRepository"></param>
    /// <param name="examNoteRepository"></param>
    /// <param name="dictionariesMultitypeRepository"></param>
    /// <param name="dictionariesRepository"></param>
    /// <param name="ddpHospitalOptionsMonitor"></param>
    /// <param name="ddpSwitch"></param>
    /// <param name="cache"></param>
    /// <param name="examAttachItemsRepository"></param>
    /// <param name="optionsMonitor"></param>
    /// <param name="labReportInfoRepository"></param>
    public GrpcAppService(
          IObjectMapper _objectMapper
        , IUnitOfWorkManager _unitOfWorkManager
        , IMedicineRepository medicineRepository
        , IHISMedicineRepository hISMedicineRepository
        , ILabProjectRepository labProjectRepository
        , IExamProjectRepository examProjectRepository
        , ITreatRepository treatRepository
        , IMedicineFrequencyRepository medicineFrequencieRepository
        , IAllItemRepository allItemRepository
        , ISeparationRepository separationRepository
        , IUsageRepository usageRepository
        , ILabSpecimenRepository labSpecimenRepository
        , ILabSpecimenPositionRepository labSpecimenPositionRepository
        , IExamPartRepository examPartRepository
        , IDepartmentRepository departmentRepository
        , IConsultingRoomRepository consultingRoomRepository
        , ILabTargetRepository labTargetRepository
        , IExamTargetRepository examTargetRepository
        , IMedicineUsageRepository medicineUsageRepository
        , INursingRecipeTypeRepository nursingRecipeTypeRepository
        , ITreatGroupRepository treatGroupRepository
        , ILogger<GrpcAppService> log
        , IExamCatalogRepository examCatalogRepository
        , IExamNoteRepository examNoteRepository
        , IDictionariesMultitypeRepository dictionariesMultitypeRepository
        , IDictionariesRepository dictionariesRepository
        , IOptionsMonitor<DdpHospital> ddpHospitalOptionsMonitor
        , DdpSwitch ddpSwitch
        , IMemoryCache cache
        , IExamAttachItemsRepository examAttachItemsRepository
        , IOptions<List<ExamExecDeptConfig>> optionsMonitor
        , ILabReportInfoRepository labReportInfoRepository)
    {
        this._medicineRepository = medicineRepository;
        _hISMedicineRepository = hISMedicineRepository;
        this._labProjectRepository = labProjectRepository;
        this._examProjectRepository = examProjectRepository;
        this._treatRepository = treatRepository;
        this._medicineFrequencieRepository = medicineFrequencieRepository;
        _allItemRepository = allItemRepository;

        _separationRepository = separationRepository;
        _usageRepository = usageRepository;
        _labSpecimenRepository = labSpecimenRepository;
        _labSpecimenPositionRepository = labSpecimenPositionRepository;
        _examPartRepository = examPartRepository;
        _departmentRepository = departmentRepository;
        _consultingRoomRepository = consultingRoomRepository;
        _labTargetRepository = labTargetRepository;
        _examTargetRepository = examTargetRepository;
        _medicineUsageRepository = medicineUsageRepository;
        _treatGroupRepository = treatGroupRepository;
        _log = log;
        _examCatalogRepository = examCatalogRepository;
        _examNoteRepository = examNoteRepository;
        _dictionariesMultitypeRepository = dictionariesMultitypeRepository;
        _dictionariesRepository = dictionariesRepository;
        _nursingRecipeTypeRepository = nursingRecipeTypeRepository;
        _ddpHospitalOptionsMonitor = ddpHospitalOptionsMonitor;
        _ddpHospital = _ddpHospitalOptionsMonitor.CurrentValue;
        _ddpSwitch = ddpSwitch;
        _ddpApiService = _ddpSwitch.CreateService(_ddpHospital);
        ObjectMapper = _objectMapper;
        UnitOfWorkManager = _unitOfWorkManager;
        _cache = cache;
        _examAttachItemsRepository = examAttachItemsRepository;
        _examExecDeptConfigs = optionsMonitor.Value;
        _labReportInfoRepository = labReportInfoRepository;
    }

    /// <summary>
    /// 获取药品信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<MedicinesResponse> GetAllMedicines(GetAllMedicinesRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var medicines = await _medicineRepository.GetListAsync(c => c.IsDeleted == false);
            var response = new MedicinesResponse();
            foreach (var medicine in medicines)
            {
                var responseMedicine = ObjectMapper.Map<Medicine, GrpcMedicineModel>(medicine);
                response.Medicines.Add(responseMedicine);
            }

            await uow.CompleteAsync();

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 根据药品Code集合获取药品信息集合
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<MedicinesResponse> GetMedicineListByCodes(GetListMedicinesRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            var response = new MedicinesResponse();

            var medicineIds = request.MedicineIds.Select(s => (decimal)s).ToArray();

            //开启DDP模式
            if (_ddpHospital.DdpSwitch)
            {
                var medicineMap = await GetDdpMedicineListByCodesAsync(medicineIds);
                response.Medicines.AddRange(medicineMap);
            }
            else
            {
                //龙岗模式
                var medicineMap = await GetLgzxMedicineListByCodesAsync(medicineIds);
                response.Medicines.AddRange(medicineMap);
            }

            await uow.CompleteAsync();

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await uow.RollbackAsync();
            throw;
        }
    }


    /// <summary>
    /// 根据药品Code集合获取药品信息集合(北大)
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<MedicinesResponse> GetMedicineListByMedicineCodes(GetListMedicinesByCodeRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            var response = new MedicinesResponse();

            var medicineIds = request.MedicineCode.Select(s => s).ToArray();

            var medicineMap = await GetDdpMedicineListByMedicineCodesAsync(medicineIds);
            response.Medicines.AddRange(medicineMap);

            await uow.CompleteAsync();

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 获取DDP的库存药品数据
    /// </summary>
    /// <param name="medicineIds"></param>
    /// <returns></returns>
    private async Task<List<GrpcMedicineModel>> GetDdpMedicineListByCodesAsync(decimal[] medicineIds)
    {
        //构造请求参数
        var request = new DdpHisMedicineByInvIdsRequest
        {
            InvId = medicineIds.Select(s => s.ToString()).ToList()
        };

        var ddpResponse = await _ddpApiService.GetHisMedicinesByInvidAsync(request);
        if (ddpResponse.Code != 200)
        {
            _log.LogError($"ddp返回异常：{ddpResponse.Code}，异常内容：{ddpResponse.Msg}");
            return new List<GrpcMedicineModel>();
        }
        var medicineMap = ObjectMapper.Map<List<DdpHisMedicineResponse>, List<GrpcMedicineModel>>(ddpResponse.Data);
        return medicineMap;
    }

    /// <summary>
    /// 获取DDP的库存药品数据
    /// </summary>
    /// <param name="medicineCodes"></param>
    /// <returns></returns>
    private async Task<List<GrpcMedicineModel>> GetDdpMedicineListByMedicineCodesAsync(string[] medicineCodes)
    {
        //构造请求参数
        var request = new DdpHisMedicineByInvIdsRequest
        {
            InvId = medicineCodes.Select(s => s.ToString()).ToList()
        };

        var ddpResponse = await _ddpApiService.GetHisMedicinesByCodeAsync(request);
        if (ddpResponse.Code != 200)
        {
            _log.LogError($"ddp返回异常：{ddpResponse.Code}，异常内容：{ddpResponse.Msg}");
            return new List<GrpcMedicineModel>();
        }
        var medicineMap = ObjectMapper.Map<List<DdpHisMedicineResponse>, List<GrpcMedicineModel>>(ddpResponse.Data);
        return medicineMap;
    }


    /// <summary>
    /// 获取龙岗中心的库存药品数据
    /// </summary>
    /// <param name="medicineIds"></param>
    /// <returns></returns>
    private async Task<List<GrpcMedicineModel>> GetLgzxMedicineListByCodesAsync(decimal[] medicineIds)
    {
        var medicines = await _hISMedicineRepository.GetHisMedicinesAsync(medicineIds);
        var medicineMap = ObjectMapper.Map<List<HISMedicine>, List<GrpcMedicineModel>>(medicines);
        return medicineMap;
    }

    /// <summary>
    /// 获取急诊药信息集合
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<HisMedicineSampleResponse> GetHisMedicineSample(HisMedicineSampleRequest request, ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            var response = new HisMedicineSampleResponse();
            if (request.EmergencySign == 0) return response;

            IList<HisMedicineSample> medicines = new List<HisMedicineSample>();

            //走DDP模式
            if (_ddpHospital.DdpSwitch)
            {
                //勾上急诊标志：  ExecDeptCode = 1(西药房) emergencySign in(0, 1)(全科室药品 + 急诊药品)
                var allMedicine = await GetHISMedicinesByDdpApiAsync();
                medicines = allMedicine
                    .Where(w => new decimal[] { 0, 1 }.Contains(w.EmergencySign))
                    .Select(s => new HisMedicineSample
                    {
                        EmergencySign = s.EmergencySign,
                        InvId = s.InvId,
                        IsFirstAid = s.IsFirstAid,
                        MedicineCode = s.MedicineCode,
                        MedicineName = s.MedicineName,
                        PharmacyCode = s.PharmacyCode,
                        PharmacyName = s.PharmacyName,
                    }).ToList();
            }
            else
            {
                //龙岗中心医院
                medicines = await _hISMedicineRepository.GetHisMedicineSampleAsync();
            }


            IList<HisMedicineSampleModel> list = new List<HisMedicineSampleModel>();

            medicines.ForEach(x =>
            {
                list.Add(new HisMedicineSampleModel
                {
                    EmergencySign = (double)x.EmergencySign,
                    InvId = x.InvId.ToString(),
                    IsFirstAid = x.IsFirstAid,
                    MedicineCode = x.MedicineCode.ToString(),
                    MedicineName = x.MedicineName,
                    PharmacyCode = x.PharmacyCode,
                    PharmacyName = x.PharmacyName
                });
            });

            response.HisMedicine.AddRange(list);


            await uow.CompleteAsync();

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 获取护士站医嘱类型
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<NursingRecipeTypesResponse> GetNursingRecipeTypes(GetNursingRecipeTypesRequest request, ServerCallContext context)
    {
        List<NursingRecipeType> nursingRecipeTypes = await _nursingRecipeTypeRepository.GetListAsync();
        NursingRecipeTypesResponse response = new NursingRecipeTypesResponse();
        foreach (NursingRecipeType item in nursingRecipeTypes)
        {
            NursingRecipeTypeModel nursingRecipeTypeModel = new NursingRecipeTypeModel();
            nursingRecipeTypeModel.TypeName = item.TypeName;
            nursingRecipeTypeModel.UsageCode = item.UsageCode;
            nursingRecipeTypeModel.UsageName = item.UsageName;
            response.NursingRecipeTypes.Add(nursingRecipeTypeModel);
        }

        return response;
    }

    /// <summary>
    /// 根据药品编号，药品厂家，药品规格获取药品信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<MedicinesResponse> GetMedicines(GetMedicinesRequest request, ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            var response = new MedicinesResponse();

            var medicineCode = int.Parse(request.MedicineCode.ToString());
            var factoryCode = int.Parse(request.FactoryCode.ToString());
            var specification = request.Specification.ToString();

            //走DDP模式
            if (_ddpHospital.DdpSwitch)
            {
                var medicineMap = await GetDdpMedicinesAsync(medicineCode, factoryCode, specification);
                response.Medicines.AddRange(medicineMap);
            }
            else
            {
                //龙岗中心医院
                var medicineMap = await GetLgzxMedicinesAsync(medicineCode, factoryCode, specification);
                response.Medicines.AddRange(medicineMap);
            }

            await uow.CompleteAsync();
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 根据药品编号，药品厂家，药品规格获取药品信息(DDP)
    /// </summary>
    /// <param name="medicineCode"></param>
    /// <param name="factoryCode"></param>
    /// <param name="specification"></param>
    /// <returns></returns>
    private async Task<List<GrpcMedicineModel>> GetDdpMedicinesAsync(int medicineCode, int factoryCode, string specification)
    {
        //构造请求参数
        var request = new DdpHisMedicineByPropRequest
        {
            FactoryCode = factoryCode.ToString(),
            MedicineCode = medicineCode.ToString(),
            Specification = specification
        };

        var ddpResponse = await _ddpApiService.GetHisMedicineAsync(request);
        if (ddpResponse.Code != 200)
        {
            _log.LogError($"ddp返回异常：{ddpResponse.Code}，异常内容：{ddpResponse.Msg}");
            return new List<GrpcMedicineModel>();
        }
        var medicineMap = ObjectMapper.Map<List<DdpHisMedicineResponse>, List<GrpcMedicineModel>>(ddpResponse.Data);
        return medicineMap;
    }


    /// <summary>
    /// 根据药品编号，药品厂家，药品规格获取药品信息(龙岗中心)
    /// </summary>
    /// <param name="medicineCode"></param>
    /// <param name="factoryCode"></param>
    /// <param name="specification"></param>
    /// <returns></returns>
    private async Task<List<GrpcMedicineModel>> GetLgzxMedicinesAsync(int medicineCode, int factoryCode, string specification)
    {
        var medicines = await _hISMedicineRepository.GetMedicinesAsync(medicineCode, factoryCode, specification);
        var medicineMap = ObjectMapper.Map<List<HISMedicine>, List<GrpcMedicineModel>>(medicines);
        return medicineMap;
    }

    /// <summary>
    /// 获取药理信息 (找办法替换这个方法)
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async override Task<ToxicListResponse> GetToxicListByMedicineIds(MedicineIdsRequest request, ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            ToxicListResponse response = new ToxicListResponse();

            //走DDP模式
            if (_ddpHospital.DdpSwitch)
            {
                var medicineIds = request.MedicineIds.Select(s => s).ToList();
                var toxics = await GetDdpToxicListByMedicineIdsAsync(medicineIds);
                response.ToxicList.AddRange(toxics);
            }
            else
            {
                var medicineIds = new decimal[1]; //request.MedicineIds.Select(s => (decimal)s).ToArray();
                var toxics = await GetLgzxToxicListByMedicineIdsAsync(medicineIds);
                response.ToxicList.AddRange(toxics);
            }

            await uow.CompleteAsync();
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 获取Ddp药理信息
    /// </summary>
    /// <param name="medicineIds"></param>
    /// <returns></returns>
    private async Task<List<ToxicListModel>> GetDdpToxicListByMedicineIdsAsync(List<string> medicineIds)
    {
        //构造请求参数
        var request = new DdpHisMedicineByInvIdsRequest
        {
            InvId = medicineIds
        };

        var ddpResponse = await _ddpApiService.GetHisMedicinesByCodeAsync(request);
        if (ddpResponse.Code != 200)
        {
            _log.LogError($"ddp返回异常：{ddpResponse.Code}，异常内容：{ddpResponse.Msg}");
            return new List<ToxicListModel>();
        }

        List<ToxicListModel> toxics = ddpResponse.Data.Select(s => new ToxicListModel
        {
            AntibioticLevel = (int?)s.AntibioticLevel,
            IsAllergyTest = s.IsAllergyTest,
            IsAnaleptic = s.IsAnaleptic,
            IsCompound = s.IsCompound,
            IsDrunk = s.IsDrunk,
            IsHighRisk = s.IsHighRisk,
            IsInsulin = s.IsInsulin,
            IsLimited = s.IsLimited,
            IsPrecious = s.IsPrecious,
            IsRefrigerated = s.IsRefrigerated,
            IsSkinTest = s.IsSkinTest,
            IsTumour = s.IsTumour,
            LimitedNote = s.LimitedNote,
            MedicineId = s.Id,
            Code = s.Code,
            ToxicLevel = (int?)s.ToxicLevel
        }).ToList();
        return toxics;
    }

    /// <summary>
    /// 龙岗中心药理信息
    /// </summary>
    /// <param name="medicineIds"></param>
    /// <returns></returns>
    private async Task<List<ToxicListModel>> GetLgzxToxicListByMedicineIdsAsync(decimal[] medicineIds)
    {
        var medicines = await _hISMedicineRepository.GetHisMedicinesAsync(medicineIds);
        var toxics = medicines.Select(s => new ToxicListModel
        {
            AntibioticLevel = s.AntibioticLevel,
            IsAllergyTest = s.IsAllergyTest.HasValue ? s.IsAllergyTest.Value == 1 : null,
            IsAnaleptic = s.IsAnaleptic.HasValue ? s.IsAnaleptic.Value == 1 : null,
            IsCompound = s.IsCompound.IsNullOrEmpty() ? null : s.IsCompound.Trim() == "1",
            IsDrunk = s.IsDrunk.HasValue ? s.IsDrunk.Value == 1 : null,
            IsHighRisk = s.IsHighRisk.IsNullOrEmpty() ? null : s.IsHighRisk.Trim() == "1",
            IsInsulin = s.IsInsulin.HasValue ? s.IsInsulin.Value == 1 : null,
            IsLimited = s.IsLimited.IsNullOrEmpty() ? null : s.IsLimited.Trim() == "1",
            IsPrecious = s.IsPrecious.HasValue ? s.IsPrecious.Value == 1 : null,
            IsRefrigerated = s.IsRefrigerated.IsNullOrEmpty() ? null : s.IsRefrigerated.Trim() == "1",
            IsSkinTest = s.IsSkinTest.HasValue ? s.IsSkinTest.Value == 1 : null,
            IsTumour = s.IsTumour.IsNullOrEmpty() ? null : s.IsTumour.Trim() == "1",
            LimitedNote = s.LimitedNote.IsNullOrEmpty() ? "" : s.LimitedNote,
            MedicineId = (int)s.InvId,
            ToxicLevel = (int?)s.ToxicLevel
        }).ToList();
        return toxics;
    }

    /// <summary>
    /// 通过药品Id查询药品信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<MedicineResponse> GetMedicineById(GetMedicineByIdRquest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var medicine = await _medicineRepository.FindAsync(x => x.Id == request.Id);

            var itemRet = new GrpcMedicineModel();

            if (medicine != null)
            {
                itemRet = ObjectMapper.Map<Medicine, GrpcMedicineModel>(medicine);
                var medicineFrequencie = await _medicineFrequencieRepository.FindAsync(w => medicine.FrequencyCode == w.FrequencyCode);

                itemRet.DailyFrequency = medicineFrequencie == null ? "" : medicineFrequencie.ThirdPartyId;

                var allItem = await _allItemRepository.FindAsync(w => w.CategoryCode == "Medicine" && w.Code == medicine.Id.ToString());
                if (allItem != null)
                {
                    itemRet.ChargeCode = !allItem.ChargeCode.IsNullOrEmpty() ? allItem.ChargeCode : "";
                    itemRet.ChargeName = !allItem.ChargeName.IsNullOrEmpty() ? allItem.ChargeName : "";
                }
            }

            await uow.CompleteAsync();

            return new MedicineResponse { Medicine = itemRet };
        }
        catch (Exception exception)
        {
            _log.LogError(exception, exception.Message);
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 通过库存相关信息查询药品库存完整信息
    /// </summary>
    /// <param name="request"></param> 
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<MedicineResponse> GetMedicineInvInfo(GetMedicineInvInfoRquest request, ServerCallContext context)
    {
        HISMedicine hisMedicine = null;
        if (_ddpHospital.DdpSwitch)
        {
            //调用DDP
            hisMedicine = await GetFirstDdpMedicineAsync(request.Code, request.Specification, request.FactoryCode, request.PharmacyCode);
        }
        else
        {
            decimal medicineCode = decimal.Parse(request.Code);
            decimal factoryCode = decimal.Parse(request.FactoryCode);
            //调用视图
            hisMedicine = await _hISMedicineRepository.FindAsync(x => x.MedicineCode == medicineCode && x.FactoryCode == factoryCode && x.PharmacyCode == request.PharmacyCode);

        }

        Medicine medicine = ObjectMapper.Map<HISMedicine, Medicine>(hisMedicine);
        GrpcMedicineModel itemRet = ObjectMapper.Map<Medicine, GrpcMedicineModel>(medicine);
        if (itemRet != null && hisMedicine != null)
        {
            itemRet.YBInneCode = hisMedicine.YBInneCode;
        }


        if (medicine != null)
        {
            var medicineFrequencie = await _medicineFrequencieRepository.FindAsync(w => w.FrequencyCode == medicine.FrequencyCode);

            itemRet.DailyFrequency = medicineFrequencie == null ? "" : medicineFrequencie.ThirdPartyId;
        }
        return new MedicineResponse { Medicine = itemRet };
    }

    /// <summary>
    ///  获取药品用法/途径
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<MedicineUsageResponse> GetMedicineUsageByCode(GetMedicineUsageByCodeRequest request, ServerCallContext context)
    {
        var usage = await _medicineUsageRepository.FindAsync(x => x.UsageCode == request.Code);
        if (usage == null)
        {
            return new MedicineUsageResponse();
        }
        var grpcUsage = ObjectMapper.Map<MedicineUsage, MedicineUsageModel>(usage);
        return new MedicineUsageResponse { Usage = grpcUsage };

    }

    /// <summary>
    /// 根据药品编码，规格，药厂，药房，获取唯一药品信息
    /// </summary>
    /// <param name="code"></param>
    /// <param name="specification"></param>
    /// <param name="factoryCode"></param>
    /// <param name="pharmacyCode"></param>
    /// <returns></returns>
    private async Task<HISMedicine> GetFirstDdpMedicineAsync(string code, string specification, string factoryCode, string pharmacyCode)
    {
        //构造请求参数
        var request = new DdpHisMedicineByPropRequest
        {
            FactoryCode = factoryCode,
            MedicineCode = code,
            Specification = specification
        };

        var ddpResponse = await _ddpApiService.GetHisMedicineAsync(request);
        if (ddpResponse.Code != 200)
        {
            _log.LogError($"ddp返回异常：{ddpResponse.Code}，异常内容：{ddpResponse.Msg}");
            return new HISMedicine();
        }

        var data = ddpResponse.Data.FirstOrDefault(w => w.PharmacyCode == pharmacyCode);

        var medicineMap = ObjectMapper.Map<DdpHisMedicineResponse, HISMedicine>(data);
        return medicineMap;
    }

    /// <summary>
    /// 获取费别信息集合
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<ChargeInfoResponse> GetChargeInfos(GetChargeInfoRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var codes = request.Codes.Select(s => s).ToList();
            var list = await _allItemRepository.GetListAsync(w => codes.Contains(w.Code));
            var chargeInfoList = new List<ChargeInfoModel>();
            list.ForEach(c => chargeInfoList.Add(new ChargeInfoModel
            {
                Code = c.Code,
                ChargeCode = c.ChargeCode.IsNullOrEmpty() ? "" : c.ChargeCode, //grpc 的工具类检查null ，所以需要判断，否则引起异常
                ChargeName = c.ChargeName.IsNullOrEmpty() ? "" : c.ChargeName,
            }));
            //var list = await  _allItemRepository.GetQueryableAsync())
            //    .Where(w => codes.Contains(w.Code))
            //    .Select(s => new ChargeInfoModel
            //    {
            //        Code = s.Code,
            //        ChargeCode = s.ChargeCode.IsNullOrEmpty() ? "" : s.ChargeCode, //grpc 的工具类检查null ，所以需要判断，否则引起异常
            //        ChargeName = s.ChargeName.IsNullOrEmpty() ? "" : s.ChargeName,
            //    })
            //    .ToListAsync();
            ChargeInfoResponse data = new ChargeInfoResponse();
            data.ChargeInfos.AddRange(chargeInfoList);
            await uow.CompleteAsync();
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 获取检验项目
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<LabProjectsResponse> GetAllLabProjects(GetAllLabProjectsRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var labProjects = await _labProjectRepository.GetListAsync();
            var response = new LabProjectsResponse();
            foreach (var labProject in labProjects)
            {
                var responseLabProject = ObjectMapper.Map<LabProject, GrpcLabProjectModel>(labProject);
                response.LabProjects.Add(responseLabProject);
            }

            await uow.CompleteAsync();

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 通过编码查询单个检验项目
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<LabProjectResponse> GetLabProjectById(GetLabProjectByIdRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var labProject = await _labProjectRepository.FindAsync(x => x.Id == request.Id);
            var itemRet = new GrpcLabProjectModel();

            if (labProject != null)
            {
                itemRet = ObjectMapper.Map<LabProject, GrpcLabProjectModel>(labProject);
                var allItem = await _allItemRepository.FindAsync(w => w.Code == labProject.Id.ToString());
                if (allItem != null)
                {
                    itemRet.ChargeCode = !allItem.ChargeCode.IsNullOrEmpty() ? allItem.ChargeCode : "";
                    itemRet.ChargeName = !allItem.ChargeName.IsNullOrEmpty() ? allItem.ChargeName : "";
                }
            }

            await uow.CompleteAsync();

            return new LabProjectResponse { LabProject = itemRet };
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 通过编码查询单个检验项目
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<LabProjectResponse> GetLabProjectByCode(GetLabProjectByCodeRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var labProject = await _labProjectRepository.FindAsync(x => x.ProjectCode == request.Code);

            var itemRet = ObjectMapper.Map<LabProject, GrpcLabProjectModel>(labProject);

            if (labProject != null)
            {
                var note = await _examNoteRepository.FindAsync(x => x.NoteCode == itemRet.GuideCode);
                if (note != null)
                {
                    itemRet.GuideName = note.NoteName;
                }
                var allItem = await _allItemRepository.FindAsync(w => w.Code == labProject.Id.ToString());
                if (allItem != null)
                {
                    itemRet.ChargeCode = !allItem.ChargeCode.IsNullOrEmpty() ? allItem.ChargeCode : "";
                    itemRet.ChargeName = !allItem.ChargeName.IsNullOrEmpty() ? allItem.ChargeName : "";
                }
            }

            await uow.CompleteAsync();

            return new LabProjectResponse { LabProject = itemRet };
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 获取检查项目
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<ExamProjectsResponse> GetAllExamProjects(GetAllExamProjectsRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var examProjects = await _examProjectRepository.GetListAsync(c => c.IsDeleted == false);
            var response = new ExamProjectsResponse();
            foreach (var examProject in examProjects)
            {
                var responseLabProject = ObjectMapper.Map<ExamProject, GrpcExamProjectModel>(examProject);
                ExamExecDeptConfig examExecDeptConfig = _examExecDeptConfigs.FirstOrDefault(x => x.ProjectCodes.Contains(responseLabProject.ProjectCode));
                if (examExecDeptConfig != null)
                {
                    responseLabProject.ExecDeptCode = examExecDeptConfig.ExecDeptCode;
                    responseLabProject.ExecDeptName = examExecDeptConfig.ExecDeptName;
                }
                response.ExamProjects.Add(responseLabProject);
            }

            await uow.CompleteAsync();

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 通过编码查询单个检查项目
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<ExamProjectResponse> GetExamProjectByCode(GetExamProjectByCodeRequest request, ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            var examProject = (await _examProjectRepository.GetListAsync(x => x.ProjectCode == request.Code))?.FirstOrDefault();

            var itemRet = ObjectMapper.Map<ExamProject, GrpcExamProjectModel>(examProject);

            if (examProject != null)
            {
                var examTargets = await _examTargetRepository.GetListAsync(x => x.ProjectCode == examProject.ProjectCode && x.IsActive == true);
                var targetCodes = examTargets.Select(x => x.TargetCode);
                var treats = await _treatRepository.GetListAsync(x => targetCodes.Contains(x.TreatCode));
                var TreatCodes = treats.Select(x => x.TreatCode);
                examTargets = examTargets.Where(x => TreatCodes.Contains(x.TargetCode)).ToList();
                itemRet.Price = (double)examTargets.Sum(x => x.Price * x.Qty);
                ExamExecDeptConfig examExecDeptConfig = _examExecDeptConfigs.FirstOrDefault(x => x.ProjectCodes.Contains(itemRet.ProjectCode));
                if (examExecDeptConfig != null)
                {
                    itemRet.ExecDeptCode = examExecDeptConfig.ExecDeptCode;
                    itemRet.ExecDeptName = examExecDeptConfig.ExecDeptName;
                }

                //查询检查的一级目录
                var examCategory = await _examCatalogRepository.FindAsync(x => x.CatalogCode == examProject.CatalogCode);
                if (examCategory != null)
                {
                    itemRet.FirstCatalogCode = examCategory.FirstNodeCode;
                    itemRet.FirstCatalogName = examCategory.FirstNodeName;
                }

                var allItem =
                    await _allItemRepository.FindAsync(w => w.Code == examProject.Id.ToString());
                if (allItem != null)
                {
                    itemRet.ChargeCode = !allItem.ChargeCode.IsNullOrEmpty() ? allItem.ChargeCode : "";
                    itemRet.ChargeName = !allItem.ChargeName.IsNullOrEmpty() ? allItem.ChargeName : "";
                }
                var note = await _examNoteRepository.FindAsync(x => x.NoteCode == itemRet.GuideCode);
                if (note != null)
                {
                    itemRet.GuideName = note?.NoteName;
                }
            }

            await uow.CompleteAsync();

            return new ExamProjectResponse { ExamProject = itemRet };
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 通过编码查询检查信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<ExamProjectsResponse> GetExamsByCodes(ExamCodeRequest request, ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            List<string> codes = request.Code.Select(s => s).ToList();
            List<ExamProject> examProjects = await _examProjectRepository.GetListAsync(x => codes.Contains(x.ProjectCode));
            IEnumerable<string> guideCodes = examProjects.Select(x => x.GuideCode);
            List<ExamNote> examNotes = await _examNoteRepository.GetListAsync(x => guideCodes.Contains(x.NoteCode));
            ExamProjectsResponse response = new ExamProjectsResponse();
            foreach (ExamProject examProject in examProjects)
            {
                GrpcExamProjectModel examProjectModel = ObjectMapper.Map<ExamProject, GrpcExamProjectModel>(examProject);
                ExamExecDeptConfig examExecDeptConfig = _examExecDeptConfigs.FirstOrDefault(x => x.ProjectCodes.Contains(examProjectModel.ProjectCode));
                if (examExecDeptConfig != null)
                {
                    examProjectModel.ExecDeptCode = examExecDeptConfig.ExecDeptCode;
                    examProjectModel.ExecDeptName = examExecDeptConfig.ExecDeptName;
                }

                ExamNote examNote = examNotes.FirstOrDefault(x => x.NoteCode == examProject.GuideCode);
                if (examNote != null)
                {
                    examProjectModel.GuideName = examNote.NoteName;
                }
                response.ExamProjects.Add(examProjectModel);
            }

            await uow.CompleteAsync();

            return response;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 获取诊疗项目
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<TreatProjectsResponse> GetAllTreatProjects(GetAllTreatProjectsRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var treatProjects = await _treatRepository.GetListAsync(c => true);
            var response = new TreatProjectsResponse();
            foreach (var treatProject in treatProjects)
            {
                var responseLabProject = ObjectMapper.Map<Treat, GrpcTreatProjectModel>(treatProject);
                response.TreatProjects.Add(responseLabProject);
            }

            await uow.CompleteAsync();

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 通过ID获取单个诊疗项目
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<TreatProjectResponse> GetTreatProjectById(GetTreatProjectByIdRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var treatProject = await _treatRepository.FindAsync(x => x.Id == request.Id);

            var itemRet = ObjectMapper.Map<Treat, GrpcTreatProjectModel>(treatProject);
            if (treatProject != null)
            {
                var allItem = await _allItemRepository.FindAsync(w => w.CategoryCode == treatProject.CategoryCode && w.Code == treatProject.Id.ToString());
                if (allItem != null)
                {
                    itemRet.ChargeCode = !allItem.ChargeCode.IsNullOrEmpty() ? allItem.ChargeCode : "";
                    itemRet.ChargeName = !allItem.ChargeName.IsNullOrEmpty() ? allItem.ChargeName : "";
                }
            }

            await uow.CompleteAsync();

            return new TreatProjectResponse { TreatProject = itemRet };
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 通过代码获取单个诊疗项目
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<TreatProjectResponse> GetTreatProjectByCode(GetTreatProjectByCodeRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();


        try
        {
            var treatProject = await _treatRepository.FindAsync(x => x.TreatCode == request.Code);

            var itemRet = ObjectMapper.Map<Treat, GrpcTreatProjectModel>(treatProject);
            if (treatProject != null)
            {
                var allItem = await _allItemRepository.FindAsync(w =>
                    w.CategoryCode == treatProject.CategoryCode && w.Code == treatProject.Id.ToString());
                if (allItem != null)
                {
                    itemRet.ChargeCode = !allItem.ChargeCode.IsNullOrEmpty() ? allItem.ChargeCode : "";
                    itemRet.ChargeName = !allItem.ChargeName.IsNullOrEmpty() ? allItem.ChargeName : "";
                }

                var group = await _treatGroupRepository.FindAsync(x =>
                    x.CatalogCode == itemRet.CategoryCode);
                if (group != null)
                {
                    itemRet.DictionaryCode = group.DictionaryCode;
                    itemRet.DictionaryName = group.DictionaryName;
                }
            }
            await uow.CompleteAsync();

            return new TreatProjectResponse { TreatProject = itemRet };
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 获取全部频次信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<FrequenciesResponse> GetAllFrequencies(GetAllFrequenciesRequest request, ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var items = await _medicineFrequencieRepository.GetListAsync(c => c.IsActive);
            var response = new FrequenciesResponse();
            foreach (var item in items)
            {
                var frequency = ObjectMapper.Map<MedicineFrequency, GrpcFrequencyModel>(item);
                response.Frequencies.Add(frequency);
            }

            await uow.CompleteAsync();

            return response;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 获取频次信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<FrequencyResponse> GetFrequencyByCode(GetFrequencyByCodeRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var item = await _medicineFrequencieRepository.FindAsync(x => x.FrequencyCode == request.Code);

            await uow.CompleteAsync();

            var itemRet = ObjectMapper.Map<MedicineFrequency, GrpcFrequencyModel>(item);

            return new FrequencyResponse { Frequency = itemRet };
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 获取指定的药品，检查，检验 等的扩展属性信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<AdviceExtendedAttributesResponse> GetAdviceExtendedAttributes(
        AdviceExtendedAttributesRequest request, ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            AdviceExtendedAttributesResponse data = null;
            //药品
            if (request.ItemType == 0)
            {
                var medicine = await _medicineRepository.FindAsync(w => w.MedicineCode == request.Code.Trim());

                data = new AdviceExtendedAttributesResponse
                {
                    Alias = medicine.Alias,
                    AliasPyCode = medicine.AliasPyCode,
                    AliasWbCode = medicine.AliasWbCode,
                    Code = medicine.MedicineCode,
                    Name = medicine.MedicineName,
                    PyCode = medicine.PyCode,
                    ScientificName = medicine.ScientificName,
                    WbCode = medicine.WbCode
                };
            }

            //检查
            if (request.ItemType == 1)
            {
                var exam = await _examProjectRepository.FindAsync(x => x.ProjectCode == request.Code);

                data = new AdviceExtendedAttributesResponse
                {
                    Alias = "",
                    AliasPyCode = "",
                    AliasWbCode = "",
                    ScientificName = "",
                    Code = exam.ProjectCode,
                    Name = exam.ProjectName,
                    PyCode = exam.PyCode,
                    WbCode = exam.WbCode
                };
            }

            //检验
            if (request.ItemType == 2)
            {
                var lab = await _labProjectRepository.FindAsync(x => x.ProjectCode == request.Code);

                data = new AdviceExtendedAttributesResponse
                {
                    Alias = "",
                    AliasPyCode = "",
                    AliasWbCode = "",
                    ScientificName = "",
                    Code = lab.ProjectCode,
                    Name = lab.ProjectName,
                    PyCode = lab.PyCode,
                    WbCode = lab.WbCode
                };
            }

            //诊疗
            if (request.ItemType == 3)
            {
                var treat = await _treatRepository.FindAsync(x => x.TreatCode == request.Code);

                data = new AdviceExtendedAttributesResponse
                {
                    Alias = "",
                    AliasPyCode = "",
                    AliasWbCode = "",
                    ScientificName = "",
                    Code = treat.TreatCode,
                    Name = treat.TreatName,
                    PyCode = treat.PyCode,
                    WbCode = treat.WbCode
                };
            }

            await uow.CompleteAsync();
            return data;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 通过分方配置的Id获取用药途径集合
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<SeparationResponse> GetSeparationById(SeparationRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            var id = Guid.Parse(request.SeparationId);
            var entity = await _separationRepository.GetAsync(id);
            await uow.CompleteAsync();
            var data = new SeparationResponse()
            {
                Id = entity.Id.ToString(),
                Title = entity.Title,
                Code = entity.Code,
                PrintSettingId = entity.PrintSettingId.ToString(),
            };
            var usageList = entity.Usages.Select(s => new UsagesModel
            { Id = s.Id.ToString(), UsageCode = s.UsageCode, UsageName = s.UsageName }).ToList();
            data.Usages.AddRange(usageList);
            return data;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 获取分方配置列表
    /// </summary>
    /// <param name="getAllSeparationRequest"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<AllSeparationResponse> GetSeparationsList(GetAllSeparationRequest getAllSeparationRequest, ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            AllSeparationResponse allSeparationResponse = new AllSeparationResponse();
            List<Separation> separations = await _separationRepository.GetListAsync();
            await uow.CompleteAsync();
            foreach (Separation separation in separations)
            {
                SeparationResponse separationResponse = new SeparationResponse()
                {
                    Id = separation.Id.ToString(),
                    Title = separation.Title,
                    Code = separation.Code,
                    PrintSettingId = separation.PrintSettingId.ToString(),
                };
                List<UsagesModel> usageList = separation.Usages.Select(s =>
                new UsagesModel
                {
                    Id = s.Id.ToString(),
                    UsageCode = s.UsageCode,
                    UsageName = s.UsageName
                }).ToList();
                separationResponse.Usages.AddRange(usageList);
                allSeparationResponse.Separations.Add(separationResponse);
            }

            return allSeparationResponse;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 获取所有分方途径和打印模板的映射关系
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async override Task<SeparationListResponse> GetSeparations(SeparationListRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            SeparationListResponse res = new SeparationListResponse();
            var list = await (await _separationRepository.GetQueryableAsync())
                .Where(w => w.PrintSettingId.HasValue).Select(s =>
                new SeparationListModel
                {
                    Id = s.Id.ToString(),
                    Code = s.Code,
                    Title = s.Title,
                    PrintSettingId = s.PrintSettingId.ToString()
                }).ToListAsync();
            foreach (var l in list)
            {
                var usages = await (await _usageRepository.GetQueryableAsync()).Where(x => x.SeparationId == Guid.Parse(l.Id)).ToListAsync();
                l.UsageCode = string.Join(",", usages.Select(s => s.UsageCode).ToList());
            }
            if (list.Count > 0) res.Separation.AddRange(list);
            await uow.CompleteAsync();
            return res;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    public override async Task<GetSeparationsByCodeReponse> GetSeparationsByCodes(
        GetSeparationsByCodeRequest request, ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            var response = new GetSeparationsByCodeReponse();
            var usagecodes = request.UsageCode.Select(s => s).ToList();
            var query = from u in (await _usageRepository.GetQueryableAsync())
                        join s in (await _separationRepository.GetQueryableAsync())
                            on u.SeparationId equals s.Id
                        where usagecodes.Contains(u.UsageCode)
                        select new
                        {
                            Id = s.Id,
                            PrintSettingId = s.PrintSettingId,
                            Title = s.Title,
                            Code = s.Code,
                            UsageCode = u.UsageCode
                        };

            var group = (await query.ToListAsync()).GroupBy(g => g.Title).ToList();
            foreach (var item in group)
            {
                var model = item.FirstOrDefault();
                response.Model.Add(
                    new GetSeparationsByCodeModel
                    {
                        SeparationId = model.Id.ToString(),
                        Code = model.Code,
                        Title = model.Title,
                        PrintSettingId = model.PrintSettingId.ToString(),
                        UsageCode = model.UsageCode
                    });
            }

            await uow.CompleteAsync();
            return response;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 查询标本
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<LabSpecimenResponse> GetSpecimenById(GetLabSpecimenByIdRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var item = await _labSpecimenRepository.FindAsync(x => x.Id == request.Id);
            var itemRet = ObjectMapper.Map<LabSpecimen, GrpcLabSpecimenModel>(item);

            await uow.CompleteAsync();

            return new LabSpecimenResponse() { Specimen = itemRet };
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 查询标本
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<LabSpecimenResponse> GetSpecimenByCode(GetLabSpecimenByCodeRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var item = await _labSpecimenRepository.FindAsync(x => x.SpecimenCode == request.Code);
            var itemRet = ObjectMapper.Map<LabSpecimen, GrpcLabSpecimenModel>(item);

            await uow.CompleteAsync();

            return new LabSpecimenResponse() { Specimen = itemRet };
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 查询标本采集部位
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<LabSpecimenPositionResponse> GetSpecimenPositionByCode(
        GetLabSpecimenPositionByCodeRequest request, ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var item = await _labSpecimenPositionRepository.FindAsync(x => x.SpecimenCode == request.Code);
            var itemRet = ObjectMapper.Map<LabSpecimenPosition, GrpcLabSpecimenPositionModel>(item);

            await uow.CompleteAsync();

            return new LabSpecimenPositionResponse() { Position = itemRet };
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 查询检查部位
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<ExamPartResponse> GetExamPartByCode(GetExamPartByCodeRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var item = await _examPartRepository.FindAsync(x => x.PartCode == request.Code);
            var itemRet = ObjectMapper.Map<ExamPart, GrpcExamPartModel>(item);

            await uow.CompleteAsync();

            return new ExamPartResponse() { Data = itemRet };
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 获取科室列表
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<DepartmentsResponse> GetDepartments(GetDepartmentsRequest request,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();

        try
        {
            var list = await _departmentRepository.GetListAsync(x => x.IsActived);
            var departments = ObjectMapper.Map<List<Department>, List<DepartmentModel>>(list);
            var result = new DepartmentsResponse();
            result.Items.AddRange(departments);
            await uow.CompleteAsync();

            return result;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 获取诊室列表
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<ConsultingRoomsResponse> GetConsultingRooms(GetConsultingRoomsRequest request,
        ServerCallContext context)
    {
        using var uow = this.UnitOfWorkManager.Begin();

        try
        {
            ConsultingRoomsResponse response = new ConsultingRoomsResponse();
            var list = await _consultingRoomRepository.GetListAsync(x => x.IsActived);
            var consultingRoomModels = ObjectMapper.Map<List<ConsultingRoom>, List<ConsultingRoomModel>>(list);
            response.Items.AddRange(consultingRoomModels);
            await uow.CompleteAsync();

            return response;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 根据检验code获取检验明细项信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<LabTargetsReponse> GetLabTargetsByCode(LabCodeRequest request,
        ServerCallContext context)
    {
        using var uow = this.UnitOfWorkManager.Begin();
        try
        {
            var codes = request.Code.Select(s => s).ToList();
            var response = new LabTargetsReponse();
            var list = await _labTargetRepository.GetListAsync(w => codes.Contains(w.ProjectCode));
            var map = ObjectMapper.Map<List<LabTarget>, List<LabTargetsModel>>(list);
            response.Targets.AddRange(map);
            await uow.CompleteAsync();
            return response;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 根据检查code获取检查明细信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<ExamTargetsReponse> GetExamTargetsByCode(ExamCodeRequest request,
        ServerCallContext context)
    {
        using var uow = this.UnitOfWorkManager.Begin();
        try
        {
            var codes = request.Code.Select(s => s).ToList();
            var response = new ExamTargetsReponse();
            var list = await _examTargetRepository.GetListAsync(w => codes.Contains(w.ProjectCode));
            var map = ObjectMapper.Map<List<ExamTarget>, List<ExamTargetsModel>>(list);
            response.Targets.AddRange(map);
            await uow.CompleteAsync();
            return response;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }


    /// <summary>
    /// 根据用法查询处置
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<GrpcTreatProjectModel> GetTreatByUsage(GetTreatByUsageCodeRequest input,
        ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            var query = await (from t in (await _treatRepository.GetQueryableAsync())
                               join m in (await _medicineUsageRepository.GetQueryableAsync()).Where(w => w.UsageCode == input.UsageCode)
                                   on t.TreatCode equals m.TreatCode
                               join g in (await _treatGroupRepository.GetQueryableAsync())
                                   on t.CategoryCode equals g.CatalogCode
                               where t.TreatCode != ""
                               select new TreatProjectModelDto
                               {
                                   Id = t.Id,
                                   CategoryCode = t.CategoryCode,
                                   CategoryName = t.CategoryName,
                                   ExecDeptCode = t.ExecDeptCode,
                                   ExecDeptName = t.ExecDeptName,
                                   FeeTypeMainCode = t.FeeTypeMainCode,
                                   FeeTypeSubCode = t.FeeTypeSubCode,
                                   FrequencyCode = t.FrequencyCode,
                                   ProjectMerge = t.ProjectMerge,
                                   PyCode = t.PyCode,
                                   Specification = t.Specification,
                                   TreatCode = t.TreatCode,
                                   TreatName = t.TreatName,
                                   Unit = t.Unit,
                                   WbCode = t.WbCode,
                                   Price = t.Price,
                                   OtherPrice = t.OtherPrice,
                                   Additional = t.Additional,
                                   PlatformType = t.PlatformType,
                                   ChargeCode = "",
                                   ChargeName = "",
                                   DictionaryCode = g == null ? "" : g.DictionaryCode,
                                   DictionaryName = g == null ? "" : g.DictionaryName,
                                   MeducalInsuranceCode = t.MeducalInsuranceCode ?? "",
                                   YBInneCode = t.YBInneCode ?? ""
                               }).FirstOrDefaultAsync();

            if (query == null) return new GrpcTreatProjectModel();
            var map = ObjectMapper.Map<TreatProjectModelDto, GrpcTreatProjectModel>(query);


            await uow.CompleteAsync();
            //return map;

            return map;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            await uow.RollbackAsync();
            throw;
        }
    }


    /// <summary>
    /// 医嘱套餐查询明细项目信息 todo 待优化循环嵌套查询
    /// </summary>
    /// <param name="combinationRequest"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<CombinationResponse> GetCombinationRecipeDetails(CombinationRequest combinationRequest, ServerCallContext context)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            CombinationResponse combinationResponse = new CombinationResponse();
            if (combinationRequest != null)
            {
                if (combinationRequest.LabInfo != null && combinationRequest.LabInfo.Any())
                {
                    foreach (var item in combinationRequest.LabInfo)
                    {
                        var labInfo = await (await _labProjectRepository.GetQueryableAsync()).Where(x => item.ProjectCode == x.ProjectCode && item.CatalogCode == x.CatalogCode).Select(x => new CombinationRecipe { Code = x.ProjectCode + x.CatalogCode, Category = "Lab", IsActive = x.IsActive }).ToListAsync();

                        combinationResponse.Details.AddRange(labInfo);
                    }
                }

                if (combinationRequest.ExamProjectCode != null && combinationRequest.ExamProjectCode.Any())
                {
                    var examInfo = await (await _examProjectRepository.GetQueryableAsync()).Where(x => combinationRequest.ExamProjectCode.Contains(x.ProjectCode)).Select(x => new CombinationRecipe { Code = x.ProjectCode, Category = "Exam", IsActive = x.IsActive }).ToListAsync();

                    combinationResponse.Details.AddRange(examInfo);
                }

                if (combinationRequest.TreatCode != null && combinationRequest.TreatCode.Any())
                {
                    var treatInfo = await (await _treatRepository.GetQueryableAsync()).Where(x => combinationRequest.TreatCode.Contains(x.TreatCode)).Select(x => new CombinationRecipe { Code = x.TreatCode, Category = "Treat", IsActive = true }).ToListAsync();

                    combinationResponse.Details.AddRange(treatInfo);
                }

                if (combinationRequest.MedicineInfo != null && combinationRequest.MedicineInfo.Any())
                {
                    List<HISMedicine> medicineList = new List<HISMedicine>();
                    if (_ddpHospital.DdpSwitch)
                    {
                        //DDP接口模式
                        medicineList = await GetHISMedicinesByDdpApiAsync();
                    }
                    else
                    {
                        //龙岗的视图模式
                        medicineList = await (await _hISMedicineRepository.GetQueryableAsync()).ToListAsync();
                    }

                    foreach (var item in combinationRequest.MedicineInfo)
                    {
                        decimal medicineCode = decimal.Parse(item.Code);
                        decimal factoryCode = decimal.Parse(item.FactoryCode);
                        HISMedicine hisMedicine = medicineList.FirstOrDefault(x => x.MedicineCode == medicineCode && x.FactoryCode == factoryCode && x.PharmacyCode == item.PharmacyCode && x.Specification == item.Specification);

                        if (hisMedicine != null)
                        {
                            CombinationRecipe combinationRecipe = new CombinationRecipe();
                            combinationRecipe.MedicineCode = hisMedicine.MedicineCode.ToString();
                            combinationRecipe.FactoryCode = hisMedicine.FactoryCode.ToString();
                            combinationRecipe.PharmacyCode = hisMedicine.PharmacyCode;
                            combinationRecipe.Specification = hisMedicine.Specification;
                            combinationRecipe.Category = "Medicine";
                            combinationRecipe.IsActive = hisMedicine.IsActive == 1;

                            combinationResponse.Details.Add(combinationRecipe);
                        }
                    }
                }

            }

            await uow.CompleteAsync();
            return combinationResponse;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 通过Ddp获取所有库存药品的集合
    /// </summary>
    /// <returns></returns>
    private async Task<List<HISMedicine>> GetHISMedicinesByDdpApiAsync()
    {
        try
        {
            var data = _cache.Get<List<HISMedicine>>(DdpDCModel.DDP_HIS_MEDICINES_KEY);
            if (data != null) return data;

            //调用DDP模式
            var ddpResponse = await _ddpApiService.GetHisMedicineAllAsync();

            if (ddpResponse.Code != 200)
            {
                //异常缓存可能会穿透
                _log.LogInformation($"Ddp返回的库存药品清单异常:{ddpResponse.Code},Msg={ddpResponse.Msg}");
                return new List<HISMedicine>();
            }
            var retData = ObjectMapper.Map<List<DdpHisMedicineResponse>, List<HISMedicine>>(ddpResponse.Data);
            _cache.Set<List<HISMedicine>>(DdpDCModel.DDP_HIS_MEDICINES_KEY, retData, TimeSpan.FromHours(1)); //暂时缓存一个小时
            return retData;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, $"获取ddp数据异常：{ex.Message}");
            throw;
        }
    }


    /// <summary>
    /// 根据treatcode 获取 诊疗信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<GetTreatByCodeResponse> GetTreatByCode(GetTreatByCodeRequest request, ServerCallContext context)
    {
        using var uow = this.UnitOfWorkManager.Begin();
        try
        {
            Treat treat = await _treatRepository.FindAsync(w => w.TreatCode == request.Code);
            if (treat == null)
            {
                return new GetTreatByCodeResponse();
            }

            GetTreatByCodeResponse response = new GetTreatByCodeResponse
            {
                Additional = treat.Additional,
                CategoryCode = treat.CategoryCode,
                CategoryName = treat.CategoryName,
                Code = treat.TreatCode,
                Name = treat.TreatName,
                FeeTypeMainCode = treat.FeeTypeMainCode ?? "",
                FeeTypeSubCode = treat.FeeTypeSubCode ?? "",
                FrequencyCode = treat.FrequencyCode ?? "",
                Id = treat.Id,
                OtherPrice = treat.OtherPrice.HasValue ? (double)treat.OtherPrice.Value : 0,
                PlatformType = (int)treat.PlatformType,
                Price = (double)treat.Price,
                ProjectMerge = treat.ProjectMerge,
                ProjectType = treat.CategoryCode,
                ProjectTypeName = treat.CategoryName,
                PyCode = treat.PyCode,
                RetPrice = (double)treat.Price,
                Specification = treat.Specification ?? "",
                Unit = treat.Unit ?? "",
                WbCode = treat.WbCode ?? "",
                DictionaryCode = "Treat",
                DictionaryName = "处置",
                MeducalInsuranceCode = treat.MeducalInsuranceCode ?? "",
                YBInneCode = treat.YBInneCode ?? ""
            };
            await uow.CompleteAsync();
            return response;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }
    /// <summary>
    /// 获取多类型字典
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<GetDictionariesMultiTypeResponse> GetDictionariesMultiType(
        GetDictionariesMultiTypeRequest request, ServerCallContext context)
    {
        using var uow = this.UnitOfWorkManager.Begin();
        try
        {
            var dictionaries = await _dictionariesMultitypeRepository.FindAsync(x =>
                x.Status && x.Code == request.Code && x.GroupCode == request.GroupCode);
            var response = ObjectMapper.Map<DictionariesMultitype, GetDictionariesMultiTypeResponse>(dictionaries);
            await uow.CompleteAsync();
            return response;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 获取字典
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<GetDictionariesResponse> GetDictionaries(
        GetDictionariesRequest request, ServerCallContext context)
    {
        using var uow = this.UnitOfWorkManager.Begin();
        try
        {
            var dictionaries = await _dictionariesRepository.FindAsync(x =>
            x.DictionariesTypeCode == request.DictionariesTypeCode && x.DictionariesCode == request.DictionariesCode);
            var response = ObjectMapper.Map<YiJian.MasterData.Dictionaries, GetDictionariesResponse>(dictionaries);
            await uow.CompleteAsync();
            return response;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 获取检查附加项
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<ExamAttachItemReponse> GetExamAttachItems(ExamCodeRequest request, ServerCallContext context)
    {
        List<string> codes = request.Code.Select(s => s).ToList();
        List<ExamAttachItem> examAttachItems = await _examAttachItemsRepository.GetListAsync(x => codes.Contains(x.ProjectCode));
        ExamAttachItemReponse examAttachItemReponse = new ExamAttachItemReponse();
        foreach (ExamAttachItem examAttachItem in examAttachItems)
        {
            ExamAttachItemModel examAttachItemModel = new ExamAttachItemModel();
            examAttachItemModel.Id = examAttachItem.Id.ToString();
            examAttachItemModel.ProjectCode = examAttachItem.ProjectCode;
            examAttachItemModel.MedicineCode = examAttachItem.MedicineCode;
            examAttachItemModel.Qty = (double)examAttachItem.Qty;
            examAttachItemModel.Type = examAttachItem.Type;
            examAttachItemReponse.ExamAttachItems.Add(examAttachItemModel);
        }

        return examAttachItemReponse;
    }

    /// <summary>
    /// 获取检验单项目信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<LabReportInfos> GetLabReportInfo(GetLabReportInfoCodes request, ServerCallContext context)
    {
        LabReportInfos labReportInfoModels = new LabReportInfos();
        List<int> codes = new List<int>();
        foreach (var item in request.Code)
        {
            if (int.TryParse(item, out int code))
            {
                codes.Add(code);
            }
        }

        List<LabReportInfo> labReportInfos = await _labReportInfoRepository.GetListAsync(x => codes.Contains(x.Code));
        foreach (var item in labReportInfos)
        {
            LabReportInfoModel labReportInfoModel = new LabReportInfoModel();
            labReportInfoModel.Code = item.Code.ToString();
            labReportInfoModel.Remark = item.Remark ?? string.Empty;
            labReportInfoModel.CatelogName = item.CatelogName ?? string.Empty;
            labReportInfoModel.ExecDeptName = item.ExecDeptName ?? string.Empty;
            labReportInfoModels.LabReportInfolist.Add(labReportInfoModel);
        }

        return labReportInfoModels;
    }
}