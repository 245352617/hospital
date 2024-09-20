using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 创建护理措施Dto
    /// </summary>
    public class CreateNursingMeasure : EntityDto<Guid>
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 护理问题Id
        /// </summary>
        public string NursingProblemId { get; set; }

        /// <summary>
        /// 护理问题
        /// </summary>
        public string NursingProblem { get; set; }
        /// <summary>
        /// 相关因素
        /// </summary>
        public string RelatedFactors { get; set; }
        /// <summary>
        /// 护理目标
        /// </summary>
        public string NursingGoal { get; set; }
        /// <summary>
        /// 护理措施
        /// </summary>
        public string NursingContext { get; set; }
        /// <summary>
        /// 开立人
        /// </summary>
        public string NurseCode { get; set; }
        /// <summary>
        /// 开立人名称
        /// </summary>
        public string NurseName { get; set; }
        /// <summary>
        /// 开立时间
        /// </summary>
        public DateTime NurseTime { get; set; }
    }
}
