using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.Statisticses.Contracts;
using YiJian.Health.Report.Statisticses.Dto;
using YiJian.Health.Report.Statisticses.Entities;

namespace YiJian.Health.Report.Statisticses
{
    /// <summary>
    /// 质控-统计报表
    /// </summary>
    public partial class StatisticsReportAppService : ReportAppService, IStatisticsReportAppService
    {
        private readonly IRpMonthDoctorAndPatientRepository _rpMonthDoctorAndPatientRepository;
        private readonly IRpQuarterDoctorAndPatientRepository _rpQuarterDoctorAndPatientRepository;
        private readonly IRpYearDoctorAndPatientRepository _rpYearDoctorAndPatientRepository;

        private readonly IRpMonthNurseAndPatientRepository _rpMonthNurseAndPatientRepository;
        private readonly IRpQuarterNurseAndPatientRepository _rpQuarterNurseAndPatientRepository;
        private readonly IRpYearNurseAndPatientRepository _rpYearNurseAndPatientRepository;

        private readonly IRpMonthLevelAndPatientRepository _rpMonthLevelAndPatientRepository;
        private readonly IRpQuarterLevelAndPatientRepository _rpQuarterLevelAndPatientRepository;
        private readonly IRpYearLevelAndPatientRepository _rpYearLevelAndPatientRepository;

        private readonly IRpMonthEmergencyroomAndPatientRepository _rpMonthEmergencyroomAndPatientRepository;
        private readonly IRpQuarterEmergencyroomAndPatientRepository _rpQuarterEmergencyroomAndPatientRepository;
        private readonly IRpYearEmergencyroomAndPatientRepository _rpYearEmergencyroomAndPatientRepository;

        private readonly IRpMonthEmergencyroomAndDeathPatientRepository _rpMonthEmergencyroomAndDeathPatientRepository;
        private readonly IRpQuarterEmergencyroomAndDeathPatientRepository _rpQuarterEmergencyroomAndDeathPatientRepository;
        private readonly IRpYearEmergencyroomAndDeathPatientRepository _rpYearEmergencyroomAndDeathPatientRepository;

        private readonly ILogger<StatisticsReportAppService> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rpMonthDoctorAndPatientRepository"></param>
        /// <param name="rpQuarterDoctorAndPatientRepository"></param>
        /// <param name="rpYearDoctorAndPatientRepository"></param>
        /// <param name="rpMonthNurseAndPatientRepository"></param>
        /// <param name="rpQuarterNurseAndPatientRepository"></param>
        /// <param name="rpYearNurseAndPatientRepository"></param>
        /// <param name="rpMonthLevelAndPatientRepository"></param>
        /// <param name="rpQuarterLevelAndPatientRepository"></param>
        /// <param name="rpYearLevelAndPatientRepository"></param>
        /// <param name="rpMonthEmergencyroomAndPatientRepository"></param>
        /// <param name="rpQuarterEmergencyroomAndPatientRepository"></param>
        /// <param name="rpYearEmergencyroomAndPatientRepository"></param>
        /// <param name="rpMonthEmergencyroomAndDeathPatientRepository"></param>
        /// <param name="rpQuarterEmergencyroomAndDeathPatientRepository"></param>
        /// <param name="rpYearEmergencyroomAndDeathPatientRepository"></param>
        /// <param name="logger"></param>
        public StatisticsReportAppService(
             IRpMonthDoctorAndPatientRepository rpMonthDoctorAndPatientRepository,
             IRpQuarterDoctorAndPatientRepository rpQuarterDoctorAndPatientRepository,
             IRpYearDoctorAndPatientRepository rpYearDoctorAndPatientRepository,
             IRpMonthNurseAndPatientRepository rpMonthNurseAndPatientRepository,
             IRpQuarterNurseAndPatientRepository rpQuarterNurseAndPatientRepository,
             IRpYearNurseAndPatientRepository rpYearNurseAndPatientRepository,
             IRpMonthLevelAndPatientRepository rpMonthLevelAndPatientRepository,
             IRpQuarterLevelAndPatientRepository rpQuarterLevelAndPatientRepository,
             IRpYearLevelAndPatientRepository rpYearLevelAndPatientRepository,
             IRpMonthEmergencyroomAndPatientRepository rpMonthEmergencyroomAndPatientRepository,
             IRpQuarterEmergencyroomAndPatientRepository rpQuarterEmergencyroomAndPatientRepository,
             IRpYearEmergencyroomAndPatientRepository rpYearEmergencyroomAndPatientRepository,
             IRpMonthEmergencyroomAndDeathPatientRepository rpMonthEmergencyroomAndDeathPatientRepository,
             IRpQuarterEmergencyroomAndDeathPatientRepository rpQuarterEmergencyroomAndDeathPatientRepository,
             IRpYearEmergencyroomAndDeathPatientRepository rpYearEmergencyroomAndDeathPatientRepository,
             ILogger<StatisticsReportAppService> logger
            )
        {
            _rpMonthDoctorAndPatientRepository = rpMonthDoctorAndPatientRepository;
            _rpQuarterDoctorAndPatientRepository = rpQuarterDoctorAndPatientRepository;
            _rpYearDoctorAndPatientRepository = rpYearDoctorAndPatientRepository;
            _rpMonthNurseAndPatientRepository = rpMonthNurseAndPatientRepository;
            _rpQuarterNurseAndPatientRepository = rpQuarterNurseAndPatientRepository;
            _rpYearNurseAndPatientRepository = rpYearNurseAndPatientRepository;
            _rpMonthLevelAndPatientRepository = rpMonthLevelAndPatientRepository;
            _rpQuarterLevelAndPatientRepository = rpQuarterLevelAndPatientRepository;
            _rpYearLevelAndPatientRepository = rpYearLevelAndPatientRepository;
            _rpMonthEmergencyroomAndPatientRepository = rpMonthEmergencyroomAndPatientRepository;
            _rpQuarterEmergencyroomAndPatientRepository = rpQuarterEmergencyroomAndPatientRepository;
            _rpYearEmergencyroomAndPatientRepository = rpYearEmergencyroomAndPatientRepository;
            _rpMonthEmergencyroomAndDeathPatientRepository = rpMonthEmergencyroomAndDeathPatientRepository;
            _rpQuarterEmergencyroomAndDeathPatientRepository = rpQuarterEmergencyroomAndDeathPatientRepository;
            _rpYearEmergencyroomAndDeathPatientRepository = rpYearEmergencyroomAndDeathPatientRepository;
            _logger = logger;
        }

