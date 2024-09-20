using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NUglify.Helpers;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.MasterData.Departments;
using YiJian.MasterData.Domain;
using YiJian.MasterData.Exams;
using YiJian.MasterData.External.LongGang.ExamAndLab;
using YiJian.MasterData.External.LongGang.Lab;
using YiJian.MasterData.External.LongGang.Medicines;
using YiJian.MasterData.External.PKU;
using YiJian.MasterData.Labs;
using YiJian.MasterData.Labs.Container;
using YiJian.MasterData.MasterData.Doctors;
using YiJian.MasterData.MasterData.HospitalClient;
using YiJian.MasterData.MasterData.HospitalClient.Doctors;
using YiJian.MasterData.Medicines;
using YiJian.MasterData.Pharmacies.Contracts;
using YiJian.MasterData.Treats;
using FrequencyEto = YiJian.MasterData.External.LongGang.Frequency.FrequencyEto;
using TreatsEto = YiJian.MasterData.External.LongGang.Teat.TreatsEto;
using UsagesEto = YiJian.MasterData.External.LongGang.Usage.UsagesEto;

namespace YiJian.MasterData.MasterData;

/// <summary>
/// 龙岗中心医院订阅处理类
/// </summary>
public class LongGangHospitalClientHandler : MasterDataAppService, IDistributedEventHandler<FullHandlerEto>,
    ITransientDependency
{
    private readonly IMedicineRepository _medicineRepository;
    private readonly ITreatRepository _treatRepository;
    private readonly ILabSpecimenRepository _labSpecimenRepository;
    private readonly IPharmacyRepository _pharmacyRepository;
    private readonly IMedicineUsageRepository _medicineUsageRepository;
    private readonly IMedicineFrequencyRepository _medicineFrequencyRepository;
    private readonly IExamNoteRepository _examNoteRepository;
    private readonly IDoctorRepository _doctorRepository;
    private ICapPublisher _capPublisher;
    private readonly IExamCatalogRepository _examCatalogRepository;
    private readonly IExamProjectRepository _examProjectRepository;
    private readonly IExamTargetRepository _examTargetRepository;

    private readonly ILabCatalogRepository _labCatalogRepository;
    private readonly ILabProjectRepository _labProjectRepository;
    private readonly ILabTargetRepository _labTargetRepository;
    private readonly ILabContainerRepository _labContainerRepository;
    private readonly IExecuteDepRuleDicRepository _executeDepRuleDicRepository;

    private readonly ILabReportInfoRepository _labReportInfoRepository;
    private readonly IExamAttachItemsRepository _examAttachItemsRepository;

    private readonly ILogger<LongGangHospitalClientHandler> _logger;

    //标记当前是否是LDC
    private bool _isLDC { get; set; }

    /// <summary>
    /// 龙岗中心医院订阅处理类
    /// </summary> 
    public LongGangHospitalClientHandler(ILabCatalogRepository labCatalogRepository,
        ILabProjectRepository labProjectRepository,
        ILabTargetRepository labTargetRepository,
        ITreatRepository treatRepository,
        ILabSpecimenRepository labSpecimenRepository,
        ILabContainerRepository labContainerRepository,
        IPharmacyRepository pharmacyRepository,
        IMedicineUsageRepository medicineUsageRepository,
        IMedicineFrequencyRepository medicineFrequencyRepository,
        IMedicineRepository medicineRepository,
        ICapPublisher capPublisher,
        IExamNoteRepository examNoteRepository,
        IDoctorRepository doctorRepository,
        IExamCatalogRepository examCatalogRepository,
        IExamProjectRepository examProjectRepository,
        IExamTargetRepository examTargetRepository,
        IExecuteDepRuleDicRepository executeDepRuleDicRepository,
        ILogger<LongGangHospitalClientHandler> logger,
        ILabReportInfoRepository labReportInfoRepository,
        IConfiguration configuration,
        IExamAttachItemsRepository examAttachItemsRepository)
    {
        _labCatalogRepository = labCatalogRepository;
        _labProjectRepository = labProjectRepository;
        _labTargetRepository = labTargetRepository;
        _treatRepository = treatRepository;
        _labSpecimenRepository = labSpecimenRepository;
        _labContainerRepository = labContainerRepository;
        _pharmacyRepository = pharmacyRepository;
        _medicineUsageRepository = medicineUsageRepository;
        _medicineFrequencyRepository = medicineFrequencyRepository;
        _medicineRepository = medicineRepository;
        _capPublisher = capPublisher;
        _examNoteRepository = examNoteRepository;
        _doctorRepository = doctorRepository;
        _examCatalogRepository = examCatalogRepository;
        _examProjectRepository = examProjectRepository;
        _examTargetRepository = examTargetRepository;
        _executeDepRuleDicRepository = executeDepRuleDicRepository;
        _logger = logger;
        _labReportInfoRepository = labReportInfoRepository;

        var hospitalCode = configuration["HospitalInfoConfig:HospitalCode"];
        if (!string.IsNullOrEmpty(hospitalCode) && hospitalCode == "LDC")
        {
            this._isLDC = true;
        }
        else
        {
            this._isLDC = false;
        }
        _examAttachItemsRepository = examAttachItemsRepository;

    }

    /// <summary>
    /// 全量同步龙岗字典信息
    /// 字典类型:1-检验;2-检查;3-科室;4-员工;5-费别;6-诊断;7-组套指引;8-诊疗材料;9-手术;10-药品用法;11-药品频次;12-药品信息;13-标本;21-检验单信息 
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    public async Task HandleEventAsync(FullHandlerEto eventData)
    {
        if (eventData == null) return;
        if (eventData.DicDatas.Count == 0) return;

        _logger.LogInformation("字典同步接收参数：{@Data}", eventData);
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            //同步检验数据
            if (eventData.DicType == 1)
            {
                await SyncLabProjects(eventData);
            }

            //同步检查数据
            if (eventData.DicType == 2)
            {
                await SyncExamProjects(eventData);
            }

            #region 全量同步科室字典

            #endregion

            #region 费别列表 5
            if (eventData.DicType == 5)
            {
                await _capPublisher.PublishAsync("sync.faber.from.masterdata", eventData);
            }
            #endregion

            #region 同步诊断数据 6

            if (eventData.DicType == 6)
            {
                await _capPublisher.PublishAsync("sync.diagnose.from.masterdata", eventData);
            }

            #endregion

            #region 同步诊疗数据 8

            //同步诊疗数据
            if (eventData.DicType == 8)
            {
                await SyncTreatProjects(eventData);
            }

            #endregion

            #region 同步药品用法信息 10

            //同步药品用法信息
            if (eventData.DicType == 10)
            {
                var list = JsonConvert.DeserializeObject<List<UsagesEto>>(
                    JsonConvert.SerializeObject(eventData.DicDatas));
                if (list == null || list.Count <= 0) return;

                var usageList = new List<MedicineUsage>();
                list?.ForEach(x =>
                {
                    usageList.Add(new MedicineUsage()
                    {
                        UsageCode = x.DrugUsageId,
                        UsageName = x.DrugUsageName,
                        FullName = x.DrugUsageName,
                        PyCode = x.SpellCode,
                        WbCode = x.DrugUsageName.FirstLetterWB(),
                        AddCard = x.AddCard,
                        TreatCode = x.Project
                    });
                });

                var usages = await _medicineUsageRepository.ToListAsync();
                var updateUsages = new List<MedicineUsage>();
                var addUsages = usageList.Where(x => usages.All(a => a.UsageCode != x.UsageCode))
                    .ToList();
                var deleteUsages = usages.Where(x => usageList.All(a => a.UsageCode != x.UsageCode))
                    .ToList();
                usageList.ForEach(x =>
                {
                    var data = usages.FirstOrDefault(g =>
                        g.UsageCode == x.UsageCode
                        && (g.UsageName != x.UsageName
                            || x.FullName != g.FullName
                            || g.AddCard != x.AddCard
                            || x.TreatCode != g.TreatCode));
                    if (data != null)
                    {
                        data.Update(x.UsageName, x.FullName, x.AddCard, x.TreatCode);
                        updateUsages.Add(data);
                    }
                });
                //去掉已删除的项
                updateUsages.RemoveAll(deleteUsages);
                //去掉新增的项
                updateUsages.RemoveAll(addUsages);
                if (addUsages.Any())
                {
                    await _medicineUsageRepository.InsertManyAsync(addUsages);
                }

                if (updateUsages.Any())
                {
                    updateUsages.ForEach(x =>
                    {
                        var data = usageList.FirstOrDefault(s => s.UsageCode == x.UsageCode);
                        if (data != null)
                        {
                            x.UsageName = data.UsageName;
                            x.FullName = data.FullName;
                            x.PyCode = data.PyCode;
                            x.WbCode = data.WbCode;
                            x.AddCard = data.AddCard;
                            x.TreatCode = data.TreatCode;
                        }
                    });
                    await _medicineUsageRepository.UpdateManyAsync(updateUsages);
                }

                if (deleteUsages.Any() && _isLDC)
                {
                    await _medicineUsageRepository.DeleteManyAsync(deleteUsages);
                }

                //foreach (var item in addUsages)
                //{
                //    _logger.LogInformation($"新增药品用法内容，{JsonConvert.SerializeObject(item)}");
                //}

                //foreach (var item in updateUsages)
                //{
                //    _logger.LogInformation($"更新药品用法内容，{JsonConvert.SerializeObject(item)}");
                //}

                //foreach (var item in deleteUsages)
                //{
                //    _logger.LogInformation($"删除药品用法内容，{JsonConvert.SerializeObject(item)}");
                //}
            }

            #endregion

            #region 同步药品频次 11

            if (eventData.DicType == 11)
            {
                var list = JsonConvert.DeserializeObject<List<FrequencyEto>>(
                    JsonConvert.SerializeObject(eventData.DicDatas));
                if (list == null || list.Count <= 0) return;
                var frequencyList = new List<MedicineFrequency>();

                foreach (var item in list)
                {
                    if (string.IsNullOrEmpty(item.DrugfrequencyCode)) continue;

                    frequencyList.Add(new MedicineFrequency()
                    {
                        ThirdPartyId = item.DrugfrequencyId,
                        FrequencyCode = item.DrugfrequencyCode,
                        FrequencyName = item.DrugfrequencyName,
                        FullName = item.DrugfrequencyName,
                        Times = int.Parse(item.DailyFrequency == "" ? "0" : item.DailyFrequency),
                        ExecDayTimes = item.ExecutionTime,
                        Sort = int.Parse(item.ArrangementOrder == "" ? "0" : item.ArrangementOrder)
                    });
                }

                var frequency = await (await _medicineFrequencyRepository.GetQueryableAsync())
                    .Where(w => !string.IsNullOrEmpty(w.FrequencyCode))
                    .ToListAsync();

                var updateFrequency = new List<MedicineFrequency>();
                var addFrequency = frequencyList.Where(x => frequency.All(a => a.FrequencyCode != x.FrequencyCode))
                    .ToList();
                var deleteFrequency = frequency
                    .Where(x => frequencyList.All(a => a.FrequencyCode != x.FrequencyCode))
                    .ToList();

                //去掉已删除的项
                frequencyList.RemoveAll(deleteFrequency);
                //去掉新增的项
                frequencyList.RemoveAll(addFrequency);
                frequencyList.ForEach(x =>
                {
                    var data = frequency.FirstOrDefault(g =>
                        g.ThirdPartyId == x.ThirdPartyId
                        && (g.FrequencyCode != x.FrequencyCode
                            || g.FrequencyName != x.FrequencyName
                            || x.FullName != g.FullName
                            || x.Times != g.Times
                            || x.ExecDayTimes != g.ExecDayTimes));
                    if (data != null)
                    {
                        data.Update(x.FrequencyCode, x.FrequencyName, x.FullName, x.Times, x.ExecDayTimes);
                        updateFrequency.Add(data);
                    }
                });

                if (addFrequency.Any())
                {
                    await _medicineFrequencyRepository.InsertManyAsync(addFrequency);
                }

                if (updateFrequency.Any())
                {
                    await _medicineFrequencyRepository.UpdateManyAsync(updateFrequency);
                }

                if (deleteFrequency.Any() && _isLDC)
                {
                    await _medicineFrequencyRepository.DeleteManyAsync(deleteFrequency);
                }

                foreach (var item in addFrequency)
                {
                    _logger.LogInformation("新增药品频次内容，{@Item}", item);
                }

                foreach (var item in updateFrequency)
                {
                    _logger.LogInformation("更新药品频次内容，{@Item}", item);
                }

                foreach (var item in deleteFrequency)
                {
                    _logger.LogInformation("删除药品频次内容，{@Item}", item);
                }
            }

            #endregion

            #region 同步药品信息 12

            if (eventData.DicType == 12)
            {
                var serializeStr = JsonConvert.SerializeObject(eventData.DicDatas);
                List<MedicinesEto> medicinesEtos = JsonConvert.DeserializeObject<List<MedicinesEto>>(serializeStr);
                if (medicinesEtos == null || medicinesEtos.Count <= 0) return;
                List<Medicine> oldMedicineEntitys = await (await _medicineRepository.GetQueryableAsync()).ToListAsync();
                // var dictStroage = await _dictionariesAppService.GetDictionariesGroupAsync("LongGangPharmacy");
                var frequency = await (await _medicineFrequencyRepository.GetQueryableAsync()).ToListAsync();
                var dicStorage = await (await _pharmacyRepository.GetQueryableAsync()).AsNoTracking().ToListAsync();
                var dict_Usage = await _medicineUsageRepository.ToListAsync();
                var updateMedicine = new List<Medicine>();
                //1:门诊西药房 5:感染药房 19:6号楼4楼门诊药房
                medicinesEtos = medicinesEtos.Where(k =>
                    Convert.ToInt32(k.Storage) == 1 || Convert.ToInt32(k.Storage) == 5 ||
                    Convert.ToInt32(k.Storage) == 19).ToList();

                List<Medicine> newMedicines = new List<Medicine>();
                medicinesEtos.GroupBy(g => g.Storage).ForEach(f =>
                {
                    f.ForEach(x =>
                    {
                        //龙岗急诊只开西药，只接入西药
                        if (x.DrugType == "0")
                        {
                            var medicine = new Medicine()
                            {
                                MedicineCode = x.DrugCode,
                                MedicineName = x.TradeName,
                                ScientificName = x.FormalName,
                                Alias = x.OtherName,
                                AliasPyCode = x.TradeName.FirstLetterPY(),
                                AliasWbCode = x.TradeName.FirstLetterWB(),
                                Specification = x.Specs,
                                Unit = string.IsNullOrEmpty(x.MinUnit) == true ? "" : x.MinUnit,
                                MedicineProperty = ConvertMedicineProperty(x.DrugType),
                                DosageForm = x.DoseModelCode,
                                DefaultDosage = double.Parse(x.OnceDose),
                                DosageQty = decimal.Parse(x.BaseDose),
                                DosageUnit = x.DoseUnit,
                                PyCode = x.SpellCode,
                                WbCode = x.TradeName.FirstLetterWB(),
                                Price = x.MinPackageIndicator == "1"
                                    ? decimal.Parse(x.RetailPrice) / x.MinimumPacking
                                    : decimal.Parse(x.RetailPrice),
                                SmallPackFactor = x.MinimumPacking,
                                FactoryName = x.ProducerName,
                                BigPackUnit = x.DrugUnit,
                                BigPackFactor = x.MinPackageIndicator == "0" ? int.Parse(x.PackQty) : 1,
                                BigPackPrice = decimal.Parse(x.RetailPrice),
                                SmallPackUnit = x.PackUnit,
                                SmallPackPrice = x.MinPackageIndicator == "1"
                                    ? decimal.Parse(x.RetailPrice) / x.MinimumPacking
                                    : decimal.Parse(x.RetailPrice),
                                ChildrenPrice = x.MinPackageIndicator == "1"
                                    ? decimal.Parse(x.RetailPrice) * decimal.Parse("1.3") / x.MinimumPacking
                                    : decimal.Parse(x.RetailPrice) * decimal.Parse("1.3"),
                                PharmacyCode = x.Storage,
                                PharmacyName = dicStorage.Count > 0
                                    ? dicStorage
                                        .FirstOrDefault(d => d.PharmacyCode == x.Storage)?.PharmacyName
                                    : "",
                                ExecDeptCode = x.Storage,
                                ExecDeptName = dicStorage.Count > 0
                                    ? dicStorage
                                        .FirstOrDefault(d => d.PharmacyCode == x.Storage)?.PharmacyName
                                    : "",
                                Unpack = GetUnPack(x.MinPackageIndicator, x.DrugRule),
                                FrequencyCode = string.IsNullOrEmpty(x.FrequencyCode) == true
                                    ? ""
                                    : frequency.FirstOrDefault(t => t.ThirdPartyId == x.FrequencyCode)
                                        ?.FrequencyCode,
                                FrequencyName = string.IsNullOrEmpty(x.FrequencyCode) == true
                                    ? ""
                                    : frequency.FirstOrDefault(t => t.ThirdPartyId == x.FrequencyCode)
                                        ?.FrequencyName,
                                InsuranceName = x.InsuranceType,
                                FixPrice = decimal.Parse(x.PurchasePrice),
                                RetPrice = decimal.Parse(x.RetailPrice),
                                InsuranceCode = string.IsNullOrEmpty(x.InsuranceCode) == true
                                    ? 4
                                    : int.Parse(x.InsuranceCode),
                                FactoryCode = x.ProducerCode,
                                IsSkinTest = string.IsNullOrEmpty(x.SkinTestSign) == true ? false :
                                    x.SkinTestSign == "0" ? false : true,
                                ToxicLevel = int.Parse(x.DrugAttributes),
                                AntibioticLevel = string.IsNullOrEmpty(x.AntibioticGrade) == true
                                    ? 0
                                    : int.Parse(x.AntibioticGrade),
                                IsLimited = string.IsNullOrEmpty(x.LimitType) == true ? false :
                                    x.LimitType == "0" ? false : true,
                                LimitedNote = x.LimitData,
                                IsActive = "0".Equals(x.UseFlag, StringComparison.Ordinal),

                                AntibioticPermission = string.IsNullOrEmpty(x.AntibioticSign) == true
                                    ? 0
                                    : int.Parse(x.AntibioticSign),
                                PrescriptionPermission = x.OctFlag ?? 0,
                                BaseFlag = ConvertBaseFlag(x.Extend2),
                                MedicalInsuranceCode = x.MedicalInsuranceCode,
                                UsageName = x.DrugChannel,
                                UsageCode = dict_Usage.FirstOrDefault(t => t.UsageName == x.DrugChannel)
                                    ?.UsageCode,
                                PlatformType = 0,

                            };

                            newMedicines.Add(medicine);
                        }
                    });
                });
                if (newMedicines.Count <= 0) return;

                List<Medicine> deleteMedicines = newMedicines.Where(x => !x.IsActive).ToList();
                List<Medicine> deleteMedicineEntitys = new List<Medicine>();
                if (deleteMedicines.Any())
                {
                    deleteMedicineEntitys = oldMedicineEntitys.Where(x => deleteMedicines.Any(a =>
                        x.PharmacyCode == a.PharmacyCode && x.MedicineCode == a.MedicineCode &&
                        x.FactoryCode == a.FactoryCode && x.Specification == a.Specification)).ToList();
                    newMedicines.RemoveAll(deleteMedicines);
                    oldMedicineEntitys.RemoveAll(deleteMedicineEntitys);
                }

                List<Medicine> addMedicineEntitys = newMedicines.Where(x => oldMedicineEntitys.All(a =>
                        !(x.PharmacyCode == a.PharmacyCode && x.MedicineCode == a.MedicineCode &&
                          x.FactoryCode == a.FactoryCode && x.Specification == a.Specification)))
                    .ToList();
                newMedicines.RemoveAll(addMedicineEntitys);

                oldMedicineEntitys.ForEach(x =>
                {
                    if (!newMedicines.Any(g =>
                            g.MedicineCode == x.MedicineCode
                            && g.MedicineName == x.MedicineName
                            && x.Specification == g.Specification
                            && x.Unit == g.Unit
                            && x.MedicineProperty == g.MedicineProperty
                            && x.DosageForm == g.DosageForm
                            && x.DosageQty == g.DosageQty
                            && x.DosageUnit == g.DosageUnit
                            && x.PyCode == g.PyCode
                            && x.IsSkinTest == g.IsSkinTest
                            && x.AntibioticPermission == g.AntibioticPermission
                            && x.IsLimited == g.IsLimited
                            && x.LimitedNote == g.LimitedNote
                            && x.PrescriptionPermission == g.PrescriptionPermission
                            && x.IsDrunk == g.IsDrunk
                            && x.IsPrecious == g.IsPrecious
                            && x.SmallPackFactor == g.SmallPackFactor
                            && x.SmallPackUnit == g.SmallPackUnit
                            && x.BigPackFactor == g.BigPackFactor
                            && x.BigPackUnit == g.BigPackUnit
                            && x.FrequencyCode == g.FrequencyCode
                            && x.FrequencyName == g.FrequencyName
                            && x.BaseFlag == g.BaseFlag
                            && x.MedicalInsuranceCode == g.MedicalInsuranceCode
                            && x.IsAllergyTest == g.IsAllergyTest
                            && x.FixPrice == g.FixPrice
                            && x.RetPrice == g.RetPrice
                            && x.PharmacyCode == g.PharmacyCode
                            && x.FactoryCode == g.FactoryCode
                            && x.FactoryName == g.FactoryName
                            && x.InsuranceCode == g.InsuranceCode
                            && x.InsuranceName == g.InsuranceName
                            && x.Unpack == g.Unpack
                            && x.IsActive == g.IsActive
                        ))
                    {
                        updateMedicine.Add(x);
                    }
                });

                if (addMedicineEntitys.Any())
                {
                    await _medicineRepository.InsertManyAsync(addMedicineEntitys);
                }

                if (updateMedicine.Any())
                {
                    updateMedicine.ForEach(x =>
                    {
                        var data = newMedicines.FirstOrDefault(s =>
                            s.PharmacyCode == x.PharmacyCode && s.MedicineCode == x.MedicineCode &&
                            s.FactoryCode == x.FactoryCode && s.Specification == x.Specification);
                        if (data != null)
                        {
                            x.MedicineCode = data.MedicineCode;
                            x.MedicineName = data.MedicineName;
                            x.ScientificName = data.ScientificName;
                            x.Alias = data.Alias;
                            x.Specification = data.Specification;
                            x.Unit = data.Unit;
                            x.MedicineProperty = data.MedicineProperty;
                            x.DosageForm = data.DosageForm;
                            x.DefaultDosage = data.DefaultDosage > 0 ? data.DefaultDosage : (double)data.DosageQty;
                            x.DosageQty = data.DosageQty;
                            x.DosageUnit = data.DosageUnit;
                            x.PyCode = data.PyCode;
                            x.WbCode = data.WbCode;
                            x.AliasPyCode = data.AliasPyCode;
                            x.AliasWbCode = data.AliasWbCode;
                            x.IsSkinTest = data.IsSkinTest;
                            x.AntibioticPermission = data.AntibioticPermission;
                            x.IsLimited = data.IsLimited;
                            x.LimitedNote = data.LimitedNote;
                            x.PrescriptionPermission = data.PrescriptionPermission;
                            x.IsDrunk = data.IsDrunk;
                            x.IsPrecious = data.IsPrecious;
                            x.SmallPackFactor = data.SmallPackFactor;
                            x.SmallPackUnit = data.SmallPackUnit;
                            x.BigPackFactor = data.BigPackFactor;
                            x.BigPackUnit = data.BigPackUnit;
                            x.ScientificName = data.ScientificName;
                            x.Alias = data.Alias;
                            x.UsageCode = data.UsageCode;
                            x.BaseFlag = data.BaseFlag;
                            x.MedicalInsuranceCode = data.MedicalInsuranceCode;
                            x.IsAllergyTest = data.IsAllergyTest;
                            x.PharmacyCode = data.PharmacyCode;
                            x.FactoryCode = data.FactoryCode;
                            x.FactoryName = data.FactoryName;
                            x.SmallPackPrice = data.SmallPackPrice;
                            x.BigPackPrice = data.BigPackPrice;
                            x.FixPrice = data.FixPrice;
                            x.RetPrice = data.RetPrice;
                            x.FrequencyCode = data.FrequencyCode;
                            x.FrequencyName = data.FrequencyName;
                            x.InsuranceCode = data.InsuranceCode;
                            x.InsuranceName = data.InsuranceName;
                            x.MedicalInsuranceCode = data.MedicalInsuranceCode;
                        }
                    });
                    await _medicineRepository.UpdateManyAsync(updateMedicine);
                }

                if (deleteMedicineEntitys.Any() && _isLDC)
                {
                    await _medicineRepository.HardDeleteAsync(x =>
                        deleteMedicineEntitys.Select(a => a.Id).Contains(x.Id));
                }

                foreach (var item in addMedicineEntitys)
                {
                    _logger.LogInformation("新增药品内容，{@Item}", item);
                }

                foreach (var item in updateMedicine)
                {
                    _logger.LogInformation("更新药品内容，{@Item}", item);
                }

                foreach (var item in deleteMedicineEntitys)
                {
                    _logger.LogInformation("删除药品内容，{@Item}", item);
                }
            }

            #endregion

            #region 标本 13

            if (eventData.DicType == 13)
            {
                var list = JsonConvert.DeserializeObject<List<SpecimenEto>>(
                    JsonConvert.SerializeObject(eventData.DicDatas));
                if (list == null || list.Count <= 0) return;
                //获取已存在的检验标本信息
                var specimenList = await _labSpecimenRepository.ToListAsync();
                //查询新增的检验标本信息
                var specimen = list
                    .Select(s => new LabSpecimen
                    {
                        SpecimenCode = s.SpecimenNo,
                        SpecimenName = s.SpecimenName,
                        PyCode = string.IsNullOrEmpty(s.SpellCode) ? s.SpecimenName.FirstLetterPY() : s.SpellCode,
                        WbCode = s.SpecimenName.FirstLetterWB()
                    }).ToList();
                //新增的标本
                var addSpecimen = specimen.Where(x => specimenList.All(a => a.SpecimenCode != x.SpecimenCode))
                    .ToList();
                //删除的标本
                var deleteSpecimen = specimenList.Where(x => specimen.All(a => a.SpecimenCode != x.SpecimenCode))
                    .ToList();
                //修改标本
                var updateSpecimen = new List<LabSpecimen>();
                specimen.RemoveAll(addSpecimen);
                specimen.RemoveAll(deleteSpecimen);
                specimen.ForEach(x =>
                {
                    var data = specimenList.FirstOrDefault(g =>
                        x.SpecimenCode == g.SpecimenCode && x.SpecimenName != g.SpecimenName);
                    if (data != null)
                    {
                        data.Update(x.SpecimenName);
                        updateSpecimen.Add(data);
                    }
                });
                if (addSpecimen.Any())
                {
                    await _labSpecimenRepository.InsertManyAsync(addSpecimen);
                }


                if (updateSpecimen.Any())
                {
                    await _labSpecimenRepository.UpdateManyAsync(updateSpecimen);
                }

                if (deleteSpecimen.Any() && _isLDC)
                {
                    await _labSpecimenRepository.DeleteManyAsync(deleteSpecimen);
                }

                foreach (var item in addSpecimen)
                {
                    _logger.LogInformation("新增检验标本内容，{@Item}", item);
                }

                foreach (var item in updateSpecimen)
                {
                    _logger.LogInformation("更新检验标本内容，{@Item}", item);
                }

                foreach (var item in deleteSpecimen)
                {
                    _logger.LogInformation("删除检验标本内容，{@Item}", item);
                }
            }

            #endregion

            #region 组套指引

            //if (eventData.DicType == 7)
            //{
            //    var list = JsonConvert.DeserializeObject<List<GuideDicEto>>(
            //        JsonConvert.SerializeObject(eventData.DicDatas));
            //    if (list == null || list.Count <= 0) return;
            //    //获取已存在的信息
            //    var examNoteList = await _examNoteRepository.ToListAsync();
            //    //查询新增的信息
            //    var examNote = list
            //        .Select(s => new ExamNote
            //        {
            //            NoteCode = s.GuideCode,
            //            NoteName = s.GuideName
            //                .Replace("~r", "\r")
            //                .Replace("~n", "\n")
            //                .Replace("~R", "\r")
            //                .Replace("~N", "\n"),
            //            DisplayName = s.GuideName.Replace("~r", "\r").Replace("~n", "\n").Replace("~R", "\r")
            //                .Replace("~N", "\n"),
            //            DescTemplate = s.GuideName.Replace("~r", "\r").Replace("~n", "\n").Replace("~R", "\r")
            //                .Replace("~N", "\n")
            //        }).ToList();
            //    //新增的
            //    var addExamNote = examNote.Where(x => examNoteList.All(a => a.NoteCode != x.NoteCode))
            //        .ToList();
            //    //删除的
            //    var deleteExamNote = examNoteList.Where(x => examNote.All(a => a.NoteCode != x.NoteCode))
            //        .ToList();
            //    //修改
            //    var updateExamNote = new List<ExamNote>();
            //    examNote.RemoveAll(addExamNote);
            //    examNote.RemoveAll(deleteExamNote);
            //    examNote.ForEach(x =>
            //    {
            //        var data = examNoteList.FirstOrDefault(
            //            g => x.NoteCode == g.NoteCode && x.NoteName != g.NoteName);
            //        if (data != null)
            //        {
            //            data.Update(x.NoteName, x.NoteName, x.NoteName);
            //            updateExamNote.Add(data);
            //        }
            //    });
            //    if (addExamNote.Any())
            //    {
            //        await _examNoteRepository.InsertManyAsync(addExamNote);
            //    }


            //    if (updateExamNote.Any())
            //    {
            //        await _examNoteRepository.UpdateManyAsync(updateExamNote);
            //    }

            //    if (deleteExamNote.Any() && _isLDC)
            //    {
            //        await _examNoteRepository.DeleteManyAsync(deleteExamNote);
            //    }
            //}

            //同步之前先清理之前的数据。 没办法监测到his后面删除的数据
            if (eventData.DicType == 7)
            {
                var list = JsonConvert.DeserializeObject<List<GuideDicEto>>(
                    JsonConvert.SerializeObject(eventData.DicDatas));
                if (list == null || list.Count <= 0) return;
                //获取已存在的信息
                var examNoteList = await _examNoteRepository.ToListAsync();
                //查询新增的信息
                var examNotes = new List<ExamNote>();

                foreach (var item in list)
                {
                    if (item == null || string.IsNullOrWhiteSpace(item.GuideName) || string.IsNullOrWhiteSpace(item.ProjectName))
                        continue;

                    examNotes.Add(new ExamNote
                    {
                        NoteCode = item.GuideCode,
                        NoteName = item.GuideName,
                        DisplayName = item.GuideName,
                        DescTemplate = item.ProjectName
                    });
                }

                if (examNotes.Count > 0)
                {
                    var ProjectNameList = examNotes.Select(c => c.DescTemplate).Distinct().ToList();
                    //var NoteCodeList = examNotes.Select(c => c.NoteCode).Distinct().ToList();
                    //查询出数据库已经有的Project 数据
                    var examProjectRepos = await _examProjectRepository.GetListAsync(c => ProjectNameList.Contains(c.ProjectName));
                    //查询出数据库中已经有的NoteCode 数据
                    var examNoteRepos = await _examNoteRepository.GetListAsync(c => c.IsDeleted == false);

                    var addExamNoteList = new List<ExamNote>();
                    var updateExamNoteList = new List<ExamNote>();
                    foreach (var item in examNotes)
                    {
                        // 查询出Project 给Item Code 赋值
                        var project = examProjectRepos.FirstOrDefault(c => c.ProjectName == item.DescTemplate);
                        // 找不到记录的直接continue
                        if (project == null || string.IsNullOrWhiteSpace(project.ProjectCode))
                        {
                            continue;
                        }
                        else
                        {
                            item.NoteCode = project.ProjectCode;
                        }
                        var model = examNoteRepos.Where(c => c.NoteCode == item.NoteCode).FirstNonDefault();
                        if (model != null && model.Id > 0)
                        {
                            model.NoteName = item.NoteName;
                            model.DescTemplate = item.DescTemplate;
                            model.DisplayName = item.NoteName;
                            //数据库中存在的修改，以his同步为准
                            updateExamNoteList.Add(model);
                        }
                        else
                        {
                            //数据库中不存在的新增
                            addExamNoteList.Add(item);
                        }
                    }
                    // 只要能查出来的数据直接赋值
                    foreach (var item in examProjectRepos)
                    {
                        item.GuideCode = item.ProjectCode;
                    }


                    if (addExamNoteList.Any())
                    {
                        await _examNoteRepository.InsertManyAsync(addExamNoteList);
                    }

                    if (updateExamNoteList.Any())
                    {
                        await _examNoteRepository.UpdateManyAsync(updateExamNoteList);
                    }
                    if (examProjectRepos.Any())
                    {
                        await _examProjectRepository.UpdateManyAsync(examProjectRepos);
                    }
                }

            }

            #endregion

            #region 员工字典信息

            if (eventData.DicType == 4)
            {
                List<DoctorEto> list = JsonConvert.DeserializeObject<List<DoctorEto>>(
                    JsonConvert.SerializeObject(eventData.DicDatas));
                if (list == null || list.Count <= 0) return;
                List<Doctor> etoList = ObjectMapper.Map<List<DoctorEto>, List<Doctor>>(list);
                //获取已存在的信息
                List<Doctor> doctorList = await _doctorRepository.ToListAsync();
                //新增的
                List<Doctor> addDoctors = etoList.Where(x => !doctorList.Any(a => a.DoctorCode == x.DoctorCode)).ToList();

                //删除的
                var deleteDoctors = doctorList.Where(x => !etoList.Any(a => a.DoctorCode == x.DoctorCode)).ToList();

                //修改
                var updateDoctors = new List<Doctor>();
                etoList.RemoveAll(addDoctors);
                doctorList.RemoveAll(deleteDoctors);

                etoList.ForEach(x =>
                {
                    var data = doctorList.FirstOrDefault(g => x.DoctorCode == g.DoctorCode
                                                              && (x.DoctorName != g.DoctorName
                                                                  || x.BranchCode != g.BranchCode
                                                                  || x.BranchName != g.BranchName
                                                                  || x.DeptCode != g.DeptCode
                                                                  || x.DeptName != g.DeptName
                                                                  || x.Sex != g.Sex
                                                                  || x.Skill != g.Skill
                                                                  || x.DoctorTitle != g.DoctorTitle
                                                                  || x.Telephone != g.Telephone
                                                                  || x.Introdution != g.Introdution
                                                                  || x.AnaesthesiaAuthority !=
                                                                  g.AnaesthesiaAuthority
                                                                  || x.DrugAuthority != g.DrugAuthority
                                                                  || x.SpiritAuthority != g.SpiritAuthority
                                                                  || x.AntibioticAuthority != g.AntibioticAuthority
                                                                  || x.PracticeCode != g.PracticeCode
                                                                  || x.IsActive != g.IsActive));
                    if (data != null)
                    {
                        data.Update(x.DoctorName, x.BranchCode, x.BranchName, x.DeptCode, x.DeptName, x.Sex,
                            x.DoctorTitle, x.Telephone, x.Skill, x.Introdution, x.AnaesthesiaAuthority,
                            x.DrugAuthority, x.SpiritAuthority, x.AntibioticAuthority, x.PracticeCode, x.IsActive,
                            x.DoctorType);
                        updateDoctors.Add(data);
                    }
                });

                if (addDoctors.Any())
                {
                    await _doctorRepository.InsertManyAsync(addDoctors);
                    //his数据有问题北大不在批量初始化
                    //IEnumerable<Doctor> doctors = addDoctors.Where(x => x.DoctorTitle.EndsWith("医师"));
                    //List<DoctorEto> doctorEtos = new List<DoctorEto>();
                    //foreach (Doctor doctor in doctors)
                    //{
                    //    DoctorEto doctorEto = new DoctorEto();
                    //    doctorEto.DoctorName = doctor.DoctorName;
                    //    doctorEto.DeptCode = doctor.DeptCode;
                    //    doctorEtos.Add(doctorEto);
                    //}
                    //await _capPublisher.PublishAsync("sync.doctorInfo.to.recipe", doctorEtos);
                }

                if (updateDoctors.Any())
                {
                    await _doctorRepository.UpdateManyAsync(updateDoctors);
                }

                if (deleteDoctors.Any())
                {
                    await _doctorRepository.DeleteManyAsync(deleteDoctors);
                }
            }

            #endregion

            #region 执行科室规则字典

            if (eventData.DicType == 15)
            {
                var input = JsonConvert.DeserializeObject<List<ExecuteDepRuleDic>>(JsonConvert.SerializeObject(eventData.DicDatas));
                if (input == null || input.Count <= 0) return;
                var entities = new List<ExecuteDepRuleDic>();
                input.ForEach(x =>
                {
                    entities.Add(new ExecuteDepRuleDic
                    {
                        ExeDepCode = x.ExeDepCode,
                        ExeDepName = x.ExeDepName,
                        RuleId = x.RuleId,
                        RuleName = x.RuleName,
                        SpellCode = x.SpellCode,
                    });
                });

                await _executeDepRuleDicRepository.DiffAndUpdateAsync(entities);
            }

            #endregion

            #region 检验单信息 21
            if (eventData.DicType == 21)
            {
                var syncEtoList = JsonConvert.DeserializeObject<List<LabReportInfoEto>>(
                    JsonConvert.SerializeObject(eventData.DicDatas));
                if (syncEtoList == null || syncEtoList.Count <= 0) return;
                //获取已存在
                var dbList = await _labReportInfoRepository.ToListAsync();
                //查询新增
                var syncList = syncEtoList
                    .Select(s => new LabReportInfo
                    {
                        Name = s.Name,
                        Code = s.Code,
                        SampleCollectType = s.SampleCollectType,
                        Remark = s.Remark,
                        CatelogName = s.CatelogName,
                        ExecDeptName = s.ExecDeptName,
                        TestTubeName = s.TestTubeName,
                        MergerNo = s.MergerNo,
                    }).ToList();
                //新增 使用AsParallel()多线程优化，使用Any替代All优化查找数量
                var addList = syncList.AsParallel().Where(x => !dbList.Any(a => a.Code == x.Code)).ToList();
                //删除 使用AsParallel()多线程优化，使用Any替代All优化查找数量
                var deleteList = dbList.AsParallel().Where(x => !syncList.Any(a => a.Code == x.Code)).ToList();
                //修改
                var updateList = new List<LabReportInfo>();
                syncList.RemoveAll(addList);
                dbList.RemoveAll(deleteList);
                foreach (LabReportInfo entity in dbList)
                {
                    LabReportInfo labReportInfo = syncList.FirstOrDefault(x => x.Code == entity.Code);
                    if (labReportInfo == null) continue;
                    if (entity.SampleCollectType != labReportInfo.SampleCollectType ||
                       entity.Remark != labReportInfo.Remark ||
                       entity.CatelogName != labReportInfo.CatelogName ||
                       entity.ExecDeptName != labReportInfo.ExecDeptName ||
                       entity.TestTubeName != labReportInfo.TestTubeName ||
                       entity.MergerNo != labReportInfo.MergerNo)
                    {
                        entity.Update(labReportInfo.Name, labReportInfo.SampleCollectType, labReportInfo.Remark, labReportInfo.CatelogName, labReportInfo.ExecDeptName, labReportInfo.TestTubeName, labReportInfo.MergerNo);
                        updateList.Add(entity);
                    }
                }

                if (addList.Any())
                {
                    await _labReportInfoRepository.InsertManyAsync(addList);
                }
                if (updateList.Any())
                {
                    await _labReportInfoRepository.UpdateManyAsync(updateList);
                }
                if (deleteList.Any())
                {
                    await _labReportInfoRepository.DeleteManyAsync(deleteList);
                }

                _logger.LogInformation("新增检验单信息数量：{Count}", addList.Count);
                foreach (var item in addList)
                {
                    _logger.LogTrace("新增检验单信息内容，{@Item}", item);
                }

                _logger.LogInformation("更新检验单信息数量：{Count}", updateList.Count);
                foreach (var item in updateList)
                {
                    _logger.LogTrace("更新检验单信息内容，{@Item}", item);
                }

                _logger.LogInformation("删除检验单信息数量：{Count}", deleteList.Count);
                foreach (var item in deleteList)
                {
                    _logger.LogTrace("删除检验单信息内容，{@Item}", item);
                }
            }

            #endregion

            #region 检查附加药品 22
            if (eventData.DicType == 22)
            {
                List<ExamMedicineCodeEto> etos = JsonConvert.DeserializeObject<List<ExamMedicineCodeEto>>(JsonConvert.SerializeObject(eventData.DicDatas));
                if (etos == null || etos.Count <= 0) return;
                await _examAttachItemsRepository.DeleteAsync(x => true);

                List<ExamAttachItem> examAttachItems = new List<ExamAttachItem>();
                foreach (ExamMedicineCodeEto eto in etos)
                {
                    ExamAttachItem examAttachItem = new ExamAttachItem(Guid.NewGuid())
                    {
                        ProjectCode = eto.ProjectCode,
                        MedicineCode = eto.MedicineCode,
                        Qty = eto.Qty,
                        Type = eto.Type,
                    };
                    examAttachItems.Add(examAttachItem);
                }
                await _examAttachItemsRepository.InsertManyAsync(examAttachItems);
            }
            #endregion
            await uow.CompleteAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            await uow.RollbackAsync();
        }
    }

    /// <summary>
    /// 处置项目同步
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    private async Task SyncTreatProjects(FullHandlerEto eventData)
    {
        var list = JsonConvert.DeserializeObject<List<TreatsEto>>(
                    JsonConvert.SerializeObject(eventData.DicDatas));
        if (list == null || list.Count <= 0) return;
        var treatList = new List<Treat>();

        var deleteCodes = new List<string>();
        foreach (var item in list)
        {
            if (string.IsNullOrEmpty(item.ProjectType)) continue;
            //需要删除的数据判断
            if (item.UseFlag != "0")
            {
                //_logger.LogInformation($"同步诊疗数据，需要删除的内容为，{JsonConvert.SerializeObject(item)}");
                deleteCodes.Add(item.ProjectId);
                continue;
            };
            treatList.Add(new Treat()
            {
                TreatCode = item.ProjectId,
                TreatName = item.ProjectName,
                CategoryName = item.ProjectTypeName,
                CategoryCode = item.ProjectType,
                Price = (decimal)(item.Price ?? 0),
                OtherPrice = item.Additional == "1" ? decimal.Parse(item.ChargeAmount.ToString("f2")) : 0,
                Additional = item.Additional == "1",
                PyCode = item.SpellCode,
                Unit = item.Unit,
                WbCode = item.ProjectName.FirstLetterPY(),
                ProjectMerge = item.ProjectMerge,
                IsDeleted = string.IsNullOrEmpty(item.UseFlag) == true || item.UseFlag == "1",
                MeducalInsuranceCode = item.MeducalInsuranceCode,
                YBInneCode = item.YBInneCode
            });
        }
        //var treats = await _treatRepository.ToListAsync();
        //删除his那边已经删除的数据 （北大这边treat 诊疗数据包含了 检验检查相关的子项数据）
        if (deleteCodes.Any())
        {
            //_logger.LogInformation($"同步诊疗数据，需要删除的TreatCode为，{JsonConvert.SerializeObject(deleteCodes)}");
            //  删除 treat 表格数据  DeleteAsync 方法删除不了  而且DeleteAsync方法底层用的也是 DeleteManyAsync 
            await _treatRepository.DeleteManyAsync(await _treatRepository.GetListAsync(c => deleteCodes.Contains(c.TreatCode)));
            //  删除（北大检验检查数据也在这里） 检验检查相关子项数据
            await _examTargetRepository.DeleteManyAsync(await _examTargetRepository.GetListAsync(c => deleteCodes.Contains(c.TargetCode)));
            await _labTargetRepository.DeleteManyAsync(await _labTargetRepository.GetListAsync(c => deleteCodes.Contains(c.TargetCode)));
        }
        if (treatList.Any())
        {
            var treatCodeList = treatList.Select(c => c.TreatCode).Distinct().ToList();
            //查询出数据库已经有的数据
            var treats = await _treatRepository.GetListAsync(c => treatCodeList.Contains(c.TreatCode));
            //数据库中不存在的新增
            var addTreats = treatList.Where(c => !treats.Select(x => x.TreatCode).Contains(c.TreatCode)).ToList();
            //数据库中存在的修改，以his同步为准
            var updateTreats = treatList.Where(c => treats.Select(x => x.TreatCode).Contains(c.TreatCode)).ToList();
            var updateList = new List<Treat>();
            //排除掉已删除的
            //var addTreats = treatList.Where(x => treats.All(a => a.TreatCode != x.TreatCode))
            //    .ToList();
            //var deleteTreats = treats.Where(x => treatList.All(a => a.TreatCode != x.TreatCode))
            //    .ToList();
            //去掉已删除的项
            //treatList.RemoveAll(deleteTreats);
            //去掉新增的项
            //treatList.RemoveAll(addTreats);
            updateTreats.ForEach(x =>
            {
                var data = treats.FirstOrDefault(g =>
                    x.TreatCode == g.TreatCode
                    && (g.CategoryName != x.CategoryName ||
                        x.Price != g.Price ||
                        x.Unit != g.Unit ||
                        g.CategoryCode != x.CategoryCode ||
                        x.TreatName != g.TreatName ||
                        x.PyCode != g.PyCode ||
                        x.OtherPrice != g.OtherPrice ||
                        x.ProjectMerge != g.ProjectMerge ||
                        x.IsDeleted != g.IsDeleted ||
                        x.Additional != g.Additional));
                if (data != null)
                {
                    data.Update(x.TreatName, x.Price, x.CategoryCode, x.CategoryName, x.Unit, x.OtherPrice,
                        x.ProjectMerge, x.IsDeleted, x.Additional);
                    data.YBInneCode = x.YBInneCode;
                    data.MeducalInsuranceCode = x.MeducalInsuranceCode;
                    updateList.Add(data);
                }
            });
            if (addTreats.Any())
            {
                await _treatRepository.InsertManyAsync(addTreats);
            }

            if (updateList.Any())
            {
                await _treatRepository.UpdateManyAsync(updateList);
            }
        }
        //if (deleteTreats.Any() && _isLDC)
        //{
        //    await _treatRepository.DeleteManyAsync(deleteTreats);
        //}

        //foreach (var item in addTreats)
        //{
        //    _logger.LogInformation($"新增诊疗材料内容，{JsonConvert.SerializeObject(item)}");
        //}

        //foreach (var item in updateTreats)
        //{
        //    _logger.LogInformation($"更新诊疗材料内容，{JsonConvert.SerializeObject(item)}");
        //}

        //foreach (var item in deleteTreats)
        //{
        //    _logger.LogInformation($"删除诊疗材料内容，{JsonConvert.SerializeObject(item)}");
        //}
    }

    /// <summary>
    /// 检查项目同步
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    private async Task SyncExamProjects(FullHandlerEto eventData)
    {
        _logger.LogInformation(
            "{Time}检查项目同步接收参数：{@DicDatas}", DateTime.Now, eventData.DicDatas);
        var dict_catalog = await _examCatalogRepository.GetListAsync();
        var dict_project = await _examProjectRepository.GetListAsync();

        var inspectGroupLists = JsonConvert.DeserializeObject<List<ExamAndLabEto>>(
            JsonConvert.SerializeObject(eventData.DicDatas));
        if (inspectGroupLists == null || inspectGroupLists.Count <= 0)
        {
            _logger.LogInformation(
                "{Time}字典同步接收参数：{@DicDatas}", DateTime.Now, eventData.DicDatas);
            _logger.LogError("序列化检查组套数据失败");
        }

        //1.检查目录
        var catalogGroup = inspectGroupLists
            .GroupBy(x => new { x.SuperiorCode, x.SuperiorName, x.SampleTypeId, x.SampleType }).ToList();

        foreach (var item in catalogGroup)
        {
            //判断目录是否存在
            var selectCatalog = dict_catalog.Find(x =>
                x.CatalogCode == item.Key.SampleTypeId && x.FirstNodeCode == item.Key.SuperiorCode);
            if (selectCatalog == null)
            {
                //1.1 新增目录
                var catalog = new ExamCatalog();
                catalog.FirstNodeCode = item.Key.SuperiorCode;
                catalog.FirstNodeName = item.Key.SuperiorName;
                catalog.CatalogCode = item.Key.SampleTypeId;
                catalog.CatalogName = item.Key.SampleType;
                catalog.DisplayName = item.Key.SampleType;
                catalog.PyCode = (item.Key.SuperiorName + item.Key.SampleType).FirstLetterPY();
                catalog.WbCode = (item.Key.SuperiorName + item.Key.SampleType).FirstLetterWB();
                catalog.Sort = 1;
                await _examCatalogRepository.InsertAsync(catalog, autoSave: true);
                _logger.LogInformation("{Time}检查目录新增内容,{@Catalog}", DateTime.Now, catalog);
            }
            else
            {
                //1.2 修改目录
                if (item.Key.SuperiorName != selectCatalog.FirstNodeName ||
                    item.Key.SampleType != selectCatalog.CatalogName)
                {
                    selectCatalog.CatalogName = item.Key.SampleType;
                    selectCatalog.FirstNodeName = item.Key.SuperiorName;
                    await _examCatalogRepository.UpdateAsync(selectCatalog, autoSave: true);
                    _logger.LogInformation(
                        "{Time}检查目录更新内容,{@SelectCatalog}", DateTime.Now, selectCatalog);
                }
            }
        }

        //2.检查项目
        //var groupLists = inspectGroupLists.GroupBy(x => new { x.GroupId, x.GroupName, x.SuperiorCode, x.SuperiorName }).ToList();
        var groupLists = inspectGroupLists.GroupBy(x => new { x.GroupId, x.GroupName }).ToList();  // 一个项目在多个目录下，只同步一个
        foreach (var group in groupLists)
        {
            var common = group.FirstOrDefault();
            var sumPrice = group.Sum(s =>
                decimal.Parse(s.Price.ToString("F2")) * decimal.Parse(s.TotalNumber)); //计算总价
            var selectProject = dict_project.Find(x =>
                x.CatalogCode == common.SampleTypeId && x.ProjectCode == common.GroupId/* && x.FirstNodeCode == common.SuperiorCode 这个条件会导致重复*/);
            if (selectProject == null)
            {

                //2.1 新增检查项目
                var examProject = new ExamProject();
                examProject.CatalogCode = common?.SampleTypeId;
                examProject.CatalogName = common?.SampleType;
                examProject.ProjectCode = group.Key.GroupId;
                examProject.ProjectName = group.Key.GroupName;
                examProject.ExecDeptCode = common?.DepartmentCode;
                examProject.ExecDeptName = common?.DepartmentName;
                examProject.Unit = common?.Unit;
                examProject.Price = sumPrice;
                examProject.GuideCode = common?.GuideCode;
                examProject.PyCode = common?.SpellCode;
                examProject.WbCode = group.Key.GroupName.FirstLetterWB();
                examProject.AddCard = common?.AddCard;
                examProject.DepExecutionType = common?.DepExecutionType;
                examProject.DepExecutionRules = common?.DepExecutionRules;
                examProject.IsActive = common?.UseFlag == "0";
                examProject.FirstNodeCode = group.First().SuperiorCode;
                examProject.FirstNodeName = group.First().SuperiorName;

                //不插入重复projectcode的项目   // 不插入his 那未被启用的数据
                if (examProject.IsActive == false || dict_project.Exists(x => x.ProjectCode == examProject.ProjectCode)) continue;

                await _examProjectRepository.InsertAsync(examProject, autoSave: true);
                _logger.LogInformation(
                    "{Time}检查项目新增内容,{@ExamProject}", DateTime.Now, examProject);
            }
            else
            {
                //if (common.UseFlag != (selectProject.IsActive == true ? "0" : "1")) //根据删除标志
                //{
                //    //2.3 删除检查项目
                //    selectProject.IsActive = common.UseFlag == "0" ? true : false;
                //    await _examProjectRepository.UpdateAsync(selectProject);
                //    _logger.LogInformation(
                //        $"{DateTime.Now}检查项目删除内容,{JsonConvert.SerializeObject(selectProject)}");
                //}
                //else
                //{
                //比对字段是否相同,不相同则更新
                if (group.Key.GroupName != selectProject.ProjectName
                    || common.SampleType != selectProject.CatalogName
                    || common.DepartmentCode != selectProject.ExecDeptCode
                    || common.DepartmentName != selectProject.ExecDeptName
                    || common.Unit != selectProject.Unit
                    || sumPrice != selectProject.Price
                    //|| selectProject.GuideCode != common?.GuideCode
                    || common.AddCard != selectProject.AddCard
                    || common.DepExecutionType != selectProject.DepExecutionType
                    || common.DepExecutionRules != selectProject.DepExecutionRules
                     || common.CheckPartCode != selectProject.CheckPartCode
                    || common.UseFlag != (selectProject.IsActive ? "0" : "1"))
                {
                    //2.2 更新检查项目
                    selectProject.ProjectName = group.Key.GroupName;
                    selectProject.CatalogName = common?.SampleType;
                    selectProject.ExecDeptCode = common?.DepartmentCode;
                    selectProject.ExecDeptName = common?.DepartmentName;
                    selectProject.Unit = common?.Unit;
                    // 因为现在分页同步数据 所以打散的数据会出现截断问题，这个价格只能在所有数据同步完成之后用脚本进行同步 这里设计有问题正常应该实时进行计算价格。三级数据价格发生变化这里有问题
                    selectProject.Price = sumPrice;
                    selectProject.PyCode = common?.SpellCode;
                    selectProject.AddCard = common?.AddCard;
                    selectProject.IsActive = common.UseFlag == "0";
                    //selectProject.GuideCode = common?.GuideCode;
                    selectProject.DepExecutionType = common?.DepExecutionType;
                    selectProject.DepExecutionRules = common?.DepExecutionRules;
                    selectProject.CheckPartCode = common?.CheckPartCode;
                    await _examProjectRepository.UpdateAsync(selectProject, autoSave: true);
                    _logger.LogInformation(
                        "{Time}检查项目更新内容,{@SelectProject}", DateTime.Now, selectProject);
                }
                //}
            }
        }

        await SyncExamTargetsAsync(inspectGroupLists);
    }

    /// <summary>
    /// 同步检查明细
    /// </summary>
    /// <param name="inspectGroupLists"></param>
    /// <returns></returns>
    private async Task SyncExamTargetsAsync(List<ExamAndLabEto> inspectGroupLists)
    {
        var dict_target = await _examTargetRepository.GetListAsync();
        // 分组去重
        var distinctInspectGroupList = inspectGroupLists.GroupBy(x => new
        {
            x.GroupId,
            x.GroupsId,
        });
        // 3.检查明细
        foreach (var groupItem in distinctInspectGroupList)
        {
            var item = groupItem.First();
            var selectTarget = dict_target.Find(x =>
                x.ProjectCode == groupItem.Key.GroupId && x.TargetCode == groupItem.Key.GroupsId/* && x.FirstNodeCode == item.SuperiorCode 这个条件会导致重复*/);
            if (selectTarget == null)
            {
                if (item.UseFlag == "1")
                {
                    continue;
                }

                //3.1新增明细项目
                var examTarget = new ExamTarget();
                examTarget.ProjectCode = item.GroupId;
                examTarget.TargetCode = item.GroupsId;
                examTarget.TargetName = item.GroupsName;
                examTarget.Specification = "";
                examTarget.PyCode = item.GroupsName.FirstLetterPY();
                examTarget.WbCode = item.GroupsName.FirstLetterWB();
                examTarget.Price = decimal.Parse(item.Price.ToString("F2"));
                examTarget.TargetUnit = item.Unit;
                examTarget.Qty = string.IsNullOrWhiteSpace(item.TotalNumber)
                    ? 0
                    : decimal.Parse(item.TotalNumber);
                examTarget.ProjectType = item.ProjectType;
                examTarget.ProjectMerge = item.ProjectMerge;
                examTarget.IsActive = item.UseFlag == "0";
                examTarget.FirstNodeCode = item.SuperiorCode;
                examTarget.FirstNodeName = item.SuperiorName;
                await _examTargetRepository.InsertAsync(examTarget, autoSave: true);
                _logger.LogInformation("{Time}检查明细新增内容,{@ExamTarget}", DateTime.Now, examTarget);
            }
            else
            {
                if (item.UseFlag == "1")
                {
                    await _examTargetRepository.DeleteAsync(selectTarget, autoSave: true);
                }
                else
                {
                    //3.2 更新明细项目
                    if (selectTarget.TargetName != item.GroupsName
                        || selectTarget.Price != decimal.Parse(item.Price.ToString("F2"))
                        || selectTarget.TargetUnit != item.Unit
                        || selectTarget.Qty != decimal.Parse(item.TotalNumber)
                        || selectTarget.ProjectType != item.ProjectType
                        || selectTarget.ProjectMerge != item.ProjectMerge
                        || selectTarget.IsActive != (item.UseFlag == "0"))
                    {
                        selectTarget.TargetName = item.GroupsName;
                        selectTarget.Specification = "";
                        selectTarget.PyCode = item.GroupsName.FirstLetterPY();
                        selectTarget.WbCode = item.GroupsName.FirstLetterWB();
                        selectTarget.Price = decimal.Parse(item.Price.ToString("F2"));
                        selectTarget.TargetUnit = item.Unit;
                        selectTarget.Qty = string.IsNullOrWhiteSpace(item.TotalNumber)
                            ? 0
                            : decimal.Parse(item.TotalNumber);
                        selectTarget.ProjectType = item.ProjectType;
                        selectTarget.ProjectMerge = item.ProjectMerge;
                        selectTarget.IsActive = item.UseFlag == "0";
                        selectTarget.FirstNodeName = item.SuperiorName;
                        await _examTargetRepository.UpdateAsync(selectTarget, autoSave: true);
                        _logger.LogInformation(
                            "{Time}检查明细更新内容,{@SelectTarget}", DateTime.Now, selectTarget);
                    }
                }

            }
        }
        // 查询已同步的数据，用于对比删除数据（需要ddp每次传过来的数据，检验项目包含的检验明细/收费项都在一个消息里面，否则就会删错数据）
        var existsExamTargets = await _examTargetRepository.GetListAsync(x => inspectGroupLists.Select(y => y.GroupId).Contains(x.ProjectCode));
        var deletedExamTargets = existsExamTargets.Where(x => !inspectGroupLists.Any(y => y.GroupId == x.ProjectCode && y.SuperiorCode == x.FirstNodeCode && x.TargetCode == y.GroupsId));
        await _examTargetRepository.DeleteManyAsync(deletedExamTargets, autoSave: true);
    }

    /// <summary>
    /// 检验项目同步
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    private async Task SyncLabProjects(FullHandlerEto eventData)
    {
        _logger.LogInformation(
            "{Time}检验项目同步接收参数：{@DicDatas}", DateTime.Now, eventData.DicDatas);
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            var list = JsonConvert.DeserializeObject<List<LabEto>>(JsonConvert.SerializeObject(eventData.DicDatas));
            var labProjectList = new List<LabProject>();
            var labTargetList = new List<LabTarget>();
            var catalogList = new List<LabCatalog>();
            var labProjectSort = 0;
            var labTargetSort = 0;
            var labCatalog = list.GroupBy(g => new { g.SampleTypeId }).ToList();
            var sortCata = 0;

            foreach (var catalogItem in labCatalog)
            {
                var sampleTyme = catalogItem.FirstOrDefault().SampleType;
                catalogList.Add(new LabCatalog()
                {
                    CatalogCode = catalogItem.Key.SampleTypeId,
                    CatalogName = sampleTyme,
                    PyCode = sampleTyme.FirstLetterPY(),
                    WbCode = sampleTyme.FirstLetterWB(),
                    Sort = sortCata++
                });
                var labProject = catalogItem.GroupBy(g => new { g.GroupId }).ToList();
                labProject.ForEach(x =>
                {
                    var catalogAndProjectCode = catalogItem.Key.SampleTypeId + x.Key.GroupId;
                    labProjectSort++;
                    var dto = x.FirstOrDefault();

                    // 项目去重
                    if (!labProjectList.Any(y => y.ProjectCode == x.Key.GroupId))
                    {
                        labProjectList.Add(new LabProject()
                        {
                            CatalogAndProjectCode = catalogAndProjectCode,
                            CatalogCode = catalogItem.Key.SampleTypeId,
                            CatalogName = sampleTyme,
                            ProjectCode = x.Key.GroupId,
                            ProjectName = dto?.GroupName,
                            SpecimenCode = dto?.SpecimenNo,
                            SpecimenName = dto?.SpecimenName,
                            ExecDeptCode = dto?.DepartmentCode,
                            ExecDeptName = dto?.DepartmentName,
                            ContainerCode = dto?.ContainerId,
                            ContainerName = dto?.ContainerType,
                            Unit = dto?.Unit,
                            // 因为现在分页同步数据 所以打散的数据会出现截断问题，这个价格只能在所有数据同步完成之后用脚本进行同步
                            Price = x.Sum(s => Convert.ToDecimal(s.Price.ToString("F2")) * (string.IsNullOrEmpty(s.TotalNumber) ? 0 : decimal.Parse(s.TotalNumber))),
                            Sort = labProjectSort,
                            PyCode = dto?.SpellCode,
                            WbCode = dto?.GroupName.FirstLetterWB(),
                            AddCard = dto?.AddCard,
                            IsActive = dto?.UseFlag == "0",
                            DepExecutionType = dto?.DepExecutionType,
                            DepExecutionRules = dto?.DepExecutionRules,
                        });
                    }
                    x.ForEach(f =>
                    {
                        // 项目去重
                        if (!labTargetList.Any(y => y.ProjectCode == x.Key.GroupId && y.TargetCode == f.GroupsId))
                        {
                            labTargetSort++;
                            labTargetList.Add(new LabTarget()
                            {
                                //自己维护treeNode 就不需要这个玩意儿。
                                //CatalogAndProjectCode = catalogAndProjectCode,
                                ProjectCode = x.Key.GroupId,
                                TargetCode = f.GroupsId,
                                TargetName = f.GroupsName,
                                Sort = labTargetSort,
                                PyCode = f.GroupsName.FirstLetterPY(),
                                WbCode = f.GroupsName.FirstLetterWB(),
                                Price = decimal.Parse(f.Price.ToString("F2")),
                                TargetUnit = f.Unit,
                                Qty = string.IsNullOrEmpty(f.TotalNumber) ? 0 : decimal.Parse(f.TotalNumber),
                                ProjectType = f.ProjectType,
                                ProjectMerge = f.ProjectMerge,
                                IsActive = f.UseFlag == "0",
                            });
                        }
                    });

                });
            }


            #region 检验目录
            //获取已存在的检验部位信息
            var catalog = await _labCatalogRepository.ToListAsync();
            //查询新增的部位信息
            //新增检验部位
            var addCatalog = catalogList.Where(x => catalog.All(a => a.CatalogCode != x.CatalogCode))
                .ToList();
            //删除检验部位
            var deleteCatalog = catalog.Where(x => catalogList.All(a => a.CatalogCode != x.CatalogCode))
                .ToList();
            //修改检验部位
            var updateCatalog = new List<LabCatalog>();
            catalogList.RemoveAll(addCatalog);
            catalogList.RemoveAll(deleteCatalog);
            catalogList.ForEach(x =>
            {
                var data = catalog.FirstOrDefault(
                    g => x.CatalogCode == g.CatalogCode && x.CatalogName != g.CatalogName);
                if (data != null)
                {
                    data.Modify(x.CatalogName, "", "", data.Sort, data.IsActive);
                    updateCatalog.Add(data);
                }
            });
            if (addCatalog.Any())
            {
                await _labCatalogRepository.InsertManyAsync(addCatalog);
            }
            if (updateCatalog.Any())
            {
                await _labCatalogRepository.UpdateManyAsync(updateCatalog);
            }

            if (deleteCatalog.Any() && _isLDC)
            {
                await _labCatalogRepository.DeleteManyAsync(deleteCatalog);
            }

            #endregion
            #region 检验项目
            var projectList = await _labProjectRepository.ToListAsync();
            var updateProject = new List<LabProject>();
            var addProject = labProjectList.Where(x => projectList.All(a => a.CatalogAndProjectCode != x.CatalogAndProjectCode))
                .ToList();
            var deleteProject = labProjectList.Where(x => x.IsActive == false).ToList();

            //去掉已删除的项
            labProjectList.RemoveAll(deleteProject);
            //去掉新增的项
            labProjectList.RemoveAll(addProject);

            labProjectList.ForEach(x =>
            {
                var data = projectList.FirstOrDefault(g =>
                    g.CatalogAndProjectCode == x.CatalogAndProjectCode
                    && (g.ProjectName != x.ProjectName
                        || x.Price != g.Price
                        || x.Unit != g.Unit
                        || x.ContainerCode != g.ContainerCode
                        || x.ContainerName != g.ContainerName
                        || x.PyCode != g.PyCode
                        || x.SpecimenCode != g.SpecimenCode
                        || x.SpecimenName != g.SpecimenName
                        || x.ExecDeptCode != g.ExecDeptCode
                        || x.ExecDeptName != g.ExecDeptName
                        || x.AddCard != g.AddCard
                        || x.DepExecutionType != g.DepExecutionType
                        || x.DepExecutionRules != g.DepExecutionRules
                        || x.CatalogCode != g.CatalogCode
                        || x.CatalogName != g.CatalogName
                        || x.IsActive != g.IsActive));
                if (data != null)
                {
                    // 因为现在分页同步数据 所以打散的数据会出现截断问题，这个价格只能在所有数据同步完成之后用脚本进行同步
                    data.Modify(
                        x.ProjectName,
                        x.CatalogCode,
                        x.CatalogName,
                        x.SpecimenCode,
                        x.SpecimenName,
                        x.ExecDeptCode,
                        x.ExecDeptName,
                        "",
                        "",
                        data.Sort,
                        x.Unit,
                        //x.Price,
                        data.OtherPrice,
                        x.IsActive,
                        x.ContainerCode,
                        x.ContainerName,
                        depExecutionType: x.DepExecutionType,
                        depExecutionRules: x.DepExecutionRules);
                    updateProject.Add(data);
                }
            });

            //不添加重复的项目
            List<string> projectCodes = projectList.Select(x => x.ProjectCode).ToList();
            addProject = addProject.Where(x => !projectCodes.Contains(x.ProjectCode)).ToList();
            //检验项目
            if (addProject.Any())
            {
                //只插入Is Active的数据
                addProject = addProject.Where(c => c.IsActive == true).ToList();
                await _labProjectRepository.InsertManyAsync(addProject);
            }

            if (updateProject.Any())
            {
                await _labProjectRepository.UpdateManyAsync(updateProject);
            }

            if (deleteProject.Any())
            {
                IEnumerable<string> projectCodeList = deleteProject.Select(x => x.ProjectCode);
                await _labProjectRepository.DeleteAsync(x => projectCodeList.Contains(x.ProjectCode));
            }

            #endregion
            #region 检验明细

            //查询检验已存在数据
            var labTarget = await _labTargetRepository.ToListAsync();
            var addTarget = labTargetList.Where(x => !labTarget.Any(a => a.TargetCode == x.TargetCode && a.ProjectCode == x.ProjectCode))
                .ToList();
            //var deleteTarget = labTarget.Where(x => !labTargetList.Any(a => a.TargetCode == x.TargetCode && a.ProjectCode == x.ProjectCode))
            //    .ToList();
            // 重写删除的逻辑（需要ddp每次传过来的数据，检验项目包含的检验明细/收费项都在一个消息里面，否则就会删错数据）
            var deleteTarget = labTarget.Where(x => labProjectList.Select(y => y.ProjectCode).Contains(x.ProjectCode))
                                        .Where(x => !labTargetList.Any(a => a.TargetCode == x.TargetCode && a.ProjectCode == x.ProjectCode));

            var updateTarget = new List<LabTarget>();
            // 除了新增跟删除的，其他都标识为修改
            var maybeUpdateLabTargetList = labTargetList.Except(deleteTarget).Except(addTarget);
            maybeUpdateLabTargetList.ForEach(x =>
            {
                var data = labTarget.FirstOrDefault(g =>
                    g.TargetCode == x.TargetCode && g.ProjectCode == x.ProjectCode
                    && (g.TargetName != x.TargetName
                        || x.Price != g.Price
                        || x.TargetUnit != g.TargetUnit
                        || x.ProjectType != g.ProjectType
                        || x.ProjectMerge != g.ProjectMerge
                        || x.Qty != g.Qty
                        || x.IsActive != g.IsActive));
                if (data != null)
                {
                    data.Modify(x.TargetName, data.Sort, x.TargetUnit, x.Qty, x.Price, InsuranceCatalog.Self, x.IsActive);
                    updateTarget.Add(data);
                }
            });

            //检验明细
            if (addTarget.Any())
            {
                await _labTargetRepository.InsertManyAsync(addTarget);
            }

            if (updateTarget.Any())
            {
                await _labTargetRepository.UpdateManyAsync(updateTarget);
            }

            if (deleteTarget.Any())
            {
                await _labTargetRepository.DeleteManyAsync(deleteTarget);
            }

            #endregion
            #region 容器

            //查询检验容器已存在数据
            var container = await _labContainerRepository.ToListAsync();
            var containerList = new List<LabContainer>();
            //添加新增的检验容器
            labProjectList
                .GroupBy(g => new { g.ContainerCode, g.ContainerName })
                .ForEach(f =>
                {
                    if (string.IsNullOrEmpty(f.Key.ContainerName))
                    {
                        return;
                    }
                    containerList.Add(new LabContainer(
                        containerCode: f.Key.ContainerCode, containerName: f.Key.ContainerName, // 容器名称
                        containerColor: "", // 容器颜色
                        isActive: true
                    ));
                });
            //新增的容器
            var addContainer = containerList.Where(x => container.All(a => a.ContainerCode != x.ContainerCode))
                .ToList();
            //删除的容器
            var deleteContainer = container
                .Where(x => containerList.All(a => a.ContainerCode != x.ContainerCode))
                .ToList();
            //修改容器
            var updateContainer = new List<LabContainer>();
            containerList.RemoveAll(addContainer);
            containerList.RemoveAll(deleteContainer);
            containerList.ForEach(x =>
            {
                var data = container.FirstOrDefault(g => x.ContainerCode == g.ContainerCode && x.ContainerName != g.ContainerName);
                if (data != null)
                {
                    data.Modify(x.ContainerName, "", true);
                    updateContainer.Add(data);
                }
            });
            if (addContainer.Any())
            {
                await _labContainerRepository.InsertManyAsync(addContainer);
            }

            if (updateContainer.Any())
            {
                await _labContainerRepository.UpdateManyAsync(updateContainer);
            }

            if (deleteContainer.Any() && _isLDC)
            {
                await _labContainerRepository.DeleteManyAsync(deleteContainer);
            }

        }
        catch (Exception)
        {
            await uow.RollbackAsync();
        }
        await uow.CompleteAsync();

        #endregion
    }



    /// <summary>
    /// 获取医保类型
    /// </summary>
    /// <param name="insuranceCode"></param>
    /// <returns></returns>
    private InsuranceCatalog GetInsuranceType(string insuranceCode)
    {
        if (string.IsNullOrEmpty(insuranceCode))
        {
            return InsuranceCatalog.Self;
        }

        switch (insuranceCode)
        {
            case "1":
                return InsuranceCatalog.ClassA;
            case "2":
                return InsuranceCatalog.ClassB;
            case "3":
                return InsuranceCatalog.ClassC;
            case "4":
                return InsuranceCatalog.Self;
            case "5":
                return InsuranceCatalog.Children;
            default:
                return InsuranceCatalog.Self;
        }
    }

    /// <summary>
    /// 获取拆零规则
    /// </summary>
    /// <param name="minPackageIndicator"></param>
    /// <param name="drugRule"></param>
    /// <returns></returns>
    private MedicineUnPack GetUnPack(string minPackageIndicator, string drugRule)
    {
        if (minPackageIndicator == "1" && drugRule == "1")
        {
            return MedicineUnPack.RoundByPackUnitTime; //3
        }

        if (minPackageIndicator == "0" && drugRule == "1")
        {
            return MedicineUnPack.RoundByPackUnitAmount; //1
        }

        if (minPackageIndicator == "1" && drugRule == "0")
        {
            return MedicineUnPack.RoundByMinUnitTime; //2
        }

        if (minPackageIndicator == "0" && drugRule == "0")
        {
            return MedicineUnPack.RoundByMinUnitAmount; // 0
        }

        if (drugRule == "2")
        {
            return MedicineUnPack.RoundByMinUnit; //4
        }

        return MedicineUnPack.UnKnown;
    }


    //0 西药 1 中成药 2 中草药
    private string ConvertMedicineProperty(string drugType)
    {
        switch (drugType)
        {
            case "0":
                return "西药";
            case "1":
                return "中成药";
            case "2":
                return "中草药";
            default:
                return "其它";
        }
    }


    private string ConvertBaseFlag(string baseFlag)
    {
        switch (baseFlag)
        {
            case "00":
                return "普通";
            case "01":
                return "国基";
            case "02":
                return "省基";
            case "03":
                return "市基";
            case "04":
                return "基药";
            case "05":
                return "中草药";
            case "06":
                return "非基药";
            default:
                return "其它";
        }
    }
}