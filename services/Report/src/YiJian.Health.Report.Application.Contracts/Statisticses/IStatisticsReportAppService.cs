using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.Statisticses.Dto;

namespace YiJian.Health.Report.Statisticses
{
    /// <summary>
    /// 质控，统计分析报表
    /// </summary>
    public interface IStatisticsReportAppService : IApplicationService
    {
        /// <summary>
        /// 获取EChart的数据内容
        /// <![CDATA[
        /// 报表；
        /// 根据 ReportType 选择不同的报表；
        /// 根据 Veidoo 选择不同报表的维度，包括月度 季度 年度 三个维度
        /// ]]> 
        /// </summary>
        /// <returns></returns>
        public Task<ResponseBase<EChartOptionResponse>> GetChartAsync(EChartDataRequestDto param);

        /// <summary>
        /// 获取医患关系报表数据
        /// </summary>
        /// <returns></returns>
        public Task<ResponseBase<List<StatisticsDoctorAndPatientResponseDto>>> GetRpDoctorAndPatientAsync(StatisticsVeidooRequestDto param);


        /// <summary>
        /// 获取医师详细报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        public Task<ResponseBase<List<UspDoctorPatientRatioResponseDto>>> GetRpDoctorDetailAsync(VeidooDetailRequestDto param);


        /// <summary>
        /// 获取接诊病人详细报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        public Task<ResponseBase<List<ViewAdmissionRecordResponseDto>>> GetRpDoctorAndPatientDetailAsync(VeidooDetailRequestDto param);


        /// <summary>
        /// 获取急诊科护患报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<ResponseBase<List<StatisticsNurseAndPatientResponseDto>>> GetRpNurseAndPatientAsync(StatisticsVeidooRequestDto param);


        /// <summary>
        /// 护士详细报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<ResponseBase<List<UspNursePatientRatioResponseDto>>> GetRpNurseDetailAsync(VeidooDetailRequestDto param);

        /// <summary>
        /// 接诊病人详细报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<ResponseBase<List<ViewAdmissionRecordResponseDto>>> GetRpNurseAndPatientDetailAsync(VeidooDetailRequestDto param);


        /// <summary>
        /// 获取急诊科各级患者比例报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<ResponseBase<List<StatisticsLevelAndPatientResponseDto>>> GetRpLevelAndPatientAsync(StatisticsVeidooRequestDto param);


        /// <summary>
        /// 抢救室滞留时间中位数月度报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        public Task<ResponseBase<List<StatisticsEmergencyroomAndPatientResponseDto>>> GetRpEmergencyroomAndPatientAsync(StatisticsVeidooRequestDto param);

        /// <summary>
        /// 急诊抢救室患者死亡率月度报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        public Task<ResponseBase<List<StatisticsEmergencyroomAndDeathPatientResponseDto>>> GetRpEmergencyroomAndDeathPatientAsync(StatisticsVeidooRequestDto param);


    }

}
