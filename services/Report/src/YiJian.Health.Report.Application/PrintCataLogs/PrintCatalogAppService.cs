using MasterDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RecipeService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.PrintSettings;

namespace YiJian.Health.Report.PrintCatalogs
{
    /// <summary>
    /// 打印目录API
    /// </summary>
    [Authorize]
    public class PrintCatalogAppService : ReportAppService, IPrintCatalogAppService
    {
        private readonly IPrintCatalogRepository _iPrintCatalogRepository;
        private readonly IPrintSettingRepository _printSettingRepository;
        private readonly GrpcMasterData.GrpcMasterDataClient _grpcMasterDataClient;
        private readonly GrpcRecipe.GrpcRecipeClient _grpcRecipeClient;
        private readonly IPageSizeRepository _pageSizeRepository;

        /// <summary>
        /// 打印目录
        /// </summary> 
        public PrintCatalogAppService(IPrintCatalogRepository iPrintCatalogRepository,
            IPrintSettingRepository printSettingRepository,
            GrpcMasterData.GrpcMasterDataClient grpcMasterDataClient,
            GrpcRecipe.GrpcRecipeClient grpcRecipeClient,
            IPageSizeRepository pageSizeRepository)
        {
            _iPrintCatalogRepository = iPrintCatalogRepository;
            _printSettingRepository = printSettingRepository;
            _grpcMasterDataClient = grpcMasterDataClient;
            _grpcRecipeClient = grpcRecipeClient;
            _pageSizeRepository = pageSizeRepository;
        }

