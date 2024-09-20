using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YiJian.Hospitals.Dto
{

    /// <summary>
    /// 诊断、就诊记录、医嘱状态变更
    /// </summary>
    public class UpdateRecordStatusRequest
    {
        /// <summary>
        /// 就诊流水号
        /// </summary>
        [JsonProperty("visSerialNo")]
        [Required]
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
        [JsonProperty("patientId")]
        [Required]
        public string PatientId { get; set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        [JsonProperty("operatorCode")]
        [Required]
        public string OperatorCode { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        [JsonProperty("deptCode")]
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 记录信息
        /// </summary>
        [JsonProperty("recordinfo")]
        public List<Recordinfo> Recordinfo { get; set; }

    }
}
