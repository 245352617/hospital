using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing
{
    public class TemperatureDto
    {
        /// <summary>
        /// 床号
        /// </summary>
        public string BedNum { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }
        
        /// <summary>
        /// 病人姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNum { get; set; }

        /// <summary>
        /// 对应数据
        /// </summary>
        public List<NursingTemperatureDto> NurseItems { get; set; }
    }
}
