using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace YiJian.EMR.DataBinds.Entities
{
    /// <summary>
    /// 数据绑定字典
    /// </summary>
    [Comment("数据绑定字典")]
    public class DataBindMap : Entity<Guid>
    {
        private DataBindMap()
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="datasource"></param>
        /// <param name="path"></param>
        /// <param name="value"></param>
        /// <param name="dataBindContextId"></param>
        public DataBindMap(Guid id, [CanBeNull] string datasource, [NotNull] string path, [CanBeNull] string value, Guid dataBindContextId)
        {
            Id = id;
            DataSource = datasource;
            Path = Check.NotNullOrEmpty(path, nameof(path), maxLength: 50);
            Value = value;
            DataBindContextId = dataBindContextId;
        }

        /// <summary>
        /// 数据分类
        /// </summary>
        [Comment("数据分类")]
        [StringLength(50)]
        public string DataSource { get; set; }

        /// <summary>
        /// 绑定的数据名称
        /// </summary>
        [Comment("绑定的数据名称")]
        [StringLength(50)]
        public string Path { get; set; }

        /// <summary>
        /// 绑定的数据
        /// </summary>
        [Comment("绑定的数据")]
        [Column(TypeName = "ntext")]
        public string Value { get; set; }

        /// <summary>
        /// 数据上下文Id
        /// </summary>
        [Comment("数据上下文Id")]
        public Guid DataBindContextId { get; set; }

        /// <summary>
        /// 数据上下文
        /// </summary>
        public virtual DataBindContext DataBindContext { get; set; }

    }
}
