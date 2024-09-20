using BeetleX.Http.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;
using YiJian.Documents.Dto;
using YiJian.ECIS.ShareModel.Etos.EMRs;
using YiJian.ECIS.ShareModel.Models.Responses;
using YiJian.Hospitals.Dto;

namespace YiJian.Hospitals
{
    /// <summary>
    /// 医院系统请求客户端(龙岗中心医院)
    /// </summary>
    [JsonFormater]
    public interface IHospitalProxyService
    {
        /// <summary>
        /// 病历信息回传
        /// </summary>
        /// <param name="model">医嘱变更的状态集合</param> 
        /// <returns></returns> 
        [Post(Route = "api/ecis/updateRecordStatus")]
        public Task<CommonResult<object>> UpdateRecordStatusAsync(UpdateRecordStatusRequest model);

        /// <summary>
        /// 医嘱信息回传
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Post(Route = "api/ecis/sendMedicalInfo")]
        public Task<CommonResult<SendMedicalInfoResponse>> SendMedicalInfoAsync(SendMedicalInfoRequest model);

        /// <summary>
        /// 获取药品库存信息
        /// </summary>
        /// <param name="queryType"></param>
        /// <param name="querycode"></param>
        /// <param name="storage"></param>
        /// <returns></returns>
        [Get(Route = "api/ecis/drugStock?queryType={queryType}&querycode={querycode}&storage={storage}")]
        public Task<CommonResult<List<DrugStockQueryResponse>>> GetDrugStockAsync(int queryType, string querycode, int storage = 0);

        /// <summary>
        /// 医嘱状态查询
        /// </summary>
        /// <param name="queryType">查询类型 1.查询指定信息  0.查询所有信息</param>
        /// <param name="visSerialNo">就诊流水号 4.5.2 就诊记录、诊断信息回传（his提供、需对接集成平台）visSeriaINo</param>
        /// <param name="mzBillId">单据号 单据前面增加，0处方:CF 非处方:YJ 多个以逗号隔开  CF102101,YJ102012 queryType = 0 可为空 4.5.3医嘱信息回传（his提供、需对接集成平台） prescriptionNo, projectItemNo</param>
        /// <returns></returns>
        [Get(Route = "api/ecis/queryMedicalInfo?queryType={queryType}&visSerialNo={visSerialNo}&mzBillId={mzBillId}")]
        public Task<CommonResult<List<QueryMedicalInfoResponse>>> QueryMedicalInfoAsync(int queryType, string visSerialNo, string mzBillId);

        #region 检查业务

        /// <summary>
        /// 查询检查报告列表
        /// </summary>
        /// <returns></returns>  
        [Post(Route = "api/ecis/getPacsReportList")]
        public Task<CommonResult<QueryPacsReportListResponse>> GetPacsReportListAsync(QueryPacsReportListRequest model);

        /// <summary>
        /// 查询检查报告信息
        /// </summary>
        /// <returns></returns>  
        [Post(Route = "api/ecis/getPacsReport")]
        public Task<CommonResult<QueryPacsReportResponse>> GetPacsReportAsync(QueryPacsReportRequest model);

        #endregion


        #region 检验业务

        /// <summary>
        /// 检验报告列表查询
        /// </summary>
        /// <returns></returns>  
        [Post(Route = "api/ecis/getExamineList")]
        public Task<CommonResult<List<GetLisReportListResponse>>> GetExamineListAsync(GetLisReportListRequest model);

        /// <summary>
        /// 检验报告详情查询
        /// </summary>
        /// <returns></returns>  
        [Post(Route = "api/ecis/getExamineInfo")]
        public Task<CommonResult<GetLisReportResponse>> GetExamineInfoAsync(GetLisReportRequest model);

        #endregion

        /// <summary>
        /// 查询云签
        /// </summary>
        /// <param name="relBizNo"></param>
        /// <returns></returns>
        [Post(Route = "v1.0/cloudsign/getstamp")]
        public Task<ResultUtil<Dictionary<string, string>>> QueryStampBaseAsync(Dictionary<string, string> relBizNo);

        /// <summary>
        /// 互联网医院病历信息回写平台
        /// <![CDATA[
        /// 接口可重复调用，相同病历会做更新操作
        /// /ecis/uploadMedicalRecord 病历信息回传
        /// ]]>
        /// </summary>
        /// <returns></returns> 
        [Post(Route = "api/ecis/uploadMedicalRecord")]
        public Task<CommonResult<string>> ReturnMedicalHistoryAsync(ReturnMedicalHistoryEto model);


    }
}
