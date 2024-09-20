using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Recipes.DoctorsAdvices.Entities
{
    /// <summary>
    /// 医嘱打印信息
    /// </summary>
    [Comment("医嘱打印信息")]
    public class PrintInfo : FullAuditedAggregateRoot<Guid>
    {
        private PrintInfo()
        {
        }

        /// <summary>
        /// 再次打印修改
        /// </summary>
        /// <param name="isPrintAgain"></param>
        public void SetPrintAgain(bool isPrintAgain = false)
        {
            IsPrintAgain = isPrintAgain;
        }

        /// <summary>
        /// 打印
        /// </summary> 
        public PrintInfo(Guid id, string printCode, string printName, string prescriptionNo, Guid templateId, bool isPrintAgain = false)
        {
            Id = id;
            PrintCode = printCode;
            PrintName = printName;
            PrintTime = DateTime.Now;
            PrescriptionNo = prescriptionNo;
            IsPrintAgain = isPrintAgain;
            TemplateId = templateId;
        }

        /// <summary>
        /// 是否是再次打印(补打)
        /// </summary>
        [Comment("是否是再次打印(补打)")]
        public bool IsPrintAgain { get; set; } = false;

        /// <summary>
        /// 印人的编码
        /// </summary>
        [Comment("印人的编码")]
        [StringLength(20)]
        public string PrintCode { get; set; }

        /// <summary>
        /// 印人的名称
        /// </summary>
        [Comment("印人的名称")]
        [StringLength(20)]
        public string PrintName { get; set; }

        /// <summary>
        /// 补打时间
        /// </summary>
        [Comment("补打时间")]
        public DateTime? PrintTime { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        [Comment("处方号")]
        [StringLength(20)]
        public string PrescriptionNo { get; set; }
        /// <summary>
        /// 打印模板id
        /// </summary>
        [Comment("打印模板id")]
        public Guid TemplateId { get; set; }

    }
}
