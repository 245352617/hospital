using BeetleX.Http.Clients;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Volo.Abp.Application.Services;
using YiJian.Apis;
using YiJian.AuditLogs.Contracts;
using YiJian.AuditLogs.Entities;
using YiJian.AuditLogs.Enums;
using YiJian.ECIS.Core.Redis;
using YiJian.ECIS.DDP;
using YiJian.ECIS.ShareModel.DDPs;
using YiJian.ECIS.ShareModel.DDPs.Requests;
using YiJian.ECIS.ShareModel.DDPs.Responses;
using YiJian.ECIS.ShareModel.Etos.EMRs;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.ShareModel.Extensions;
using YiJian.ECIS.ShareModel.Models.Responses;
using YiJian.Hospitals.Dto;

namespace YiJian.Hospitals
{
    /// <summary>
    /// 医院系统客户端请求类
    /// </summary>
    public class HospitalClientAppService : ApplicationService, IHospitalClientAppService, ICapSubscribe
    {
        private readonly ILogger<HospitalClientAppService> _logger;
        private readonly IOptionsMonitor<RemoteServices> _remoteServices;
        private readonly IAuditLogRepository _auditLogRepository;

        private readonly IOptionsMonitor<DdpHospital> _ddpHospitalOptionsMonitor;
        private readonly DdpHospital _ddpHospital;
        private readonly DdpSwitch _ddpSwitch;
        private readonly IDdpApiService _ddpApiService;
        private readonly IRedisClient _redisClient;

        /// <summary>
        /// 医院系统客户端请求类
        /// </summary> 
        public HospitalClientAppService(
            ILogger<HospitalClientAppService> logger,
            IOptionsMonitor<RemoteServices> remoteServices,
            IAuditLogRepository auditLogRepository,
            IOptionsMonitor<DdpHospital> ddpHospitalOptionsMonitor,
            DdpSwitch ddpSwitch,
            IRedisClient redisClient)
        {
            _logger = logger;
            _remoteServices = remoteServices;
            _auditLogRepository = auditLogRepository;

            _ddpHospitalOptionsMonitor = ddpHospitalOptionsMonitor;
            _ddpHospital = _ddpHospitalOptionsMonitor.CurrentValue;
            _ddpSwitch = ddpSwitch;
            _ddpApiService = _ddpSwitch.CreateService(_ddpHospital);
            _redisClient = redisClient;
        }

        /// <summary>
        /// 诊断、就诊记录、医嘱状态变更
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public async Task<CommonResult<dynamic>> UpdateRecordStatusAsync(UpdateRecordStatusRequest model)
        {
            var request = JsonConvert.SerializeObject(model);
            await WriterLogAsync("updateRecordStatus", EMethod.POST, "api/ecis/updateRecordStatus", visSerialNo: model.VisSerialNo, patientId: model.PatientId, request: request,
                "", 0);
            _logger.LogDebug($"诊断、就诊记录、医嘱状态变更： {request}");
            var service = BuildHospitalService();
            var ret = await service.UpdateRecordStatusAsync(model);
            _logger.LogDebug($"诊断、就诊记录、医嘱状态变更： {JsonConvert.SerializeObject(ret)}");
            if (ret.Code != 0) Oh.Error(ret.Msg);
            var data = await Task.FromResult(ret);

            await WriterLogAsync("updateRecordStatus", EMethod.POST, "api/ecis/updateRecordStatus", visSerialNo: model.VisSerialNo, patientId: model.PatientId, request: "",
                response: JsonConvert.SerializeObject(data), 1);
            return data;
        }

