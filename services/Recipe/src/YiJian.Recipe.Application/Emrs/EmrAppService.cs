using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Uow;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Etos.EMRs;
using YiJian.Emrs.Contracts;
using YiJian.Emrs.Dto;
using YiJian.Emrs.Entities;
using YiJian.Hospitals;
using YiJian.Hospitals.Dto;
using YiJian.Recipes.DoctorsAdvices.Contracts;

namespace YiJian.Emrs
{
    /// <summary>
    /// 电子病历数据源服务
    /// </summary>
    [Authorize]
    public class EmrAppService : ApplicationService, IEmrAppService, ICapSubscribe
    {
        private readonly IHospitalClientAppService _hospitalClientAppService;

        private readonly IDoctorsAdviceRepository _doctorsAdviceRepository;
        private readonly IPrescribeRepository _prescribeRepository;
        private readonly ITreatRepository _treatRepository;
        private readonly IEmrUsedAdviceRecordRepository _emrUsedAdviceRecordRepository;
        private readonly ILogger<EmrAppService> _logger;

        /// <summary>
        /// 电子病历数据源服务
        /// </summary> 
        public EmrAppService(IHospitalClientAppService hospitalClientAppService
            , IDoctorsAdviceRepository doctorsAdviceRepository
            , IPrescribeRepository prescribeRepository
            , ITreatRepository treatRepository
            , IEmrUsedAdviceRecordRepository emrUsedAdviceRecordRepository
            , ILogger<EmrAppService> logger)
        {

            _doctorsAdviceRepository = doctorsAdviceRepository;
            _prescribeRepository = prescribeRepository;
            _treatRepository = treatRepository;
            _hospitalClientAppService = hospitalClientAppService;
            _emrUsedAdviceRecordRepository = emrUsedAdviceRecordRepository;
            _logger = logger;
        }


        #region 检查检验

        /// <summary>
        /// 查询检查报告列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<List<EmrPacsListResponse>> PacsReportListAsync(QueryPacsReportListRequest model)
        {
            List<EmrPacsListResponse> list = new List<EmrPacsListResponse>();
            QueryPacsReportListResponse response = await _hospitalClientAppService.QueryPacsReportListAsync(model);

            if (response == null || response.ReportInfos == null) return list;

            foreach (ReportInfoListResponse item in response.ReportInfos)
            {
                list.Add(new EmrPacsListResponse
                {
                    ApplyNo = item.ApplyNo,
                    ExamType = item.ExamType,
                    ItemCode = item.ItemCode,
                    ItemName = item.ItemName,
                    Name = response.PatientName,
                    PatientId = response.PatientId,
                    ReportNo = item.ReportNo,
                    ReportTitle = item.ReportTitle,
                    StudyId = item.StudyId,
                    VisitNo = response.VisitNo,
                    ExamTime = item.ExamTime,
                    Url = item.Url,
                });
            }

            if (!string.IsNullOrEmpty(model.StartDate) && !string.IsNullOrEmpty(model.EndDate))
            {
                DateTime startTime = DateTime.Parse(model.StartDate);
                DateTime endTime = DateTime.Parse(model.EndDate);
                list = list.Where(x => x.ExamTime > startTime && x.ExamTime < endTime).ToList();
            }
            return list;
        }

        /// <summary>
        /// 查询检查报告信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<EmrPacsReportResponse> PacsReportAsync(QueryPacsReportRequest model)
        {
            GetPacsReportResponse data = await _hospitalClientAppService.QueryPacsReportAsync(model);

            return new EmrPacsReportResponse
            {
                DiagnoseCode = data.PatientInfo.DiagnoseCode,
                DiagnoseName = data.PatientInfo.DiagnoseName,
                MedicalHistory = data.PatientInfo.MedicalHistory,
                PatientId = data.PatientInfo.PatientId,
                VisitNo = data.PatientInfo.VisitNo,
                AbnormalFlag = data.ReportInfo.AbnormalFlag,
                ExamPart = data.ReportInfo.ExamPart,
                ExamPartDesc = data.ReportInfo.ExamPartDesc,
                ExamPurpose = data.ReportInfo.ExamPurpose,
                ExamType = data.ReportInfo.ExamType,
                ItemCode = data.ReportInfo.ItemCode,
                ItemName = data.ReportInfo.ItemName,
                ParticipantTime = data.ReportInfo.ParticipantTime,
                ReportNo = data.ReportInfo.ReportNo,
                ReportTitle = data.ReportInfo.ReportTitle,
                StudyHint = data.ReportInfo.StudyHint,
                StudySee = data.ReportInfo.StudySee,
                VisitStateDesc = data.ReportInfo.VisitStateDesc,
            };
        }

