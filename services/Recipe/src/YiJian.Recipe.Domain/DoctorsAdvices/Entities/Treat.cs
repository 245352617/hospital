using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.Hospitals.Enums;

namespace YiJian.Recipes.DoctorsAdvices.Entities
{
    /// <summary>
    /// 诊疗项
    /// </summary>
    [Comment("诊疗项")]
    public class Treat : FullAuditedAggregateRoot<Guid>
    {
        private Treat()
        {
        }

        /// <summary>
        /// 诊疗项
        /// </summary> 
        /// <param name="id"></param>
        public Treat(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 诊疗项
        /// </summary> 
        public Treat(Guid id,
            string specification,
            string frequencyCode,
            string frequencyName,
            string feeTypeMainCode,
            string feeTypeSubCode,
            Guid doctorsAdviceId,
            bool additional,
            decimal? otherPrice,
            string usageCode,
            string usageName,
            int longDays,
            string projectType,
            string projectName,
            string projectMerge,
            int treatId, Guid additionalItemsId,
            EAdditionalItemType additionalItemsType)
        {
            Id = id;
            Specification = specification;
            FrequencyCode = frequencyCode;
            FrequencyName = frequencyName;
            FeeTypeMainCode = feeTypeMainCode;
            FeeTypeSubCode = feeTypeSubCode;
            DoctorsAdviceId = doctorsAdviceId;
            Additional = additional;
            OtherPrice = otherPrice;
            UsageCode = usageCode;
            UsageName = usageName;
            LongDays = longDays;
            ProjectType = projectType;
            ProjectName = projectName;
            ProjectMerge = projectMerge;
            TreatId = treatId;
            AdditionalItemsId = additionalItemsId;
            AdditionalItemsType = additionalItemsType;
        }


        /// <summary>
        /// 加收标志	
        /// </summary>
        [Comment("加收标志")]
        public bool Additional { get; set; }


        /// <summary>
        /// 其它价格 (诊疗项用作儿童加收费用)
        /// </summary>
        [Comment("其它价格")]
        public decimal? OtherPrice { get; set; }

        /// <summary>
        /// 默认频次代码
        /// </summary>
        [Comment("默认频次代码")]
        [StringLength(20)]
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        [Comment("频次")]
        [StringLength(20)]
        public string FrequencyName { get; set; }

        /// <summary>
        /// 收费大类代码
        /// </summary>
        [Comment("收费大类代码")]
        [StringLength(50)]
        public string FeeTypeMainCode { get; set; }

        /// <summary>
        /// 收费小类代码
        /// </summary>
        [Comment("收费小类代码")]
        [StringLength(50)]
        public string FeeTypeSubCode { get; set; }


        /// <summary>
        /// 包装规格
        /// </summary>
        [Comment("包装规格")]
        [StringLength(200)]
        public string Specification { get; set; }

        /// <summary>
        /// 用法编码
        /// </summary>
        [Comment("用法编码")]
        [StringLength(20)]
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary>
        [Comment("用法名称")]
        [StringLength(20)]
        public string UsageName { get; set; }

        /// <summary>
        /// 开药天数
        /// </summary>
        [Comment("开药天数")]
        public int LongDays { get; set; } = 1;

        /// <summary>
        /// 项目类型
        /// </summary>
        [Comment("项目类型")]
        [StringLength(50)]
        public string ProjectType { get; set; }

        /// <summary>
        /// 项目类型名称
        /// </summary>
        [Comment("项目类型名称")]
        [StringLength(50)]
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目归类
        /// </summary>
        [StringLength(20)]
        [Comment("项目归类")]
        public string ProjectMerge { get; set; }

        /// <summary>
        /// 诊疗项Id
        /// </summary>
        [Comment("诊疗项Id")]
        public int TreatId { get; set; }

        /// <summary>
        /// 医嘱Id
        /// </summary> 
        [Comment("医嘱Id")]
        public Guid DoctorsAdviceId { get; set; }

        /// <summary>
        /// 附加类型
        /// </summary>
        [Comment("附加类型")]
        public EAdditionalItemType AdditionalItemsType { get; set; }

        /// <summary>
        /// 药品附加项id
        /// </summary>
        [Comment("药品附加项id")]
        public Guid AdditionalItemsId { get; set; }

        /// <summary>
        /// 医嘱信息
        /// </summary>
        [NotMapped]
        public virtual DoctorsAdvice DoctorsAdvice { get; set; }

        /// <summary>
        /// 克隆的时候需要重新设置关联关系
        /// </summary>
        /// <param name="id"></param>
        /// <param name="doctorsAdviceId"></param>
        public void ResetId([NotNull] Guid id, [NotNull] Guid doctorsAdviceId)
        {
            Id = id;
            DoctorsAdviceId = doctorsAdviceId;
            DoctorsAdvice = null;
        }

        /// <summary>
        /// 更新
        /// </summary> 
        public void Update(Treat obj)
        {
            Specification = obj.Specification;
            Additional = obj.Additional;
            OtherPrice = obj.OtherPrice;
            FrequencyCode = obj.FrequencyCode;
            FeeTypeMainCode = obj.FeeTypeMainCode;
            FeeTypeSubCode = obj.FeeTypeSubCode;
            UsageCode = obj.UsageCode;
            UsageName = obj.UsageName;
            LongDays = obj.LongDays;
            ProjectType = obj.ProjectType;
            ProjectName = obj.ProjectName;
            ProjectMerge = obj.ProjectMerge;
            TreatId = obj.TreatId;
        }

        /// <summary>
        /// 诊疗项
        /// </summary> 
        public void Update(
            string specification,
            string frequencyCode,
            string frequencyName,
            string feeTypeMainCode,
            string feeTypeSubCode,
            bool additional,
            decimal? otherPrice,
            string usageCode,
            string usageName,
            int longDays,
            string projectType,
            string projectName,
            string projectMerge,
            Guid additionalItemsId,
            EAdditionalItemType additionalItemsType,
            int treatId)
        {
            Specification = specification;
            FrequencyCode = frequencyCode;
            FrequencyName = frequencyName;
            FeeTypeMainCode = feeTypeMainCode;
            FeeTypeSubCode = feeTypeSubCode;
            Additional = additional;
            OtherPrice = otherPrice;
            UsageCode = usageCode;
            UsageName = usageName;
            LongDays = longDays;
            ProjectType = projectType;
            ProjectName = projectName;
            ProjectMerge = projectMerge;
            AdditionalItemsId = additionalItemsId;
            AdditionalItemsType = additionalItemsType;
            TreatId = treatId;
        }

        /// <summary>
        /// 获取诊疗项的儿童价格
        /// </summary>
        /// <param name="isChildren"></param>  
        /// <param name="isAdditionalPrice"></param>
        /// <returns></returns>
        public decimal GetAmount(bool isChildren, out bool isAdditionalPrice)
        {
            //如果没有医嘱信息，这返回零值
            if (DoctorsAdvice == null)
            {
                isAdditionalPrice = false;
                return 0;
            }

            if (Additional && isChildren && OtherPrice.HasValue && OtherPrice.Value > 0)
            {
                isAdditionalPrice = true;
                return decimal.Parse(OtherPrice.Value.ToString("f3")) * DoctorsAdvice.RecieveQty;
            }
            else
            {
                isAdditionalPrice = false;
                return decimal.Parse(DoctorsAdvice.Price.ToString("f3")) * DoctorsAdvice.RecieveQty;
            }
        }

    }
}