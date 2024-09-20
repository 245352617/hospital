using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 导出接口
    /// </summary>
    [ApiController]
    [Authorize]
    public class TriageExcelController : ControllerBase
    {
        private readonly IPatientInfoAppService _patientInfoService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ITableSettingAppService _iTableSettingService;
        private readonly IFastTrackRegisterInfoAppService _iFastTrackRegisterInfoAppService;
        private readonly ILogger<TriageExcelController> _log;


        /// <summary>
        /// 导出接口
        /// </summary>
        /// <param name="patientInfoService"></param>
        /// <param name="hostEnvironment"></param>
        /// <param name="iTableSettingService"></param>
        /// <param name="iFastTrackRegisterInfoAppService"></param>
        public TriageExcelController(IPatientInfoAppService patientInfoService,
            IWebHostEnvironment hostEnvironment,
            ITableSettingAppService iTableSettingService,
            IFastTrackRegisterInfoAppService iFastTrackRegisterInfoAppService,
            ILogger<TriageExcelController> log)
        {
            _patientInfoService = patientInfoService;
            _hostEnvironment = hostEnvironment;
            _iTableSettingService = iTableSettingService;
            _iFastTrackRegisterInfoAppService = iFastTrackRegisterInfoAppService;
            _log= log;
        }

        /// <summary>
        /// 分诊记录导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("api/TriageService/PatientInfo/PatientInfoListExport")]
        // [AllowAnonymous]
        public async Task<IActionResult> PatientInfoExport([FromQuery] PatientInfoWhereInput input)
        {
            try
            {
                _log.LogDebug("api/TriageService/PatientInfo/PatientInfoListExport 分诊记录导出开始");
                input.MaxResultCount = int.MaxValue;
                var list = await _patientInfoService.GetPatientInfoListAsync(input);
                var emportList = list.Data.Items.BuildAdapter().AdaptToType<List<PatientInfoExportExcelDto>>();
                var triageTable = await _iTableSettingService.QueryTableSetting(new QueryTableSettingInput()
                    { TableCode = "TriageTable" });
                string name = "";
                string value = "";
                foreach (var t in triageTable.Data.Where(t => t.Visible))
                {
                    name += t.ColumnName + ",";
                    if (t.ColumnValue == "operator")
                    {
                        value += "operators,";
                    }
                    else
                    {
                        value += t.ColumnValue + ",";
                    }
                }
                
                foreach (var item in emportList)
                {
                    // 主诉特殊处理：在后面添加重点病历
                    if (!string.IsNullOrWhiteSpace(item.DiseaseCode))
                    {
                        if (!string.IsNullOrEmpty(item.Narration))
                            item.Narration += "," + item.DiseaseCode;
                        else
                            item.Narration = item.DiseaseCode;
                    }
                }

                byte[] buffer = new ExcelHelper<PatientInfoExportExcelDto>().ExportToExcel(_hostEnvironment,
                    "分诊记录",
                    emportList,
                    value.TrimEnd(',').Split(','),
                    name.TrimEnd(',').Split(','));
                return File(buffer, "application/octet-stream",
                    "分诊记录.xls" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
            }
            catch (Exception e)
            {
                _log.LogError($"PatientInfo/PatientInfoListExport 分诊记录导出错误：{e.Message}");
                throw e;
            }
        }

        /// <summary>
        /// 快速通道导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("api/TriageService/FastTrackRegisterInfoApp/FastTrackRegisterInfoExport")]
        public async Task<IActionResult> FastTrackRegisterInfoExport([FromQuery] FastTrackRegisterInfoInput input)
        {
            input.MaxResultCount = int.MaxValue;
     
            var fastList = await _iFastTrackRegisterInfoAppService.GetListAsync(input);
            var exportList = fastList.Data.Items.ToList();
            byte[] buffer = new ExcelHelper<FastTrackRegisterInfoDto>().ExportToExcel(_hostEnvironment,
                "快速通道列表",
                exportList,
                new[]
                {
                    "PatientName", "Sex", "Age", "PoliceStationName", "PoliceStationPhone", "PoliceCode", "PoliceName",
                    "ReceptionNurse", "Remark", "CreationTime"
                },
                new[] {"姓名", "性别", "年龄", "所属派出所", "派出所电话", "警务人员电话", "警务人员姓名", "接诊护士", "备注", "创建时间"});
            return File(buffer, "application/octet-stream", "快速通道列表"+DateTime.Now.ToString("yyyyMMddHHmmss")+".xls");
        }

        /// <summary>
        /// 告知记录导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("api/TriageService/PatientInfo/PatientInformListExport")]
        public async Task<IActionResult> PatientInformListExport([FromQuery] PatientInformQueryDto input)
        {
            input.MaxResultCount = int.MaxValue;

            var list = await _patientInfoService.GetPatientInformListByInputAsync(input);
            byte[] buffer = new ExcelHelper<PatientInformExportExcelDto>().ExportToExcel(_hostEnvironment,
                "告知记录列表",
                list.Data.Items.ToList(),
                new[]
                {
                    "TaskInfoNum",
                    "CarNum",
                    "PatientId",
                    "PatientName",
                    "Gender",
                    "Age",
                    "IdTypeName",
                    "IdentityNo",
                    "WarningLv",
                    "DiseaseIdentification",
                    "InformTime",
                    "Source"
                },
                new[] { "任务单流水号", "车牌号", "患者编号", "患者姓名", "性别", "年龄", "证件类型", "身份证号", "预警级别", "病种判断", "告知时间", "告知患者来源" });
            return File(buffer, "application/octet-stream", "告知记录列表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
    }
}