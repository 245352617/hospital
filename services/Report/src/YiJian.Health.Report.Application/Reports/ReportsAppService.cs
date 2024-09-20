using FastReport.Export.Html;
using FastReport.Export.Image;
using FastReport.Export.Pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nito.AsyncEx;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.Core.Redis;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.PrintSettings;

namespace YiJian.Health.Report.Controllers
{
    /// <summary>
    /// 打印API
    /// </summary>
    [Route("/api/ecis/report/reports")]
    [Authorize]
    [NonUnify]
    public partial class ReportsAppService : ReportAppService
    {
        private readonly IPrintSettingRepository _printSettingRepository;
        private readonly IPrintSettingAppService _settingAppService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<ReportsAppService> _logger;
        private readonly IRedisClient _redis;

        private static AsyncLock _lockObject = new AsyncLock();

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="printSettingRepository"></param>
        /// <param name="settingAppService"></param>
        /// <param name="httpClientFactory"></param>
        /// <param name="httpContext"></param>
        /// <param name="logger"></param
        /// <param name="redis"></param>
        public ReportsAppService(IPrintSettingRepository printSettingRepository
            , IPrintSettingAppService settingAppService
            , IHttpClientFactory httpClientFactory
            , IHttpContextAccessor httpContext
            , ILogger<ReportsAppService> logger
            , IRedisClient redis)
        {
            _printSettingRepository = printSettingRepository;
            _settingAppService = settingAppService;
            _httpClientFactory = httpClientFactory;
            _httpContext = httpContext;
            _logger = logger;
            _redis = redis;
        }

        /// <summary>
        /// 上传并保存打印设置
        /// </summary>
        /// <param name="file"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        public async Task<ResponseBase<Guid>> UploadAsync([FromForm] IFormFileCollection file,
            [FromForm] PrintSettingCreateOrUpdate dto)
        {
            if (file is { Count: > 0 })
            {
                var fileFirst = file.FirstOrDefault();
                await using var stream = fileFirst!.OpenReadStream();
                //文件后缀
                var fileExtension = Path.GetExtension(fileFirst!.FileName).Trim('.').ToLower(); //获取文件格式，拓展名
                // frx：FastReport，repx：DevExpress
                if (fileExtension != "frx" && fileExtension != "repx")
                {
                    return new ResponseBase<Guid>(EStatusCode.C200, message: "文件格式错误，请确定格式是frx");
                }

                using StreamReader reader = new StreamReader(stream);
                dto.TempContent = await reader.ReadToEndAsync();
                if (string.IsNullOrWhiteSpace(dto.TempContent))
                {
                    return new ResponseBase<Guid>(EStatusCode.C200, message: "文件内容为空");
                }
            }

            try
            {
                var Code_key = $"PrintCode:{dto.Code}";
                string PrintTempId_key = $"PrintTempId:{dto.Id}";
                if (!string.IsNullOrEmpty(dto.Code))
                {
                    await _redis.RedisDeleteAsync(Code_key);
                }
                if (!string.IsNullOrEmpty(dto.Id.ToString()))
                {
                    await _redis.RedisDeleteAsync(PrintTempId_key);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"清除打印模版Redis缓存PrintCode:{dto.Code}失败：{ex.Message}");
            }

            return await _settingAppService.SavePrintSettingAsync(dto);
        }

