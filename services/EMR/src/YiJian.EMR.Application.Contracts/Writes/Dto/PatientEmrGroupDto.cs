using System;
using System.Collections.Generic;

namespace YiJian.EMR.Writes.Dto
{
    /// <summary>
    /// 根据入院出院分组记录
    /// </summary>
    public class PatientEmrGroupDto
    {
        /// <summary>
        /// 入院时间
        /// </summary> 
        public DateTime AdmissionTime { get; set; }

        /// <summary>
        /// 患者唯一Id
        /// </summary>
        public Guid Piid { get; set; }

        /// <summary>
        /// 患者电子病历信息
        /// </summary>
        public List<PatientEmrDto> PatientEmrs { get; set; } 

    } 
}
