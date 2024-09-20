using System;
using Volo.Abp.Application.Dtos;

namespace YiJian.Health.Report.ReportDatas.Dto
{
    /// <summary>
    /// 报表数据Dto
    /// </summary>
    public class ReportDataDto : EntityDto<Guid>
    {
        /// <summary>
        /// 患者分诊id
        /// </summary>
        public Guid PIID { get; set; }

        /// <summary>
        /// 模板id
        /// </summary>
        public Guid TempId { get; set; }
    }
    
}