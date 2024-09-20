using System.Collections.Generic;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Etos.EMRs;
using YiJian.Emrs.Dto;
using YiJian.Hospitals.Dto;

namespace YiJian.Emrs
{
    /// <summary>
    /// 电子病历数据接口
    /// </summary>
    public interface IEmrAppService
    {

        #region 检查检验

        /// <summary>
        /// 查询检查报告列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public Task<List<EmrPacsListResponse>> PacsReportListAsync(QueryPacsReportListRequest model);

        /// <summary>
        /// 查询检查报告信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public Task<EmrPacsReportResponse> PacsReportAsync(QueryPacsReportRequest model);

        /// <summary>
        /// 检验报告列表查询
        /// </summary>
        /// <returns></returns>   
        public Task<List<EmrLisListResponse>> LisReportListAsync(GetLisReportListRequest model);

        /// <summary>
        /// 检验报告详情查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>   
        public Task<List<EmrLisResponse>> LisReportAsync(GetLisReportRequest model);

        #endregion


        #region 医嘱信息

        /// <summary>
        /// 获取当前患者的医嘱信息集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public Task<List<AdviceListResponse>> AdviceListAsync(AdviceListRequest model);

        /// <summary>
        /// 已打印则将所有的未设为导入的设为导入，方便下次导入不再重复
        /// </summary>
        /// <returns></returns>
        public Task<bool> PrintedAsync(PrintedAdviceEto eto);

        #endregion
    }
}
