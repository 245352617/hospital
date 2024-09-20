using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Entities
{
    /// <summary>
    /// 药理
    /// </summary>
    [Comment("药理")]
    public class Toxic : Entity<Guid>
    {
        /// <summary>
        /// 药理
        /// </summary>
        private Toxic()
        {

        }


        /// <summary>
        /// 药理
        /// </summary> 
        public Toxic(Guid id,
            int medicineId,
            bool? isSkinTest,
            bool? isCompound,
            bool? isDrunk,
            int? toxicLevel,
            bool? isHighRisk,
            bool? isRefrigerated,
            bool? isTumour,
            int? antibioticLevel,
            bool? isPrecious,
            bool? isInsulin,
            bool? isAnaleptic,
            bool? isAllergyTest,
            bool? isLimited,
            string limitedNote)
        {
            Id = id;
            IsSkinTest = isSkinTest;
            IsCompound = isCompound;
            IsDrunk = isDrunk;
            ToxicLevel = toxicLevel;
            IsHighRisk = isHighRisk;
            IsRefrigerated = isRefrigerated;
            IsTumour = isTumour;
            AntibioticLevel = antibioticLevel;
            IsPrecious = isPrecious;
            IsInsulin = isInsulin;
            IsAnaleptic = isAnaleptic;
            IsAllergyTest = isAllergyTest;
            IsLimited = isLimited;
            LimitedNote = limitedNote;
            MedicineId = medicineId;
        }

        /// <summary>
        /// 皮试药
        /// </summary>
        [Comment("皮试药")]
        public bool? IsSkinTest { get; set; }

        /// <summary>
        /// 复方药
        /// </summary>
        [Comment("复方药")]
        public bool? IsCompound { get; set; }

        /// <summary>
        /// 麻醉药
        /// </summary>
        [Comment("麻醉药")]
        public bool? IsDrunk { get; set; }

        /// <summary>
        /// 药理 0=普通药品,1=毒性药品,2=麻醉、精一药品，3=精二类，4=放射性，5=贵重药品，6=妊娠药品，7=狂犬疫苗
        /// </summary>
        [Comment("药理 0=普通药品,1=毒性药品,2=麻醉、精一药品，3=精二类，4=放射性，5=贵重药品，6=妊娠药品，7=狂犬疫苗")]
        public int? ToxicLevel { get; set; }

        /// <summary>
        /// 高危药
        /// </summary>
        [Comment("高危药")]
        public bool? IsHighRisk { get; set; }

        /// <summary>
        /// 冷藏药
        /// </summary>
        [Comment("冷藏药")]
        public bool? IsRefrigerated { get; set; }

        /// <summary>
        /// 肿瘤药
        /// </summary>
        [Comment("肿瘤药")]
        public bool? IsTumour { get; set; }

        /// <summary>
        /// 抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级
        /// </summary>
        [Comment("抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级")]
        public int? AntibioticLevel { get; set; }

        /// <summary>
        /// 贵重药
        /// </summary>
        [Comment("贵重药")]
        public bool? IsPrecious { get; set; }

        /// <summary>
        /// 胰岛素
        /// </summary>
        [Comment("胰岛素")]
        public bool? IsInsulin { get; set; }

        /// <summary>
        /// 兴奋剂
        /// </summary>
        [Comment("兴奋剂")]
        public bool? IsAnaleptic { get; set; }

        /// <summary>
        /// 试敏药
        /// </summary>
        [Comment("试敏药")]
        public bool? IsAllergyTest { get; set; }

        /// <summary>
        /// 限制性用药标识
        /// </summary>
        [Comment("限制性用药标识")]
        public bool? IsLimited { get; set; }

        /// <summary>
        /// 限制性用药描述
        /// </summary>
        [Comment("限制性用药描述")]
        [StringLength(1024)]
        public string LimitedNote { get; set; }

        /// <summary>
        /// 药品id
        /// </summary>
        [Comment("药品id")]
        public int MedicineId { get; set; }

        /// <summary>
        /// 药理属性
        /// </summary>
        public Tuple<bool, string> Toxicprop
        {
            get
            {
                if (IsDrunk.HasValue && IsDrunk.Value) return new Tuple<bool, string>(IsDrunk.Value, "麻醉药");

                if (ToxicLevel.HasValue)
                {
                    if (ToxicLevel == 1) return new Tuple<bool, string>(true, "精一");
                    if (ToxicLevel == 2) return new Tuple<bool, string>(true, "精二");
                }

                if (IsHighRisk.HasValue && IsHighRisk.Value) return new Tuple<bool, string>(true, "高危药");


                if (IsRefrigerated.HasValue && IsRefrigerated.Value) return new Tuple<bool, string>(true, "冷藏药");
                if (IsTumour.HasValue && IsTumour.Value) return new Tuple<bool, string>(true, "肿瘤药");

                if (AntibioticLevel.HasValue)
                {
                    //抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级 
                    if (AntibioticLevel.Value == 1) return new Tuple<bool, string>(true, "抗菌药-非限制级");
                    if (AntibioticLevel.Value == 2) return new Tuple<bool, string>(true, "抗菌药-限制级");
                    if (AntibioticLevel.Value == 3) return new Tuple<bool, string>(true, "抗菌药-特殊使用级");
                }

                if (IsPrecious.HasValue && IsPrecious.Value) return new Tuple<bool, string>(true, "贵重药");
                if (IsInsulin.HasValue && IsInsulin.Value) return new Tuple<bool, string>(true, "胰岛素");
                if (IsAnaleptic.HasValue && IsAnaleptic.Value) return new Tuple<bool, string>(true, "兴奋剂");
                if (IsAllergyTest.HasValue && IsAllergyTest.Value) return new Tuple<bool, string>(true, "试敏药");

                return new Tuple<bool, string>(false, "");
            }
        }


        /// <summary>
        /// 更新
        /// </summary>  
        public void Update(
            bool? isSkinTest,
            bool? isCompound,
            bool? isDrunk,
            int? toxicLevel,
            bool? isHighRisk,
            bool? isRefrigerated,
            bool? isTumour,
            int? antibioticLevel,
            bool? isPrecious,
            bool? isInsulin,
            bool? isAnaleptic,
            bool? isAllergyTest,
            bool? isLimited,
            string limitedNote)
        {
            IsSkinTest = isSkinTest;
            IsCompound = isCompound;
            IsDrunk = isDrunk;
            ToxicLevel = toxicLevel;
            IsHighRisk = isHighRisk;
            IsRefrigerated = isRefrigerated;
            IsTumour = isTumour;
            AntibioticLevel = antibioticLevel;
            IsPrecious = isPrecious;
            IsInsulin = isInsulin;
            IsAnaleptic = isAnaleptic;
            IsAllergyTest = isAllergyTest;
            IsLimited = isLimited;
            LimitedNote = limitedNote;
        }

    }
}
