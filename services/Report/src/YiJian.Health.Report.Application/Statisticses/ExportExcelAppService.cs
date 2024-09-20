using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using YiJian.Health.Report.Enums;
using YiJian.Health.Report.Statisticses.Dto;
using YiJian.Health.Report.Statisticses.Entities;

namespace YiJian.Health.Report.Statisticses
{
    /// <summary>
    /// 导出Excel报表部分
    /// </summary>
    public partial class StatisticsReportAppService
    {
        /// <summary>
        /// 质控-统计报表
        /// </summary> 
        /// <returns></returns>
        public async Task<IActionResult> GetDownloadExcelAsync(DownloadExcelDto param)
        {
            string filename = string.Empty;
            Stream stream = new MemoryStream();
            switch (param.ReportType)
            {
                case EReportType.DoctorAndPatient:
                    {
                        if (param.IsDetail)
                        {
                            switch (param.DetailInfo!)
                            {
                                case EDetailInfo.DoctorDetail:
                                    {
                                        var data = await ExportDoctorDetailAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                case EDetailInfo.PatientDetail:
                                    {
                                        var data = await ExportDoctorAndPatientDetailAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            switch (param.Veidoo)
                            {
                                case EVeidoo.Month:
                                    {
                                        var data = await ExportMonthDoctorAndPatientAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                case EVeidoo.Quarter:
                                    {
                                        var data = await ExportQuarterDoctorAndPatientAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                case EVeidoo.Year:
                                    {
                                        var data = await ExportYearDoctorAndPatientAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                case EReportType.NurseAndPatient:
                    {
                        if (param.IsDetail)
                        {
                            switch (param.DetailInfo!)
                            {
                                case EDetailInfo.NurseDetail:
                                    {
                                        var data = await ExportNurseDetailAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                case EDetailInfo.PatientDetail:
                                    {
                                        var data = await ExportNurseAndPatientDetailAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            switch (param.Veidoo)
                            {
                                case EVeidoo.Month:
                                    {
                                        var data = await ExportMonthNurseAndPatientAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                case EVeidoo.Quarter:
                                    {
                                        var data = await ExportQuarterNurseAndPatientAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                case EVeidoo.Year:
                                    {
                                        var data = await ExportYearNurseAndPatientAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                case EReportType.LevelAndPatient:
                    {
                        if (param.IsDetail)
                        {
                            var data = await ExportLevelAndPatientDetailAsync(param);
                            stream = data.Stream;
                            filename = data.FileName;
                        }
                        else
                        {
                            switch (param.Veidoo)
                            {
                                case EVeidoo.Month:
                                    {
                                        var data = await ExportMonthLevelAndPatientAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                case EVeidoo.Quarter:
                                    {
                                        var data = await ExportQuarterLevelAndPatientAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                case EVeidoo.Year:
                                    {
                                        var data = await ExportYearLevelAndPatientAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                case EReportType.EmergencyroomAndPatient:
                    {
                        if (param.IsDetail)
                        {
                            var data = await ExportEmergencyroomAndPatientDetailAsync(param);
                            stream = data.Stream;
                            filename = data.FileName;
                        }
                        else
                        {
                            switch (param.Veidoo)
                            {
                                case EVeidoo.Month:
                                    {
                                        var data = await ExportMonthEmergencyroomAndPatientAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                case EVeidoo.Quarter:
                                    {
                                        var data = await ExportQuarterEmergencyroomAndPatientAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                case EVeidoo.Year:
                                    {
                                        var data = await ExportYearEmergencyroomAndPatientAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                case EReportType.EmergencyroomAndDeathPatient:
                    {
                        if (param.IsDetail)
                        {
                            var data = await ExportEmergencyroomAndDeathPatientDetailAsync(param);
                            stream = data.Stream;
                            filename = data.FileName;
                        }
                        else
                        {
                            switch (param.Veidoo)
                            {
                                case EVeidoo.Month:
                                    {
                                        var data = await ExportMonthEmergencyroomAndDeathPatientAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                case EVeidoo.Quarter:
                                    {
                                        var data = await ExportQuarterEmergencyroomAndDeathPatientAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                case EVeidoo.Year:
                                    {
                                        var data = await ExportYearEmergencyroomAndDeathPatientAsync(param);
                                        stream = data.Stream;
                                        filename = data.FileName;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            if (filename.IsNullOrEmpty()) throw new Exception("没有查找到你需要的报表文件");
            if (stream == null) throw new Exception("导出文件异常");

            string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return new FileStreamResult(stream, mimeType)
            {
                FileDownloadName = filename
            };
        }

        #region 急诊科医患关系比

        /// <summary>
        /// 急诊科医患月度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportMonthDoctorAndPatientAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            var list = await _rpMonthDoctorAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsMonthDoctorAndPatient>, List<StatisticsMonthDoctorAndPatientDto>>(list);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpMonthDoctorAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpMonthDoctorAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "DoctorAndPatient", "DoctorAndPatientTemplate.xlsx");
            var value = new
            {
                Title = "急诊科医患比-月度(月)",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);

            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse("急诊科医患比-月度(月).xlsx", stream);
        }

        /// <summary>
        /// 急诊科医患季度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportQuarterDoctorAndPatientAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            var list = await _rpQuarterDoctorAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsQuarterDoctorAndPatient>, List<StatisticsQuarterDoctorAndPatientDto>>(list);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpQuarterDoctorAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpQuarterhDoctorAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "DoctorAndPatient", "DoctorAndPatientTemplate.xlsx");
            var value = new
            {
                Title = "急诊科医患比-季度(季)",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse("急诊科医患比-季度(季).xlsx", stream);
        }

        /// <summary>
        /// 急诊科医患年度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportYearDoctorAndPatientAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            var list = await _rpYearDoctorAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsYearDoctorAndPatient>, List<StatisticsYearDoctorAndPatientDto>>(list);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpYearDoctorAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpYearDoctorAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "DoctorAndPatient", "DoctorAndPatientTemplate.xlsx");
            var value = new
            {
                Title = "急诊科医患比-年度(年)",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse("急诊科医患比-年度(年).xlsx", stream);
        }

        /// <summary>
        /// 医师详细报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportDoctorDetailAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.DateDetail, "begin");
            var end = GetDate(param.Veidoo, param.DateDetail, "end");
            var data = await _rpMonthDoctorAndPatientRepository.GetUspDoctorPatientRatiosAsync(begin, end);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpDoctorDetail_{param.DateDetail}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpDoctorDetail_{param.DateDetail}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "DoctorAndPatient", "DoctorDetailTemplate.xlsx");
            var value = new
            {
                Title = param.DateDetail,
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse($"急诊科医患-{param.DateDetail}-汇总.xlsx", stream);
        }

        /// <summary>
        /// 接诊病人详细报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportDoctorAndPatientDetailAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.DateDetail, "begin");
            var end = GetDate(param.Veidoo, param.DateDetail, "end");
            var data = await _rpMonthDoctorAndPatientRepository.GetViewAdmissionRecordsAsync(begin, end);

            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpDoctorAndPatientDetail_{param.DateDetail}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpDoctorAndPatientDetail_{param.DateDetail}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "PatientDetailTemplate.xlsx");
            var value = new
            {
                Title = param.DateDetail,
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse($"接诊病人详细报表(医生)-{param.DateDetail}.xlsx", stream);
        }

        #endregion

        #region 急诊科护患关系比

        /// <summary>
        /// 急诊科护患月度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportMonthNurseAndPatientAsync(DownloadExcelDto param)
        {

            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            var list = await _rpMonthNurseAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsMonthNurseAndPatient>, List<StatisticsMonthNurseAndPatientDto>>(list);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpMonthNurseAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpMonthNurseAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "NurseAndPatient", "NurseAndPatientTemplate.xlsx");
            var value = new
            {
                Title = "急诊科护患比-月度(月)",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse("急诊科护患比-月度(月).xlsx", stream);
        }

        /// <summary>
        /// 急诊科护患季度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportQuarterNurseAndPatientAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            var list = await _rpQuarterNurseAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsQuarterNurseAndPatient>, List<StatisticsQuarterNurseAndPatientDto>>(list);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpQuarterNurseAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpQuarterNurseAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "NurseAndPatient", "NurseAndPatientTemplate.xlsx");
            var value = new
            {
                Title = "急诊科护患比-季度(季)",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse("急诊科护患比-季度(季).xlsx", stream);
        }

        /// <summary>
        /// 急诊科护患年度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportYearNurseAndPatientAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            var list = await _rpYearNurseAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsYearNurseAndPatient>, List<StatisticsYearNurseAndPatientDto>>(list);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpYearNurseAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpYearNurseAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "NurseAndPatient", "NurseAndPatientTemplate.xlsx");
            var value = new
            {
                Title = "急诊科护患比-年度(年)",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse("急诊科护患比-年度(年).xlsx", stream);
        }

        /// <summary>
        /// 护士详细报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<DownloadExcelResponse> ExportNurseDetailAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.DateDetail, "begin");
            var end = GetDate(param.Veidoo, param.DateDetail, "end");
            var data = await _rpMonthNurseAndPatientRepository.GetUspNursePatientRatiosAsync(begin, end);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpNurseDetail_{param.DateDetail}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpNurseDetail_{param.DateDetail}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "NurseAndPatient", "NurseDetailTemplate.xlsx");
            var value = new
            {
                Title = param.DateDetail,
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse($"急诊科护患-{param.DateDetail}-汇总.xlsx", stream);
        }


        /// <summary>
        /// 接诊病人详细报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<DownloadExcelResponse> ExportNurseAndPatientDetailAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.DateDetail, "begin");
            var end = GetDate(param.Veidoo, param.DateDetail, "end");
            var data = await _rpMonthNurseAndPatientRepository.GetViewAdmissionRecordsAsync(begin, end);

            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpNurseAndPatientDetail_{param.DateDetail}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpNurseAndPatientDetail_{param.DateDetail}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "PatientDetailTemplate.xlsx");
            var value = new
            {
                Title = param.DateDetail,
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse($"接诊病人详细报表（护士）-{param.DateDetail}.xlsx", stream);
        }

        #endregion

        #region 急诊科各级患者比例

        /// <summary>
        /// 急诊科各级患者比例月度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportMonthLevelAndPatientAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            var list = await _rpMonthLevelAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsMonthLevelAndPatient>, List<StatisticsMonthLevelAndPatientDto>>(list);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpMonthLevelAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpMonthLevelAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "LevelAndPatient", "LevelAndPatientTemplate.xlsx");
            var value = new
            {
                Title = "急诊科各级患者比例-月度(月)",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse("急诊科各级患者比例-月度(月).xlsx", stream);
        }

        /// <summary>
        /// 急诊科各级患者比例季度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportQuarterLevelAndPatientAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            var list = await _rpQuarterLevelAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsQuarterLevelAndPatient>, List<StatisticsQuarterLevelAndPatientDto>>(list);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpQuarterLevelAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpQuarterLevelAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "LevelAndPatient", "LevelAndPatientTemplate.xlsx");
            var value = new
            {
                Title = "急诊科各级患者比例-季度(季)",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse("急诊科各级患者比例-季度(季).xlsx", stream);
        }

        /// <summary>
        /// 急诊科各级患者比例年度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportYearLevelAndPatientAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            var list = await _rpYearLevelAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsYearLevelAndPatient>, List<StatisticsYearLevelAndPatientDto>>(list);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpYearLevelAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpYearLevelAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "LevelAndPatient", "LevelAndPatientTemplate.xlsx");
            var value = new
            {
                Title = "急诊科各级患者比例-年度(年)",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse("急诊科各级患者比例-年度(年).xlsx", stream);
        }

        /// <summary>
        /// 详细报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportLevelAndPatientDetailAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.DateDetail, "begin");
            var end = GetDate(param.Veidoo, param.DateDetail, "end");
            var data = await _rpMonthLevelAndPatientRepository.GetViewAdmissionRecordsAsync(begin, end);

            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpLevelAndPatientDetail_{param.DateDetail}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpLevelAndPatientDetail_{param.DateDetail}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "LevelAndPatient", "LevelAndPatientDetailTemplate.xlsx");
            var value = new
            {
                Title = param.DateDetail,
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse($"各级患者比例详细报表-{param.DateDetail}.xlsx", stream);
        }

        #endregion

        #region 抢救室滞留时间中位数比例

        /// <summary>
        /// 抢救室滞留时间中位数月度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportMonthEmergencyroomAndPatientAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            var list = await _rpMonthEmergencyroomAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsMonthEmergencyroomAndPatient>, List<StatisticsMonthEmergencyroomAndPatientDto>>(list);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpMonthEmergencyroomAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpMonthEmergencyroomAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "EmergencyroomAndPatient", "EmergencyroomAndPatientTemplate.xlsx");
            var value = new
            {
                Title = "抢救室滞留时间中位数-月度(月)",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse("抢救室滞留时间中位数-月度(月).xlsx", stream);
        }

        /// <summary>
        /// 抢救室滞留时间中位数季度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportQuarterEmergencyroomAndPatientAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            var list = await _rpQuarterEmergencyroomAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsQuarterEmergencyroomAndPatient>, List<StatisticsQuarterEmergencyroomAndPatientDto>>(list);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpQuarterEmergencyroomAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpQuarterEmergencyroomAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "EmergencyroomAndPatient", "EmergencyroomAndPatientTemplate.xlsx");
            var value = new
            {
                Title = "抢救室滞留时间中位数-季度(季)",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse("抢救室滞留时间中位数-季度(季).xlsx", stream);
        }

        /// <summary>
        /// 抢救室滞留时间中位数年度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportYearEmergencyroomAndPatientAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            var list = await _rpYearEmergencyroomAndPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsYearEmergencyroomAndPatient>, List<StatisticsYearEmergencyroomAndPatientDto>>(list);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpYearEmergencyroomAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpYearEmergencyroomAndPatient_{param.BeginDate}_{param.EndDate}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "EmergencyroomAndPatient", "EmergencyroomAndPatientTemplate.xlsx");
            var value = new
            {
                Title = "抢救室滞留时间中位数-年度(年)",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse("抢救室滞留时间中位数-年度(年).xlsx", stream);
        }

        /// <summary>
        /// 抢救室滞留详情报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportEmergencyroomAndPatientDetailAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.DateDetail, "begin");
            var end = GetDate(param.Veidoo, param.DateDetail, "end");
            var data = await _rpMonthEmergencyroomAndPatientRepository.GetViewAdmissionRecordsAsync(begin, end);

            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpEmergencyroomAndPatientDetail_{param.DateDetail}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpEmergencyroomAndPatientDetail_{param.DateDetail}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "EmergencyroomAndPatient", "EmergencyroomAndPatientDetailTemplate.xlsx");
            var value = new
            {
                Title = param.DateDetail,
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse($"抢救室滞留详情报表-{param.DateDetail}.xlsx", stream);
        }

        #endregion

        #region 急诊抢救室患者死亡率

        /// <summary>
        /// 急诊抢救室患者死亡率月度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportMonthEmergencyroomAndDeathPatientAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            var list = await _rpMonthEmergencyroomAndDeathPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsMonthEmergencyroomAndDeathPatient>, List<StatisticsMonthEmergencyroomAndDeathPatientDto>>(list);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpMonthEmergencyroomAndDeathPatient_{param.BeginDate}_{param.EndDate}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpMonthEmergencyroomAndDeathPatient_{param.BeginDate}_{param.EndDate}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "EmergencyroomAndDeathPatient", "EmergencyroomAndDeathPatientTemplate.xlsx");
            var value = new
            {
                Title = "急诊抢救室患者死亡率-月度(月)",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse("急诊抢救室患者死亡率-月度(月).xlsx", stream);
        }


        /// <summary>
        /// 急诊抢救室患者死亡率季度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportQuarterEmergencyroomAndDeathPatientAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            var list = await _rpQuarterEmergencyroomAndDeathPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsQuarterEmergencyroomAndDeathPatient>, List<StatisticsQuarterEmergencyroomAndDeathPatientDto>>(list);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpQuarterEmergencyroomAndDeathPatient_{param.BeginDate}_{param.EndDate}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpQuarterEmergencyroomAndDeathPatient_{param.BeginDate}_{param.EndDate}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "EmergencyroomAndDeathPatient", "EmergencyroomAndDeathPatientTemplate.xlsx");
            var value = new
            {
                Title = "急诊抢救室患者死亡率-季度(季)",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse("急诊抢救室患者死亡率-季度(季).xlsx", stream);
        }


        /// <summary>
        /// 急诊抢救室患者死亡率年度报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportYearEmergencyroomAndDeathPatientAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.BeginDate, "begin");
            var end = GetDate(param.Veidoo, param.EndDate, "end");

            var list = await _rpYearEmergencyroomAndDeathPatientRepository.GetListAsync(begin, end);
            var data = ObjectMapper.Map<List<StatisticsYearEmergencyroomAndDeathPatient>, List<StatisticsYearEmergencyroomAndDeathPatientDto>>(list);
            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpYearEmergencyroomAndDeathPatient_{param.BeginDate}_{param.EndDate}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpYearEmergencyroomAndDeathPatient_{param.BeginDate}_{param.EndDate}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "EmergencyroomAndDeathPatient", "EmergencyroomAndDeathPatientTemplate.xlsx");
            var value = new
            {
                Title = "急诊抢救室患者死亡率-年度(年)",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse("急诊抢救室患者死亡率-年度(年).xlsx", stream);
        }

        /// <summary>
        /// 急诊抢救室患者死亡率详情报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        private async Task<DownloadExcelResponse> ExportEmergencyroomAndDeathPatientDetailAsync(DownloadExcelDto param)
        {
            var begin = GetDate(param.Veidoo, param.DateDetail, "begin");
            var end = GetDate(param.Veidoo, param.DateDetail, "end");
            var data = await _rpMonthEmergencyroomAndDeathPatientRepository.GetViewAdmissionRecordsAsync(begin, end);

            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            var path = Path.Combine(tempPath, $"RpEmergencyroomAndDeathPatientDetail_{param.DateDetail}.xlsx");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_RpEmergencyroomAndDeathPatientDetail_{param.DateDetail}.xlsx");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "EmergencyroomAndDeathPatient", "EmergencyroomAndDeathPatientDetailTemplate.xlsx");
            var value = new
            {
                Title = param.DateDetail,
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new DownloadExcelResponse($"急诊抢救室患者死亡率详情报表-{param.DateDetail}.xlsx", stream);
        }

        #endregion

        /// <summary>
        /// 获取Excel模板文件，目录从Templates 开始，之后的目录需要写到filename里，eg "DoctorAndPatient/DoctorDetailTemplate.xlsx"
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private string GetExcelTemplateFile(string filename)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", filename);
        }

        /// <summary>
        /// Veidoo = 0 月度, 传值表达式为 2023M2 ,表示2023年第二个月结束
        /// Veidoo = 1 季度, 传值表达式为 2023Q1 ,表示2023年第一个季度结束 
        /// Veidoo = 2 年度, 传值表达式为 2023 ,表示2023年结束
        /// </summary>
        /// <param name="veidoo"></param> 
        /// <param name="date"></param>
        /// <param name="flag">标记位，begin=开始时间，other=结束时间</param>
        private DateTime GetDate(EVeidoo veidoo, string date, string flag = "begin")
        {
            try
            {
                switch (veidoo)
                {
                    case EVeidoo.Month:
                        {
                            var month = date.Split('M', 'm');
                            var begin = new DateTime(int.Parse(month[0]), int.Parse(month[1]), 1, 0, 0, 0);
                            if (flag == "begin") return begin;  //开始时间
                            return begin.AddMonths(1).AddSeconds(-1);  //结束时间
                        }
                    case EVeidoo.Quarter:
                        {
                            var quarter = date.Split('Q', 'q');
                            var month = 1;
                            var q = int.Parse(quarter[1]);
                            switch (q)
                            {
                                case 1:
                                    month = 1;
                                    break;
                                case 2:
                                    month = 4;
                                    break;
                                case 3:
                                    month = 7;
                                    break;
                                case 4:
                                    month = 10;
                                    break;
                                default:
                                    break;
                            }

                            var begin = new DateTime(int.Parse(quarter[0]), month, 1);
                            if (flag == "begin") return begin;
                            return begin.AddMonths(3).AddSeconds(-1);
                        }
                    case EVeidoo.Year:
                        {
                            var begin = new DateTime(int.Parse(date), 1, 1);
                            if (flag == "begin") return begin;
                            return begin.AddYears(1).AddSeconds(-1);
                        }
                    default:
                        throw new Exception("没有实现非年度，季度，月度的维度参数转换");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取规则的日期转换异常：{ex.Message}");
                throw new Exception("查询时间转换异常");
            }
        }



    }

}
