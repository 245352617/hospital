using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;
using YiJian.ECIS.ShareModel.Etos.EMRs;
using YiJian.ECIS.ShareModel.Models;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.ECIS.ShareModel.Utils;
using YiJian.EMR.DataBinds.Contracts;
using YiJian.EMR.DataBinds.Dto;
using YiJian.EMR.DataBinds.Entities;
using YiJian.EMR.Enums;
using YiJian.EMR.HospitalClients;
using YiJian.EMR.HttpClients.Dto;
using YiJian.EMR.Props.Contracts;
using YiJian.EMR.Templates.Contracts;
using YiJian.EMR.Templates.Dto;
using YiJian.EMR.Writes.Contracts;
using YiJian.EMR.Writes.Dto;
using YiJian.EMR.Writes.Entities;
using YiJian.EMR.XmlHistories.Contracts;
using YiJian.EMR.XmlHistories.Entities;

namespace YiJian.EMR.Writes
{
    /// <summary>
    /// 书写电子病历
    /// </summary>
    [Authorize]
    public partial class WriteAppService : EMRAppService, IWriteAppService, ICapSubscribe
    {
        //router key
        private const string pushRouteKey = "emr.patientInfo.case";
        private const string emrSaveAsPDF = "yijian.emr.saveAsPDF";
        private const string returnMedicalHistory = "emr.return.medical.history";
        private const string getMedicalHistory = "emr.get.medical.history";

        private readonly IOptionsMonitor<PushEmrDataSetting> _pushEmrData;
        private readonly IOptionsMonitor<EmrWatermarkModel> _emrWatermarkModel;
        private readonly ILogger<WriteAppService> _logger;
        private readonly IPatientEmrRepository _patientEmrRepository;
        private readonly IPatientEmrXmlRepository _patientEmrXmlRepository;
        private readonly ICategoryPropertyRepository _categoryPropertyRepository;
        private readonly IXmlHistoryRepository _xmlHistoryRepository;
        private readonly IDataBindMapRepository _dataBindMapRepository;
        private readonly IEmrBaseInfoRepository _emrBaseInfoRepository;
        private readonly IMergeTemplateWhiteListRepository _mergeTemplateWhiteListRepository;
        private readonly IMinioEmrInfoRepository _minioEmrInfoRepository;

        private readonly IDataBindContextRepository _dataBindContextRepository;
        private readonly IOptionsMonitor<MinioSetting> _minio;
        private readonly ICapPublisher _capPublisher;

        private readonly IOptionsMonitor<RemoteServices> _remoteServices;
        //private readonly IMasterDataAppService _masterDataAppService;
        private readonly IPatientAppService _patientAppService;
        private readonly IRecipeAppService _recipeAppService;

        private readonly IDataFilter _dataFilter;
        private readonly IConfiguration _configuration;


        /// <summary>
        /// 书写电子病历
        /// </summary> 
        public WriteAppService(
            IOptionsMonitor<PushEmrDataSetting> pushEmrData,
            IOptionsMonitor<EmrWatermarkModel> emrWatermarkModel,
            ILogger<WriteAppService> logger,
            IPatientEmrRepository patientEmrRepository,
            IPatientEmrXmlRepository patientEmrXmlRepository,
            ICategoryPropertyRepository categoryPropertyRepository,
            IXmlHistoryRepository xmlHistoryRepository,
            IDataBindMapRepository dataBindMapRepository,
            IDataBindContextRepository dataBindContextRepository,
            IEmrBaseInfoRepository emrBaseInfoRepository,
            IMergeTemplateWhiteListRepository mergeTemplateWhiteListRepository,
            IMinioEmrInfoRepository minioEmrInfoRepository,
            IOptionsMonitor<MinioSetting> minio,
            ICapPublisher capPublisher,
            IOptionsMonitor<RemoteServices> remoteServices,
            //IMasterDataAppService masterDataAppService,
            IPatientAppService patientAppService,
            IRecipeAppService recipeAppService,
            IDataFilter dataFilter,
            IConfiguration configuration
        )
        {
            _pushEmrData = pushEmrData;
            _emrWatermarkModel = emrWatermarkModel;
            _logger = logger;
            _patientEmrRepository = patientEmrRepository;
            _patientEmrXmlRepository = patientEmrXmlRepository;
            _categoryPropertyRepository = categoryPropertyRepository;
            _xmlHistoryRepository = xmlHistoryRepository;
            _dataBindContextRepository = dataBindContextRepository;
            _dataBindMapRepository = dataBindMapRepository;
            _emrBaseInfoRepository = emrBaseInfoRepository;
            _mergeTemplateWhiteListRepository = mergeTemplateWhiteListRepository;
            _minioEmrInfoRepository = minioEmrInfoRepository;
            _minio = minio;
            _capPublisher = capPublisher;
            _remoteServices = remoteServices;
            //_masterDataAppService = masterDataAppService;
            _patientAppService = patientAppService;
            _recipeAppService = recipeAppService;
            _dataFilter = dataFilter;
            _configuration = configuration;
        }

