using System;

namespace YiJian.BodyParts.Application.Contracts.Dtos.QualityControl
{

    public class PT_QcAntibacterialInfo
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PatId { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string DrugName { get; set; }

        /// <summary>
        /// 是否送检
        /// </summary>
        public string IsInspect { get; set; }
        
        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime? UseTime { get; set; }
    }
}