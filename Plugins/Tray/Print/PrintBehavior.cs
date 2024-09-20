using DevExpress.XtraReports.UI;
using FastReport.Export.Pdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using Spire.Pdf;
using Spire.Pdf.Print;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using YiJian.CardReader.Print;
using YiJian.CardReader.WebServer;
using static System.Drawing.Printing.PrinterSettings;

namespace YiJian.CardReader.CardReaders
{
    public class PrintBehavior : WebSocketBehavior
    {
        private static readonly ILogger _log = LogManager.GetCurrentClassLogger();
        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        private string _defaultPrinter = string.Empty;

        public string GetSessionInfoStr()
        {
            if (Sessions != null)
                return $"目前操作的ID:{ID}，目前在连接的ID列表:{string.Join('|', Sessions.IDs.ToArray())},PrintBehavior,";
            return "";
        }

        public void SetupPrint(string defaultPrinter)
        {
            _defaultPrinter = defaultPrinter;
            _log.Info(GetSessionInfoStr() + "SetupPrint invoke");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            _log.Info(GetSessionInfoStr() + "OnClose invoke");
            base.OnClose(e);
        }

        protected override void OnError(WebSocketSharp.ErrorEventArgs e)
        {
            _log.Error(GetSessionInfoStr() + "OnError invoke");
            base.OnError(e);
        }

        protected override void OnOpen()
        {
            _log.Info(GetSessionInfoStr() + "OnOpen invoke");
            base.OnOpen();
        }

        private void WebSocketSend(string data)
        {
            try
            {
                Send(data);
            }
            catch (InvalidOperationException ex)
            {
                _log.Error($"WebSocket 断开连接： {ex}");
                return;
            }
            catch (Exception ex)
            {
                _log.Error($"WebSocketSend 断开异常： {ex}");
                return;
            }
        }

        protected override async void OnMessage(MessageEventArgs e)
        {
            string message = GetSessionInfoStr() + $"接受到的信息如下 : {e.Data}";
            _log.Info(message);

            if (e.Data.Equals("IsHere**"))//客户端定时发送心跳，维持链接
                return;
            if (string.IsNullOrWhiteSpace(e.Data)) return;

            PrintReqest recievedData;
            try
            {
                recievedData = JsonConvert.DeserializeObject<PrintReqest>(e.Data, _serializerSettings) ?? new PrintReqest();
            }
            catch (Exception)
            {
                WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error("", "参数不符合要求"), _serializerSettings));
                return;
            }

