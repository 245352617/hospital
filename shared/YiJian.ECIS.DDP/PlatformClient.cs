using BeetleX.Http.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.DDPs;
using YiJian.ECIS.ShareModel.WebApiClient.Model;

namespace YiJian.ECIS.DDP;

/// <summary>
///  API 客户端
/// </summary>
public interface PlatformClient
{
    /// <summary>
    /// 根据患者id 获取报告列表
    /// </summary>
    /// <param name="patientId"></param>
    /// <returns></returns>
    [Get(Route = "socket/inspect/queryInspectForm?patientId={patientId}&patientName={patientName}")]
    Task<DdpBaseResponse<List<GetLisReportListResponse>>> GetQueryInspectFormAsync(string patientId, string patientName);

    /// <summary>
    /// 根据报告的OrderNum 获取报告详情
    /// </summary>
    /// <param name="placerOrderNum"></param>
    /// <returns></returns>
    [Get(Route = "socket/inspect/queryInspectionReport?placerOrderNum={placerOrderNum}")]
    Task<DdpBaseResponse<List<GetLisReportResponse>>> GetQueryInspectReportAsync(string placerOrderNum);

}
