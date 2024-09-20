using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkyWalking.NetworkProtocol.V3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YiJian.EMR.Templates.Contracts;
using YiJian.EMR.Templates.Entities;
using YiJian.EMR.Writes.Contracts;

namespace YiJian.EMR.Tools
{
    /// <summary>
    /// 电子病历运维工具
    /// </summary>
    [Authorize] 
    public class OpsAppService: EMRAppService
    {

        private readonly IMyXmlTemplateRepository _myXmlTemplateRepository;
        private readonly ILogger<OpsAppService> _logger;

        /// <summary>
        /// 电子病历运维工具
        /// </summary> 
        public OpsAppService(
            IMyXmlTemplateRepository myXmlTemplateRepository,
            ILogger<OpsAppService> logger)
        {
            _myXmlTemplateRepository = myXmlTemplateRepository;
            _logger = logger;
        }

        /// <summary>
        /// 擦除就的电子病历水印信息
        /// </summary>
        /// <returns></returns>
        private async Task<string> CleanOldWatermarkAsync()
        { 
            var list = await (await _myXmlTemplateRepository.GetQueryableAsync()).Select(s => s.Id).ToListAsync(); 
            foreach (var item in list)
            {
                var entity = await (await _myXmlTemplateRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == item);
                var clean = CleanWatermark(entity.TemplateXml);
                if (clean.Item1)
                {
                    entity.TemplateXml = clean.Item2;
                    await _myXmlTemplateRepository.UpdateAsync(entity, autoSave: true);
                    _logger.LogInformation($"清除掉的电子病历水印有 id =[ {entity.Id} ]");
                } 
            } 
            return "ok";
        }

        /// <summary>
        /// 擦除xml水印内容
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>第一个返回的是是否有水印，第二个返回的是xml的内容</returns>
        private Tuple<bool,string> CleanWatermark(string xml)
        { 
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml); 
            var pageSettings = doc.SelectSingleNode("XTextDocument/PageSettings");
            var ele = doc.SelectSingleNode("XTextDocument/PageSettings/Watermark");
            if (pageSettings is not null && ele is not null)
            {
                pageSettings.RemoveChild(ele);
                return new Tuple<bool, string>(true, ConvertXmlToString(doc));
            }
            return new Tuple<bool, string>(false,xml);
        }

        /// <summary>
        /// 将XmlDocument转化为string
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        private static string ConvertXmlToString(XmlDocument xmlDoc)
        {
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();
            return xmlString;
        }



    }
}
