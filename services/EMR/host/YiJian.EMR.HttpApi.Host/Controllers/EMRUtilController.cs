using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.Libs;
using YiJian.EMR.Libs.Entities;

namespace YiJian.EMR.Controllers
{
    /// <summary>
    /// 电子病历工具包
    /// </summary>
    [Authorize]
    [Route("/api/ecis/emr/upload")]
    public class EmrUtilController : AbpController
    {
        private readonly ICatalogueRepository _catalogueRepository;
        private readonly IXmlTemplateRepository _xmlTemplateRepository;
        private readonly ILogger<EmrUtilController> _logger;

        /// <summary>
        /// 电子病历工具包
        /// </summary>
        /// <param name="catalogueRepository"></param>
        /// <param name="xmlTemplateRepository"></param>
        /// <param name="logger"></param>
        public EmrUtilController(
            ICatalogueRepository catalogueRepository,
            IXmlTemplateRepository xmlTemplateRepository,
            ILogger<EmrUtilController> logger
        )
        {
            _catalogueRepository = catalogueRepository;
            _xmlTemplateRepository = xmlTemplateRepository;
            _logger = logger;
        }

        /// <summary>
        /// 导入xml电子病历，非xml文件跳过不处理 
        /// </summary>
        /// <param name="formCollection">上传的文件</param>
        /// <param name="catalogueIds">目录Id，【上传多个的时候，上传的xml电子病历需要一一对应否则，后端抛弃】</param> 
        /// <returns></returns> 
        [HttpPost("import-xml")]
        public async Task<ResponseBase<bool>> ImportXmlAsync([FromForm] IFormCollection formCollection,Guid[] catalogueIds)
        {
            if (formCollection == null || formCollection.Files.Count == 0)
            {
                return new ResponseBase<bool>(EStatusCode.CNULL);
            }
 
            try
            {
                ResponseBase<int> ret = new ResponseBase<int>();

                FormFileCollection filelist = (FormFileCollection)formCollection.Files;

                if (filelist.Count != catalogueIds.Length)
                {
                    return new ResponseBase<bool>(EStatusCode.C10001);
                }

                List<XmlTemplate> xmls = new List<XmlTemplate>();

                int index = 0; 
                foreach (IFormFile file in filelist)
                {
                    //文件后缀
                    var fileExtension = Path.GetExtension(file.FileName).Trim('.').ToLower();//获取文件格式，拓展名
                    if (fileExtension != "xml")
                    {
                        ret.AppendExten(file.FileName, "该文件不是期望的xml文件");
                        continue;
                    }

                    string xml = string.Empty;

                    using (var stream = file.OpenReadStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        xml = await reader.ReadToEndAsync();
                    }

                    var catalogueId = catalogueIds[index];

                    var entity = await (await _xmlTemplateRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.CatalogueId == catalogueId);
                    if (entity != null)
                    {
                        entity.UpdateTemplateXml(xml);
                        var retModel = await _xmlTemplateRepository.UpdateAsync(entity);
                        xmls.Add(retModel);
                    }
                    else
                    {
                        var catalogue =(await _catalogueRepository.GetQueryableAsync()).FirstOrDefault(w => w.Id == catalogueIds[index]);
                        if (catalogue == null)
                        {
                            ret.AppendExten($"电子病历catalogueId={catalogueIds[index]}的目录不存在", "该文件不是期望的xml文件");
                        }
                        var xmlTemplate = new XmlTemplate(GuidGenerator.Create(), xml, catalogueIds[index]);
                        var retModel = await _xmlTemplateRepository.InsertAsync(xmlTemplate);
                        xmls.Add(retModel);
                    }
                    index++;
                }

                return new ResponseBase<bool>(EStatusCode.COK, true);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"导入xml异常，异常信息为:{ex.Message}");
                return new ResponseBase<bool>(EStatusCode.CFail, data: false, "导入xml异常");
            }
        }

    }
}
