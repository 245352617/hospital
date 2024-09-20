using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using YiJian.DoctorsAdvices.Enums;

namespace YiJian.Recipes.DoctorsAdvices.Entities
{
    /// <summary>
    /// 检查小项
    /// </summary>
    [Comment("检查小项")]
    public class PacsItem : Entity<Guid>
    {
        private PacsItem()
        {

        }

        /// <summary>
        /// 检查小项
        /// </summary> 
        public PacsItem(
            Guid id,
            string targetCode,
            string targetName,
            string targetUnit,
            decimal price,
            decimal qty,
            string insuranceCode,
            EInsuranceCatalog insuranceType,

            string projectCode,
            decimal otherPrice,
            string specification,
            int sort,
            string pyCode,
            string wbCode,
            string specialFlag,
            bool isActive,
            string projectType,
            string projectMerge,

            Guid pacsId)
        {
            Id = id;
            TargetCode = targetCode;
            TargetName = targetName;
            TargetUnit = targetUnit;
            Price = price;
            Qty = qty;
            InsuranceCode = insuranceCode;
            InsuranceType = insuranceType;

            ProjectCode = projectCode;
            OtherPrice = otherPrice;
            Specification = specification;
            Sort = sort;
            PyCode = pyCode;
            WbCode = wbCode;
            SpecialFlag = specialFlag;
            IsActive = isActive;
            ProjectType = projectType;
            ProjectMerge = projectMerge;

            PacsId = pacsId;
        }

        /// <summary>
        /// 小项编码
        /// </summary>
        [Comment("小项编码")]
        [Required, StringLength(50)]
        public string TargetCode { get; set; }

        /// <summary>
        /// 小项名称
        /// </summary>
        [Comment("小项名称")]
        [Required, StringLength(200)]
        public string TargetName { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [Comment("单价")]
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Comment("数量")]
        public decimal Qty { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Comment("单位")]
        [StringLength(20)]
        public string TargetUnit { get; set; }

        /// <summary>
        /// 医保目录编码
        /// </summary>
        [Comment("医保目录编码")]
        [StringLength(20)]
        public string InsuranceCode { get; set; }

        /// <summary>
        /// 医保目录:0=自费,1=甲类,2=乙类,3=其它
        /// </summary>
        [Comment("医保目录:0=自费,1=甲类,2=乙类,3=其它")]
        public EInsuranceCatalog InsuranceType { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        [Comment("项目编码")]
        [StringLength(20)]
        public string ProjectCode { get; set; }

        /// <summary>
        /// 其它价格
        /// </summary>
        [Comment("其它价格")]
        public decimal OtherPrice { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [Comment("规格")]
        [StringLength(50)]
        public string Specification { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Comment("排序号")]
        public int Sort { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        [Comment("拼音码")]
        [StringLength(50)]
        public string PyCode { get; set; }

        /// <summary>
        /// 五笔
        /// </summary>
        [Comment("五笔")]
        [StringLength(50)]
        public string WbCode { get; set; }

        /// <summary>
        /// 特殊标识
        /// </summary>
        [Comment("特殊标识")]
        [StringLength(50)]
        public string SpecialFlag { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Comment("是否启用")]
        public bool IsActive { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary>
        [Comment("项目类型")]
        [StringLength(50)]
        public string ProjectType { get; set; }

        /// <summary>
        /// 项目归类
        /// </summary>
        [Comment("项目归类")]
        [StringLength(50)]
        public string ProjectMerge { get; set; }

        /// <summary>
        /// 检查项Id
        /// </summary>
        [Comment("检查项Id")]
        public Guid PacsId { get; set; }

        /// <summary>
        /// 医保机构编码
        /// </summary>
        [Comment("医保机构编码")]
        [StringLength(50)]
        public string MeducalInsuranceCode { get; set; }

        /// <summary>
        /// 医保二级编码
        /// </summary>
        [Comment("医保二级编码")]
        [StringLength(50)]
        public string YBInneCode { get; set; }

        /// <summary>
        /// 检查项
        /// </summary>
        [JsonIgnore]
        public virtual Pacs Pacs { get; set; }

        /// <summary>
        /// 克隆的时候需要重新设置关联关系
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="pacsId"></param>
        public void ResetId(Guid id, [NotNull] Guid pacsId)
        {
            Id = id;
            PacsId = pacsId;
            Pacs = null;
        }

    }
}
