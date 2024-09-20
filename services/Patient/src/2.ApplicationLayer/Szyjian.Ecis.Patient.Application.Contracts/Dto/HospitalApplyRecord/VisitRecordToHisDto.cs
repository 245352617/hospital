using System;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 描述：就诊记录
    /// </summary>
    public class VisitRecordToHisDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊序号
        /// </summary>
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 就诊号码
        /// </summary>
        public string InvoiceNum { get; set; }

        /// <summary>
        /// 挂号序号
        /// </summary>
        public string RegisterNo { get; set; }

        /// <summary>
        /// 医生代码
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 开始时间
        /// 特别说明：开始就诊时该本段必填，北大医院有特别要求，结束就诊时该字段也必填
        /// </summary>
        public DateTime? StartVisitTime { get; set; }

        /// <summary>
        /// 结束时间：开始就诊时该字段给空，但结束就诊时该字段为必填
        /// </summary>
        public DateTime? EndVisitTime { get; set; }

        /// <summary>
        /// 就诊状态(1-诊中，2-挂起，9-就诊结束)
        /// </summary>
        public string VisitStatus { get; set; }

        /// <summary>
        /// 初诊判别（1-初诊,0-复诊）
        /// </summary>
        public string FirstVisit { get; set; }

        /// <summary>
        /// 病人去向(1-离院; 2-检验检查; 3-诊疗; 4-治疗; 15-重新分诊; 5-输液; 6-抢救; 7-留观; 8-EICU; 9-手术室; 10-介入室; 11-入院; 12-自动出院; 13-转院; 14-死亡;)
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// 就诊类型(1住院病人 2新生儿 3门诊病人 4急诊病人 5留观病人)
        /// </summary>
        public string VisitType { get; set; }

        /// <summary>
        /// 诊断编码
        /// </summary>
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        public string DiagnoseName { get; set; }
    }
}
