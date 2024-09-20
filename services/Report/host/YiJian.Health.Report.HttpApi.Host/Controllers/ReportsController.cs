//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using FastReport;
//using FastReport.Export.Html;
//using FastReport.Export.Image;
//using FastReport.Export;
//using FastReport.Export.Pdf;
//using FastReport.Utils;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using Volo.Abp.AspNetCore.Mvc;
//using Volo.Abp.Domain.Repositories;
//using YiJian.ECIS.ShareModel.Enums;
//using YiJian.ECIS.ShareModel.Exceptions;
//using YiJian.ECIS.ShareModel.Responses;
//using YiJian.Health.Report.Patients.Dto;
//using YiJian.Health.Report.PrintSettings;

//namespace YiJian.Health.Report.Controllers
//{
//    /// <summary>
//    /// 打印API
//    /// </summary>
//    [Route("/api/ecis/report/reports")]
//    [ApiController]
//    [Authorize]
//    public class ReportsController : AbpController
//    {
//        private readonly IPrintSettingRepository _printSettingRepository;
//        private readonly IPrintSettingAppService _settingAppService;
//        private readonly IHttpClientFactory _httpClientFactory;
//        private readonly IHttpContextAccessor _httpContext;
//        private readonly ILogger<ReportsController> _logger;

//        public ReportsController(IPrintSettingRepository printSettingRepository,
//            IPrintSettingAppService settingAppService, IHttpClientFactory httpClientFactory,
//            IHttpContextAccessor httpContext, ILogger<ReportsController> logger)
//        {
//            _printSettingRepository = printSettingRepository;
//            _settingAppService = settingAppService;
//            _httpClientFactory = httpClientFactory;
//            _httpContext = httpContext;
//            _logger = logger;
//        }

//        /// <summary>
//        /// 上传并保存打印设置
//        /// </summary>
//        /// <param name="file"></param>
//        /// <param name="dto"></param>
//        /// <returns></returns>
//        [HttpPost("upload")]
//        public async Task<ResponseBase<Guid>> UploadAsync([FromForm] IFormFileCollection file,
//            [FromForm] PrintSettingCreateOrUpdate dto)
//        {
//            if (file is { Count: > 0 })
//            {
//                var fileFirst = file.FirstOrDefault();
//                await using var stream = fileFirst!.OpenReadStream();
//                //文件后缀
//                var fileExtension = Path.GetExtension(fileFirst!.FileName).Trim('.').ToLower(); //获取文件格式，拓展名
//                // frx：FastReport，repx：DevExpress
//                if (fileExtension != "frx" && fileExtension != "repx")
//                {
//                    return new ResponseBase<Guid>(EStatusCode.C200, message: "文件格式错误，请确定格式是frx");
//                }

//                using StreamReader reader = new StreamReader(stream);
//                dto.TempContent = await reader.ReadToEndAsync();
//                if (string.IsNullOrWhiteSpace(dto.TempContent))
//                {
//                    return new ResponseBase<Guid>(EStatusCode.C200, message: "文件内容为空");
//                }
//            }

//            return await _settingAppService.SavePrintSettingAsync(dto);
//        }

//        /// <summary>
//        /// FastReport绑定数据
//        /// </summary>
//        /// <param name="query"></param>
//        /// <returns></returns>
//        /// <exception cref="EcisBusinessException"></exception>
//        [HttpPost("bind")]
//        [AllowAnonymous]
//        public async Task<ResponseBase<Dictionary<string, Object>>> GetAsync(ReportQuery query)
//        {
//            string mime = "application/" + query.Format;
//            var set = await _printSettingRepository.FirstOrDefaultAsync(x => x.Id == query.PrintTempId);
//            if (set == null)
//            {
//                return new ResponseBase<Dictionary<string, Object>>(code: EStatusCode.C200, message: "打印模板不存在");
//            }

//            if (string.IsNullOrWhiteSpace(set.TempContent))
//            {
//                return new ResponseBase<Dictionary<string, Object>>(code: EStatusCode.C200, message: "打印模板不存在");
//            }

//            var patientInfo = new AdmissionRecordDto();
//            if (query.PI_ID != Guid.Empty)
//            {
//                //获取患者信息
//                patientInfo =
//                    await GetPatientAsync(
//                        $"/api/patientService/admissionRecord/admissionRecordById?aR_ID=0&pI_ID={query.PI_ID}",
//                        query.Token);
//            }

//            //获取数据信息
//            var clientDataDto = new Dictionary<string, object>();
//            if (!query.ParamUrl.Contains("admissionRecordById"))
//            {
//                clientDataDto = await GetClientAsync(query.ParamUrl, query.Token);
//            }

