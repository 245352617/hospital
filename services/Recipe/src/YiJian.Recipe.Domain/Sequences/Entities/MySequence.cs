using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace YiJian.Sequences.Entities
{
    /// <summary>
    /// 我的系列号管理器
    /// </summary>
    [Comment("我的系列号管理器")]
    public class MySequence : Entity<Guid>
    {
        private MySequence()
        {

        }

        /// <summary>
        /// 我的系列号管理器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName">表名</param>
        /// <param name="filedName">字段名</param>
        /// <param name="increment">增长计数器</param>
        public MySequence(Guid id, [NotNull] string tableName, [NotNull] string filedName, int increment = 1)
        {
            Id = id;
            TableName = Check.NotNullOrEmpty(tableName, nameof(TableName), maxLength: 50);
            FiledName = Check.NotNullOrEmpty(filedName, nameof(filedName), maxLength: 50);
            Increment = increment;
        }

        /// <summary>
        /// 受控表名
        /// </summary>
        [Comment("受控表名")]
        [StringLength(50)]
        public string TableName { get; set; }

        /// <summary>
        /// 受控的字段
        /// </summary>
        [Comment("受控的字段")]
        [StringLength(50)]
        public string FiledName { get; set; }

        /// <summary>
        /// 增长计数器
        /// </summary>
        [Comment("增长计数器")]
        public int Increment { get; set; }

    }
}
