using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class IndeptAssessDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 评估时间
        /// </summary>
        public DateTime AssessTime { get; set; }

        /// <summary>
        /// 护士编号
        /// </summary>
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 提示内容
        /// </summary>
        public string[] PromptContent { get; set; }

        /// <summary>
        /// 护理问题
        /// </summary>
        public List<NursingProblem> NursingProblem { get; set; }

        public List<NavigationList> navigations { get; set; }
    }

    /// <summary>
    /// 评估返回参数
    /// </summary>
    public class NavigationList
    {
        public string Pcode { get; set; }

        public string Pname { get; set; }

        public List<SelectAssessDto> SelectAssessDto { get; set; }
    }

    public class SelectAssessDto
    {
        public string Pcode { get; set; }

        public string Pname { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public int? IsRequired { get; set; }

        /// <summary>
        /// 是否评分
        /// </summary>
        public bool? GuidFlag { get; set; }

        /// <summary>
        /// 评分名称
        /// </summary>
        public string GuidId { get; set; }

        /// <summary>
        /// 是否有子项目
        /// </summary>
        public int? IsSubItem { get; set; }

        /// <summary>
        /// 类型（单选框1，复选框2）
        /// </summary>
        public int? Style { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }

        public string Pvalue { get; set; }

        /// <summary>
        /// 文本值
        /// </summary>
        public string Tvalue { get; set; }

        /// <summary>
        /// 评分Id
        /// </summary>
        public string ScoreId { get; set; }

        public List<AssessDto> AssessDto { get; set; }
    }

    public class AssessDto
    {
        public string Pcode { get; set; }

        public string Pname { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public int? IsRequired { get; set; }

        /// <summary>
        /// 是否带单文本框
        /// </summary>
        public int? SingleText { get; set; }

        /// <summary>
        /// 是否带复文本框
        /// </summary>
        public int? RichEdit { get; set; }

        /// <summary>
        /// 类型（单选框1，复选框2）
        /// </summary>
        public int? Style { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }

        public string Pvalue { get; set; }

        /// <summary>
        /// 文本值
        /// </summary>
        public string Tvalue { get; set; }

        /// <summary>
        /// 是否评分
        /// </summary>
        public bool? GuidFlag { get; set; }

        /// <summary>
        /// 评分名称
        /// </summary>
        public string GuidId { get; set; }

        /// <summary>
        /// 评分Id
        /// </summary>
        public string ScoreId { get; set; }

        public List<AssessDetailDto> AssessDetailDto { get; set; }

    }

    public class AssessDetailDto
    {
        public string Pcode { get; set; }

        public string Pname { get; set; }

        /// <summary>
        /// 是否带单文本框
        /// </summary>
        public int? SingleText { get; set; }

        /// <summary>
        /// 是否带复文本框
        /// </summary>
        public int? RichEdit { get; set; }
    }
}
