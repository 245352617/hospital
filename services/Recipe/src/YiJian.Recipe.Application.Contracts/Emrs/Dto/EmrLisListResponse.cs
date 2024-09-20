using System;
using YiJian.Hospitals.Enums;

namespace YiJian.Emrs.Dto
{
    /// <summary>
    /// 导入检验结果
    /// </summary>
    public class EmrLisListResponse
    {
        /// <summary>
        /// 检验时间 [展示]（展示格式化由前端做）
        /// </summary>
        public DateTime? LabTime { get; set; }

        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 病人类别
        /// </summary>
        public EPatientType PatientType { get; set; }

        /// <summary>
        /// 就诊号  门诊挂号号/住院号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊流水号  就诊唯一号
        /// </summary>
        public string VisitSerialNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 报告单号
        /// </summary>
        public string ReportNo { get; set; }

        /// <summary>
        /// 申请单号  多个申请单用分号隔开；
        /// </summary>
        public string ApplyNo { get; set; }

        /// <summary>
        /// 检验内部ID
        /// </summary>
        public string MasterItemId { get; set; }

        /// <summary>
        /// 检验项目序号
        /// </summary>
        public string MasterItemNo { get; set; }

        /// <summary>
        /// 检验项目代码
        /// </summary>
        public string MasterItemCode { get; set; }

        /// <summary>
        /// 检验项目名称[展示]
        /// </summary>
        public string MasterItemName { get; set; }

    }
}