        /// <summary>
        /// 医嘱信息回传
        /// </summary>
        /// <returns></returns>  
        public async Task<SendMedicalInfoResponse> SendMedicalInfoAsync(SendMedicalInfoRequest model)
        {
            var request = JsonConvert.SerializeObject(model);

            await WriterLogAsync("sendMedicalInfo", EMethod.POST, "api/ecis/sendMedicalInfo", visSerialNo: model.VisSerialNo, patientId: model.PatientId, request: request, "", 0);
            _logger.LogDebug($"医嘱信息回传参数： {request}");
            var service = BuildHospitalService();
            var ret = await service.SendMedicalInfoAsync(model);
            if (ret.Code != 0) Oh.Error(ret.Msg.IsNullOrEmpty() ? "调用医院医嘱信息回传异常" : ret.Msg);
            _logger.LogDebug($"医嘱信息回传结果： {JsonConvert.SerializeObject(ret)}");
            var data = await Task.FromResult(ret.Data);
            await WriterLogAsync("sendMedicalInfo", EMethod.POST, "api/ecis/sendMedicalInfo", visSerialNo: model.VisSerialNo, patientId: model.PatientId, request: "",
                response: JsonConvert.SerializeObject(data), 1);
            return data;
        }


        /// <summary>
        /// 获取药品库存信息
        /// </summary>
        /// <param name="models">请求参数</param>
        /// <returns></returns> 
        [AllowAnonymous]
        public async Task<List<DrugStockStatusResposne>> DrugStockListAsync(List<DrugStockQueryRequest> models)
        {
            var ret = new List<DrugStockStatusResposne>();
            foreach (var item in models)
            {
                var data = await GetDrugStockAsync(item);
                string storageName = ((EDrugStoreCode)item.Storage).GetDescription();

                if (data != null && data.Any())
                {
                    ret.Add(new DrugStockStatusResposne
                    {
                        QueryCode = item.QueryCode,
                        Storage = item.Storage,
                        StorageName = storageName,
                        Exists = true
                    });
                }
                else
                {
                    ret.Add(new DrugStockStatusResposne
                    {
                        QueryCode = item.QueryCode,
                        Storage = item.Storage,
                        StorageName = storageName,
                        Exists = false
                    });
                }
            }

            return ret;
        }

        /// <summary>
        /// 获取药品库存信息
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns> 
        public async Task<List<MyDrugStockQueryResponse>> GetDrugStockAsync(DrugStockQueryRequest model)
        {
            var query = await QueryHisDrugStockAsync(model);
            if (query == null) return null;

            return query.Where(s => s.Quantity > 0).Select(s => new MyDrugStockQueryResponse
            {
                Code = s.DrugCode,
                Name = s.DrugName,
                Dosage = s.Dosage, //要兼容龙岗的要做数据迁移
                DosageUnit = s.DosageUnit,
                DrugDose = s.DrugDose,
                DrugUnit = s.DrugUnit,
                FactoryCode = s.FirmID,
                FactoryName = s.FirmID,
                FixPrice = s.PurchasePrice,
                MinPackageIndicator = s.MinPackageIndicator,
                MinPackageUnit = s.MinPackageUnit,
                PackageAmount = s.PackageAmount,
                PharmacyCode = s.Storage,
                PharmUnit = s.PharmUnit,
                PharSpec = s.PharSpec,
                Quantity = s.Quantity,
                RetPrice = s.RetailPrice,
                ReturnDesc = s.ReturnDesc,
                Specification = s.DrugSpec,
                BatchNo = s.DrugBatchNumber,
                ExpirDate = s.ExpiryDate,
            }).ToList();
        }

