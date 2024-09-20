using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.Nursing.RecipeExecutes.Entities
{
    /// <summary>
    /// 描述：执行记录表
    /// 创建人： yangkai
    /// 创建时间：2023/3/8 18:16:01
    /// </summary>
    [Comment("执行记录表")]
    public class RecipeExecRecord : Entity<Guid>
    {
        /// <summary>
        /// 执行单Id
        /// </summary>
        [Comment("执行单Id")]
        public Guid RecipeExecId { get; set; }

        /// <summary>
        /// 执行剂量
        /// </summary>
        [Comment("执行剂量")]
        public decimal DosageQty { get; set; }

        /// <summary>
        /// 余量
        /// </summary>
        [Comment("余量")]
        public decimal RemainDosage { get; set; }

        /// <summary>
        /// 废弃量
        /// </summary>
        [Comment("废弃量")]
        public decimal DiscardDosage { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [Comment("计量单位")]
        [StringLength(30)]
        public string Unit { get; set; }

        /// <summary>
        /// 执行护士
        /// </summary>
        [StringLength(20)]
        [Comment("执行护士")]
        public string ExcuteNurseCode { get; set; }

        /// <summary>
        /// 执行护士名称
        /// </summary>
        [StringLength(50)]
        [Comment("执行护士名称")]
        public string ExcuteNurseName { get; set; }

        /// <summary>
        /// 护士执行时间
        /// </summary>
        [Comment("护士执行时间")]
        public DateTime ExcuteNurseTime { get; set; }

        /// <summary>
        /// 执行记录状态
        /// </summary>
        [Comment("执行记录状态")]
        public ExecRecordStatusEnum ExecRecordStatus { get; set; } = ExecRecordStatusEnum.Exec;

        /// <summary>
        /// 构造方法
        /// </summary>
        protected RecipeExecRecord() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        public RecipeExecRecord(Guid id) : base(id)
        {
        }
    }
}
