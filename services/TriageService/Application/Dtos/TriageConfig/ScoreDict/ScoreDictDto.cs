using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 评分字典Dto
    /// </summary>
    public class ScoreDictDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 评分类型
        /// </summary>
        [MaxLength(50,ErrorMessage = "评分类型的最大长度为{1}")]
        public string Category { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [MaxLength(200,ErrorMessage = "显示名称的最大长度为{1}")]
        public string DisplayText { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 评分标题级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        
        /// <summary>
        /// 是否启用，0：否，1：是
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 评分选项样式，1：拖动轴，2：单选按钮（文本在按钮中，选中时呈现白字绿底），3：单选按钮（禁用启用类），4：下拉框且后面带文本框输入分数后自动选中该分数选项
        /// </summary>
        public int OptionStyle { get; set; }

        /// <summary>
        /// Icon Url
        /// </summary>
        [MaxLength(200,ErrorMessage = "Icon Url 的最大长度为{1}")]
        public string IconUrl { get; set; }

        /// <summary>
        /// 关联选项
        /// </summary>
        public string AssociatedOptions { get; set; }

        /// <summary>
        /// 备注文本样式，1：纯文本，2：Html
        /// </summary>
        public int RemarkStyle { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public Guid? DefaultValue { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        public List<ScoreDictDto> Children { get; set; }
    }
}