        /// <summary>
        /// 获取EChart的数据内容
        /// <![CDATA[
        /// 报表；
        /// 根据 ReportType 选择不同的报表；
        /// 根据 Veidoo 选择不同报表的维度，包括月度 季度 年度 三个维度
        /// ]]> 
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseBase<EChartOptionResponse>> GetChartAsync(EChartDataRequestDto param)
        {

            switch (param.ReportType)
            {
                case Enums.EReportType.DoctorAndPatient:
                    {
                        switch (param.Veidoo)
                        {
                            case Enums.EVeidoo.Month:
                                return await GetMonthDoctorAndPatientAsync(param);
                            case Enums.EVeidoo.Quarter:
                                return await GetQuarterDoctorAndPatientAsync(param);
                            case Enums.EVeidoo.Year:
                                return await GetYearDoctorAndPatientAsync(param);
                            default:
                                break;
                        }
                    }
                    break;
                case Enums.EReportType.NurseAndPatient:
                    {
                        switch (param.Veidoo)
                        {
                            case Enums.EVeidoo.Month:
                                return await GetMonthNurseAndPatientAsync(param);
                            case Enums.EVeidoo.Quarter:
                                return await GetQuarterNurseAndPatientAsync(param);
                            case Enums.EVeidoo.Year:
                                return await GetYearNurseAndPatientAsync(param);
                            default:
                                break;
                        }
                    }
                    break;
                case Enums.EReportType.LevelAndPatient:
                    {
                        switch (param.Veidoo)
                        {
                            case Enums.EVeidoo.Month:
                                return await GetMonthLevelAndPatientAsync(param);
                            case Enums.EVeidoo.Quarter:
                                return await GetQuarterLevelAndPatientAsync(param);
                            case Enums.EVeidoo.Year:
                                return await GetYearLevelAndPatientAsync(param);
                            default:
                                break;
                        }
                    }
                    break;
                case Enums.EReportType.EmergencyroomAndPatient:
                    {
                        switch (param.Veidoo)
                        {
                            case Enums.EVeidoo.Month:
                                return await GetMonthEmergencyroomAndPatientAsync(param);
                            case Enums.EVeidoo.Quarter:
                                return await GetQuarterEmergencyroomAndPatientAsync(param);
                            case Enums.EVeidoo.Year:
                                return await GetYearEmergencyroomAndPatientAsync(param);
                            default:
                                break;
                        }
                    }
                    break;
                case Enums.EReportType.EmergencyroomAndDeathPatient:
                    {
                        switch (param.Veidoo)
                        {
                            case Enums.EVeidoo.Month:
                                return await GetMonthEmergencyroomAndDeathPatientAsync(param);
                            case Enums.EVeidoo.Quarter:
                                return await GetQuarterEmergencyroomAndDeathPatientAsync(param);
                            case Enums.EVeidoo.Year:
                                return await GetYearEmergencyroomAndDeathPatientAsync(param);
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
            return new ResponseBase<EChartOptionResponse>(EStatusCode.CFail, data: null, message: "查询条件异常，请按照要求传参！");

        }


        /// <summary>
        /// 获取医患关系报表数据
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseBase<List<StatisticsDoctorAndPatientResponseDto>>> GetRpDoctorAndPatientAsync(StatisticsVeidooRequestDto param)
        {
            List<StatisticsDoctorAndPatientResponseDto> report = new();

            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            switch (param.Veidoo)
            {
                case Enums.EVeidoo.Month:
                    {
                        var list = await _rpMonthDoctorAndPatientRepository.GetListAsync(begin, end);
                        var data = ObjectMapper.Map<List<StatisticsMonthDoctorAndPatient>, List<StatisticsMonthDoctorAndPatientDto>>(list);
                        var items = data.Select(s => new StatisticsDoctorAndPatientResponseDto
                        {
                            DoctorTotal = s.DoctorTotal,
                            FormatDate = s.FormatDate,
                            FormatRatio = s.FormatRatio,
                            ReceptionTotal = s.ReceptionTotal,
                        });
                        report.AddRange(items);
                    }
                    break;
                case Enums.EVeidoo.Quarter:
                    {
                        var list = await _rpQuarterDoctorAndPatientRepository.GetListAsync(begin, end);
                        var data = ObjectMapper.Map<List<StatisticsQuarterDoctorAndPatient>, List<StatisticsQuarterDoctorAndPatientDto>>(list);
                        var items = data.Select(s => new StatisticsDoctorAndPatientResponseDto
                        {
                            DoctorTotal = s.DoctorTotal,
                            FormatDate = s.FormatDate,
                            FormatRatio = s.FormatRatio,
                            ReceptionTotal = s.ReceptionTotal,
                        });
                        report.AddRange(items);
                    }
                    break;
                case Enums.EVeidoo.Year:
                    {
                        var list = await _rpYearDoctorAndPatientRepository.GetListAsync(begin, end);
                        var data = ObjectMapper.Map<List<StatisticsYearDoctorAndPatient>, List<StatisticsYearDoctorAndPatientDto>>(list);
                        var items = data.Select(s => new StatisticsDoctorAndPatientResponseDto
                        {
                            DoctorTotal = s.DoctorTotal,
                            FormatDate = s.FormatDate,
                            FormatRatio = s.FormatRatio,
                            ReceptionTotal = s.ReceptionTotal,
                        });
                        report.AddRange(items);
                    }
                    break;
                default:
                    break;
            }
            return new ResponseBase<List<StatisticsDoctorAndPatientResponseDto>>(EStatusCode.COK, data: report);
        }

        /// <summary>
        /// 获取医师详细报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        public async Task<ResponseBase<List<UspDoctorPatientRatioResponseDto>>> GetRpDoctorDetailAsync(VeidooDetailRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.DateDetail, "begin");
            var end = GetDate(param.Veidoo, param.DateDetail, "end");
            var data = await _rpMonthDoctorAndPatientRepository.GetUspDoctorPatientRatiosAsync(begin, end);
            var map = ObjectMapper.Map<List<UspDoctorPatientRatio>, List<UspDoctorPatientRatioResponseDto>>(data);
            return new ResponseBase<List<UspDoctorPatientRatioResponseDto>>(EStatusCode.COK, data: map);
        }

        /// <summary>
        /// 获取接诊病人详细报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        public async Task<ResponseBase<List<ViewAdmissionRecordResponseDto>>> GetRpDoctorAndPatientDetailAsync(VeidooDetailRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.DateDetail, "begin");
            var end = GetDate(param.Veidoo, param.DateDetail, "end");
            var data = await _rpMonthDoctorAndPatientRepository.GetViewAdmissionRecordsAsync(begin, end);
            var map = ObjectMapper.Map<List<ViewAdmissionRecord>, List<ViewAdmissionRecordResponseDto>>(data);
            return new ResponseBase<List<ViewAdmissionRecordResponseDto>>(EStatusCode.COK, data: map);
        }

        /// <summary>
        /// 获取急诊科护患报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ResponseBase<List<StatisticsNurseAndPatientResponseDto>>> GetRpNurseAndPatientAsync(StatisticsVeidooRequestDto param)
        {
            List<StatisticsNurseAndPatientResponseDto> report = new();

            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            switch (param.Veidoo)
            {
                case Enums.EVeidoo.Month:
                    {
                        var list = await _rpMonthNurseAndPatientRepository.GetListAsync(begin, end);
                        var data = ObjectMapper.Map<List<StatisticsMonthNurseAndPatient>, List<StatisticsMonthNurseAndPatientDto>>(list);
                        var items = data.Select(s => new StatisticsNurseAndPatientResponseDto
                        {
                            NurseTotal = s.NurseTotal,
                            FormatDate = s.FormatDate,
                            FormatRatio = s.FormatRatio,
                            ReceptionTotal = s.ReceptionTotal,
                        });
                        report.AddRange(items);
                    }
                    break;
                case Enums.EVeidoo.Quarter:
                    {
                        var list = await _rpQuarterNurseAndPatientRepository.GetListAsync(begin, end);
                        var data = ObjectMapper.Map<List<StatisticsQuarterNurseAndPatient>, List<StatisticsQuarterNurseAndPatientDto>>(list);
                        var items = data.Select(s => new StatisticsNurseAndPatientResponseDto
                        {
                            NurseTotal = s.NurseTotal,
                            FormatDate = s.FormatDate,
                            FormatRatio = s.FormatRatio,
                            ReceptionTotal = s.ReceptionTotal,
                        });
                        report.AddRange(items);
                    }
                    break;
                case Enums.EVeidoo.Year:
                    {
                        var list = await _rpYearNurseAndPatientRepository.GetListAsync(begin, end);
                        var data = ObjectMapper.Map<List<StatisticsYearNurseAndPatient>, List<StatisticsYearNurseAndPatientDto>>(list);
                        var items = data.Select(s => new StatisticsNurseAndPatientResponseDto
                        {
                            NurseTotal = s.NurseTotal,
                            FormatDate = s.FormatDate,
                            FormatRatio = s.FormatRatio,
                            ReceptionTotal = s.ReceptionTotal,
                        });
                        report.AddRange(items);
                    }
                    break;
                default:
                    break;
            }
            return new ResponseBase<List<StatisticsNurseAndPatientResponseDto>>(EStatusCode.COK, data: report);

        }

        /// <summary>
        /// 护士详细报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ResponseBase<List<UspNursePatientRatioResponseDto>>> GetRpNurseDetailAsync(VeidooDetailRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.DateDetail, "begin");
            var end = GetDate(param.Veidoo, param.DateDetail, "end");
            var data = await _rpMonthNurseAndPatientRepository.GetUspNursePatientRatiosAsync(begin, end);
            var map = ObjectMapper.Map<List<UspNursePatientRatio>, List<UspNursePatientRatioResponseDto>>(data);
            return new ResponseBase<List<UspNursePatientRatioResponseDto>>(EStatusCode.COK, data: map);
        }

