using DCSoft.DCPDF;
using DCSoft.Writer.Dom;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Volo.Abp.Json.Newtonsoft;
using Volo.Abp.Json.SystemTextJson;
using YiJian.ECIS.ShareModel.Etos.EMRs;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.ShareModel.Models;
using YiJian.ECIS.ShareModel.Extensions;
using System.Xml.Linq;
using Microsoft.AspNetCore.WebUtilities;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Presentation;
using YiJian.ECIS.ShareModel.Utils;

namespace YiJian.EMR.DCWriter.Controllers
{
    /// <summary>
    /// 绑定电子病历数据源
    /// </summary>
    [Route("api/bind-database")]
    public class BindDatabaseController : ICapSubscribe
    {
        private readonly ILogger<MinioController> _logger;
        private readonly IConfiguration _configration;
        private readonly string _dcRegisterCode;
        private readonly ICapPublisher _capPublisher;  
        private readonly IOptionsMonitor<MinioSetting> _minio;

        public BindDatabaseController(IConfiguration configuration,
            ILogger<MinioController> logger,
            ICapPublisher capPublisher,
            IOptionsMonitor<MinioSetting> minio)
        {
            _logger = logger;
            _configration = configuration;
            _dcRegisterCode = _configration["DcRegistrationCode:Code"];
            _capPublisher = capPublisher;
            _minio = minio;
        }
        /* xml-test
        [HttpGet("xml-test")]
        public string TestXml()
        {
            XTextDocument doc = new();//获去到前端的文档
            doc.RegisterCode = _dcRegisterCode; //注册
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xml", "consultation_record.xml"); //会诊记录xml电子病历模板                             
            doc.LoadFromFile(file, "xml");
             
            string id = "consultationCategory"; 
            var fragementXml = doc.GetElementById(id);
            fragementXml.InnerText = "单科会诊 全院会诊 院外会诊 科室会诊";
            XTextElementList list = new XTextElementList();
             
            XTextCheckBoxElement check = new XTextCheckBoxElement()
            {
                ID = "1111",
                InnerID = 110111,
                Name = "单科会诊", 
                Width = 62,
                Height = 62,
                StyleIndex = 5,
            }; 
            list.Add(check);
             
            var txt = new XTextLabelElement(){ 
                ID = "1121",
                InnerText = "单科会诊 " 
            };
            list.Add(txt);
            check = new XTextCheckBoxElement()
            {
                ID = "1112",
                InnerID = 110112,
                Width = 62,
                Height = 62,
                Name = "全院会诊", 
            };
            list.Add(check);
            txt = new XTextLabelElement()
            {
                ID = "1122",
                InnerText = "全院会诊 "
            };
            list.Add(txt);
            check = new XTextCheckBoxElement()
            {
                ID = "1113",
                InnerID = 110113,
                Width = 62,
                Height = 62,
                Name = "院外会诊",
                CheckedValue = "true",
                Value = "true"
            };
            list.Add(check);
            txt = new XTextLabelElement()
            {
                ID = "1123",
                InnerText = "院外会诊 "
            };
            list.Add(txt);
            check = new XTextCheckBoxElement()
            {
                ID = "1114",
                InnerID = 110114,
                Width = 62,
                Height = 62,
                Name = "科室会诊", 
            };
            list.Add(check);
            txt = new XTextLabelElement()
            {
                ID = "1124",
                InnerText = "科室会诊"
            };
            list.Add(txt);
            fragementXml.Elements = list;

            //doc.Save("saveas_2.xml","xml");
             
            return fragementXml.ToString(); 
        }
        */

