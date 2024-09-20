using System;
using System.Collections.Generic;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 微生物报告项列表
    /// </summary>
    public class LisMicroReportItemInfo
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
        /// 培养目的
        /// </summary>
        public string Aim { get; set; }

        /// <summary>
        /// 菌落数
        /// </summary>
        public string ColonyNum { get; set; }

        /// <summary>
        /// 试剂方法
        /// </summary>
        public string ReagentMethod { get; set; }

        /// <summary>
        /// 测试时间
        /// </summary>
        public DateTime? TestDate { get; set; }

        /// <summary>
        /// 测试方法
        /// </summary>
        public string TestMethod { get; set; }

        /// <summary>
        /// 原始结果
        /// </summary>
        public string ResultBac { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        public string TestResult { get; set; }

        /// <summary>
        /// 结果描述
        /// </summary>
        public string BacResultDesc { get; set; }

        /// <summary>
        /// 结果数值
        /// </summary>
        public string BacResultValue { get; set; }

        /// <summary>
        /// 附属结果
        /// </summary>
        public string AffiliatedResult { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// 评语
        /// </summary>
        public string CommentBac { get; set; }

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
        public string BacItemAbnormalFlag { get; set; }

        /// <summary>
        /// 达到危急值标记		0：未达到；1：达到
        /// </summary>
        public string BacEmergencyFlag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<LisBacAntiItemResponse> BacAntiItemList { get; set; }
    }


}