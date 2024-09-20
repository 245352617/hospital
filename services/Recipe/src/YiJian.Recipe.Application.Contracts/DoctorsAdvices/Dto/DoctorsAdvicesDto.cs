using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Extensions;
using YiJian.Hospitals.Enums;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 医嘱记录信息
    /// </summary>
    public class DoctorsAdvicesDto : EntityDto<Guid>
    {
        /// <summary>
        /// 患者年龄参数，用来检查是否是儿童
        /// </summary>
        public PatientInfoDto PatientInfo { get; set; }

        #region 医嘱主体

        /// <summary>
        /// 系统标识: 0=急诊，1=院前
        /// </summary> 
        public EPlatformType PlatformType { get; set; }

        /// <summary>
        /// 系统标识文本描述
        /// </summary>
        public string PlatformTypeText
        {
            get
            {
                return PlatformType.GetDescription();
            }
        }

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
        /// 医嘱编码
        /// </summary> 
        public string Code { get; set; }

        /// <summary>
        /// 医嘱名称
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        ///  医嘱项目分类编码
        /// </summary> 
        public string CategoryCode { get; set; }

        /// <summary>
        /// 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
        /// </summary> 
        public string CategoryName { get; set; }

        /// <summary>
        /// 是否补录
        /// </summary> 
        public bool IsBackTracking { get; set; }

        /// <summary>
        /// 处方号
        /// </summary> 
        public string PrescriptionNo { get; set; }

        /// <summary>
        /// 医嘱号
        /// </summary>  
        public string RecipeNo { get; set; }

        /// <summary>
        /// 医嘱子号（同组下参数修改）
        /// </summary> 
        public int RecipeGroupNo { get; set; } = 1;

        /// <summary>
        /// 开嘱时间
        /// </summary> 
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 开始时间展示字段
        /// </summary>
        public string ApplyTimeText
        {
            get
            {
                return ApplyTime.ToString("yyyy/MM/dd HH:mm");
            }
        }

        /// <summary>
        /// 开嘱日期
        /// </summary>
        public string ApplyDate
        {
            get
            {
                return ApplyTime.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 申请医生编码
        /// </summary> 
        public string ApplyDoctorCode { get; set; }

        /// <summary>
        /// 申请医生
        /// </summary> 
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 申请医生签名
        /// </summary>
        public string ApplyDoctorSignature { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary> 
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请科室
        /// </summary> 
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 管培生代码
        /// </summary> 
        public string TraineeCode { get; set; }

        /// <summary>
        /// 管培生名称
        /// </summary> 
        public string TraineeName { get; set; }

        /// <summary>
        /// 执行科室编码
        /// </summary> 
        public string ExecDeptCode { get; set; }

        /// <summary>
        /// 执行科室名称
        /// </summary> 
        public string ExecDeptName { get; set; }

        /// <summary>
        /// 执行者编码
        /// </summary> 
        public string ExecutorCode { get; set; }

        /// <summary>
        /// 执行者名称
        /// </summary> 
        public string ExecutorName { get; set; }

        /// <summary>
        /// 执行者签名
        /// </summary> 
        public string ExecutorSignature { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary> 
        public DateTime? ExecTime { get; set; }

        /// <summary>
        /// 停嘱医生代码
        /// </summary> 
        public string StopDoctorCode { get; set; }

        /// <summary>
        /// 停嘱医生名称
        /// </summary> 
        public string StopDoctorName { get; set; }

        /// <summary>
        /// 停嘱时间
        /// </summary> 
        public DateTime? StopDateTime { get; set; }

        /// <summary>
        /// 医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,18=已缴费,19=已退费
        /// </summary> 
        public ERecipeStatus Status { get; set; }

        /// <summary>
        /// 医嘱状态文本描述
        /// </summary>
        public string StatusText
        {
            get
            {
                return Status.GetDescription();
            }
        }

        /// <summary>
        /// 付费类型编码
        /// </summary> 
        public string PayTypeCode { get; set; }

        /// <summary>
        /// 付费类型: 0=自费,1=医保,2=其它
        /// </summary> 
        public ERecipePayType PayType { get; set; }

        /// <summary>
        /// 付费类型文本描述
        /// </summary>
        public string PayTypeText
        {
            get
            {
                return PayType.GetDescription();
            }
        }

        /// <summary>
        /// 支付状态 , 0=待支付,1=已支付,2=部分支付,3=已退费
        /// </summary> 
        public EPayStatus PayStatus { get; set; }

        /// <summary>
        /// 单位
        /// </summary> 
        public string Unit { get; set; }

        /// <summary>
        /// 单价
        /// </summary>  
        public decimal Price { get; set; }

        /// <summary>
        /// 总费用
        /// </summary> 
        public decimal Amount { get; set; }

        /// <summary>
        /// 是否是加收价格 true=是加收价格， false=不是加收价格
        /// </summary> 
        public bool IsAdditionalPrice { get; set; }


        /// <summary>
        /// 提交序列号，每次提交只生成一个序列号,0=没有提交序号
        /// </summary> 
        public int CommitSerialNo { get; set; } = 0;

        /// <summary>
        /// 医嘱创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 获取诊疗项的儿童价格
        /// </summary>
        /// <param name="isChildren"></param> 
        /// <param name="isAdditionalPrice"></param>
        /// <returns></returns>
        public decimal GetAmount(bool isChildren, out bool isAdditionalPrice)
        {
            if (ItemType == EDoctorsAdviceItemType.Treat && Additional && isChildren && OtherPrice.HasValue && OtherPrice.Value > 0)
            {
                isAdditionalPrice = true;
                return OtherPrice.Value * RecieveQty;
            }
            else
            {
                isAdditionalPrice = false;
                return Price * RecieveQty;
            }
        }

        /// <summary>
        /// 医保目录编码
        /// </summary> 
        public string InsuranceCode { get; set; }

        /// <summary>
        /// 医保目录:0=自费,1=甲类,2=乙类,3=其它
        /// </summary> 
        public EInsuranceCatalog InsuranceType { get; set; }

        /// <summary>
        /// 医保目录文本描述
        /// </summary>
        public string InsuranceTypeText
        {
            get
            {
                return InsuranceType.GetDescription();
            }
        }

        /// <summary>
        /// 是否慢性病
        /// </summary> 
        public bool? IsChronicDisease { get; set; }

        /// <summary>
        /// 是否打印过
        /// </summary> 
        public bool IsRecipePrinted { get; set; }

        /// <summary>
        /// HIS医嘱号
        /// </summary> 
        public string HisOrderNo { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>   
        public string Diagnosis { get; set; }

        /// <summary>
        /// 医嘱各项分类: 0=药方项,1=检查项,2=检验项,3=诊疗项
        /// </summary>
        public EDoctorsAdviceItemType ItemType { get; set; }

        /// <summary>
        /// 医嘱各项分类文本描述
        /// </summary>
        public string ItemTypeText
        {
            get
            {
                return ItemType.GetDescription();
            }
        }

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

        #endregion

        #region 药方

        /// <summary>
        /// 药方Id
        /// </summary>
        public Guid? PrescribeId { get; set; }

        /// <summary>
        /// 是否自备药：false=非自备药,true=自备药
        /// </summary> 
        public bool IsOutDrug { get; set; }

        /// <summary>
        /// 药物属性：西药、中药、西药制剂、中药制剂
        /// </summary> 
        public string MedicineProperty { get; set; }

        /// <summary>
        /// 药理等级：如（毒、麻、精一、精二）
        /// xml 存储
        /// </summary> 
        public string ToxicProperty { get; set; }

        /// <summary>
        /// 医嘱类型编码
        /// </summary> 
        public string PrescribeTypeCode { get; set; }

        /// <summary>
        /// 医嘱类型：临嘱、长嘱、出院带药等
        /// </summary> 
        public string PrescribeTypeName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary> 
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary> 
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 用法编码
        /// </summary> 
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary> 
        public string UsageName { get; set; }

        /// <summary>
        /// 滴速
        /// </summary> 
        public string Speed { get; set; }

        /// <summary>
        /// 开药天数
        /// </summary> 
        public int? LongDays { get; set; }

        /// <summary>
        /// 实际天数
        /// </summary> 
        public int? ActualDays { get; set; }

        /// <summary>
        /// 剂型
        /// </summary>
        public string DosageForm { get; set; }

        /// <summary>
        /// 每次剂量
        /// </summary> 
        public decimal DosageQty { get; set; }

        /// <summary>
        /// 每次剂量
        /// </summary> 
        public decimal DefaultDosageQty { get; set; }

        /// <summary>
        /// 每次用量
        /// </summary> 
        public decimal? QtyPerTimes { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary> 
        public string DosageUnit { get; set; }

        /// <summary>
        /// 默认规格剂量单位
        /// </summary> 
        public string DefaultDosageUnit { get; set; }

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


        private decimal _recieveQty;
        /// <summary>
        /// 领量(数量)
        /// </summary> 
        public decimal RecieveQty
        {
            get
            {
                if (ItemType == EDoctorsAdviceItemType.Lis || ItemType == EDoctorsAdviceItemType.Pacs)
                {
                    return _recieveQty <= 0 ? 1 : _recieveQty;
                }
                return _recieveQty;
            }
            set { _recieveQty = value; }
        }

        private string _recieveUnit;
        /// <summary>
        /// 领量单位
        /// </summary> 
        public string RecieveUnit
        {
            get
            {
                if (ItemType == EDoctorsAdviceItemType.Lis || ItemType == EDoctorsAdviceItemType.Pacs)
                {
                    if (_recieveUnit.IsNullOrEmpty()) return "次";
                }
                return _recieveUnit;
            }
            set { _recieveUnit = value; }
        }

        /// <summary>
        /// 包装规格
        /// </summary> 
        public string Specification { get; set; }

        /// <summary>
        /// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
        /// </summary> 
        public EMedicineUnPack Unpack { get; set; }

        /// <summary>
        /// 门诊拆分属性文本描述
        /// </summary>
        public string UnpackText
        {
            get
            {
                return Unpack.GetDescription();
            }
        }

        /// <summary>
        /// 包装价格
        /// </summary> 
        public decimal BigPackPrice { get; set; }

        /// <summary>
        /// 大包装系数(拆零系数)
        /// </summary> 
        public int BigPackFactor { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary> 
        public string BigPackUnit { get; set; }

        /// <summary>
        /// 小包装单价
        /// </summary> 
        public decimal SmallPackPrice { get; set; }

        /// <summary>
        /// 小包装单位
        /// </summary>  
        public string SmallPackUnit { get; set; }

        /// <summary>
        /// 小包装系数(拆零系数)
        /// </summary> 
        public int SmallPackFactor { get; set; }

        private string _frequencyCode;
        /// <summary>
        /// 频次码
        /// </summary>
        public string FrequencyCode
        {
            get
            {
                if (PlatformType == EPlatformType.PreHospital && _frequencyCode.IsNullOrEmpty())
                {
                    return "ONCE";
                }
                return _frequencyCode;
            }
            set { _frequencyCode = value; }
        }

        private string _frequencyName;
        /// <summary>
        ///  频次
        /// </summary>
        public string FrequencyName
        {
            get
            {
                if (PlatformType == EPlatformType.PreHospital && _frequencyName.IsNullOrEmpty())
                {
                    return "ONCE";
                }
                return _frequencyName;
            }
            set { _frequencyName = value; }
        }

        /// <summary>
        /// 医保编码
        /// </summary>
        public string MedicalInsuranceCode { get; set; }

        /// <summary>
        /// 在一个周期内执行的次数
        /// </summary> 
        public int? FrequencyTimes { get; set; }

        /// <summary>
        /// 周期单位，比如1D=每天，1W=每周，2D=隔天执行，1H=每小时或者每半小时
        /// </summary> 
        public string FrequencyUnit { get; set; }

        /// <summary>
        /// 一天内的执行时间，格式如："00:01,00:30","21:00:00.000","14:00,02:00","周日：08：00","周一：08：00；周四：08：00；"。
        /// 日时间点只有一个的时候，格式为：HH:mm:ss.fff。
        /// 日时间点多个的时候，格式为：HH:mm，以逗号（,）分割。
        /// 周时间点一个到多个的时候，格式为：周[一 | 二 | 三 | 四 | 五 | 六 | 日 | 天]:HH:mm，以分号（;）分割。 
        /// </summary> 
        public string FrequencyExecDayTimes { get; set; }

        /// <summary>
        /// HIS频次编码
        /// </summary> 
        public string DailyFrequency { get; set; }

        /// <summary>
        /// 药品编码
        /// </summary>
        public int? MedicineId { get; set; }

        /// <summary>
        /// 药房编码
        /// </summary> 
        public string PharmacyCode { get; set; }

        /// <summary>
        /// 药房
        /// </summary> 
        public string PharmacyName { get; set; }

        /// <summary>
        /// 厂家
        /// </summary> 
        public string FactoryName { get; set; }

        /// <summary>
        /// 厂家代码
        /// </summary> 
        public string FactoryCode { get; set; }

        /// <summary>
        /// 批次号
        /// </summary> 
        public string BatchNo { get; set; }

        /// <summary>
        /// 失效期
        /// </summary> 
        public DateTime? ExpirDate { get; set; }

        /// <summary>
        /// 是否皮试 false=不需要皮试 true=需要皮试
        /// </summary> 
        public bool? IsSkinTest { get; set; }

        /// <summary>
        /// 皮试结果:false=阴性 ture=阳性
        /// </summary> 
        public bool? SkinTestResult { get; set; }

        /// <summary>
        /// 皮试选择结果 皮试选择结果,默认空什么都没选择，0=否，1=是，2=续用
        /// </summary> 
        public ESkinTestSignChoseResult? SkinTestSignChoseResult { get; set; }

        /// <summary>
        /// 耗材金额
        /// </summary> 
        public decimal? MaterialPrice { get; set; }

        /// <summary>
        /// 用于判断关联耗材是否手动删除
        /// </summary> 
        public bool? IsBindingTreat { get; set; }

        /// <summary>
        /// 是否抢救后补：false=非抢救后补，true=抢救后补
        /// </summary> 
        public bool? IsAmendedMark { get; set; }

        /// <summary>
        /// 是否医保适应症
        /// </summary> 
        public bool? IsAdaptationDisease { get; set; }

        /// <summary>
        /// 是否是急救药
        /// </summary> 
        public bool? IsFirstAid { get; set; }

        /// <summary>
        /// 位置编码
        /// </summary>  
        public string PositionCode { get; set; }

        /// <summary>
        /// 位置
        /// </summary>  
        public string PositionName { get; set; }

        /// <summary>
        /// 抗生素权限
        /// </summary> 
        public int AntibioticPermission { get; set; }

        /// <summary>
        /// 处方权
        /// </summary>
        public int PrescriptionPermission { get; set; }

        #endregion

        #region 诊疗

        /// <summary>
        /// 数量
        /// </summary>  
        public decimal? Qty { get; set; }

        /// <summary>
        /// 其它价格
        /// </summary> 
        public decimal? OtherPrice { get; set; }

        /// <summary>
        /// 加收标志	
        /// </summary> 
        public bool Additional { get; set; }

        /// <summary>
        /// 收费大类代码
        /// </summary> 
        public string FeeTypeMainCode { get; set; }

        /// <summary>
        /// 收费小类代码
        /// </summary> 
        public string FeeTypeSubCode { get; set; }

        /// <summary>
        /// 检查部位编码
        /// </summary>
        public string PartCode { get; set; }
        /// <summary>
        /// 检查部位
        /// </summary>
        public string PartName { get; set; }
        /// <summary>
        /// 标本编码
        /// </summary>
        public string SpecimenCode { get; set; }
        /// <summary>
        /// 标本名称
        /// </summary>
        public string SpecimenName { get; set; }
        /// <summary>
        /// 检查目录编码
        /// </summary>
        public string CatalogCode { get; set; }
        /// <summary>
        /// 检查目录名称
        /// </summary>
        public string CatalogName { get; set; }
        /// <summary>
        /// 标本采集部位编码
        /// </summary>
        /// 
        /// <summary>
        /// 临床症状
        /// </summary> 
        public string ClinicalSymptom { get; set; }
        /// <summary>
        /// 病史简要
        /// </summary> 
        public string MedicalHistory { get; set; }
        /// <summary>
        /// 目录描述名称 例如心电图申请单、超声申请单
        /// </summary> 
        public string CatalogDisplayName { get; set; }
        /// <summary>
        /// 是否紧急
        /// </summary> 
        public bool? IsEmergency { get; set; }
        /// <summary>
        /// 是否在床旁
        /// </summary> 
        public bool? IsBedSide { get; set; }
        public string SpecimenPartCode { get; set; }
        /// <summary>
        /// 标本采集部位
        /// </summary>
        public string SpecimenPartName { get; set; }
        /// <summary>
        /// 标本容器代码
        /// </summary> 
        public string ContainerCode { get; set; }

        /// <summary>
        /// 标本容器
        /// </summary> 
        public string ContainerName { get; set; }
        /// <summary>
        /// 标本容器颜色:0=红帽,1=蓝帽,2=紫帽
        /// </summary> 
        public string ContainerColor { get; set; }
        /// <summary>
        /// 标本说明
        /// </summary> 
        public string SpecimenDescription { get; set; }
        /// <summary>
        /// 标本采集时间
        /// </summary> 
        public DateTime? SpecimenCollectDatetime { get; set; }
        /// <summary>
        /// 标本接收时间
        /// </summary> 
        public DateTime? SpecimenReceivedDatetime { get; set; }

        /// <summary>
        /// 诊疗Id
        /// </summary>
        public Guid? TreatId { get; set; }

        /// <summary>
        /// 字典带过来的诊疗项Id
        /// </summary>
        public int intTreatId { get; set; }

        /// <summary>
        /// 检查Id
        /// </summary>
        public Guid? PacsId { get; set; }
        /// <summary>
        /// 检验Id
        /// </summary>
        public Guid? LisId { get; set; }

        /// <summary>
        /// 限制用药标记 “1.限制用药 2.非限制用药” 出现1.限制用药需要弹出 限制费用提示消息
        /// </summary> 
        public int? LimitType { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary> 
        [StringLength(50)]
        public string ProjectType { get; set; }

        /// <summary>
        /// 项目类型名称
        /// </summary> 
        [StringLength(50)]
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目归类
        /// </summary>
        [StringLength(20)]
        public string ProjectMerge { get; set; }

        /// <summary>
        /// 精神药  0非精神药,1一类精神药,2二类精神药
        /// </summary>  
        public int? ToxicLevel { get; set; }

        /// <summary>
        /// 抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级
        /// </summary> 
        public int? AntibioticLevel { get; set; }

        /// <summary>
        /// 附加类型
        /// </summary>
        public EAdditionalItemType AdditionalItemsType { get; set; }
        /// <summary>
        /// 药品附加项id
        /// </summary>
        public Guid AdditionalItemsId { get; set; }
        /// <summary>
        /// 收费类型
        /// </summary>  
        public ERestrictedDrugs? RestrictedDrugs { get; set; }

        /// <summary>
        /// 是否危急处方药
        /// </summary>
        public bool IsCriticalPrescription { get; set; }

        /// <summary>
        /// 医保机构编码
        /// </summary>
        public string MeducalInsuranceCode { get; set; }

        /// <summary>
        /// 医保二级编码
        /// </summary>
        public string YBInneCode { get; set; }

        /// <summary>
        /// 医保统一名称
        /// </summary>
        public string MeducalInsuranceName { get; set; }

        /// <summary>
        /// 检查病理小项
        /// </summary>
        public PacsPathologyItemDto pacsPathologyItemDto { get; set; }

        /// <summary>
        /// 检查小项
        /// </summary>
        public List<PacsItemDto> pacsItemDtos { get; set; }

        /// <summary>
        /// 检验小项
        /// </summary>
        public List<LisItemDto> lisItemDtos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RestrictedDrugsText
        {
            get
            {
                if (RestrictedDrugs == null || RestrictedDrugs == 0) return "";
                return RestrictedDrugs.GetDescription();
            }
        }

        /// <summary>
        /// 如果是急诊的诊疗频次，设为空
        /// </summary>
        public void SetTreanFrequency()
        {
            if (PlatformType == EPlatformType.EmergencyTreatment && ItemType == EDoctorsAdviceItemType.Treat)
            {
                FrequencyCode = "";
                FrequencyName = "";
            }
        }

        /// <summary>
        /// 是否打印
        /// </summary>
        public bool? IsPrint { get; set; }

        #endregion
    }
}