        /// <summary>
        /// 检验报告列表查询
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<List<EmrLisListResponse>> LisReportListAsync(GetLisReportListRequest model)
        {
            var ret = new List<EmrLisListResponse>();
            var data = await _hospitalClientAppService.QueryLisReportListAsync(model);
            _logger.LogInformation($"执行QueryLisReportListAsync返回：{System.Text.Json.JsonSerializer.Serialize(data)}");
            var group = data.GroupBy(g => g.ReportNo);

            foreach (var g in group)
            {
                var lises = g.ToList();
                List<string> applyNo = new();
                List<string> masterItemIds = new();
                List<string> masterItemNos = new();
                List<string> masterItemCodes = new();
                List<string> masterItemNames = new();

                lises.ForEach(ele =>
                {
                    ele.ApplyInfoList.ForEach(a =>
                    {
                        applyNo.Append(a.ApplyNo);
                        a.MasterItemList.ForEach(x =>
                        {
                            masterItemIds.Add(x.MasterItemId);
                            masterItemNos.Add(x.MasterItemNo);
                            masterItemCodes.Add(x.MasterItemCode);
                            masterItemNames.Add(x.MasterItemName);
                        });
                    });
                });

                var lis = lises.FirstOrDefault();
                _logger.LogInformation($"当前打印的时间为：{lis.LabTime}");
                ret.Add(new EmrLisListResponse
                {
                    ApplyNo = applyNo.FirstOrDefault(),
                    MasterItemId = masterItemIds.Distinct().JoinAsString(","),
                    MasterItemNo = masterItemNos.Distinct().JoinAsString(","),
                    MasterItemCode = masterItemCodes.Distinct().JoinAsString(","),
                    MasterItemName = masterItemNames.Distinct().JoinAsString(","),
                    PatientId = lis.PatientId,
                    PatientName = lis.PatientName,
                    PatientType = lis.PatientType,
                    ReportNo = lis.ReportNo,
                    VisitNo = lis.VisitNo,
                    VisitSerialNo = lis.VisitSerialNo,
                    LabTime = lis.LabTime,
                });
            }

            if (!string.IsNullOrEmpty(model.StartDate) && !string.IsNullOrEmpty(model.EndDate))
            {
                DateTime startTime = DateTime.Parse(model.StartDate);
                DateTime endTime = DateTime.Parse(model.EndDate);
                ret = ret.Where(x => x.LabTime > startTime && x.LabTime < endTime).ToList();
            }
            return ret;
        }

        /// <summary>
        /// 检验报告详情查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<List<EmrLisResponse>> LisReportAsync(GetLisReportRequest model)
        {
            var data = await _hospitalClientAppService.QueryLisReportAsync(model);

            var list = new List<EmrLisResponse>();
            if (data.ReportItemList == null) return new List<EmrLisResponse>();

            foreach (var x in data.ReportItemList)
            {
                string itemResultFlag = string.Empty;
                switch (x.ItemResultFlag)
                {
                    case "0": itemResultFlag = "N"; break;
                    case "1": itemResultFlag = "H"; break;
                    case "2": itemResultFlag = "L"; break;
                    default: break;
                }

                list.Add(new EmrLisResponse
                {
                    Id = Guid.NewGuid(),
                    LisNo = data.LisNo,
                    ItemAbnormalFlag = x.ItemAbnormalFlag,
                    ItemChiName = x.ItemChiName,
                    ItemCode = x.ItemCode,
                    ItemNo = x.ItemNo,
                    ItemResult = x.ItemResult,
                    ItemResultFlag = itemResultFlag,
                    ItemResultUnit = x.ItemResultUnit,
                    LabTime = data.LabTime,
                    PatientId = data.PatientId,
                    ReferenceDesc = x.ReferenceDesc,
                    ReferenceHighLimit = x.ReferenceHighLimit,
                    ReferenceLowLimit = x.ReferenceLowLimit,
                    VisitNo = data.VisitNo
                });
            }

            list = list.DistinctBy(x => x.ItemCode).ToList();
            return list;
        }

