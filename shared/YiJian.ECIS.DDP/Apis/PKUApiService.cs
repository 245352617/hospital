using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiJian.Apis;
using YiJian.ECIS.ShareModel.DDPs;
using YiJian.ECIS.ShareModel.DDPs.Requests;
using YiJian.ECIS.ShareModel.DDPs.Responses;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.ShareModel.Utils;
using YiJian.ECIS.ShareModel.WebApiClient.Model;

namespace YiJian.ECIS.DDP.Apis;

/// <summary>
/// PKU: 北京大学深圳医院（Peking University Shenzhen Hospital）接口服务
/// </summary>
public class PKUApiService : DdpBasicApi, IDdpApiService
{
    private ILogger<PKUApiService> _logger { get; set; }
    private readonly DdpHospital _ddpHospital;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// PKU: 北京大学深圳医院（Peking University Shenzhen Hospital）接口服务
    /// </summary>
    /// <param name="ddpHospital"></param>
    public PKUApiService(DdpHospital ddpHospital, ILogger<PKUApiService> logger, IConfiguration configuration) : base(ddpHospital)
    {
        _ddpHospital = ddpHospital;
        _logger = logger;
        _configuration = configuration;
    }

    /// <summary>
    /// 医嘱信息回传
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpSendMedicalInfoRequest
    /// </code>
    /// </param>
    /// <returns></returns> 
    public async Task<DdpBaseResponse<DdpSendMedicalInfoResponse>> SendMedicalInfoAsync(object req)
    {
        //强转
        var param = req as PKUSendMedicalInfoRequest;
        var ddpApi = Builder();
        var request = new DdpBaseRequest<PKUSendMedicalInfoRequest>("api/ecis/sendMedicalInfo", param);
        PrintLog("医嘱信息回传", nameof(SendMedicalInfoAsync), "Ddp参数", request);
        var data = await ddpApi.CallAsync<PKUSendMedicalInfoRequest, DdpSendMedicalInfoResponse>(request);
        PrintLog("医嘱信息回传", nameof(SendMedicalInfoAsync), "Ddp返回结果", data);
        return data;
    }

    public async Task<DdpBaseResponse<object>> ObsAdviceAsync(object req)
    {
        //强转
        var param = req as PKUObsAdviceRequest;
        var ddpApi = Builder();
        var request = new DdpBaseRequest<PKUObsAdviceRequest>("api/ecis/obsAdvice", param);
        PrintLog("作废医嘱", nameof(ObsAdviceAsync), "Ddp参数", request);

        var data = await ddpApi.CallAsync<PKUObsAdviceRequest, object>(request);

        PrintLog("医嘱信息回传", nameof(ObsAdviceAsync), "Ddp返回结果", data);
        return data;
    }

    #region 获取药品库存信息


    /// <summary>
    /// 获取药品库存信息
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpDrugStockQueryRequest
    /// </code>
    /// </param>
    /// <returns></returns> 
    public async Task<DdpBaseResponse<List<DdpDrugStockQueryResponse>>> GetDrugStockAsync(object req)
    {
        var param = req as PKUDrugStockQueryRequest;
        switch (param.QueryType)
        {
            case 0: return await GetDrugStockAllAsync(new PKUDrugStockQueryAllRequest(param.Storage));
            case 1: return await GetDrugStockByNameAsync(new PKUDrugStockQueryByNameRequest(param.QueryCode, param.Storage));
            case 2: return await GetDrugStockByCodeAsync(new PKUDrugStockQueryByCodeRequest(param.QueryCode, param.Storage));
            default: return await Task.FromResult(new DdpBaseResponse<List<DdpDrugStockQueryResponse>>());
        }
    }