        /// <summary>
        /// 接诊病人详细报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ResponseBase<List<ViewAdmissionRecordResponseDto>>> GetRpNurseAndPatientDetailAsync(VeidooDetailRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.DateDetail, "begin");
            var end = GetDate(param.Veidoo, param.DateDetail, "end");
            var data = await _rpMonthNurseAndPatientRepository.GetViewAdmissionRecordsAsync(begin, end);
            var map = ObjectMapper.Map<List<ViewAdmissionRecord>, List<ViewAdmissionRecordResponseDto>>(data);
            return new ResponseBase<List<ViewAdmissionRecordResponseDto>>(EStatusCode.COK, data: map);
        }

        /// <summary>
        /// 获取急诊科各级患者比例报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ResponseBase<List<StatisticsLevelAndPatientResponseDto>>> GetRpLevelAndPatientAsync(StatisticsVeidooRequestDto param)
        {
            List<StatisticsLevelAndPatientResponseDto> report = new();

            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            switch (param.Veidoo)
            {
                case Enums.EVeidoo.Month:
                    {
                        var list = await _rpMonthLevelAndPatientRepository.GetListAsync(begin, end);
                        var data = ObjectMapper.Map<List<StatisticsMonthLevelAndPatient>, List<StatisticsMonthLevelAndPatientDto>>(list);
                        report = ObjectMapper.Map<List<StatisticsMonthLevelAndPatientDto>, List<StatisticsLevelAndPatientResponseDto>>(data);
                    }
                    break;
                case Enums.EVeidoo.Quarter:
                    {
                        var list = await _rpQuarterLevelAndPatientRepository.GetListAsync(begin, end);
                        var data = ObjectMapper.Map<List<StatisticsQuarterLevelAndPatient>, List<StatisticsQuarterLevelAndPatientDto>>(list);
                        report = ObjectMapper.Map<List<StatisticsQuarterLevelAndPatientDto>, List<StatisticsLevelAndPatientResponseDto>>(data);
                    }
                    break;
                case Enums.EVeidoo.Year:
                    {
                        var list = await _rpYearLevelAndPatientRepository.GetListAsync(begin, end);
                        var data = ObjectMapper.Map<List<StatisticsYearLevelAndPatient>, List<StatisticsYearLevelAndPatientDto>>(list);
                        report = ObjectMapper.Map<List<StatisticsYearLevelAndPatientDto>, List<StatisticsLevelAndPatientResponseDto>>(data);
                    }
                    break;
                default:
                    break;
            }
            return new ResponseBase<List<StatisticsLevelAndPatientResponseDto>>(EStatusCode.COK, data: report);

        }

