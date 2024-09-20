using Consul;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Uow;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;
using YiJian.ECIS.ShareModel.Etos.EMRs;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.DataBinds.Dto;
using YiJian.EMR.Enums;
using YiJian.EMR.Writes.Dto;
using YiJian.EMR.Writes.Entities;

namespace YiJian.EMR.Writes
{
    /// <summary>
    /// 书写病历总线部分
    /// </summary>
    public partial class WriteAppService 
    {
        /// <summary>
        /// 同步患者出科时间
        /// </summary>
        /// <returns></returns> 
        [CapSubscribe("sync.patient.outDeptTime.emr")]
        public async Task SyncPatientDischargeTimeAsync(Dictionary<string, object> eto)
        {
            try
            {
                using var uow = UnitOfWorkManager.Begin();

                var dischargeTime = DateTime.Parse(eto["OutDeptTime"].ToString());  //出科时间
                var piid = Guid.Parse(eto["PI_ID"].ToString()); //患者唯一标识 
                var pemrs = await (await _patientEmrRepository.GetQueryableAsync()).Where(w => w.PI_ID == piid).ToListAsync();
                if (pemrs == null) return;

                pemrs.ForEach(x => x.DischargeTime = dischargeTime);
                await _patientEmrRepository.UpdateManyAsync(pemrs);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"同步患者出科时间异常:{ex.Message},请求参数：{JsonSerializer.Serialize(eto)}");
                throw;
            }
        }