        /// <summary>
        /// 获取患者电子病历列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //[Authorize(EMRPermissions.Writes.List)] 
        [AllowAnonymous]
        public async Task<ResponseBase<List<PatientEmrDto>>> GetPatientEmrsAsync(PatientEmrRequestDto model)
        {
            model.GetAllRecord = false;
            var query = await QueryPatientEmrsAsync(model);
            var list = ObjectMapper.Map<List<PatientEmr>, List<PatientEmrDto>>(query);

            var ids = list.Select(s => s.Id.Value).ToList();
            var urls = await GetUrlsAsync(ids.ToArray());

            if (urls.Any())
            {
                list.ForEach(x =>
                {
                    x.PdfUrl = urls.FirstOrDefault(w => w.Key == x.Id.Value).Value;
                });
            }

            return new ResponseBase<List<PatientEmrDto>>(EStatusCode.COK, list);
        }

        /// <summary>
        /// 获取患者电子病历列表(根据入院出院时间分组)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //[Authorize(EMRPermissions.Writes.List)]
        public async Task<ResponseBase<List<PatientEmrGroupDto>>> GetPatientEmrsGroupAsync(PatientEmrRequestDto model)
        {
            var data = new List<PatientEmrGroupDto>();

            model.GetAllRecord = true;
            var query = await QueryPatientEmrsAsync(model);

            if (!query.Any()) return new ResponseBase<List<PatientEmrGroupDto>>(EStatusCode.CNULL);

            var list = ObjectMapper.Map<List<PatientEmr>, List<PatientEmrDto>>(query);

            var ids = list.Select(s => s.Id.Value).ToList();
            var urls = await GetUrlsAsync(ids.ToArray());

            if (urls.Any())
            {
                list.ForEach(x =>
                {
                    x.PdfUrl = urls.FirstOrDefault(w => w.Key == x.Id.Value).Value;
                });
            }


            var group = list.Where(w => w.AdmissionTime.HasValue).GroupBy(g => new { g.PI_ID, g.AdmissionTime });
            foreach (var item in group)
            {
                var key = item.Key;
                data.Add(new PatientEmrGroupDto
                {
                    Piid = key.PI_ID,
                    AdmissionTime = key.AdmissionTime.Value,
                    PatientEmrs = item.ToList()
                });
            }

            var ret = data.OrderByDescending(o => o.AdmissionTime).ToList();
            return new ResponseBase<List<PatientEmrGroupDto>>(EStatusCode.COK, ret);
        }

        /// <summary>
        /// 获取查询到的患者电子病历信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<List<PatientEmr>> QueryPatientEmrsAsync(PatientEmrRequestDto model)
        {
            return await (await _patientEmrRepository.GetQueryableAsync())
                .Where(w => w.PatientNo == model.PatientNo)
                .WhereIf(model.GetAllRecord.HasValue && !model.GetAllRecord.Value, w => w.PI_ID == model.Piid)
                .Where(w => w.Classify == model.Classify)
                .WhereIf(!model.DoctorCode.IsNullOrEmpty(), w => w.DoctorCode == model.DoctorCode)
                .OrderByDescending(o => o.CreationTime)
                .ToListAsync();
        }