        /// <summary>
        /// 抢救室滞留时间中位数月度报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        public async Task<ResponseBase<List<StatisticsEmergencyroomAndPatientResponseDto>>> GetRpEmergencyroomAndPatientAsync(StatisticsVeidooRequestDto param)
        {

            List<StatisticsEmergencyroomAndPatientResponseDto> report = new();
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            switch (param.Veidoo)
            {
                case Enums.EVeidoo.Month:
                    {
                        var list = await _rpMonthEmergencyroomAndPatientRepository.GetListAsync(begin, end);
                        var data = ObjectMapper.Map<List<StatisticsMonthEmergencyroomAndPatient>, List<StatisticsMonthEmergencyroomAndPatientDto>>(list);
                        report = ObjectMapper.Map<List<StatisticsMonthEmergencyroomAndPatientDto>, List<StatisticsEmergencyroomAndPatientResponseDto>>(data);
                    }
                    break;
                case Enums.EVeidoo.Quarter:
                    {
                        var list = await _rpQuarterEmergencyroomAndPatientRepository.GetListAsync(begin, end);
                        var data = ObjectMapper.Map<List<StatisticsQuarterEmergencyroomAndPatient>, List<StatisticsQuarterEmergencyroomAndPatientDto>>(list);
                        report = ObjectMapper.Map<List<StatisticsQuarterEmergencyroomAndPatientDto>, List<StatisticsEmergencyroomAndPatientResponseDto>>(data);
                    }
                    break;
                case Enums.EVeidoo.Year:
                    {
                        var list = await _rpYearEmergencyroomAndPatientRepository.GetListAsync(begin, end);
                        var data = ObjectMapper.Map<List<StatisticsYearEmergencyroomAndPatient>, List<StatisticsYearEmergencyroomAndPatientDto>>(list);
                        report = ObjectMapper.Map<List<StatisticsYearEmergencyroomAndPatientDto>, List<StatisticsEmergencyroomAndPatientResponseDto>>(data);
                    }
                    break;
                default:
                    break;
            }
            return new ResponseBase<List<StatisticsEmergencyroomAndPatientResponseDto>>(EStatusCode.COK, data: report);
        }

