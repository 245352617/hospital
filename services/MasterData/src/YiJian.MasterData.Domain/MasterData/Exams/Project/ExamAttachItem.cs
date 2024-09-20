using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.MasterData.Exams
{
    /// <summary>
    /// 描    述:检查附加项
    /// 创 建 人:杨凯
    /// 创建时间:2023/11/20 17:07:16
    /// </summary>
    [Comment("检查附加项")]
    public class ExamAttachItem : Entity<Guid>
    {
        /// <summary>
        /// 检查编码
        /// </summary>
        [StringLength(20)]
        [Comment("检查编码")]
        public string ProjectCode { get; set; }

        /// <summary>
        /// 药品或处置编码
        /// </summary>
        [StringLength(20)]
        [Comment("药品或处置编码")]
        public string MedicineCode { get; set; }

        /// <summary>
        /// 剂量或数量
        /// </summary>
        [Comment("剂量或数量")]
        public float Qty { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [StringLength(20)]
        [Comment("类型")]
        public string Type { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        public ExamAttachItem(Guid id)
        {
            Id = id;
        }
    }
}