        /// <summary>
        /// 新增/更新操作
        /// </summary>
        /// <param name="model">新增的时候模型必须完整，更新的时候，不操作xml之外的模型</param>
        /// <returns></returns>
        [UnitOfWork]
        //[AllowAnonymous]
        //[Authorize(EMRPermissions.Writes.Modify)]
        public async Task<ResponseBase<PatientEmrDto>> ModifyEmrAsync(ModifyPatientEmrDto model)
        {
            PushEmrDataEto pushData = null;
            if (model.Classify == EClassify.EMR)
            {
                var setting = _pushEmrData.CurrentValue;
                pushData = model.DataBind.GetPushEmrData(setting);
                if (pushData != null)
                {
                    pushData.SetBaseInfo(model.DeptCode, model.DoctorCode);
                    //如果采集到数据及可回写
                    await ModifyEmrBaseInfoAsync(pushData);
                }

            }

            if (model.Id.HasValue)
            {
                //1. 修改电子病历

                var entity = await (await _patientEmrRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.Id.Value);
                if (entity == null) return new ResponseBase<PatientEmrDto>(EStatusCode.CNULL, "查找不到患者的电子病历");

                var originalDoctorCode = entity.DoctorCode;
                var originalName = entity.DoctorName;

                var xmlEntity = await (await _patientEmrXmlRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.PatientEmrId == entity.Id);
                //1.历史xml留痕 
                var xmlHistoryEntity = new XmlHistory(
                    id: GuidGenerator.Create(),
                    xmlId: xmlEntity.Id,
                    xmlCategory: EXmlCategory.Archived,
                    emrXml: xmlEntity.EmrXml,
                    doctorCode: entity.DoctorCode,
                    doctorName: entity.DoctorName);
                _ = await _xmlHistoryRepository.InsertAsync(xmlHistoryEntity);

                entity.Update(
                    patientNo: model.PatientNo,
                    patientName: model.PatientName,
                    doctorCode: entity.DoctorCode,
                    doctorName: entity.DoctorName,
                    deptCode: model.DeptCode,
                    deptName: model.DeptName,
                    emrTitle: model.Title,
                    categoryLv1: model.CategoryLv1,
                    categoryLv2: model.CategoryLv2);
                _ = await _patientEmrRepository.UpdateAsync(entity);

                //2.更新最新xml
                xmlEntity.Update(model.EmrXml);
                await _patientEmrXmlRepository.UpdateAsync(xmlEntity);

                model.DataBind.PatientEmrId = xmlEntity.PatientEmrId;
                await ModifyBindAsync(model.DataBind);

                if (model.Classify == EClassify.EMR)
                {
                    //提交数据给医嘱 
                    if (pushData != null) await _capPublisher.PublishAsync(pushRouteKey, pushData);
                }
                //修改理由：保存的PDF目录保留一份，其他的人修改的病历还是放在之前的目录下，避免访问的时候异常
                var xmlEto = new XmlModelEto($"{model.Title}_{xmlEntity.Id}.pdf", xmlEntity.EmrXml, originalName/*model.DoctorName*/, model.PatientName);
                await _capPublisher.PublishAsync(emrSaveAsPDF, xmlEto);

                //推送电子病例到医院平台，改为HIS提供视图了
                //var eto = new GetMedicalHistoryRequestEto { PatientEmrId = entity.Id, RegisterSerialNo = model.DataBind.RegisterSerialNo };
                //await _capPublisher.PublishAsync(getMedicalHistory, eto);
                //保存之后把医嘱信息全部设为已使用
                var eto = new PrintedAdviceEto()
                {
                    Piid = model.PI_ID,
                    DoctorCode = model.DoctorCode,
                    DoctorName = model.DoctorName,
                };

                //await _capPublisher.PublishAsync("printed.advice.from.emr", eto);
                _ = await _recipeAppService.PrintedAsync(eto);

                if (model.DiagnoseRecordUsed != null && model.DiagnoseRecordUsed.Pdid.Any())
                {
                    //await _capPublisher.PublishAsync("modify.diagnoseRecord.emrUsed", model.DiagnoseRecordUsed.Pdid);
                    //改为同步  
                    _ = await _patientAppService.ModifyDiagnoseRecordEmrUsedAsync(model.DiagnoseRecordUsed.Pdid);
                }

                var map = ObjectMapper.Map<PatientEmr, PatientEmrDto>(entity);
                return new ResponseBase<PatientEmrDto>(EStatusCode.COK, map);
            }
            else
            {
                //2. 新增电子病历

                var id = GuidGenerator.Create();
                var entity = new PatientEmr(
                    id: id,
                    PiId: model.PI_ID,
                    patientNo: model.PatientNo,
                    patientName: model.PatientName,
                    doctorCode: model.DoctorCode,
                    doctorName: model.DoctorName,
                    deptCode: model.DeptCode,
                    deptName: model.DeptName,
                    emrTitle: model.Title,
                    categoryLv1: model.CategoryLv1,
                    categoryLv2: model.CategoryLv2,
                    classify: model.Classify,
                    originalId: model.OriginalId,
                    originId: model.OriginId,
                    admissionTime: model.AdmissionTime ?? DateTime.Now,
                    dischargeTime: model.DischargeTime,
                    code: model.Code,
                    subTitle: model.SubTitle);
                var ret = await _patientEmrRepository.InsertAsync(entity);

                var xmlEntity = new PatientEmrXml(GuidGenerator.Create(), model.EmrXml, id);
                await _patientEmrXmlRepository.InsertAsync(xmlEntity);

                model.DataBind.PatientEmrId = xmlEntity.PatientEmrId;
                await ModifyBindAsync(model.DataBind);

                if (model.Classify == EClassify.EMR)
                {
                    //提交数据给医嘱 
                    if (pushData != null) await _capPublisher.PublishAsync(pushRouteKey, pushData);
                }

                var xmlEto = new XmlModelEto($"{model.Title}_{xmlEntity.Id}.pdf", xmlEntity.EmrXml, model.DoctorName, model.PatientName);
                await _capPublisher.PublishAsync(emrSaveAsPDF, xmlEto);

                /* 改为提供视图给HIS了
                if (model.Classify == EClassify.EMR)
                {
                    //推送电子病例到医院平台
                    var eto = new GetMedicalHistoryRequestEto { PatientEmrId = entity.Id, RegisterSerialNo = model.DataBind.RegisterSerialNo };
                    await _capPublisher.PublishAsync(getMedicalHistory, eto);
                }
                */

                //var mergeEto = new MergeCourseOfObservationRequestEto
                //{
                //    PatientNo = entity.PatientNo,
                //    OriginalId = entity.OriginalId
                //};
                ////合并留观病程电子病历
                //await _capPublisher.PublishAsync("emr.merge.courseOfObservation", mergeEto);

                //保存之后把医嘱信息全部设为已使用
                var eto = new PrintedAdviceEto()
                {
                    Piid = model.PI_ID,
                    DoctorCode = model.DoctorCode,
                    DoctorName = model.DoctorName,
                };


                //await _capPublisher.PublishAsync("printed.advice.from.emr", eto); // 消费者在recipe项目里
                _ = await _recipeAppService.PrintedAsync(eto);

                if (model.DiagnoseRecordUsed != null && model.DiagnoseRecordUsed.Pdid.Any())
                {
                    //await _capPublisher.PublishAsync("modify.diagnoseRecord.emrUsed", model.DiagnoseRecordUsed.Pdid);
                    //改为同步 
                    _ = await _patientAppService.ModifyDiagnoseRecordEmrUsedAsync(model.DiagnoseRecordUsed.Pdid);
                }

                var map = ObjectMapper.Map<PatientEmr, PatientEmrDto>(entity);

                return new ResponseBase<PatientEmrDto>(EStatusCode.COK, map);
            }
        }

