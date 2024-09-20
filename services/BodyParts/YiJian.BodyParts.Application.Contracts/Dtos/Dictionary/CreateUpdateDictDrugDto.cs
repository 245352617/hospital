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
    public class CreateUpdateDictDrugDto : EntityDto<Guid>
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
        public string DrugRefer { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
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
        /// 剂型
        /// </summary>
        /// <example></example>
        public string DrugForm { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        /// <example></example>
        public decimal? DosePerUnit { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        public string DoseUnit { get; set; }

        /// <summary>
        /// 是否高危药品
        /// </summary>
        [Required]
        public bool IsHighRisk { get; set; }
    }

}
