using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Preferences.Dto
{
    /// <summary>
    /// 快速医嘱查询记录
    /// </summary>
    public class QuickStartQueryDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 药品Id(his 的 invid)
        /// </summary> 
        public int MedicineId { get; set; }

        /// <summary>
        /// 药品编码
        /// </summary> 
        public string MedicineCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary> 
        public string MedicineName { get; set; }

        /// <summary>
        /// 基本单位
        /// </summary> 
        public string Unit { get; set; }

        /// <summary>
        /// 基本单位价格
        /// </summary> 
        public decimal Price { get; set; }

        /// <summary>
        /// 备注（医嘱说明）
        /// </summary> 
        public string Remark { get; set; }

        /// <summary>
        /// 皮试药
        /// </summary> 
        public bool? IsSkinTest { get; set; }

        /// <summary>
        /// 规格
        /// </summary> 
        public string Specification { get; set; }

        /// <summary>
        /// 剂型
        /// </summary> 
        public string DosageForm { get; set; }

        /// <summary>
        /// 默认频次编码
        /// </summary> 
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 默认频次名称
        /// </summary> 
        public string FrequencyName { get; set; }

        /// <summary>
        /// 在一个周期内执行的次数
        /// </summary> 
        public int? FrequencyTimes { get; set; }

        /// <summary>
        /// 周期单位，比如1D=每天，1W=每周，2D=隔天执行，1H=每小时或者每半小时
        /// </summary> 
        public string FrequencyUnit { get; set; }

        /// <summary>
        /// 每日次(HIS的配置频次信息)
        /// </summary> 
        public string DailyFrequency { get; set; }

        /// <summary>
        /// 医保编码
        /// </summary>
        public string MedicalInsuranceCode { get; set; }

        /// <summary>
        /// 药房代码
        /// </summary> 
        public string PharmacyCode { get; set; }

        /// <summary>
        /// 药房
        /// </summary> 
        public string PharmacyName { get; set; }

        /// <summary>
        /// 数量
        /// </summary> 
        public decimal Qty { get; set; }

        /// <summary>
        /// 统计使用过的次数（个人统计）
        /// </summary> 
        public int UsageCount { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        ///  医嘱项目分类编码
        /// </summary> 
        public string CategoryCode { get; set; }

        /// <summary>
        /// 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
        /// </summary> 
        public string CategoryName { get; set; }

        /// <summary>
        /// 开嘱时间
        /// </summary> 
        public DateTime? ApplyTime { get; set; }

        /// <summary>
        /// 申请医生编码
        /// </summary> 
        public string ApplyDoctorCode { get; set; }

        /// <summary>
        /// 申请医生
        /// </summary> 
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 付费类型编码
        /// </summary> 
        public string PayTypeCode { get; set; }

        /// <summary>
        /// 付费类型: 0=自费,1=医保,2=其它
        /// </summary> 
        public ERecipePayType PayType { get; set; }

        /// <summary>
        /// 医嘱说明
        /// </summary> 
        public string Remarks { get; set; }

        /// <summary>
        /// 收费类型编码
        /// </summary> 
        public string ChargeCode { get; set; }

        /// <summary>
        /// 收费类型名称
        /// </summary> 
        public string ChargeName { get; set; }

        /// <summary>
        /// 学名
        /// </summary> 
        public string ScientificName { get; set; }

        /// <summary>
        /// 药物属性：西药、中药、西药制剂、中药制剂
        /// </summary> 
        public string MedicineProperty { get; set; }

        #region 剂量

        /// <summary>
        /// 默认剂量
        /// </summary> 
        public double DefaultDosage { get; set; }

        /// <summary>
        /// 剂量
        /// </summary> 
        public decimal DosageQty { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary> 
        public string DosageUnit { get; set; }

        #endregion 剂量

        #region 大包装价格

        /// <summary>
        /// 大包装价格
        /// </summary> 
        public decimal BigPackPrice { get; set; }

        /// <summary>
        /// 大包装量(大包装系数)
        /// </summary> 
        public int BigPackFactor { get; set; }

        /// <summary>
        /// 大包装单位
        /// </summary> 
        public string BigPackUnit { get; set; }

        #endregion 大包装价格

        #region 小包装价格

        /// <summary>
        /// 小包装单价
        /// </summary> 
        public decimal SmallPackPrice { get; set; }

        /// <summary>
        /// 小包装单位
        /// </summary> 
        public string SmallPackUnit { get; set; }

        /// <summary>
        /// 小包装量(小包装系数)
        /// </summary> 
        public int SmallPackFactor { get; set; }

        #endregion 小包装价格

        /// <summary>
        /// 儿童价格
        /// </summary> 
        public decimal? ChildrenPrice { get; set; }

        /// <summary>
        /// 批发价格
        /// </summary> 
        public decimal? FixPrice { get; set; }

        /// <summary>
        /// 零售价格
        /// </summary> 
        public decimal? RetPrice { get; set; }


        /// <summary>
        /// 默认用法编码
        /// </summary> 
        public string UsageCode { get; set; }

        /// <summary>
        /// 默认用法名称
        /// </summary> 
        public string UsageName { get; set; }

        /// <summary>
        /// 急救药
        /// </summary> 
        public bool? IsFirstAid { get; set; }

        /// <summary>
        /// 库存
        /// </summary> 
        public int Stock { get; set; }

        /// <summary>
        /// 基药标准  N普通,Y国基,P省基,C市基
        /// </summary> 
        public string BaseFlag { get; set; }

        /// <summary>
        /// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
        /// </summary> 
        public EMedicineUnPack Unpack { get; set; }


        #region 医嘱相关的默认值属性

        /// <summary>
        /// 领量(数量)
        /// </summary> 
        public decimal RecieveQty { get; set; }

        /// <summary>
        /// 领量单位
        /// </summary> 
        public string RecieveUnit { get; set; }

        /// <summary>
        /// 药理等级：如（毒、麻、精一、精二） 
        /// </summary> 
        public string ToxicProperty { get; set; }

        /// <summary>
        /// 开药天数
        /// </summary> 
        public int LongDays { get; set; }

        /// <summary>
        /// 实际天数
        /// </summary> 
        public int? ActualDays { get; set; }

        /// <summary>
        /// 默认规格剂量
        /// </summary> 
        public decimal DefaultDosageQty { get; set; }

        /// <summary>
        /// 每次用量
        /// </summary> 
        public decimal? QtyPerTimes { get; set; }

        /// <summary>
        /// 默认规格剂量单位
        /// </summary> 
        public string DefaultDosageUnit { get; set; }

        /// <summary>
        /// 皮试结果:false=阴性 ture=阳性
        /// </summary> 
        public bool? SkinTestResult { get; set; }

        #endregion

        /// <summary>
        /// 快速开嘱医嘱信息Id
        /// </summary>
        public Guid QuickStartAdviceId { get; set; }

        /// <summary>
        /// 限制用药标记 “1.限制用药 2.非限制用药” 出现1.限制用药需要弹出 限制费用提示消息
        /// </summary> 
        public int? LimitType { get; set; }

        /// <summary>
        /// 厂家名称
        /// </summary> 
        public string FactoryName { get; set; }

        /// <summary>
        /// 厂家代码
        /// </summary> 
        public string FactoryCode { get; set; }

        /// <summary>
        /// 是否危急药
        /// </summary>
        public bool IsCriticalPrescription { get; set; }

        /// <summary>
        /// (Unit)提交给HIS的一次剂量的单位，视图里面的那个Unit单位（原封不动的传过来，不要做任何修改）
        /// </summary> 
        [StringLength(20)]
        public string HisUnit { get; set; }

        /// <summary>
        /// HIS的一次剂量数量（HIS标准剂量单位的数量）
        /// </summary> 
        [StringLength(20)]
        public string HisDosageUnit { get; set; }

        /// <summary>
        /// HIS的一次剂量数量（HIS标准剂量单位的数量）
        /// </summary> 
        public decimal HisDosageQty { get; set; }

        /// <summary>
        /// 提交的一次剂量数量，急诊换算后提交给HIS的一次剂量
        /// </summary> 
        public decimal CommitHisDosageQty { get; private set; }
    }

}
