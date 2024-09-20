using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace YiJian.Health.Report
{
    /// <summary>
    /// 纸张大小
    /// </summary>
    [Comment("纸张大小")]
    public class PageSize : Entity<Guid>
    {
        public PageSize(string code, decimal height, decimal width)
        {
            Code = code;
            Update(height, width);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public void Update(decimal height, decimal width)
        {
            Height = height;
            Width = width;
        }

        /// <summary>
        /// 编码
        /// </summary>
        [Comment("编码")]
        [StringLength(50)]
        public string Code { get; private set; }

        /// <summary>
        /// 高
        /// </summary>
        [Comment("高")]
        public decimal Height { get; private set; }

        /// <summary>
        /// 宽
        /// </summary>
        [Comment("宽")]
        public decimal Width { get; private set; }
    }
}