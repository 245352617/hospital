using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class ReportSettingListDto:EntityDto<Guid>
    {

        /// <summary>
        /// 报表类型名称
        /// </summary>
        public string ReportTypeName { get; set; }

        /// <summary>
        /// 报表类型Code
        /// </summary>
        public string ReportTypeCode { get; set; }
        
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 报表集合
        /// </summary>
        public ICollection<ReportSettingDto> ReportSettingDto { get; set; }
    }
}