        /// <summary>
        /// 获取药品库存信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public async Task<List<DrugStockQueryResponse>> QueryHisDrugStockAsync(DrugStockQueryRequest model)
        {
            try
            {
                //调用DDP模式
                if (_ddpHospital.DdpSwitch)
                {
                    return await QueryDdpHisDrugStockAsync(model);
                }
                else
                {
                    return await QueryLgzxHisDrugStockAsync(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取药品库存信息(GetDrugStockAsync)异常：{ex.Message}");
                return null; //TODO 
            }
        }

        /// <summary>
        /// 获取药品库存信息（龙岗中心医院）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<List<DrugStockQueryResponse>> QueryLgzxHisDrugStockAsync(DrugStockQueryRequest model)
        {
            //龙岗中心医院
            var queryCode = HttpUtility.UrlEncode(model.QueryCode);
            _logger.LogDebug(
                $"获取药品库存信息:{$"api/ecis/drugStock?queryType={model.QueryType}&querycode={queryCode}&storage={model.Storage}"}");
            var service = BuildHospitalService();
            var ret = await service.GetDrugStockAsync(model.QueryType, queryCode, model.Storage);
            _logger.LogDebug($"获取药品库存信息成功：{JsonConvert.SerializeObject(ret)}");
            if (ret.Code == 0) return ret.Data.ToList();
            _logger.LogDebug($"获取药品库存信息异常：Code={ret.Code} Message:{ret.Msg}");
            return null;
        }

        /// <summary>
        /// 获取药品库存信息(DDP模式)
        /// </summary>
        /// <returns></returns>
        private async Task<List<DrugStockQueryResponse>> QueryDdpHisDrugStockAsync(DrugStockQueryRequest model)
        {
            var ddpResponse = await _ddpApiService.GetDrugStockAsync(new PKUDrugStockQueryRequest
            { QueryCode = model.QueryCode, QueryType = model.QueryType, Storage = model.Storage });
            if (ddpResponse.Code != 200)
            {
                _logger.LogError(
                    $"获取药品库存信息(DDP模式)异常,请求参数：{JsonConvert.SerializeObject(model)} , 异常描述：{ddpResponse.Msg}");
                return new List<DrugStockQueryResponse>();
            }

            return ObjectMapper.Map<List<DdpDrugStockQueryResponse>, List<DrugStockQueryResponse>>(ddpResponse.Data);
        }

        /// <summary>
        /// 获取云签
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<string> QuerySignatureAsync(string userName)
        {
            var ddpResponse = await _ddpApiService.GetSignatureAsync(new DdpSignatureRequest { UserName = userName });

            if (ddpResponse.Code != 200)
            {
                _logger.LogError(
                    $"获取云签(DDP模式)异常,请求参数：{JsonConvert.SerializeObject(userName)} , 异常描述：{ddpResponse.Msg}");
                return string.Empty;
            }
            if (ddpResponse.Data.Count == 0)
            {
                return string.Empty;
            }
            //做字符串阶段
            var signature = ddpResponse.Data.FirstOrDefault()?.Signature;
            if (!string.IsNullOrEmpty(signature))
            {
                var splitArray = signature.Split(",");
                if (splitArray.Length > 1)
                {
                    return splitArray[splitArray.Length - 1];
                }
            }
            return ddpResponse.Data.FirstOrDefault()?.Signature;

        }

        /// <summary>
        /// 医嘱状态查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        [NonAction]
        [AllowAnonymous]
        public async Task<List<QueryMedicalInfoResponse>> QueryMedicalInfoAsync(QueryMedicalInfoRequest model)
        {
            try
            {
                //调用DDP模式
                if (_ddpHospital.DdpSwitch)
                {
                    //TODO 库存校验
                    var ddpResponse = await _ddpApiService.QueryMedicalInfoAsync(new { });
                    return new List<QueryMedicalInfoResponse>();
                }

                _logger.LogDebug($"医嘱状态查询请求参数=>{JsonConvert.SerializeObject(model)}");
                var service = BuildHospitalService();
                var visSerialNo = HttpUtility.UrlEncode(model.VisSerialNo);
                var ret = await service.QueryMedicalInfoAsync(model.QueryType, visSerialNo, model.MzBillId);
                _logger.LogDebug($"医嘱状态查询返回结果=>{JsonConvert.SerializeObject(ret)}");
                return ret.Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"医嘱状态查询(QueryMedicalInfoAsync)异常,请求参数{JsonConvert.SerializeObject(model)}：{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 查询检查报告列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        [AllowAnonymous]
        public async Task<QueryPacsReportListResponse> QueryPacsReportListAsync(QueryPacsReportListRequest model)
        {
            QueryPacsReportListResponse response = new QueryPacsReportListResponse();

            //调用DDP模式
            if (_ddpHospital.DdpSwitch)
            {
                DdpBaseResponse<List<DdpQueryPacsReportListResponse>> ddpResponse = await _ddpApiService.GetPacsReportListAsync(new PKUQueryPacsReportListRequest { PatientId = model.PatientId, VisitSerialNo = model.VisitNo });
                if (ddpResponse.Code == 200)
                {
                    response.ReportInfos = ddpResponse.Data?.Select(p => new ReportInfoListResponse() { ApplyNo = p.ApplyNo, ItemCode = p.ItemCode, ItemName = p.ItemName, ExamTime = p.ExamTime, Url = p.Url }).ToList();
                    return response;
                }
                else
                {
                    _logger.LogError($"调用DDP查询检查报告列表异常,返回内容：{ddpResponse.Msg}");
                }
            }

            //折中方法，平台不提供
            string[] examTypes = new[] { "RIS", "US", "ES", "PAT", "ECG" };

            foreach (var examType in examTypes)
            {
                try
                {
                    model.ExamType = examType;
                    _logger.LogDebug("查询检查报告列表入参：" + JsonConvert.SerializeObject(model));

                    var service = BuildHospitalService();
                    var ret = await service.GetPacsReportListAsync(model);
                    if (ret.Code != 0) _logger.LogInformation(ret.Msg ?? "调用查询检查报告列表异常");

                    if (!response.ReportInfos.Any())
                    {
                        ret.Data.ReportInfos.ForEach(x => x.ExamType = examType);
                        response = ret.Data;
                    }
                    else
                    {
                        if (ret.Data != null && ret.Data.ReportInfos.Any())
                        {
                            ret.Data.ReportInfos.ForEach(x => x.ExamType = examType);
                            response.ReportInfos.AddRange(ret.Data.ReportInfos);
                        }
                    }
#if DEBUG
                    _logger.LogDebug("查询检查报告列表返回结果：" + JsonConvert.SerializeObject(ret.Data));
#endif
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"检查查询异常：类型={examType},异常内容：{ex.Message}");
                }
            }

            return response;
        }

        /// <summary>
        /// 查询检查报告信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public async Task<GetPacsReportResponse> QueryPacsReportAsync(QueryPacsReportRequest model)
        {
            _logger.LogDebug("查询检查报告信息入参：" + JsonConvert.SerializeObject(model));

            //调用DDP模式
            if (_ddpHospital.DdpSwitch)
            {
                DdpBaseResponse<DdpQueryPacsReportResponse> ddpResponse = await _ddpApiService.GetPacsReportAsync(new PKUQueryPacsReportRequest { ApplyNo = model.ApplyNo, PatientId = model.PatientId, VisitSerialNo = model.VisitSerialNo });
                if (ddpResponse.Code == 200 && ddpResponse.Data != null)
                {
                    var result = new GetPacsReportResponse();
                    result.ReportInfo = new PacsReportInfoResponse()
                    {
                        ItemCode = ddpResponse.Data.ItemCode,
                        ItemName = ddpResponse.Data.ItemName,
                        StudySee = ddpResponse.Data.StudySee,
                        StudyHint = ddpResponse.Data.StudyHint,
                        ParticipantTime = ddpResponse.Data.ParticipantTime
                    };
                    result.PatientInfo = new PacsReportPatientInfoResponse();
                    return result;
                }
                else
                {
                    _logger.LogError($"调用DDP查询检查报告信息异常,返回内容：{ddpResponse.Msg}");
                }
            }

            var service = BuildHospitalService();
            var ret = await service.GetPacsReportAsync(model);
            if (ret.Code != 0) Oh.Error(ret.Msg ?? "调用查询检查报告信息异常");
#if DEBUG
            _logger.LogDebug("查询检查报告信息返回结果：" + JsonConvert.SerializeObject(ret.Data));
#endif
            return new GetPacsReportResponse
            {
                PatientInfo = ret.Data.InspectInfoRespToPatient,
                ReportInfo = ret.Data.ReportInfos.FirstOrDefault(w => w.ReportNo == model.ReportNo)
            };
        }

        /// <summary>
        /// 检验报告列表查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>   
        public async Task<List<GetLisReportListResponse>> QueryLisReportListAsync(GetLisReportListRequest model)
        {
            _logger.LogInformation("检验报告列表查询入参：" + JsonConvert.SerializeObject(model));
            //调用DDP模式
            if (_ddpHospital.DdpSwitch)
            {
                var result = new List<GetLisReportListResponse>();
                var ddpResponse = await _ddpApiService.GetExamineListAsync(new PKUGetLisReportListRequest { PatientId = model.PatientId, PatientName = model.PatientName, VisitSerialNo = model.VisitNo });
                if (ddpResponse.Code == 0)
                {
                    if (ddpResponse.Data != null)
                    {
                        foreach (var item in ddpResponse.Data)
                        {
                            result.Add(new GetLisReportListResponse()
                            {
                                LabTime = item.LabTime,
                                ReportNo = item.ReportNo,
                                PatientId = model.PatientId,
                                VisitNo = model.VisitNo,
                                ApplyInfoList = new List<LisReportApplyInfoResponse>() {
                                new LisReportApplyInfoResponse()
                                {
                                    MasterItemList = new List<ReportMasterItemResponse>(){
                                        new ReportMasterItemResponse() {
                                            MasterItemCode = item.MasterItemCode,
                                            MasterItemName = item.MasterItemName
                                        }
                                    }
                                }
                            }
                            });
                        }
                    }
                    return result;
                }
                if (ddpResponse.Code != 0) Oh.Error(ddpResponse.Msg ?? "调用检验报告列表查询异常");
                return result;
            }

            var service = BuildHospitalService();
            var ret = await service.GetExamineListAsync(model);
            if (ret.Code != 0) Oh.Error(ret.Msg ?? "调用检验报告列表查询异常");
#if DEBUG
            _logger.LogDebug("检验报告列表查询返回结果：" + JsonConvert.SerializeObject(ret.Data));
#endif
            if (!ret.Data.Any()) return ret.Data;
            var begin = DateTime.Parse(model.StartDate);
            var end = DateTime.Parse(model.EndDate);
            var data = ret.Data.Where(w => w.LabTime.Value >= begin && w.LabTime.Value <= end)
                .OrderByDescending(o => o.LabTime).ToList();
            return data;
        }

        /// <summary>
        /// 检验报告详情查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>   
        public async Task<GetLisReportResponse> QueryLisReportAsync(GetLisReportRequest model)
        {
            _logger.LogInformation("检验报告详情查询入参：" + JsonConvert.SerializeObject(model));

            //调用DDP模式
            if (_ddpHospital.DdpSwitch)
            {
                var ddpResponse = await _ddpApiService.GetExamineInfoAsync(new PKUGetLisReportRequest { ApplyNo = model.ApplyNo, PatientId = model.PatientId, VisitSerialNo = model.VisitSerialNo, ReportNo = model.ReportNo });
                if (ddpResponse.Code == 200)
                {
                    var result = new GetLisReportResponse();
                    result.ReportItemList = ddpResponse.Data?.Select(p => new LisReportItemInfo()
                    {
                        ItemCode = p.ItemCode,
                        ItemChiName = p.ItemChiName,
                        ItemResult = p.ItemResult,
                        ItemResultUnit = p.ItemResultUnit,
                        ItemResultFlag = p.ItemResultFlag,
                        ReferenceDesc = p.ReferenceDesc,
                        ReferenceHighLimit = p.ReferenceHighLimit,
                        ReferenceLowLimit = p.ReferenceLowLimit,
                    }).ToList();
                    return result;
                }
                else
                {
                    _logger.LogError($"调用DDP查询检验报告详情异常,返回内容：{ddpResponse.Msg}");
                }
            }
            var service = BuildHospitalService();
            var ret = await service.GetExamineInfoAsync(model);
            if (ret.Code != 0) Oh.Error(ret.Msg ?? "调用检验报告详情查询异常");
#if DEBUG
            _logger.LogDebug("检验报告详情查询返回结果：" + JsonConvert.SerializeObject(ret.Data));
#endif
            return ret.Data;
        }

        /// <summary>
        /// 获取检验报告
        /// </summary>
        /// <param name="applyNo"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> GetLisReportPdfAsync(string applyNo)
        {
            DdpBaseResponse<PKULisReportResponse> ddpResponse = await _ddpApiService.GetLisReportPdfAsync(new PKULisReportRequest { ReportNo = applyNo });
            if (ddpResponse.Code == 200 && ddpResponse.Data != null)
            {
                ReportDetail report = ddpResponse.Data.Lis.FirstOrDefault();
                return report.Report;
            }
            throw new Exception($"请求DDP失败{JsonConvert.SerializeObject(ddpResponse)}");
        }

        /// <summary>
        /// 互联网医院病历信息回写平台
        /// </summary>
        /// <returns></returns>  
        [CapSubscribe("emr.return.medical.history")]
        public async Task<string> ReturnMedicalHistoryAsync(ReturnMedicalHistoryEto model)
        {
            var request = JsonConvert.SerializeObject(model);
            await WriterLogAsync("sendMedicalInfo", EMethod.POST, "api/ecis/uploadMedicalRecord", visSerialNo: model.VisitNo, patientId: model.PatientId, request: request, "",
                0);
            _logger.LogDebug($"互联网医院病历信息回写平台参数： {request}");
            var service = BuildHospitalService();
            var ret = await service.ReturnMedicalHistoryAsync(model);
            if (ret.Code != 0) Oh.Error(ret.Msg.IsNullOrEmpty() ? "互联网医院病历信息回写平台异常" : ret.Msg);
            _logger.LogDebug($"互联网医院病历信息回写平台结果： {JsonConvert.SerializeObject(ret)}");
            var data = await Task.FromResult(ret.Data);

            await WriterLogAsync("sendMedicalInfo", EMethod.POST, "api/ecis/uploadMedicalRecord", visSerialNo: model.VisitNo, patientId: model.PatientId, request: "",
                response: JsonConvert.SerializeObject(data), 1);

            return data;
        }

        /// <summary>
        /// 查询云签
        /// </summary>
        /// <param name="relBizNo"></param>
        /// <returns></returns>   
        public async Task<string> QueryStampBaseAsync(string relBizNo)
        {
            try
            {
                relBizNo = relBizNo.Length < 4 ? relBizNo.PadLeft(4, '0') : relBizNo;
                if (_ddpHospital.DdpSwitch)
                {
                    string key = $"signature:{relBizNo}";
                    string signature = await _redisClient.RedisGetStringAsync(key);
                    if (!string.IsNullOrEmpty(signature))
                    {
                        return signature;
                    }

                    signature = await QuerySignatureAsync(relBizNo);
                    if (!string.IsNullOrEmpty(signature))
                    {
                        await _redisClient.RedisSetStringAsync(key, signature);
                    }
                    return signature;
                }

                var service = BuildHospitalService("StampBase");
                var dic = new Dictionary<string, string>
                {
                    { "relBizNo", relBizNo }
                };
                var ret = await service.QueryStampBaseAsync(dic);
                if (ret.StatusCode == 0)
                {
                    return ret.EventValue.Count > 0 ? ret.EventValue["stampBase64"] : "";
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"查询云签(QueryStampBaseAsync)异常：{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 构建代理服务
        /// </summary>
        /// <returns></returns>
        private IHospitalProxyService BuildHospitalService(string server = "")
        {
            var remoteServices = _remoteServices.CurrentValue;
            HttpCluster httpCluster = new()
            {
                TimeOut = 10 * 60 * 1000
            };
            var host = remoteServices.Hospital.BaseUrl;
            if (server == "StampBase")
            {
                host = remoteServices.StampBase.BaseUrl;
            }

            httpCluster.DefaultNode.Add(host);
            var service = httpCluster.Create<IHospitalProxyService>();
            return service;
        }

        /// <summary>
        /// 写HIS请求设计日志
        /// </summary>
        /// <param name="methodName">方法名称</param>
        /// <param name="httpMethods">方法类型  GET = 0, POST = 1, PUT = 2,DELETE = 3,HEAD = 4,OPTIONS = 5, PATCH = 6</param>
        /// <param name="url">请求的HTTP URL</param>
        /// <param name="visSerialNo">流水号</param>
        /// <param name="patientId">患者id</param>
        /// <param name="request">请求参数</param>
        /// <param name="response">返回数据记录</param>
        /// <param name="operationType">操作类型，0=请求，1=返回</param>
        private async Task WriterLogAsync(string methodName, EMethod httpMethods, string url, string visSerialNo, string patientId, string request,
            string response, int operationType = 0)
        {
            try
            {
                await _auditLogRepository.WriterLogAsync(new AuditLog(methodName, httpMethods, url, visSerialNo, patientId, request, response, operationType));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}