        /// <summary>
        ///  绑定会诊记录电子病历
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("bind-consultation-record")]
        [CapSubscribe("yijian.emr.bindConsultationRecord")]
        public async System.Threading.Tasks.Task BindConsultationRecordAsync([FromBody]BindConsultationRecordEto model)
        {
            try
            {
                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xml", "consultation_record.xml"); //会诊记录xml电子病历模板

                var visitNo = model.VisitNo.IsNullOrEmpty()? model.Data.PatientInfo.VisitNo: model.VisitNo; //防止第一次没有流水号
                if (visitNo.IsNullOrEmpty()) Oh.Error($"患者{model.PatientName}没有流水号，或者会诊申请流水号为空。");
                 
                var databind = new ConsultationRecordBindEto
                {
                    Classify = 0,
                    OrgCode = "H7110",
                    PatientId = model.PatientNo,
                    PatientName = model.PatientName,
                    PI_ID = model.Piid,
                    VisitNo = visitNo,
                    RegisterSerialNo = model.RegisterSerialNo,
                    WriterId = model.DoctorCode,
                    WriterName = model.DoctorName,
                    Data = model.Data.GetMap()
                };

                 
                foreach (var item in model.Data.InviteDepartmentDic)
                {
                    XTextDocument doc = new();//获去到前端的文档
                    doc.RegisterCode = _dcRegisterCode; //注册
                    //doc.LoadFromString(xml, "xml"); //doc加载XML文档 
                    doc.LoadFromFile(file, "xml");

                    #region 输出树结构

#if DEBUG
                    Dictionary<string, string[]> dic = new Dictionary<string, string[]>();
                    var datasource = doc.GetDataSourceBindingDescriptions()
                        .Where(w => !w.DataSource.IsNullOrEmpty())
                        .Select(s => new { s.DataSource, s.BindingPath })
                        .GroupBy(g => g.DataSource);

                    foreach (var data in datasource)
                    {
                        dic.Add(data.Key, data.Select(s => s.BindingPath).ToArray());
                    }
                    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(dic, Newtonsoft.Json.Formatting.Indented));
#endif
                    #endregion
                
                    doc.SetDocumentParameterValue("patientInfo", model.Data.PatientInfo.GetPatientInfoMap());
                    doc.SetDocumentParameterValue("medicalInfo", model.Data.MedicalInfo.GetMedicalInfoMap(item.Value));
                    doc.WriteDataFromDataSourceToDocument();
                    //var newXml = doc.ToString();
                    var newXml = doc.GetXMLText(false);
                    
                    var eto = new ConsultationRecordEmrEto()
                    {
                        AdmissionTime = model.AdmissionTime,
                        CategoryLv1 = "",
                        CategoryLv2 = "",
                        Title = "会诊记录",
                        Classify = 0,
                        DeptCode = model.DeptCode,
                        DeptName = model.DeptName,
                        DoctorCode = model.DoctorCode,
                        DoctorName = model.DoctorName,
                        PatientNo = model.PatientNo,
                        PatientName = model.PatientName,
                        PI_ID = model.Piid,
                        EmrXml = newXml,
                        DataBind = databind,
                        Diagnosis = model.Diagnosis,
                    };
                    eto.ModifyDept(item.Key, item.Value);
                    await _capPublisher.PublishAsync("emr.create.consultationRecord", eto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "绑定会诊记录电子病历异常：" + ex.Message);
            }

        }