        /// <summary>
        /// 更新采集的患者基础信息
        /// </summary>
        /// <returns></returns> 
        private async Task ModifyEmrBaseInfoAsync(PushEmrDataEto model)
        {
            try
            {
                var entity = await _emrBaseInfoRepository.GetByAsync(model.RegisterNo);
                //如果找不到，则新增，否则创建
                if (entity == null)
                {
                    entity = new EmrBaseInfo(
                        id: GuidGenerator.Create(),
                        orgCode: "H7110",
                        deptCode: model.DeptCode,
                        doctorCode: model.DoctorCode,
                        patientId: model.PatientId,
                        visitNo: model.VisitNo,
                        registerNo: model.RegisterNo,
                        chiefComplaint: model.Narrationname,
                        historyPresentIllness: model.Presentmedicalhistory,
                        allergySign: model.AllergySign,
                        medicalHistory: model.Pastmedicalhistory,
                        bodyExam: model.Physicalexamination,
                        preliminaryDiagnosis: model.Diagnosename,
                        handlingOpinions: model.Treatopinion,
                        outpatientSurgery: model.OutpatientSurgery,
                        auxiliaryExamination: model.Aidpacs,
                        channel: "SZKJ");

                    _ = await _emrBaseInfoRepository.AddAsync(entity);

                }
                else
                {
                    entity.Update(
                        chiefComplaint: model.Narrationname,
                        historyPresentIllness: model.Presentmedicalhistory,
                        allergySign: model.AllergySign,
                        medicalHistory: model.Pastmedicalhistory,
                        bodyExam: model.Physicalexamination,
                        preliminaryDiagnosis: model.Diagnosename,
                        handlingOpinions: model.Treatopinion,
                        outpatientSurgery: model.OutpatientSurgery,
                        auxiliaryExamination: model.Aidpacs);

                    _ = await _emrBaseInfoRepository.ModifyAsync(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"更新采集的电子病历基础信息异常：{ex.Message}");
            }
        }


        /// <summary>
        /// 修改或新增绑定的电子病历，文书数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>   
        private async Task ModifyBindAsync(ModifyDataBindDto model)
        {
            try
            {
                var entity =
                    await (await _dataBindContextRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.PatientEmrId == model.PatientEmrId);
                if (entity == null)
                {
                    var newEntity = new DataBindContext(
                        id: GuidGenerator.Create(),
                        visitNo: model.VisitNo,
                        orgCode: model.OrgCode,
                        registerSerialNo: model.RegisterSerialNo,
                        pi_id: model.PI_ID,
                        patientId: model.PatientId,
                        patientName: model.PatientName,
                        writerId: model.WriterId,
                        writerName: model.WriterName,
                        classify: model.Classify,
                        patientEmrId: model.PatientEmrId.Value);
                    var retEntity = await _dataBindContextRepository.InsertAsync(newEntity, autoSave: true);
                    entity = retEntity;
                }
                else
                {
                    entity.Update(
                        visitNo: model.VisitNo,
                        orgCode: model.OrgCode,
                        registerSerialNo: model.RegisterSerialNo,
                        patientId: model.PatientId,
                        patientName: model.PatientName,
                        writerId: model.WriterId,
                        writerName: model.WriterName,
                        classify: model.Classify);

                    _ = await _dataBindContextRepository.UpdateAsync(entity, autoSave: true);
                }

                Dictionary<string, Dictionary<string, string>> data = new();
                foreach (var item in model.Data)
                {
                    if (data.ContainsKey(item.Key)) continue;
                    Dictionary<string, string> dic = new();
                    foreach (var otem in item.Value)
                    {
                        if (dic.ContainsKey(otem.Key)) continue;

                        if (otem.Value is string)
                        {
                            dic.Add(otem.Key, otem.Value as string);
                        }
                        else
                        {
                            dic.Add(otem.Key, JsonConvert.SerializeObject(otem.Value));
                        }
                    }

                    if (dic.Count > 0) data.Add(item.Key, dic);
                }

                await _dataBindMapRepository.BatchUpdateAsync(entity.Id, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"绑定电子病历数据异常:{ex.Message}");
            }
        }

        /// <summary>
        /// 获取单个书写过的xml病历文件
        /// </summary>
        /// <param name="patientEmrId">患者病历Id</param>
        /// <returns></returns>
        //[Authorize(EMRPermissions.Writes.Detail)]
        public async Task<ResponseBase<PatientEmrXmlDto>> GetEmrAsync(Guid patientEmrId)
        {
            var entity = await (await _patientEmrXmlRepository.GetQueryableAsync()).Include(i => i.PatientEmr).FirstOrDefaultAsync(w => w.PatientEmrId == patientEmrId);
            if (entity == null) return new ResponseBase<PatientEmrXmlDto>(EStatusCode.CNULL);

            var data = await GetBindAsync(patientEmrId);
            var emrxml = XmlUtil.CleanWatermark(entity.EmrXml);

            var model = new PatientEmrXmlDto
            {
                PatientEmrId = entity.PatientEmrId,
                OriginalId = entity.PatientEmr.OriginalId,
                EmrXml = emrxml.Item2, //entity.EmrXml,
                Id = entity.Id,
                Data = data
            };
            return new ResponseBase<PatientEmrXmlDto>(EStatusCode.COK, model);
        }

        /// <summary>
        /// 获取留痕历史记录列表(只查询最近50条)
        /// </summary>
        /// <param name="patientEmrId">xml的Id</param>
        /// <param name="xmlCategory">xml 电子病例模板类型(0=电子病历库的模板，1=我的电子病历模板的模板，2=已存档的患者电子病历)</param>
        /// <param name="task">希望查询的最近记录数量，默认50</param>
        /// <returns></returns>
        //[Authorize(EMRPermissions.Writes.List)]
        public async Task<ResponseBase<List<XmlHistoryDto>>> GetEmrXmlHistoriesAsync(Guid patientEmrId,
            EXmlCategory xmlCategory, int task = 50)
        {
            var query = from x in (await _patientEmrXmlRepository.GetQueryableAsync())
                        join h in (await _xmlHistoryRepository.GetQueryableAsync())
                        on x.Id equals h.XmlId
                        where x.PatientEmrId == patientEmrId && h.XmlCategory == xmlCategory
                        select new XmlHistoryDto
                        {
                            Id = h.Id,
                            XmlId = h.XmlId,
                            XmlCategory = h.XmlCategory,
                            DoctorCode = h.DoctorCode,
                            DoctorName = h.DoctorName,
                            CreationTime = h.CreationTime
                        };

            var list = await query.OrderByDescending(o => o.CreationTime).Take(task).ToListAsync();

            //var list = await _xmlHistoryRepository
            //    .Where(w => w.XmlId == xmlId && w.XmlCategory == xmlCategory)
            //    .OrderByDescending(o => o.CreationTime)
            //    .Take(task)
            //    .Select(s => new XmlHistoryDto
            //    {
            //        CreationTime = s.CreationTime,
            //        Id = s.Id,
            //        XmlCategory = s.XmlCategory,
            //        XmlId = s.XmlId,
            //        DoctorCode = s.DoctorCode,
            //        DoctorName = s.DoctorName
            //    })
            //    .ToListAsync();

            return await Task.FromResult(new ResponseBase<List<XmlHistoryDto>>(EStatusCode.COK, list));
        }

        /// <summary>
        /// 获取指定留痕记录的详细内容
        /// </summary>
        /// <param name="id">emr-xml-histories 接口返回的Id</param>
        /// <returns></returns>
        //[Authorize(EMRPermissions.Writes.Detail)]
        public async Task<ResponseBase<XmlHistoryFullDto>> GetHistoryXmlDetailAsync(Guid id)
        {
            var entity = await (await _xmlHistoryRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == id);
            var map = ObjectMapper.Map<XmlHistory, XmlHistoryFullDto>(entity);
            return new ResponseBase<XmlHistoryFullDto>(EStatusCode.COK, map);
        }

        /// <summary>
        /// 获取绑定
        /// </summary>
        /// <param name="patientEmrId"></param>
        /// <returns></returns>
        private async Task<Dictionary<string, Dictionary<string, string>>> GetBindAsync(Guid patientEmrId)
        {
            var map = new Dictionary<string, string>();
            var entity = await _dataBindContextRepository.GetDetailAsync(patientEmrId);
            if (entity == null) return new Dictionary<string, Dictionary<string, string>>();

            var ret = new Dictionary<string, Dictionary<string, string>>();
            var list = entity.DataBindMaps;
            var group = list.GroupBy(g => g.DataSource).OrderBy(o => o.Key).ToList();
            foreach (var datasource in group)
            {
                var item = new Dictionary<string, string>();
                foreach (var data in datasource)
                {
                    item.Add(data.Path, data.Value);
                }

                ret.Add(datasource.Key, item);
            }

            return ret;
        }

        /// <summary>
        /// 判断患者是否存在已开病历，用于患者主页流转
        /// </summary>
        /// <param name="pI_ID"></param>
        /// <returns></returns>
        public async Task<ResponseBase<bool>> GetIsTransferByEmrAsync(Guid pI_ID)
        {
            if (await (await _patientEmrRepository.GetQueryableAsync()).AnyAsync(w => w.PI_ID == pI_ID && w.Classify == 0))
            {
                return new ResponseBase<bool>(EStatusCode.COK, data: false);
            }
            return new ResponseBase<bool>(EStatusCode.COK, data: true);
        }


        /// <summary>
        /// 跟进患者的电子病历Id移除不需要的电子病历
        /// </summary>
        /// <param name="patientEmrId"></param>
        /// <returns></returns>
        public async Task<ResponseBase<Guid>> RemoveAsync(Guid patientEmrId)
        {
            await _patientEmrRepository.DeleteAsync(patientEmrId);
            return new ResponseBase<Guid>(EStatusCode.COK, patientEmrId);
        }

        /// <summary>
        /// 合并留观病程电子病历(合并可能需要点时间，合并完成之后即可点击打印)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns> 
        //[AllowAnonymous]
        public async Task<ResponseBase<bool>> MergeCourseOfObservationAsync(MergeCourseOfObservationRequestEto request)
        {
            try
            {
                //合并电子病历白名单过滤器
                var checkNeedMergeEmrs = await CheckNeedMergeEmrsAsync(request.OriginId);
                if (!checkNeedMergeEmrs) return new ResponseBase<bool>(EStatusCode.CFail, false, "合并的电子病历不在白名单内");

                var query = from e in (await _patientEmrRepository.GetQueryableAsync())
                            join x in (await _patientEmrXmlRepository.GetQueryableAsync())
                            on e.Id equals x.PatientEmrId
                            where e.PI_ID == request.Piid && e.OriginId == request.OriginId && !e.IsDeleted
                            select new
                            {
                                e.Id,
                                e.OriginId,
                                e.EmrTitle,
                                x.EmrXml,
                            };

                var list = await query.ToListAsync();
                //如果没有记录或者记录小于两个，则跳出，不需要合并
                if (!list.Any() || list.Count() < 2) return new ResponseBase<bool>(EStatusCode.CFail, false, "只有一个文件无需合并");

                var firstEmr = list.FirstOrDefault();
                var data = from c in (await _dataBindContextRepository.GetQueryableAsync())
                           join m in (await _dataBindMapRepository.GetQueryableAsync())
                           on c.Id equals m.DataBindContextId
                           where c.PatientEmrId == firstEmr.Id && m.DataSource.Trim() == "patientInfo".Trim()
                           select new
                           {
                               m.Path,
                               m.Value
                           };
                var bindMap = await data.ToListAsync();
                var dic = new Dictionary<string, string>();
                bindMap.GroupBy(g => g.Path).ForEach(x =>
                {
                    dic.Add(x.Key.ToLower(), x.FirstOrDefault().Value);
                });

                var eto = new MergeCourseOfObservationVitalSignsEto
                {
                    FileName = $"{firstEmr.EmrTitle}_{request.Piid}.pdf",
                    PatientInfo = new VitalSignsPatientInfoEto
                    {
                        //Age = dic["age"],
                        //ContactsPhone = dic["contactsphone"],
                        PatientName = dic["patientname"]
                        //SexName = dic["sexname"],
                        //TriageDeptName = dic["triagedeptname"],
                        //VisitNo = dic["visitno"]
                    },
                    EmrXmls = list.Select(s => s.EmrXml).ToList()
                };

                _logger.LogInformation($"合并病程患者：{eto.PatientInfo.PatientName}");
                //推送到都昌电子病历服务处理合并问题
                await _capPublisher.PublishAsync("emr.merge-and-save-as-pdf", eto);

                return new ResponseBase<bool>(EStatusCode.COK, true, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"合并留观病程电子病历异常：{ex.Message}");
                return new ResponseBase<bool>(EStatusCode.CFail, false, "操作异常");
            }
        }

        /// <summary>
        /// 检测是否需要合并电子病历
        /// </summary>
        /// <param name="originalId"></param>
        /// <returns></returns>
        private async Task<bool> CheckNeedMergeEmrsAsync(Guid originalId)
        {
            //try
            //{
            //    var mergeEmrs = _configuration.GetSection("Merge_EmrPatientEmr_OriginalId").Value;
            //    var arr = mergeEmrs.Split(';', '|').Select(s => new Guid(s)).ToList();
            //    if (!arr.Contains(originalId)) return false;
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, $"电子病历合并'MergeEmrs'配置错误:{ex.Message}");
            //    return false;
            //}

            var list = await _mergeTemplateWhiteListRepository.GetListAsync();
            var any = list.Any(w => w.TemplateId == originalId);
            return any;
        }