            try
            {
                message = GetSessionInfoStr() + $"执行操作: {recievedData.Operator}";
                _log.Info(message);

                switch (recievedData.Operator)
                {
                    case "print":
                        {
                            PrintDevReqest? printDevReqest = JsonConvert.DeserializeObject<PrintDevReqest>(e.Data, _serializerSettings);
                            string reportFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Report", printDevReqest?.TemplateCode + ".repx");
                            if (File.Exists(reportFilePath))
                            {
                                using (XtraReport report = new())
                                {
                                    report.Name = printDevReqest?.TemplateCode + DateTime.Now.ToString("yyyyMMddHHmmss");
                                    report.LoadLayout(reportFilePath);
                                    report.DataSource = printDevReqest?.Data;
                                    ReportPrintTool rpt = new ReportPrintTool(report);

                                    if (!string.IsNullOrWhiteSpace(_defaultPrinter))
                                    {
                                        // 设置默认打印机
                                        rpt.PrinterSettings.PrinterName = _defaultPrinter;
                                    }
                                    //默认打印一份
                                    rpt.Print();

                                    WebSocketSend(JsonConvert.SerializeObject(PrintResult.Begin(printDevReqest?.TemplateCode), _serializerSettings));
                                }
                            }
                            else
                            {
                                WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(printDevReqest?.TemplateCode, "未找到对应模板"), _serializerSettings));
                            }
                            break;
                        }
                    case "print-pdf":
                        {
                            await PrintPdf(e.Data, recievedData);
                            break;
                        }
                    case "print-multi-pdf":
                        {
                            //HttpClient优化版
                            await PrintMultiPdf(e.Data, recievedData);
                            break;
                        }
                    case "print-multi-temp":
                        {
                            //客户端生成模版
                            await PrintMultiByTemplate(e.Data, recievedData.SourceType);
                            break;
                        }
                    default:
                        WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(recievedData.TemplateCode, "未找到对应操作，请检查Operator参数"), _serializerSettings));
                        break;
                }
            }
            catch (Exception ex)
            {
                _log.Error("打印失败：{Error}", ex.ToString());
                WebSocketSend(JsonConvert.SerializeObject(JsonResult.Fail($"打印失败：{ex.Message}")));
            }
        }

        /// <summary>
        /// 打印PDF
        /// </summary
        /// <param name="requestString"></param>
        /// <param name="sourceType"></param>
        public async Task PrintPdf(string requestString, PrintReqest recievedData)
        {
            ESourceType sourceType = recievedData.SourceType ?? ESourceType.Default;
            PrintPdfReqest printPdfRquest = JsonConvert.DeserializeObject<PrintPdfReqest>(requestString, _serializerSettings) ?? new PrintPdfReqest();
            if (string.IsNullOrWhiteSpace(printPdfRquest?.PdfUrl))
            {
                WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(printPdfRquest?.TemplateCode, "PdfUrl不能为空"), _serializerSettings));
                return;
            }

            //加载PDF文档
            PdfDocument pdfDocument = new PdfDocument();
            PrintSetComment(pdfDocument, printPdfRquest.PaperName, sourceType);
            //设置文档打印页码范围（指定页打印）
            if (printPdfRquest.Pages != null && printPdfRquest.Pages.Length > 0)
            {
                pdfDocument.PrintSettings.SelectSomePages(printPdfRquest.Pages);
            }
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                using (HttpClient client = new HttpClient(handler))
                {
                    HttpResponseMessage response = await client.GetAsync(printPdfRquest.PdfUrl);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // 响应成功，可以获取文件流
                        using (Stream stream = await response.Content.ReadAsStreamAsync())
                        {
                            PdfDocument doc = new PdfDocument();
                            if (recievedData.SourceType == ESourceType.A5ToA4)
                            {
                                doc.PageSettings.Size = PdfPageSize.A5;
                                doc.PageSettings.SetMargins(0, 0, 0, 0);
                                doc.LoadFromStream(stream);

                                int pageCount = doc.Pages.Count;
                                for (int i = 0; i < pageCount; i += 2)
                                {
                                    PdfPageBase? page1 = doc.Pages[i];
                                    PdfPageBase? page2 = null;
                                    if (i + 1 < pageCount)
                                    {
                                        page2 = doc.Pages[i + 1];
                                    }
                                    pdfDocument.PageSettings.SetMargins(0, 0, 0, 0);
                                    // 添加新的页面
                                    PdfPageBase page = pdfDocument.Pages.Add(PdfPageSize.A4);

                                    // 将第一个PDF文件的内容绘制到页面的上半部分
                                    page.Canvas.DrawTemplate(page1.CreateTemplate(), PointF.Empty);
                                    // 将第二个PDF文件的内容绘制到页面的下半部分
                                    if (page2 != null)
                                    {
                                        page.Canvas.DrawTemplate(page2.CreateTemplate(), new PointF(0, page1.Size.Width));
                                    }
                                }
                            }
                            else
                            {
                                doc.LoadFromStream(stream);
                                pdfDocument.AppendPage(doc);
                            }

                            if (recievedData.IsTransverse)
                            {
                                for (var i = 0; i < pdfDocument.Pages.Count; i++)
                                {
                                    pdfDocument.Pages[i].Rotation = PdfPageRotateAngle.RotateAngle90;
                                }
                            }
                        }
                    }
                    else
                    {
                        _log.Info($"response.StatusCode:{response.StatusCode},url:{printPdfRquest.PdfUrl},response:{response},打印文件下载失败...");
                        WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(printPdfRquest.TemplateCode, $"文件下载失败,状态码:{response.StatusCode}"), _serializerSettings));
                        return;
                    }
                }
            }
            catch (Exception)
            {
                WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(printPdfRquest.TemplateCode, "下载PDF文件失败"), _serializerSettings));
                return;
            }

            //打印
            await Print(pdfDocument, printPdfRquest.TemplateCode);
            pdfDocument.Dispose();
        }

        private static PaperSize GetPaperSizeByName(string printerName, string paperName)
        {

            PaperSize p = null;
            // 使用PrinterSettings获取打印机设置
            PrinterSettings printerSettings = new PrinterSettings();
            printerSettings.PrinterName = printerName;

            // 获取指定打印机的PaperSize集合
            PaperSizeCollection paperSizes = printerSettings.PaperSizes;

            // 打印所有PaperSize的信息
            foreach (PaperSize ps in paperSizes)
            {
                if (ps.PaperName.Equals(paperName))
                {
                    p = ps;
                    break;
                }
            }
            return p;
        }

        /// <summary>
        /// 批量打印PDF-HttpClient方式
        /// </summary>
        /// <param name="requestString"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public async Task PrintMultiPdf(string requestString, PrintReqest recievedData)
        {
            PrintMultiPdfReqest? printPdfRquest = JsonConvert.DeserializeObject<PrintMultiPdfReqest>(requestString, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            if (printPdfRquest == null || printPdfRquest.PdfUrls.Count() <= 0)
            {
                WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(printPdfRquest?.TemplateCode, "PdfUrls不能为空"), _serializerSettings));
                return;
            }

            var tempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SamjanTray", "temp");
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);

            try
            {
                var pageSise = string.IsNullOrEmpty(ConfigurationManager.AppSettings["PrintPage"]) ? 3 : Convert.ToInt32(ConfigurationManager.AppSettings["PrintPage"]);
                var groupedPages = printPdfRquest.PdfUrls.Select((ds, index) => new { Index = index, urls = ds })
                                              .GroupBy(x => x.Index / pageSise)
                                              .Select(g => g.Select(x => x.urls).ToList())
                                              .ToList();

                // 发到打印机开始打印
                WebSocketSend(JsonConvert.SerializeObject(PrintResult.Begin(printPdfRquest?.TemplateCode), _serializerSettings));

                foreach (var groupUrl in groupedPages)
                {
                    //加载PDF文档
                    using (PdfDocument pdfDocument = new PdfDocument())
                    {
                        var handler = new HttpClientHandler();
                        handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                        using (var client = new HttpClient(handler))
                        {
                            List<PdfDocument> pdfs = new List<PdfDocument>();
                            var tasks = groupUrl.Select(async url =>
                            {
                                var response = await client.GetAsync(url);
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    // 响应成功，可以获取文件流
                                    using (var stream = await response.Content.ReadAsStreamAsync())
                                    {
                                        Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
                                        lock (pdfDocument)
                                        {
                                            if (recievedData.SourceType == ESourceType.A5ToA4)
                                            {
                                                doc.PageSettings.Size = PdfPageSize.A5;
                                                doc.PageSettings.SetMargins(0, 0, 0, 0);
                                                doc.LoadFromStream(stream);
                                                pdfs.Add(doc);
                                            }
                                            else
                                            {
                                                doc.LoadFromStream(stream);
                                                pdfDocument.AppendPage(doc);
                                            }

                                            if (recievedData.IsTransverse)
                                            {
                                                for (var i = 0; i < pdfDocument.Pages.Count; i++)
                                                {
                                                    pdfDocument.Pages[i].Rotation = PdfPageRotateAngle.RotateAngle90;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    _log.Error($"response.StatusCode:{response.StatusCode}, url:{url},response:{response},打印文件下载失败...");
                                    WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(printPdfRquest?.TemplateCode, $"文件下载失败,状态码:{response.StatusCode}"), _serializerSettings));
                                    return;
                                }
                            });

                            await Task.WhenAll(tasks);
                            if (recievedData.SourceType == ESourceType.A5ToA4 && pdfs.Any())
                            {
                                List<PdfPageBase> pageBases = new List<PdfPageBase>();
                                foreach (PdfDocument pdf in pdfs)
                                {
                                    for (int i = 0; i < pdf.Pages.Count; i++)
                                    {
                                        var page = pdf.Pages[i];
                                        pageBases.Add(page);
                                    }
                                }

                                int pageCount = pageBases.Count;
                                for (int i = 0; i < pageCount; i += 2)
                                {
                                    PdfPageBase? page1 = pageBases[i];
                                    PdfPageBase? page2 = null;
                                    if (i + 1 < pageCount)
                                    {
                                        page2 = pageBases[i + 1];
                                    }
                                    pdfDocument.PageSettings.SetMargins(0, 0, 0, 0);
                                    // 添加新的页面
                                    PdfPageBase page = pdfDocument.Pages.Add(PdfPageSize.A4);

                                    // 将第一个PDF文件的内容绘制到页面的上半部分
                                    page.Canvas.DrawTemplate(page1.CreateTemplate(), PointF.Empty);
                                    // 将第二个PDF文件的内容绘制到页面的下半部分
                                    if (page2 != null)
                                    {
                                        page.Canvas.DrawTemplate(page2.CreateTemplate(), new PointF(0, page1.Size.Width));
                                    }
                                }

                            }
                        }
                        _log.Info($"TemplateCode:{printPdfRquest?.TemplateCode},开始打印...");
                        PrintSetComment(pdfDocument, printPdfRquest.PaperName, recievedData.SourceType);
                        MultiPrint(pdfDocument, printPdfRquest?.TemplateCode);
                    }
                }

                // 打印成功
                WebSocketSend(JsonConvert.SerializeObject(PrintResult.Finished(printPdfRquest?.TemplateCode), _serializerSettings));
            }
            catch (Exception ex)
            {
                WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(printPdfRquest?.TemplateCode, "下载PDF文件失败"), _serializerSettings));
                _log.Error("Error：" + ex);
                return;
            }
        }

        /// <summary>
        /// 根据打印中心模板生成PDF打印
        /// </summary>
        /// <param name="requestString"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public async Task PrintMultiByTemplate(string requestString, ESourceType? sourceType)
        {
            PrintMultiPdfReqest? printPdfRquest = JsonConvert.DeserializeObject<PrintMultiPdfReqest>(requestString, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            if (printPdfRquest == null || printPdfRquest.Data.Count() <= 0)
            {
                WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(printPdfRquest?.TemplateCode, "Pdf打印单数据异常"), _serializerSettings));
                return;
            }

            try
            {
                string[] filepaths;
                try
                {
                    string reportFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Report");
                    filepaths = Directory.EnumerateFiles(reportFilePath).ToArray();
                }
                catch
                {
                    WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(printPdfRquest?.TemplateCode, "未找到对应模板,请先下载打印模板"), _serializerSettings));
                    return;
                }

                if (filepaths.Length == 0)
                {
                    WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(printPdfRquest?.TemplateCode, "未找到对应模板,请先下载打印模板"), _serializerSettings));
                    return;
                }

                var datasets = printPdfRquest.Data;
                var ds = datasets.FirstOrDefault();
                if (ds != null)
                {
                    var templateId = ds.Tables["inputDto"].Rows[0]["templateId"].ToString();
                    var filename = filepaths.FirstOrDefault(filepath => filepath.Contains(templateId));
                    string reportType = Path.GetExtension(filename)?.TrimStart('.').ToLower();
                    if (reportType == "frx")
                    {
                        // 发到打印机开始打印
                        WebSocketSend(JsonConvert.SerializeObject(PrintResult.Begin(printPdfRquest?.TemplateCode), _serializerSettings));

                        await ExportMultiFastReportToPdf(datasets, filepaths, printPdfRquest.PaperName, printPdfRquest?.TemplateCode, sourceType);

                        // 打印成功
                        WebSocketSend(JsonConvert.SerializeObject(PrintResult.Finished(printPdfRquest?.TemplateCode), _serializerSettings));
                    }
                    if (reportType == "repx")
                    {
                        PdfDocument pdfDocument = new();
                        PrintSetComment(pdfDocument, printPdfRquest.PaperName, sourceType);
                        foreach (var devds in datasets)
                        {
                            var devtemplateId = devds.Tables["inputDto"].Rows[0]["templateId"].ToString();
                            var devfilename = filepaths.FirstOrDefault(filepath => filepath.Contains(devtemplateId));
                            if (string.IsNullOrEmpty(devfilename))
                            {
                                WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(printPdfRquest?.TemplateCode, "未找到对应模板,请先下载打印模板"), _serializerSettings));
                                return;
                            }
                            await ExportDevExpressToPdf(devfilename, devds, pdfDocument);
                        }

                        //批量打印通用方法
                        await Print(pdfDocument, printPdfRquest?.TemplateCode);
                    }
                }
                else
                {
                    WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(printPdfRquest?.TemplateCode, "打印数据异常，打印失败"), _serializerSettings));
                    return;
                }

            }
            catch (Exception ex)
            {
                WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(printPdfRquest?.TemplateCode, "生成PDF文件失败"), _serializerSettings));
                _log.Error("Error：" + ex.Message);
                return;
            }
        }

        /// <summary>
        /// 分批次打印
        /// </summary>
        /// <param name="datasets"></param>
        /// <param name="pdfDocument"></param>
        /// <param name="templateCode"></param>
        /// <returns></returns>
        private async Task ExportMultiFastReportToPdf(IEnumerable<DataSet> datasets, string[] filepaths, string paperName, string templateCode, ESourceType? sourceType)
        {
            var timeout = string.IsNullOrEmpty(ConfigurationManager.AppSettings["PrintPage"]) ? 3 : Convert.ToInt32(ConfigurationManager.AppSettings["PrintPage"]);

            var groupedDatasets = datasets.Select((ds, index) => new { Index = index, Dataset = ds })
                                          .GroupBy(x => x.Index / timeout)
                                          .Select(g => g.Select(x => x.Dataset).ToList())
                                          .ToList();

            foreach (var group in groupedDatasets)
            {
                //加载PDF文档
                PdfDocument pdfDocument = new PdfDocument();
                PrintSetComment(pdfDocument, paperName, sourceType);

                var tempPdfDocument = await GeneratePdfDocument(group, filepaths, templateCode);
                pdfDocument.AppendPage(tempPdfDocument);

                //分批打印
                MultiPrint(pdfDocument, templateCode);
            }
        }

        private async Task<PdfDocument> GeneratePdfDocument(List<DataSet> datasets, string[] filepaths, string templateCode)
        {
            PdfDocument pdfDocument = new PdfDocument();
            foreach (DataSet ds in datasets)
            {
                var templateId = ds.Tables["inputDto"].Rows[0]["templateId"].ToString();
                var filename = filepaths.FirstOrDefault(filepath => filepath.Contains(templateId));
                if (string.IsNullOrEmpty(filename))
                {
                    WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(templateCode, "未找到对应模板,请先下载打印模板"), _serializerSettings));
                    return pdfDocument;
                }

                using (FastReport.Report report = new FastReport.Report())
                {
                    report.Load(filename);
                    report.RegisterData(ds);
                    report.Prepare();

                    using (MemoryStream stream = new MemoryStream())
                    {
                        PDFExport pdf = new PDFExport();
                        pdf.Export(report, stream);
                        stream.Position = 0;

                        PdfDocument tempPdfDocument = new PdfDocument();
                        tempPdfDocument.LoadFromStream(stream);
                        pdfDocument.AppendPage(tempPdfDocument);
                    }
                }
            }

            return pdfDocument;
        }

        /// <summary>
        /// DevExpressToPdf
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="ds"></param>
        /// <param name="pdfDocument"></param>
        private async Task ExportDevExpressToPdf(string filename, DataSet ds, PdfDocument pdfDocument)
        {
            await Task.Run(() =>
            {
                using (var report = new XtraReport())
                {
                    report.Name = ds.Tables["inputDto"].Rows[0]["templateId"].ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");
                    report.LoadLayout(filename);
                    report.DataSource = ds;

                    using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                    {
                        report.ExportToPdf(stream);
                        stream.Position = 0;
                        //pdfDocument.LoadFromStream(stream);

                        Spire.Pdf.PdfDocument tempPdfDocument = new Spire.Pdf.PdfDocument();
                        tempPdfDocument.LoadFromStream(stream);
                        pdfDocument.AppendPage(tempPdfDocument);
                    }
                }

            });
        }

        /// <summary>
        /// 批量打印通用方法
        /// </summary>
        private void PrintSetComment(PdfDocument pdfDocument, string? printPaperName, ESourceType? sourceType)
        {
            try
            {
                //设置打印机
                var printerName = GetDefaultPrinter(sourceType);
                if (!printerName.IsNullOrEmpty())
                {
                    pdfDocument.PrintSettings.PrinterName = printerName;
                }

                var paperName = !string.IsNullOrWhiteSpace(printPaperName) ? printPaperName : GetDefaultPaperName(sourceType);
                PaperSize paperSize = GetPaperSizeByName(printerName, paperName);

                //设置打印的纸张大小
                pdfDocument.PrintSettings.PaperSize = paperSize;
                pdfDocument.PrintSettings.SelectSinglePageLayout(PdfSinglePageScalingMode.ActualSize, false);

                pdfDocument.PrintSettings.SetPaperMargins(0, 0, 0, 0);

                // 根据不同的长宽比旋转90度
                int printScale = pdfDocument.PrintSettings.PaperSize.Width / pdfDocument.PrintSettings.PaperSize.Height;
                float documentScale = pdfDocument.PageSettings.Size.Width / pdfDocument.PageSettings.Size.Height;

                if (!(printScale < 1 && documentScale < 1))
                {
                    for (int i = 0; i < pdfDocument.Pages.Count; i++)
                    {
                        pdfDocument.Pages[i].Rotation = PdfPageRotateAngle.RotateAngle90;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("打印设置异常 Error：" + ex);
                return;
            }
        }

        /// <summary>
        /// 批量打印通用方法
        /// </summary>
        private async Task Print(PdfDocument pdfDocument, string templateCode)
        {
            // 发到打印机开始打印
            WebSocketSend(JsonConvert.SerializeObject(PrintResult.Begin(templateCode), _serializerSettings));
            //使用默认打印机打印文档所有页面
            try
            {
                await Task.Run(() =>
                {
                    pdfDocument.Print();
                });
            }
            catch (Exception ex)
            {
                WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(templateCode, "下载异常"), _serializerSettings));
                _log.Error("Error：" + ex);
                return;
            }
            // 打印成功
            WebSocketSend(JsonConvert.SerializeObject(PrintResult.Finished(templateCode), _serializerSettings));
        }

        /// <summary>
        /// 批量打印通用方法
        /// </summary>
        private void MultiPrint(PdfDocument pdfDocument, string templateCode)
        {
            _log.Info("Print：" + pdfDocument);

            //使用默认打印机打印文档所有页面
            try
            {
                pdfDocument.Print();
            }
            catch (Exception ex)
            {
                WebSocketSend(JsonConvert.SerializeObject(PrintResult.Error(templateCode, "下载异常"), _serializerSettings));
                _log.Error("Error：" + ex);
                return;
            }
        }

        /// <summary>
        /// 根据打印的来源判断使用指定的打印机
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetDefaultPrinter(ESourceType? type)
        {
            if (!type.HasValue)
            {
                return ConfigurationManager.AppSettings["DefaultPrinter"] ?? string.Empty;
            }
            switch (type.Value)
            {
                case ESourceType.Default:
                    return ConfigurationManager.AppSettings["DefaultPrinter"] ?? string.Empty;
                case ESourceType.DoctorsAdvice:
                    return ConfigurationManager.AppSettings["DefaultDoctorsAdvicePrinter"] ?? string.Empty;
                case ESourceType.Emr:
                    return ConfigurationManager.AppSettings["DefaultEmrPrinter"] ?? string.Empty;
                case ESourceType.PastingBottles:
                    return ConfigurationManager.AppSettings["PastingBottlesPrinter"] ?? string.Empty;
                case ESourceType.WristStrap:
                    return ConfigurationManager.AppSettings["WristStrapPrinter"] ?? string.Empty;
                default:
                    break;
            }
            return ConfigurationManager.AppSettings["DefaultPrinter"] ?? string.Empty;
        }

        /// <summary>
        /// 根据打印的来源判断使用指定的纸张规格
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetDefaultPaperName(ESourceType? type)
        {
            string defaultPagerName = "A5";
            var getTypePaperName = (string settingName) =>
            {
                return !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[settingName]) ? ConfigurationManager.AppSettings[settingName] : defaultPagerName;
            };
            if (!type.HasValue)
            {
                return getTypePaperName("DefaultPaperName");
            }
            switch (type.Value)
            {
                case ESourceType.Default:
                    return getTypePaperName("DefaultPaperName");
                case ESourceType.DoctorsAdvice:
                    return getTypePaperName("DefaultDoctorsAdvicePaperName");
                case ESourceType.Emr:
                    return getTypePaperName("DefaultEmrPaperName");
                case ESourceType.PastingBottles:
                    return getTypePaperName("PastingBottlesPaperName");
                case ESourceType.WristStrap:
                    return getTypePaperName("WristStrapPaperName");
                default:
                    break;
            }
            return getTypePaperName("DefaultPaperName");
        }
    }
}