//            await using MemoryStream stream = new MemoryStream();
//            var pages = 0;
//            try
//            {
//                using (DataSet dataSet = new DataSet())
//                {
//                    if (patientInfo != null)
//                    {
//                        //获取患者信息
//                        var tablePatient = ToDataTable(new List<AdmissionRecordDto> { patientInfo });
//                        tablePatient.TableName = "Patient";
//                        dataSet.Tables.Add(tablePatient);
//                    }

//                    Config.WebMode = true;
//                    using (global::FastReport.Report report = new global::FastReport.Report())
//                    {
//                        if (clientDataDto is { Count: > 0 })
//                        {
//                            var dicColumns =
//                                JsonConvert.DeserializeObject<Dictionary<string, Object>>(
//                                    clientDataDto["columns"].ToString() ?? string.Empty);
//                            var dicModels =
//                                JsonConvert.DeserializeObject<Dictionary<string, Object>>(
//                                    clientDataDto["models"].ToString() ?? string.Empty);
//                            foreach (var dic in dicColumns!)
//                            {
//                                var table = new DataTable();
//                                ArrayList arrayList = JsonConvert.DeserializeObject<ArrayList>(dic.Value.ToString()!);
//                                foreach (var column in arrayList!)
//                                {
//                                    table.Columns.Add(column.ToString());
//                                }

//                                var data = dicModels![dic.Key];
//                                if (data != null)
//                                {
//                                    ToDataTableRows(data.ToString(), ref table);
//                                }

//                                table.TableName = dic.Key;
//                                dataSet.Tables.Add(table);
//                            }
//                        }

//                        report.ScriptLanguage = Language.CSharp;
//                        report.LoadFromString(set.TempContent);
//                        report.RegisterData(dataSet);
//                        if (query.Parameter != null)
//                        {
//                            report.SetParameterValue("Parameter", query.Parameter);
//                        }

//                        //Prepare the report
//                        report.Prepare();

//                        if (query.Format == "png")
//                        {
//                            // Export report to PDF
//                            ImageExport png = new ImageExport();
//                            png.ImageFormat = ImageExportFormat.Png;
//                            png.SeparateFiles = false;
//                            report.Export(png, stream);
//                        }
//                        //If html report format is selected
//                        else if (query.Format == "html")
//                        {
//                            // Export Report to HTML
//                            HTMLExport html = new HTMLExport();
//                            html.SinglePage = true; // Single page report
//                            html.Navigator = false; // Top navigation bar
//                            html.EmbedPictures = true; // Embeds images into a document
//                            report.Export(html, stream);
//                            mime = "text/" + query.Format; // Override mime for html
//                        }
//                        else if (query.Format == "pdf")
//                        {
//                            // Export Report to pdf
//                            PDFExport pdf = new PDFExport();
//                            report.Export(pdf, stream);
//                        }

//                        pages = report.MaxPages;
//                    }
//                }

//                var fileResult = File(stream.ToArray(), mime);
//                var fileString = Encoding.Default.GetString(fileResult.FileContents);
//                var dict = new Dictionary<string, Object>
//                {
//                    { "PrintTempId", query.PrintTempId.ToString() },
//                    { "HtmlPage", fileString },
//                    { "Pages", pages }
//                };
//                return new ResponseBase<Dictionary<string, Object>>(EStatusCode.C200,
//                    data: dict);
//            }
//            // Handle exceptions
//            catch (Exception ex)
//            {
//                return new ResponseBase<Dictionary<string, Object>>(EStatusCode.C400,
//                    ex.Message);
//            }
//            finally
//            {
//                await stream.DisposeAsync();
//            }
//        }

//        /// <summary>
//        /// FastReport绑定数据
//        /// </summary>
//        /// <param name="query"></param>
//        /// <returns></returns>
//        /// <exception cref="EcisBusinessException"></exception>
//        [HttpGet("print")]
//        [AllowAnonymous]
//        public async Task<IActionResult> GetFormAsync([FromQuery] ReportQuery query)
//        {
//            string mime = "application/" + query.Format;
//            var set = await _printSettingRepository.FirstOrDefaultAsync(x => x.Id == query.PrintTempId);
//            if (set == null)
//            {
//                throw new EcisBusinessException(message: "打印模板不存在");
//            }

//            var patientInfo = new AdmissionRecordDto();
//            if (query.PI_ID != Guid.Empty)
//            {
//                //获取患者信息
//                patientInfo =
//                    await GetPatientAsync(
//                        $"/api/patientService/admissionRecord/admissionRecordById?aR_ID=0&pI_ID={query.PI_ID}",
//                        query.Token);
//            }

//            //获取数据信息
//            var clientDataDto = new Dictionary<string, object>();
//            if (!query.ParamUrl.Contains("admissionRecordById"))
//            {
//                clientDataDto = await GetClientAsync(query.ParamUrl, query.Token);
//            }