        /// <summary>
        /// 互联网医院病历信息回写平台(异步) 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        [CapSubscribe("emr.get.medical.history")]
        public async Task ReturnMedicalHistoryAsync(GetMedicalHistoryRequestEto model)
        {
            try
            {
                using var uow = UnitOfWorkManager.Begin();
                var entity = await (await _patientEmrRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.PatientEmrId);
                var dataBindContext = await (await _dataBindContextRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.PatientEmrId == model.PatientEmrId);
                if (dataBindContext is null)
                {
                    throw new ArgumentException("数据库中未找到对应EMR，请检查PatientEmrId:"+ model.PatientEmrId);
                }

                var datamap = await (await _dataBindMapRepository.GetQueryableAsync()).Where(w => w.DataBindContextId == dataBindContext.Id).ToListAsync();


                var data = datamap.Select(s => new { DataSource = s.DataSource, Value = s.Value, Path = s.Path.Trim() }).GroupBy(g => g.DataSource);

                if (data.Any())
                {
                    var xdoc = new XDocument(
                        new XDeclaration("1.0", "utf-8", ""),
                        new XComment($"EmrTitle={entity.EmrTitle},PatientName{dataBindContext.PatientName}-DoctorName{entity.DoctorName}--OrgCode={dataBindContext.OrgCode}-RegisterSerialNo={dataBindContext.RegisterSerialNo}-VisitNo={dataBindContext.VisitNo}"),
                        new XElement("root", data.Select(ds => new XElement(ds.Key, ds.Select(v => new XElement(v.Path, new XCData(v.Value))))).ToList()));

                    var info = await _emrBaseInfoRepository.GetByAsync(model.RegisterSerialNo);

                    if (info == null || !info.CanSubmit())
                    {
                        _logger.LogInformation("本电子病历不能提交，因为还没有采集到必须的诊断信息");
                        return;
                    }

                    var eto = new ReturnMedicalHistoryEto
                    {
                        AllergySign = info.AllergySign,
                        AuxiliaryExamination = info.AuxiliaryExamination,
                        BodyExam = info.BodyExam,
                        Channel = info.Channel,
                        ChiefComplaint = info.ChiefComplaint,
                        CompletionDate = DateTime.Now,
                        CreateDate = entity.CreationTime,
                        DeptCode = info.DeptCode,
                        DoctorCode = info.DoctorCode,
                        FullContentXml = xdoc.ToString(),
                        HandlingOpinions = info.HandlingOpinions,
                        HistoryPresentIllness = info.HistoryPresentIllness,
                        MedicalHistory = info.MedicalHistory,
                        MedicalName = entity.EmrTitle,
                        OrgCode = info.OrgCode,
                        OutpatientSurgery = info.OutpatientSurgery,
                        PatientId = info.PatientId,
                        PreliminaryDiagnosis = info.PreliminaryDiagnosis,
                        RegisterNo = info.RegisterNo,
                        ReserveNum1 = "",
                        VisitNo = info.VisitNo
                    };

                    _capPublisher.Publish(returnMedicalHistory,eto); 
                }
                  
                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"互联网医院病历信息回写平台(异步) 异常:{ex.Message},请求参数：PatientEmrId={model.PatientEmrId},RegisterSerialNo={model.RegisterSerialNo}");
                throw;
            }
        }


        /// <summary>
        /// 创建会诊记录电子病历记录
        /// </summary> 
        /// <returns></returns>
        [CapSubscribe("emr.create.consultationRecord")]
        //[AllowAnonymous]
        public async Task CreateConsultationRecordEmrAsync(ConsultationRecordEmrEto model)
        {
            try
            {
                using var uow = UnitOfWorkManager.Begin();
                
                var data = model.DataBind; 
                var databind = new ModifyDataBindDto
                {
                    Classify = (EClassify)data.Classify,
                    Data = data.Data,
                    OrgCode = data.OrgCode,
                    PatientEmrId = data.PatientEmrId,
                    PatientId = data.PatientId,
                    PatientName = data.PatientName,
                    PI_ID = data.PI_ID,
                    RegisterSerialNo = data.RegisterSerialNo,
                    VisitNo = data.VisitNo,
                    WriterId = data.WriterId,
                    WriterName = data.WriterName,
                };

                var emr = new ModifyPatientEmrDto
                {
                    AdmissionTime = model.AdmissionTime, 
                    Classify = Enums.EClassify.EMR,
                    DataBind = databind,
                    DeptCode = model.DeptCode,
                    DeptName = model.DeptName,
                    DischargeTime = model.DischargeTime,
                    DoctorCode = model.DoctorCode,
                    DoctorName = model.DoctorName,
                    PI_ID = model.PI_ID,
                    PatientName = model.PatientName,
                    EmrXml = model.EmrXml,
                    PatientNo = model.PatientNo,
                    //Title = model.Title ?? "会诊记录",
                    Title = model.Title.IsNullOrEmpty() ? "会诊记录" : $"{model.DeptName}-{model.Title}",
                };

                await ModifyEmrAsync(emr); // 此处容易出现BUG，问题在于数据

                var eto = new ConsultationRecordTreatEto()
                {
                    DeptCode = model.DeptCode,
                    DeptName = model.DeptName,
                    DoctorCode = model.DoctorCode,
                    DoctorName = model.DoctorName,
                    PatientName = model.PatientName,
                    PatientNo = model.PatientNo,
                    PI_ID = model.PI_ID,
                    Diagnosis = model.Diagnosis, 
                };
                //创建会诊诊疗记录
                await _capPublisher.PublishAsync("create.consultation.treat", eto);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"创建会诊记录电子病历记录异常:{ex.Message}");
                throw;
            }

        }

        /// <summary>
        /// 采集Minio电子病历PDF信息
        /// </summary>
        /// <returns></returns>
        [CapSubscribe("emr.gather.minio.emr")]
        public async Task GatherMinioEmrAsync(string message)
        {
            _logger.LogInformation($"[-] [{message}] 采集Minio电子病历PDF信息");
            var emrList = await _minioEmrInfoRepository.GetEmrDataAsync();
            var urls = emrList.Select(s=>s.PatientEmrId).ToArray();
            if (urls.Any())
            {
                List<MinioEmrInfo> list = new List<MinioEmrInfo>();

                var data = await GetUrlsAsync(urls); 
                foreach (var item in data)
                {
                    var emr = emrList.FirstOrDefault(w=>w.PatientEmrId== item.Key);
                    if (emr != null)
                    {
                        var info = new MinioEmrInfo(GuidGenerator.Create(), patientEmrId: emr.PatientEmrId, emrTitle: emr.EmrTitle, minioUrl: item.Value, pI_ID: emr.PI_ID, patientNo: emr.PatientNo, patientName: emr.PatientName, doctorCode:emr.DoctorCode, doctorName:emr.DoctorName );
                        list.Add(info);
                    } 
                }


               await _minioEmrInfoRepository.AddAsync(list); 
            }  
        } 

    }
}
