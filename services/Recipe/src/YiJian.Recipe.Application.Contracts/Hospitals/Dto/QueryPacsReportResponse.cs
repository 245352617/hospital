using System.Collections.Generic;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 查询检查报告信息
    /// </summary>
    public class QueryPacsReportResponse
    {
        /// <summary>
        /// 患者信息
        /// </summary>
        public PacsReportPatientInfoResponse InspectInfoRespToPatient { get; set; }

        /// <summary>
        /// 报表信息
        /// </summary>
        public List<PacsReportInfoResponse> ReportInfos { get; set; }

    }

    /// <summary>
    /// 查询检查报告信息
    /// </summary>
    public class GetPacsReportResponse
    {
        /// <summary>
        /// 患者信息
        /// </summary>
        public PacsReportPatientInfoResponse PatientInfo { get; set; }

        /// <summary>
        /// 报表信息
        /// </summary>
        public PacsReportInfoResponse ReportInfo { get; set; }

    }


}