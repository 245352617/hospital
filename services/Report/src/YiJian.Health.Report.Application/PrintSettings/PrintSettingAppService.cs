using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.PrintCatalogs;

namespace YiJian.Health.Report.PrintSettings
{
    /// <summary>
    /// 打印设置API
    /// </summary>
    [Authorize]
    public class PrintSettingAppService : ReportAppService, IPrintSettingAppService
    {
        private readonly IPrintSettingRepository _printSettingRepository;
        private readonly IPrintCatalogRepository _iPrintCatalogRepository;

        /// <summary>
        /// 打印设置API
        /// </summary>
        /// <param name="printSettingRepository"></param>
        /// <param name="iPrintCatalogRepository"></param>
        public PrintSettingAppService(IPrintSettingRepository printSettingRepository,
            IPrintCatalogRepository iPrintCatalogRepository)
        {
            _printSettingRepository = printSettingRepository;
            _iPrintCatalogRepository = iPrintCatalogRepository;
        }

        /// <summary>
        /// 打印设置保存
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="EcisBusinessException"></exception>
        // [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ResponseBase<Guid>> SavePrintSettingAsync(PrintSettingCreateOrUpdate dto)
        {
            if (!await _iPrintCatalogRepository.AnyAsync(x => x.Id == dto.CataLogId))
            {
                throw new EcisBusinessException(message: "目录不存在！");
            }

            if (dto.Id == Guid.Empty)
            {
                if (dto.Sort == 0)
                {
                    //排序号为0时，获取最大排序号，排在后面
                    var sortPrintSetting =
                        await (await _printSettingRepository.GetQueryableAsync()).OrderByDescending(o => o.Sort).FirstOrDefaultAsync();
                    dto.Sort = (sortPrintSetting?.Sort ?? 0) + 1;
                }

                var model = new PrintSetting(GuidGenerator.Create(), dto.CataLogId, dto.Name, dto.ParamUrl, dto.Sort,
                    dto.Status, dto.TempContent, CurrentUser.FindClaimValue("fullName"), dto.Code
                    , dto.PageSizeCode, dto.Layout, dto.IsPreview, dto.TempType, dto.PrintMethod, dto.Comm, dto.Remark);
                var result = await _printSettingRepository.InsertAsync(model);
                return new ResponseBase<Guid>(EStatusCode.C200, result.Id);
            }
            else
            {
                var printSetting = await _printSettingRepository.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (printSetting == null)
                {
                    throw new EcisBusinessException(message: "数据不存在！");
                }

                printSetting.Update(dto.Name, dto.ParamUrl, dto.Sort,
                    dto.Status, string.IsNullOrEmpty(dto.TempContent) ? printSetting.TempContent : dto.TempContent,
                    dto.PageSizeCode, dto.Layout, dto.IsPreview, dto.TempType, dto.PrintMethod, dto.Comm, dto.Remark);
                var result = await _printSettingRepository.UpdateAsync(printSetting);
                return new ResponseBase<Guid>(EStatusCode.C200, result.Id);
            }
        }

        /// <summary>
        /// 根据编码获取
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseBase<PrintSettingDto>> GetPrintAsync(string code)
        {
            var model = await _printSettingRepository.FirstOrDefaultAsync(x => x.Code == code);
            return new ResponseBase<PrintSettingDto>(EStatusCode.C200,
                ObjectMapper.Map<PrintSetting, PrintSettingDto>(model));
        }

        /// <summary>
        /// 删除打印设置
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="EcisBusinessException"></exception>
        public async Task<ResponseBase<bool>> DeletePrintSettingAsync(Guid id)
        {
            try
            {
                if (!await _printSettingRepository.AnyAsync(x => x.Id == id))
                {
                    throw new EcisBusinessException(message: "数据不存在！");
                }

                await _printSettingRepository.DeleteAsync(id);
                return new ResponseBase<bool>(EStatusCode.C200, true);
            }
            catch (Exception e)
            {
                throw new EcisBusinessException(message: e.Message);
            }
        }

        /// <summary>
        /// 获取打印设置列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ResponseBase<List<PrintSettingListDto>>> GetPrintSettingListAsync(PrintSettingInput input)
        {
            var list = await (await _printSettingRepository.GetQueryableAsync())
                .WhereIf(input.Catalog != Guid.Empty, x => x.CataLogId == input.Catalog)
                .ToListAsync();
            return new ResponseBase<List<PrintSettingListDto>>(EStatusCode.C200,
                ObjectMapper.Map<List<PrintSetting>, List<PrintSettingListDto>>(list));
        }

        /// <summary>
        /// 获取Dev打印设置列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ResponseBase<List<PrintSettingListDto>>> GetDevPrintSettingListAsync()
        {
            var list = await (await _printSettingRepository.GetQueryableAsync())
                .Where(x => x.TempType == "DevExpress" && x.Status)
                .ToListAsync();
            return new ResponseBase<List<PrintSettingListDto>>(EStatusCode.C200,
                ObjectMapper.Map<List<PrintSetting>, List<PrintSettingListDto>>(list));
        }

        /// <summary>
        /// 修改模板状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<ResponseBase<bool>> StatusAsync(Guid id, bool status)
        {
            try
            {
                var model = await _printSettingRepository.FirstOrDefaultAsync(x => x.Id == id);
                if (model == null)
                {
                    return new ResponseBase<bool>(EStatusCode.CFail, "数据不存在");
                }

                model.SetStatus(status);
                return new ResponseBase<bool>(EStatusCode.C200, true);
            }
            catch (Exception e)
            {
                return new ResponseBase<bool>(EStatusCode.CFail, e.Message);
            }
        }

        /// <summary>
        /// 模板是否预览
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isPreview"></param>
        /// <returns></returns>
        public async Task<ResponseBase<bool>> IsPreviewAsync(Guid id, bool isPreview)
        {
            try
            {
                var model = await _printSettingRepository.FirstOrDefaultAsync(x => x.Id == id);
                if (model == null)
                {
                    return new ResponseBase<bool>(EStatusCode.CFail, "数据不存在");
                }

                model.SetIsPreview(isPreview);
                return new ResponseBase<bool>(EStatusCode.C200, true);
            }
            catch (Exception e)
            {
                return new ResponseBase<bool>(EStatusCode.CFail, e.Message);
            }
        }
    }
}