//            using (MemoryStream stream = new MemoryStream())
//            {
//                try
//                {
//                    using (DataSet dataSet = new DataSet())
//                    {
//                        if (patientInfo != null)
//                        {
//                            //获取患者信息
//                            var tablePatient = ToDataTable(new List<AdmissionRecordDto> { patientInfo });
//                            tablePatient.TableName = "Patient";
//                            dataSet.Tables.Add(tablePatient);
//                        }

//                        Config.WebMode = true;
//                        using (global::FastReport.Report report = new global::FastReport.Report())
//                        {
//                            if (clientDataDto is { Count: > 0 })
//                            {
//                                var dicColumns =
//                                    JsonConvert.DeserializeObject<Dictionary<string, Object>>(
//                                        clientDataDto["columns"].ToString() ?? string.Empty);
//                                var dicModels =
//                                    JsonConvert.DeserializeObject<Dictionary<string, Object>>(
//                                        clientDataDto["models"].ToString() ?? string.Empty);
//                                foreach (var dic in dicColumns!)
//                                {
//                                    var table = new DataTable();
//                                    ArrayList arrayList =
//                                        JsonConvert.DeserializeObject<ArrayList>(dic.Value.ToString()!);
//                                    foreach (var column in arrayList!)
//                                    {
//                                        table.Columns.Add(column.ToString());
//                                    }

//                                    var data = dicModels![dic.Key];
//                                    if (data != null)
//                                    {
//                                        ToDataTableRows(data.ToString(), ref table);
//                                    }

//                                    table.TableName = dic.Key;
//                                    dataSet.Tables.Add(table);
//                                }
//                            }

//                            report.LoadFromString(set.TempContent);
//                            report.RegisterData(dataSet);
//                            if (query.Parameter != null)
//                            {
//                                report.SetParameterValue("Parameter", query.Parameter);
//                            }

//                            //Prepare the report
//                            report.Prepare();
//                            if (query.Format == "png")
//                            {
//                                // Export report to png
//                                ImageExport png = new ImageExport();
//                                png.ImageFormat = ImageExportFormat.Png;
//                                png.SeparateFiles = false;
//                                report.Export(png, stream);
//                            }
//                            //If html report format is selected
//                            else if (query.Format == "html")
//                            {
//                                // Export Report to HTML
//                                HTMLExport html = new HTMLExport();
//                                html.SinglePage = true; // Single page report
//                                html.Navigator = false; // Top navigation bar
//                                html.EmbedPictures = true; // Embeds images into a document
//                                report.Export(html, stream);
//                                mime = "text/" + query.Format; // Override mime for html
//                            }
//                            else if (query.Format == "pdf")
//                            {
//                                // Export Report to pdf
//                                PDFExport pdf = new PDFExport();
//                                report.Export(pdf, stream);
//                            }
//                        }
//                    }

//                    if (query.Inline)
//                    {
//                        var fileResult = File(stream.GetBuffer(), mime);

//                        return fileResult;
//                    }

//                    // Otherwise download the report file 
//                    return File(stream.GetBuffer(), mime, $"file{DateTime.Now}." + query.Format); // attachment
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogError("msg: {Message}", ex.Message);
//                    throw new EcisBusinessException(ex.Message);
//                }
//            }
//        }


//        /// <summary>
//        /// 获取患者信息
//        /// </summary>
//        /// <param name="uri"></param>
//        /// <param name="token"></param>
//        /// <returns></returns>
//        [HttpGet("patient")]
//        public async Task<AdmissionRecordDto> GetPatientAsync(string uri, string token)
//        {
//            token = token.IsNullOrEmpty() ? await GetTokenAsync() : token;
//            using var client = _httpClientFactory.CreateClient("patient");
//            client.DefaultRequestHeaders.Add("Authorization", token);
//            var patientInfo = await client.GetAsync(uri);
//            if (patientInfo.StatusCode != System.Net.HttpStatusCode.OK)
//            {
//                return null;
//            }

//            var content = await patientInfo.Content.ReadAsStringAsync();
//            if (content.IsNullOrWhiteSpace())
//            {
//                return null;
//            }

//            _logger.LogInformation("打印患者信息：{0}", content);
//            var data = JsonConvert.DeserializeObject<ResponseResult<AdmissionRecordDto>>(content);
//            if (data is { Code: HttpStatusCodeEnum.Ok })
//            {
//                var dto = data.Data;
//                dto.GreenRoadName = string.IsNullOrWhiteSpace(dto.GreenRoadName)
//                    ? ""
//                    : "绿色通道";
//                dto.VisitDate = DateTime.Parse(dto.VisitDate).ToString("yyyy-MM-dd HH:mm");
//                return dto;
//            }

