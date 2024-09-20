using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.ECIS.Call.CallConfig
{
    /// <summary>
    /// 【列配置】领域实体
    /// </summary>
    public class RowConfig : Entity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required, StringLength(50)]
        [Key]
        public virtual string Key { get; private set; }

        /// <summary>
        /// 字段名
        /// </summary>
        [Required, StringLength(50)]
        public virtual string Field { get; private set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual uint Order { get; private set; }

        /// <summary>
        /// 列名
        /// </summary>
        [Required, StringLength(80)]
        public virtual string Text { get; private set; }

        /// <summary>
        /// 列宽
        /// </summary>
        public virtual uint Width { get; private set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public virtual bool Visible { get; private set; }

        /// <summary>
        /// 是否换号
        /// </summary>
        public virtual bool Wrap { get; private set; }

        /// <summary>
        /// 排序（默认值）
        /// </summary>
        public virtual uint DefaultOrder { get; private set; }

        /// <summary>
        /// 列名（默认值）
        /// </summary>
        [Required, StringLength(80)]
        public virtual string DefaultText { get; private set; }

        /// <summary>
        /// 列宽（默认值）
        /// </summary>
        public virtual uint DefaultWidth { get; private set; }

        /// <summary>
        /// 是否显示（默认值）
        /// </summary>
        public virtual bool DefaultVisible { get; private set; }

        /// <summary>
        /// 是否换号（默认值）
        /// </summary>
        public bool DefaultWrap { get; private set; }

        /// <summary>
        /// 主键定义
        /// </summary>
        /// <returns></returns>
        public override object[] GetKeys()
        {
            return new object[] { Key };
        }

        /// <summary>
        /// 创建列配置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="order"></param>
        /// <param name="field"></param>
        /// <param name="visible"></param>
        /// <param name="wrap"></param>
        /// <param name="text"></param>
        /// <param name="width"></param>
        public RowConfig([NotNull] string key, uint order = 0, [NotNull] string field = null,
                         bool visible = true, bool wrap = false,
                         [NotNull] string text = null, uint width = 0)
        {
            this.Key = Check.NotNullOrEmpty(key, nameof(Key), 50);
            this.Field = Check.NotNullOrEmpty(field, nameof(Field), 50);
            this.DefaultOrder = order;
            this.Order = order;
            this.DefaultVisible = visible;
            this.Visible = visible;
            this.DefaultWrap = wrap;
            this.Wrap = wrap;
            this.DefaultText = Check.NotNullOrEmpty(text, nameof(DefaultText), 80);
            this.Text = DefaultText;
            this.DefaultWidth = width >= 0 ? width : 60;
            this.Width = DefaultWidth;
        }

        /// <summary>
        /// 修改列配置
        /// </summary>
        /// <param name="order"></param>
        /// <param name="visible"></param>
        /// <param name="wrap"></param>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public RowConfig Modify(uint order, bool visible, bool wrap, [NotNull] string text, uint width)
        {
            this.Order = order;
            this.Visible = visible;
            this.Wrap = wrap;
            this.Text = Check.NotNull(text, nameof(Text), 80);
            this.Width = width;
            return this;
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <returns></returns>
        public RowConfig Reset()
        {
            this.Order = this.DefaultOrder;
            this.Visible = DefaultVisible;
            this.Wrap = DefaultWrap;
            this.Text = this.DefaultText;
            this.Width = this.DefaultWidth;
            return this;
        }
    }
}
