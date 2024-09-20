using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class CreateLevelTriageRelationDirectionDto
    {
        /// <summary>
        /// 分诊去向级别代码
        /// </summary>
        public string LevelTriageDirectionCode { get; set; }
        /// <summary>
        /// 分诊级别代码  字典表获取
        /// </summary>
        public string TriageLevelCode { get; set; }
        /// <summary>
        /// 分诊去向code
        /// </summary>
        public string TriageDirectionCode { get; set; }

        /// <summary>
        /// 其他去向code
        /// </summary>
        public string OtherDirectionCode { get; set; }
    }
}
