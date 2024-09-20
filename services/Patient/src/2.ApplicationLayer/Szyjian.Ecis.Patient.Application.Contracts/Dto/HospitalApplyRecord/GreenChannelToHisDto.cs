using Newtonsoft.Json;
using System.Collections.Generic;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 描述：绿色通道
    /// </summary>
    public class GreenChannelToHisDto
    {
        /// <summary>
        /// 就诊序号
        /// </summary>
        [JsonProperty("visitNo")]
        public string VisitNo { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        [JsonProperty("doctorName")]
        public string DoctorName { get; set; }

        /// <summary>
        /// 医生代码
        /// </summary>
        [JsonProperty("doctorCode")]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        [JsonProperty("gender")]
        public string Gender { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        [JsonProperty("patientId")]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>
        [JsonProperty("patientName")]
        public string PatientName { get; set; }

        /// <summary>
        /// 病人类型（1-日间手术，3-绿色通道，4-eras通道）
        /// </summary>
        [JsonProperty("greenChannelType")]
        public string GreenChannelType { get; set; }

        /// <summary>
        /// 就诊序号
        /// </summary>
        [JsonProperty("visSerialNo")]
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        [JsonProperty("deptCode")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 挂号序号
        /// </summary>
        [JsonProperty("registerNo")]
        public string RegisterNo { get; set; }

        ///// <summary>
        ///// 入室时间
        ///// </summary>
        //[JsonProperty("inDeptTime")]
        //public DateTime? InDeptTime { get; set; }

        /// <summary>
        /// 作废判别(1-作废,0-正常)
        /// </summary>
        [JsonProperty("operationType")]
        public string OperationType { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        [JsonProperty("diagnose")]
        public List<DiagnoseList> DiagnoseList { get; set; }

    }

    public class DiagnoseList
    {
        /// <summary>
        /// 诊断代码
        /// </summary>
        [JsonProperty("diagnoseCode")]
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        [JsonProperty("diagnoseName")]
        public string DiagnoseName { get; set; }
    }
}
