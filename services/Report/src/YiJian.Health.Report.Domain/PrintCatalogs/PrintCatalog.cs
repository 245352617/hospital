using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace YiJian.Health.Report.PrintCatalogs
{
    /// <summary>
    /// 打印目录
    /// </summary>
    [Comment("打印目录")]
    public class PrintCatalog : Entity<Guid>
    {
        /// <summary>
        /// 目录名称
        /// </summary>
        [Comment("目录名称")]
        [StringLength(100)]
        public string CataLogName { get; private set; }

        /// <summary>
        /// 类型，0:打印中心，1：其他地方打印
        /// </summary>
        [Comment("类型，0:打印中心，1：其他地方打印")]
        public int Type { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cataLogName"></param>
        /// <param name="type"></param>
        public PrintCatalog(Guid id, string cataLogName, int type)
        {
            Id = id;
            Update(cataLogName, type);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="cataLogName"></param>
        /// <param name="type"></param>
        public void Update(string cataLogName, int type)
        {
            CataLogName = cataLogName;
            Type = type;
        }
    }
}