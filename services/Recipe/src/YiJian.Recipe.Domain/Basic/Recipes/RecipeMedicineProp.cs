using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Recipes.Basic
{
    /// <summary>
    /// 医嘱-药品属性
    /// Author: ywlin
    /// Date: 2021-12-04
    /// </summary>
    [Comment("医嘱-药品属性")]
    public class RecipeMedicineProp : Entity<Guid>
    {
        public RecipeMedicineProp()
        {

        }

        public RecipeMedicineProp(Guid id)
        {
            this.Id = id;
        }

        public void SetId(Guid id)
        {
            this.Id = id;
        }

        #region Properties

        /// <summary>
        /// 药物属性：西药、中药、西药制剂、中药制剂
        /// </summary>
        [Required]
        [StringLength(50)]
        [Comment("药物属性：西药、中药、西药制剂、中药制剂")]
        public string MedicineProperty { get; set; }

        #region 基本(单位)价格

        /// <summary>
        /// 基本单位
        /// </summary>
        [Required]
        [StringLength(20)]
        [Comment("基本单位")]
        public string Unit { get; set; }

        /// <summary>
        /// 基本单位价格
        /// </summary>
        [Comment("基本单位价格")]
        public decimal Price { get; set; }

        #endregion 基本(单位)价格

        #region 大包装价格

        /// <summary>
        /// 包装价格
        /// </summary>
        [Comment("包装价格")]
        public decimal BigPackPrice { get; set; }

        /// <summary>
        /// 大包装量
        /// </summary>
        [Required]
        [StringLength(20)]
        [Comment("大包装量")]
        public int BigPackFactor { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary>
        [Required]
        [StringLength(20)]
        [Comment("包装单位")]
        public string BigPackUnit { get; set; }

        #endregion 大包装价格

        #region 小包装价格

        /// <summary>
        /// 小包装单价
        /// </summary>
        [Comment("小包装单价")]
        public decimal SmallPackPrice { get; set; }

        /// <summary>
        /// 小包装单位
        /// </summary>
        [Required]
        [StringLength(20)]
        [Comment("小包装单位")]
        public string SmallPackUnit { get; set; }

        /// <summary>
        /// 小包装量
        /// </summary>
        [Comment("小包装量")]
        public int SmallPackFactor { get; set; }

        #endregion 小包装价格

        /// <summary>
        /// 剂量（药品特有属性）
        /// </summary>
        [Comment("剂量")]
        public virtual decimal DosageQty { get; set; }

        /// <summary>
        /// 剂量单位（药品特有属性）
        /// </summary>
        [StringLength(20)]
        [Comment("剂量单位")]
        public virtual string DosageUnit { get; set; }

        /// <summary>
        /// 药房代码
        /// </summary>
        [StringLength(20)]
        [Comment("药房代码")]
        public virtual string PharmacyCode { get; set; }

        /// <summary>
        /// 药房
        /// </summary>
        [StringLength(50)]
        [Comment("药房")]
        public virtual string PharmacyName { get; set; }

        /// <summary>
        /// 皮试药（药品特有属性）
        /// </summary>
        [Comment("皮试药")]
        public virtual bool IsSkinTest { get; set; }

        /// <summary>
        /// 复方药（药品特有属性）
        /// </summary>
        [Comment("复方药")]
        public virtual bool IsCompound { get; set; }

        /// <summary>
        /// 麻醉药（药品特有属性）
        /// </summary>
        [Comment("麻醉药")]
        public virtual bool IsDrunk { get; set; }

        /// <summary>
        /// 用途/途径编码
        /// </summary>
        [StringLength(20)]
        [Comment("用途/途径编码")]
        public virtual string UsageCode { get; set; }

        /// <summary>
        /// 用途/途径名称
        /// </summary>
        [StringLength(50)]
        [Comment("用途/途径名称")]
        public virtual string UsageName { get; set; }

        /// <summary>
        /// 频次编码
        /// </summary>
        [StringLength(20)]
        [Comment("频次编码")]
        public virtual string FrequencyCode { get; set; }

        /// <summary>
        /// 频次名称
        /// </summary>
        [StringLength(50)]
        [Comment("频次名称")]
        public virtual string FrequencyName { get; set; }

        /// <summary>
        /// 厂家代码
        /// </summary>
        [StringLength(50)]
        [Comment("厂家代码")]
        public virtual string FactoryCode { get; set; }

        /// <summary>
        /// 厂家名称
        /// </summary>
        [StringLength(100)]
        [Comment("厂家名称")]
        public virtual string FactoryName { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        [StringLength(100)]
        [Comment("批次")]
        public virtual string BatchNo { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [Comment("失效日期")]
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 药理等级：如（毒、麻、精一、精二）
        /// </summary> 
        [StringLength(5000)]
        public string ToxicProperty { get; set; }

        /// <summary>
        /// 精神药  0非精神药,1一类精神药,2二类精神药
        /// </summary>
        [Comment("精神药  0非精神药,1一类精神药,2二类精神药")]
        public int? ToxicLevel { get; set; }

        /// <summary>
        /// 抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级
        /// </summary>
        [Comment("抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级")]
        public int? AntibioticLevel { get; set; }

        /// <summary>
        /// 抗生素权限
        /// </summary>
        [Comment("抗生素权限")]
        public int AntibioticPermission { get; set; }

        /// <summary>
        /// 处方权
        /// </summary>
        [Comment("处方权")]
        public int PrescriptionPermission { get; set; }

        /// <summary>
        /// 限制性用药标识
        /// </summary>
        [Comment("限制性用药标识")]
        public bool? IsLimited { get; set; }

        /// <summary>
        /// 限制性用药描述
        /// </summary>
        [StringLength(1024)]
        [Comment("限制性用药描述")]
        public string LimitedNote { get; set; }

        /// <summary>
        /// 医保类型：0自费,1甲类,2乙类，3丙类
        /// </summary>
        [Comment("医保类型：0自费,1甲类,2乙类，3丙类")]
        public EInsuranceCatalog InsuranceType { get; set; }

        /// <summary>
        /// 医保类型代码
        /// </summary>
        [Comment("医保类型代码")]
        public string InsuranceCode { get; set; }


        /// <summary>
        /// 收费分类代码
        /// </summary>
        [StringLength(50)]
        [Comment("收费分类代码")]
        public string ChargeCode { get; set; }

        /// <summary>
        /// 收费分类名称
        /// </summary>
        [StringLength(50)]
        [Comment("收费分类名称")]
        public string ChargeName { get; set; }

        /// <summary>
        /// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
        /// </summary>
        [Comment("门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分")]
        public MedicineUnPack Unpack { get; set; }

        /// <summary>
        /// 医嘱主要内容
        /// </summary>
        public virtual RecipeProject RecipeProject { get; set; }


        #endregion
    }
}
