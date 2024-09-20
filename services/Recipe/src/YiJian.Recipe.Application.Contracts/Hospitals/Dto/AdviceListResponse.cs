using System;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Extensions;
using YiJian.Hospitals.Enums;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 导入医嘱信息
    /// </summary>
    public class AdviceListResponse
    {
        /// <summary>
        /// 医嘱Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 医嘱号
        /// </summary>  
        public string RecipeNo { get; set; }

        /// <summary>
        /// 医嘱子号[1,2,3..]
        /// </summary> 
        public int RecipeGroupNo { get; set; } = 1;

        /// <summary>
        /// 系统标识: 0=急诊，1=院前
        /// </summary> 
        public EPlatformType PlatformType { get; set; }

        /// <summary>
        /// 患者唯一标识
        /// </summary> 
        public Guid PIID { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary> 
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary> 
        public string PatientName { get; set; }

        /// <summary>
        /// 开嘱时间
        /// </summary> 
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// 开嘱时间日期
        /// </summary>
        public string ApplyDate
        {
            get
            {
                return ApplyTime.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 医嘱类型：临嘱、长嘱、出院带药等
        /// </summary> 
        public string PrescribeTypeName { get; set; }


        /// <summary>
        /// 医嘱各项分类: 0=药方项,1=检查项,2=检验项,3=诊疗项
        /// </summary> 
        public EDoctorsAdviceItemType ItemType { get; set; }

        /// <summary>
        /// 医嘱各项分类名称
        /// </summary>
        public string ItemTypeText
        {
            get
            {
                return ItemType.GetDescription();
            }
        }

        /// <summary>
        /// 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
        /// </summary> 
        public string CategoryName { get; set; }

        /// <summary>
        /// 医嘱编码
        /// </summary> 
        public string Code { get; set; }

        /// <summary>
        /// 医嘱名称
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        /// 每次剂量
        /// </summary> 
        public decimal? DosageQty { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary> 
        public string DosageUnit { get; set; }

        /// <summary>
        /// 用法编码 【途径编码】
        /// </summary> 
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法名称 【途径名称】
        /// </summary> 
        public string UsageName { get; set; }

        /// <summary>
        /// 频次码
        /// </summary> 
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 频次
        /// </summary> 
        public string FrequencyName { get; set; }


        /// <summary>
        /// 领量(数量)
        /// </summary> 
        public decimal RecieveQty { get; set; }

        /// <summary>
        /// 领量单位
        /// </summary> 
        public string RecieveUnit { get; set; }

        /// <summary>
        /// 包装规格
        /// </summary> 
        public string Specification { get; set; }

        /// <summary>
        /// 开药天数
        /// </summary> 
        public int LongDays { get; set; }

        /// <summary>
        /// 该记录被导入过或者是使用过： true=导入过，false=未导入过
        /// </summary>
        public bool Used { get; set; } = false;


        /// <summary>
        /// 附加类型
        /// </summary>
        public EAdditionalItemType AdditionalItemsType { get; set; }
    }

}