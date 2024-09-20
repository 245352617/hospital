using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 评分字典
    /// </summary>
    public class ScoreDict : Entity<Guid>,ISoftDelete
    {
        /// <summary>
        /// 评分类型
        /// </summary>
        [Description("评分类型")]
        [MaxLength(50,ErrorMessage = "评分类型的最大长度为{1}")]
        public string Category { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Description("显示名称")]
        [MaxLength(200,ErrorMessage = "显示名称的最大长度为{1}")]
        public string DisplayText { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        [Description("分数")]
        public int Grade { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        [Description("父级Id")]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 评分标题级别
        /// </summary>
        [Description("评分标题级别")]
        public int Level { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Description("排序号")]
        public int Sort { get; set; }

        /// <summary>
        /// 备注文本
        /// </summary>
        [Description("备注文本")]
        public string Remark { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Description("是否删除")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 是否启用，0：否，1：是
        /// </summary>
        [Description("是否启用，0：否，1：是")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 评分选项样式，-1：非选项纯文本，1：拖动轴，2：单选按钮（文本在按钮中，选中时呈现白字绿底），3：单选按钮（禁用启用类），4：下拉框且后面带文本框输入分数后自动选中该分数选项
        /// </summary>
        public int OptionStyle { get; set; }

        /// <summary>
        /// Icon Url
        /// </summary>
        [Description("Icon Url")]
        [MaxLength(200,ErrorMessage = "Icon Url 的最大长度为{1}")]
        public string IconUrl { get; set; }

        /// <summary>
        /// 关联选项
        /// </summary>
        [Description("关联选项")]
        public string AssociatedOptions { get; set; }

        /// <summary>
        /// 备注文本样式，-1：无，1：纯文本，2：Html
        /// </summary>
        [Description("备注文本样式，-1：无，1：纯文本，2：Html")]
        public int RemarkStyle { get; set; }

        /// <summary>
        /// 默认值选项
        /// </summary>
        [Description("默认值选项")]
        public Guid? DefaultValue { get; set; }

        /// <summary>
        /// 赋值Id
        /// </summary>
        /// <returns></returns>
        public ScoreDict SetId(Guid id)
        {
            Id = id;
            return this;
        }
        
    }
}