using System;

namespace YiJian.Health.Report.ReportDatas.Dto
{
    /// <summary>
    /// 报表数据创建Dto
    /// </summary>
    public class ReportDataCreateDto
    {
        /// <summary>
        /// 患者分诊id
        /// </summary>
        public Guid PIID { get; set; }

        /// <summary>
        /// 模板id
        /// </summary>
        public Guid TempId { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string PrescriptionNo { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public string DataContent { get;  set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        public string OperationCode { get;  set; }
    }
}