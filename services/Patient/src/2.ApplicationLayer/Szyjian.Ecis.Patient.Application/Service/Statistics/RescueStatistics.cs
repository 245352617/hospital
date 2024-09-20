using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Dtos;

namespace Szyjian.Ecis.Patient.Application
{
    [Authorize]
    public class RescueStatistics : EcisPatientAppService
    {
        private readonly IFreeSql _freeSql;

        public RescueStatistics(IFreeSql freeSql)
        {
            _freeSql = freeSql;
        }

        public ResponseResult<PagedResultDto<RescueResponseDto>> GetRescueStatistics(RescueRequestDto rescueRequestDto)
        {
            if (rescueRequestDto == null)
            {
                return RespUtil.Error(data: new PagedResultDto<RescueResponseDto>(), msg: "请求参数为空");
            }

            PagedResultDto<RescueResponseDto> crisisDtos = QueryRescueStatistics(rescueRequestDto);
            return RespUtil.Ok(data: crisisDtos);
        }

        public FileResult ExportRescueStatistics(RescueRequestDto rescueRequestDto)
        {
            rescueRequestDto.Index = 1;
            rescueRequestDto.PageCount = int.MaxValue;
            try
            {


                PagedResultDto<RescueResponseDto> rescueResponseDtos = QueryRescueStatistics(rescueRequestDto);

                byte[] bytes = ExcelUtil.ExportExcel(rescueResponseDtos.Items);
                FileContentResult fileContentResult = new FileContentResult(bytes, "application/vnd.ms-excel");
                fileContentResult.FileDownloadName = string.Format("{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmss"));
                return fileContentResult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseResult<PagedResultDto<ObservationResponseDto>> GetObservationStatistics(ObservationRequestDto rescueRequestDto)
        {
            if (rescueRequestDto == null)
            {
                return RespUtil.Error(data: new PagedResultDto<ObservationResponseDto>(), msg: "请求参数为空");
            }

            PagedResultDto<ObservationResponseDto> crisisDtos = QueryObservationStatistics(rescueRequestDto);
            return RespUtil.Ok(data: crisisDtos);
        }

        public FileResult ExportObservationStatistics(ObservationRequestDto observationRequestDto)
        {
            observationRequestDto.Index = 1;
            observationRequestDto.PageCount = int.MaxValue;
            try
            {


                PagedResultDto<ObservationResponseDto> observationResponseDtos = QueryObservationStatistics(observationRequestDto);

                byte[] bytes = ExcelUtil.ExportExcel(observationResponseDtos.Items);
                FileContentResult fileContentResult = new FileContentResult(bytes, "application/vnd.ms-excel");
                fileContentResult.FileDownloadName = string.Format("{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmss"));
                return fileContentResult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private PagedResultDto<RescueResponseDto> QueryRescueStatistics(RescueRequestDto rescueRequestDto)
        {
            string startTime = null;
            if (rescueRequestDto.StartTime.HasValue)
            {
                startTime = rescueRequestDto.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }

            string endTime = null;
            if (rescueRequestDto.EndTime.HasValue)
            {
                endTime = rescueRequestDto.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }

            string deptCode = rescueRequestDto.DeptCode;
            if (string.IsNullOrEmpty(deptCode))
            {
                deptCode = null;
            }

            string visitState = null;
            if (rescueRequestDto.VisitState > 0)
            {
                visitState = rescueRequestDto.VisitState.ToString();
            }

            string inpatientName = rescueRequestDto.InpatientName;
            if (string.IsNullOrEmpty(inpatientName))
            {
                inpatientName = null;
            }

            string search = rescueRequestDto.Search;
            if (string.IsNullOrEmpty(search))
            {
                search = null;
            }

            string isGreen = null;
            if (rescueRequestDto.IsGreen.HasValue)
            {
                isGreen = rescueRequestDto.IsGreen.Value.ToString();
            }

            DbParameter outParameter = null;
            DataTable dataTable = _freeSql.Ado.CommandFluent("sp_rescue_statistics")
                 .CommandType(CommandType.StoredProcedure)
                 .CommandTimeout(30 * 60 * 60)
                 .WithParameter("pageIndex", rescueRequestDto.Index, x =>
                 {
                     x.DbType = DbType.Int32;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("pageCount", rescueRequestDto.PageCount, x =>
                 {
                     x.DbType = DbType.Int32;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("rowTotal", null, x =>
                 {
                     outParameter = x;
                     x.DbType = DbType.Int32;
                     x.Direction = ParameterDirection.Output;
                 })
                 .WithParameter("startTime", startTime, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("endTime", endTime, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("deptCode", deptCode, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("visitState", visitState, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("inpatientName", inpatientName, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("search", search, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("isGreen", isGreen, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .ExecuteDataTable();

            int rowTotal = (int)outParameter.Value;
            List<RescueResponseDto> rescueResponseDtos = DataTableToList<RescueResponseDto>(dataTable);

            rescueResponseDtos = Conversion(rescueResponseDtos);

            PagedResultDto<RescueResponseDto> res = new PagedResultDto<RescueResponseDto>
            {
                TotalCount = rowTotal,
                Items = rescueResponseDtos
            };

            return res;
        }

        private PagedResultDto<ObservationResponseDto> QueryObservationStatistics(ObservationRequestDto observationRequestDto)
        {
            string startTime = null;
            if (observationRequestDto.StartTime.HasValue)
            {
                startTime = observationRequestDto.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }

            string endTime = null;
            if (observationRequestDto.EndTime.HasValue)
            {
                endTime = observationRequestDto.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }

            string deptCode = observationRequestDto.DeptCode;
            if (string.IsNullOrEmpty(deptCode))
            {
                deptCode = null;
            }

            string visitState = null;
            if (observationRequestDto.VisitState > 0)
            {
                visitState = observationRequestDto.VisitState.ToString();
            }

            string inpatientName = observationRequestDto.InpatientName;
            if (string.IsNullOrEmpty(inpatientName))
            {
                inpatientName = null;
            }

            string search = observationRequestDto.Search;
            if (string.IsNullOrEmpty(search))
            {
                search = null;
            }

            string isGreen = null;
            if (observationRequestDto.IsGreen.HasValue)
            {
                isGreen = observationRequestDto.IsGreen.Value.ToString();
            }

            DbParameter outParameter = null;
            DataTable dataTable = _freeSql.Ado.CommandFluent("sp_observation_statistics")
                 .CommandType(CommandType.StoredProcedure)
                 .CommandTimeout(30 * 60 * 60)
                 .WithParameter("pageIndex", observationRequestDto.Index, x =>
                 {
                     x.DbType = DbType.Int32;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("pageCount", observationRequestDto.PageCount, x =>
                 {
                     x.DbType = DbType.Int32;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("rowTotal", null, x =>
                 {
                     outParameter = x;
                     x.DbType = DbType.Int32;
                     x.Direction = ParameterDirection.Output;
                 })
                 .WithParameter("startTime", startTime, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("endTime", endTime, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("deptCode", deptCode, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("visitState", visitState, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("inpatientName", inpatientName, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("search", search, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("isGreen", isGreen, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .ExecuteDataTable();

            int rowTotal = (int)outParameter.Value;
            List<ObservationResponseDto> observationResponseDtos = DataTableToList<ObservationResponseDto>(dataTable);

            observationResponseDtos = Conversion(observationResponseDtos);

            PagedResultDto<ObservationResponseDto> res = new PagedResultDto<ObservationResponseDto>
            {
                TotalCount = rowTotal,
                Items = observationResponseDtos
            };

            return res;
        }

        private List<RescueResponseDto> Conversion(List<RescueResponseDto> rescueResponseDtos)
        {
            foreach (RescueResponseDto rescueResponseDto in rescueResponseDtos)
            {
                rescueResponseDto.VisitStatusName = GetVisitStatusName(rescueResponseDto.VisitStatus);
                rescueResponseDto.InRescueTime1 = rescueResponseDto.InRescueTime.ToString("yyyy-MM-dd HH:mm:ss");
                if (rescueResponseDto.FirstRecipeTime.HasValue)
                {
                    rescueResponseDto.FirstRecipeTime1 = rescueResponseDto.FirstRecipeTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (rescueResponseDto.OutRescueTime.HasValue)
                {
                    rescueResponseDto.OutRescueTime1 = rescueResponseDto.OutRescueTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (rescueResponseDto.OutRescueTime.HasValue)
                {
                    TimeSpan time = rescueResponseDto.OutRescueTime.Value - rescueResponseDto.InRescueTime;
                    rescueResponseDto.RescueRetentionTime = time.TotalMinutes;
                }
                else
                {
                    TimeSpan time = DateTime.Now - rescueResponseDto.InRescueTime;
                    rescueResponseDto.RescueRetentionTime = time.TotalMinutes;
                }

                int retentionTime = (int)rescueResponseDto.RescueRetentionTime;
                int day = retentionTime / (60 * 24);
                retentionTime = retentionTime % (60 * 24);
                int hour = retentionTime / 60;
                int min = retentionTime % 60;

                StringBuilder sb = new StringBuilder();
                if (day > 0) sb.AppendFormat("{0}天", day);
                if (hour > 0) sb.AppendFormat("{0}时", hour);
                if (min > 0) sb.AppendFormat("{0}分", min);
                rescueResponseDto.RescueRetentionTime1 = sb.ToString();

                rescueResponseDto.RescueRetentionTime2 = (rescueResponseDto.RescueRetentionTime / 60).ToString("#0.#");

                rescueResponseDto.TriageLevelName = GetTriageLevelName(rescueResponseDto.TriageLevel);
                rescueResponseDto.BedHeadStickerName = GetBedHeadSticker(rescueResponseDto.BedHeadSticker);
                rescueResponseDto.ToAreaName = GetToAreaName(rescueResponseDto.ToArea);
                rescueResponseDto.IsOpenGreenChannlName = rescueResponseDto.IsOpenGreenChannl ? "是" : string.Empty;
            }

            return rescueResponseDtos;
        }

        private List<ObservationResponseDto> Conversion(List<ObservationResponseDto> observationResponseDtos)
        {
            foreach (ObservationResponseDto observationResponseDto in observationResponseDtos)
            {
                observationResponseDto.VisitStatusName = GetVisitStatusName(observationResponseDto.VisitStatus);
                observationResponseDto.InObservationTime1 = observationResponseDto.InObservationTime.ToString("yyyy-MM-dd HH:mm:ss");
                if (observationResponseDto.FirstRecipeTime.HasValue)
                {
                    observationResponseDto.FirstRecipeTime1 = observationResponseDto.FirstRecipeTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (observationResponseDto.OutObservationTime.HasValue)
                {
                    observationResponseDto.OutObservationTime1 = observationResponseDto.OutObservationTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (observationResponseDto.OutObservationTime.HasValue)
                {
                    TimeSpan time = observationResponseDto.OutObservationTime.Value - observationResponseDto.InObservationTime;
                    observationResponseDto.ObservationRetentionTime = time.TotalMinutes;
                }
                else
                {
                    TimeSpan time = DateTime.Now - observationResponseDto.InObservationTime;
                    observationResponseDto.ObservationRetentionTime = time.TotalMinutes;
                }

                int retentionTime = (int)observationResponseDto.ObservationRetentionTime;
                int day = retentionTime / (60 * 24);
                retentionTime = retentionTime % (60 * 24);
                int hour = retentionTime / 60;
                int min = retentionTime % 60;

                StringBuilder sb = new StringBuilder();
                if (day > 0) sb.AppendFormat("{0}天", day);
                if (hour > 0) sb.AppendFormat("{0}时", hour);
                if (min > 0) sb.AppendFormat("{0}分", min);
                observationResponseDto.ObservationRetentionTime1 = sb.ToString();

                observationResponseDto.ObservationRetentionTime2 = (observationResponseDto.ObservationRetentionTime / 60).ToString("#0.#");

                observationResponseDto.TriageLevelName = GetTriageLevelName(observationResponseDto.TriageLevel);
                observationResponseDto.BedHeadStickerName = GetBedHeadSticker(observationResponseDto.BedHeadSticker);
                observationResponseDto.ToAreaName = GetToAreaName(observationResponseDto.ToArea);
                observationResponseDto.IsOpenGreenChannlName = observationResponseDto.IsOpenGreenChannl ? "是" : string.Empty;
            }

            return observationResponseDtos;
        }

        private string GetToAreaName(string toArea)
        {
            if (string.IsNullOrEmpty(toArea)) return string.Empty;

            switch (toArea)
            {
                case "ToHospital": return "转住院";
                case "ObservationArea": return "留观";
                case "RescueArea": return "抢救";
                case "EndVisit": return "结束就诊";
                case "OutDept": return "出科";
                default: return toArea;
            }
        }

        private string GetBedHeadSticker(string bedHeadSticker)
        {
            if (string.IsNullOrEmpty(bedHeadSticker)) return string.Empty;

            string[] bedHeadStickers = bedHeadSticker.Split(',', StringSplitOptions.RemoveEmptyEntries);
            List<string> result = new List<string>();
            foreach (string item in bedHeadStickers)
            {
                switch (item.ToLower())
                {
                    case "preventionoffalls": result.Add("跌"); break;
                    case "pressureproofsore": result.Add("压"); break;
                    case "fallproofwear": result.Add("坠"); break;
                    case "criticallyill": result.Add("危"); break;
                    case "wasseriouslyill": result.Add("重"); break;
                    default: break;
                }
            }

            return string.Join(",", result);
        }

        private string GetTriageLevelName(string triageLevel)
        {
            if (string.IsNullOrEmpty(triageLevel)) return string.Empty;

            switch (triageLevel)
            {
                case "TriageLevel_001": return "I级";
                case "TriageLevel_002": return "II级";
                case "TriageLevel_003": return "III级";
                case "TriageLevel_004": return "IV级";
                case "TriageLevel_005": return "V级";
                default: return triageLevel;
            }
        }

        private string GetVisitStatusName(int visitStatus)
        {
            switch (visitStatus)
            {
                case 0: return "未挂号";
                case 1: return "待就诊";
                case 2: return "过号";
                case 3: return "已退号";
                case 4: return "正在就诊";
                case 5: return "已就诊";
                case 6: return "出科";
                default: return "未知";
            }
        }

        private List<T> DataTableToList<T>(DataTable dataTable) where T : new()
        {
            List<T> list = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                T obj = new T();
                foreach (DataColumn column in dataTable.Columns)
                {
                    PropertyInfo propertyInfo = typeof(T).GetProperty(column.ColumnName);
                    if (propertyInfo != null && row[column] != DBNull.Value)
                    {
                        propertyInfo.SetValue(obj, row[column]);
                    }
                }
                list.Add(obj);
            }

            return list;
        }

        public async Task<ResponseResult<List<RescueStatisticsDto>>> GetRescueProjectStatistics(DateTime? dateTime)
        {
            if (dateTime == null) dateTime = DateTime.Now;
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();

            Task<List<RescueStatisticsDto>> rescueTotalCounts = RescueTotalCountAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> criticallyillCounts = CriticallyillCountAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> wasseriouslyillCounts = WasseriouslyillCountAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> toHospitalWayCodeCounts = ToHospitalWayCodeCountAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> greenRoadCounts = GreenRoadCountAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> retentionCount = RetentionCountAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> usageCounts = UsageCountAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> patientDestinationCounts = PatientDestinationCountAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> totalCounts = TotalCountAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> cPRCounts = CPRCountAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> intubationCounts = IntubationCountsAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> ventilatorCounts = VentilatorCountsAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> conduitCounts = ConduitCountsAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> iABPCounts = IABPCountsAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> gastricLavageCounts = GastricLavageCountsAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> crrtCounts = CRRTCountsAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> eCMOCounts = ECMOCountsAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> hotCounts = HotCountsAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> aBGCounts = ABGCountsAsync(dateTime.Value);
            Task<List<RescueStatisticsDto>> sputumCounts = SputumCountsAsync(dateTime.Value);

            rescueStatisticsDtos.AddRange(await rescueTotalCounts);
            rescueStatisticsDtos.AddRange(await criticallyillCounts);
            rescueStatisticsDtos.AddRange(await wasseriouslyillCounts);
            rescueStatisticsDtos.AddRange(await toHospitalWayCodeCounts);
            rescueStatisticsDtos.AddRange(await greenRoadCounts);
            rescueStatisticsDtos.AddRange(await cPRCounts);
            rescueStatisticsDtos.AddRange(await intubationCounts);
            rescueStatisticsDtos.AddRange(await ventilatorCounts);
            rescueStatisticsDtos.AddRange(await conduitCounts);
            rescueStatisticsDtos.AddRange(await iABPCounts);
            rescueStatisticsDtos.AddRange(await gastricLavageCounts);
            rescueStatisticsDtos.AddRange(await crrtCounts);
            rescueStatisticsDtos.AddRange(await eCMOCounts);
            rescueStatisticsDtos.AddRange(await hotCounts);
            rescueStatisticsDtos.AddRange(await retentionCount);
            rescueStatisticsDtos.AddRange(await aBGCounts);
            rescueStatisticsDtos.AddRange(await sputumCounts);
            rescueStatisticsDtos.AddRange(await usageCounts);
            rescueStatisticsDtos.AddRange(await patientDestinationCounts);
            rescueStatisticsDtos.AddRange(await totalCounts);

            return RespUtil.Ok(data: rescueStatisticsDtos);
        }

        private async Task<List<RescueStatisticsDto>> SputumCountsAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);

            int count1 = await StatisticsConduitCountAsync(startTime, endTime, "吸痰");
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "吸痰次数",
                Count = count1
            });

            return rescueStatisticsDtos;
        }

        private async Task<List<RescueStatisticsDto>> ABGCountsAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);

            int count1 = await StatisticsConduitCountAsync(startTime, endTime, "动脉血气分析");
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "动脉血气分析次数",
                Count = count1
            });

            return rescueStatisticsDtos;
        }

        private async Task<List<RescueStatisticsDto>> HotCountsAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);

            List<DiagnoseRecord> diagnoseRecords = await _freeSql.Select<DiagnoseRecord>().Where(x => x.DiagnoseClassCode == DiagnoseClass.开立 && !x.IsDeleted && x.CreationTime >= startTime && x.CreationTime < endTime && x.DiagnoseName.Contains("发热")).ToListAsync();
            IEnumerable<Guid> piids = diagnoseRecords.Select(x => x.PI_ID).Distinct();
            List<Guid> piidList = await _freeSql.Select<AdmissionRecord>().Where(x => piids.Contains(x.PI_ID) && x.AreaCode == "RescueArea").ToListAsync(x => x.PI_ID);

            piids = await _freeSql.Select<AdmissionRecord>().Where(x => x.AreaCode == "RescueArea" && x.InDeptTime >= startTime && x.InDeptTime < endTime).ToListAsync(x => x.PI_ID);
            piids = await _freeSql.Select<VitalSignInfo>().Where(x => piids.Contains(x.PI_ID) && Convert.ToDouble(x.Temp) >= 37.2).ToListAsync(x => x.PI_ID);
            piidList.AddRange(piids);
            int count = piidList.Distinct().Count();

            rescueStatisticsDtos.Add(new RescueStatisticsDto
            {
                ProjectName = "发热人数",
                Count = count
            });
            return rescueStatisticsDtos;
        }

        private async Task<List<RescueStatisticsDto>> ECMOCountsAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);

            int count1 = await StatisticsConduitCountAsync(startTime, endTime, "ECMO");

            List<DiagnoseRecord> diagnoseRecords = await _freeSql.Select<DiagnoseRecord>().Where(x => x.DiagnoseClassCode == DiagnoseClass.开立 && !x.IsDeleted && x.CreationTime >= startTime && x.CreationTime < endTime && (x.DiagnoseName.Contains("中毒") || x.DiagnoseName.Contains("酒精过量"))).ToListAsync();

            IEnumerable<Guid> piids = diagnoseRecords.Select(x => x.PI_ID).Distinct();
            long count2 = await _freeSql.Select<AdmissionRecord>().Where(x => piids.Contains(x.PI_ID) && x.AreaCode == "RescueArea").CountAsync();

            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "ECMO人数",
                Count = count1
            });

            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "中毒人数",
                Count = (int)count2
            });

            return rescueStatisticsDtos;
        }

        private async Task<List<RescueStatisticsDto>> CRRTCountsAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);

            DataTable dataTable = await _freeSql.Ado.CommandFluent("sp_rescue_crrt")
              .CommandType(CommandType.StoredProcedure)
              .CommandTimeout(30 * 60 * 60)
              .WithParameter("startTime", startTime, x =>
              {
                  x.DbType = DbType.DateTime2;
                  x.Direction = ParameterDirection.Input;
              })
             .WithParameter("endTime", endTime, x =>
             {
                 x.DbType = DbType.DateTime2;
                 x.Direction = ParameterDirection.Input;
             })
             .ExecuteDataTableAsync();

            int count = (int)dataTable.Rows[0][0];
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "CRRT人数",
                Count = count
            });

            return rescueStatisticsDtos;
        }

        private async Task<List<RescueStatisticsDto>> GastricLavageCountsAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);

            int count1 = await StatisticsConduitCountAsync(startTime, endTime, "洗胃");
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "洗胃人数",
                Count = count1
            });

            return rescueStatisticsDtos;
        }

        private async Task<List<RescueStatisticsDto>> IABPCountsAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);

            int count1 = await StatisticsConduitCountAsync(startTime, endTime, "中心静脉测压");
            int count2 = await StatisticsConduitCountAsync(startTime, endTime, "动脉血压监测");
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "IABP监测总人数（ABP监测人数+CVP监测人数）",
                Count = count1 + count2
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "ABP监测人数",
                Count = count1
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "CVP监测人数",
                Count = count2
            });

