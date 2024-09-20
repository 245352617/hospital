using DotNetCore.CAP;
using MasterDataService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using YiJian.DoctorsAdvices.Dto;
using YiJian.DoctorsAdvices.Entities;
using YiJian.DoctorsAdvices.Enums;
using YiJian.Documents.Dto;
using YiJian.ECIS.ShareModel.DDPs;
using YiJian.ECIS.ShareModel.DDPs.Dto;
using YiJian.ECIS.ShareModel.DDPs.Requests;
using YiJian.ECIS.ShareModel.DDPs.Responses;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.ShareModel.Utils;
using YiJian.Hospitals.Dto;
using YiJian.Hospitals.Enums;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.DoctorsAdvices;

/// <summary>
/// 医嘱(订阅部分内容)
/// </summary> 
public partial class DoctorsAdviceAppService
{
    private const string DEPT_CODE = "280";
    private const string DEPT_NAME = "急诊科";

    /// <summary>
    /// 同步药理数据
    /// </summary>
    /// <param name="eto"></param>
    /// <returns></returns>
    [CapSubscribe("sync.masterdata.toxic.all")]
    public async Task SyncToxicAsync(List<ToxicEto> eto)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            var insertToxics = new List<Toxic>();
            var updateToxics = new List<Toxic>();

            var toxics = await (await _toxicRepository.GetQueryableAsync()).AsTracking().ToListAsync();

            //没有记录时，全部新增
            if (!toxics.Any())
            {
                var entities = eto.Select(s => new Toxic(GuidGenerator.Create(), s.MedicineId, s.IsSkinTest, s.IsCompound, s.IsDrunk, s.ToxicLevel,
                    s.IsHighRisk, s.IsRefrigerated, s.IsTumour, s.AntibioticLevel, s.IsPrecious, s.IsInsulin,
                    s.IsAnaleptic, s.IsAllergyTest, s.IsLimited, s.LimitedNote)).ToList();
                await _toxicRepository.InsertManyAsync(entities);
            }
            else
            {
                //有更新，有新增
                foreach (var item in eto)
                {
                    var entity = toxics.FirstOrDefault(w => w.MedicineId == item.MedicineId);
                    if (entity == null)
                    {
                        //new toxic
                        var newToxic = eto.FirstOrDefault(w => w.MedicineId == item.MedicineId);
                        var model = new Toxic(GuidGenerator.Create(), newToxic.MedicineId, newToxic.IsSkinTest, newToxic.IsCompound, newToxic.IsDrunk, newToxic.ToxicLevel,
                                            newToxic.IsHighRisk, newToxic.IsRefrigerated, newToxic.IsTumour, newToxic.AntibioticLevel, newToxic.IsPrecious, newToxic.IsInsulin,
                                            newToxic.IsAnaleptic, newToxic.IsAllergyTest, newToxic.IsLimited, newToxic.LimitedNote);

                        insertToxics.Add(model);
                    }
                    else
                    {
                        var updateToxic = eto.FirstOrDefault(w => w.MedicineId == item.MedicineId);
                        //update toxic
                        entity.Update(
                            isSkinTest: updateToxic.IsSkinTest,
                            isCompound: updateToxic.IsCompound,
                            isDrunk: updateToxic.IsDrunk,
                            toxicLevel: updateToxic.ToxicLevel,
                            isHighRisk: updateToxic.IsHighRisk,
                            isRefrigerated: updateToxic.IsRefrigerated,
                            isTumour: updateToxic.IsTumour,
                            antibioticLevel: updateToxic.AntibioticLevel,
                            isPrecious: updateToxic.IsPrecious,
                            isInsulin: updateToxic.IsInsulin,
                            isAnaleptic: updateToxic.IsAnaleptic,
                            isAllergyTest: updateToxic.IsAllergyTest,
                            isLimited: updateToxic.IsLimited,
                            limitedNote: updateToxic.LimitedNote);

                        updateToxics.Add(entity);
                    }
                }
            }

            if (insertToxics.Any()) await _toxicRepository.InsertManyAsync(insertToxics);
            if (updateToxics.Any()) await _toxicRepository.UpdateManyAsync(updateToxics);