        #region 留痕恢复操作

        /*
         * 1. 点击病历展示该病历的所有痕迹病历（倒序排）
         * 2. 点击指定痕迹病历预览
         * 3. 选择指定预览过的痕迹病历点击恢复使用中的病历
         */

        /// <summary>
        /// 根据病历的ID获取病历痕迹的列表数据
        /// </summary>
        /// <param name="patientEmrId"></param>
        /// <returns></returns>
        public async Task<ResponseBase<List<XmlHistoryTraceDto>>> GetTraceXmlHistoryListAsync(Guid patientEmrId)
        {
            var query = from e in (await _patientEmrRepository.GetQueryableAsync()).Where(w => w.Id == patientEmrId)
                        join x in (await _patientEmrXmlRepository.GetQueryableAsync())
                        on e.Id equals x.PatientEmrId
                        join t in (await _xmlHistoryRepository.GetQueryableAsync())
                          on x.Id equals t.XmlId
                        where e.Id == patientEmrId
                        select new XmlHistoryTraceDto
                        {
                            Id = e.Id, //病历ID
                            XmlId = t.XmlId, //病历XMLID，如果破坏，需要修复的部分
                            CreationTime = t.CreationTime,
                            DoctorCode = t.DoctorCode,
                            DoctorName = t.DoctorName,
                            EmrTitle = e.EmrTitle,
                            TraceId = t.Id,
                        };
            var data = await query.ToListAsync();

            return new ResponseBase<List<XmlHistoryTraceDto>>(EStatusCode.COK, data);
        }

