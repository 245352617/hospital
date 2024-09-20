using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Application.Contracts.Dtos.QualityControl
{
    /// <summary>
    /// 导管记录Dto
    /// </summary>
    public class NursingCanulasDto
    {
        /// <summary>
        /// 导管分类
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 导管名称
        /// </summary>
        /// <example></example>
        public string ModuleName { get; set; }

        /// <summary>
        /// 管道名称
        /// </summary>

        public string CanulaName { get; set; }

        /// <summary>
        /// 插管时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 拔管时间
        /// </summary>
        public DateTime? StopTime { get; set; }



    }
}
