using System;
using System.Collections.Generic;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 描述：保存诊断数据(急诊) API/Treatment/SaveDiagsJZ
    /// 创建人： yangkai
    /// 创建时间：2023/3/1 11:13:21
    /// </summary>
    internal class SaveDiagsDto
    {
        /// <summary>
        /// 就诊号码
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 挂号序号
        /// </summary>
        public string RegisterNo { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNum { get; set; }

        /// <summary>
        /// 就诊序号
        /// </summary>
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 病人id
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 基本字段
        /// </summary>
        public List<DiagnoseDto> DiagnoseDtos { get; set; }
    }

    internal class DiagnoseDto
    {
        /// <summary>
        /// 疾病编号(诊断代码表主键)
        /// </summary>
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断类别(1-门诊初步诊断,2-复诊诊断)
        /// </summary>
        public string DiagnoseType { get; set; }

        /// <summary>
        /// 医生代码
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 诊断时间
        /// </summary>
        public DateTime DiagnoseTime { get; set; }

        /// <summary>
        /// 疾病名称
        /// </summary>
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 描述诊断
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// icd编码
        /// </summary>
        public string ICD { get; set; }

        /// <summary>
        /// 疾病组号(从2开始，每一条数据+1)
        /// </summary>
        public string DiseaseNo { get; set; }

        /// <summary>
        /// 作废判别(1-作废,0-正常)
        /// </summary>
        public string IsDelete { get; set; }
    }
}