        /// <summary>
        ///  展示病历痕迹用的病历接口
        /// </summary>
        /// <param name="traceId">痕迹病历的ID</param>
        /// <param name="patientEmrId">原病历的id,有这个ID才能获取绑定的数据字典</param>
        /// <returns></returns>
        public async Task<ResponseBase<PatientEmrXmTraceDto>> GetTraceEmrAsync(Guid traceId, Guid patientEmrId)
        {
            var entity = await (await _xmlHistoryRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == traceId);
            if (entity == null) return new ResponseBase<PatientEmrXmTraceDto>(EStatusCode.CNULL);

            var data = await GetBindAsync(patientEmrId);
            var emrxml = XmlUtil.CleanWatermark(entity.EmrXml);

            var model = new PatientEmrXmTraceDto
            {
                PatientEmrId = patientEmrId,
                EmrXml = emrxml.Item2,
                Data = data,
            };
            return new ResponseBase<PatientEmrXmTraceDto>(EStatusCode.COK, model);
        }


        /// <summary>
        /// 替换痕迹的病历XML
        /// </summary>
        /// <param name="xmlId">需要被替换的XMLID</param>
        /// <param name="traceId">病历痕迹XMLid,最终需要用上的XMLID</param>
        /// <returns></returns>
        public async Task<ResponseBase<bool>> PutTraceReplaceAsync(Guid xmlId, Guid traceId)
        {

            try
            {
                var traceEntity = await (await _xmlHistoryRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == traceId);
                var xmlEntity = await (await _patientEmrXmlRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == xmlId);

                if (traceEntity != null && xmlEntity != null)
                {
                    xmlEntity.EmrXml = traceEntity.EmrXml;
                    _ = await _patientEmrXmlRepository.UpdateAsync(xmlEntity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"替换痕迹的病历XML异常：{ex.Message}");
                return new ResponseBase<bool>(EStatusCode.CFail, false);
            }

            return new ResponseBase<bool>(EStatusCode.COK, true);

        }

        #endregion

    }
}