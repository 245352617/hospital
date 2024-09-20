using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Minio;
using System;
using System.Threading.Tasks;
using YiJian.EMR.DCWriter.Models;
using System.IO;
using DCSoft.Writer.Dom;
using DotNetCore.CAP;
using System.Linq;
using Microsoft.Extensions.Configuration;
using YiJian.ECIS.ShareModel.Etos.EMRs;
using Microsoft.Extensions.Logging;
using YiJian.ECIS.ShareModel.Models;
using DCSoft.Writer;
using YiJian.ECIS.ShareModel.Utils;
using DCSoft.Drawing;
using YiJian.EMR.DCWriter.Services;
using Volo.Abp.Json.Newtonsoft;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;

namespace YiJian.EMR.DCWriter.Controllers
{
    /// <summary>
    /// 对象存储
    /// </summary>
    [Route("api/minio")]
    public class MinioController : ICapSubscribe
    {
        private readonly IOptionsMonitor<MinioSetting> _minio;
        private readonly ILogger<MinioController> _logger;
        private readonly IConfiguration _configration;
        private readonly string _dcRegisterCode;
        private readonly IMasterRemoteervice _masterRemoteervice;

        public MinioController(IOptionsMonitor<MinioSetting> minio,
            IConfiguration configuration,
            ILogger<MinioController> logger,
            IMasterRemoteervice masterRemoteervice)
        {
            _minio = minio;
            _logger = logger;
            _configration = configuration;
            _dcRegisterCode = _configration["DcRegistrationCode:Code"];
            _masterRemoteervice = masterRemoteervice;
        }

        /// <summary>
        /// 另存为PDF
        /// </summary>
        /// <param name="xml">xml 文档-字符串的方式传递过来</param>
        /// <param name="fileName">pdf的文件名称 eg:出诊病历_2022_09_05_001.pdf </param>
        /// <returns></returns> 
        [HttpPost("save-as-pdf")]
        [CapSubscribe("yijian.emr.saveAsPDF")]
        public async Task SaveAsPDFAsync([FromBody] XmlModelEto model)
        {
            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // UrlEncode会导致在访问文件的时候会找不到路径
                //var filename = UrlUtil.UrlEncode(model.FileName);
                var filename = model.FileName.Replace('\\', '-').Replace('/', '-')
                    .Replace('&', '-').Replace('?', '-').Replace('%', '-');
                var fullName = Path.Combine(path, filename);

                if (File.Exists(fullName))
                {
                    //File.Delete(fullName);
                    var dotIndex = fullName.LastIndexOf(".");
                    var newFullName =
                        $"{fullName.Substring(0, dotIndex)}_{DateTime.Now.Ticks}.{fullName.Substring(dotIndex + 1)}";
                    File.Move(fullName, newFullName);
                }

                // 1. 保存文件
                XTextDocument doc = new(); //获去到前端的文档
                doc.RegisterCode = _dcRegisterCode;
                 
                doc.LoadFromString(model.Xml, "xml"); 

                doc.PageSettings.Watermark  = null; //清空水印

                //添加水印
                await WriterWatermarkingAsync(doc);
                doc.SaveToFile($"{fullName}.xml","xml");
                doc.SaveToFile(fullName, "pdf");

                //2 上传到对象存储服务
                var setting = _minio.CurrentValue;
                var minio = new MinioClient()
                    .WithEndpoint(setting.Endpoint)
                    .WithCredentials(setting.AccessKey, setting.SecretKey)
                    .Build();

                var objectName = $"{model.DoctorName}/{model.PatientName}/{filename}";
                var contentType = "application/pdf";
                var bucketExists = new BucketExistsArgs().WithBucket(setting.Bucket.Emr);

                bool found = await minio.BucketExistsAsync(bucketExists);
                var bucket = setting.Bucket.Emr;
                var make = new MakeBucketArgs()
                    .WithBucket(bucket)
                    .WithLocation("us-east-1");
                if (!found) await minio.MakeBucketAsync(make);

                var putObject = new PutObjectArgs()
                    .WithBucket(bucket)
                    .WithObject(objectName)
                    .WithFileName(fullName)
                    .WithContentType(contentType);
                await minio.PutObjectAsync(putObject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存PDF文件异常：" + ex.Message);
            }
        }
  
        /// <summary>
        /// 给电子病历添加水印(异常之后水印无效)
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private async Task WriterWatermarkingAsync(XTextDocument doc)
        {
            try
            {
                var emrWatermark = await _masterRemoteervice.GetEmrWatermarkingAsync();

                if (emrWatermark.Enabled)
                {
                    var watermarking = emrWatermark.Watermark;

                    var font = watermarking.Font.Split(",");
                    var fontValue = new XFontValue(font[0], float.Parse(font[1]));
                    //fontValue.Bold = true;

                    //添加背景水印 
                   var watermark = new WatermarkInfo()
                    {
                        Alpha = int.Parse(watermarking.Alpha),
                        Angle = float.Parse(watermarking.Angle),
                        //BackColor = watermarking.BackColorValue,
                        BackColorValue = watermarking.BackColorValue,
                        //Color = watermarking.ColorValue,
                        ColorValue = watermarking.ColorValue,
                        DensityForRepeat = float.Parse(watermarking.DensityForRepeat),
                        Font = fontValue,
                        Repeat = true, //bool.Parse(watermarking.Repeat),
                        Text = watermarking.Text,
                        Type = watermarking.Type.ToLower() == "text" ? WatermarkType.Text : (watermarking.Type.ToLower() == "image" ? WatermarkType.Image : WatermarkType.None)
                    };

                    doc.PageSettings.TopMargin = 0;
                    doc.PageSettings.BottomMargin = 0;
                    doc.PageSettings.Watermark = watermark;

                    _logger.LogInformation($"水印配置数据：{Newtonsoft.Json.JsonConvert.SerializeObject(doc.PageSettings.Watermark)}");

                }
            }
            catch (Exception ex)
            {
               _logger.LogError(ex, $"配置电子病历水印异常:{ex.Message}");
            } 
        }

        ///// <summary>
        ///// 测试水印
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("test-watermarking")]
        //[AllowAnonymous]
        //public async Task<string> TestWatermarkingAsync()
        //{
        //    //var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xml", $"consultation_record.xml"); //会诊记录xml电子病历模板 
        //    var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xml", $"writed.xml"); //会诊记录xml电子病历模板 
        //    XTextDocument doc = new();//获去到前端的文档

        //    doc.LoadFromFile(file, "xml");
        //    doc.PageSettings.Watermark = null;
        //    await WriterWatermarkingAsync(doc);
        //    var filename = $"dcwriter_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
        //    var xml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xml", $"{filename}.xml");
        //    var pdf = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xml", $"{filename}.pdf");
        //    doc.SaveToFile(xml, "xml");
        //    doc.SaveToFile(pdf, "pdf");

        //    return doc.FileName;
        //}


    }
}