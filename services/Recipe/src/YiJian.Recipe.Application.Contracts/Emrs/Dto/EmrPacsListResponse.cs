using System;

namespace YiJian.Emrs.Dto
{
    /// <summary>
    /// 导入检查结果
    /// </summary>
    public class EmrPacsListResponse
    {
        /// <summary>
        /// 自动唯一ID
        /// </summary>
        public Guid Id
        {
            get
            {
                return Guid.NewGuid();
            }
        }

        /// <summary>
        /// 病人ID HIS的病人ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号 门诊号/住院号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 姓名 [需要展示]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 检查类型 	1:RIS放射;2:US超声; 3:ES内镜;4:病理PAT; 5:心电ECG;
        /// </summary>
        public string ExamType { get; set; }

        /// <summary>
        /// 申请单号[需要展示]
        /// </summary>
        public string ApplyNo { get; set; }

        /// <summary>
        /// 检查号
        /// </summary>
        public string StudyId { get; set; }

        /// <summary>
        /// 检查项目代码
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 检查项目名称[需要展示]
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 报告单号
        /// </summary>
        public string ReportNo { get; set; }

        /// <summary>
        /// 报告标题
        /// </summary>
        public string ReportTitle { get; set; }

        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime? ExamTime { get; set; }

        /// <summary>
        /// url
        /// </summary>
        public string Url { get; set; }
    }


}
