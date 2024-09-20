using MasterDataService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiJian.DoctorsAdvices.Enums;
using YiJian.Documents.Dto;
using YiJian.Documents.Enums;
using YiJian.ECIS.Core.FastReport;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.Hospitals.Enums;
using YiJian.Recipe;

namespace YiJian.Documents
{
    /// <summary>
    /// 打印服务（分方打印）
    /// </summary>
    public partial class DocumentsAppService
    {
        /// <summary>
        /// 报表的Schema，方便用户配置
        /// </summary>
        /// <returns></returns> 
        [NonUnify]
        public DocumentResponseDto GetReportSchema()
        {
            try
            {
                return new DocumentResponseDto()
                {
                    Lises = new List<LisAdviceDto>
                {
                    new LisAdviceDto
                    {
                        Amount = 0, ApplyDeptCode = "", ApplyDeptName = "", ApplyDoctorCode = "",
                        ApplyDoctorName = "", ApplyTime = DateTime.Now, CatalogCode = "", CatalogName = "",
                        CategoryCode = "", CategoryName = "",
                        ClinicalSymptom = "", Code = "", ContainerCode = "", ContainerColor = "", ContainerName = "",
                        Diagnosis = "", ExecDeptCode = "",
                        ExecDeptName = "", HasReportName = false, HisOrderNo = "", Id = Guid.Empty, InsuranceCode = "",
                        InsuranceType = EInsuranceCatalog.Self,
                        IsBackTracking = false, IsBedSide = false, IsChronicDisease = false, IsEmergency = false,
                        Name = "", PayType = ERecipePayType.Self,
                        PayTypeCode = "", PIID = Guid.Empty, PrescribeTypeCode = "", PrescribeTypeName = "",
                        PrescriptionNo = "", Price = 0, RecieveQty = 0,
                        RecieveUnit = "", RecipeGroupNo = 0, RecipeNo = "", Remarks = "", ReportDoctorCode = "",
                        ReportDoctorName = "", ReportTime = null,
                        SpecimenCode = "", SpecimenCollectDatetime = null, SpecimenDescription = "", SpecimenName = "",
                        SpecimenPartCode = "", SpecimenPartName = "",
                        SpecimenReceivedDatetime = null, Status = ERecipeStatus.Submitted, Unit = "",
                        ExecTime = DateTime.Now, ExecutorName = "", ExecutorCode = "",
                        AddCard = "", IsRecipePrinted = false, ApplyTimeToString = "", ChannelNo = "",
                        ChannelNumber = "",
                        HisNumber = "", Lgjkzx_payurl = "", Lgzxyy_payurl = "", MedicalNo = "", MedType = "",
                    }
                },
                    Pacses = new List<PacsAdviceDto>
                {
                    new PacsAdviceDto
                    {
                        Amount = 0, Unit = "", Status = ERecipeStatus.Submitted, ApplyDeptCode = "",
                        ApplyDeptName = "", ApplyDoctorCode = "", ApplyDoctorName = "", ApplyTime = DateTime.Now,
                        CatalogCode = "", CatalogDisplayName = "",
                        CatalogName = "", CategoryCode = "", CategoryName = "", ClinicalSymptom = "", Code = "",
                        Diagnosis = "", HasReport = false, HisOrderNo = "",
                        Id = Guid.Empty, InsuranceCode = "", InsuranceType = EInsuranceCatalog.Self,
                        IsBackTracking = false, IsBedSide = false,
                        IsChronicDisease = false, IsEmergency = false, MedicalHistory = "", Name = "", PartCode = "",
                        PartName = "", PayType = ERecipePayType.Self,
                        PayTypeCode = "", PIID = Guid.Empty, PrescribeTypeCode = "", PrescribeTypeName = "",
                        PrescriptionNo = "", Price = 0, RecieveQty = 0,
                        RecieveUnit = "", RecipeGroupNo = 0, RecipeNo = "", Remarks = "", ReportDoctorCode = "",
                        ReportDoctorName = "", ReportTime = null,
                        ExecutorCode = "", ExecutorName = "", ExecTime = DateTime.Now, ExecDeptName = "",
                        ExecDeptCode = "", AddCard = "", IsRecipePrinted = false, ApplyTimeToString = "",
                        ChannelNo = "", ChannelNumber = "",
                        HisNumber = "", Lgjkzx_payurl = "", Lgzxyy_payurl = "", MedicalNo = "", MedType = "",
                    }
                },
                    Medicines = new List<MedicineAdviceDto>
                {
                    new MedicineAdviceDto
                    {
                        SkinTestSignChoseResult = ESkinTestSignChoseResult.Yes,
                        RestrictedDrugs = ERestrictedDrugs.QuanZifei, LimitType = 1, Remarks = "", RecipeNo = "",
                        RecipeGroupNo = 0, RecieveUnit = "",
                        RecieveQty = 0, Price = 0, ActualDays = 0, Amount = 0, AntibioticPermission = 0,
                        ApplyDeptCode = "", ApplyDeptName = "", ApplyDoctorCode = "",
                        ApplyDoctorName = "", ApplyTime = DateTime.Now, BatchNo = "", CategoryCode = "",
                        CategoryName = "", Code = "", Diagnosis = "",
                        DosageQty = 0, DosageUnit = "", FactoryCode = "", FactoryName = "", FrequencyCode = "",
                        FrequencyExecDayTimes = null, FrequencyName = "",
                        FrequencyTimes = 0, FrequencyUnit = "", HisOrderNo = "", Id = Guid.Empty, InsuranceCode = "",
                        InsuranceType = EInsuranceCatalog.Self,
                        IsAdaptationDisease = false, IsBackTracking = false, IsChronicDisease = false,
                        IsFirstAid = false, IsSkinTest = false, LongDays = 0,
                        MaterialPrice = 0, MedicineProperty = "", Name = "", PayType = ERecipePayType.Self,
                        PayTypeCode = "", PharmacyCode = "", PharmacyName = "",
                        PIID = Guid.Empty, PrescribeTypeCode = "", PrescribeTypeName = "", PrescriptionNo = "",
                        PrescriptionPermission = 0, QtyPerTimes = 0,
                        SkinTestResult = false, Specification = "", Speed = "", Status = ERecipeStatus.Saved,
                        ToxicProperty = "", Unit = "", UsageCode = "", UsageName = "",
                        ExecutorCode = "", ExecutorName = "", ExecTime = DateTime.Now, ExecDeptName = "",
                        ExecDeptCode = "", IsRecipePrinted = false, ApplyTimeToString = "", ChannelNo = "",
                        ChannelNumber = "",
                        HisNumber = "", Lgjkzx_payurl = "", Lgzxyy_payurl = "", MedicalNo = "", MedType = "",
                    }
                },
                    Treats = new List<TreatAdviceDto>
                {
                    new TreatAdviceDto
                    {
                        Unit = "", Amount = 0, ApplyDeptCode = "", ApplyDeptName = "", ApplyDoctorCode = "",
                        ApplyDoctorName = "", ApplyTime = DateTime.Now, CategoryCode = "", CategoryName = "", Code = "",
                        Diagnosis = "", FeeTypeMainCode = "",
                        FeeTypeSubCode = "", FrequencyCode = "", FrequencyName = "", HisOrderNo = "", Id = Guid.Empty,
                        InsuranceCode = "", InsuranceType = EInsuranceCatalog.Self,
                        IsBackTracking = false, IsChronicDisease = false, Name = "", OtherPrice = 0,
                        PayType = ERecipePayType.Self, PayTypeCode = "",
                        PIID = Guid.Empty, PrescribeTypeCode = "", PrescribeTypeName = "", PrescriptionNo = "",
                        Price = 0, RecieveQty = 0, RecieveUnit = "",
                        RecipeGroupNo = 0, RecipeNo = "", Remarks = "", Specification = "",
                        Status = ERecipeStatus.Saved,
                        ExecutorCode = "", ExecutorName = "", ExecTime = DateTime.Now, ExecDeptName = "",
                        ExecDeptCode = "", IsRecipePrinted = false, ApplyTimeToString = "", ChannelNo = "",
                        ChannelNumber = "",
                        HisNumber = "", Lgjkzx_payurl = "", Lgzxyy_payurl = "", MedicalNo = "", MedType = "",
                    }
                },
                    AdmissionRecords = new List<AdmissionRecordDto>
                {
                    new AdmissionRecordDto
                    {
                        Age = "", AllergyHistory = "", AreaCode = "", AreaName = "",
                        AR_ID = 0, Bed = "", Birthday = null, CallStatus = ECallStatus.Calling, CardNo = "",
                        ChargeType = "", ChestFlag = false, ContactsPerson = "",
                        ContactsPhone = "", CoughFlag = false, DeathTime = DateTime.Now.AddDays(100), DiagnoseCode = "",
                        DiagnoseName = "", DutyDoctorCode = "",
                        DutyDoctorName = "", DutyNurseCode = "", DutyNurseName = "",
                        EmergencyLevel = EEmergencyLevel.一般, FinishVisitTime = DateTime.MinValue,
                        FirstDoctorCode = "", FirstDoctorName = "", FluFlag = false, FluTemp = "", GreenRoad = "",
                        Height = "", HomeAddress = "", IdentityCode = "",
                        IdentityName = "", IDNo = "", InDeptTime = null, InDeptWay = "", InfectiousHistory = "",
                        IsAttention = "", IsHasTransfer = false,
                        IsInHospital = false, IsPlanBackRoom = false, IsTop = false, KeyDiseasesCode = "",
                        LockDate = null, MedicalCard = "", NarrationCode = "",
                        NarrationName = "", NurseGrade = "", OperatorCode = "", OperatorName = "",
                        OutDeptReason = EOutDeptReason.转住院, OutDeptTime = DateTime.MinValue,
                        PastMedicalHistory = "", PatientID = "", PatientName = "", PatientWhereAbout = "",
                        PI_ID = Guid.Empty, RegisterDoctorCode = "",
                        RegisterDoctorName = "", RegisterNo = "", RegisterTime = null, RetentionTime = null, Sex = "",
                        Sort = 0, SupplementaryNotes = "",
                        TriageDeptCode = "", TriageDeptName = "", TriageDirectionCode = "", TriageDirectionName = "",
                        TriageErrorFlag = false, TriageLevel = "",
                        TriageLevelName = "", TriageTime = DateTime.Now, TriageUserCode = "", TriageUserName = "",
                        TypeOfVisitCode = "", TypeOfVisitName = "",
                        VisitDate = DateTime.Now, VisitNo = "", VisitStatus = EVisitStatus.过号, Weight = ""
                    }
                },
                    OtherInfo = new List<OtherInfoDto>
                    { new OtherInfoDto { Barcode1 = "", QRCode1 = "", Barcode2 = "", QRCode2 = "" } }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("报表的Schema，方便用户配置异常：" + ex.Message);
                throw Oh.Error("报表的Schema，方便用户配置异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 根据预先配置号的分方Id获取各种单的数据
        /// </summary>
        /// <param name="piid"></param>
        /// <param name="comm">命令参数: 0=获取处方单,1=获取注射单,2=获取输液单,3=获取检验单,4=获取检查单,5=获取处置单,6=获取治疗单,7=获取物化单,8=获取预防接种单，9=麻药</param>
        /// <param name="platformType"></param> 
        /// <param name="separationId"></param>
        /// <param name="token"></param> 
        /// <param name="reprint">是否需要查看重复的打印记录</param>
        /// <param name="prescriptionNo"></param>
        /// <param name="templateId">模板id</param>
        /// <param name="forcePrintFlag">强制设置打印标识（前端控制是否已打标识）0：未打印</param>
        /// <returns></returns>     
        [NonUnify]
        public async Task<DocumentResponseDto> GetAsync(Guid piid, ECommandParam comm,
            EPlatformType platformType = EPlatformType.EmergencyTreatment,
            Guid? separationId = null, string token = "",
            int reprint = -1, string prescriptionNo = "", Guid templateId = default, int forcePrintFlag = -1)
        {
            try
            {
                var data = new DocumentResponseDto();
                var patientInfo = await GetPatientInfoAsync(piid, token);
                data.Push(patientInfo);
                switch (comm)
                {
                    case ECommandParam.GetMedicine: //处方单(药品)
                        var medicine = await GetMedicineV2Async(piid, platformType, reprint, prescriptionNo: prescriptionNo, templateId: templateId, forcePrintFlag: forcePrintFlag);
                        List<MedicineAdviceDto> medicineList = await medicine.ToListAsync();
                        medicineList.OrderBy(x => x.RecipeNo).ThenBy(x => x.RecipeGroupNo).ThenBy(x => x.ApplyTime).ToList();

                        //根据医生编码查询云签
                        medicineList.GroupBy(g => g.ApplyDoctorCode).ForEach(f =>
                        {
                            var stampBase = _hospitalClientAppService.QueryStampBaseAsync(f.Key);
                            if (!string.IsNullOrEmpty(stampBase.Result))
                            {
                                f.ForEach(ff => ff.StampBase = stampBase.Result);
                            }
                        });
                        await PushGroupMedicineAsync(data, medicineList);
                        return _hospitalAPI.SetCodeUrl(data);
                    case ECommandParam.GetInjection: //注射单(药品)
                    case ECommandParam.GetTransfusion: //输液单(药品)
                    case ECommandParam.GetAerosolization: //雾化单
                        if (separationId != null)
                        {
                            var usageList = await GetByUsageAsync(piid, platformType, separationId.Value, reprint, prescriptionNo, templateId: templateId, forcePrintFlag: forcePrintFlag);
                            if (ECommandParam.GetInjection == comm)
                            {
                                usageList = usageList.Where(x => x.SourceType != 1).ToList();
                            }

                            usageList.OrderBy(x => x.RecipeNo).ThenBy(x => x.RecipeGroupNo).ThenBy(x => x.ApplyTime).ToList();

                            //根据医生编码查询云签
                            usageList.GroupBy(g => g.ApplyDoctorCode).ForEach(f =>
                            {
                                var stampBase = _hospitalClientAppService.QueryStampBaseAsync(f.Key);
                                if (!string.IsNullOrEmpty(stampBase.Result))
                                {
                                    f.ForEach(ff => ff.StampBase = stampBase.Result);
                                }
                            });
                            //成组药品单独查询 精毒麻不在同一个组内
                            await PushGroupMedicineAsync(data, usageList, false);
                            return _hospitalAPI.SetCodeUrl(data);
                        }
                        break;
                    case ECommandParam.GetLaboratory: //检验单
                        var lab = await GetLaboratoryAsync(piid, platformType, reprint, prescriptionNo: prescriptionNo, templateId: templateId, forcePrintFlag: forcePrintFlag);
                        //根据医生编码查询云签
                        lab.GroupBy(g => g.ApplyDoctorCode).ForEach(f =>
                        {
                            var stampBase = _hospitalClientAppService.QueryStampBaseAsync(f.Key);
                            if (!string.IsNullOrEmpty(stampBase.Result))
                            {
                                f.ForEach(ff => ff.StampBase = stampBase.Result);
                            }
                        });
                        data.Push(lab);
                        await PushNovelCoronavirusRnaAsync(data, lab);
                        return _hospitalAPI.SetCodeUrl(data);
                    case ECommandParam.GetExamine: //检查单
                        List<PacsAdviceDto> pacs = await GetExamineAsync(piid, platformType, reprint, prescriptionNo: prescriptionNo, templateId: templateId, forcePrintFlag: forcePrintFlag);
                        string name = string.Join(';', pacs.Select(f => f.Name));
                        decimal amount = pacs.Sum(x => x.Amount);
                        foreach (PacsAdviceDto advice in pacs)
                        {
                            advice.Amount = amount;
                            advice.Name = name;
                        }

                        //根据医生编码查询云签
                        pacs.GroupBy(g => g.ApplyDoctorCode).ForEach(f =>
                        {
                            var stampBase = _hospitalClientAppService.QueryStampBaseAsync(f.Key);
                            if (!string.IsNullOrEmpty(stampBase.Result))
                            {
                                f.ForEach(ff => ff.StampBase = stampBase.Result);
                            }
                        });
                        data.Push(pacs);
                        return _hospitalAPI.SetCodeUrl(data);
                    case ECommandParam.GetTreat: //门诊处置治疗单
                        var treatList = await GetTreatAsync(piid, platformType, reprint, prescriptionNo: prescriptionNo, templateId: templateId, forcePrintFlag: forcePrintFlag);
                        //根据医生编码查询云签
                        treatList.GroupBy(g => g.ApplyDoctorCode).ForEach(f =>
                        {
                            var stampBase = _hospitalClientAppService.QueryStampBaseAsync(f.Key);
                            if (!string.IsNullOrEmpty(stampBase.Result))
                            {
                                f.ForEach(ff => ff.StampBase = stampBase.Result);
                            }
                        });
                        return _hospitalAPI.SetCodeUrl(data.Push(treatList));
                    case ECommandParam.GetAnesthetic://判断是否查询麻药单
                    case ECommandParam.GetPsychotropicDrugsII:
                        var toxicLevel = comm == ECommandParam.GetAnesthetic ? 2 : 3;
                        var medicineAnesthetic = await GetMedicineV2Async(piid, platformType, reprint, false, toxicLevel, prescriptionNo: prescriptionNo, templateId: templateId, forcePrintFlag: forcePrintFlag);
                        var medicineAnestheticList = await medicineAnesthetic.ToListAsync();
                        //根据医生编码查询云签
                        medicineAnestheticList.GroupBy(g => g.ApplyDoctorCode).ForEach(f =>
                        {
                            var stampBase = _hospitalClientAppService.QueryStampBaseAsync(f.Key);
                            if (!string.IsNullOrEmpty(stampBase.Result))
                            {
                                f.ForEach(ff => ff.StampBase = stampBase.Result);
                            }
                        });
                        await PushGroupMedicineAsync(data, medicineAnestheticList);
                        return _hospitalAPI.SetCodeUrl(data);
                    //case ECommandParam.GetPremunitive: //防疫接种单(药品) 
                    //  break; 
                    default:
                        break;
                }

                return new DocumentResponseDto();
            }
            catch (Exception ex)
            {
                _logger.LogError("根据预先配置号的分方Id获取各种单的数据异常：" + ex.Message);
                throw Oh.Error("根据预先配置号的分方Id获取各种单的数据异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 添加药品和成组的单
        /// </summary>
        /// <param name="data"></param>
        /// <param name="medicineList"></param>
        /// <param name="isToxicLevel"></param>
        private async Task PushGroupMedicineAsync(DocumentResponseDto data, List<MedicineAdviceDto> medicineList, bool isToxicLevel = true)
        {
            var groupList = new List<GroupMedicineDto>();
            var groupMedicine = medicineList.GroupBy(g => new
            {
                g.RecipeNo,
                ArgToxicLevel = isToxicLevel ? g.ToxicLevel : 0
            }).ToList();
            // 成组的分录ID
            int groupEntryId = 1;
            foreach (var item in groupMedicine)
            {
                groupList.Add(new GroupMedicineDto
                {
                    EntryId = groupEntryId++,
                    RecipeNo = item.Key.RecipeNo,
                    MedicineCount = item.Count(),
                    GroupAmount = item.Sum(g => g.Amount),
                });
            }
            // 药品的分录ID
            foreach (var item in medicineList)
            {
                item.EntryId = medicineList.IndexOf(item) + 1;
            }
            // 单据主要数据（业务稍微严谨一些，这应该跟患者信息、单据开具时间等组合在一起）
            var mainInfo = new MainInfoDto
            {
                TotalAmount = medicineList.Select(x => x.Amount).Sum(),
                GroupCount = groupList.Count(),
                MedicineCount = medicineList.Count(),
                ResultTime = medicineList.FirstOrDefault()?.ResultTime,
            };
            data.Push(medicineList);
            data.Push(groupList);
            data.Push(mainInfo);

            await Task.CompletedTask;
        }

        /// <summary>
        /// 检验单获取新冠rna申请单数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="lab"></param>
        /// <returns></returns>
        private async Task PushNovelCoronavirusRnaAsync(DocumentResponseDto data, List<LisAdviceDto> lab)
        {
            if (lab.Any(x => x.AddCard == "14"))
            {
                var ids = lab.Where(x => x.AddCard == "14").Select(s => s.Id);
                var novelCoronavirus = await (await _coronavirusRnaRepository.GetQueryableAsync())
                    .Where(t => ids.Contains(t.DoctorsAdviceId))
                    .ToListAsync();
                var mapNovel = ObjectMapper
                    .Map<List<NovelCoronavirusRna>, List<NovelCoronavirusRnaDto>>(novelCoronavirus);
                data.Push(mapNovel);
            }
        }

        /// <summary>
        /// 途径分类（separation.Code  0=注射单，皮试单，1=输液单，2=雾化单）
        /// </summary>
        /// <returns></returns> 
        private async Task<List<MedicineAdviceDto>> GetByUsageAsync(Guid piid, EPlatformType platformType,
            Guid separationId, int reprint = -1, string prescriptionNo = "", Guid templateId = default, int forcePrintFlag = -1)
        {
            var separation = await GetSeparationAsync(separationId);
            var usages = separation.Usages.Select(s => s.UsageCode).ToList();
            var list = await (await GetMedicineV2Async(piid, platformType, reprint, false, prescriptionNo: prescriptionNo, templateId: templateId, forcePrintFlag: forcePrintFlag))
                .WhereIf(separation != null, w => usages.Contains(w.UsageCode)).ToListAsync();
            return list;
        }

        /// <summary>
        /// 获取处方单
        /// </summary>
        /// <param name="piid"></param>
        /// <param name="platformType"></param>
        /// <param name="reprint"></param>
        /// <param name="isCommon"></param>
        /// <param name="toxicLevel"></param>
        /// <param name="prescriptionNo"></param>
        /// <param name="templateId"></param>
        /// <param name="forcePrintFlag">强制设置打印标识（前端控制是否已打标识），0：未打印</param>
        /// <returns></returns>
        private async Task<IQueryable<MedicineAdviceDto>> GetMedicineV2Async(Guid piid, EPlatformType platformType,
            int reprint = -1, bool isCommon = true, int toxicLevel = -1, string prescriptionNo = "", Guid templateId = default, int forcePrintFlag = -1)
        {
            var status = new List<ERecipeStatus>
                { ERecipeStatus.Submitted, ERecipeStatus.Confirmed, ERecipeStatus.Executed,ERecipeStatus.PayOff };
            //根据途径查询 
            var query = from a in (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w =>
                    w.PIID == piid && w.ItemType == EDoctorsAdviceItemType.Prescribe && status.Contains(w.Status) &&
                    w.PlatformType == platformType)
                        join p in (await _prescribeRepository.GetQueryableAsync())
                            on a.Id equals p.DoctorsAdviceId
                        join t in (await _toxicRepository.GetQueryableAsync()).WhereIf(isCommon, x => x.ToxicLevel != 2 && x.ToxicLevel != 3)
                                .WhereIf(toxicLevel != -1, x => x.ToxicLevel == toxicLevel).DefaultIfEmpty()//TODO 排除麻醉单
                            on p.MedicineId equals t.MedicineId into s
                        from t in s.DefaultIfEmpty()
                        join m in (await _medDetailResultRepository.GetQueryableAsync())
                                .WhereIf(!string.IsNullOrEmpty(prescriptionNo), x => prescriptionNo.Contains(x.HisNumber))
                            on a.PrescriptionNo equals m.ChannelNumber
                        join i in (await _printInfoRepository.GetQueryableAsync()).WhereIf(templateId != Guid.Empty, x => x.TemplateId == templateId)
                            on m.ChannelNumber equals i.PrescriptionNo into print
                        from i in print.DefaultIfEmpty()
                        orderby a.RecipeNo, a.RecipeGroupNo, p.PharmacyCode
                        select new MedicineAdviceDto
                        {
                            #region list-data
                            Id = a.Id,
                            PIID = a.PIID,
                            Code = a.Code,
                            Name = a.Name,
                            CategoryCode = a.CategoryCode,
                            CategoryName = a.CategoryName,
                            IsBackTracking = a.IsBackTracking,
                            PrescriptionNo = m.HisNumber,//取医院的处方单号
                            RecipeNo = a.RecipeNo,
                            RecipeGroupNo = a.RecipeGroupNo,
                            ApplyTime = a.ApplyTime,
                            ApplyTimeToString = a.ApplyTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            ApplyDoctorCode = a.ApplyDoctorCode,
                            ApplyDoctorName = a.ApplyDoctorName,
                            ApplyDeptCode = a.ApplyDeptCode,
                            ApplyDeptName = a.ApplyDeptName,
                            Status = a.Status,
                            PayType = a.PayType,
                            PayTypeCode = a.PayTypeCode,
                            Unit = a.Unit,
                            Price = a.Price,
                            Amount = a.Amount,
                            InsuranceCode = a.InsuranceCode,
                            InsuranceType = a.InsuranceType,
                            IsChronicDisease = a.IsChronicDisease,
                            HisOrderNo = a.HisOrderNo,
                            Diagnosis = a.Diagnosis,
                            PrescribeTypeCode = a.PrescribeTypeCode,
                            PrescribeTypeName = a.PrescribeTypeName,
                            RecieveQty = a.RecieveQty,
                            RecieveUnit = a.RecieveUnit,
                            Remarks = a.Remarks,
                            IsRecipePrinted = a.IsRecipePrinted,
                            SourceType = a.SourceType,
                            MedicineProperty = p.MedicineProperty,
                            ToxicProperty = p.ToxicProperty,
                            UsageCode = p.UsageCode,
                            UsageName = p.UsageName,
                            Speed = p.Speed,
                            LongDays = p.LongDays,
                            ActualDays = p.ActualDays,
                            DosageQty = p.DosageQty,
                            QtyPerTimes = p.QtyPerTimes,
                            DosageUnit = p.DosageUnit,
                            FrequencyCode = p.FrequencyCode,
                            FrequencyName = p.FrequencyName,
                            FrequencyTimes = p.FrequencyTimes,
                            FrequencyUnit = p.FrequencyUnit,
                            FrequencyExecDayTimes = p.FrequencyExecDayTimes,
                            PharmacyCode = p.PharmacyCode,
                            PharmacyName = p.PharmacyName,
                            FactoryCode = p.FactoryCode,
                            FactoryName = p.FactoryName,
                            BatchNo = p.BatchNo,
                            IsSkinTest = p.IsSkinTest,
                            SkinTestResult = p.SkinTestResult,
                            MaterialPrice = p.MaterialPrice,
                            IsAdaptationDisease = p.IsAdaptationDisease,
                            IsFirstAid = p.IsFirstAid,
                            AntibioticPermission = p.AntibioticPermission,
                            PrescriptionPermission = p.PrescriptionPermission,
                            Specification = p.Specification,
                            LimitType = p.LimitType,
                            RestrictedDrugs = p.RestrictedDrugs,
                            SkinTestSignChoseResult = p.SkinTestSignChoseResult,
                            ExecDeptCode = a.ExecDeptCode,
                            ExecDeptName = a.ExecDeptName,
                            ExecTime = a.ExecTime,
                            ExecutorCode = a.ExecutorCode,
                            ExecutorName = a.ExecutorName,
                            Lgzxyy_payurl = m.LgzxyyPayurl,
                            Lgjkzx_payurl = m.LgjkzxPayurl,
                            MedNature = m.MedNature,
                            MedFee = m.MedFee,
                            ChannelNo = m.ChannelNo,
                            ChannelNumber = m.ChannelNumber,
                            HisNumber = m.HisNumber,
                            MedicalNo = m.MedicalNo,
                            MedType = m.MedType,
                            ToxicLevel = t != null ? t.ToxicLevel ?? 0 : 0,
                            ResultTime = m.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            PrintStr = (forcePrintFlag != 0 && i != null ?
                                "报销处方" :
                                (t.ToxicLevel == 2 ?
                                "麻、精一" :
                                (t.ToxicLevel == 3 ?
                                "精二" :
                                "急诊处方")
                                )),
                            CommitSerialNo = a.CommitSerialNo,
                            IsCriticalPrescription = p.IsCriticalPrescription
                            #endregion
                        };
            return await Task.FromResult(query.WhereIf(reprint == 1, x => x.PrintStr != "").WhereIf(reprint == 0, x => x.PrintStr == "").AsQueryable());
        }

        /// <summary>
        /// 获取指定分方目录下的用药途径
        /// </summary>
        /// <param name="separationId"></param>
        /// <returns></returns>
        public async Task<SeparationDto> GetSeparationAsync(Guid separationId)
        {
            var id = new SeparationRequest() { SeparationId = separationId.ToString() };
            var model = await _grpcMasterDataClient.GetSeparationByIdAsync(id);
            if (model == null || model.Usages == null) Oh.Error("没有你要查询的分方数据");

            var data = new SeparationDto
            {
                Id = Guid.Parse(model.Id),
                Title = model.Title,
                Code = model.Code,
                Usages = model.Usages.Select(s => new UsageDto
                {
                    Id = Guid.Parse(s.Id),
                    UsageCode = s.UsageCode,
                    UsageName = s.UsageName
                }).ToList()
            };

            return data;
        }

        /// <summary>
        /// 获取用于FastReport打印模板设计的xml文件
        /// </summary>
        /// <param name="piid"></param>
        /// <param name="comm"></param>
        /// <param name="platformType"></param>
        /// <param name="separationId"></param>
        /// <param name="token"></param>
        /// <param name="reprint"></param>
        /// <param name="prescriptionNo"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        [NonUnify]
        public async Task<string> GetXmlAsync(Guid piid, ECommandParam comm,
            EPlatformType platformType = EPlatformType.EmergencyTreatment,
            Guid? separationId = null, string token = "",
            int reprint = -1, string prescriptionNo = "", Guid templateId = default)
        {
            try
            {
                DocumentResponseDto data = await GetAsync(piid, comm, platformType, separationId, token, reprint, prescriptionNo, templateId);
                string xsdAndXmlString = FastReportUtils.GetXmlSchemalAndDataString(data);

                return xsdAndXmlString;
            }
            catch (Exception ex)
            {
                _logger.LogError("获取用于FastReport打印模板设计的xml文件异常：" + ex.Message);
                throw Oh.Error("获取用于FastReport打印模板设计的xml文件异常：" + ex.Message);
            }
        }
    }
}