using HisApiMockService.Models;
using HisApiMockService.Models.Advices;
using HisApiMockService.Models.Medicals;
using HisApiMockService.Models.Stores;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace HisApiMockService.Controllers;

/// <summary>
/// 龙岗中心医院接口
/// </summary>
[Route("api/ecis")]
public class LgzxHospitalController : Controller
{
    private readonly JsonFileUtil _jsonFile;
    private readonly ILogger _logger;
    private readonly SqliteDbContext _sqliteDbContext;

    public LgzxHospitalController(
        JsonFileUtil jsonFile,
        ILogger<JsonFileUtil> logger, SqliteDbContext sqliteDbContext)
    {
        _jsonFile = jsonFile;
        _logger = logger;
        _sqliteDbContext = sqliteDbContext;
    }

    /// <summary>
    /// 根据药品信息获取库存信息
    /// </summary>
    /// <param name="drugCode"></param>
    /// <returns></returns> 
    [HttpGet("DrugStock")]
    public async Task<BaseResponse<List<DrugStockQueryResponse>>> DrugStockAsync(DrugStockQueryRequest model)
    {
        Console.WriteLine(
            $"api/ecisDrugStock?queryType={model.QueryType}&queryCode={model.QueryCode}&storage={model.Storage}");

        var fileName = "drug_stock.json";
        var data = await _jsonFile.ReadAsync<IEnumerable<DrugStockQueryResponse>>(fileName);
        if (data == null) return new BaseResponse<List<DrugStockQueryResponse>>(new List<DrugStockQueryResponse>());
        var query = data.AsQueryable();
        if (model.QueryType == 0)
        {
            var retdata = data.Take(20).ToList();
            return await Task.FromResult(new BaseResponse<List<DrugStockQueryResponse>>(retdata));
        }

        if (model.QueryType == 1) query = query.Where(w => w.DrugName.Contains(model.QueryCode));
        if (model.QueryType == 2) query = query.Where(w => w.DrugCode == model.QueryCode);
        if (model.Storage > 0) query = query.Where(w => w.Storage == model.Storage);
        _sqliteDbContext.DrugStock.AddRange(query.ToList());
        await _sqliteDbContext.SaveChangesAsync();
        return await Task.FromResult(new BaseResponse<List<DrugStockQueryResponse>>(query.ToList()));
    }

    /// <summary>
    /// 诊断、就诊记录、医嘱状态变更
    /// <![CDATA[
    /// 备注：诊断、医嘱作废时使用；就诊记录回传结束就诊日期、标志等
    /// ]]> 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    [HttpPost("UpdateRecordStatus")]
    public async Task<BaseResponse<object>> UpdateRecordStatusAsync([FromBody] UpdateRecordStatusRequest model)
    {
        var input = Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented);
        Console.WriteLine("诊断、就诊记录、医嘱状态变更 请求参数:\n" + input);

