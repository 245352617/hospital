using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Application.Contracts.Dtos.QualityControl
{
    /// <summary>
    /// 质控15项中间表对应导管dto类
    /// </summary>
    public class CanulasDto
    {
        /// <summary>
        /// 护理时间
        /// </summary>
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 导管分类
        /// </summary>

        public string ModuleCode { get; set; }

        /// <summary>
        /// 管道名称
        /// </summary>

        public string CanulaName { get; set; }


        /// <summary>
        /// 入科号
        /// </summary>
        public string PI_ID { get; set; }
    }
}
