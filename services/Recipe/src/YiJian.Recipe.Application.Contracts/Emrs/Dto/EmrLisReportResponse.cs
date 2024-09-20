using System;
using System.Collections.Generic;


namespace YiJian.Emrs.Dto
{
    /// <summary>
    /// 检验报告详情查询
    /// </summary>
    public class EmrLisResponse
    {
        /// <summary>
        /// 自动Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 检查号
        /// </summary>
        public string LisNo { get; set; }

        /// <summary>
        /// 病人ID 	内部唯一号
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime? LabTime { get; set; }

        /// <summary>
        /// 细项序号
        /// </summary>
        public string ItemNo { get; set; }

        /// <summary>
        /// 细项代码
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 细项名称 [指标名称,展示]
        /// </summary>
        public string ItemChiName { get; set; }

        /// <summary>
        /// 结果值 [数值,展示]
        /// </summary>
        public string ItemResult { get; set; }

        /// <summary>
        /// 结果单位 [单位]
        /// </summary>
        public string ItemResultUnit { get; set; }

        /// <summary>
        /// 结果值标志 N正常;L偏低;H偏高 [状态]
        /// </summary>
        public string ItemResultFlag { get; set; }

        /// <summary>
        /// 参考值描述
        /// </summary>
        public string ReferenceDesc { get; set; }

        /// <summary>
        /// 参考值上限 [参考范围max]
        /// </summary>
        public string ReferenceHighLimit { get; set; }

        /// <summary>
        /// 参考值下限[参考范围min]
        /// </summary>
        public string ReferenceLowLimit { get; set; }

        /// <summary>
        /// 参考范围[上限-下限，显示]
        /// </summary>
        public string ReferenceRange
        {
            get
            {
                if (ReferenceHighLimit.IsNullOrEmpty() && ReferenceLowLimit.IsNullOrEmpty()) return "";
                if (ReferenceHighLimit.IsNullOrEmpty() && !ReferenceLowLimit.IsNullOrEmpty()) return ReferenceLowLimit;
                if (!ReferenceHighLimit.IsNullOrEmpty() && ReferenceLowLimit.IsNullOrEmpty()) return ReferenceHighLimit;
                return $"{ReferenceHighLimit}-{ReferenceLowLimit}";
            }
        }

        /// <summary>
        /// 当前结果正常异常标记 1正常2异常
        /// </summary>
        public string ItemAbnormalFlag { get; set; }


    }

}