        foreach (var item in model.Recordinfo)
        {
            var content = new
            {
                model.RecordType,
                model.VisSerialNo,
                model.PatientId,
                model.OperatorCode,
                model.DeptCode,
                item.OperatorDate,
                item.RecordState,
                item.RecordNo,
                item.RecordDetailNo
            };
            _ = await _jsonFile.AppendFileAsync("record_status.txt",
                Newtonsoft.Json.JsonConvert.SerializeObject(content, Newtonsoft.Json.Formatting.None));
        }
        _sqliteDbContext.RecordStatusRequest.Add(model);
        await _sqliteDbContext.SaveChangesAsync();
        return await Task.FromResult(new BaseResponse<object>(true));
    }

    /// <summary>
    /// 医嘱信息回传
    /// </summary>
    /// <returns></returns> 
    [HttpPost("SendMedicalInfo")]
    public async Task<BaseResponse<object>> SendMedicalInfoAsync([FromBody] SendMedicalInfoRequest model)
    {
        var input = Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented);
        Console.WriteLine("医嘱信息回传 请求参数:\n" + input);
        _ = await _jsonFile.AppendFileAsync("medicalinfo.txt",
            Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.None));
        _sqliteDbContext.SendMedicalInfo.Add(model);
        await _sqliteDbContext.SaveChangesAsync();
        return await Task.FromResult(new BaseResponse<object>(true));
    }

    /// <summary>
    /// 医嘱状态查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet("QueryMedicalInfo")]
    public async Task<BaseResponse<QueryMedicalInfoResponse>> QueryMedicalInfoAsync(QueryMedicalInfoRequest model)
    {
        Console.WriteLine(
            $"queryMedicalInfo in: {Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented)}");

        var data = new QueryMedicalInfoResponse
        {
            BillState = 1,
            ChannelBillId = "10001",
            DeptCode = "100",
            DoctorCode = "200",
            HisBillId = model.MzBillId,
            PatientName = "400",
            VisSerialNo = model.VisSerialNo
        };
        _sqliteDbContext.Add(data);
        await _sqliteDbContext.SaveChangesAsync();
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented));
        return await Task.FromResult(new BaseResponse<QueryMedicalInfoResponse>(data));
    }


    #region 检查业务

    /// <summary>
    /// 查询检查报告列表
    /// </summary>
    /// <returns></returns>
    [HttpPost("getPacsReportList")]
    public async Task<BaseResponse<QueryPacsReportListResponse>> QueryPacsReportListAsync(
        [FromBody] QueryPacsReportListRequest model)
    {
        Console.WriteLine(
            $"QueryPacsReportListAsync请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented)}");
        var fileName = "pacs_report.json";
        var data = await _jsonFile.ReadAsync<List<QueryPacsReportListResponse>>(fileName);
        if (data == null || data.Count() == 0)
            return new BaseResponse<QueryPacsReportListResponse>(new QueryPacsReportListResponse());

        Random random = new Random();
        var index = random.Next(data.Count());
        return new BaseResponse<QueryPacsReportListResponse>(data[index]);
    }

    /// <summary>
    /// 查询检查报告信息
    /// </summary>
    /// <returns></returns> 
    [HttpPost("getPacsReport")]
    public async Task<BaseResponse<QueryPacsReportResponse>> QueryPacsReportAsync(
        [FromBody] QueryPacsReportRequest model)
    {
        Console.WriteLine(
            $"QueryPacsReportAsync请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented)}");
        var fileName = "pacs.report_detail.json";
        var data = await _jsonFile.ReadAsync<List<QueryPacsReportResponse>>(fileName);
        if (data == null || data.Count() == 0)
            return new BaseResponse<QueryPacsReportResponse>(new QueryPacsReportResponse());
        Random random = new Random();
        var index = random.Next(data.Count());
        return new BaseResponse<QueryPacsReportResponse>(data[index]);
    }

    #endregion


    #region 检验业务

    /// <summary>
    /// 检验报告列表查询
    /// </summary>
    /// <returns></returns> 
    [HttpPost("getExamineList")]
    public async Task<BaseResponse<List<GetLisReportListResponse>>> GetLisReportListAsync(
        [FromBody] GetLisReportListRequest model)
    {
        Console.WriteLine(
            $"GetLisReportListAsync请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented)}");
        var fileName = "lis_report.json";
        var data = await _jsonFile.ReadAsync<List<GetLisReportListResponse>>(fileName);
        if (data == null || data.Count() == 0)
            return new BaseResponse<List<GetLisReportListResponse>>(new List<GetLisReportListResponse>());

        Random random = new Random();
        var index = random.Next(data.Count() - 10);
        var list = data.Skip(index).Take(10).ToList();
        return new BaseResponse<List<GetLisReportListResponse>>(list);
    }

    /// <summary>
    /// 检验报告详情查询
    /// </summary>
    /// <returns></returns> 
    [HttpPost("getExamineInfo")]
    public async Task<BaseResponse<GetLisReportResponse>> GetLisReportAsync([FromBody] GetLisReportRequest model)
    {
        Console.WriteLine(
            $"GetLisReportAsync请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented)}");
        var fileName = "lis_report_detail.json";
        var data = await _jsonFile.ReadAsync<List<GetLisReportResponse>>(fileName);
        if (data == null || data.Count() == 0)
            return new BaseResponse<GetLisReportResponse>(new GetLisReportResponse());
        Random random = new Random();
        var index = random.Next(data.Count());
        var retData = data[index];
        return new BaseResponse<GetLisReportResponse>(retData);
    }

    #endregion

    /// <summary>
    /// 生成一份检验报告详情数据(mock数据)
    /// </summary>
    /// <returns></returns> 
    [HttpGet("create-lis-report-data-detail")]
    public async Task<string> CreateLisReportDetailDataAsync()
    {
        await Task.CompletedTask;

        var fileName = "lis_report.json";
        var list = await _jsonFile.ReadAsync<List<GetLisReportListResponse>>(fileName);
        var data = new List<GetLisReportResponse>();
        var count = 1;

        Random random = new Random();

        foreach (var item in list)
        {
            var applyinfo = new List<ListApplyInfoResponse>();
            item.ApplyInfoList.ForEach(x =>
            {
                applyinfo.Add(new ListApplyInfoResponse
                {
                    ApplyDeptCode = x.ApplyDeptCode,
                    ApplyDeptName = x.ApplyDeptName,
                    ApplyNo = x.ApplyNo,
                    ApplyOperatorCode = x.ApplyOperatorCode,
                    ApplyOperatorName = x.ApplyOperatorName,
                    ApplyTime = x.ApplyTime.HasValue
                        ? x.ApplyTime.Value.ToString("yyyy-MM-dd HH:mm:dd")
                        : DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd"),
                    VisitDeptCode = x.VisitDeptCode,
                    VisitDeptName = x.VisitDeptName,
                    MasterItems = x.MasterItemList
                });
            });

            Random random2 = new Random();
            var num = random2.Next(1, 10);
            var reportItemList = new List<LisReportItemInfo>();
            for (int i = 0; i < num; i++)
            {
                //N正常;L偏低;H偏高
                var irl = "NLH";
                Random r = new Random();
                var index = r.Next(0, 3);
                var itemResultFlag = irl.Substring(index, 1);

                Random rlow = new Random();
                Random rhigh = new Random();

                var low = rlow.Next(0, 200);
                var high = rhigh.Next(0, 200);
                if (low > high)
                {
                    var tmp = low;
                    low = high;
                    high = tmp;
                }

                var units = "mg/L;umol/L;umol/L;umol/L;g/L";
                var arr = units.Split(";");
                Random runit = new Random();
                var uindex = runit.Next(0, arr.Length);
                string itemResultUnit = arr[uindex];

                reportItemList.Add(new LisReportItemInfo
                {
                    Comment = "",
                    CommentDocCode = "",
                    CommentDocName = "",
                    EmergencyFlag = "",
                    ItemAbnormalFlag = "",
                    ItemChiName = $"新型冠状病毒核酸.{i}",
                    ItemCode = "ORF lab",
                    ItemLoincCode = "",
                    ItemLoincName = "",
                    ItemNo = $"{count.ToString("0000")}{i.ToString("00")}",
                    ItemResult = "阴性",
                    ItemResultFlag = itemResultFlag,
                    ItemResultUnit = itemResultUnit,
                    Notes = "",
                    ReagentMethod = $"实时荧光PCR法.{count}.{i}",
                    ReferenceDesc = "阴性",
                    ReferenceHighLimit = $"{low}",
                    ReferenceLowLimit = $"{high}",
                    Remark = ""
                });
            }

            data.Add(new GetLisReportResponse
            {
                applyInfoList = applyinfo,
                AuditOperator = "admin",
                AuditOperatorCode = "1000",
                AuditTime = DateTime.Now,
                BarcodeNo = item.BarcodeNo,
                BedNo = $"BED{item.VisitNo}",
                SafetyNo = count.ToString("000000"),
                SpecimenAcceptOperator = "王辉",
                SpecimenAcceptOperatorCode = "",
                SpecimenAcceptTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ChargeType = "",
                DeptCode = "1032",
                DeptName = "肾内科住院",
                DiagnoseCode = "",
                DiagnoseName = "",
                EmergencyFlag = "",
                Gender = (random.Next(2) / 2 == 0) ? "1" : "2",
                IcCardNo = "",
                LabDept = "",
                LabOperator = "余卓恒",
                LabInstrument = "AB7500",
                LabInstrumentName = "ABI 7500荧光定量PCR仪",
                LabMethod = "",
                LabPurpose = "",
                LabTime = DateTime.Now,
                LisNo = item.ReportNo,
                Name = "李桂英",
                PatientCondition = "",
                PatientId = $"337{count.ToString("0000")}",
                PatientType = EPatientType.EmergencyDepartment,
                PrintOperator = "",
                PrintOperatorCode = "",
                PrintTime = null,
                ReportNo = item.ReportNo,
                ReportOperator = "王辉",
                ReportOperatorCode = "",
                ReportTemplateCode = "药敏报告",
                ReportTime = DateTime.Now.ToString(),
                ReportTitle = "药敏报告-报告标题",
                ReportType = EReportType.Common,
                ReportUrl = "",
                SpecimenCode = "60",
                SpecimenName = "分泌物",
                SpecimenCollectOperator = "丘友谊",
                SpecimenCollectOperatorCode = "",
                SpecimenCollectPart = "",
                SpecimenCollectPartDesc = "",
                SpecimenCollectTime = DateTime.Now.ToString(),
                SpecimenQuality = "",
                SpecimenQualityDesc = "",
                SpecimenRiskFactor = "",
                SpecimenRiskFactorDesc = "",
                SubjectClass = "细菌",
                VisitNo = $"8355{count.ToString("0000")}",
                VisitNumber = count.ToString(),
                VisitSerialNo = (count * 2).ToString("1810000"),
                ReportItemList = reportItemList
            });
        }

        await _jsonFile.WriteFileAsync("lis_report_detail.json",
            JsonConvert.SerializeObject(data, Formatting.Indented));

        return "ok";
    }
}