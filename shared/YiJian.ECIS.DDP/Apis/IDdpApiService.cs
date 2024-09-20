using System.Collections.Generic;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.DDPs;
using YiJian.ECIS.ShareModel.DDPs.Requests;
using YiJian.ECIS.ShareModel.DDPs.Responses;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

namespace YiJian.Apis;

/// <summary>
/// DDP 接口
/// </summary>
public interface IDdpApiService
{
    /// <summary>
    /// 医嘱信息回传
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns> 
    public Task<DdpBaseResponse<DdpSendMedicalInfoResponse>> SendMedicalInfoAsync(object request);

    /// <summary>
    /// 作废医嘱
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<DdpBaseResponse<object>> ObsAdviceAsync(object request);

    /// <summary>
    /// 获取药品库存信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns> 
    public Task<DdpBaseResponse<List<DdpDrugStockQueryResponse>>> GetDrugStockAsync(object request);

    /// <summary>
    /// 获取云签
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public Task<DdpBaseResponse<List<DdpSignatureResponse>>> GetSignatureAsync(DdpSignatureRequest param);

    /// <summary>
    /// 获取HIS库存药品信息清单
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<DdpBaseResponse<List<DdpHisMedicineResponse>>> GetHisMedicineAsync(object request);


    /// <summary>
    /// 获取HIS库存药品信息清单(根据药品的拼音或中文名称等查询)
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpHisMedicineRequest
    /// </code>
    /// </param>
    /// <returns></returns>
    public Task<DdpBaseResponse<List<DdpHisMedicineResponse>>> GetHisMedicinesPageAsync(object req);

    /// <summary>
    /// 获取HIS库存药品信息清单(根据库存的Id集合查询)
    /// </summary>
    /// <param name="req">
    /// <code>
    /// IF PKU request as DdpHisMedicineRequest
    /// </code>
    /// </param>
    /// <returns></returns>
    public Task<DdpBaseResponse<List<DdpHisMedicineResponse>>> GetHisMedicinesByInvidAsync(object req);

    /// <summary>
    /// 根据code查询药品信息
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    public Task<DdpBaseResponse<List<DdpHisMedicineResponse>>> GetHisMedicinesByCodeAsync(object req);

    /// <summary>
    /// 获取HIS库存药品信息清单(查询所有的，同步toxic有用)
    /// </summary> 
    /// <code>
    /// IF PKU request as DdpHisMedicineRequest
    /// </code>
    /// </param>
    /// <returns></returns>
    public Task<DdpBaseResponse<List<DdpHisMedicineResponse>>> GetHisMedicineAllAsync();


    /// <summary>
    /// 医嘱状态查询
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns> 
    public Task<DdpBaseResponse<List<DdpQueryMedicalInfoResponse>>> QueryMedicalInfoAsync(object request);

    #region 检查业务

    /// <summary>
    /// 查询检查报告列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>   
    public Task<DdpBaseResponse<List<DdpQueryPacsReportListResponse>>> GetPacsReportListAsync(object request);

    /// <summary>
    /// 查询检查报告信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>   
    public Task<DdpBaseResponse<DdpQueryPacsReportResponse>> GetPacsReportAsync(object request);

    #endregion


    #region 检验业务

    /// <summary>
    /// 检验报告列表查询
    /// <param name="request"></param>
    /// </summary>
    /// <returns></returns>   
    public Task<DdpBaseResponse<List<DdpGetLisReportListResponse>>> GetExamineListAsync(object request);

    /// <summary>
    /// 检验报告详情查询
    /// <param name="request"></param>
    /// </summary>
    /// <returns></returns>   
    public Task<DdpBaseResponse<List<DdpGetLisReportResponse>>> GetExamineInfoAsync(object request);

    /// <summary>
    /// 获取检验报告
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    public Task<DdpBaseResponse<PKULisReportResponse>> GetLisReportPdfAsync(object req);

    #endregion

    /// <summary>
    /// 查询云签
    /// </summary>
    /// <param name="relBizNo"></param>
    /// <returns></returns> 
    public Task<DdpBaseResponse<Dictionary<string, string>>> QueryStampBaseAsync(Dictionary<string, string> relBizNo);

    /// <summary>
    /// 互联网医院病历信息回写平台
    /// <![CDATA[
    /// 接口可重复调用，相同病历会做更新操作
    /// /ecis/uploadMedicalRecord 病历信息回传
    /// ]]>
    /// </summary>
    /// <returns></returns>  
    public Task<DdpBaseResponse<string>> ReturnMedicalHistoryAsync(PKUReturnMedicalHistoryEto model);

    /// <summary>
    /// 查询his所有医嘱
    /// </summary>
    /// <returns></returns>
    public Task<DdpBaseResponse<List<SubmitDoctorsAdviceEto>>> QueryHisAllRecipeListAsync(PKUQueryHisRecipeRequest req);

    /// <summary>
    /// 查询his药品医保信息
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<DdpBaseResponse<List<PKUQueryHisYbInfoResponse>>> GetHisYbInfoAsync(string name);

    /// <summary>
    /// 查询his医嘱缴费状态
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    Task<DdpBaseResponse<List<PKUQueryRecipeStatusResponse>>> QueryHisRecipeStatusAsync(PKUQueryRecipeStatusRequest req);

    /// <summary>
    /// 对接叫号大屏
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    Task<DdpBaseResponse<object>> ReferralOperationAsync(PKUDReferralOperation req);
}
