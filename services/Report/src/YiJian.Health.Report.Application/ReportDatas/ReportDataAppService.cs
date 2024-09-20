using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Etos.ReportHistoryDatas;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.ReportDatas.Dto;

namespace YiJian.Health.Report.ReportDatas
{
    /// <summary>
    /// 打印数据
    /// </summary>
    public class ReportDataAppService : ReportAppService, IReportDataAppService, ICapSubscribe
    {
        private readonly IReportDataRepository _reportDataRepository;
        private readonly ILogger<ReportDataAppService> _logger;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportDataRepository"></param>
        /// <param name="logger"></param>
        /// <param name="httpContext"></param>
        /// <param name="httpClientFactory"></param>
        public ReportDataAppService(IReportDataRepository reportDataRepository, ILogger<ReportDataAppService> logger,
            IHttpContextAccessor httpContext, IHttpClientFactory httpClientFactory)
        {
            _reportDataRepository = reportDataRepository;
            _logger = logger;
            _httpContext = httpContext;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 新增打印数据
        /// </summary>
        /// <param name="dtoList"></param>
        /// <returns></returns>
        /// <exception cref="EcisBusinessException"></exception>
        [CapSubscribe("save.historyreportdata.from.recipe")]
        public async Task<ResponseBase<string>> CreateReportDataAsync(List<ReportDataEto> dtoList)
        {
            try
            {
                var dataList = await (await _reportDataRepository.GetQueryableAsync()).Where(x => x.PIID == dtoList.FirstOrDefault().PIID).ToListAsync();
                var modelList = new List<ReportData>();
                var updateModel = new List<ReportData>();
                foreach (var dto in dtoList)
                {
                    var data = dataList.FirstOrDefault(s => s.TempId == dto.TempId && s.PrescriptionNo == dto.PrescriptionNo);
                    if (data != null)
                    {
                        data.Modify(dto.DataContent);
                        updateModel.Add(data);
                    }
                    else
                    {
                        var model = new ReportData(dto.PIID, dto.TempId, dto.DataContent, dto.PrescriptionNo,
                            dto.OperationCode);
                        modelList.Add(model);
                    }

                }
                if (modelList.Any())
                {
                    await _reportDataRepository.InsertManyAsync(modelList);
                }
                if (updateModel.Any())
                {
                    await _reportDataRepository.UpdateManyAsync(updateModel);
                }
                return new ResponseBase<string>(EStatusCode.C200, message: "保存成功");
            }
            catch (Exception e)
            {
                _logger.LogError("CreateReportDataAsync异常：{0}", e);
                return new ResponseBase<string>(EStatusCode.C500, message: e.Message);
            }
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="pIId"></param>
        /// <param name="tempId"></param>
        /// <returns></returns>
        public async Task<ResponseBase<List<ReportDataDto>>> GetReportDataAsync(Guid pIId, Guid tempId)
        {
            try
            {
                var dataList = await _reportDataRepository.GetListAsync(pIId, tempId);
                return new ResponseBase<List<ReportDataDto>>(EStatusCode.C200,
                    ObjectMapper.Map<List<ReportData>, List<ReportDataDto>>(dataList));
            }

            catch (Exception e)
            {
                _logger.LogError("GetReportDataAsync异常：{0}", e);
                return new ResponseBase<List<ReportDataDto>>(EStatusCode.C500, message: e.Message);
            }
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="token"></param>
        /// <returns></returns> 
        private async Task<string> GetDataAsync(string uri, string token = "")
        {
            token ??= await GetTokenAsync();
            var client = _httpClientFactory.CreateClient("recipe");
            client.DefaultRequestHeaders.Add("Authorization", token);
            var info = await client.GetAsync(uri);
            if (info.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var content = await info.Content.ReadAsStringAsync();
            _logger.LogInformation("数据信息：{0}", content);
            return content;
        }


        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetTokenAsync()
        {
            var token = _httpContext.HttpContext!.Request.Headers["Authorization"];
            return await Task.FromResult(token.ToString());
        }
    }
}