        /// <summary>
        /// 急诊抢救室患者死亡率月度报表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        public async Task<ResponseBase<List<StatisticsEmergencyroomAndDeathPatientResponseDto>>> GetRpEmergencyroomAndDeathPatientAsync(StatisticsVeidooRequestDto param)
        {
            List<StatisticsEmergencyroomAndDeathPatientResponseDto> report = new();
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            switch (param.Veidoo)
            {
                case Enums.EVeidoo.Month:
                    {
                        var list = await _rpMonthEmergencyroomAndDeathPatientRepository.GetListAsync(begin, end);
                        var data = ObjectMapper.Map<List<StatisticsMonthEmergencyroomAndDeathPatient>, List<StatisticsMonthEmergencyroomAndDeathPatientDto>>(list);
                        report = ObjectMapper.Map<List<StatisticsMonthEmergencyroomAndDeathPatientDto>, List<StatisticsEmergencyroomAndDeathPatientResponseDto>>(data);
                    }
                    break;
                case Enums.EVeidoo.Quarter:
                    {
                        var list = await _rpQuarterEmergencyroomAndDeathPatientRepository.GetListAsync(begin, end);
                        var data = ObjectMapper.Map<List<StatisticsQuarterEmergencyroomAndDeathPatient>, List<StatisticsQuarterEmergencyroomAndDeathPatientDto>>(list);
                        report = ObjectMapper.Map<List<StatisticsQuarterEmergencyroomAndDeathPatientDto>, List<StatisticsEmergencyroomAndDeathPatientResponseDto>>(data);
                    }
                    break;
                case Enums.EVeidoo.Year:
                    {
                        var list = await _rpYearEmergencyroomAndDeathPatientRepository.GetListAsync(begin, end);
                        var data = ObjectMapper.Map<List<StatisticsYearEmergencyroomAndDeathPatient>, List<StatisticsYearEmergencyroomAndDeathPatientDto>>(list);
                        report = ObjectMapper.Map<List<StatisticsYearEmergencyroomAndDeathPatientDto>, List<StatisticsEmergencyroomAndDeathPatientResponseDto>>(data);
                    }
                    break;
                default:
                    break;
            }
            return new ResponseBase<List<StatisticsEmergencyroomAndDeathPatientResponseDto>>(EStatusCode.COK, data: report);
        }

        #region 急诊科医患比报表

        /// <summary>
        /// 获取急诊科医患月度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<ResponseBase<EChartOptionResponse>> GetMonthDoctorAndPatientAsync(EChartDataRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");
            var list = await _rpMonthDoctorAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsMonthDoctorAndPatient>, List<StatisticsMonthDoctorAndPatientDto>>(list);

            var xAxis = data.Select(s => s.FormatDate).ToList();
            var doctorTotals = data.Select(x => (dynamic)x.DoctorTotal).ToList();
            var receptionTotals = data.Select(x => (dynamic)x.ReceptionTotal).ToList();
            var ratios = data.Select(x => (dynamic)(decimal.Parse(x.Ratio.ToString("F3")))).ToList();

