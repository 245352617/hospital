namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 正常报告项列表
    /// </summary>
    public class LisReportItemInfo
    {
        /// <summary>
        /// 细项序号
        /// </summary>
        public string ItemNo { get; set; }

        /// <summary>
        /// 细项代码
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 细项名称 [指标名称]
        /// </summary>
        public string ItemChiName { get; set; }

        /// <summary>
        /// LOINC编码
        /// </summary>
        public string ItemLoincCode { get; set; }

        /// <summary>
        /// 对应英文名称
        /// </summary>
        public string ItemLoincName { get; set; }

        /// <summary>
        /// 结果值
        /// </summary>
        public string ItemResult { get; set; }

        /// <summary>
        /// 结果单位
        /// </summary>
        public string ItemResultUnit { get; set; }

        /// <summary>
        /// 结果值标志 N正常;L偏低;H偏高
        /// </summary>
        public string ItemResultFlag { get; set; }

        /// <summary>
        /// 参考值描述
        /// </summary>
        public string ReferenceDesc { get; set; }

        /// <summary>
        /// 参考值上限
        /// </summary>
        public string ReferenceHighLimit { get; set; }

        /// <summary>
        /// 参考值下限
        /// </summary>
        public string ReferenceLowLimit { get; set; }

        /// <summary>
        /// 试剂方法
        /// </summary>
        public string ReagentMethod { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// 评语
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 评语医生编码
        /// </summary>
        public string CommentDocCode { get; set; }

        /// <summary>
        /// 评语医生名称
        /// </summary>
        public string CommentDocName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 当前结果正常异常标记		1正常2异常
        /// </summary>
        public string ItemAbnormalFlag { get; set; }

        /// <summary>
        /// 达到危急值标记		0：未达到；1：达到
        /// </summary>
        public string EmergencyFlag { get; set; }

    }


}