        /// <summary>
        /// 获取检验报告PDF
        /// </summary>
        /// <param name="applyNo"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> GetLisReportPdfAsync(string applyNo)
        {
            string mime = "application/pdf";
            string report = await _hospitalClientAppService.GetLisReportPdfAsync(applyNo);
            byte[] pdfBytes = Convert.FromBase64String(report);

            FileContentResult file = new FileContentResult(pdfBytes, mime);
            file.FileDownloadName = $"{applyNo}.pdf";
            return file;
        }
        #endregion


        #region 医嘱信息

        /// <summary>
        /// 获取当前患者的医嘱信息集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        //[AllowAnonymous]
        public async Task<List<AdviceListResponse>> AdviceListAsync(AdviceListRequest model)
        {
            var status = new List<ERecipeStatus>
            {
                //ERecipeStatus.Saved, 
                ERecipeStatus.Submitted,
                ERecipeStatus.Confirmed,
                ERecipeStatus.Executed,
                ERecipeStatus.PayOff,
            };

            var query = (from a in (await _doctorsAdviceRepository.GetQueryableAsync()).WhereIf(!model.DoctorCode.IsNullOrEmpty(), w => w.ApplyDoctorCode == model.DoctorCode)
                         join pr in (await _prescribeRepository.GetQueryableAsync())
                         on a.Id equals pr.DoctorsAdviceId
                         into temp
                         from p in temp.DefaultIfEmpty()
                         where a.PIID == model.PIID && a.PlatformType == ECIS.ShareModel.Enums.EPlatformType.EmergencyTreatment && status.Contains(a.Status)
                         join tr in (await _treatRepository.GetQueryableAsync())
                         on a.Id equals tr.DoctorsAdviceId
                         into temp2
                         from t in temp2.DefaultIfEmpty()
                         select new AdviceListResponse
                         {
                             Id = a.Id,
                             RecipeNo = a.RecipeNo,
                             RecipeGroupNo = a.RecipeGroupNo,
                             Name = a.Name,
                             PIID = model.PIID,
                             ApplyTime = a.ApplyTime,
                             CategoryName = a.CategoryName,
                             Code = a.Code,
                             ItemType = a.ItemType,
                             PatientId = a.PatientId,
                             PlatformType = a.PlatformType,
                             FrequencyCode = a.ItemType == EDoctorsAdviceItemType.Prescribe ? (p != null ? p.FrequencyCode : "") : (a.ItemType == EDoctorsAdviceItemType.Treat ? (t != null ? t.FrequencyCode : "") : ""),
                             FrequencyName = a.ItemType == EDoctorsAdviceItemType.Prescribe ? (p != null ? p.FrequencyName : "") : (a.ItemType == EDoctorsAdviceItemType.Treat ? (t != null ? t.FrequencyName : "") : ""),
                             PatientName = a.PatientName,
                             PrescribeTypeName = a.PrescribeTypeName,
                             RecieveQty = a.RecieveQty,
                             RecieveUnit = a.RecieveUnit,
                             DosageQty = p != null ? p.DosageQty : null,
                             DosageUnit = p != null ? p.DosageUnit : "",
                             UsageCode = p != null ? p.UsageCode : "",
                             UsageName = p != null ? p.UsageName : "",
                             Specification = p != null ? p.Specification : "",
                             LongDays = p != null ? p.LongDays : 1,
                             AdditionalItemsType = t != null ? t.AdditionalItemsType : 0,

                         }).AsQueryable();

            var usedAdvices = await (await _emrUsedAdviceRecordRepository.GetQueryableAsync()).Where(w => w.PIID == model.PIID).ToListAsync();

            //获取未使用（未导入）的医嘱
            if (model.UnusedAdvice)
            {
                //获取所有的患者医嘱记录
                var list = await query.ToListAsync();
                if (!usedAdvices.Any())
                {
                    if (model.OpenAdditionalTreat.HasValue && model.OpenAdditionalTreat.Value)
                    {
                        return list.OrderBy(o => o.RecipeNo).ThenBy(t => t.RecipeGroupNo).ToList();
                    }
                    else
                    {
                        return list.Where(w => w.AdditionalItemsType == 0).OrderBy(o => o.RecipeNo).ThenBy(t => t.RecipeGroupNo).ToList();
                    }
                }
                var usedAdviceids = usedAdvices.Select(s => s.DoctorsAdviceId).ToList();
                var res = list.Where(w => !usedAdviceids.Contains(w.Id)).OrderBy(o => o.RecipeNo).ThenBy(t => t.RecipeGroupNo).ToList();
                res.ForEach(x => x.Used = false);

                //开启附加项
                if (model.OpenAdditionalTreat.HasValue && model.OpenAdditionalTreat.Value) return res;
                //没有开启展示附加项
                return res.Where(w => w.AdditionalItemsType == 0).ToList();
            }
            //根据查询条件获取一段记录
            else
            {
                var list = await query
                .WhereIf(model.BeginDate.HasValue && model.EndDate.HasValue, w => w.ApplyTime >= model.BeginDate.Value && w.ApplyTime < model.EndDate.Value.AddDays(1))
                .WhereIf(model.BeginDate.HasValue && !model.EndDate.HasValue, w => w.ApplyTime >= model.BeginDate.Value)
                .WhereIf(!model.BeginDate.HasValue && model.EndDate.HasValue, w => w.ApplyTime < model.EndDate.Value.AddDays(1))
                .OrderBy(o => o.RecipeNo)
                .ThenBy(o => o.RecipeGroupNo)
                .ToListAsync();

                foreach (var item in list)
                {
                    var any = usedAdvices.Any(w => w.DoctorsAdviceId == item.Id);
                    if (any) item.Used = true;
                }
                //开启附加项
                if (model.OpenAdditionalTreat.HasValue && model.OpenAdditionalTreat.Value) return list;
                //没有开启展示附加项
                return list.Where(w => w.AdditionalItemsType == 0).ToList();

            }
        }

