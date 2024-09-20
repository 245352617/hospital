using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Model
{
    /// <summary>
    /// 入科评估字典
    /// </summary>
    public class DictAssessDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 评估代码
        /// </summary>
        public string Pcode { get; set; }

        /// <summary>
        /// 评估名称
        /// </summary>
        public string Pname { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 是否评分
        /// </summary>
        public bool? GuidFlag { get; set; }

        /// <summary>
        /// 评分名称
        /// </summary>
        public string GuidId { get; set; }

        /// <summary>
        /// 类型（单选框1，复选框2）
        /// </summary>

        public int? Style { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 是否有子项目
        /// </summary>
        public int? IsSubItem { get; set; }

        /// <summary>
        /// 是否带单文本框
        /// </summary>
        public int? SingleText { get; set; }

        /// <summary>
        /// 是否带复文本框
        /// </summary>
        public int? RichEdit { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public int? IsShow { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public int? IsRequired { get; set; }

        /// <summary>
        /// 知识库代码
        /// </summary>
        public string KBCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        public List<DictAssessChildren> children { get; set; }
    }

    public class DictAssessChildren
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 评估代码
        /// </summary>
        public string Pcode { get; set; }

        /// <summary>
        /// 评估名称
        /// </summary>
        public string Pname { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 是否带单文本框
        /// </summary>
        public int? SingleText { get; set; }

        /// <summary>
        /// 是否带复文本框
        /// </summary>
        public int? RichEdit { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
    }
}