    /// <summary>
    /// 获取云签
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public async Task<DdpBaseResponse<List<DdpSignatureResponse>>> GetSignatureAsync(DdpSignatureRequest param)
    {
        try
        {
            var ddpApi = Builder();
            var request = new DdpBaseRequest<DdpSignatureRequest>("/api/ecis/get-user-signature", param);
            PrintLog("获取云签", nameof(GetSignatureAsync), "Ddp参数", request);
            var ddpResponse = await ddpApi.CallAsync<DdpSignatureRequest, List<DdpSignatureResponse>>(request);
            PrintLog("获取云签", nameof(GetSignatureAsync), "返回结果", ddpResponse);
            return new DdpBaseResponse<List<DdpSignatureResponse>>
            {
                Code = ddpResponse.Code,
                Msg = ddpResponse.Msg,
                Data = ddpResponse.Data
            };
        }
        catch (Exception ex)
        {
            PrintLog("获取云签", nameof(GetSignatureAsync), ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 获取药品库存信息(0:查询所有药品 )
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpDrugStockQueryRequest
    /// </code>
    /// </param>
    /// <returns></returns> 
    private async Task<DdpBaseResponse<List<DdpDrugStockQueryResponse>>> GetDrugStockAllAsync(PKUDrugStockQueryAllRequest param)
    {
        try
        {
            var ddpApi = Builder();
            var request = new DdpBaseRequest<PKUDrugStockQueryAllRequest>("/api/ecis/get-drug-stock-all", param);
            PrintLog("获取药品库存信息 0:查询所有药品", nameof(GetDrugStockAsync), "Ddp参数", request);
            var ddpResponse = await ddpApi.CallAsync<PKUDrugStockQueryAllRequest, List<PKUDrugStockQueryResponse>>(request);
            PrintLog("获取药品库存信息 0:查询所有药品", nameof(GetDrugStockAsync), "返回结果", ddpResponse);
            var data = GetDrugStockList(ddpResponse);
            return new DdpBaseResponse<List<DdpDrugStockQueryResponse>>
            {
                Code = ddpResponse.Code,
                Msg = ddpResponse.Msg,
                Data = data
            };
        }
        catch (Exception ex)
        {
            PrintLog("获取药品库存信息 0:查询所有药品异常", nameof(GetDrugStockAsync), ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 获取药品库存信息(1:药品名称(支持模糊检索))
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpDrugStockQueryRequest
    /// </code>
    /// </param>
    /// <returns></returns> 
    private async Task<DdpBaseResponse<List<DdpDrugStockQueryResponse>>> GetDrugStockByNameAsync(PKUDrugStockQueryByNameRequest param)
    {
        try
        {
            var ddpApi = Builder();
            var request = new DdpBaseRequest<PKUDrugStockQueryByNameRequest>("/api/ecis/get-drug-stock-by-name", param);
            PrintLog("获取药品库存信息 1:药品名称(支持模糊检索)", nameof(GetDrugStockAsync), "Ddp参数", request);
            var ddpResponse = await ddpApi.CallAsync<PKUDrugStockQueryByNameRequest, List<PKUDrugStockQueryResponse>>(request);
            PrintLog("获取药品库存信息 1:药品名称(支持模糊检索)", nameof(GetDrugStockAsync), "返回结果", ddpResponse);
            var data = GetDrugStockList(ddpResponse);
            return new DdpBaseResponse<List<DdpDrugStockQueryResponse>>
            {
                Code = ddpResponse.Code,
                Msg = ddpResponse.Msg,
                Data = data
            };
        }
        catch (Exception ex)
        {
            PrintLog("获取药品库存信息 1:药品名称(支持模糊检索)异常", nameof(GetDrugStockAsync), ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 获取药品库存信息(2:药品编码 )
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpDrugStockQueryRequest
    /// </code>
    /// </param>
    /// <returns></returns> 
    private async Task<DdpBaseResponse<List<DdpDrugStockQueryResponse>>> GetDrugStockByCodeAsync(PKUDrugStockQueryByCodeRequest param)
    {
        try
        {
            var ddpApi = Builder();
            var request = new DdpBaseRequest<PKUDrugStockQueryByCodeRequest>("/api/ecis/get-drug-stock-by-code", param);
            PrintLog("获取药品库存信息 2:药品编码", nameof(GetDrugStockAsync), "Ddp参数", request);
            var ddpResponse = await ddpApi.CallAsync<PKUDrugStockQueryByCodeRequest, List<PKUDrugStockQueryResponse>>(request);
            PrintLog("获取药品库存信息 2:药品编码", nameof(GetDrugStockAsync), "返回结果", ddpResponse);
            var data = GetDrugStockList(ddpResponse);
            return new DdpBaseResponse<List<DdpDrugStockQueryResponse>>
            {
                Code = ddpResponse.Code,
                Msg = ddpResponse.Msg,
                Data = data
            };
        }
        catch (Exception ex)
        {
            PrintLog("获取药品库存信息 2:药品编码异常", nameof(GetDrugStockAsync), ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 重新构建返回的库存验证数据
    /// </summary>
    /// <param name="ddpResponse"></param>
    /// <returns></returns>
    private static List<DdpDrugStockQueryResponse> GetDrugStockList(DdpBaseResponse<List<PKUDrugStockQueryResponse>> ddpResponse)
    {
        return ddpResponse.Data.Select(s => new DdpDrugStockQueryResponse
        {
            Dosage = s.Dosage.DdpParseDecimal(),
            DosageUnit = s.DosageUnit,
            DrugBatchNumber = s.DrugBatchNumber,
            DrugCode = s.DrugCode,
            DrugDose = s.DrugDose.DdpParseDecimal(),
            DrugName = s.DrugName,
            DrugSpec = s.DrugSpec,
            DrugUnit = s.DrugUnit,
            ExpiryDate = null,
            FirmID = s.FirmID,
            MinPackageIndicator = s.MinPackageIndicator.DdpParseInt(),
            MinPackageUnit = s.MinPackageUnit,
            PackageAmount = s.PackageAmount.DdpParseDecimal(),
            PharmUnit = s.PharmUnit,
            PharSpec = s.PharSpec,
            PurchasePrice = s.PurchasePrice.DdpParseDecimal(),
            Quantity = s.Quantity.DdpParseDecimal(),
            RetailPrice = s.RetailPrice.DdpParseDecimal(),
            ReturnDesc = s.ReturnDesc,
            Storage = s.Storage.DdpParseInt(),
            StorageName = s.StorageName,
        }).ToList();
    }

    #endregion


    #region HIS库存药品清单

    /// <summary>
    /// 获取HIS库存药品信息清单(根据药品的厂商，编码，规格查询)
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpHisMedicineRequest
    /// </code>
    /// </param>
    /// <returns></returns>
    public async Task<DdpBaseResponse<List<DdpHisMedicineResponse>>> GetHisMedicineAsync(object req)
    {
        try
        {
            var param = req as DdpHisMedicineByPropRequest;
            var ddpApi = Builder();
            var request = new DdpBaseRequest<DdpHisMedicineByPropRequest>("/api/ecis/get-medicines", param);
            PrintLog("获取HIS库存药品信息清单(根据药品的厂商，编码，规格查询)", nameof(GetHisMedicineAsync), "Ddp请求参数", request);
            var response = await ddpApi.CallAsync<DdpHisMedicineByPropRequest, List<PKUHisMedicineResponse>>(request);
            PrintLog("获取HIS库存药品信息清单(根据药品的厂商，编码，规格查询)", nameof(GetHisMedicineAsync), "返回结果", response);
            return GetDdpHisMedicine(response);
        }
        catch (Exception ex)
        {
            PrintLog("获取HIS库存药品信息清单(根据药品的厂商，编码，规格查询)异常", nameof(GetHisMedicineAsync), ex.Message);
            throw;
        }

    }

    /// <summary>
    /// 获取HIS库存药品信息清单(根据药品的拼音或中文名称等查询)
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpHisMedicineRequest
    /// </code>
    /// </param>
    /// <returns></returns>
    public async Task<DdpBaseResponse<List<DdpHisMedicineResponse>>> GetHisMedicinesPageAsync(object req)
    {
        try
        {
            var param = req as DdpHisMedicineSearchRequest;
            var ddpApi = Builder();
            var request = new DdpBaseRequest<DdpHisMedicineSearchRequest>("/api/ecis/get-medicines-page", param);
            PrintLog("获取HIS库存药品信息清单(根据药品的拼音或中文名称等查询)", nameof(GetHisMedicinesPageAsync), "Ddp请求参数", request);
            var response = await ddpApi.CallAsync<DdpHisMedicineSearchRequest, List<PKUHisMedicineResponse>>(request);
            PrintLog("获取HIS库存药品信息清单(根据药品的拼音或中文名称等查询)", nameof(GetHisMedicinesPageAsync), response);
            return GetDdpHisMedicine(response);
        }
        catch (Exception ex)
        {
            PrintLog("获取HIS库存药品信息清单(根据药品的拼音或中文名称等查询)异常", nameof(GetHisMedicinesPageAsync), ex.Message);
            throw;
        }
    }



    /// <summary>
    /// 获取HIS库存药品信息清单(根据库存的Id集合查询)
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpHisMedicineRequest
    /// </code>
    /// </param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<DdpBaseResponse<List<DdpHisMedicineResponse>>> GetHisMedicinesByInvidAsync(object req)
    {
        try
        {
            var param = req as DdpHisMedicineByInvIdsRequest;
            var ddpApi = Builder();
            var request = new DdpBaseRequest<DdpHisMedicineByInvIdsRequest>("/api/ecis/get-medicines-by-invid", param);
            PrintLog("获取HIS库存药品信息清单(根据库存的Id集合查询)", nameof(GetHisMedicinesByInvidAsync), "Ddp请求参数", request);
            var response = await ddpApi.CallAsync<DdpHisMedicineByInvIdsRequest, List<PKUHisMedicineResponse>>(request);
            PrintLog("获取HIS库存药品信息清单(根据库存的Id集合查询)", nameof(GetHisMedicinesByInvidAsync), "返回结果", response);
            return GetDdpHisMedicine(response);
        }
        catch (Exception ex)
        {
            PrintLog("获取HIS库存药品信息清单(根据库存的Id集合查询)异常", nameof(GetHisMedicinesByInvidAsync), ex.Message);
            throw;
        }
    }

    public async Task<DdpBaseResponse<List<DdpHisMedicineResponse>>> GetHisMedicinesByCodeAsync(object req)
    {
        try
        {
            var param = req as DdpHisMedicineByInvIdsRequest;
            var ddpApi = Builder();
            var request = new DdpBaseRequest<DdpHisMedicineByInvIdsRequest>("/api/ecis/get-medicines-by-code", param);
            PrintLog("获取HIS库存药品信息清单(根据药品Code集合查询)", nameof(GetHisMedicinesByCodeAsync), "Ddp请求参数", request);
            var response = await ddpApi.CallAsync<DdpHisMedicineByInvIdsRequest, List<PKUHisMedicineResponse>>(request);
            PrintLog("获取HIS库存药品信息清单(根据药品Code集合查询)", nameof(GetHisMedicinesByCodeAsync), "返回结果", response);
            return GetDdpHisMedicine(response);
        }
        catch (Exception ex)
        {
            PrintLog("获取HIS库存药品信息清单(根据药品Code集合查询)异常", nameof(GetHisMedicinesByCodeAsync), ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 获取HIS库存药品信息清单(查询所有的，同步toxic有用)
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpHisMedicineRequest
    /// </code>
    /// </param>
    /// <returns></returns>
    public async Task<DdpBaseResponse<List<DdpHisMedicineResponse>>> GetHisMedicineAllAsync()
    {
        try
        {
            PrintLog("获取HIS库存药品信息清单(查询所有的，同步toxic有用)", nameof(GetHisMedicineAllAsync));
            var ddpApi = Builder();
            var request = new DdpBaseRequest<object>("/api/ecis/get-medicines-all", new object { });
            var response = await ddpApi.CallAsync<object, List<PKUHisMedicineResponse>>(request);
            PrintLog("获取HIS库存药品信息清单(查询所有的，同步toxic有用)", nameof(GetHisMedicineAllAsync), response.Code, response.Msg);
            return GetDdpHisMedicine(response);
        }
        catch (Exception ex)
        {
            PrintLog("获取HIS库存药品信息清单(查询所有的，同步toxic有用)异常", nameof(GetHisMedicineAllAsync), ex.Message);
            throw;
        }

    }

    /// <summary>
    /// 获取DDP返回的数据并且重新赋值
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private static DdpBaseResponse<List<DdpHisMedicineResponse>> GetDdpHisMedicine(DdpBaseResponse<List<PKUHisMedicineResponse>> response)
    {
        var data = response.Data.Select(s => new DdpHisMedicineResponse
        {
            Id = s.InvId.DdpParseInt(),
            InvId = s.InvId.DdpParseInt(),
            Alias = s.Alias,
            AliasPyCode = s.AliasPyCode,
            AliasWbCode = s.AliasWbCode,
            AntibioticLevel = s.AntibioticLevel.DdpParseNullableInt(),
            AntibioticPermission = s.AntibioticPermission.DdpParseInt(),
            BaseFlag = s.BaseFlag,
            BigPackFactor = s.BigPackFactor.DdpParseInt(),
            BigPackPrice = s.BigPackPrice.DdpParseDecimal(),
            BigPackUnit = s.BigPackUnit,
            CategoryCode = s.CategoryCode,
            CategoryName = s.CategoryName,
            ChildrenPrice = s.ChildrenPrice.DdpParseNullableDecimal(),
            Code = s.MedicineCode,
            DailyFrequency = s.DailyFrequency,
            DefaultDosage = s.DefaultDosage.DdpParseDouble(),
            DosageForm = s.DosageForm,
            DosageQty = s.DosageQty.DdpParseDecimal(),
            DosageUnit = s.DosageUnit,
            EmergencySign = s.EmergencySign.DdpParseInt(),
            ExecDeptCode = s.ExecDeptCode,
            ExecDeptName = s.ExecDeptName,
            FactoryCode = s.FactoryCode,
            FactoryName = s.FactoryName,
            FixPrice = s.FixPrice.DdpParseNullableDecimal(),
            FrequencyCode = s.FrequencyCode,
            FrequencyExecDayTimes = s.FrequencyExecDayTimes,
            FrequencyName = s.FrequencyName,
            FrequencyTimes = s.FrequencyTimes.DdpParseNullableInt(),
            FrequencyUnit = s.FrequencyUnit,
            InsuranceCode = s.InsuranceCode.DdpParseInsuranceCatalog(),
            InsurancePayRate = s.InsurancePayRate.DdpParseNullableInt(),
            IsActive = s.IsActive.DdpParseNullableBool(),
            IsAllergyTest = s.IsAllergyTest.DdpParseNullableBool(),
            IsAnaleptic = s.IsAnaleptic.DdpParseNullableBool(),
            IsCompound = s.IsCompound.DdpParseNullableBool(),
            IsFirstAid = s.IsFirstAid.DdpParseNullableBool(),
            IsDrunk = s.IsDrunk.DdpParseNullableBool(),
            IsHighRisk = s.IsHighRisk.DdpParseNullableBool(),
            IsInsulin = s.IsInsulin.DdpParseNullableBool(),
            IsLimited = s.IsLimited.DdpParseNullableBool(),
            IsPrecious = s.IsPrecious.DdpParseNullableBool(),
            IsRefrigerated = s.IsRefrigerated.DdpParseNullableBool(),
            IsSkinTest = s.IsSkinTest.DdpParseNullableBool(),
            IsTumour = s.IsTumour.DdpParseNullableBool(),
            LimitedNote = s.LimitedNote,
            MedicalInsuranceCode = s.MedicalInsuranceCode,
            YBInneCode = s.YBInneCode,
            MeducalInsuranceName = s.MeducalInsuranceName,
            //LimitType = "",
            MedicineProperty = s.MedicineProperty,
            Name = s.MedicineName,
            PharmacyCode = s.PharmacyCode,
            PharmacyName = s.PharmacyName,
            PlatformType = 0,
            PrescriptionPermission = s.PrescriptionPermission.DdpParseInt(),
            Price = s.Price.DdpParseDecimal(),
            PyCode = s.PyCode,
            Remark = s.Remark,
            Specification = s.Specification,
            RetPrice = s.RetPrice.DdpParseNullableDecimal(),
            ScientificName = s.ScientificName,
            SmallPackFactor = s.SmallPackFactor.DdpParseInt(),
            SmallPackUnit = s.SmallPackUnit,
            SmallPackPrice = s.SmallPackPrice.DdpParseDecimal(),
            Unit = s.Unit,
            ToxicLevel = s.ToxicLevel.DdpParseNullableInt(),
            Unpack = (MedicineUnPack)s.Unpack.DdpParseUnpack(),
            UsageCode = s.UsageCode,
            UsageName = s.UsageName,
            Volume = s.Volume.DdpParseNullableDouble(),
            VolumeUnit = s.VolumeUnit,
            WbCode = s.WbCode,
            Weight = s.Weight.DdpParseNullableDouble(),
            WeightUnit = s.WeightUnit,
            Qty = s.Qty,

        }).ToList();

        return new DdpBaseResponse<List<DdpHisMedicineResponse>>()
        {
            Code = response.Code,
            Msg = response.Msg,
            Data = data
        };
    }

    #endregion


    /// <summary>
    /// 医嘱状态查询
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpQueryMedicalInfoRequest
    /// </code>
    /// </param>
    /// <returns></returns> 
    public async Task<DdpBaseResponse<List<DdpQueryMedicalInfoResponse>>> QueryMedicalInfoAsync(object req)
    {
        var param = req as PKUQueryMedicalInfoRequest;
        var ddpApi = Builder();
        var request = new DdpBaseRequest<PKUQueryMedicalInfoRequest>("api/ecis/queryMedicalInfo", param);
        var data = await ddpApi.CallAsync<PKUQueryMedicalInfoRequest, List<DdpQueryMedicalInfoResponse>>(request);
        return data;
    }

    #region 检查业务

    /// <summary>
    /// 查询检查报告列表
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpQueryPacsReportListRequest
    /// </code>
    /// </param>
    /// <returns></returns>   
    public async Task<DdpBaseResponse<List<DdpQueryPacsReportListResponse>>> GetPacsReportListAsync(object req)
    {
        var param = req as PKUQueryPacsReportListRequest;
        var ddpApi = Builder();
        var request = new DdpBaseRequest<PKUQueryPacsReportListRequest>("api/ecis/queryPacsReportList", param);
        var data = await ddpApi.CallAsync<PKUQueryPacsReportListRequest, List<DdpQueryPacsReportListResponse>>(request);
        return data;
    }

    /// <summary>
    /// 查询检查报告信息
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpQueryPacsReportRequest
    /// </code>
    /// </param>
    /// <returns></returns>   
    public async Task<DdpBaseResponse<DdpQueryPacsReportResponse>> GetPacsReportAsync(object req)
    {
        var param = req as PKUQueryPacsReportRequest;
        var ddpApi = Builder();
        var request = new DdpBaseRequest<PKUQueryPacsReportRequest>("api/ecis/queryPacsReportInfo", param);
        var data = await ddpApi.CallAsync<PKUQueryPacsReportRequest, DdpQueryPacsReportResponse>(request);
        return data;
    }

    #endregion


    #region 检验业务

    /// <summary>
    /// 检验报告列表查询
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpGetLisReportListRequest
    /// </code>
    /// </param>
    /// <returns></returns>   
    public async Task<DdpBaseResponse<List<DdpGetLisReportListResponse>>> GetExamineListAsync(object req)
    {
        var param = req as PKUGetLisReportListRequest;
        //var ddpApi = Builder();
        //var request = new DdpBaseRequest<PKUGetLisReportListRequest>("api/ecis/queryExamineList", param);
        //var data = await ddpApi.CallAsync<PKUGetLisReportListRequest, List<DdpGetLisReportListResponse>>(request);
        //北大的ddp数据接口未完善暂时从这里进行查询数据
        //var api = PlatformBuilder();

        //string patient = UrlUtil.UrlEncode(param.PatientName);
        //var response = await api.GetQueryInspectFormAsync(string.Empty, patient);
        //_logger.LogInformation("检验报告列表平台返回结果：{@Response}", response);
        StringBuilder sb = new StringBuilder("socket/inspect/queryInspectForm");
        if (!string.IsNullOrEmpty(param.VisitSerialNo))
        {
            sb.AppendFormat("?patientId={0}", param.VisitSerialNo);
        }

        if (!string.IsNullOrEmpty(param.VisitSerialNo) && !string.IsNullOrEmpty(param.PatientName))
        {
            sb.AppendFormat("&patientName={0}", param.PatientName);
        }

        if (string.IsNullOrEmpty(param.VisitSerialNo) && !string.IsNullOrEmpty(param.PatientName))
        {
            sb.AppendFormat("?patientName={0}", param.PatientName);
        }

        string url = sb.ToString();
        HttpClientUtil _httpClientUtil = new HttpClientUtil(new System.Net.Http.HttpClient(), _logger);
        var response = await _httpClientUtil.GetAsync<DdpBaseResponse<List<GetLisReportListResponse>>>(_ddpHospital.PlatformHost + url);
        PrintLog("调用底层平台返回的数据为", nameof(GetExamineListAsync), "Ddp请求参数", response);
        var data = new DdpBaseResponse<List<DdpGetLisReportListResponse>>();
        if (response != null)
        {
            data.Code = data.Code;
            data.Msg = response.Msg;
            if (response.Data != null && response.Code == 0)
            {

                data.Data = response.Data.Select(c => new DdpGetLisReportListResponse
                {
                    LabTime = c.LabTime,
                    //报告Id
                    ReportNo = c.MasterItemCode,
                    MasterItemCode = c.MasterItemCode,
                    MasterItemName = c.MasterItemName,
                }).ToList();
            }
        }
        else
        {
            Oh.Error($"检验报告列表平台返回为空");
        }

        return data;
    }

    /// <summary>
    /// 检验报告详情查询
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpGetLisReportRequest
    /// </code>
    /// </param>
    /// <returns></returns>   
    public async Task<DdpBaseResponse<List<DdpGetLisReportResponse>>> GetExamineInfoAsync(object req)
    {
        var param = req as PKUGetLisReportRequest;
        //var ddpApi = Builder();
        //var request = new DdpBaseRequest<PKUGetLisReportRequest>("api/ecis/queryExamineInfo", param);
        //var data = await ddpApi.CallAsync<PKUGetLisReportRequest, List<DdpGetLisReportResponse>>(request);

        var api = PlatformBuilder();
        var response = await api.GetQueryInspectReportAsync(param.ReportNo);
        var data = new DdpBaseResponse<List<DdpGetLisReportResponse>>();
        if (response != null)
        {
            //返回内容不为空默认返回两百解决不一致的问题
            data.Code = 200;
            data.Msg = response.Msg;
            data.Data = response.Data.Select(c => new DdpGetLisReportResponse
            {
                ItemChiName = c.ItemChiName,
                ItemCode = c.ItemCode,
                ItemResult = c.ItemResult,
                ItemResultFlag = c.ItemResultFlag,
                ItemResultUnit = c.ItemResultUnit,
                ReferenceDesc = c.ReferenceDesc,
                ReferenceHighLimit = c.ReferenceHighLimit,
                ReferenceLowLimit = c.ReferenceLowLimit,
            }).ToList();
        }
        return data;
    }

    /// <summary>
    /// 获取检验报告
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    public async Task<DdpBaseResponse<PKULisReportResponse>> GetLisReportPdfAsync(object req)
    {
        PKULisReportRequest param = req as PKULisReportRequest;
        DdpApiClient ddpApi = Builder();
        DdpBaseRequest<PKULisReportRequest> request = new DdpBaseRequest<PKULisReportRequest>("/api/ecis/lis-report", param);
        DdpBaseResponse<PKULisReportResponse> data = await ddpApi.CallAsync<PKULisReportRequest, PKULisReportResponse>(request);
        return data;
    }

    #endregion

    /// <summary>
    /// 查询云签
    /// </summary>
    /// <param name="relBizNo"></param>
    /// <returns></returns> 
    public async Task<DdpBaseResponse<Dictionary<string, string>>> QueryStampBaseAsync(Dictionary<string, string> relBizNo)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    /// <summary>
    /// 互联网医院病历信息回写平台
    /// <![CDATA[
    /// 接口可重复调用，相同病历会做更新操作
    /// /ecis/uploadMedicalRecord 病历信息回传
    /// ]]>
    /// </summary>
    /// <returns></returns>  
    public async Task<DdpBaseResponse<string>> ReturnMedicalHistoryAsync(PKUReturnMedicalHistoryEto model)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    /// <summary>
    /// 查询his所有医嘱
    /// </summary>
    /// <returns></returns>
    public async Task<DdpBaseResponse<List<SubmitDoctorsAdviceEto>>> QueryHisAllRecipeListAsync(PKUQueryHisRecipeRequest req)
    {
        try
        {
            var ddpApi = Builder();
            var request = new DdpBaseRequest<PKUQueryHisRecipeRequest>("/api/ecis/advice/list", req);
            PrintLog("查询his所有医嘱", nameof(QueryHisAllRecipeListAsync), "Ddp请求参数", request);
            var response = await ddpApi.CallAsync<PKUQueryHisRecipeRequest, List<SubmitDoctorsAdviceEto>>(request);
            PrintLog("查询his所有医嘱", nameof(QueryHisAllRecipeListAsync), response);
            return response;
        }
        catch (Exception ex)
        {
            PrintLog("查询his所有医嘱异常", nameof(QueryHisAllRecipeListAsync), ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 查询his药品医保信息
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<DdpBaseResponse<List<PKUQueryHisYbInfoResponse>>> GetHisYbInfoAsync(string name)
    {
        try
        {
            DdpApiClient ddpApi = Builder();
            var request = new DdpBaseRequest<PKUQueryHisYbInfoRequest>("api/ecis/queryYbCategory", new PKUQueryHisYbInfoRequest() { Name = name });
            PrintLog("查询his药品医保信息", nameof(GetHisYbInfoAsync), "Ddp请求参数", request);
            var response = await ddpApi.CallAsync<PKUQueryHisYbInfoRequest, List<PKUQueryHisYbInfoResponse>>(request);
            PrintLog("查询his药品医保信息", nameof(GetHisYbInfoAsync), response);
            return response;
        }
        catch (Exception ex)
        {
            PrintLog("查询his药品医保信息异常", nameof(GetHisYbInfoAsync), ex.Message);
            throw;
        }
    }

    public async Task<DdpBaseResponse<List<PKUQueryRecipeStatusResponse>>> QueryHisRecipeStatusAsync(PKUQueryRecipeStatusRequest req)
    {
        try
        {
            DdpApiClient ddpApi = Builder();
            var request = new DdpBaseRequest<PKUQueryRecipeStatusRequest>("api/recipe/queryStatus", req);
            PrintLog("查询his医嘱缴费状态", nameof(QueryHisRecipeStatusAsync), "Ddp请求参数", request);
            var response = await ddpApi.CallAsync<PKUQueryRecipeStatusRequest, List<PKUQueryRecipeStatusResponse>>(request);
            PrintLog("查询his医嘱缴费状态", nameof(QueryHisRecipeStatusAsync), response);
            return response;
        }
        catch (Exception ex)
        {
            PrintLog("查询his医嘱缴费状态异常", nameof(QueryHisRecipeStatusAsync), ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 对接叫号大屏
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    public async Task<DdpBaseResponse<object>> ReferralOperationAsync(PKUDReferralOperation req)
    {
        try
        {
            DdpApiClient ddpApi = Builder();
            var request = new DdpBaseRequest<PKUDReferralOperation>("api/ecis/referralOperation", req);
            PrintLog("对接叫号大屏", nameof(ReferralOperationAsync), "Ddp请求参数", request);
            var response = await ddpApi.CallAsync<PKUDReferralOperation, object>(request);
            PrintLog("对接叫号大屏", nameof(ReferralOperationAsync), response);
            return response;
        }
        catch (Exception ex)
        {
            PrintLog("对接叫号大屏异常", nameof(QueryHisRecipeStatusAsync), ex.Message);
            //不做任何处理
            return null;
            //throw;
        }
    }

    /// <summary>
    /// 打印日志
    /// </summary>
    /// <param name="obj"></param>
    private void PrintLog(params object[] array)
    {
        StringBuilder sb = new StringBuilder();
        string elasticsearchUrl = _configuration["ElasticsearchUrl"];
        sb.Append($"Ddp日志 Start [{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] ：\n");
        foreach (var param in array)
        {
            sb.Append($"[-]: {Newtonsoft.Json.JsonConvert.SerializeObject(param)} \n");
        }
        sb.Append("End\n");

        var logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Infrastructure", Serilog.Events.LogEventLevel.Error)
                .MinimumLevel.Override("YiJian.Recipe.Application.Backgrounds.HospitalBackground", Serilog.Events.LogEventLevel.Error)
                .MinimumLevel.Override("YiJian.GrpcServer.GrpcAppService", Serilog.Events.LogEventLevel.Error)
                .MinimumLevel.Override("System.Net.Http.HttpClient.patient.ClientHandler", Serilog.Events.LogEventLevel.Error)
                .MinimumLevel.Override("System.Net.Http.HttpClient.patient.LogicalHandler", Serilog.Events.LogEventLevel.Error)
                .MinimumLevel.Override("Grpc.Net.Client.Internal.GrpcCall", Serilog.Events.LogEventLevel.Error)
                .Enrich.FromLogContext()
#if DEBUG
                .WriteTo.Console()
#endif
                .WriteTo.File("logs/ddp.log",
                    rollingInterval: RollingInterval.Day, // 每天一个日志文件
                    shared: true    // 允许其他进程共享日志文件
                );
        if (!string.IsNullOrWhiteSpace(elasticsearchUrl))
        {
            logger
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticsearchUrl))
                    {
                        AutoRegisterTemplate = true,
                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                        IndexFormat = $"ecis-ddp-prd-{DateTime.Now:yyyy-MM-dd}"
                    });
        }
        Log.Logger = logger.CreateLogger();
        Log.Information(sb.ToString());
    }
}
