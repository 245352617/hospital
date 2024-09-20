using System;

namespace YiJian.Emrs.Dto
{
    /// <summary>
    /// 查询检查报告信息
    /// </summary>
    public class EmrPacsReportResponse
    {
        /// <summary>
        /// 检查时间 [展示]
        /// </summary>
        public DateTime? ParticipantTime { get; set; }

        /// <summary>
        /// 检查项目代码[展示]
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 检查项目名称[展示]
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 检查所见[展示]
        /// </summary>
        public string StudySee { get; set; }

        /// <summary>
        /// 检查提示 (检查结论)[展示]
        /// </summary>
        public string StudyHint { get; set; }

        /// <summary>
        /// 病人ID HIS的病人ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号 门诊号/住院号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 诊断代码
        /// </summary>
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 病史
        /// </summary>
        public string MedicalHistory { get; set; }

        /// <summary>
        /// 检查类型 	1:RIS放射;2:US超声; 3:ES内镜;4:病理PAT; 5:心电ECG;
        /// </summary>
        public string ExamType { get; set; }

        /// <summary>
        /// 检查部位
        /// </summary>
        public string ExamPart { get; set; }

        /// <summary>
        /// 检查部位描述
        /// </summary>
        public string ExamPartDesc { get; set; }

        /// <summary>
        /// 检查目的
        /// </summary>
        public string ExamPurpose { get; set; }

        /// <summary>
        /// 病情描述
        /// </summary>
        public string VisitStateDesc { get; set; }

        /// <summary>
        /// 报告单号
        /// </summary>
        public string ReportNo { get; set; }

        /// <summary>
        /// 报告标题
        /// </summary>
        public string ReportTitle { get; set; }

        /// <summary>
        /// 异常标志		0正常1异常
        /// </summary>
        public string AbnormalFlag { get; set; }
    }
}
