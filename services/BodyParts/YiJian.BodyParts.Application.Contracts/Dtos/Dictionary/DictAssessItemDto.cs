using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class DictAssessItemDto
    {
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
        /// 类型（单选框1，复选框2，文本框3）
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
        /// 是否显示
        /// </summary>
        public int? IsShow { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public int? IsRequired { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        public List<DictAssessItemChildren> children { get; set; }
    }
    public class DictAssessItemChildren
    {
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
        /// 类型（单选框1，复选框2，文本框3）
        /// </summary>
        public int? Style { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public int? IsShow { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public int? IsRequired { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
    }
}