//            return null;
//        }

//        /// <summary>
//        /// 获取数据
//        /// </summary>
//        /// <param name="uri"></param>
//        /// <param name="token"></param>
//        /// <returns></returns>
//        private async Task<Dictionary<string, object>> GetClientAsync(string uri, string token)
//        {
//            token = token.IsNullOrEmpty() ? await GetTokenAsync() : token;
//            var client = _httpClientFactory.CreateClient("patient");
//            if (uri.Contains("recipe"))
//            {
//                client = _httpClientFactory.CreateClient("recipe");
//            }

//            if (uri.Contains("TriageService"))
//            {
//                client = _httpClientFactory.CreateClient("triage");
//            }

//            client.DefaultRequestHeaders.Add("Authorization", token);
//            var info = await client.GetAsync(uri);
//            if (info.StatusCode != System.Net.HttpStatusCode.OK)
//            {
//                return null;
//            }

//            var content = await info.Content.ReadAsStringAsync();
//            _logger.LogInformation("打印数据信息：{0}", content);
//            if (content.IsNullOrWhiteSpace())
//            {
//                return null;
//            }

//            var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
//            if (dic == null)
//            {
//                return null;
//            }

//            if (dic["data"] != null)
//            {
//                var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(dic["data"].ToString() ?? string.Empty);
//                return data;
//            }


//            return null;
//        }

//        /// <summary>
//        /// 获取token
//        /// </summary>
//        /// <returns></returns>
//        private async Task<string> GetTokenAsync()
//        {
//            var token = _httpContext.HttpContext!.Request.Headers["Authorization"];
//            return await Task.FromResult(token.ToString());
//        }

//        /// <summary>
//        /// List转DataTable
//        /// </summary>
//        /// <param name="varList"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        private DataTable ToDataTable<T>(List<T> varList)
//        {
//            DataTable dtReturn = new DataTable();
//            PropertyInfo[] oProps = null;
//            foreach (T rec in varList)
//            {
//                if (oProps == null)
//                {
//                    oProps = rec.GetType().GetProperties();
//                    foreach (PropertyInfo pi in oProps)
//                    {
//                        // 当字段类型是Nullable<>时
//                        Type colType = pi.PropertyType;
//                        if (colType.IsGenericType && colType.GetGenericTypeDefinition() == typeof(Nullable<>))
//                        {
//                            colType = colType.GetGenericArguments()[0];
//                        }

//                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
//                    }
//                }

//                DataRow dr = dtReturn.NewRow();
//                foreach (PropertyInfo pi in oProps)
//                {
//                    dr[pi.Name] = (pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null)) ?? "";
//                }

//                dtReturn.Rows.Add(dr);
//            }

//            return dtReturn;
//        }

//        private static DataTable ToDataTableRows(string json, ref DataTable dataTable)
//        {
//            // DataTable dataTable = new DataTable(); //实例化
//            DataTable result;
//            try
//            {
//                if (json.StartsWith("["))
//                {
//                    var arrayList = JsonConvert.DeserializeObject<ArrayList>(json);
//                    if (arrayList is { Count: > 0 })
//                    {
//                        foreach (var array in arrayList)
//                        {
//                            var dictionary =
//                                JsonConvert.DeserializeObject<Dictionary<string, object>>(array.ToString() ??
//                                    string.Empty);
//                            if (!dictionary!.Keys.Any())
//                            {
//                                result = dataTable;
//                                return result;
//                            }

//                            //Columns
//                            if (dataTable.Columns.Count == 0)
//                            {
//                                foreach (string current in dictionary.Keys)
//                                {
//                                    dataTable.Columns.Add(current, dictionary[current].GetType());
//                                }
//                            }

//                            //Rows
//                            DataRow dataRow = dataTable.NewRow();
//                            foreach (string current in dictionary.Keys)
//                            {
//                                dataRow[current] = dictionary[current];
//                            }

//                            dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
//                        }
//                    }
//                }
//                else
//                {
//                    var dictionary =
//                        JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
//                    if (!dictionary!.Keys.Any())
//                    {
//                        result = dataTable;
//                        return result;
//                    }

//                    //Columns
//                    if (dataTable.Columns.Count == 0)
//                    {
//                        foreach (string current in dictionary.Keys)
//                        {
//                            dataTable.Columns.Add(current, dictionary[current].GetType());
//                        }
//                    }

//                    //Rows
//                    DataRow dataRow = dataTable.NewRow();
//                    foreach (string current in dictionary.Keys)
//                    {
//                        dataRow[current] = dictionary[current];
//                    }

//                    dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
//                }
//            }
//            catch (Exception e)
//            {
//                throw new EcisBusinessException(message: e.Message);
//            }

//            result = dataTable;
//            return result;
//        }
//    }
//}