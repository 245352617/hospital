using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.MasterData
{
    /// <summary>
    /// 描    述 ：护士医嘱类别
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/8/24 16:07:47
    /// </summary>
    [Comment("护士医嘱类别")]
    public class NursingRecipeType : Entity<Guid>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        public NursingRecipeType(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 设置主键
        /// </summary>
        /// <param name="id"></param>
        public void SetId(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 类别名称
        /// </summary>
        [Required]
        [Comment("类别名称")]
        public string TypeName { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [StringLength(50)]
        [Comment("编码")]
        public string UsageCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(200)]
        [Comment("名称")]
        public string UsageName { get; set; }
    }
}
