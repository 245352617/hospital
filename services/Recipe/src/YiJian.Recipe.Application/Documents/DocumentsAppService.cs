using DotNetCore.CAP;
using MasterDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using Volo.Abp.Users;
using YiJian.Cases.Contracts;
using YiJian.DoctorsAdvices.Entities;
using YiJian.DoctorsAdvices.Enums;
using YiJian.Documents.Dto;
using YiJian.Documents.Enums;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.Hospitals;
using YiJian.Recipe;
using YiJian.Recipes;
using YiJian.Recipes.DoctorsAdvices.Contracts;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Documents
{
    /// <summary>
    /// 打印服务（分方打印）
    /// </summary>
    [Authorize]
    public partial class DocumentsAppService : ApplicationService, IDocumentsAppService
    {
        private readonly IDoctorsAdviceRepository _doctorsAdviceRepository;
        private readonly IPrescribeRepository _prescribeRepository;
        private readonly ILisRepository _lisRepository;
        private readonly IPacsRepository _pacsRepository;
        private readonly IPacsPathologyItemRepository _pacsPathologyItemRepository;
        private readonly IPacsPathologyItemNoRepository _pacsPathologyItemNoRepository;
        private readonly ITreatRepository _treatRepository;
        private readonly IPrintInfoRepository _printInfoRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IMedDetailResultRepository _medDetailResultRepository;
        private readonly IHospitalClientAppService _hospitalClientAppService;
        private readonly ILogger<DocumentsAppService> _logger;
        private readonly INovelCoronavirusRnaRepository _coronavirusRnaRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IToxicRepository _toxicRepository;
        private readonly IConfiguration _configuration;
        private readonly IPatientCaseRepository _caseRepository;
        private readonly GrpcMasterData.GrpcMasterDataClient _grpcMasterDataClient;
        private readonly ICapPublisher _capPublisher;
        private readonly IHospitalAPI _hospitalAPI;
        private readonly IUserSettingRepository _personSettingRepository;

        /// <summary>
        /// 打印服务（分方打印）
        /// </summary> 
        public DocumentsAppService(
            IDoctorsAdviceRepository doctorsAdviceRepository,
            IPrescribeRepository prescribeRepository,
            ILisRepository lisRepository,
            IPacsRepository pacsRepository,
            ITreatRepository treatRepository,
            IPrintInfoRepository printInfoRepository,
            ILogger<DocumentsAppService> logger,
            IHttpContextAccessor httpContext,
            IHttpClientFactory httpClientFactory,
            GrpcMasterData.GrpcMasterDataClient grpcMasterDataClient,
            INovelCoronavirusRnaRepository coronavirusRnaRepository,
            IPrescriptionRepository prescriptionRepository,
            IMedDetailResultRepository medDetailResultRepository,
            IHospitalClientAppService hospitalClientAppService,
            IToxicRepository toxicRepository,
            IConfiguration configuration,
            IPatientCaseRepository caseRepository, ICapPublisher capPublisher,
            IHospitalAPI hospitalAPI,
            IPacsPathologyItemRepository pacsPathologyItemRepository,
            IPacsPathologyItemNoRepository pacsPathologyItemNoRepository,
            IUserSettingRepository userSettingRepository)
        {
            _doctorsAdviceRepository = doctorsAdviceRepository;
            _prescribeRepository = prescribeRepository;
            _lisRepository = lisRepository;
            _pacsRepository = pacsRepository;
            _treatRepository = treatRepository;
            _printInfoRepository = printInfoRepository;
            _logger = logger;
            _httpContext = httpContext;
            _httpClientFactory = httpClientFactory;
            _grpcMasterDataClient = grpcMasterDataClient;
            _coronavirusRnaRepository = coronavirusRnaRepository;
            _prescriptionRepository = prescriptionRepository;
            _medDetailResultRepository = medDetailResultRepository;
            _hospitalClientAppService = hospitalClientAppService;
            _toxicRepository = toxicRepository;
            _configuration = configuration;
            _caseRepository = caseRepository;
            _capPublisher = capPublisher;
            _hospitalAPI = hospitalAPI;
            _pacsPathologyItemRepository = pacsPathologyItemRepository;
            _pacsPathologyItemNoRepository = pacsPathologyItemNoRepository;
            _personSettingRepository = userSettingRepository;
        }

        /// <summary>
        /// 打印回调操作，打印完之后调用一下，告知医嘱服务打印情况（每打印一次就记录一次）
        /// </summary>
        /// <param name="modelList">打印反馈的记录数据</param>
        /// <returns></returns> 
        [UnitOfWork]
        public async Task<bool> HasBeenPrintedAsync(List<PrintInfoDto> modelList)
        {
            List<string> prescriptionNos = new List<string>();
            foreach (PrintInfoDto item in modelList)
            {
                string[] nos = item.PrescriptionNo.Split(',', StringSplitOptions.RemoveEmptyEntries);
                prescriptionNos.AddRange(nos);
            }
            var medDetailResults = await _medDetailResultRepository.GetListAsync(c => prescriptionNos.Contains(c.ChannelNo));
            var adviceIds = medDetailResults.Select(s => s.HisNumber).ToList();
            var advices = await _doctorsAdviceRepository.GetListAsync(w => adviceIds.Contains(w.HisOrderNo));
            if (!advices.Any())
                Oh.Error("反馈的打印医嘱信息没有记录");

            advices = advices.DistinctBy(c => c.PrescriptionNo).ToList();
            List<PrintInfo> printInfos = new();
            List<PrintInfo> printInfosUpdate = new();
            var userCode = CurrentUser.UserName;
            var userName = CurrentUser.FindClaimValue("fullName");

            foreach (var item in advices)
            {
                var med = medDetailResults.FirstOrDefault(c => c.ChannelNumber == item.PrescriptionNo);
                if (!medDetailResults.Any())
                    Oh.Error("medDetailResults没有记录");
                var mods = modelList.Where(c => c.PrescriptionNo.Contains(med.ChannelNo));

                foreach (var mod in mods)
                {
                    var printInfo = await _printInfoRepository.FirstOrDefaultAsync(f => f.TemplateId == mod.TemplateId && f.PrescriptionNo == item.PrescriptionNo);
                    if (printInfo != null)
                    {
                        printInfo.SetPrintAgain(true);
                        printInfosUpdate.Add(printInfo);
                    }
                    else
                    {
                        var entity = new PrintInfo(GuidGenerator.Create(), userCode, userName, item.PrescriptionNo,
                            templateId: mod.TemplateId, false);
                        printInfos.Add(entity);
                    }
                }
            }

            if (printInfos.Any())
                await _printInfoRepository.InsertManyAsync(printInfos);

            if (printInfosUpdate.Any())
                await _printInfoRepository.UpdateManyAsync(printInfosUpdate);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// 勾选变成已打
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public async Task<bool> AddPrintAsync(List<PrintInfoDto> modelList)
        {
            if (modelList == null || !modelList.Any())
            {
                Oh.Error("请求参数为空");
            }

            string userCode = CurrentUser.UserName;
            string userName = CurrentUser.FindClaimValue("fullName");

            List<MedDetailResult> medDetailResults = await _medDetailResultRepository.GetListAsync(c => modelList.Select(s => s.PrescriptionNo).Contains(c.ChannelNo));

            List<PrintInfo> printInfos = new List<PrintInfo>();
            foreach (PrintInfoDto item in modelList)
            {
                MedDetailResult medDetailResult = medDetailResults.FirstOrDefault(x => x.ChannelNo == item.PrescriptionNo);
                if (medDetailResult == null) continue;

                PrintInfo printInfo = new PrintInfo(GuidGenerator.Create(), userCode, userName, medDetailResult.ChannelNumber, item.TemplateId);

                bool exist = await _printInfoRepository.AnyAsync(x => x.TemplateId == printInfo.TemplateId && x.PrescriptionNo == printInfo.PrescriptionNo);
                if (exist) continue;
                printInfos.Add(printInfo);
            }

            if (printInfos.Any()) await _printInfoRepository.InsertManyAsync(printInfos);
            return true;
        }

        /// <summary>
        /// 删除已打标识
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        [WebApiClientCore.Attributes.HttpPost]
        public async Task<bool> CancelPrintAsync(List<PrintInfoDto> modelList)
        {
            if (modelList == null || !modelList.Any())
            {
                Oh.Error("请求参数为空");
            }

            List<string> prescriptionNos = new List<string>();
            foreach (PrintInfoDto item in modelList)
            {
                string[] nos = item.PrescriptionNo.Split(',', StringSplitOptions.RemoveEmptyEntries);
                prescriptionNos.AddRange(nos);
            }

            List<MedDetailResult> medDetailResults = await _medDetailResultRepository.GetListAsync(c => prescriptionNos.Contains(c.ChannelNo));

            foreach (PrintInfoDto item in modelList)
            {
                IEnumerable<string> channelNumbers = medDetailResults.Where(x => item.PrescriptionNo.Contains(x.ChannelNo)).Select(x => x.ChannelNumber);
                if (!channelNumbers.Any()) continue;

                await _printInfoRepository.DeleteAsync(x => channelNumbers.Contains(x.PrescriptionNo) && x.TemplateId == item.TemplateId);
            }

            return true;
        }

        /// <summary>
        /// 勾选打印，用户决定
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="comm"></param>
        /// <returns></returns> 
        public async Task<DocumentResponseDto> PrintChecksAsync(List<Guid> ids, ECommandParam comm)
        {
            switch (comm)
            {
                case ECommandParam.GetMedicine: //处方单
                    return await GetMedicineAsync(ids);
                case ECommandParam.GetInjection: //注射单
                    return await GetInjectionAsync(ids);
                case ECommandParam.GetTransfusion: //输液单
                    return await GetTransfusionAsync(ids);
                case ECommandParam.GetLaboratory: //检验单
                    var lab = await GetLaboratoryAsync(ids);
                    await PushNovelCoronavirusRnaAsync(lab);
                    return lab;
                case ECommandParam.GetExamine: //检查单
                    return await GetExamineAsync(ids);
                case ECommandParam.GetTreat: //治疗单
                    return await GetTreatAsync(ids);
                case ECommandParam.GetAerosolization: //物化单
                    return await GetAerosolizationAsync(ids);
                case ECommandParam.GetPremunitive: //预防接种单
                    return await GetPremunitiveAsync(ids);
                default: //不符合规则返回空对象
                    return await Task.FromResult(new DocumentResponseDto());
            }
        }

        /// <summary>
        /// 获取检验条码打印数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [NonUnify]
        public async Task<DocumentResponseDto> GetPathologyNoDataAsync(int id, string token = "")
        {
            DocumentResponseDto data = new DocumentResponseDto();
            try
            {
                PacsPathologyItemNo pacsPathologyItemNo = await _pacsPathologyItemNoRepository.FindAsync(x => x.Id == id);
                if (pacsPathologyItemNo == null) { return data; }

                List<PacsAdviceDto> pacsAdviceDtos = await GetExamineAsync(pacsPathologyItemNo.PacsId);
                Guid doctorsAdviceId = pacsAdviceDtos.FirstOrDefault().Id;
                DoctorsAdvice doctorsAdvice = await _doctorsAdviceRepository.FindAsync(x => x.Id == doctorsAdviceId);
                AdmissionRecordDto patientInfo = await GetPatientInfoAsync(doctorsAdvice.PIID, token);
                PacsItemNoDto pacsItemNoDto = new PacsItemNoDto()
                {
                    Id = pacsPathologyItemNo.Id,
                    PacsName = doctorsAdvice?.Name,
                    PathologyName = pacsPathologyItemNo.SpecimenName,
                };

                data.Push(patientInfo).Push(pacsAdviceDtos).Push(pacsItemNoDto);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError("获取检验条码打印数据异常：" + ex.Message);
                throw Oh.Error("获取检验条码打印数据异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 获取检查数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<List<PacsAdviceDto>> GetExamineAsync(Guid id)
        {
            List<ERecipeStatus> status = new List<ERecipeStatus>
                { ERecipeStatus.Submitted, ERecipeStatus.Confirmed, ERecipeStatus.Executed, ERecipeStatus.PayOff };
            var query = (from a in (await _doctorsAdviceRepository.GetQueryableAsync())
                    .Where(w => w.ItemType == EDoctorsAdviceItemType.Pacs && status.Contains(w.Status))
                         join p in (await _pacsRepository.GetQueryableAsync()).Where(x => x.Id == id)
                             on a.Id equals p.DoctorsAdviceId
                         join m in (await _medDetailResultRepository.GetQueryableAsync())
                             on a.PrescriptionNo equals m.ChannelNumber
                         join i in (await _printInfoRepository.GetQueryableAsync())
                             on m.ChannelNumber equals i.PrescriptionNo into print
                         from i in print.DefaultIfEmpty()
                         select new PacsAdviceDto
                         {
                             #region list-data

                             Id = a.Id,
                             PIID = a.PIID,
                             Code = a.Code,
                             Name = a.Name + ";",
                             CategoryCode = a.CategoryCode,
                             CategoryName = a.CategoryName,
                             IsBackTracking = a.IsBackTracking,
                             PrescriptionNo = m.HisNumber, //取医院的处方单号
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
                             CatalogCode = p.CatalogCode,
                             PacsId = p.Id,
                             CatalogName = p.CatalogName,
                             FirstCatalogCode = p.FirstCatalogCode,
                             FirstCatalogName = p.FirstCatalogName,
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
                             ExecDeptCode = a.ExecDeptCode,
                             ExecDeptName = a.ExecDeptName,
                             ExecutorCode = a.ExecutorCode,
                             ExecutorName = a.ExecutorName,
                             ExecTime = a.ExecTime,
                             AddCard = p.AddCard,
                             IsRecipePrinted = a.IsRecipePrinted,
                             MedicalNo = m.MedicalNo,
                             ChannelNo = m.ChannelNo,
                             ChannelNumber = m.ChannelNumber,
                             HisNumber = m.HisNumber,
                             Lgjkzx_payurl = m.LgjkzxPayurl,
                             Lgzxyy_payurl = m.LgzxyyPayurl,
                             MedType = m.MedType,
                             MedNature = m.MedNature,
                             MedFee = m.MedFee,
                             GuideName = p.GuideName,
                             ExamTitle = p.ExamTitle,
                             ReservationPlace = p.ReservationPlace,
                             ResultTime = m.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                             PrintStr = i == null ? "" : "报销单据"
                             #endregion
                         }).AsQueryable();

            List<PacsAdviceDto> pacsAdviceDtos = await query.ToListAsync();

            ExamCodeRequest examCodeRequest = new ExamCodeRequest();
            examCodeRequest.Code.AddRange(pacsAdviceDtos.Select(s => s.Code));
            ExamProjectsResponse examProjectsResponse = _grpcMasterDataClient.GetExamsByCodes(examCodeRequest);

            IEnumerable<Guid> pacsIds = pacsAdviceDtos.Select(x => x.PacsId);
            List<PacsPathologyItem> pacsPathologyItems = await _pacsPathologyItemRepository.GetListAsync(x => pacsIds.Contains(x.PacsId));
            List<PacsPathologyItemNo> pacsPathologyItemNos = await _pacsPathologyItemNoRepository.GetListAsync(x => pacsIds.Contains(x.PacsId));

            foreach (PacsAdviceDto pacsAdviceDto in pacsAdviceDtos)
            {
                GrpcExamProjectModel examProject = examProjectsResponse.ExamProjects.FirstOrDefault(x => x.ProjectCode == pacsAdviceDto.Code);
                if (examProject != null)
                {
                    pacsAdviceDto.GuideName = examProject.GuideName;
                    pacsAdviceDto.ReservationTime = examProject.ReservationTime;
                    if (string.IsNullOrEmpty(pacsAdviceDto.ReservationPlace))
                    {
                        pacsAdviceDto.ReservationPlace = examProject.ReservationPlace;
                    }
                }

                PacsPathologyItem pacsPathologyItem = pacsPathologyItems.FirstOrDefault(x => x.PacsId == pacsAdviceDto.PacsId);
                if (pacsPathologyItem != null)
                {
                    pacsAdviceDto.Specimen = pacsPathologyItem.Specimen;
                    pacsAdviceDto.DrawMaterialsPart = pacsPathologyItem.DrawMaterialsPart;
                    pacsAdviceDto.SpecimenQty = pacsPathologyItem.SpecimenQty;
                    pacsAdviceDto.LeaveTime = pacsPathologyItem.LeaveTime;
                    pacsAdviceDto.RegularTime = pacsPathologyItem.RegularTime;
                    pacsAdviceDto.SpecificityInfect = pacsPathologyItem.SpecificityInfect;
                    pacsAdviceDto.ApplyForObjective = pacsPathologyItem.ApplyForObjective;
                    pacsAdviceDto.Symptom = pacsPathologyItem.Symptom;
                }

                pacsAdviceDto.pacsPathologyItemNoDtos = new List<PacsPathologyItemNoDto>();
                List<PacsPathologyItemNo> pacsNos = pacsPathologyItemNos.Where(x => x.PacsId == pacsAdviceDto.PacsId).ToList();

                foreach (PacsPathologyItemNo pacsNo in pacsNos)
                {
                    PacsPathologyItemNoDto pacsPathologyItemNo = new PacsPathologyItemNoDto()
                    {
                        Id = pacsNo.Id,
                        SpecimenName = pacsNo.SpecimenName
                    };
                    pacsAdviceDto.pacsPathologyItemNoDtos.Add(pacsPathologyItemNo);
                }
            }

            return pacsAdviceDtos;
        }

        /// <summary>
        /// 更新已打
        /// </summary>
        /// <param name="pacsItemNoDtos"></param>
        /// <returns></returns>
        [WebApiClientCore.Attributes.HttpPost]
        public async Task<bool> PathologyNoPrintInfoAsync(List<PacsItemNoDto> pacsItemNoDtos)
        {
            if (pacsItemNoDtos == null || !pacsItemNoDtos.Any())
            {
                Oh.Error("请求参数为空");
            }

            IEnumerable<int> ids = pacsItemNoDtos.Select(x => x.Id);
            List<PacsPathologyItemNo> pacsPathologyItemNos = await _pacsPathologyItemNoRepository.GetListAsync(x => ids.Contains(x.Id));
            foreach (PacsPathologyItemNo item in pacsPathologyItemNos)
            {
                PacsItemNoDto pacsItemNoDto = pacsItemNoDtos.FirstOrDefault(x => x.Id == item.Id);
                if (pacsItemNoDto == null) continue;

                item.IsPrint = pacsItemNoDto.IsPrint;
            }

            if (pacsPathologyItemNos.Any()) await _pacsPathologyItemNoRepository.UpdateManyAsync(pacsPathologyItemNos);

            return true;
        }

        /// <summary>
        /// 检验单获取新冠rna申请单数据
        /// </summary>
        /// <param name="lab"></param>
        // <returns></returns>
        private async Task PushNovelCoronavirusRnaAsync(DocumentResponseDto lab)
        {
            if (lab.Lises.Any(x => x.AddCard == "14"))
            {
                var novelCoronavirus = await (await _coronavirusRnaRepository.GetQueryableAsync()).Where(t =>
                    lab.Lises.Where(x => x.AddCard == "14")
                        .Select(s => s.Id).Contains(t.DoctorsAdviceId)).ToListAsync();
                lab.Push(ObjectMapper
                    .Map<List<NovelCoronavirusRna>, List<NovelCoronavirusRnaDto>>(novelCoronavirus));
            }
        }

        /// <summary>
        /// 处方单
        /// </summary>
        /// <returns></returns> 
        private async Task<DocumentResponseDto> GetMedicineAsync(List<Guid> ids)
        {
            var data = await (await QueryAdviceAsync(ids))
                .OrderBy(o => o.RecipeNo)
                .ThenBy(o => o.RecipeGroupNo).ToListAsync();
            return new DocumentResponseDto(data);
        }

        /// <summary>
        /// 注射单
        /// </summary>
        /// <returns></returns> 
        private async Task<DocumentResponseDto> GetInjectionAsync(Guid piid, EPlatformType platformType)
        {
            var data = await (await QueryAdviceAsync(piid, platformType)).Where(w => w.UsageName.Contains("注射")).ToListAsync();
            return new DocumentResponseDto(data);
        }

        /// <summary>
        /// 注射单
        /// </summary>
        /// <returns></returns> 
        private async Task<DocumentResponseDto> GetInjectionAsync(List<Guid> ids)
        {
            var data = await (await QueryAdviceAsync(ids)).Where(w => w.UsageName.Contains("注射")).ToListAsync();
            return new DocumentResponseDto(data);
        }

        /// <summary>
        /// 输液单
        /// </summary>
        /// <returns></returns> 
        private async Task<DocumentResponseDto> GetTransfusionAsync(Guid piid, EPlatformType platformType)
        {
            //查询原始数据
            var data = await (await QueryAdviceAsync(piid, platformType)).Where(w => w.UsageName.Contains("输液")).ToListAsync();
            return new DocumentResponseDto(data);
        }

        /// <summary>
        /// 输液单
        /// </summary>
        /// <returns></returns> 
        private async Task<DocumentResponseDto> GetTransfusionAsync(List<Guid> ids)
        {
            //查询原始数据
            var data = await (await QueryAdviceAsync(ids)).Where(w => w.UsageName.Contains("输液")).ToListAsync();
            return new DocumentResponseDto(data);
        }

        /// <summary>
        /// 检验单
        /// </summary>
        /// <returns></returns> 
        private async Task<List<LisAdviceDto>> GetLaboratoryAsync(Guid piid, EPlatformType platformType,
            int reprint = -1, string prescriptionNo = "", Guid templateId = default, int forcePrintFlag = -1)
        {
            var status = new List<ERecipeStatus>
                { ERecipeStatus.Submitted, ERecipeStatus.Confirmed, ERecipeStatus.Executed, ERecipeStatus.PayOff };
            var query = from a in (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w =>
                    w.PIID == piid && w.PlatformType == platformType && w.ItemType == EDoctorsAdviceItemType.Lis &&
                    status.Contains(w.Status))
                        join l in (await _lisRepository.GetQueryableAsync())
                            on a.Id equals l.DoctorsAdviceId
                        join m in (await _medDetailResultRepository.GetQueryableAsync())
                                .WhereIf(!string.IsNullOrEmpty(prescriptionNo), x => prescriptionNo.Contains(x.HisNumber))
                            on a.PrescriptionNo equals m.ChannelNumber
                        join i in (await _printInfoRepository.GetQueryableAsync()).WhereIf(templateId != Guid.Empty, x => x.TemplateId == templateId)
                            on m.ChannelNumber equals i.PrescriptionNo into print
                        from i in print.DefaultIfEmpty()
                        select new LisAdviceDto
                        {
                            #region list-data

                            Id = a.Id,
                            PIID = a.PIID,
                            Code = a.Code,
                            Name = a.Name,
                            CategoryCode = a.CategoryCode,
                            CategoryName = a.CategoryName,
                            IsBackTracking = a.IsBackTracking,
                            PrescriptionNo = m.HisNumber, //取医院的处方单号
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
                            ExecDeptCode = a.ExecDeptCode,
                            ExecDeptName = a.ExecDeptName,
                            ExecutorCode = a.ExecutorCode,
                            ExecutorName = a.ExecutorName,
                            ExecTime = a.ExecTime,
                            AddCard = l.AddCard,
                            IsRecipePrinted = a.IsRecipePrinted,
                            MedicalNo = m.MedicalNo,
                            ChannelNo = m.ChannelNo,
                            ChannelNumber = m.ChannelNumber,
                            HisNumber = m.HisNumber,
                            Lgjkzx_payurl = m.LgjkzxPayurl,
                            Lgzxyy_payurl = m.LgzxyyPayurl,
                            MedType = m.MedType,
                            MedNature = m.MedNature,
                            MedFee = m.MedFee,
                            GuideName = l.GuideName,
                            GuideCatelogName = l.GuideCatelogName,
                            ResultTime = m.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            PrintStr = forcePrintFlag == 0 || i == null ? "" : "报销单据"
                            #endregion
                        };
            var result = await query.WhereIf(reprint == 0, w => w.PrintStr == "").WhereIf(reprint == 1, w => w.PrintStr != "").ToListAsync();

            GetLabReportInfoCodes labReportInfoCodes = new GetLabReportInfoCodes();
            labReportInfoCodes.Code.AddRange(result.Select(s => s.Code));
            LabReportInfos labReportInfos = _grpcMasterDataClient.GetLabReportInfo(labReportInfoCodes);
            foreach (var item in result)
            {
                if (string.IsNullOrEmpty(item.GuideName) || string.IsNullOrEmpty(item.GuideCatelogName))
                {
                    var labReportInfo = labReportInfos.LabReportInfolist.FirstOrDefault(x => x.Code == item.Code);
                    if (labReportInfo != null)
                    {
                        item.GuideName = labReportInfo.Remark;
                        item.GuideCatelogName = labReportInfo.CatelogName;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 检验单
        /// </summary>
        /// <returns></returns> 
        private async Task<DocumentResponseDto> GetLaboratoryAsync(List<Guid> ids)
        {
            var status = new List<ERecipeStatus>
                { ERecipeStatus.Submitted, ERecipeStatus.Confirmed, ERecipeStatus.Executed };
            var query = from a in (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w =>
                    ids.Contains(w.Id) && w.ItemType == EDoctorsAdviceItemType.Lis && status.Contains(w.Status))
                        join l in (await _lisRepository.GetQueryableAsync())
                            on a.Id equals l.DoctorsAdviceId
                        join m in (await _medDetailResultRepository.GetQueryableAsync())
                            on a.PrescriptionNo equals m.ChannelNumber
                        select new LisAdviceDto
                        {
                            #region list-data

                            Id = a.Id,
                            PIID = a.PIID,
                            Code = a.Code,
                            Name = a.Name,
                            CategoryCode = a.CategoryCode,
                            CategoryName = a.CategoryName,
                            IsBackTracking = a.IsBackTracking,
                            PrescriptionNo = m.HisNumber, //取医院的处方单号
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
                            ExecDeptCode = a.ExecDeptCode,
                            ExecDeptName = a.ExecDeptName,
                            ExecutorCode = a.ExecutorCode,
                            ExecutorName = a.ExecutorName,
                            ExecTime = a.ExecTime,
                            AddCard = l.AddCard,
                            IsRecipePrinted = a.IsRecipePrinted,

                            MedicalNo = m.MedicalNo,
                            ChannelNo = m.ChannelNo,
                            ChannelNumber = m.ChannelNumber,
                            HisNumber = m.HisNumber,
                            Lgjkzx_payurl = m.LgjkzxPayurl,
                            Lgzxyy_payurl = m.LgzxyyPayurl,
                            MedType = m.MedType,
                            MedNature = m.MedNature,
                            MedFee = m.MedFee,
                            #endregion
                        };
            var data = await query.ToListAsync();
            return new DocumentResponseDto(data);
        }

        /// <summary>
        /// 检查单
        /// </summary>
        /// <returns></returns>
        private async Task<List<PacsAdviceDto>> GetExamineAsync(Guid piid, EPlatformType platformType, int reprint = -1, string prescriptionNo = "", Guid templateId = default, int forcePrintFlag = -1)
        {
            List<ERecipeStatus> status = new List<ERecipeStatus>
                { ERecipeStatus.Submitted, ERecipeStatus.Confirmed, ERecipeStatus.Executed, ERecipeStatus.PayOff };
            var query = (from a in (await _doctorsAdviceRepository.GetQueryableAsync())
                    .Where(w => w.PIID == piid && w.PlatformType == platformType &&
                                w.ItemType == EDoctorsAdviceItemType.Pacs && status.Contains(w.Status))
                         join p in (await _pacsRepository.GetQueryableAsync())
                             on a.Id equals p.DoctorsAdviceId
                         join m in (await _medDetailResultRepository.GetQueryableAsync())
                                 .WhereIf(!string.IsNullOrEmpty(prescriptionNo), x => prescriptionNo.Contains(x.HisNumber))
                             on a.PrescriptionNo equals m.ChannelNumber
                         join i in (await _printInfoRepository.GetQueryableAsync()).WhereIf(templateId != Guid.Empty, x => x.TemplateId == templateId)
                             on m.ChannelNumber equals i.PrescriptionNo into print
                         from i in print.DefaultIfEmpty()
                         select new PacsAdviceDto
                         {
                             #region list-data

                             Id = a.Id,
                             PIID = a.PIID,
                             Code = a.Code,
                             Name = a.Name,
                             CategoryCode = a.CategoryCode,
                             CategoryName = a.CategoryName,
                             IsBackTracking = a.IsBackTracking,
                             PrescriptionNo = m.HisNumber, //取医院的处方单号
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
                             CatalogCode = p.CatalogCode,
                             PacsId = p.Id,
                             CatalogName = p.CatalogName,
                             FirstCatalogCode = p.FirstCatalogCode,
                             FirstCatalogName = p.FirstCatalogName,
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
                             ExecDeptCode = a.ExecDeptCode,
                             ExecDeptName = a.ExecDeptName,
                             ExecutorCode = a.ExecutorCode,
                             ExecutorName = a.ExecutorName,
                             ExecTime = a.ExecTime,
                             AddCard = p.AddCard,
                             IsRecipePrinted = a.IsRecipePrinted,
                             MedicalNo = m.MedicalNo,
                             ChannelNo = m.ChannelNo,
                             ChannelNumber = m.ChannelNumber,
                             HisNumber = m.HisNumber,
                             Lgjkzx_payurl = m.LgjkzxPayurl,
                             Lgzxyy_payurl = m.LgzxyyPayurl,
                             MedType = m.MedType,
                             MedNature = m.MedNature,
                             MedFee = m.MedFee,
                             GuideName = p.GuideName,
                             ExamTitle = p.ExamTitle,
                             ReservationPlace = p.ReservationPlace,
                             ResultTime = m.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                             PrintStr = forcePrintFlag == 0 || i == null ? "" : "报销单据"
                             #endregion
                         }).AsQueryable();

            List<PacsAdviceDto> pacsAdviceDtos = await query.WhereIf(reprint == 0, w => w.PrintStr == "").WhereIf(reprint == 1, w => w.PrintStr != "").ToListAsync();

            ExamCodeRequest examCodeRequest = new ExamCodeRequest();
            examCodeRequest.Code.AddRange(pacsAdviceDtos.Select(s => s.Code));
            ExamProjectsResponse examProjectsResponse = _grpcMasterDataClient.GetExamsByCodes(examCodeRequest);

            IEnumerable<Guid> pacsIds = pacsAdviceDtos.Select(x => x.PacsId);
            List<PacsPathologyItem> pacsPathologyItems = await _pacsPathologyItemRepository.GetListAsync(x => pacsIds.Contains(x.PacsId));
            List<PacsPathologyItemNo> pacsPathologyItemNos = await _pacsPathologyItemNoRepository.GetListAsync(x => pacsIds.Contains(x.PacsId));

            foreach (PacsAdviceDto pacsAdviceDto in pacsAdviceDtos)
            {
                GrpcExamProjectModel examProject = examProjectsResponse.ExamProjects.FirstOrDefault(x => x.ProjectCode == pacsAdviceDto.Code);
                if (examProject != null)
                {
                    pacsAdviceDto.GuideName = examProject.GuideName;
                    pacsAdviceDto.ReservationTime = examProject.ReservationTime;
                    if (string.IsNullOrEmpty(pacsAdviceDto.ReservationPlace))
                    {
                        pacsAdviceDto.ReservationPlace = examProject.ReservationPlace;
                    }
                }

                PacsPathologyItem pacsPathologyItem = pacsPathologyItems.FirstOrDefault(x => x.PacsId == pacsAdviceDto.PacsId);
                if (pacsPathologyItem != null)
                {
                    pacsAdviceDto.Specimen = pacsPathologyItem.Specimen;
                    pacsAdviceDto.DrawMaterialsPart = pacsPathologyItem.DrawMaterialsPart;
                    pacsAdviceDto.SpecimenQty = pacsPathologyItem.SpecimenQty;
                    pacsAdviceDto.LeaveTime = pacsPathologyItem.LeaveTime;
                    pacsAdviceDto.RegularTime = pacsPathologyItem.RegularTime;
                    pacsAdviceDto.SpecificityInfect = pacsPathologyItem.SpecificityInfect;
                    pacsAdviceDto.ApplyForObjective = pacsPathologyItem.ApplyForObjective;
                    pacsAdviceDto.Symptom = pacsPathologyItem.Symptom;
                }

                pacsAdviceDto.pacsPathologyItemNoDtos = new List<PacsPathologyItemNoDto>();
                List<PacsPathologyItemNo> pacsNos = pacsPathologyItemNos.Where(x => x.PacsId == pacsAdviceDto.PacsId).ToList();

                foreach (PacsPathologyItemNo pacsNo in pacsNos)
                {
                    PacsPathologyItemNoDto pacsPathologyItemNo = new PacsPathologyItemNoDto()
                    {
                        Id = pacsNo.Id,
                        SpecimenName = pacsNo.SpecimenName
                    };
                    pacsAdviceDto.pacsPathologyItemNoDtos.Add(pacsPathologyItemNo);
                }
            }

            return pacsAdviceDtos;
        }

        /// <summary>
        /// 检查单
        /// </summary>
        /// <returns></returns>
        private async Task<DocumentResponseDto> GetExamineAsync(List<Guid> ids)
        {
            List<ERecipeStatus> status = new List<ERecipeStatus>
                { ERecipeStatus.Submitted, ERecipeStatus.Confirmed, ERecipeStatus.Executed };
            var query = (from a in (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w =>
                    ids.Contains(w.Id) && w.ItemType == EDoctorsAdviceItemType.Pacs && status.Contains(w.Status))
                         join p in (await _pacsRepository.GetQueryableAsync())
                             on a.Id equals p.DoctorsAdviceId
                         join m in (await _medDetailResultRepository.GetQueryableAsync())
                             on a.PrescriptionNo equals m.ChannelNumber
                         select new PacsAdviceDto
                         {
                             #region list-data

                             Id = a.Id,
                             PIID = a.PIID,
                             Code = a.Code,
                             Name = a.Name,
                             CategoryCode = a.CategoryCode,
                             CategoryName = a.CategoryName,
                             IsBackTracking = a.IsBackTracking,
                             PrescriptionNo = m.HisNumber, //取医院的处方单号
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
                             ExecDeptCode = a.ExecDeptCode,
                             ExecDeptName = a.ExecDeptName,
                             ExecutorCode = a.ExecutorCode,
                             ExecutorName = a.ExecutorName,
                             ExecTime = a.ExecTime,
                             AddCard = p.AddCard,
                             IsRecipePrinted = a.IsRecipePrinted,

                             MedicalNo = m.MedicalNo,
                             ChannelNo = m.ChannelNo,
                             ChannelNumber = m.ChannelNumber,
                             HisNumber = m.HisNumber,
                             Lgjkzx_payurl = m.LgjkzxPayurl,
                             Lgzxyy_payurl = m.LgzxyyPayurl,
                             MedType = m.MedType,
                             MedNature = m.MedNature,
                             MedFee = m.MedFee,
                             #endregion
                         }).AsQueryable();
            var data = await query.ToListAsync();
            return new DocumentResponseDto(data);
        }

        /// <summary>
        /// 治疗单
        /// </summary>
        /// <returns></returns> 
        private async Task<List<TreatAdviceDto>> GetTreatAsync(Guid piid, EPlatformType platformType,
            int reprint = -1, string prescriptionNo = "", Guid templateId = default, int forcePrintFlag = -1)
        {
            //查询原始数据
            return await (await QueryTreatAdviceAsync(piid, platformType, reprint, prescriptionNo, templateId: templateId, forcePrintFlag: forcePrintFlag)).ToListAsync();
        }

        /// <summary>
        /// 治疗单
        /// </summary>
        /// <returns></returns> 
        private async Task<DocumentResponseDto> GetTreatAsync(List<Guid> ids)
        {
            //查询原始数据
            var data = await (await QueryTreatAdviceAsync(ids)).ToListAsync();
            return new DocumentResponseDto(data);
        }

        /// <summary>
        /// 雾化单
        /// </summary>
        /// <returns></returns> 
        private async Task<DocumentResponseDto> GetAerosolizationAsync(List<Guid> ids)
        {
            await Task.CompletedTask;
            return new DocumentResponseDto();
        }

        /// <summary>
        /// 预防接种单
        /// </summary>
        /// <returns></returns> 
        private async Task<DocumentResponseDto> GetPremunitiveAsync(Guid piid, EPlatformType platformType)
        {
            //预防接种单：给出打印样式，不设置内容先； 
            await Task.CompletedTask;
            return new DocumentResponseDto();
        }

        /// <summary>
        /// 预防接种单
        /// </summary>
        /// <returns></returns> 
        private async Task<DocumentResponseDto> GetPremunitiveAsync(List<Guid> ids)
        {
            //预防接种单：给出打印样式，不设置内容先； 
            await Task.CompletedTask;
            return new DocumentResponseDto();
        }

        /// <summary>
        /// 构建查询药方医嘱的基础内容
        /// </summary>
        /// <param name="piid"></param>
        /// <param name="platformType"></param>
        /// <returns></returns>
        private async Task<IQueryable<MedicineAdviceDto>> QueryAdviceAsync(Guid piid, EPlatformType platformType)
        {
            var status = new List<ERecipeStatus>
                { ERecipeStatus.Submitted, ERecipeStatus.Confirmed, ERecipeStatus.Executed };
            var query = (from a in (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w =>
                    w.PIID == piid && w.PlatformType == platformType &&
                    w.ItemType == EDoctorsAdviceItemType.Prescribe && status.Contains(w.Status))
                         join p in (await _prescribeRepository.GetQueryableAsync())
                             on a.Id equals p.DoctorsAdviceId
                         join m in (await _medDetailResultRepository.GetQueryableAsync())
                             on a.PrescriptionNo equals m.ChannelNumber
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
                             PrescriptionNo = m.HisNumber, //取医院的处方单号
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
                             ExecDeptCode = a.ExecDeptCode,
                             ExecDeptName = a.ExecDeptName,
                             ExecutorCode = a.ExecutorCode,
                             ExecutorName = a.ExecutorName,
                             ExecTime = a.ExecTime,
                             IsRecipePrinted = a.IsRecipePrinted,
                             LimitType = p.LimitType,
                             SkinTestSignChoseResult = p.SkinTestSignChoseResult,
                             RestrictedDrugs = p.RestrictedDrugs,

                             MedicalNo = m.MedicalNo,
                             ChannelNo = m.ChannelNo,
                             ChannelNumber = m.ChannelNumber,
                             HisNumber = m.HisNumber,
                             Lgjkzx_payurl = m.LgjkzxPayurl,
                             Lgzxyy_payurl = m.LgzxyyPayurl,
                             MedType = m.MedType,
                             MedNature = m.MedNature,
                             MedFee = m.MedFee,

                             #endregion
                         }).AsQueryable();
            return await Task.FromResult(query);
        }

        /// <summary>
        /// 构建查询药方医嘱的基础内容
        /// </summary> 
        /// <param name="ids"></param>
        /// <returns></returns>
        private async Task<IQueryable<MedicineAdviceDto>> QueryAdviceAsync(List<Guid> ids)
        {
            var status = new List<ERecipeStatus>
                { ERecipeStatus.Submitted, ERecipeStatus.Confirmed, ERecipeStatus.Executed };
            var query = (from a in (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w =>
                    ids.Contains(w.Id) && w.ItemType == EDoctorsAdviceItemType.Prescribe && status.Contains(w.Status))
                         join p in (await _prescribeRepository.GetQueryableAsync())
                             on a.Id equals p.DoctorsAdviceId
                         join m in (await _medDetailResultRepository.GetQueryableAsync())
                             on a.PrescriptionNo equals m.ChannelNumber
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
                             PrescriptionNo = m.HisNumber, //取医院的处方单号
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
                             MedicineProperty = p.MedicineProperty,
                             ToxicProperty = p.ToxicProperty,
                             IsCriticalPrescription = p.IsCriticalPrescription,
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
                             ExecDeptCode = a.ExecDeptCode,
                             ExecDeptName = a.ExecDeptName,
                             ExecutorCode = a.ExecutorCode,
                             ExecutorName = a.ExecutorName,
                             ExecTime = a.ExecTime,
                             IsRecipePrinted = a.IsRecipePrinted,
                             LimitType = p.LimitType,
                             RestrictedDrugs = p.RestrictedDrugs,
                             SkinTestSignChoseResult = p.SkinTestSignChoseResult,
                             MedicalNo = m.MedicalNo,
                             ChannelNo = m.ChannelNo,
                             ChannelNumber = m.ChannelNumber,
                             HisNumber = m.HisNumber,
                             Lgjkzx_payurl = m.LgjkzxPayurl,
                             Lgzxyy_payurl = m.LgzxyyPayurl,
                             MedType = m.MedType,
                             MedNature = m.MedNature,
                             MedFee = m.MedFee,
                             #endregion
                         }).AsQueryable();
            return await Task.FromResult(query);
        }

        /// <summary>
        /// 构建查询诊疗的基础内容
        /// </summary>
        /// <param name="piid"></param>
        /// <param name="platformType"></param>
        /// <param name="reprint"></param>
        /// <param name="prescriptionNo"></param>
        /// <param name="templateId"></param>
        /// <param name="forcePrintFlag"></param>
        /// <returns></returns>
        private async Task<IQueryable<TreatAdviceDto>> QueryTreatAdviceAsync(Guid piid, EPlatformType platformType, int reprint = -1, string prescriptionNo = "", Guid templateId = default,
            int forcePrintFlag = -1)
        {
            List<ERecipeStatus> status = new List<ERecipeStatus>
                { ERecipeStatus.Submitted, ERecipeStatus.Confirmed, ERecipeStatus.Executed,ERecipeStatus.PayOff };

            bool isPrintAdditionalTreat = true;
            UserSetting setting = await _personSettingRepository.FindAsync(x => x.UserName == "system" && x.Code == "PrintAdditionalTreat");
            if (setting is { Value: "true" })
            {
                isPrintAdditionalTreat = false;
            }

            var query = (from a in (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w =>
                    w.PIID == piid && w.PlatformType == platformType && w.ItemType == EDoctorsAdviceItemType.Treat && w.CategoryCode == "Treat" &&
                    status.Contains(w.Status)).WhereIf(isPrintAdditionalTreat, x => x.Additional != 1)
                         join t in (await _treatRepository.GetQueryableAsync())
                             on a.Id equals t.DoctorsAdviceId
                         join m in (await _medDetailResultRepository.GetQueryableAsync())
                                 .WhereIf(!string.IsNullOrEmpty(prescriptionNo), x => prescriptionNo.Contains(x.HisNumber))
                             on a.PrescriptionNo equals m.ChannelNumber
                         join i in (await _printInfoRepository.GetQueryableAsync()).WhereIf(templateId != Guid.Empty, x => x.TemplateId == templateId)
                             on m.ChannelNumber equals i.PrescriptionNo into print
                         from i in print.DefaultIfEmpty()
                         select new TreatAdviceDto
                         {
                             #region list-data

                             Id = a.Id,
                             PIID = a.PIID,
                             Code = a.Code,
                             Name = a.Name,
                             CategoryCode = a.CategoryCode,
                             CategoryName = a.CategoryName,
                             IsBackTracking = a.IsBackTracking,
                             PrescriptionNo = m.HisNumber, //取医院的处方单号
                             RecipeNo = a.RecipeNo,
                             RecipeGroupNo = a.RecipeGroupNo,
                             ApplyTime = a.ApplyTime,
                             ApplyTimeToString = a.ApplyTime.ToString("yyyy-MM-dd HH:mm:ss"),
                             IsAdditionalPrice = a.IsAdditionalPrice,
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
                             OtherPrice = t.OtherPrice,
                             Additional = t.Additional,
                             FrequencyCode = t.FrequencyCode,
                             FrequencyName = t.FrequencyName,
                             FeeTypeMainCode = t.FeeTypeMainCode,
                             FeeTypeSubCode = t.FeeTypeSubCode,
                             Specification = t.Specification,
                             ExecDeptCode = a.ExecDeptCode,
                             ExecDeptName = a.ExecDeptName,
                             ExecutorCode = a.ExecutorCode,
                             ExecutorName = a.ExecutorName,
                             ExecTime = a.ExecTime,
                             IsRecipePrinted = a.IsRecipePrinted,
                             MedicalNo = m.MedicalNo,
                             ChannelNo = m.ChannelNo,
                             ChannelNumber = m.ChannelNumber,
                             HisNumber = m.HisNumber,
                             Lgjkzx_payurl = m.LgjkzxPayurl,
                             Lgzxyy_payurl = m.LgzxyyPayurl,
                             MedType = m.MedType,
                             MedNature = m.MedNature,
                             MedFee = m.MedFee,
                             ResultTime = m.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                             PrintStr = forcePrintFlag == 0 || i == null ? "" : "报销治疗单"
                             #endregion
                         }).AsQueryable();

            return await Task.FromResult(query.WhereIf(reprint == 0, w => w.PrintStr == "").WhereIf(reprint == 1, w => w.PrintStr != "").AsQueryable());
        }

        /// <summary>
        /// 构建查询诊疗的基础内容
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        private async Task<IQueryable<TreatAdviceDto>> QueryTreatAdviceAsync(List<Guid> ids)
        {
            List<ERecipeStatus> status = new List<ERecipeStatus>
                { ERecipeStatus.Submitted, ERecipeStatus.Confirmed, ERecipeStatus.Executed };
            var query = (from a in (await _doctorsAdviceRepository.GetQueryableAsync()).Where(w =>
                    ids.Contains(w.Id) && w.ItemType == EDoctorsAdviceItemType.Treat &&
                    status.Contains(w.Status))
                         join t in (await _treatRepository.GetQueryableAsync())
                             on a.Id equals t.DoctorsAdviceId
                         join m in (await _medDetailResultRepository.GetQueryableAsync())
                             on a.PrescriptionNo equals m.ChannelNumber
                         select new TreatAdviceDto
                         {
                             #region list-data

                             Id = a.Id,
                             PIID = a.PIID,
                             Code = a.Code,
                             Name = a.Name,
                             CategoryCode = a.CategoryCode,
                             CategoryName = a.CategoryName,
                             IsBackTracking = a.IsBackTracking,
                             PrescriptionNo = m.HisNumber, //取医院的处方单号
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

                             OtherPrice = t.OtherPrice,
                             FrequencyCode = t.FrequencyCode,
                             FrequencyName = t.FrequencyName,
                             FeeTypeMainCode = t.FeeTypeMainCode,
                             FeeTypeSubCode = t.FeeTypeSubCode,
                             Specification = t.Specification,
                             ExecDeptCode = a.ExecDeptCode,
                             ExecDeptName = a.ExecDeptName,
                             ExecutorCode = a.ExecutorCode,
                             ExecutorName = a.ExecutorName,
                             ExecTime = a.ExecTime,
                             IsRecipePrinted = a.IsRecipePrinted,


                             MedicalNo = m.MedicalNo,
                             ChannelNo = m.ChannelNo,
                             ChannelNumber = m.ChannelNumber,
                             HisNumber = m.HisNumber,
                             Lgjkzx_payurl = m.LgjkzxPayurl,
                             Lgzxyy_payurl = m.LgzxyyPayurl,
                             MedType = m.MedType,
                             MedNature = m.MedNature,
                             MedFee = m.MedFee,
                             #endregion
                         }).AsQueryable();

            return await Task.FromResult(query);
        }
    }
}