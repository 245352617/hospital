using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 停嘱
    /// </summary>
    public class StopDoctorsAdviceDto
    {
        public List<Guid> Ids { get; set; }

        /// <summary>
        /// 医生/护士编码
        /// </summary>
        [Required(ErrorMessage = "无操作人编码")]
        public string OperatorCode { get; set; }

        /// <summary>
        /// 医生/护士名称
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 医生选择的停嘱时间
        /// </summary>
        public DateTime StopTime { get; set; }

        /// <summary>
        /// 科室编码(医生的可是信息) 
        /// </summary> 
        [Required(ErrorMessage = "无科室编码")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary> 
        [Required(ErrorMessage = "无病人ID")]
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary> 
        [Required(ErrorMessage = "无传就诊流水号")]
        public string VisSerialNo { get; set; }

    }
}