        /// <summary>
        /// 下载模板
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="EcisBusinessException"></exception>
        [HttpGet("template/{fileName}")]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadTemplateAsync(string fileName)
        {
            string suffix = fileName.Split('.').Last();
            if (fileName.Split('.').Length <= 1 && suffix != "repx" && suffix != "frx")
            {
                return new NotFoundResult();
            }

            string mime = "application/json";
            Guid id = new(fileName[..36]);
            var set = await _printSettingRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (set == null)
            {
                throw new EcisBusinessException(message: "打印模板不存在");
            }

            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(set.TempContent);
                //string format = type switch { "DevExpress" => "repx", "FastReport" => "frx", _ => "" };
                // Otherwise download the report file 
                var file = new FileContentResult(bytes, mime)
                {
                    FileDownloadName = set.Id + "." + suffix
                };
                return file; // attachment
            }
            catch (Exception ex)
            {
                _logger.LogError("msg: {Message}", ex.Message);
                throw new EcisBusinessException(ex.Message);
            }
        }

        /// <summary>
        /// FastReport绑定数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns> 
        [HttpGet("print")]
        [AllowAnonymous]
        public async Task<IActionResult> PrintAsync(ReportQuery query)
        {
            string mime = "application/" + query.Format;
            string key = $"PrintTempId:{query.PrintTempId}";
            TimeSpan expire = TimeSpan.FromDays(7);

            //缓存模版数据
            var set = await _redis.GetRedisDataAsync(key, async () =>
            {
                var model = await _printSettingRepository.FirstOrDefaultAsync(x => x.Id == query.PrintTempId);
                return model;
            }, expire);

            //var set = await _printSettingRepository.FirstOrDefaultAsync(x => x.Id == query.PrintTempId);
            if (set == null)
            {
                throw new EcisBusinessException(message: "打印模板不存在");
            }
            _logger.LogInformation($"打印模版Id: {set.Id},{set.Name}");

            try
            {
                var dataset = await GetDataAsync(query.ParamUrl, query.Token);
                _logger.LogInformation($"打印模版dataset: {dataset.ToString().Length}");

                // Config.WebMode = true;
                using (await _lockObject.LockAsync())
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        using (global::FastReport.Report report = new global::FastReport.Report())
                        {
                            //调试用，在当前debug/bin目录下有report_jz_url.frx模板才可以
                            //var frx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "report_jz_url.frx");
                            //var report_file = await File.ReadAllTextAsync(frx); 
                            //report.LoadFromString(report_file);
                            report.LoadFromString(set.TempContent);
                            report.RegisterData(dataset);
                            //Prepare the report
                            report.Prepare();
                            if (query.Format == "png")
                            {
                                // Export report to png
                                using ImageExport png = new ImageExport();
                                png.ImageFormat = ImageExportFormat.Png;
                                png.SeparateFiles = false;
                                report.Export(png, stream);
                            }
                            //If html report format is selected
                            else if (query.Format == "html")
                            {
                                // Export Report to HTML
                                using HTMLExport html = new HTMLExport();
                                html.SinglePage = true; // Single page report
                                html.Navigator = false; // Top navigation bar
                                html.EmbedPictures = true; // Embeds images into a document
                                report.Export(html, stream);
                                mime = "text/" + query.Format; // Override mime for html
                            }
                            else if (query.Format == "pdf")
                            {
                                // Export Report to pdf
                                using PDFExport pdf = new PDFExport();

                                report.Export(pdf, stream);
                            }
                        }
                        if (query.Inline)
                        {
                            var fileResult = new FileContentResult(stream.GetBuffer(), mime);
                            return fileResult;
                        }

                        // Otherwise download the report file 
                        byte[] buffer = stream.GetBuffer();
                        var file = new FileContentResult(buffer, mime);
                        file.FileDownloadName = $"file{DateTime.Now}." + query.Format;
                        return file;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("打印绑定数据: {Message}", ex.Message);
                throw new EcisBusinessException(ex.Message);
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="token"></param>
        /// <returns></returns> 
        private async Task<DataSet> GetDataAsync(string uri, string token)
        {
            token ??= await GetTokenAsync();
            var client = _httpClientFactory.CreateClient("recipe");
            if (uri.Contains("patientService"))
            {
                client = _httpClientFactory.CreateClient("patient");
            }
            if (uri.Contains("nursing"))
            {
                client = _httpClientFactory.CreateClient("nursing");
            }

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Add("Authorization", token);
            }
            var info = await client.GetAsync(uri);
            if (info.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }
            var content = await info.Content.ReadAsStringAsync();
            _logger.LogInformation("打印数据信息：{0}", content);
            var dataset = JsonConvert.DeserializeObject<DataSet>(content);
            return dataset;
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