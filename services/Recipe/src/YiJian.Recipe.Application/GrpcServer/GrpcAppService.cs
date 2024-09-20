using Grpc.Core;
using MasterDataService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using YiJian.DoctorsAdvices.Enums;
using YiJian.Documents.Dto;
using YiJian.ECIS.Grpc;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.Recipe;
using YiJian.Recipes;
using YiJian.Recipes.DoctorsAdvices.Contracts;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.GrpcServer
{
    /// <summary>
    /// GrpcAppService
    /// </summary>
    public class GrpcAppService : GrpcRecipe.GrpcRecipeBase, IGrpcAppService
    {
        private IObjectMapper ObjectMapper;
        private IUnitOfWorkManager UnitOfWorkManager;
        private readonly IToxicRepository _toxicRepository;
        private readonly ILogger<GrpcAppService> _logger;
        private readonly IOperationApplyRepository _operationApplyRepository;
        private readonly INovelCoronavirusRnaRepository _coronavirusRnaRepository;
        private readonly IDoctorsAdviceRepository _doctorsAdviceRepository;
        private readonly IPrescribeRepository _prescribeRepository;
        private readonly ILisRepository _lisRepository;
        private readonly IPacsRepository _pacsRepository;
        private readonly IPrintInfoRepository _printInfoRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly GrpcMasterData.GrpcMasterDataClient _grpcMasterDataClient;
        private readonly IUserSettingRepository _personSettingRepository;
        private readonly IPacsPathologyItemNoRepository _pacsPathologyItemNoRepository;

        /// <summary>
        /// GrpcAppService
        /// </summary> 
        public GrpcAppService(IOperationApplyRepository operationApplyRepository,
            IDoctorsAdviceRepository doctorsAdviceRepository,
            IPrescribeRepository prescribeRepository,
            ILisRepository lisRepository,
            IPacsRepository pacsRepository,
            IPrescriptionRepository prescriptionRepository,
            IToxicRepository toxicRepository,
            INovelCoronavirusRnaRepository coronavirusRnaRepository,
            IPrintInfoRepository printInfoRepository,
            GrpcMasterData.GrpcMasterDataClient grpcMasterDataClient,
            ILogger<GrpcAppService> logger,
            IUnitOfWorkManager unitOfWorkManager,
            IObjectMapper objectMapper,
            IUserSettingRepository personSettingRepository,
            IPacsPathologyItemNoRepository pacsPathologyItemNoRepository)
        {
            _operationApplyRepository = operationApplyRepository;
            _doctorsAdviceRepository = doctorsAdviceRepository;
            _prescribeRepository = prescribeRepository;
            _lisRepository = lisRepository;
            _pacsRepository = pacsRepository;
            _prescriptionRepository = prescriptionRepository;
            _toxicRepository = toxicRepository;
            _coronavirusRnaRepository = coronavirusRnaRepository;
            _printInfoRepository = printInfoRepository;
            _logger = logger;
            UnitOfWorkManager = unitOfWorkManager;
            ObjectMapper = objectMapper;
            _grpcMasterDataClient = grpcMasterDataClient;
            _personSettingRepository = personSettingRepository;
            _pacsPathologyItemNoRepository = pacsPathologyItemNoRepository;
        }

        /// <summary>
        /// GetOperApplyList
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<OperApplyResponse> GetOperApplyList(GetOperApplyRequest request,
            ServerCallContext context)
        {
            using var uow = UnitOfWorkManager.Begin();

            try
            {
                List<OperationApply> operApplyList = await this._operationApplyRepository.GetListByPIIDDateAsync(new Guid(request.PID),
                    Convert.ToDateTime(request.StartTime), Convert.ToDateTime(request.EndTime), null, null);
                var response = new OperApplyResponse();

                foreach (var operApply in operApplyList)
                {
                    var responseOperApply = ObjectMapper.Map<OperationApply, GrpcOperApplyModel>(operApply);
                    response.OperApply.Add(responseOperApply);
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
        /// 打印报表目录
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<GetReportCatalogResponse> GetReportCatalog(GetReportCatalogRequest request, ServerCallContext context)
        {
            using IUnitOfWork uow = UnitOfWorkManager.Begin();

            try
            {
                List<ERecipeStatus> status = new List<ERecipeStatus> { ERecipeStatus.Submitted, ERecipeStatus.Confirmed, ERecipeStatus.Executed, ERecipeStatus.PayOff };
                var query = from a in (await _doctorsAdviceRepository.GetQueryableAsync())
                        .Where(w =>
                            w.PIID == Guid.Parse(request.Piid) && w.CategoryCode != "Entrust" &&
                            w.PlatformType == (EPlatformType)(request.PlatformType) &&
                            //w.Remarks != "检查自动带出" &&
                            status.Contains(w.Status))
                            join p in (await _prescriptionRepository.GetQueryableAsync())
                        on a.Id equals p.DoctorsAdviceId
                            join m in (await _prescribeRepository.GetQueryableAsync())
                                on a.Id equals m.DoctorsAdviceId into temp
                            from t in temp.DefaultIfEmpty()
                            join tx in (await _toxicRepository.GetQueryableAsync()).DefaultIfEmpty()
                                on t.MedicineId equals tx.MedicineId into t1
                            from tt in t1.DefaultIfEmpty()
                            join pacs in (await _pacsRepository.GetQueryableAsync())
                                on a.Id equals pacs.DoctorsAdviceId into paces
                            from pacs in paces.DefaultIfEmpty()
                            join lis in (await _lisRepository.GetQueryableAsync())
                                on a.Id equals lis.DoctorsAdviceId into lises
                            from lis in lises.DefaultIfEmpty()
                            join i in (await _printInfoRepository.GetQueryableAsync())
                                on a.PrescriptionNo equals i.PrescriptionNo into print
                            from i in print.DefaultIfEmpty()
                            select new
                            {
                                Id = a.Id,
                                ItemType = a.ItemType,
                                Code = a.Code,
                                SourceType = a.SourceType,
                                Additional = a.Additional,
                                PrescriptionNo = p.PrescriptionNo, // his PrescriptionNo 这里和DA表的PrescriptionNo不一样。
                                MyPrescriptionNo = p.MyPrescriptionNo,//我们自己系统的PrescriptionNo 和DA表的PrescriptionNo 一致
                                UsageCode = t != null ? t.UsageCode : "",
                                ToxicLevel = tt != null ? tt.ToxicLevel : 0,
                                AddCard = pacs != null ? pacs.AddCard : lis != null ? lis.AddCard : "",
                                PacsTemplateId = pacs != null ? (string.IsNullOrEmpty(pacs.TemplateId) ? "" : pacs.TemplateId) : "",
                                RecipeNo = a.RecipeNo,
                                IsPrint = i == null ? 0 : 1,
                                TemplateId = i == null ? "" : i.TemplateId.ToString(),
                                CommitSerialNo = a.CommitSerialNo,
                                IsCriticalPrescription = t != null && t.IsCriticalPrescription
                            }
                    into p
                            group p by new
                            {
                                p.Id,
                                p.ItemType,
                                p.Code,
                                p.SourceType,
                                p.Additional,
                                p.PrescriptionNo,
                                p.MyPrescriptionNo,
                                p.UsageCode,
                                p.ToxicLevel,
                                p.AddCard,
                                p.PacsTemplateId,
                                p.RecipeNo,
                                p.IsPrint,
                                p.TemplateId,
                                CommitSerialNo = p.CommitSerialNo,
                                IsCriticalPrescription = p.IsCriticalPrescription
                            }
                    into g
                            select new
                            {
                                g.Key.Id,
                                g.Key.ItemType,
                                g.Key.Code,
                                g.Key.SourceType,
                                g.Key.Additional,
                                g.Key.PrescriptionNo,
                                g.Key.MyPrescriptionNo,
                                g.Key.UsageCode,
                                g.Key.ToxicLevel,
                                g.Key.AddCard,
                                g.Key.PacsTemplateId,
                                g.Key.RecipeNo,
                                g.Key.IsPrint,
                                g.Key.TemplateId,
                                CommitSerialNo = g.Key.CommitSerialNo,
                                IsCriticalPrescription = g.Key.IsCriticalPrescription
                            };
                _logger.LogInformation("sql:{0}", query.ToQueryString());
                var response = new GetReportCatalogResponse();
                var list = await query.OrderBy(o => o.RecipeNo)
                    .ThenBy(o => o.PrescriptionNo).ToListAsync();

                ExamCodeRequest examCodeRequest = new ExamCodeRequest();
                examCodeRequest.Code.AddRange(list.Where(x => x.ItemType == EDoctorsAdviceItemType.Pacs).Select(s => s.Code));
                ExamProjectsResponse examProjectsResponse = _grpcMasterDataClient.GetExamsByCodes(examCodeRequest);

                bool isPrintAdditionalTreat = true;
                UserSetting setting = await _personSettingRepository.FindAsync(x => x.UserName == "system" && x.Code == "PrintAdditionalTreat");
                if (setting is { Value: "true" })
                {
                    isPrintAdditionalTreat = false;
                }

                foreach (var item in list)
                {
                    GrpcExamProjectModel examProject = examProjectsResponse.ExamProjects.FirstOrDefault(x => x.ProjectCode == item.Code);
                    var model = new ReportCatalogModel()
                    {
                        PrescriptionNo = item.PrescriptionNo ?? string.Empty,
                        MyPrescriptionNo = item.MyPrescriptionNo ?? string.Empty,
                        AddCard = item.AddCard ?? string.Empty,
                        UsageCode = item.UsageCode ?? string.Empty,
                        ItemType = item.ItemType.ToString(),
                        ToxicLevel = item.ToxicLevel.HasValue ? item.ToxicLevel.Value : 0,
                        RecipeNo = item.RecipeNo ?? string.Empty,
                        IsPrint = item.IsPrint,
                        TemplateId = item.TemplateId ?? string.Empty,
                        CommitSerialNo = item.CommitSerialNo,
                        IsCriticalPrescription = item.IsCriticalPrescription,
                        PacsTemplateId = string.IsNullOrEmpty(examProject?.TemplateId) ? "" : examProject?.TemplateId,
                        SourceType = item.SourceType,
                        Additional = isPrintAdditionalTreat ? item.Additional : 0
                    };
                    if (item.ItemType == EDoctorsAdviceItemType.Lis)
                    {
                        //查询新冠单据是否存在
                        if (!await (await _coronavirusRnaRepository.GetQueryableAsync()).AnyAsync(a => item.Id == a.DoctorsAdviceId))
                        {
                            model.AddCard = "";
                        }
                    }

                    response.Model.Add(model);
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
        /// 检查条码打印目录
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<PacsItemNoResponse> GetPrintPathologyNoList(PacsItemNoRequest request, ServerCallContext context)
        {
            Guid piId = Guid.Parse(request.Piid);
            List<PacsItemNoDto> pacsItemNoDtos = new List<PacsItemNoDto>();
            List<DoctorsAdvice> doctorsAdvices = await _doctorsAdviceRepository.GetListAsync(x => x.PIID == piId && x.CategoryCode == "Exam" && x.Status != ERecipeStatus.Saved && x.Status != ERecipeStatus.Cancelled);
            IEnumerable<Guid> doctorsAdviceIds = doctorsAdvices.Select(x => x.Id);
            List<Pacs> pacsList = await _pacsRepository.GetListAsync(x => doctorsAdviceIds.Contains(x.DoctorsAdviceId));
            IEnumerable<Guid> pacsIds = pacsList.Select(x => x.Id);
            List<PacsPathologyItemNo> pacsPathologyItemNos = await _pacsPathologyItemNoRepository.GetListAsync(x => pacsIds.Contains(x.PacsId));
            PacsItemNoResponse pacsItemNoResponse = new PacsItemNoResponse();
            foreach (PacsPathologyItemNo item in pacsPathologyItemNos)
            {
                Pacs pacs = pacsList.FirstOrDefault(x => x.Id == item.PacsId);
                DoctorsAdvice doctorsAdvice = doctorsAdvices.FirstOrDefault(x => x.Id == pacs?.DoctorsAdviceId);

                PacsItemNoModel pacsItemNoModel = new PacsItemNoModel()
                {
                    Id = item.Id,
                    PacsName = doctorsAdvice?.Name ?? "",
                    PathologyName = item.SpecimenName ?? "",
                    IsPrint = item.IsPrint
                };

                pacsItemNoResponse.Model.Add(pacsItemNoModel);
            }
            return pacsItemNoResponse;
        }
    }
}