        /// <summary>
        /// 合并留观病程（生命体征）的病历内容
        /// </summary>
        /// <returns></returns>
        [HttpPost("merge-and-save-as-pdf")]
        [CapSubscribe("emr.merge-and-save-as-pdf")]
        public async System.Threading.Tasks.Task MergeAndSaveAsPdfAsync(MergeCourseOfObservationVitalSignsEto eto)
        {
            try
            {
                if (eto.EmrXmls.Count < 2) return;
                //XTextDocument doc = new();//获去到前端的文档
                //doc.RegisterCode = _dcRegisterCode; //注册  

                //doc.LoadFromString(eto.EmrXmls[0], "xml");
                //var subitems = eto.EmrXmls.Skip(1).ToList();

                XTextElementList list = new XTextElementList();

                XTextDocument doc = new();//获去到前端的文档
                doc.RegisterCode = _dcRegisterCode; //注册 
                doc.LoadFromString(eto.EmrXmls[0], "xml");

                doc.RemoveChild(doc.Body);

                var subitems = eto.EmrXmls.ToList();
                
                foreach (var item in subitems)
                {
                    XTextDocument subdoc = new();//获去到前端的文档
                    subdoc.RegisterCode = _dcRegisterCode; //注册 
                    subdoc.LoadFromString(item, "xml");

                    list.Add(subdoc.Body);
                }

                doc.Body.AppendContentElements(list);

                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "merges");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filename = eto.FileName;
                //var filename = UrlUtil.UrlEncode(eto.FileName);

                var pdfFile = Path.Combine(path, eto.FileName); //会诊记录xml电子病历模板 
                

                if (File.Exists(pdfFile))
                {
                    var dotIndex = pdfFile.LastIndexOf(".");
                    var newFullName = $"{pdfFile.Substring(0, dotIndex)}_{DateTime.Now.Ticks}.{pdfFile.Substring(dotIndex + 1)}";
                    File.Move(pdfFile, newFullName);
                }

                doc.SaveToFile(pdfFile, "pdf");
                await UploadAsync(eto.PatientInfo.PatientName, filename, pdfFile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"合并留观病程（生命体征）的病历内容异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 将患者的病历合并之后上传到Minio上
        /// </summary> 
        /// <param name="patientName"></param> 
        /// <param name="filename">xxx.pdf</param>
        /// <param name="fullName">PDF的完整物理路径名称</param>
        /// <returns></returns>
        private async System.Threading.Tasks.Task UploadAsync(string patientName,string filename, string fullName)
        {
            try
            {
                var setting = _minio.CurrentValue;
                var minio = new MinioClient()
                   .WithEndpoint(setting.Endpoint)
                   .WithCredentials(setting.AccessKey, setting.SecretKey)
                   .Build(); 
                var objectName = $"{patientName}/{filename}";
                var contentType = "application/pdf";
                var bucketExists = new BucketExistsArgs().WithBucket(setting.Bucket.Merge);

                bool found = await minio.BucketExistsAsync(bucketExists);
                var bucket = setting.Bucket.Merge;
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
                _logger.LogError(ex,$"上传{filename}文件到minio异常：{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 保存PDF文件测试
        /// </summary>
        [HttpGet("save-pdf-test")]
        public void SavePdfTest()
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xml", "consultation_record.xml");
            var fullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pdfs", "consultation_record.pdf");
            XTextDocument doc = new(); 
            doc.RegisterCode = _dcRegisterCode;
            doc.LoadFromFile(file, "xml");  
            doc.SaveToFile(fullName, "pdf"); 

        }

        /* 测试模块
        [HttpGet("merge-test")]
        public void test()
        {
            try
            {
                IList<string> list = new List<string>();     
                for (int i = 1; i < 6; i++)
                {
                    var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xml", $"{i}.xml"); //会诊记录xml电子病历模板 
                    XTextDocument doc = new();//获去到前端的文档
                    //doc.RegisterCode = _dcRegisterCode; //注册 
                    doc.LoadFromFile(file, "xml");
                    //var xml = doc.GetXMLText(false).EncodeBase64(); 
                    //list.Add(xml); 
                    list.Add(doc.GetXMLText(false));
                }

                var eto = new MergeCourseOfObservationVitalSignsEto
                {
                    EmrXmls = list,
                    FileName = "留观病程（生命体征）_ee143d73-5f1d-26f4-f75b-3a07da1c597c.pdf",
                    PatientInfo = new VitalSignsPatientInfoEto
                    {
                        Age = "27岁",
                        ContactsPhone = "18681081080",
                        SexName = "男",
                        VisitNo = "300211",
                        PatientName = "林三",
                        TriageDeptName = "急诊外科"
                    }
                };

                _capPublisher.Publish("emr.merge-and-save-as-pdf", eto);  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"合并留观病程（生命体征）的病历内容异常：{ex.Message}"); 
            }
        }
        */

    }
}