        /// <summary>
        /// 保存打印目录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="EcisBusinessException"></exception>
        public async Task<ResponseBase<Guid>> SavePrintCatalogAsync(PrintCatalogDto dto)
        {
            //新增
            if (dto.Id == Guid.Empty)
            {
                var result = await _iPrintCatalogRepository.InsertAsync(new PrintCatalog(Guid.Empty, dto.CataLogName,
                    dto.Type));
                return new ResponseBase<Guid>(EStatusCode.C200, result.Id);
            }

            //修改
            var model = await (await _iPrintCatalogRepository.GetQueryableAsync()).FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (model == null)
            {
                throw new EcisBusinessException(message: "数据不存在");
            }

            model.Update(dto.CataLogName, dto.Type);
            await _iPrintCatalogRepository.UpdateAsync(model);
            return new ResponseBase<Guid>(EStatusCode.C200, model.Id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="EcisBusinessException"></exception>
        public async Task<ResponseBase<bool>> DeletePrintCatalogAsync(Guid id)
        {
            try
            {
                if (!await (await _iPrintCatalogRepository.GetQueryableAsync()).AnyAsync(x => x.Id == id))
                {
                    throw new EcisBusinessException(message: "数据不存在");
                }

                //判断下级是否有数据
                if (await (await _printSettingRepository.GetQueryableAsync()).AnyAsync(x => x.CataLogId == id))
                {
                    throw new EcisBusinessException(message: "包含子集，无法删除！");
                }

                await _iPrintCatalogRepository.DeleteAsync(id);
                return new ResponseBase<bool>(EStatusCode.C200, true);
            }
            catch (Exception e)
            {
                throw new EcisBusinessException(message: e.Message);
            }
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseBase<List<PrintCatalogDto>>> GetListAsync()
        {
            var list = await _iPrintCatalogRepository.GetListAsync();
            return new ResponseBase<List<PrintCatalogDto>>(EStatusCode.C200,
                ObjectMapper.Map<List<PrintCatalog>, List<PrintCatalogDto>>(list));
        }


        /// <summary>
        /// 打印中心查询打印
        /// </summary>
        /// <param name="piid"></param>
        /// <param name="platformType"></param>
        /// <param name="isPrint"></param>
        /// <returns></returns> 
        public async Task<ResponseBase<List<PrintSettingListDto>>> GetPrintCenterListAsync(Guid piid,
            EPlatformType platformType = EPlatformType.EmergencyTreatment, int isPrint = -1)
        {
            //获取药品途径配置信息
            var separationsResponse = await _grpcMasterDataClient.GetSeparationsAsync(new SeparationListRequest());
            var separations = separationsResponse.Separation;
            var catalog = await _grpcRecipeClient.GetReportCatalogAsync(new GetReportCatalogRequest
            { Piid = piid.ToString(), PlatformType = 0 });
            var adviceData = catalog.Model;
            var printList = new List<PrintSettingListDto>();
            var printSettingList =
                await (from c in (await _iPrintCatalogRepository.GetQueryableAsync()).Where(x => x.Type == 0)
                       join s in (await _printSettingRepository.GetQueryableAsync()) on c.Id equals s.CataLogId
                       join p in (await _pageSizeRepository.GetQueryableAsync())
                           on s.PageSizeCode equals p.Code
                           into temp
                       from t in temp.DefaultIfEmpty()
                       orderby s.Sort
                       where s.Status
                       select new PrintSettingListDto
                       {
                           CataLogId = s.CataLogId,
                           Code = s.Code,
                           Status = s.Status,
                           CreationName = s.CreationName,
                           CreationTime = s.CreationTime,
                           Id = s.Id,
                           IsPreview = s.IsPreview,
                           Layout = s.Layout,
                           Name = s.Name,
                           PageSizeCode = s.PageSizeCode,
                           PageSizeHeight = t != null ? t.Height : 0,
                           PageSizeWidth = t != null ? t.Width.ToString() : "",
                           ParamUrl = s.ParamUrl,
                           PrintMethod = s.PrintMethod,
                           SeparationId = null,
                           Sort = s.Sort,
                           TempType = s.TempType,
                           Comm = s.ReportTypeCode,
                           Remark = s.Remark
                       }).ToListAsync();
            foreach (var print in printSettingList)
            {
                var templateId = print.Id.ToString().ToUpper();
                if (separations.Any())
                {
                    var separation = separations
                        .FirstOrDefault(x => x.PrintSettingId == templateId);
                    print.SeparationId = separation != null ? Guid.Parse(separation.Id) : null;
                    print.UsageCode = separation != null ? separation.UsageCode : "";
                }

                //默认查询0的处方单
                var data = adviceData
                    .Where(x => x.ToxicLevel != 2 && x.ToxicLevel != 3 && x.ItemType == "Prescribe")
                    .WhereIf(isPrint != -1, x => x.IsPrint == isPrint)
                    .ToList();
                data = data.DistinctBy(x => x.MyPrescriptionNo).ToList();
                //if (print.Comm is "1" or "2" or "7")
                if ((new string[] { "1", "2", "7" }).Contains(print.Comm))
                {
                    data = adviceData.Where(x =>
                            x.ItemType == "Prescribe" && print.UsageCode.Split(',').ToList().Contains(x.UsageCode))
                        .ToList();
                    var specialData = data.Where(x => x.SourceType == 8).ToList();
                    if (print.Comm == "1")
                    {
                        data = data.Where(x => x.SourceType != 1 && x.SourceType != 8).ToList();
                    }
                    List<PrintSettingsChildDto> child = new List<PrintSettingsChildDto>();
                    if (data.Any())
                    {
                        int[] toxicLevels = { 2, 3 };
                        child = data.GroupBy(g => new { g.CommitSerialNo }).Select(s =>
                            new PrintSettingsChildDto
                            {
                                PrescriptionNo = string.Join(",", s.GroupBy(g => g.PrescriptionNo).Select(x => x.Key).ToList()),
                                MyPrescriptionNo = s.FirstOrDefault().MyPrescriptionNo,
                                Name = print.Name,
                                ParamUrl = print.ParamUrl,
                                SeparationId = print.SeparationId,
                                TemplateId = print.Id,
                                Comm = print.Comm,
                                PageSizeCode = print.PageSizeCode,
                                PageSizeHeight = print.PageSizeHeight,
                                PageSizeWidth = print.PageSizeWidth,
                                IsCriticalPrescription = s.FirstOrDefault().IsCriticalPrescription,
                                IsNarcotic = s.Any(w => toxicLevels.Contains(w.ToxicLevel)), //如果是麻醉药，精一,精二药品标记为true
                                IsPrint = s.FirstOrDefault(t => t.TemplateId == templateId) == null ? 0 : s.FirstOrDefault(t => t.TemplateId == templateId).IsPrint
                            }).OrderByDescending(g => g.PrescriptionNo.Length).ToList();
                    }

                    //if (print.Comm == "1" && specialData.Any())
                    //{
                    //    int[] toxicLevels = { 2, 3 };
                    //    var child1 = specialData.GroupBy(g => new { g.MyPrescriptionNo }).Select(s =>
                    //        new PrintSettingsChildDto
                    //        {
                    //            PrescriptionNo = string.Join(",", s.GroupBy(g => g.PrescriptionNo).Select(x => x.Key).ToList()),
                    //            MyPrescriptionNo = s.FirstOrDefault().MyPrescriptionNo,
                    //            Name = print.Name,
                    //            ParamUrl = print.ParamUrl,
                    //            SeparationId = print.SeparationId,
                    //            TemplateId = print.Id,
                    //            Comm = print.Comm,
                    //            PageSizeCode = print.PageSizeCode,
                    //            PageSizeHeight = print.PageSizeHeight,
                    //            PageSizeWidth = print.PageSizeWidth,
                    //            IsCriticalPrescription = s.FirstOrDefault().IsCriticalPrescription,
                    //            IsNarcotic = s.Any(w => toxicLevels.Contains(w.ToxicLevel)), //如果是麻醉药，精一,精二药品标记为true
                    //            IsPrint = s.FirstOrDefault(t => t.TemplateId == templateId) == null ? 0 : s.FirstOrDefault(t => t.TemplateId == templateId).IsPrint
                    //        }).OrderByDescending(g => g.PrescriptionNo.Length).ToList();
                    //    child.AddRange(child1);
                    //}

                    if (child.Any())
                    {
                        print.Child = child;
                        print.PageCount = print.Child.Count;
                    }
                }
                else if (print.Comm == "3")
                {
                    data = adviceData.Where(x => x.ItemType == "Lis").ToList();
                    data = data.DistinctBy(x => x.MyPrescriptionNo).ToList();
                }
                else if (print.Comm == "4")
                {
                    data = adviceData.Where(x => x.ItemType == "Pacs").ToList();
                }
                else if (print.Comm == "6")
                {
                    data = adviceData.Where(x => x.ItemType == "Treat" && x.Additional != 1).ToList();
                    data = data.DistinctBy(x => x.MyPrescriptionNo).ToList();
                }
                else if (print.Comm == "8")
                {
                    data = adviceData.Where(x => x.ToxicLevel == 3 && x.ItemType == "Prescribe").ToList();
                }
                else if (print.Comm == "9")
                {
                    data = adviceData.Where(x => x.ToxicLevel == 2 && x.ItemType == "Prescribe").ToList();
                }
                else if (print.Comm == "10")
                {
                    data = adviceData.Where(x => x.AddCard == "16" && x.ItemType == "Pacs").ToList();
                }
                else if (print.Comm == "12")
                {
                    data = adviceData.Where(x => x.AddCard == "14" && x.ItemType == "Lis").ToList();
                }

                //注射单，输液单，雾化单特殊处理
                //if (data.Any() && !"1,2,7".Contains(print.Comm))
                if (data.Any() && !(new string[] { "1", "2", "7" }).Contains(print.Comm))
                {
                    int[] toxicLevels = { 2, 3 };
                    print.Child = data.GroupBy(g => g.MyPrescriptionNo).Select(s => new PrintSettingsChildDto
                    {
                        PrescriptionNo = string.Join(",", s.GroupBy(g => g.PrescriptionNo).Select(x => x.Key).ToList()),
                        MyPrescriptionNo = s.FirstOrDefault().MyPrescriptionNo,
                        Name = print.Name,
                        ParamUrl = print.ParamUrl,
                        SeparationId = print.SeparationId,
                        TemplateId = string.IsNullOrEmpty(s.FirstOrDefault().PacsTemplateId) ? print.Id : Guid.Parse(s.FirstOrDefault().PacsTemplateId),
                        Comm = print.Comm,
                        PageSizeCode = print.PageSizeCode,
                        PageSizeHeight = print.PageSizeHeight,
                        PageSizeWidth = print.PageSizeWidth,
                        IsCriticalPrescription = s.FirstOrDefault().IsCriticalPrescription,
                        IsNarcotic = s.Any(w => toxicLevels.Contains(w.ToxicLevel)),
                        IsPrint = print.Comm == "4" ? s.FirstOrDefault().IsPrint : s.FirstOrDefault(t => t.TemplateId == templateId) == null ? 0 : s.FirstOrDefault(t => t.TemplateId == templateId).IsPrint
                    }).ToList();
                    print.PageCount = print.Child.Count;
                }
                printList.Add(print);
            }

            return new ResponseBase<List<PrintSettingListDto>>(EStatusCode.COK, printList);
        }

        /// <summary>
        /// 检查条码打印
        /// </summary>
        /// <param name="piid"></param>
        /// <returns></returns>
        /// <exception cref="EcisBusinessException"></exception>
        public async Task<ResponseBase<List<PacsItemNoDto>>> GetPacsItemNoListAsync(Guid piid)
        {
            PacsItemNoResponse pacsItemNoResponse = await _grpcRecipeClient.GetPrintPathologyNoListAsync(new PacsItemNoRequest { Piid = piid.ToString() });
            var pacsItemNoModels = pacsItemNoResponse.Model;

            PrintSetting printSetting = await _printSettingRepository.FindAsync(x => x.ReportTypeCode == "41");
            if (printSetting == null)
            {
                throw new EcisBusinessException(message: "没有打印模板");
            }

            List<PacsItemNoDto> pacsItemNoDtos = new List<PacsItemNoDto>();
            foreach (var item in pacsItemNoModels)
            {
                PacsItemNoDto pacsItemNoDto = new PacsItemNoDto()
                {
                    Id = item.Id,
                    PacsName = item.PacsName,
                    PathologyName = item.PathologyName,
                    IsPrint = item.IsPrint
                };
                pacsItemNoDto.TemplateId = printSetting.Id;
                pacsItemNoDto.PrintUrl = printSetting.ParamUrl;
                pacsItemNoDtos.Add(pacsItemNoDto);
            }

            return new ResponseBase<List<PacsItemNoDto>>(EStatusCode.COK, pacsItemNoDtos);
        }
    }
}