            var series = new List<SeriesResponse>
            {
                new SeriesResponse("doctorTotal", doctorTotals),
                new SeriesResponse("receptionTotal", receptionTotals),
                new SeriesResponse("ratio", ratios)
            };
            var option = new EChartOptionResponse(xAxis, series);
            return new ResponseBase<EChartOptionResponse>(EStatusCode.COK, data: option);
        }

        /// <summary>
        /// 获取急诊科医患季度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<ResponseBase<EChartOptionResponse>> GetQuarterDoctorAndPatientAsync(EChartDataRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");
            var list = await _rpQuarterDoctorAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsQuarterDoctorAndPatient>, List<StatisticsQuarterDoctorAndPatientDto>>(list);

            var xAxis = data.Select(s => s.FormatDate).ToList();
            var doctorTotals = data.Select(x => (dynamic)x.DoctorTotal).ToList();
            var receptionTotals = data.Select(x => (dynamic)x.ReceptionTotal).ToList();
            var ratios = data.Select(x => (dynamic)(decimal.Parse(x.Ratio.ToString("F3")))).ToList();

            var series = new List<SeriesResponse>
            {
                new SeriesResponse("doctorTotal", doctorTotals),
                new SeriesResponse("receptionTotal", receptionTotals),
                new SeriesResponse("ratio", ratios)
            };
            var option = new EChartOptionResponse(xAxis, series);
            return new ResponseBase<EChartOptionResponse>(EStatusCode.COK, data: option);
        }

        /// <summary>
        /// 获取急诊科医患年度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<ResponseBase<EChartOptionResponse>> GetYearDoctorAndPatientAsync(EChartDataRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");
            var list = await _rpYearDoctorAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsYearDoctorAndPatient>, List<StatisticsYearDoctorAndPatientDto>>(list);

            var xAxis = data.Select(s => s.FormatDate).ToList();
            var doctorTotals = data.Select(x => (dynamic)x.DoctorTotal).ToList();
            var receptionTotals = data.Select(x => (dynamic)x.ReceptionTotal).ToList();
            var ratios = data.Select(x => (dynamic)(decimal.Parse(x.Ratio.ToString("F3")))).ToList();

            var series = new List<SeriesResponse>
            {
                new SeriesResponse("doctorTotal", doctorTotals),
                new SeriesResponse("receptionTotal", receptionTotals),
                new SeriesResponse("ratio", ratios)
            };
            var option = new EChartOptionResponse(xAxis, series);
            return new ResponseBase<EChartOptionResponse>(EStatusCode.COK, data: option);

        }

        #endregion

        #region 急诊科护患报表

        /// <summary>
        /// 获取急诊科护患月度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<ResponseBase<EChartOptionResponse>> GetMonthNurseAndPatientAsync(EChartDataRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");
            var list = await _rpMonthNurseAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsMonthNurseAndPatient>, List<StatisticsMonthNurseAndPatientDto>>(list);

            var xAxis = data.Select(s => s.FormatDate).ToList();
            var nurseTotals = data.Select(x => (dynamic)x.NurseTotal).ToList();
            var receptionTotals = data.Select(x => (dynamic)x.ReceptionTotal).ToList();
            var ratios = data.Select(x => (dynamic)(decimal.Parse(x.Ratio.ToString("F3")))).ToList();

            var series = new List<SeriesResponse>
            {
                new SeriesResponse("nurseTotal", nurseTotals),
                new SeriesResponse("receptionTotal", receptionTotals),
                new SeriesResponse("ratio", ratios)
            };
            var option = new EChartOptionResponse(xAxis, series);
            return new ResponseBase<EChartOptionResponse>(EStatusCode.COK, data: option);
        }

        /// <summary>
        /// 获取急诊科护患季度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<ResponseBase<EChartOptionResponse>> GetQuarterNurseAndPatientAsync(EChartDataRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");
            var list = await _rpQuarterNurseAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsQuarterNurseAndPatient>, List<StatisticsQuarterNurseAndPatientDto>>(list);

            var xAxis = data.Select(s => s.FormatDate).ToList();
            var nurseTotals = data.Select(x => (dynamic)x.NurseTotal).ToList();
            var receptionTotals = data.Select(x => (dynamic)x.ReceptionTotal).ToList();
            var ratios = data.Select(x => (dynamic)(decimal.Parse(x.Ratio.ToString("F3")))).ToList();

            var series = new List<SeriesResponse>
            {
                new SeriesResponse("nurseTotal", nurseTotals),
                new SeriesResponse("receptionTotal", receptionTotals),
                new SeriesResponse("ratio", ratios)
            };
            var option = new EChartOptionResponse(xAxis, series);
            return new ResponseBase<EChartOptionResponse>(EStatusCode.COK, data: option);
        }

        /// <summary>
        /// 获取急诊科护患年度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<ResponseBase<EChartOptionResponse>> GetYearNurseAndPatientAsync(EChartDataRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");
            var list = await _rpYearNurseAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsYearNurseAndPatient>, List<StatisticsYearNurseAndPatientDto>>(list);

            var xAxis = data.Select(s => s.FormatDate).ToList();
            var nurseTotals = data.Select(x => (dynamic)x.NurseTotal).ToList();
            var receptionTotals = data.Select(x => (dynamic)x.ReceptionTotal).ToList();
            var ratios = data.Select(x => (dynamic)(decimal.Parse(x.Ratio.ToString("F3")))).ToList();
            var series = new List<SeriesResponse>
            {
                new SeriesResponse("nurseTotal", nurseTotals),
                new SeriesResponse("receptionTotal", receptionTotals),
                new SeriesResponse("ratio", ratios)
            };
            var option = new EChartOptionResponse(xAxis, series);
            return new ResponseBase<EChartOptionResponse>(EStatusCode.COK, data: option);
        }

        #endregion

        #region 急诊科各级患者比例报表

        /// <summary>
        /// 获取急诊科各级患者比例月度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<ResponseBase<EChartOptionResponse>> GetMonthLevelAndPatientAsync(EChartDataRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");
            var list = await _rpMonthLevelAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsMonthLevelAndPatient>, List<StatisticsMonthLevelAndPatientDto>>(list);

            var xAxis = data.Select(s => s.FormatDate).ToList();
            var i = data.Select(x => (dynamic)x.LI).ToList();
            var ii = data.Select(x => (dynamic)x.LII).ToList();
            var iii = data.Select(x => (dynamic)x.LIII).ToList();
            var iva = data.Select(x => (dynamic)x.LIVa).ToList();
            var ivb = data.Select(x => (dynamic)x.LIVb).ToList();

            var series = new List<SeriesResponse>
            {
                new SeriesResponse("LI", i,"lv"),
                new SeriesResponse("LII", ii,"lv"),
                new SeriesResponse("LIII", iii, "lv"),
                new SeriesResponse("LIVa", iva,"lv"),
                new SeriesResponse("LIVb", ivb, "lv"),
            };
            var option = new EChartOptionResponse(xAxis, series);
            return new ResponseBase<EChartOptionResponse>(EStatusCode.COK, data: option);
        }

        /// <summary>
        /// 获取急诊科各级患者比例季度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<ResponseBase<EChartOptionResponse>> GetQuarterLevelAndPatientAsync(EChartDataRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");
            var list = await _rpQuarterLevelAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsQuarterLevelAndPatient>, List<StatisticsQuarterLevelAndPatientDto>>(list);

            var xAxis = data.Select(s => s.FormatDate).ToList();
            var i = data.Select(x => (dynamic)x.LI).ToList();
            var ii = data.Select(x => (dynamic)x.LII).ToList();
            var iii = data.Select(x => (dynamic)x.LIII).ToList();
            var iva = data.Select(x => (dynamic)x.LIVa).ToList();
            var ivb = data.Select(x => (dynamic)x.LIVb).ToList();

            var series = new List<SeriesResponse>
            {
                new SeriesResponse("LI", i,"lv"),
                new SeriesResponse("LII", ii,"lv"),
                new SeriesResponse("LIII", iii, "lv"),
                new SeriesResponse("LIVa", iva,"lv"),
                new SeriesResponse("LIVb", ivb, "lv"),
            };
            var option = new EChartOptionResponse(xAxis, series);
            return new ResponseBase<EChartOptionResponse>(EStatusCode.COK, data: option);
        }

        /// <summary>
        /// 获取急诊科各级患者比例年度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<ResponseBase<EChartOptionResponse>> GetYearLevelAndPatientAsync(EChartDataRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");
            var list = await _rpYearLevelAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsYearLevelAndPatient>, List<StatisticsYearLevelAndPatientDto>>(list);

            var xAxis = data.Select(s => s.FormatDate).ToList();
            var i = data.Select(x => (dynamic)x.LI).ToList();
            var ii = data.Select(x => (dynamic)x.LII).ToList();
            var iii = data.Select(x => (dynamic)x.LIII).ToList();
            var iva = data.Select(x => (dynamic)x.LIVa).ToList();
            var ivb = data.Select(x => (dynamic)x.LIVb).ToList();

            var series = new List<SeriesResponse>
            {
                new SeriesResponse("LI", i,"lv"),
                new SeriesResponse("LII", ii,"lv"),
                new SeriesResponse("LIII", iii, "lv"),
                new SeriesResponse("LIVa", iva,"lv"),
                new SeriesResponse("LIVb", ivb, "lv"),
            };
            var option = new EChartOptionResponse(xAxis, series);
            return new ResponseBase<EChartOptionResponse>(EStatusCode.COK, data: option);
        }

        #endregion

        #region 抢救室滞留时间中位数报表

        /// <summary>
        /// 获取抢救室滞留时间中位数月度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<ResponseBase<EChartOptionResponse>> GetMonthEmergencyroomAndPatientAsync(EChartDataRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");
            var list = await _rpMonthEmergencyroomAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsMonthEmergencyroomAndPatient>, List<StatisticsMonthEmergencyroomAndPatientDto>>(list);

            var xAxis = data.Select(s => s.FormatDate).ToList();
            var rescueTotals = data.Select(x => (dynamic)x.RescueTotal).ToList();
            var avgDetentionTimes = data.Select(x => (dynamic)x.AvgDetentionTime).ToList();
            var midDetentionTimes = data.Select(x => (dynamic)x.MidDetentionTime).ToList();

            var series = new List<SeriesResponse>
            {
                new SeriesResponse("rescueTotal", rescueTotals),
                new SeriesResponse("avgDetentionTime", avgDetentionTimes),
                new SeriesResponse("midDetentionTime", midDetentionTimes)
            };
            var option = new EChartOptionResponse(xAxis, series);
            return new ResponseBase<EChartOptionResponse>(EStatusCode.COK, data: option);
        }

        /// <summary>
        /// 获取抢救室滞留时间中位数季度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<ResponseBase<EChartOptionResponse>> GetQuarterEmergencyroomAndPatientAsync(EChartDataRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");
            var list = await _rpQuarterEmergencyroomAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsQuarterEmergencyroomAndPatient>, List<StatisticsQuarterEmergencyroomAndPatientDto>>(list);

            var xAxis = data.Select(s => s.FormatDate).ToList();
            var rescueTotals = data.Select(x => (dynamic)x.RescueTotal).ToList();
            var avgDetentionTimes = data.Select(x => (dynamic)x.AvgDetentionTime).ToList();
            var midDetentionTimes = data.Select(x => (dynamic)x.MidDetentionTime).ToList();

            var series = new List<SeriesResponse>
            {
                new SeriesResponse("rescueTotal", rescueTotals),
                new SeriesResponse("avgDetentionTime", avgDetentionTimes),
                new SeriesResponse("midDetentionTime", midDetentionTimes)
            };
            var option = new EChartOptionResponse(xAxis, series);
            return new ResponseBase<EChartOptionResponse>(EStatusCode.COK, data: option);
        }

        /// <summary>
        /// 获取抢救室滞留时间中位数年度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<ResponseBase<EChartOptionResponse>> GetYearEmergencyroomAndPatientAsync(EChartDataRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");
            var list = await _rpYearEmergencyroomAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsYearEmergencyroomAndPatient>, List<StatisticsYearEmergencyroomAndPatientDto>>(list);

            var xAxis = data.Select(s => s.FormatDate).ToList();
            var rescueTotals = data.Select(x => (dynamic)x.RescueTotal).ToList();
            var avgDetentionTimes = data.Select(x => (dynamic)x.AvgDetentionTime).ToList();
            var midDetentionTimes = data.Select(x => (dynamic)x.MidDetentionTime).ToList();

            var series = new List<SeriesResponse>
            {
                new SeriesResponse("rescueTotal", rescueTotals),
                new SeriesResponse("avgDetentionTime", avgDetentionTimes),
                new SeriesResponse("midDetentionTime", midDetentionTimes)
            };
            var option = new EChartOptionResponse(xAxis, series);
            return new ResponseBase<EChartOptionResponse>(EStatusCode.COK, data: option);
        }

        #endregion

        #region 急诊抢救室患者死亡率报表

        /// <summary>
        /// 获取急诊抢救室患者死亡率月度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<ResponseBase<EChartOptionResponse>> GetMonthEmergencyroomAndDeathPatientAsync(EChartDataRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");
            var list = await _rpMonthEmergencyroomAndDeathPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsMonthEmergencyroomAndDeathPatient>, List<StatisticsMonthEmergencyroomAndDeathPatientDto>>(list);

            var xAxis = data.Select(s => s.FormatDate).ToList();
            var rescueTotals = data.Select(x => (dynamic)x.RescueTotal).ToList();
            var deathTolls = data.Select(x => (dynamic)x.DeathToll).ToList();
            var deathRates = data.Select(x => (dynamic)x.DeathRate).ToList();

            var series = new List<SeriesResponse>
            {
                new SeriesResponse("rescueTotal", rescueTotals),
                new SeriesResponse("deathToll", deathTolls),
                new SeriesResponse("deathRate", deathRates)
            };
            var option = new EChartOptionResponse(xAxis, series);
            return new ResponseBase<EChartOptionResponse>(EStatusCode.COK, data: option);
        }

        /// <summary>
        /// 获取急诊抢救室患者死亡率季度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<ResponseBase<EChartOptionResponse>> GetQuarterEmergencyroomAndDeathPatientAsync(EChartDataRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");
            var list = await _rpQuarterEmergencyroomAndDeathPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsQuarterEmergencyroomAndDeathPatient>, List<StatisticsQuarterEmergencyroomAndDeathPatientDto>>(list);

            var xAxis = data.Select(s => s.FormatDate).ToList();
            var rescueTotals = data.Select(x => (dynamic)x.RescueTotal).ToList();
            var deathTolls = data.Select(x => (dynamic)x.DeathToll).ToList();
            var deathRates = data.Select(x => (dynamic)x.DeathRate).ToList();

            var series = new List<SeriesResponse>
            {
                new SeriesResponse("rescueTotal", rescueTotals),
                new SeriesResponse("deathToll", deathTolls),
                new SeriesResponse("deathRate", deathRates)
            };
            var option = new EChartOptionResponse(xAxis, series);
            return new ResponseBase<EChartOptionResponse>(EStatusCode.COK, data: option);
        }

        /// <summary>
        /// 获取急诊抢救室患者死亡率年度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<ResponseBase<EChartOptionResponse>> GetYearEmergencyroomAndDeathPatientAsync(EChartDataRequestDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");
            var list = await _rpYearEmergencyroomAndDeathPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsYearEmergencyroomAndDeathPatient>, List<StatisticsYearEmergencyroomAndDeathPatientDto>>(list);

            var xAxis = data.Select(s => s.FormatDate).ToList();
            var rescueTotals = data.Select(x => (dynamic)x.RescueTotal).ToList();
            var deathTolls = data.Select(x => (dynamic)x.DeathToll).ToList();
            var deathRates = data.Select(x => (dynamic)x.DeathRate).ToList();

            var series = new List<SeriesResponse>
            {
                new SeriesResponse("rescueTotal", rescueTotals),
                new SeriesResponse("deathToll", deathTolls),
                new SeriesResponse("deathRate", deathRates)
            };
            var option = new EChartOptionResponse(xAxis, series);
            return new ResponseBase<EChartOptionResponse>(EStatusCode.COK, data: option);
        }

        #endregion

    }
}
