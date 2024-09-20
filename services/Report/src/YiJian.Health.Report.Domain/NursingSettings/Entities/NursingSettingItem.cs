using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingSettings.Entities
{
    /// <summary>
    /// 护理单配置项
    /// </summary>
    [Comment("护理单配置项")]
    public class NursingSettingItem : FullAuditedAggregateRoot<Guid>
    {
        private NursingSettingItem()
        {

        }

        public NursingSettingItem(
            Guid id,
            EInputType inputType,
            [NotNull] string value,
            [CanBeNull] string watermark,
            [CanBeNull] string text,
            bool hasTextblock,
            [CanBeNull] string textblockLeft,
            [CanBeNull] string textblockRight,
            Guid? nursingSettingHeaderId,
            bool hasNext = false,
            int lv = 0,
            bool isCarryInputBox = false,
            Guid? nursingSettingItemId = null,
            int sort = 0)
        {
            Id = id;
            InputType = inputType;
            Value = Check.NotNullOrWhiteSpace(value, nameof(value), maxLength: 50);
            Watermark = Check.Length(watermark,nameof(watermark),maxLength:50);
            Text = Check.Length(text, nameof(text), maxLength: 50);
            HasTextblock = hasTextblock;
            TextblockLeft = Check.Length(textblockLeft, nameof(textblockLeft), maxLength: 50); ;
            TextblockRight = Check.Length(textblockRight, nameof(textblockRight), maxLength: 50); ;
            NursingSettingHeaderId = nursingSettingHeaderId;
            
            HasNext = hasNext;
            Lv = lv;
            IsCarryInputBox = isCarryInputBox;

            NursingSettingItemId = nursingSettingItemId;
            Sort = sort;
        }

        /// <summary>
        /// 表单域类型（0=普通文本域,1=单选按钮,2=复选框,3=下拉框）
        /// </summary>
        [Comment("表单域类型（0=普通文本域,1=单选按钮,2=复选框,3=下拉框）")]
        public EInputType InputType { get; set; }

        /// <summary>
        /// 配置的值，护理单表单域需要提交保存的值
        /// </summary>
        [Comment("配置的值")]
        [Required, StringLength(50, ErrorMessage = "配置的值需在50字内")]
        public string Value { get;set;}

        /// <summary>
        /// 水印配置，文本域用【保留】
        /// </summary>
        [Comment("水印配置，文本域用")]
        [StringLength(50, ErrorMessage = "水印需在50字内")]
        public string Watermark { get; set; }

        /// <summary>
        /// 文本描述（复选框、单选按钮、下拉框用）
        /// </summary>
        [Comment("文本描述（复选框、单选按钮、下拉框用）")]
        [StringLength(50, ErrorMessage = "Text需在50字内")]
        public string Text { get; set; }

        /// <summary>
        /// 是否有提示文本（文本域会有这种左右提示，如：ml,g,% ...）
        /// </summary>
        [Comment("是否有提示文本")]
        public bool HasTextblock { get;set;}

        /// <summary>
        /// 左边提示文本
        /// </summary>
        [Comment("左边提示文本")]
        [StringLength(50, ErrorMessage = "左边提示文本需在50字内")]
        public string TextblockLeft { get; set; }

        /// <summary>
        /// 右边提示文本
        /// </summary>
        [Comment("右边提示文本")]
        [StringLength(50, ErrorMessage = "右边提示文本需在50字内")]
        public string TextblockRight { get; set; }
        
        /// <summary>
        /// 护理单表头配置Id
        /// </summary>
        [Comment("护理单表头配置Id")]
        public Guid? NursingSettingHeaderId { get;set;}

        /// <summary>
        /// 第一层表单域
        /// </summary>
        [Comment("层")]
        public int Lv { get; set; } = 0;

        /// <summary>
        /// 是否带输入框
        /// </summary>
        [Comment("是否带输入框")]
        public bool IsCarryInputBox { get; set; } = false;

        /// <summary>
        /// 是否有下一层
        /// </summary>
        [Comment("是否有下一层")]
        [Required]
        public bool HasNext { get;set;} = false;

        /// <summary>
        /// 自关联外键
        /// </summary>
        [Comment("自关联外键")]
        public Guid? NursingSettingItemId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 护理单配置项
        /// </summary>
        public virtual List<NursingSettingItem> Items { get; set; }

        /// <summary>
        /// 护理单表头配置
        /// </summary>
        public virtual NursingSettingHeader NursingSettingHeader { get; set; }
         
        public void Update(
            EInputType inputType,
            [NotNull] string value,
            [CanBeNull] string watermark,
            [CanBeNull] string text,
            bool hasTextblock,
            [CanBeNull] string textblockLeft,
            [CanBeNull] string textblockRight,
            Guid? nursingSettingHeaderId,
            bool hasNext = false,
            int lv = 0,
            bool isCarryInputBox = false,
            Guid? nursingSettingItemId = null,
            int sort=0)
        {
            InputType = inputType;
            Value = Check.NotNullOrWhiteSpace(value, nameof(value), maxLength: 50);
            Watermark = Check.Length(watermark, nameof(watermark), maxLength: 50);
            Text = Check.Length(text, nameof(text), maxLength: 50);
            HasTextblock = hasTextblock;
            TextblockLeft = Check.Length(textblockLeft, nameof(textblockLeft), maxLength: 50); ;
            TextblockRight = Check.Length(textblockRight, nameof(textblockRight), maxLength: 50); ;
            NursingSettingHeaderId = nursingSettingHeaderId;

            HasNext = hasNext;
            Lv = lv;
            IsCarryInputBox = isCarryInputBox;

            NursingSettingItemId = nursingSettingItemId;
            Sort = sort;
        }
    }
}
