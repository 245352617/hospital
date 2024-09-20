using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Etos.EMRs;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.ECIS.ShareModel.Utils;
using YiJian.EMR.Writes.Dto;

namespace YiJian.EMR.Writes
{
    /// <summary>
    /// 书写病历总线部分
    /// </summary>
    public partial class WriteAppService
    {
        /// <summary>
        /// 获取预览地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ResponseBase<string>> GetUrlAsync(Guid id)
        {
            Guid[] ids = new Guid[] { id };
            var data = await GetUrlsAsync(ids);
            if (data.Any())
            {
                return new ResponseBase<string>(EStatusCode.COK, data: data.FirstOrDefault().Value);
            }
            return new ResponseBase<string>(EStatusCode.CNULL);
        }

        /// <summary>
        /// 补救方法（如果前端找不到数据可以调用），推送电子病例到minio对象存储服务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseBase<bool>> PushEmrAsync(Guid id)
        {
            var entity = await (await _patientEmrRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == id);
            if (entity == null) return new ResponseBase<bool>(EStatusCode.CNULL, false, "没有你指定的电子病例");
            var xmlEntity = await (await _patientEmrXmlRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.PatientEmrId == entity.Id);
            if (xmlEntity == null) return new ResponseBase<bool>(EStatusCode.CNULL, false, "没有你指定的XML电子病例文档");

            var xmlEto = new XmlModelEto($"{entity.EmrTitle}_{xmlEntity.Id}.pdf", xmlEntity.EmrXml, entity.DoctorName, entity.PatientName);
            await _capPublisher.PublishAsync("yijian.emr.saveAsPDF", xmlEto);
            return new ResponseBase<bool>(EStatusCode.COK, true);
        }

        /// <summary>
        /// 获取预览地址
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        private async Task<Dictionary<Guid, string>> GetUrlsAsync(Guid[] ids)
        {
            var entities = await (await _patientEmrRepository.GetQueryableAsync()).Where(w => ids.Contains(w.Id)).ToListAsync();
            if (!entities.Any()) return new Dictionary<Guid, string>();

            var patientEmrIds = entities.Select(s => s.Id).ToList();

            var xmlEntities = await (await _patientEmrXmlRepository.GetQueryableAsync())
                .Where(w => patientEmrIds.Contains(w.PatientEmrId))
                .Select(s => new { s.Id, s.PatientEmrId }).ToListAsync();

            if (!xmlEntities.Any()) return new Dictionary<Guid, string>();

            var setting = _minio.CurrentValue;
            try
            {
                var minio = new MinioClient()
                    .WithEndpoint(setting.Endpoint)
                    .WithCredentials(setting.AccessKey, setting.SecretKey)
                    .Build();

                Dictionary<Guid, string> urls = new();

                foreach (var entity in entities)
                {
                    var xmlEntity = xmlEntities.FirstOrDefault(w => w.PatientEmrId == entity.Id);
                    if (xmlEntity == null) continue;

                    // var fileName = $"{UrlUtil.UrlEncode(entity.EmrTitle)}_{xmlEntity.Id}.pdf";
                    string replaceFileName = entity.EmrTitle.Replace('\\', '-').Replace('/', '-')
                            .Replace('&', '-').Replace('?', '-').Replace('%', '-');
                    var fileName = $"{replaceFileName}_{xmlEntity.Id}.pdf";
                    var objectName = $"{entity.DoctorName}/{entity.PatientName}/{fileName}";

                    var presigned = new PresignedGetObjectArgs()
                        .WithBucket(setting.Bucket.Emr)
                        .WithObject(objectName)
                        .WithExpiry(604800); //设最长
                    var url = await minio.PresignedGetObjectAsync(presigned);

                    urls.Add(entity.Id, url);
                    _logger.LogInformation($"id={entity.Id}生成的PDF地址：{url}");
                }

                return urls;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"file upload error :{e.Message}");
                return new Dictionary<Guid, string>();
            }
        }


        /// <summary>
        /// 打印合并的电子病历
        /// </summary>
        /// <param name="piid">患者唯一Id</param>
        /// <param name="originId">电子病历模板地址</param>
        /// <returns></returns>
        //[AllowAnonymous]
        public async Task<ResponseBase<PrintMergeEmrDto>> GetPrintMergeEmrAsync(Guid piid, Guid originId)
        {
            var entity = await (await _patientEmrRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.PI_ID == piid && w.OriginId == originId && !w.IsDeleted);
            var setting = _minio.CurrentValue;
            try
            {
                var minio = new MinioClient()
                    .WithEndpoint(setting.Endpoint)
                    .WithCredentials(setting.AccessKey, setting.SecretKey)
                    .Build();

                var fileName = $"{entity.EmrTitle}_{piid}.pdf";
                var objectName = $"{entity.PatientName}/{fileName}";

                var presigned = new PresignedGetObjectArgs()
                    .WithBucket(setting.Bucket.Merge)
                    .WithObject(objectName)
                    .WithExpiry(604800); //设最长
                var url = await minio.PresignedGetObjectAsync(presigned);
                _logger.LogInformation($"id={entity.Id}生成的PDF地址：{url}");
                var data = new PrintMergeEmrDto
                {
                    EmrTitle = entity.EmrTitle,
                    PatientName = entity.PatientName,
                    OriginId = originId,
                    Piid = piid,
                    Pdf = url
                };
                return new ResponseBase<PrintMergeEmrDto>(EStatusCode.COK, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取打印合并的电子病历异常：{ex.Message}");
                return new ResponseBase<PrintMergeEmrDto>(EStatusCode.CFail);
            }

        }

        /// <summary>
        /// 根据就诊流水号获取患者当前就诊的所有电子病历
        /// </summary>
        /// <param name="visitSerialNo"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ResponseBase<List<EmrVisitSerialDto>>> GetEmrByVisitSerialNoAsync(string visitSerialNo)
        {
            var emrs = await _patientEmrRepository.GetEmrByVisitSerialAsync(visitSerialNo);
            var ids = emrs.Select(s => s.Id);
            if (!ids.Any()) return new ResponseBase<List<EmrVisitSerialDto>>(EStatusCode.COK, data: null, message: $"就诊流水号：{visitSerialNo}，没有病历记录");

            List<EmrVisitSerialDto> data = new();
            var urls = await GetUrlsAsync(ids.ToArray());
            foreach (var item in urls)
            {
                var entity = emrs.FirstOrDefault(w => w.Id == item.Key);
                data.Add(new EmrVisitSerialDto(emrTitle: entity.EmrTitle, pdf: item.Value, patientName: entity.PatientName, doctorName: entity.DoctorName));
            }
            return new ResponseBase<List<EmrVisitSerialDto>>(EStatusCode.COK, data: data);
        }
    }
}
