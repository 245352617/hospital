using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Models.Responses;
using YiJian.Hospitals.Dto;

namespace YiJian.Hospitals
{
    /// <summary>
    /// 医院接口客户端
    /// </summary>
    public interface IHospitalClientAppService : IApplicationService
    {
        /// <summary>
        /// 诊断、就诊记录、医嘱状态变更
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public Task<CommonResult<dynamic>> UpdateRecordStatusAsync(UpdateRecordStatusRequest model);


        /// <summary>
        /// 医嘱信息回传
        /// </summary>
        /// <returns></returns>  
        public Task<SendMedicalInfoResponse> SendMedicalInfoAsync(SendMedicalInfoRequest model);

        /// <summary>
        /// 获取药品库存信息
        /// </summary>
        /// <param name="models">请求参数</param>
        /// <returns></returns> 
        public Task<List<DrugStockStatusResposne>> DrugStockListAsync(List<DrugStockQueryRequest> models);

        /// <summary>
        /// 获取药品库存信息
        /// </summary>
        /// <param name="model">请求参数</param>
        /// <returns></returns> 
        public Task<List<MyDrugStockQueryResponse>> GetDrugStockAsync(DrugStockQueryRequest model);

        /// <summary>
        /// 获取药品库存信息(内部调用)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public Task<List<DrugStockQueryResponse>> QueryHisDrugStockAsync(DrugStockQueryRequest model);

        /// <summary>
        /// 医嘱状态查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<List<QueryMedicalInfoResponse>> QueryMedicalInfoAsync(QueryMedicalInfoRequest model);


        /// <summary>
        /// 查询检查报告列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public Task<QueryPacsReportListResponse> QueryPacsReportListAsync(QueryPacsReportListRequest model);

        /// <summary>
        /// 查询检查报告信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public Task<GetPacsReportResponse> QueryPacsReportAsync(QueryPacsReportRequest model);

        /// <summary>
        /// 检验报告列表查询
        /// </summary>
        /// <returns></returns>   
        public Task<List<GetLisReportListResponse>> QueryLisReportListAsync(GetLisReportListRequest model);

        /// <summary>
        /// 检验报告详情查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>   
        public Task<GetLisReportResponse> QueryLisReportAsync(GetLisReportRequest model);

        /// <summary>
        /// 获取检验报告
        /// </summary>
        /// <param name="applyNo"></param>
        /// <returns></returns>
        public Task<string> GetLisReportPdfAsync(string applyNo);

        /// <summary>
        /// 查询云签
        /// </summary>
        /// <param name="relBizNo"></param>
        /// <returns></returns>
        Task<string> QueryStampBaseAsync(string relBizNo);
    }
}