            return rescueStatisticsDtos;
        }

        private async Task<List<RescueStatisticsDto>> ConduitCountsAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);

            int count1 = await StatisticsConduitCountAsync(startTime, endTime, "中心静脉导管");
            int count2 = await StatisticsConduitCountAsync(startTime, endTime, "胸腔引流管");
            int count3 = await StatisticsConduitCountAsync(startTime, endTime, "腹腔引流导管");
            int count4 = await StatisticsConduitCountAsync(startTime, endTime, "导尿管");
            int count5 = await StatisticsConduitCountAsync(startTime, endTime, "胃管");
            int count6 = await StatisticsConduitCountAsync(startTime, endTime, "动脉导管");

            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "中心静脉导管置管人数",
                Count = count1
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "胸腔引流管置管人数",
                Count = count2
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "腹腔引流管置管人数",
                Count = count3
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "尿管置管人数",
                Count = count4
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "胃管置管人数",
                Count = count5
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "动脉导管置管人数",
                Count = count6
            });

            return rescueStatisticsDtos;
        }

        private async Task<int> StatisticsConduitCountAsync(DateTime startTime, DateTime endTime, string recipeName)
        {
            DataTable dataTable = await _freeSql.Ado.CommandFluent("sp_rescue_conduit")
                          .CommandType(CommandType.StoredProcedure)
                          .CommandTimeout(30 * 60 * 60)
                          .WithParameter("startTime", startTime, x =>
                          {
                              x.DbType = DbType.DateTime2;
                              x.Direction = ParameterDirection.Input;
                          })
                         .WithParameter("endTime", endTime, x =>
                         {
                             x.DbType = DbType.DateTime2;
                             x.Direction = ParameterDirection.Input;
                         })
                         .WithParameter("recipeName", $"%{recipeName}%", x =>
                         {
                             x.DbType = DbType.String;
                             x.Direction = ParameterDirection.Input;
                         })
                         .ExecuteDataTableAsync();

            return (int)dataTable.Rows[0][0];
        }

        private async Task<List<RescueStatisticsDto>> VentilatorCountsAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);

            int count1 = await StatisticsVentilatorCountAsync(startTime, endTime, "有创呼吸机辅助通气");
            int count2 = await StatisticsVentilatorCountAsync(startTime, endTime, "无创呼吸机辅助通气");

            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "呼吸机辅助通气总人数（有创呼吸机辅助通气人数+无创呼吸机辅助通气人数）",
                Count = count1 + count2
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "有创呼吸机辅助通气人数",
                Count = count1
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "无创呼吸机辅助通气人数",
                Count = count2
            });

            return rescueStatisticsDtos;
        }

        private async Task<int> StatisticsVentilatorCountAsync(DateTime startTime, DateTime endTime, string recipeName)
        {
            DataTable dataTable = await _freeSql.Ado.CommandFluent("sp_rescue_ventilator")
                          .CommandType(CommandType.StoredProcedure)
                          .CommandTimeout(30 * 60 * 60)
                          .WithParameter("startTime", startTime, x =>
                          {
                              x.DbType = DbType.DateTime2;
                              x.Direction = ParameterDirection.Input;
                          })
                         .WithParameter("endTime", endTime, x =>
                         {
                             x.DbType = DbType.DateTime2;
                             x.Direction = ParameterDirection.Input;
                         })
                         .WithParameter("recipeName", recipeName, x =>
                         {
                             x.DbType = DbType.String;
                             x.Direction = ParameterDirection.Input;
                         })
                         .ExecuteDataTableAsync();

            return (int)dataTable.Rows[0][0];
        }

        private async Task<List<RescueStatisticsDto>> IntubationCountsAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);

            DataTable dataTable = await _freeSql.Ado.CommandFluent("sp_rescue_intubation")
                            .CommandType(CommandType.StoredProcedure)
                            .CommandTimeout(30 * 60 * 60)
                            .WithParameter("startTime", startTime, x =>
                            {
                                x.DbType = DbType.DateTime2;
                                x.Direction = ParameterDirection.Input;
                            })
                            .WithParameter("endTime", endTime, x =>
                            {
                                x.DbType = DbType.DateTime2;
                                x.Direction = ParameterDirection.Input;
                            })
                            .ExecuteDataTableAsync();

            List<Guid> piids = new List<Guid>();
            foreach (DataRow item in dataTable.Rows)
            {
                piids.Add((Guid)item[0]);
            }

            long totalCount = await _freeSql.Select<AdmissionRecord>().Where(x => piids.Contains(x.PI_ID) && x.AreaCode == "RescueArea").CountAsync();

            long deathCount = await _freeSql.Select<AdmissionRecord>().Where(x => piids.Contains(x.PI_ID) && x.AreaCode == "RescueArea" && x.DeathTime! != null).CountAsync();

            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "气管插管人数",
                Count = (int)totalCount
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "气管插管成功人数",
                Count = (int)(totalCount - deathCount)
            });

            return rescueStatisticsDtos;
        }

        private async Task<List<RescueStatisticsDto>> CPRCountAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);

            DataTable dataTable = await _freeSql.Ado.CommandFluent("sp_rescue_cpr")
                            .CommandType(CommandType.StoredProcedure)
                            .CommandTimeout(30 * 60 * 60)
                            .WithParameter("startTime", startTime, x =>
                            {
                                x.DbType = DbType.DateTime2;
                                x.Direction = ParameterDirection.Input;
                            })
                            .WithParameter("endTime", endTime, x =>
                            {
                                x.DbType = DbType.DateTime2;
                                x.Direction = ParameterDirection.Input;
                            })
                            .ExecuteDataTableAsync();

            List<Guid> piids = new List<Guid>();
            foreach (DataRow item in dataTable.Rows)
            {
                piids.Add((Guid)item[0]);
            }

            long totalCount = await _freeSql.Select<AdmissionRecord>().Where(x => piids.Contains(x.PI_ID) && x.AreaCode == "RescueArea").CountAsync();

            long deathCount = await _freeSql.Select<AdmissionRecord>().Where(x => piids.Contains(x.PI_ID) && x.AreaCode == "RescueArea" && x.DeathTime! != null).CountAsync();

            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "心肺复苏人数",
                Count = (int)totalCount
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "心肺复苏成功人数",
                Count = (int)(totalCount - deathCount)
            });

            return rescueStatisticsDtos;
        }

        private async Task<List<RescueStatisticsDto>> TotalCountAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);
            DateTime preStartTime = startTime.AddYears(-10);

            long count0 = await _freeSql.Select<AdmissionRecord>().Where(x => x.InDeptTime < startTime && x.AreaCode == "RescueArea" && (x.OutDeptTime == null || x.OutDeptTime > startTime)).CountAsync();

            long count1 = await _freeSql.Select<AdmissionRecord>().Where(x => x.InDeptTime >= startTime && x.InDeptTime < endTime && x.AreaCode == "RescueArea").CountAsync();
            count1 = count0 + count1;

            List<RescueStatisticsDto> criticallyills = await CriticallyillCountAsync(dateTime);
            List<RescueStatisticsDto> wasseriouslyills = await WasseriouslyillCountAsync(dateTime);

            int count2 = criticallyills[0].Count;
            int count3 = wasseriouslyills[0].Count;

            long count4 = await _freeSql.Select<AdmissionRecord>().Where(x => x.AreaCode == "RescueArea" && x.InDeptTime < dateTime && (x.OutDeptTime > dateTime || x.OutDeptTime == null) && x.DeptCode == "1901").CountAsync();
            long count5 = await _freeSql.Select<AdmissionRecord>().Where(x => x.AreaCode == "RescueArea" && x.InDeptTime < dateTime && (x.OutDeptTime > dateTime || x.OutDeptTime == null) && x.DeptCode == "1902").CountAsync();

            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "现有总人数(病危人数+病重人数+非危重人数)",
                Count = (int)count1
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "病危人数",
                Count = count2
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "病重人数",
                Count = count3
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "非危重人数",
                Count = (int)count1 - count2 - count3
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "内科人数",
                Count = (int)count4
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "外科人数",
                Count = (int)count5
            });

            return rescueStatisticsDtos;
        }

        private async Task<List<RescueStatisticsDto>> PatientDestinationCountAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);

            long count1 = await _freeSql.Select<AdmissionRecord>().Where(x => x.AreaCode == "RescueArea" && x.OutDeptReasonName == "转住院" && x.OutDeptTime >= startTime && x.OutDeptTime < endTime).CountAsync();
            long count2 = await _freeSql.Select<TransferRecord>().Where(x => x.FromAreaCode == "RescueArea" && x.ToAreaCode == "ObservationArea" && x.TransferTime >= startTime && x.TransferTime < endTime).CountAsync();

            long count3 = await _freeSql.Select<AdmissionRecord>().Where(x => x.AreaCode == "RescueArea" && x.VisitStatus == VisitStatus.出科 && x.OutDeptTime >= startTime && x.OutDeptTime < endTime && x.OutDeptReasonName == "常规离院").CountAsync();
            long count4 = await _freeSql.Select<AdmissionRecord>().Where(x => x.AreaCode == "RescueArea" && x.VisitStatus == VisitStatus.出科 && x.OutDeptTime >= startTime && x.OutDeptTime < endTime && x.OutDeptReasonName == "签字离院").CountAsync();
            long count5 = await _freeSql.Select<AdmissionRecord>().Where(x => x.AreaCode == "RescueArea" && x.VisitStatus == VisitStatus.出科 && x.OutDeptTime >= startTime && x.OutDeptTime < endTime && x.OutDeptReasonName == "自行离院").CountAsync();

            long count6 = await _freeSql.Select<AdmissionRecord>().Where(x => x.AreaCode == "RescueArea" && x.DeathTime >= startTime && x.DeathTime < endTime).CountAsync();

            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "住院人数",
                Count = (int)count1
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "转观察区人数",
                Count = (int)count2
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "离院人数",
                Count = (int)(count3 + count4 + count5)
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "常规离院人数",
                Count = (int)count3
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "签字离院人数",
                Count = (int)count4
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "自行离院人数",
                Count = (int)count5
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "转院人数",
                Count = (int)count1
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "死亡人数",
                Count = (int)count6
            });

            return rescueStatisticsDtos;
        }

        private async Task<List<RescueStatisticsDto>> UsageCountAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);
            int count1 = await StatisticsUsageCountAsync(startTime, endTime, "静脉泵入");
            int count2 = await StatisticsUsageCountAsync(startTime, endTime, "静脉输液");
            int count3 = await StatisticsUsageCountAsync(startTime, endTime, "静脉注射");
            int count4 = await StatisticsUsageCountAsync(startTime, endTime, "肌肉注射");
            int count5 = await StatisticsUsageCountAsync(startTime, endTime, "皮下注射");
            int count6 = await StatisticsUsageCountAsync(startTime, endTime, "皮内注射");
            int count7 = await StatisticsUsageCountAsync(startTime, endTime, "雾化吸入");
            int count8 = await StatisticsUsageCountAsync(startTime, endTime, "腹膜透析");

            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "静脉泵入次数",
                Count = count1
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "静脉输液次数",
                Count = count2
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "静脉注射次数",
                Count = count3
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "肌肉注射次数",
                Count = count4
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "皮下注射次数",
                Count = count5
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "皮内注射次数",
                Count = count6
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "雾化吸入次数",
                Count = count7
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "腹膜透析次数",
                Count = count8
            });

            return rescueStatisticsDtos;
        }

        private async Task<int> StatisticsUsageCountAsync(DateTime startTime, DateTime endTime, string usageName)
        {
            DataTable dataTable = await _freeSql.Ado.CommandFluent("sp_rescue_usage")
                  .CommandType(CommandType.StoredProcedure)
                  .CommandTimeout(30 * 60 * 60)
                  .WithParameter("startTime", startTime, x =>
                  {
                      x.DbType = DbType.DateTime2;
                      x.Direction = ParameterDirection.Input;
                  })
                 .WithParameter("endTime", endTime, x =>
                 {
                     x.DbType = DbType.DateTime2;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("usageName", usageName, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .ExecuteDataTableAsync();

            return (int)dataTable.Rows[0][0];
        }

        private async Task<List<RescueStatisticsDto>> RetentionCountAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            List<AdmissionRecord> admissionRecords = await _freeSql.Select<AdmissionRecord>().Where(x => x.AreaCode == "RescueArea" && (x.OutDeptTime > dateTime || x.OutDeptTime == null)).ToListAsync();

            int count = admissionRecords.Where(x => x.InDeptTime.HasValue).Count(x => (dateTime - x.InDeptTime.Value).TotalHours >= 6);
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "滞留≥6h人次",
                Count = count
            });

            return rescueStatisticsDtos;
        }

        private async Task<List<RescueStatisticsDto>> GreenRoadCountAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);
            long count = await _freeSql.Select<AdmissionRecord>().Where(x => x.InDeptTime >= startTime && x.InDeptTime < endTime && x.AreaCode == "RescueArea" && x.IsOpenGreenChannl == true).CountAsync();
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "绿色通道人数",
                Count = (int)count
            });

            return rescueStatisticsDtos;
        }

        private async Task<List<RescueStatisticsDto>> ToHospitalWayCodeCountAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);
            long count1 = await _freeSql.Select<AdmissionRecord>().Where(x => x.InDeptTime >= startTime && x.InDeptTime < endTime && x.AreaCode == "RescueArea" && x.ToHospitalWayName == "本院120").CountAsync();
            long count2 = await _freeSql.Select<AdmissionRecord>().Where(x => x.InDeptTime >= startTime && x.InDeptTime < endTime && x.AreaCode == "RescueArea" && x.ToHospitalWayName == "外院120").CountAsync();

            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "120转入总人数（院前急救+院外转入）",
                Count = (int)(count1 + count2)
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "本院120人数",
                Count = (int)count1
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "外院120人数",
                Count = (int)count2
            });

            return rescueStatisticsDtos;
        }

        private async Task<List<RescueStatisticsDto>> WasseriouslyillCountAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);
            int count1 = await StatisticsEmr1CountAsync(startTime, "病重病历");
            int count2 = await StatisticsEmr2CountAsync(startTime, endTime, "病重病历");
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "病重总人数（原有病重人数+新入病重人数）",
                Count = count1 + count2
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "原有病重人数",
                Count = count1
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "新入病重人数",
                Count = count2
            });

            return rescueStatisticsDtos;
        }

        private async Task<List<RescueStatisticsDto>> CriticallyillCountAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);
            int count1 = await StatisticsEmr1CountAsync(startTime, "病危病历");
            int count2 = await StatisticsEmr2CountAsync(startTime, endTime, "病危病历");
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "病危总人数（原有病危人数+新入病危人数）",
                Count = count1 + count2
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "原有病危人数",
                Count = count1
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "新入病危人数",
                Count = count2
            });

            return rescueStatisticsDtos;
        }

        private async Task<int> StatisticsEmr1CountAsync(DateTime startTime, string emrTitle)
        {
            DataTable dataTable = await _freeSql.Ado.CommandFluent("sp_rescue_emr1")
                  .CommandType(CommandType.StoredProcedure)
                  .CommandTimeout(30 * 60 * 60)
                  .WithParameter("startTime", startTime, x =>
                  {
                      x.DbType = DbType.DateTime2;
                      x.Direction = ParameterDirection.Input;
                  })
                 .WithParameter("emrTitle", emrTitle, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .ExecuteDataTableAsync();

            return (int)dataTable.Rows[0][0];
        }

        private async Task<int> StatisticsEmr2CountAsync(DateTime startTime, DateTime endTime, string emrTitle)
        {
            DataTable dataTable = await _freeSql.Ado.CommandFluent("sp_rescue_emr2")
                  .CommandType(CommandType.StoredProcedure)
                  .CommandTimeout(30 * 60 * 60)
                  .WithParameter("startTime", startTime, x =>
                  {
                      x.DbType = DbType.DateTime2;
                      x.Direction = ParameterDirection.Input;
                  })
                 .WithParameter("endTime", endTime, x =>
                 {
                     x.DbType = DbType.DateTime2;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("emrTitle", emrTitle, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .ExecuteDataTableAsync();

            return (int)dataTable.Rows[0][0];
        }

        private async Task<List<RescueStatisticsDto>> RescueTotalCountAsync(DateTime dateTime)
        {
            List<RescueStatisticsDto> rescueStatisticsDtos = new List<RescueStatisticsDto>();
            DateTime startTime = dateTime.Date;
            DateTime endTime = startTime.AddDays(1);
            long count1 = await _freeSql.Select<AdmissionRecord>().Where(x => x.InDeptTime < startTime && x.AreaCode == "RescueArea" && (x.OutDeptTime == null || x.OutDeptTime > startTime)).CountAsync();

            long count2 = await _freeSql.Select<AdmissionRecord>().Where(x => x.InDeptTime >= startTime && x.InDeptTime < endTime && x.AreaCode == "RescueArea").CountAsync();
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "抢救总人数（原有人数+新入人数）",
                Count = (int)(count1 + count2)
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "原有人数",
                Count = (int)count1
            });
            rescueStatisticsDtos.Add(new RescueStatisticsDto()
            {
                ProjectName = "新入人数",
                Count = (int)count2
            });

            return rescueStatisticsDtos;
        }
    }
}
