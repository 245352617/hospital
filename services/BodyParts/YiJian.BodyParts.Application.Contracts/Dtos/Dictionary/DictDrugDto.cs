using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:字典-药品
    /// </summary>
    public class DictDrugDto : EntityDto<Guid>
    {
        /// <summary>
        /// 药品代码
        /// </summary>
        /// <example></example>
        public string DrugCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        /// <example></example>
        public string DrugName { get; set; }

        /// <summary>
        /// 药品简称
        /// </summary>
        /// <example></example>
        public string DrugRefer { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        /// <example></example>
        public string PinYin { get; set; }

        /// <summary>
        /// 药品分类
        /// </summary>
        /// <example></example>
        public string ClassCode { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 药品性质
        /// </summary>
        public string DrugQuality { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        /// <example></example>
        public string DrugSpec { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        /// <example></example>
        public string DrugUnit { get; set; }

        /// <summary>
        /// 最小包装单位
        /// </summary>
        /// <example></example>
        public string MinUnit { get; set; }

        /// <summary>
        /// 剂型编码
        /// </summary>
        /// <example></example>
        public string DrugForm { get; set; }

        /// <summary>
        /// 剂型
        /// </summary>
        /// <example></example>
        public string DrugFormName { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        /// <example></example>
        public string Property { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        /// <example></example>
        public decimal? DosePerUnit { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        /// <example></example>
        public string DoseUnit { get; set; }

        /// <summary>
        /// 用法编码
        /// </summary>
        public string UsageCode { get; set; }

        /// <summary>
        /// 频次编码
        /// </summary>
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 一次用量
        /// </summary>
        public string OnceDose { get; set; }
        /// <summary>
        /// 是否高危药品
        /// </summary>
        /// <example></example>

        public bool IsHighRisk { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        /// <example></example>

        public DateTime? RecordTime { get; set; }

        /// <summary>
        /// 有效状态（1-有效，0-无效）
        /// </summary>
        /// <example></example>
        public int ValidState { get; set; }


    }
}