            await uow.CompleteAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"同步药理数据异常:{ex.Message},请求参数：{JsonSerializer.Serialize(eto)}");
            await uow.RollbackAsync();
            throw;
        }
    }


    /// <summary>
    /// 已驳回,已确认,已执行... 
    /// </summary>
    /// <see cref="EDoctorsAdviceStatus"/>
    /// <returns></returns>
    [CapSubscribe("syncadvice.nursingservice.to.recipeservice")]
    public async Task SyncDoctorsAdviceAsync(SyncDoctorsAdviceEto eto)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            ERecipeStatus status = (ERecipeStatus)((int)eto.DoctorsAdviceStatus);
            List<ERecipeStatus> canUpdateStatus = new List<ERecipeStatus>() { ERecipeStatus.Rejected, ERecipeStatus.Confirmed, ERecipeStatus.Executed, ERecipeStatus.Submitted };
            if (!canUpdateStatus.Contains(status)) Oh.Error("您反馈的执行状态不在我们预设范围内，暂时不允许操作");

            List<DoctorsAdvice> doctorsAdvices = await _doctorsAdviceRepository.GetListAsync(w => eto.Ids.Contains(w.Id));
            foreach (DoctorsAdvice doctorsAdvice in doctorsAdvices)
            {
                doctorsAdvice.UpdateStatus(status);
            }

            if (eto.DoctorsAdviceStatus == EDoctorsAdviceStatus.Executed)
            {
                foreach (var entity in doctorsAdvices)
                {
                    entity.Exec(eto.OperationCode, eto.OperationName, eto.OperationTime);
                }
            }

            await _doctorsAdviceRepository.UpdateManyAsync(doctorsAdvices);

            var audits = new List<DoctorsAdviceAudit>();

            foreach (var id in eto.Ids)
            {
                var audit = new DoctorsAdviceAudit(GuidGenerator.Create(), status, eto.OperationCode, eto.OperationName, eto.OperationTime, EOriginType.Nurse, id);
                audits.Add(audit);
            }

            await _doctorsAdviceAuditRepository.InsertManyAsync(audits);

            //驳回响应 ,执行响应,确认响应  
            var replyEto = new DoctorsAdviceReplyEto<SyncDoctorsAdviceEto>()
            {
                Data = eto
            };
            //_capPublisher.Publish("reply.syncadvice.recipeservice.to.nursingservice", replyEto);

            await uow.CompleteAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"订阅护理服务的驳回功能异常:{ex.Message},请求参数：{JsonSerializer.Serialize(eto)}");
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 创建院内会诊诊疗记录
    /// </summary>
    /// <param name="eto"></param>
    /// <returns></returns>
    [CapSubscribe("create.consultation.treat")]
    public async Task CreateConsultationTreatAsync(ConsultationRecordTreatEto eto)
    {
        using var uow = UnitOfWorkManager.Begin();
        try
        {
            var req = new GetTreatByCodeRequest() { Code = _configuration["Consultation"] };
            var res = await _grpcMasterDataClient.GetTreatByCodeAsync(req);
            if (res == null) return;
            var treat = new AddTreatDto()
            {
                IsAddition = res.Additional,
                PatientInfo = new PatientInfoDto(),
                DoctorsAdvice = new ModifyDoctorsAdviceBaseDto
                {
                    ApplyDeptCode = eto.DeptCode,
                    ApplyDeptName = eto.DeptName,
                    ApplyDoctorCode = eto.DoctorCode,
                    ApplyDoctorName = eto.DoctorName,
                    Diagnosis = eto.Diagnosis,//诊断内容
                    IsChronicDisease = false,
                    ItemType = EDoctorsAdviceItemType.Treat,
                    PatientId = eto.PatientNo,
                    PatientName = eto.PatientName,
                    PIID = eto.PI_ID,
                    PlatformType = EPlatformType.EmergencyTreatment,
                    PrescribeTypeCode = "PrescribeTemp",
                    PrescribeTypeName = "临",
                    TraineeCode = "",
                    TraineeName = ""
                },
                Items = new TreatDto
                {
                    Additional = res.Additional,
                    PositionCode = "",
                    PositionName = "",
                    PayType = 0,
                    PayTypeCode = "",
                    ApplyTime = DateTime.Now,
                    CategoryCode = "Treat",//res.CategoryCode,
                    CategoryName = "处置",//res.CategoryName,
                    Code = res.Code,
                    ExecDeptCode = eto.DeptCode,
                    ExecDeptName = eto.DeptName,
                    FrequencyCode = "",
                    FrequencyName = "",
                    FeeTypeMainCode = "",
                    FeeTypeSubCode = "",
                    HisOrderNo = "",
                    InsuranceCode = "",
                    InsuranceType = EInsuranceCatalog.Self,
                    RecieveQty = 1,
                    RecieveUnit = res.Unit,
                    Unit = res.Unit,
                    Specification = res.Specification,
                    WbCode = res.WbCode,
                    PyCode = res.PyCode,
                    RecipeNo = "",
                    RecipeGroupNo = 1,
                    Remarks = "系统开的院内会诊诊疗记录",
                    ProjectMerge = res.ProjectMerge,
                    LongDays = 1,
                    Name = res.Name,
                    Price = (decimal)res.Price,
                    OtherPrice = (decimal)res.OtherPrice,
                    IsBackTracking = false,
                    PrescribeTypeCode = "PrescribeTemp",
                    PrescribeTypeName = "临",
                    intTreatId = res.Id,
                    IsRecipePrinted = false,
                    ProjectName = res.ProjectTypeName,
                    ProjectType = res.ProjectType,
                    AdditionalItemsType = EAdditionalItemType.No,
                }
            };  //构建 AddTreatDto
            _ = await AddTreatAsync(treat);

            await uow.CompleteAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"订阅护理服务的驳回功能异常:{ex.Message},请求参数：{JsonSerializer.Serialize(eto)}");
            await uow.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// 更新医嘱缴费状态
    /// </summary>
    /// <returns></returns>
    private async Task UpdateRecipeBillStatusAsync(IEnumerable<DoctorsAdvice> doctorsAdvices)
    {
        try
        {
            IEnumerable<Guid> doctorsAdviceIds = doctorsAdvices.Select(x => x.Id);
            List<Prescription> prescriptions = await _prescriptionRepository.GetListAsync(x => doctorsAdviceIds.Contains(x.DoctorsAdviceId));

            IEnumerable<Task> synchronousTasks = doctorsAdvices.Select(x => SynchronousRecipeStatusAsync(x, prescriptions));

            await Task.WhenAll(synchronousTasks);
            await _doctorsAdviceRepository.UpdateManyAsync(doctorsAdvices);
            await _prescriptionRepository.UpdateManyAsync(prescriptions);

            List<DoctorsAdvice> havePays = doctorsAdvices.Where(x => x.PayStatus == EPayStatus.HavePaid).ToList();

            _logger.LogDebug("推送HIS返回的状态到护理服务:：" + Newtonsoft.Json.JsonConvert.SerializeObject(havePays));
            SendAdviceHisStatus(havePays);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "同步医嘱状态失败");
        }
    }

    /// <summary>
    /// 同步医嘱状态
    /// </summary>
    /// <param name="doctorsAdvice"></param>
    /// <param name="prescriptions"></param>
    /// <returns></returns>
    private async Task SynchronousRecipeStatusAsync(DoctorsAdvice doctorsAdvice, List<Prescription> prescriptions)
    {
        PKUQueryRecipeStatusRequest req = new PKUQueryRecipeStatusRequest()
        {
            RecipeType = doctorsAdvice.CategoryCode == "Medicine" ? 1 : 2,
            HisOrderNo = doctorsAdvice.HisOrderNo
        };

        DdpBaseResponse<List<PKUQueryRecipeStatusResponse>> resp = await _ddpApiService.QueryHisRecipeStatusAsync(req);
        if (resp.Code == 500 || resp.Data == null || !resp.Data.Any())
        {
            _logger.LogError($"同步医嘱缴费状态异常{resp.Msg}");
            return;
        }
        Prescription prescription = prescriptions.First(x => x.DoctorsAdviceId == doctorsAdvice.Id);

        PKUQueryRecipeStatusResponse hisRecipeStatus = resp.Data.First();
        float.TryParse(hisRecipeStatus.Status, out float status);
        int hisStatus = (int)status;
        if (hisStatus == 1)
        {
            doctorsAdvice.PayStatus = EPayStatus.HavePaid;
            doctorsAdvice.InvoiceNo = hisRecipeStatus.InvoiceNo;
            doctorsAdvice.Status = ERecipeStatus.PayOff;
            prescription.BillState = 1;
        }

        if (hisStatus == 4)
        {
            doctorsAdvice.Status = ERecipeStatus.Cancelled;
            prescription.BillState = -1;
        }

        if (hisStatus == 6)
        {
            doctorsAdvice.PayStatus = EPayStatus.HaveRefund;
            doctorsAdvice.Status = ERecipeStatus.ReturnPremium;
            prescription.BillState = 0;
        }
    }

    /// <summary>
    /// 处理医嘱信息回传
    /// </summary>
    /// <returns></returns> 
    //[CapSubscribe("self.call.sendMedicalInfo")] //放弃异步，用同步调用
    private async Task<List<PushDoctorsAdviceModel>> CallSendMedicalInfoAsync(SendMedicalInfoEto eto, bool isChildren = false)
    {
        //using var uow = UnitOfWorkManager.Begin();
        try
        {
            List<PushDoctorsAdviceModel> response = new();

            var baseinfo = eto.BaseInfo;
            var group = eto.AdviceGroup; //根据分方的单号分组

            var list = new List<MedDetailResult>();
            var prescriptionNos = eto.AdviceGroup.Select(s => s.Key).ToList();
            var prescriptions = await (await _prescriptionRepository.GetQueryableAsync()).Where(w => prescriptionNos.Contains(w.MyPrescriptionNo)).ToListAsync();

            var request = new SendMedicalInfoRequest(
                    visSerialNo: baseinfo.VisSerialNo,
                    patientId: baseinfo.PatientId,
                    doctorCode: baseinfo.OperatorCode,
                    doctorName: baseinfo.OperatorName,
                    deptCode: baseinfo.DeptCode, //"1031",  
                    deptName: baseinfo.DeptName //"儿科内科门诊" //
                    );

            var updateAdviceList = new List<DoctorsAdvice>();

            //异步处理，一个请求只处理一个方子
            foreach (var item in group)
            {
                //string prescriptionNo = item.Key; 
                var ids = item.Value.Select(s => s.Id).ToList(); //同一个方子的医嘱id  
                if (!item.Value.Any()) continue;
                var itemtype = item.Value.FirstOrDefault().ItemType;

                var advices = await (await _doctorsAdviceRepository.GetQueryableAsync())
                            .Where(w => ids.Contains(w.Id))
                            .ToListAsync();

                var adviceFirst = advices.FirstOrDefault(w => w.PrescriptionNo == item.Key);
                if (adviceFirst == null) continue;

                //根据方子分好组之后一个方子内的属性分类就必须一样了，处方，检查，检验，诊疗 都是只能分开提交了
                switch (itemtype)
                {
                    case EDoctorsAdviceItemType.Prescribe:  //药品

                        #region 药品

                        var prescribe = await (await _prescribeRepository.GetQueryableAsync())
                            .Where(w => ids.Contains(w.DoctorsAdviceId))
                            .ToListAsync();

                        var medids = prescribe.Select(s => s.MedicineId).Distinct().ToList();
                        var toxics = (await _toxicRepository.GetQueryableAsync()).Where(w => medids.Contains(w.MedicineId)); //1.GRPC 调用HIS的视图 

                        if (!prescribe.Any()) continue;

                        var drugStockQueries = await (await _drugStockQueryRepository.GetQueryableAsync()).Where(w => ids.Contains(w.DoctorsAdviceId)).ToListAsync();

                        var items = new List<DrugItemRequest>();

                        List<ChargeDetailRequest> chargeDetails = new();

                        int counter = 1; //同一个处方单号的计数器

                        Dictionary<string, int> dic = new Dictionary<string, int>();

                        bool containsKeyRecipeNo = false;
                        foreach (var pres in prescribe)
                        {
                            var advice = advices.FirstOrDefault(w => pres.DoctorsAdviceId == w.Id);
                            if (advice == null) continue;

                            //如果是同一组则字号相同，否则顺序递增
                            if (!dic.ContainsKey(advice.RecipeNo))
                            {
                                dic.Add(advice.RecipeNo, counter);
                                advice.SequenceNo = counter; //设置一个序号
                                containsKeyRecipeNo = false;
                            }
                            else
                            {
                                advice.SequenceNo = dic[advice.RecipeNo]; //存在该组之后，序号设为上一个同组的序号
                                containsKeyRecipeNo = true;
                            }

                            updateAdviceList.Add(advice);

                            var drugStock = drugStockQueries.OrderByDescending(o => o.Id).FirstOrDefault(w => w.DoctorsAdviceId == pres.DoctorsAdviceId);

                            /*  4.4.12 药品信息查询（his提供）
                                drugAttributes = 7时必传
                                第一针(2次):1
                                第二针:2
                                第三针:3
                                第四针:7
                                第五针:8
                             */
                            var needleTimes = 0;
                            var toxic = await toxics.FirstOrDefaultAsync(w => pres.MedicineId == w.MedicineId);
                            if (toxic != null && toxic.ToxicLevel.HasValue && toxic.ToxicLevel == 7)
                            {
                                //TODO 需要填充疫苗次数
                                var immunizationRecord = await _iImmunizationRecordRepository.GetByAdviceIdAsync(pres.DoctorsAdviceId);
                                if (immunizationRecord != null)
                                {
                                    needleTimes = immunizationRecord.Times;
                                    //needleTimes = Transform(immunizationRecord.AcupunctureManipulation, immunizationRecord.Times); 
                                    _ = await _iImmunizationRecordRepository.ConfirmedAsync(immunizationRecord.Id); //使用完之后设置为已提交
                                }
                            }

                            chargeDetails.Add(new ChargeDetailRequest
                            {
                                ChargeDetailNo = advice.DetailId.ToString(), //His需要的int类型的候选键
                                DailyFrequency = pres.FrequencyTimes.HasValue ? pres.FrequencyTimes.Value.ToString() : "1", //pres.DailyFrequency,
                                DrugChannel = pres.UsageCode,
                                DrugCode = drugStock != null ? drugStock.DrugCode : advice.Code,
                                //DrugGroupNo = advice.RecipeNo,
                                DrugGroupNo = $"{advice.SequenceNo}", //每一个处方单的序列号，从1开始，顺序增加
                                DrugName = drugStock != null ? drugStock.DrugName : advice.Name,
                                DrugPrice = decimal.Parse(advice.Price.ToString("F4")),
                                DrugQuantity = advice.RecieveQty,
                                DrugTotamount = decimal.Parse((advice.Price * advice.RecieveQty).ToString("F2")),//医院要求四舍五入
                                DrugUsageDic = pres.DailyFrequency, //pres.FrequencyCode,
                                FirmID = drugStock != null ? drugStock.FirmID : pres.FactoryCode,
                                NeedleTimes = needleTimes,
                                PackageAmount = drugStock != null ? drugStock.PackageAmount.ToString() : "",
                                PharmUnit = drugStock != null ? drugStock.MinPackageUnit : pres.HisDosageUnit,
                                PharSpec = drugStock != null ? drugStock.PharSpec : "",
                                //PrimaryDose = pres.DosageQty.ToString(),//每次剂量
                                PrimaryDose = pres.CommitHisDosageQty == 0 ? pres.DosageQty.ToString() : pres.CommitHisDosageQty.ToString(),//每次剂量
                                Remarks = advice.Remarks,
                                RestrictedDrugs = pres.RestrictedDrugs,
                                SkinTest = (pres.SkinTestSignChoseResult.HasValue && pres.SkinTestSignChoseResult == ESkinTestSignChoseResult.Yes) ? 1 : 0,
                                PrescriptionDays = pres.LongDays, //用药天数 longdays
                            });

                            //如果是同一组则字号相同，否则顺序递增
                            if (!containsKeyRecipeNo) counter = counter + 1;

                        }

                        var pre = prescribe.FirstOrDefault(w => w.DoctorsAdviceId == adviceFirst.Id);
                        if (pre == null) continue;

                        var prescriptionType = EPrescriptionType.PutongChufang;
                        if (pre.IsCriticalPrescription)
                        {
                            prescriptionType = EPrescriptionType.WeijiChufang;
                        }
                        if (await toxics.AnyAsync())
                        {
                            var toxic = await toxics.FirstOrDefaultAsync(w => w.MedicineId == pre.MedicineId);
                            if (toxic != null)
                            {
                                switch (toxic.ToxicLevel)
                                {
                                    case 1:
                                    case 2:
                                        prescriptionType = EPrescriptionType.MazuiChufang;
                                        break;
                                    case 3:
                                        prescriptionType = EPrescriptionType.JingshenChufang;
                                        break;
                                    case 0:
                                    case 4:
                                    case 5:
                                    case 6:
                                    case 7:
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                        var drugItemRequest = new DrugItemRequest
                        {
                            ChargeDetail = chargeDetails,
                            DrugAdministration = "", //drugtype = 2 必填，否则非必填
                            DrugDecoct = "", //DOTO 不开drugType=2 的，待定
                            DrugType = EDrugType.XiYao, //TODO 需要对药品判断 medicineproperty
                            PrescriptionDate = adviceFirst.ApplyTime.ToString("yyyy-MM-dd HH:mm:ss"), //item.ApplyTime.ToString("yyyy-MM-dd HH:mm:ss"), 
                            PrescriptionNo = adviceFirst.PrescriptionNo.ToString(),
                            PrescriptionType = prescriptionType, //prescriptionType, //TODO 需要对药物判断 
                            Storage = pre.PharmacyCode
                        };

                        if (prescriptionType == EPrescriptionType.MazuiChufang)
                        {
                            var agencyPeople = eto.BaseInfo.AgencyPeople;

                            //prescriptionType = 3（麻醉处方）、代办人姓名必填 
                            //prescriptionType = 3（麻醉处方）、代办人证件必填
                            if (agencyPeople == null) Oh.Error("您开的药品为麻醉处方,代办人信息必填。");
                            if (string.IsNullOrWhiteSpace(agencyPeople.AgencyPeopleName) || string.IsNullOrWhiteSpace(agencyPeople.AgencyPeopleCard))
                            {
                                Oh.Error("麻醉处方，代办人姓名必和代办人证件必填");
                            }

                            //麻醉处方之类的需要代办人
                            #region 代办人信息

                            drugItemRequest.AgencyPeopleAge = agencyPeople.AgencyPeopleAge;
                            drugItemRequest.AgencyPeopleCard = agencyPeople.AgencyPeopleCard;
                            drugItemRequest.AgencyPeopleMobile = agencyPeople.AgencyPeopleMobile;
                            drugItemRequest.AgencyPeopleName = agencyPeople.AgencyPeopleName;
                            drugItemRequest.AgencyPeopleSex = agencyPeople.AgencyPeopleSex;

                            #endregion
                        }

                        items.Add(drugItemRequest);

                        request.AddDrugItems(items);

                        #endregion

                        break;
                    case EDoctorsAdviceItemType.Pacs: //检查

                        #region 检查
                        var pacses = await (await _pacsRepository.GetQueryableAsync())
                            .Include(i => i.PacsItems)
                            .Where(w => ids.Contains(w.DoctorsAdviceId))
                            .ToListAsync();

                        if (!pacses.Any()) continue;

                        var pacsItem = new List<ProjectListRequest>();
                        var projectPacsItem = new List<ProjectItemRequest>();
                        var pacsDetail = new List<ProjectDetailRequest>();
                        ProjectItemRequest projectPacsItemRequest = null;

                        var pacsTargets = new List<Guid>();
                        pacses.ForEach(x =>
                        {
                            x.PacsItems.ForEach(y =>
                            {
                                pacsTargets.Add(y.Id);
                            });
                        });

                        var pmtm = await (await _medicalTechnologyMapRepository.GetQueryableAsync()).Where(w => pacsTargets.Contains(w.LPTId)).ToListAsync();

                        foreach (var pacs in pacses)
                        {
                            var advice = advices.FirstOrDefault(w => pacs.DoctorsAdviceId == w.Id);
                            if (advice == null) continue;

                            foreach (var s in pacs.PacsItems)
                            {
                                var projectDetailNo = pmtm.FirstOrDefault(w => s.Id == w.LPTId);

                                var target = new ProjectDetailRequest
                                {
                                    ProjectDetailNo = projectDetailNo.Id.ToString(),
                                    GroupsId = s.TargetCode, //TargetCode 
                                    ProjectMain = "1",  //0
                                    ProjectMerge = s.ProjectMerge,
                                    ProjectName = /*advice.Name,*/ s.TargetName,
                                    ProjectPrice = Math.Round(s.Price, 2), //医院要求四舍五入两位
                                    ProjectQuantity = s.Qty, //qty
                                    ProjectTotamount = Math.Round(s.Price * s.Qty, 2), //ProjectPrice*ProjectQuantity
                                    ProjectType = s.ProjectType,
                                    Remarks = advice.Remarks,
                                    RestrictedDrugs = ERestrictedDrugs.Default, //TODO  
                                    ContainerId = "", //TODO
                                    GroupId = advice.Code, //TODO
                                    SpecimenNo = "", //TODO  
                                };

                                pacsDetail.Add(target);
                            }

                            if (projectPacsItemRequest == null)
                            {
                                projectPacsItemRequest = new ProjectItemRequest
                                {
                                    BillingDate = advice.ApplyTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    EmergencySign = pacs.IsEmergency ? 1 : 0,
                                    ExecuteDeptCode = advice.ExecDeptCode,
                                    ProjectItemNo = advice.PrescriptionNo.ToString(),
                                    SpecialType = 0,
                                };
                            }
                        }

                        if (projectPacsItemRequest == null) continue;
                        projectPacsItemRequest.ProjectDetail = pacsDetail;
                        projectPacsItem.Add(projectPacsItemRequest);

                        var projectPe = ""; //203 体格检查 必填
                        var projectPhl = ""; //203 现病史 必填

                        var patientCase = await _patientCaseRepository.GetPatientCaseAsync(eto.BaseInfo.Piid);
                        if (patientCase != null)
                        {
                            projectPe = patientCase.Physicalexamination.Length > 250 ? patientCase.Physicalexamination.Substring(0, 250) : patientCase.Physicalexamination;
                            projectPhl = patientCase.Presentmedicalhistory.Length > 250 ? patientCase.Presentmedicalhistory.Substring(0, 250) : patientCase.Presentmedicalhistory;
                        }

                        pacsItem.Add(new ProjectListRequest
                        {
                            GroupType = "203",
                            ProjectItem = projectPacsItem,
                            ProjectPe = projectPe, //203 体格检查 必填
                            ProjectPhl = projectPhl, //203 现病史 必填
                        });

                        request.AddProjectItems(pacsItem);

                        #endregion

                        break;
                    case EDoctorsAdviceItemType.Lis: //检验

                        #region 检验

                        var lises = await (await _lisRepository.GetQueryableAsync())
                            .Include(i => i.LisItems)
                            .Where(w => ids.Contains(w.DoctorsAdviceId)).ToListAsync();

                        if (!lises.Any()) continue;

                        var lisItem = new List<ProjectListRequest>();
                        var projectListItem = new List<ProjectItemRequest>();
                        ProjectItemRequest projectLisItemRequest = null;
                        var lisDetail = new List<ProjectDetailRequest>();
                        var lisTargets = new List<Guid>();
                        lises.ForEach(x =>
                        {
                            x.LisItems.ForEach(y =>
                            {
                                lisTargets.Add(y.Id);
                            });
                        });

                        var lmtm = await (await _medicalTechnologyMapRepository.GetQueryableAsync()).Where(w => lisTargets.Contains(w.LPTId)).ToListAsync();

                        foreach (var lis in lises)
                        {
                            var advice = advices.FirstOrDefault(w => lis.DoctorsAdviceId == w.Id);
                            if (advice == null) continue;

                            //设置ProjectDetail

                            foreach (var s in lis.LisItems)
                            {
                                var projectDetailNo = lmtm.FirstOrDefault(w => s.Id == w.LPTId);
                                var target = new ProjectDetailRequest
                                {
                                    ProjectDetailNo = projectDetailNo.Id.ToString(),
                                    GroupsId = s.TargetCode,//TargetCode 
                                    ProjectMain = "1",
                                    ProjectMerge = s.ProjectMerge,
                                    ProjectName = /*advice.Name,*/ s.TargetName,
                                    ProjectPrice = Math.Round(s.Price, 2),
                                    ProjectQuantity = s.Qty,
                                    ProjectTotamount = Math.Round(s.Price * s.Qty, 2),
                                    ProjectType = s.ProjectType,
                                    Remarks = advice.Remarks,
                                    RestrictedDrugs = ERestrictedDrugs.Default,
                                    SpecimenNo = lis.SpecimenCode, //TODO
                                    GroupId = advice.Code, //TODO
                                    ContainerId = lis.ContainerCode, //TODO 

                                };
                                lisDetail.Add(target);
                            }

                            if (projectLisItemRequest == null)
                            {
                                projectLisItemRequest = new ProjectItemRequest
                                {
                                    BillingDate = advice.ApplyTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    EmergencySign = lis.IsEmergency ? 1 : 0,
                                    ExecuteDeptCode = advice.ExecDeptCode,
                                    ProjectItemNo = advice.PrescriptionNo.ToString(),
                                    SpecialType = 0,
                                };
                            }
                        }

                        if (projectLisItemRequest == null) continue;
                        projectLisItemRequest.ProjectDetail = lisDetail;
                        projectListItem.Add(projectLisItemRequest);

                        lisItem.Add(new ProjectListRequest
                        {
                            GroupType = "202",
                            ProjectItem = projectListItem,
                            ProjectPe = "",
                            ProjectPhl = "",
                        });

                        request.AddProjectItems(lisItem);

                        #endregion

                        break;
                    case EDoctorsAdviceItemType.Treat: //诊疗

                        #region 诊疗

                        var treats = await (await _treatRepository.GetQueryableAsync())
                            .Where(w => ids.Contains(w.DoctorsAdviceId))
                            .ToListAsync();

                        if (!treats.Any()) continue;

                        var treatItem = new List<ProjectListRequest>();
                        var projectTreatItem = new List<ProjectItemRequest>();
                        ProjectItemRequest projectTreatItemRequest = null;
                        var projectDetails = new List<ProjectDetailRequest>();

                        var treatmtm = treats.Select(s => s.Id).ToList();

                        var tmtm = await (await _medicalTechnologyMapRepository.GetQueryableAsync()).Where(w => treatmtm.Contains(w.LPTId)).ToListAsync();

                        foreach (var treat in treats)
                        {
                            var advice = advices.FirstOrDefault(w => treat.DoctorsAdviceId == w.Id);
                            if (advice == null) continue;

                            var projectDetailNo = tmtm.FirstOrDefault(w => treat.Id == w.LPTId);

                            var price = Math.Round(advice.Price, 2);
                            var totalAmount = Math.Round(advice.Price * advice.RecieveQty, 2);

                            //添加儿童价格配置
                            var witch = false;
                            bool.TryParse(_configuration["ChildrenPriceSwitch"], out witch);
                            if (witch)
                            {
                                //儿童价格判断 
                                if (isChildren && treat.Additional && treat.OtherPrice > 0)
                                {
                                    price = Math.Round(treat.OtherPrice ?? advice.Price, 2);
                                    totalAmount = Math.Round(treat.OtherPrice.HasValue ? treat.OtherPrice.Value * advice.RecieveQty : advice.Price * advice.RecieveQty, 2);
                                    _logger.LogInformation($"儿童价格数据：price={price}, totalAmount={totalAmount}");
                                }
                            }

                            var detail = new ProjectDetailRequest
                            {
                                ProjectDetailNo = projectDetailNo.Id.ToString(),
                                GroupsId = advice.Code,
                                ProjectMain = "1",
                                ProjectMerge = treat.ProjectMerge,
                                ProjectName = treat.ProjectName,
                                ProjectPrice = price,
                                ProjectQuantity = advice.RecieveQty,
                                ProjectTotamount = totalAmount,
                                ProjectType = treat.ProjectType,
                                Remarks = advice.Remarks,
                                RestrictedDrugs = ERestrictedDrugs.Default,
                                SpecimenNo = "", //TODO
                                GroupId = "", //TODO
                                ContainerId = "", //TODO    
                            };

                            projectDetails.Add(detail);

                            if (projectTreatItemRequest == null)
                            {
                                projectTreatItemRequest = new ProjectItemRequest
                                {
                                    BillingDate = advice.ApplyTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    EmergencySign = 0,
                                    ExecuteDeptCode = advice.ExecDeptCode.IsNullOrEmpty() ? "0" : advice.ExecDeptCode,
                                    ProjectItemNo = advice.PrescriptionNo.ToString(),
                                    SpecialType = 0,
                                };
                            }
                        }

                        if (projectTreatItemRequest == null) continue;

                        projectTreatItemRequest.ProjectDetail = projectDetails;
                        projectTreatItem.Add(projectTreatItemRequest);

                        treatItem.Add(new ProjectListRequest
                        {
                            GroupType = "0",
                            ProjectItem = projectTreatItem,
                            ProjectPe = "",
                            ProjectPhl = "",
                        });

                        request.AddProjectItems(treatItem);

                        #endregion

                        break;
                    default:
                        break;

                }
            }

            //提交医嘱信息给医院 
            var model = await _hospitalClientAppService.SendMedicalInfoAsync(request);

            var medDetails = model.MedDetail;

            foreach (var md in medDetails)
            {
                var entity = new MedDetailResult(
                       visSerialNo: request.VisSerialNo,
                       patientId: request.PatientId,
                       doctorCode: baseinfo.OperatorCode,
                       doctorName: baseinfo.OperatorName,
                       deptCode: baseinfo.DeptCode,
                       deptName: baseinfo.DeptName,
                       medType: md.MedType,
                       channelNumber: md.ChannelNumber,
                       hisNumber: md.HisNumber,
                       channelNo: md.ChannelNo,
                       medicalNo: model.MedicalNo,
                       lgzxyyPayurl: md.lgzxyyPayurl,
                       lgjkzxPayurl: md.lgjkzxPayurl,
                       medNature: md.MedNature,
                       medFee: md.MedFee);
                list.Add(entity);

                response.Add(new PushDoctorsAdviceModel(md.MedType, md.ChannelNumber, md.HisNumber));
            }

            await _medDetailResultRepository.InsertManyAsync(list); //将放回的记录保存到本地数据库中 

            foreach (var item in prescriptions)
            {
                var retmodel = list.FirstOrDefault(w => w.ChannelNumber == item.MyPrescriptionNo);
                if (retmodel != null)
                {
                    item.Update(retmodel.HisNumber, baseinfo.VisSerialNo);
                }
            }

            if (prescriptions.Any()) await _prescriptionRepository.UpdateManyAsync(prescriptions); //更新处方单号信息

            if (updateAdviceList.Any()) await _doctorsAdviceRepository.UpdateManyAsync(updateAdviceList); //更新同一个订单的序号

            return response;
            //await uow.CompleteAsync();
        }
        catch (Exception ex)
        {
            //await uow.RollbackAsync();
            _logger.LogError(ex, $"回传医嘱信息给HIS异常，{ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// 处理医嘱信息回传
    /// </summary>
    /// <returns></returns>
    private async Task<List<PushDoctorsAdviceModel>> CallDdpSendMedicalInfoAsync(SendMedicalInfoEto prescriptions, bool isChildren = false)
    {
        if (!prescriptions.AdviceGroup.Any()) Oh.Error("不能提交空单");

        List<PushDoctorsAdviceModel> result = new List<PushDoctorsAdviceModel>();
        SubmitDoctorsAdviceDto baseInfo = prescriptions.BaseInfo;

        AdmissionRecordDto admissionRecord = await _patientAppService.GetPatientInfoAsync(baseInfo.Piid);

        //TODO 实现DDP提交医嘱信息部分
        var ddpRequest = new PKUSendMedicalInfoRequest
        {
            ClinicalSymptom = string.Empty, //TODO
            Diagnosis = admissionRecord.DiagnoseName.Length > 32 ? admissionRecord.DiagnoseName.Substring(0, 32) : admissionRecord.DiagnoseName,//诊断太多his报错
            InvoiceNo = admissionRecord.InvoiceNum, //TODO
            MedicalHistory = string.Empty, //TODO
            PatientId = admissionRecord.PatientID,
            PatientName = admissionRecord.PatientName.Length > 5 ? admissionRecord.PatientName.Substring(0, 5) : admissionRecord.PatientName,
            RegistSerialNo = admissionRecord.RegisterSerialNo,
            WeekOfPregnant = 1,
            Weight = admissionRecord.Weight,
            VisSerialNo = admissionRecord.VisSerialNo,
        };
        bool isChild = false;
        if (!string.IsNullOrEmpty(admissionRecord.IDNo))
        {
            var idcard = IDCard.IDCard.Verify(admissionRecord.IDNo);
            if (idcard.IsVerifyPass)
            {
                isChild = idcard.DayOfBirth.IsChildren();
            }
        }

        List<Prescription4HisDto> prescriptionList = new List<Prescription4HisDto>();

        foreach (var item in prescriptions.AdviceGroup)
        {
            //一个KEY一个单
            EDoctorsAdviceItemType itemType = item.Value.FirstOrDefault().ItemType;

            Prescription4HisDto prescription = new Prescription4HisDto
            {
                ItemType = (int)itemType,
                PrescriptionNo = item.Key,
            };
            List<Guid> ids = item.Value.Select(s => s.Id).ToList();
            switch (itemType)
            {
                case EDoctorsAdviceItemType.Prescribe:
                    {
                        var prescribeList = new List<Prescribe4HisDto>();
                        var doctorsAdvices = await (from d in (await _doctorsAdviceRepository.GetQueryableAsync())
                                                    join p in (await _prescribeRepository.GetQueryableAsync())
                                                    on d.Id equals p.DoctorsAdviceId
                                                    where ids.Contains(d.Id)
                                                    select new { DoctorsAdvice = d, drug = p }).ToListAsync();
                        foreach (var adviceItem in doctorsAdvices)
                        {
                            DoctorsAdvice advice = adviceItem.DoctorsAdvice;
                            Prescribe drug = adviceItem.drug;

                            prescribeList.Add(new Prescribe4HisDto
                            {
                                ActualDays = drug.ActualDays,
                                Amount = advice.Amount,
                                AntibioticPermission = drug.AntibioticPermission,
                                ApplyDeptCode = DEPT_CODE,
                                ApplyDeptName = DEPT_NAME,
                                ApplyDoctorCode = advice.ApplyDoctorCode,
                                ApplyDoctorName = advice.ApplyDoctorName,
                                ApplyTime = advice.ApplyTime,
                                BatchNo = drug.BatchNo,
                                BigPackFactor = drug.BigPackFactor,
                                BigPackPrice = drug.BigPackPrice,
                                BigPackUnit = drug.BigPackUnit,
                                CategoryCode = advice.CategoryCode,
                                CategoryName = advice.CategoryName,
                                ChildrenPrice = drug.ChildrenPrice,
                                Code = advice.Code,
                                DailyFrequency = drug.DailyFrequency,
                                DefaultDosageQty = drug.DefaultDosageQty,
                                DefaultDosageUnit = drug.DefaultDosageUnit,
                                DosageQty = drug.DosageQty,
                                DosageUnit = drug.DosageUnit,
                                EndTime = advice.EndTime,
                                ExecDeptCode = DEPT_CODE,
                                ExecDeptName = DEPT_NAME,
                                ExpirDate = drug.ExpirDate,
                                FactoryAddr = drug.FactoryName, // DOTO我们没有
                                FactoryCode = drug.FactoryCode,
                                FactoryName = drug.FactoryName,
                                FeeTypeMainCode = "38", // DOTO 
                                FeeTypeSubCode = "",  // DOTO
                                FixPrice = drug.FixPrice,
                                FrequencyCode = drug.FrequencyCode,
                                FrequencyExecDayTimes = drug.FrequencyExecDayTimes,
                                FrequencyName = drug.FrequencyName,
                                FrequencyTimes = drug.FrequencyTimes,
                                FrequencyUnit = drug.FrequencyUnit,
                                //GroupNo = advice.RecipeNo,
                                SequenceNo = advice.SequenceNo,
                                InsuranceCode = advice.InsuranceCode,
                                InsuranceType = (int)advice.InsuranceType,
                                IsAdaptationDisease = drug.IsAdaptationDisease,
                                IsAmendedMark = drug.IsAmendedMark,
                                RecipeGroupNo = advice.RecipeGroupNo,
                                RecieveQty = advice.RecieveQty,
                                RecipeNo = advice.RecipeNo,
                                IsChronicDisease = advice.IsChronicDisease,
                                IsCriticalPrescription = drug.IsCriticalPrescription,
                                IsFirstAid = drug.IsFirstAid,
                                IsOutDrug = drug.IsOutDrug,
                                IsSkinTest = drug.IsSkinTest,
                                LimitType = drug.LimitType,
                                LongDays = drug.LongDays,
                                MaterialPrice = drug.MaterialPrice,
                                MedicalInsuranceCode = drug.MedicalInsuranceCode,
                                MedicineId = drug.MedicineId,
                                MedicineProperty = drug.MedicineProperty,
                                Name = advice.Name,
                                PayType = advice.PayType,
                                PayTypeCode = advice.PayTypeCode,
                                PharmacyCode = drug.PharmacyCode,
                                PharmacyName = drug.PharmacyName,
                                PrescribeId = item.Key,
                                PrescriptionPermission = drug.PrescriptionPermission,
                                PrescribeTypeCode = advice.PrescribeTypeCode,
                                PrescribeTypeName = advice.PrescribeTypeName,
                                PrescriptionNo = item.Key,
                                Price = advice.Price,
                                QtyPerTimes = drug.QtyPerTimes,
                                RecieveUnit = advice.RecieveUnit,
                                Remarks = advice.Remarks,
                                RestrictedDrugs = drug.RestrictedDrugs.HasValue ? (int)drug.RestrictedDrugs.Value : (int)ERestrictedDrugs.QuanZifei,
                                RetPrice = drug.RetPrice,
                                SmallPackFactor = drug.SmallPackFactor,
                                SmallPackPrice = drug.SmallPackPrice,
                                SmallPackUnit = drug.SmallPackUnit,
                                Specification = drug.Specification,
                                Speed = drug.Speed,
                                StartTime = advice.StartTime,
                                ToxicProperty = drug.ToxicProperty,
                                TraineeCode = advice.TraineeCode,
                                TraineeName = advice.TraineeName,
                                Unit = advice.Unit,
                                Unpack = (int)drug.Unpack,
                                UsageCode = drug.UsageCode,
                                UsageName = drug.UsageName,
                            });
                        }
                        prescription.PrescribeList = prescribeList;
                    }
                    break;
                case EDoctorsAdviceItemType.Pacs:
                    {
                        List<Pacs4HisDto> pacsList = new List<Pacs4HisDto>();
                        var doctorsAdvices = await (from d in (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => ids.Contains(w.Id))
                                                    join p in (await _pacsRepository.GetQueryableAsync())
                                                    on d.Id equals p.DoctorsAdviceId
                                                    select new
                                                    {
                                                        DoctorsAdvice = d,
                                                        Pacs = p
                                                    }).ToListAsync();

                        var pacsItemList = await (from p in (await _pacsRepository.GetQueryableAsync()).Where(w => ids.Contains(w.DoctorsAdviceId))
                                                  join i in (await _pacsItemRepository.GetQueryableAsync())
                                                  on p.Id equals i.PacsId
                                                  select new
                                                  {
                                                      Items = i
                                                  }).ToListAsync();

                        foreach (var adviceItem in doctorsAdvices)
                        {
                            var advice = adviceItem.DoctorsAdvice;
                            var pacs = adviceItem.Pacs;
                            var pacsItems = pacsItemList.Select(s => s.Items).Where(w => w.PacsId == pacs.Id).ToList();

                            var items = ObjectMapper.Map<List<PacsItem>, List<PacsItem4HisDto>>(pacsItems);
                            foreach (PacsItem4HisDto pacsItem4HisDto in items)
                            {
                                pacsItem4HisDto.TargetName = pacsItem4HisDto.TargetName?.Length > 18 ? pacsItem4HisDto.TargetName?.Substring(0, 18) : pacsItem4HisDto.TargetName;
                            }

                            pacsList.Add(new Pacs4HisDto
                            {
                                AddCard = pacs.AddCard,
                                Amount = advice.Amount,
                                ApplyDeptCode = DEPT_CODE,
                                ApplyDeptName = DEPT_NAME,
                                ApplyDoctorCode = advice.ApplyDoctorCode,
                                ApplyDoctorName = advice.ApplyDoctorName,
                                ApplyTime = advice.ApplyTime,
                                CatalogCode = pacs.CatalogCode,
                                CategoryCode = advice.CategoryCode,
                                CatalogDisplayName = pacs.CatalogDisplayName,
                                CategoryName = advice.CategoryName,
                                CatalogName = pacs.CatalogName,
                                Code = advice.Code,
                                ExecDeptCode = advice.ExecDeptCode,
                                ExecDeptName = advice.ExecDeptName,
                                FeeTypeMainCode = "38", // TODO
                                FeeTypeSubCode = "",// TODO
                                FirstCatalogCode = pacs.FirstCatalogCode,
                                FirstCatalogName = pacs.FirstCatalogName,
                                GuideCode = pacs.GuideCode,
                                GuideName = pacs.GuideName,
                                ExamTitle = pacs.ExamTitle,
                                InsuranceCode = advice.InsuranceCode,
                                InsuranceType = (int)advice.InsuranceType,
                                IsBedSide = pacs.IsBedSide,
                                IsChronicDisease = advice.IsChronicDisease,
                                IsEmergency = pacs.IsEmergency,
                                Name = advice.Name?.Length > 18 ? advice.Name.Substring(0, 18) : advice.Name,
                                PartCode = pacs.PartCode,
                                PartName = pacs.PartName,
                                PayType = advice.PayType,
                                PayTypeCode = advice.PayTypeCode,
                                PrescribeTypeCode = advice.PrescribeTypeCode,
                                PrescriptionNo = advice.PrescriptionNo,
                                Price = advice.Price,
                                ProjectCode = pacs.ProjectCode,
                                RecipeGroupNo = advice.RecipeGroupNo,
                                RecipeNo = advice.RecipeNo,
                                Remarks = advice.Remarks,
                                TraineeCode = advice.TraineeCode,
                                TraineeName = advice.TraineeName,
                                Manufacturer = item.Key,
                                PacsItems = items,
                            });
                        }
                        prescription.PacsList = pacsList;
                    }
                    break;
                case EDoctorsAdviceItemType.Lis:
                    {
                        var lisList = new List<Lis4HisDto>();
                        var doctorsAdvices = await (from d in (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => ids.Contains(w.Id))
                                                    join l in (await _lisRepository.GetQueryableAsync())
                                                    on d.Id equals l.DoctorsAdviceId
                                                    select new { DoctorsAdvice = d, Lis = l }).ToListAsync();

                        var lisItems = await (from l in (await _lisRepository.GetQueryableAsync()).Where(w => ids.Contains(w.DoctorsAdviceId))
                                              join i in (await _lisItemRepository.GetQueryableAsync())
                                              on l.Id equals i.LisId
                                              select new { LisItems = i }).ToListAsync();


                        foreach (var adviceItem in doctorsAdvices)
                        {
                            var advice = adviceItem.DoctorsAdvice;
                            var lis = adviceItem.Lis;
                            var lisITems = lisItems.Select(s => s.LisItems).Where(w => w.LisId == lis.Id).ToList();
                            var items = ObjectMapper.Map<List<LisItem>, List<LisItem4HisDto>>(lisITems);
                            foreach (LisItem4HisDto lisItem4HisDto in items)
                            {
                                lisItem4HisDto.TargetName = lisItem4HisDto.TargetName?.Length > 18 ? lisItem4HisDto.TargetName?.Substring(0, 18) : lisItem4HisDto.TargetName;
                            }

                            lisList.Add(new Lis4HisDto
                            {
                                LisId = item.Key,
                                AddCard = lis.AddCard,
                                Amount = advice.Amount,
                                ApplyDeptCode = DEPT_CODE,
                                ApplyDeptName = DEPT_NAME,
                                ApplyDoctorCode = advice.ApplyDoctorCode,
                                ApplyDoctorName = advice.ApplyDoctorName,
                                ApplyTime = advice.ApplyTime,
                                CatalogCode = lis.SpecimenCode,
                                CategoryCode = advice.CategoryCode,
                                CategoryName = advice.CategoryName,
                                CatalogName = lis.SpecimenName,
                                Code = advice.Code + "1",
                                ExecDeptCode = advice.ExecDeptCode,
                                ExecDeptName = advice.ExecDeptName,
                                FeeTypeMainCode = "38", // TODO
                                FeeTypeSubCode = "",// TODO 
                                GuideCode = lis.GuideCode,
                                GuideName = lis.GuideName,
                                InsuranceCode = advice.InsuranceCode,
                                InsuranceType = (int)advice.InsuranceType,
                                IsBedSide = lis.IsBedSide,
                                IsChronicDisease = advice.IsChronicDisease,
                                IsEmergency = lis.IsEmergency,
                                Name = advice.Name,
                                PayType = advice.PayType,
                                PayTypeCode = advice.PayTypeCode,
                                PrescribeTypeCode = advice.PrescribeTypeCode,
                                PrescriptionNo = advice.PrescriptionNo,
                                Price = advice.Price,
                                ProjectCode = advice.Code + "1",
                                RecipeGroupNo = advice.RecipeGroupNo,
                                RecipeNo = advice.RecipeNo,
                                Remarks = advice.Remarks,
                                TraineeCode = advice.TraineeCode,
                                TraineeName = advice.TraineeName,
                                ContainerCode = lis.ContainerCode,
                                ContainerColor = lis.ContainerColor,
                                ContainerName = lis.ContainerName,
                                SpecimenCode = lis.SpecimenCode,
                                SpecimenDescription = lis.SpecimenDescription,
                                SpecimenName = lis.SpecimenName,
                                SpecimenPartName = lis.SpecimenPartName,
                                LisItems = items,
                            });
                        }
                        prescription.LisList = lisList;
                    }
                    break;
                case EDoctorsAdviceItemType.Treat:
                    {
                        List<Treat4HisDto> treatList = new List<Treat4HisDto>();
                        List<Treat> treats = await _treatRepository.GetListAsync(w => ids.Contains(w.DoctorsAdviceId) && w.ProjectType == "7");
                        List<DoctorsAdvice> doctorsAdvice = await _doctorsAdviceRepository.GetListAsync(w => ids.Contains(w.Id));

                        foreach (Treat treat in treats)
                        {
                            DoctorsAdvice advice = doctorsAdvice.FirstOrDefault(p => p.Id == treat.DoctorsAdviceId);
                            if (advice != null)
                            {

                                treatList.Add(new Treat4HisDto
                                {
                                    Amount = advice.Amount,
                                    ApplyDeptCode = DEPT_CODE,
                                    ApplyDeptName = DEPT_NAME,
                                    ApplyDoctorCode = advice.ApplyDoctorCode,
                                    ApplyDoctorName = advice.ApplyDoctorName,
                                    ApplyTime = advice.ApplyTime,
                                    CategoryCode = advice.CategoryCode,
                                    CategoryName = advice.CategoryName,
                                    Code = advice.Code,
                                    ExecDeptCode = advice.ExecDeptName.Contains("急诊") ? DEPT_CODE : advice.ExecDeptCode,
                                    ExecDeptName = advice.ExecDeptName.Contains("急诊") ? DEPT_NAME : advice.ExecDeptName,
                                    FeeTypeMainCode = "38", // TODO
                                    FeeTypeSubCode = "",// TODO  
                                    InsuranceCode = advice.InsuranceCode,
                                    InsuranceType = (int)advice.InsuranceType,
                                    IsChronicDisease = advice.IsChronicDisease,
                                    Name = treat.ProjectName,
                                    PayType = advice.PayType,
                                    PayTypeCode = advice.PayTypeCode,
                                    PrescribeTypeCode = advice.PrescribeTypeCode,
                                    PrescriptionNo = advice.PrescriptionNo,
                                    Price = (treat.Additional && isChild) ? (treat.OtherPrice ?? 0) : advice.Price,
                                    RecipeGroupNo = advice.RecipeGroupNo,
                                    RecipeNo = advice.RecipeNo,
                                    Remarks = advice.Remarks,
                                    TraineeCode = advice.TraineeCode,
                                    TraineeName = advice.TraineeName,
                                    Additional = treat.Additional,
                                    AdditionalItemsType = (int)treat.AdditionalItemsType,
                                    FrequencyCode = treat.FrequencyCode,
                                    FrequencyName = treat.FrequencyName,
                                    LongDays = treat.LongDays,
                                    OtherPrice = treat.OtherPrice,
                                    ProjectMerge = treat.ProjectMerge,
                                    ProjectName = string.Empty,
                                    ProjectType = treat.ProjectType,
                                    Specification = treat.Specification,
                                    TreatId = treat.TreatId,
                                    UsageCode = treat.UsageCode,
                                    UsageName = treat.UsageName,
                                    Qty = advice.RecieveQty,
                                    Manufacturer = item.Key
                                });
                            }
                        }
                        prescription.TreatList = treatList;
                    }
                    break;
                default:
                    break;
            }
            prescriptionList.Add(prescription);
        }
        ddpRequest.PrescriptionList = prescriptionList;
        DdpBaseResponse<DdpSendMedicalInfoResponse> ddpResponse = await _ddpApiService.SendMedicalInfoAsync(ddpRequest);

        if (ddpResponse.Code == 200)
        {
            List<Guid> ids = prescriptions.BaseInfo.Ids;
            List<Prescription> prescriptionLists = await _prescriptionRepository.GetListAsync(x => ids.Contains(x.DoctorsAdviceId));
            List<DoctorsAdvice> doctorsAdviceList = await _doctorsAdviceRepository.GetListAsync(x => ids.Contains(x.Id));

            List<MedDetail> resultList = ddpResponse.Data.medDetail;
            List<MedDetailResult> medDetailResultList = new List<MedDetailResult>();
            foreach (MedDetail item in resultList)
            {
                doctorsAdviceList.Where(x => x.PrescriptionNo == item.ChannelNumber).ForEach(x => x.HisOrderNo = item.HisNumber);

                IEnumerable<Guid> adviceIds = doctorsAdviceList.Where(x => x.PrescriptionNo == item.ChannelNumber).Select(x => x.Id);

                prescriptionLists.Where(x => adviceIds.Contains(x.DoctorsAdviceId)).ForEach(x => x.PrescriptionNo = item.ChannelNo);

                MedDetailResult medDetailResult = new MedDetailResult(
                               visSerialNo: admissionRecord.VisSerialNo,
                               patientId: admissionRecord.PatientID,
                               doctorCode: baseInfo.OperatorCode,
                               doctorName: baseInfo.OperatorName,
                               deptCode: baseInfo.DeptCode,
                               deptName: baseInfo.DeptName,
                               medType: item.ChannelNo != item.HisNumber ? "CF" : "YJ",
                               channelNumber: item.ChannelNumber,
                               hisNumber: item.HisNumber,
                               channelNo: item.ChannelNo,
                               medicalNo: string.Empty,
                               lgzxyyPayurl: string.Empty,
                               lgjkzxPayurl: string.Empty,
                               medNature: string.Empty,
                               medFee: string.Empty);
                medDetailResultList.Add(medDetailResult);


                PushDoctorsAdviceModel pushDoctorsAdviceModel = new PushDoctorsAdviceModel(item.ChannelNo != item.HisNumber ? "CF" : "YJ", item.ChannelNo, item.HisNumber);
                result.Add(pushDoctorsAdviceModel);
            }

            if (prescriptionList.Any()) await _prescriptionRepository.UpdateManyAsync(prescriptionLists);
            if (doctorsAdviceList.Any()) await _doctorsAdviceRepository.UpdateManyAsync(doctorsAdviceList);
            if (medDetailResultList.Any())
            {
                //medDetailResultList = medDetailResultList.CustomDistinctBy(x => x.ChannelNumber).ToList();
                await _medDetailResultRepository.InsertManyAsync(medDetailResultList);
            }
        }
        else
        {
            _logger.LogError($"回传医嘱信息给HIS异常，{ddpResponse.Msg}");
            throw new Exception($"回传医嘱信息给HIS异常，{ddpResponse.Msg}");
        }

        return result;
    }

    /// <summary>
    /// 针次转换
    /// <![CDATA[
    /// 四针法：（times vs histimes）
    ///      1:1, 2:3, 3:7
    /// 五针法：
    ///      1:1, 2:2, 3:3, 4:7, 5:8
    /// ]]>
    /// </summary>
    /// <param name="acupunctureManipulation"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    private int Transform(EAcupunctureManipulation acupunctureManipulation, int times)
    {
        if (acupunctureManipulation == EAcupunctureManipulation.FourTimes)
        {
            //四针法
            switch (times)
            {
                case 1: return 1;
                case 2: return 3;
                case 3: return 7;
                default:
                    break;
            }
        }
        else
        {
            //五针法
            switch (times)
            {
                case 1: return 1;
                case 2: return 2;
                case 3: return 3;
                case 4: return 7;
                case 5: return 8;
                default:
                    break;
            }
        }
        return 0;
    }

    private async Task UpdatePatientInfoAsync(string entrustName, Guid piId)
    {
        List<string> list = new List<string>()
        {
            "告病危","告病重","患者开绿通","取消告病危","取消告病重","患者取消绿通"
        };

        if (!list.Contains(entrustName))
        {
            return;
        }

        await _capPublisher.PublishAsync("recipe.to.updatebedhead", new { EntrustName = entrustName, PiId = piId });
    }
}
