using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field,
            Inherited = true, AllowMultiple = false)]
    public class DbDescriptionAttribute : Attribute
    {
        /// <summary>
        /// 应用的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 初始化新的实例
        /// </summary>
        /// <param name="description">说明内容</param>
        public DbDescriptionAttribute(string description)
        {
            Description = description;
        }

        /// <summary>
        /// 说明
        /// </summary>
        public virtual string Description { get; }

        /// <summary>
        /// 是否启用vue的列头
        /// </summary>
        public virtual bool IsEnbaleTbColumn { get; set; } = true;

        /// <summary>
        /// vue的列头排序
        /// </summary>
        public int Sort { get; set; } = 0;

        /// <summary>
        /// 是否排序
        /// </summary>
        public bool IsSort { get; set; } = true;

        /// <summary>
        /// 排序方向
        /// </summary>
        public OrderDirectEnum OrderDire { get; set; } = OrderDirectEnum.降序;

    }
}