        /// <summary>
        /// 已打印则将所有的未设为导入的设为导入，方便下次导入不再重复
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [CapSubscribe("printed.advice.from.emr")]
        public async Task<bool> PrintedAsync(PrintedAdviceEto eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                var status = new List<ERecipeStatus>
                {
                    //ERecipeStatus.Saved,
                    ERecipeStatus.Submitted,
                    ERecipeStatus.Confirmed,
                    ERecipeStatus.Executed,
                    ERecipeStatus.PayOff,
                };
                var usedAdvices = await (await _emrUsedAdviceRecordRepository.GetQueryableAsync()).Where(w => w.PIID == eto.Piid).Select(s => s.DoctorsAdviceId).ToListAsync();
                var unUsedAdvices = await (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w => w.PIID == eto.Piid && status.Contains(w.Status) && !usedAdvices.Contains(w.Id)).ToListAsync();

                var entities = new List<EmrUsedAdviceRecord>();
                foreach (var item in unUsedAdvices)
                {
                    var entity = new EmrUsedAdviceRecord(
                        id: GuidGenerator.Create(),
                        doctorsAdviceId: item.Id,
                        piid: eto.Piid,
                        usedAt: DateTime.Now,
                        doctorCode: eto.DoctorCode,
                        docktorName: eto.DoctorName);
                    entities.Add(entity);
                }

                await _emrUsedAdviceRecordRepository.InsertManyAsync(entities);
                await uow.CompleteAsync();

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"订阅护理服务的驳回功能异常:{ex.Message},请求参数：{System.Text.Json.JsonSerializer.Serialize(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        #endregion
    }
}
