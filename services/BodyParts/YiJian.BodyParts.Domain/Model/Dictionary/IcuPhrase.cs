using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.BodyParts.Model
{
    /// <summary>
    /// 表:常用语模板
    /// </summary>
    public class IcuPhrase : Entity<Guid>
    {
        public IcuPhrase() { }

        public IcuPhrase(Guid id) : base(id) { }


        /// <summary>
        /// 类型代码
        /// </summary>
        [CanBeNull]
        [StringLength(10)]
        public string TypeCode { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        public string TypeName { get; set; }

        /// <summary>
        /// 上级编号
        /// </summary>
        [CanBeNull]
        [StringLength(10)]
        public string ParentId { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        [CanBeNull]
        [StringLength(10)]
        public string DeptCode { get; set; }

        /// <summary>
        /// 员工代码
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string StaffCode { get; set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        [CanBeNull]
        [StringLength(4000)]
        public string PhraseText { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 是否有效(1-有效，0-无效)
        /// </summary>
        [Required]
        public int ValidState { get; set; }

    }
}
