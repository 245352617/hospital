using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Recipes.DoctorsAdvices.Entities
{
    /// <summary>
    /// 医嘱主表
    /// </summary>
    [Comment("医嘱主表")]
    public class DoctorsAdvice : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 医嘱主表
        /// </summary>
        private DoctorsAdvice()
        {
        }

        /// <summary>
        /// 新建一个传入id的医嘱(目前支持套餐使用)
        /// </summary> 
        public DoctorsAdvice(Guid id,
            int detailId,
            Guid piid,
            EPlatformType platformType,
            string patientId,
            string patientName,
            string applyDeptCode,
            string applyDeptName,
            string applyDoctorCode,
            string applyDoctorName,
            string diagnosis,
            string traineeCode,
            string traineeName,
            bool? isChronicDisease,
            decimal recieveQty,
            string recieveUnit,
            string prescribeTypeCode = "PrescribeTemp",
            string prescribeTypeName = "临")
        {
            Id = id;
            DetailId = detailId;
            PlatformType = platformType;
            PIID = piid;
            PatientId = patientId;
            PatientName = patientName;
            ApplyDeptCode = applyDeptCode;
            ApplyDeptName = applyDeptName;
            ApplyDoctorCode = applyDoctorCode;
            ApplyDoctorName = applyDoctorName;
            ApplyTime = DateTime.Now;
            Diagnosis = diagnosis;
            TraineeCode = traineeCode;
            TraineeName = traineeName;
            IsChronicDisease = isChronicDisease;

            RecieveQty = recieveQty;
            RecieveUnit = recieveUnit;

            //默认值
            PrescribeTypeCode = prescribeTypeCode.IsNullOrEmpty() ? "" : prescribeTypeCode;
            PrescribeTypeName = prescribeTypeName.IsNullOrEmpty() ? "" : prescribeTypeName;
            PayStatus = EPayStatus.NoPayment;
            IsRecipePrinted = false;
            Status = ERecipeStatus.Saved;
            StopDateTime = null;
            StopDoctorName = "";
            StopDoctorCode = "";
            ExecTime = null;
            SourceType = 2;
        }

        /// <summary>
        /// 医嘱主表
        /// </summary>
        public DoctorsAdvice(Guid id,
            int detailId,
            EPlatformType platformType,
            Guid piid,
            string patientId,
            string patientName,
            string code,
            string name,
            string categoryCode,
            string categoryName,
            bool isBackTracking,
            string prescriptionNo,
            string recipeNo,
            int recipeGroupNo,
            DateTime applyTime,
            string applyDoctorCode,
            string applyDoctorName,
            string applyDeptCode,
            string applyDeptName,
            string traineeCode,
            string traineeName,
            string payTypeCode,
            ERecipePayType payType,
            decimal price,
            string unit,
            decimal amount,
            string insuranceCode,
            EInsuranceCatalog insuranceType,
            bool? isChronicDisease,
            bool isRecipePrinted,
            string hisOrderNo,
            string diagnosis,
            string execDeptCode,
            string execDeptName,
            string positionCode,
            string positionName,
            string remarks,
            string chargeCode,
            string chargeName,
            string prescribeTypeCode,
            string prescribeTypeName,
            DateTime? startTime,
            DateTime? endTime,
            decimal recieveQty,
            string recieveUnit,
            string pyCode,
            string wbCode,
            EDoctorsAdviceItemType itemType,
            bool isAdditionalPrice = false,
            int sourceType = 0)
        {
            Id = id;
            DetailId = detailId;
            PlatformType = platformType;
            PIID = piid;
            PatientId = patientId;
            PatientName = patientName;
            Code = code;
            Name = name;
            CategoryCode = categoryCode;
            CategoryName = categoryName;
            IsBackTracking = isBackTracking;
            PrescriptionNo = prescriptionNo;
            RecipeNo = recipeNo;
            RecipeGroupNo = recipeGroupNo;
            ApplyTime = applyTime;
            ApplyDoctorCode = applyDoctorCode;
            ApplyDoctorName = applyDoctorName;
            ApplyDeptCode = applyDeptCode;
            ApplyDeptName = applyDeptName;
            TraineeCode = traineeCode;
            TraineeName = traineeName;
            PayTypeCode = payTypeCode;
            PayType = payType;
            Price = price;
            Unit = unit;
            //传零，默认自己试算，如果传一个数值，则取传过来的
            if (amount > 0) Amount = amount;
            InsuranceCode = insuranceCode;
            InsuranceType = insuranceType;
            IsChronicDisease = isChronicDisease;
            IsRecipePrinted = isRecipePrinted;
            HisOrderNo = hisOrderNo;
            Diagnosis = diagnosis;
            ItemType = itemType;
            PositionCode = positionCode;
            PositionName = positionName;
            Remarks = remarks;
            ChargeCode = chargeCode;
            ChargeName = chargeName;
            PrescribeTypeCode = prescribeTypeCode;
            PrescribeTypeName = prescribeTypeName;
            StartTime = startTime;
            EndTime = endTime;
            Status = ERecipeStatus.Saved;
            ExecDeptCode = execDeptCode;
            ExecDeptName = execDeptName;
            ExecutorCode = "";
            ExecutorName = "";
            ExecTime = null;
            RecieveQty = recieveQty;
            RecieveUnit = recieveUnit;
            PyCode = pyCode;
            WbCode = wbCode;
            IsAdditionalPrice = isAdditionalPrice;
            SourceType = sourceType;
        }

        /// <summary>
        /// 明细ID，给医院使用，唯一（备用候选键）
        /// </summary>
        [Comment("明细ID，给医院使用，唯一（备用候选键）")]
        public int DetailId { get; set; }

        /// <summary>
        /// 系统标识: 0=急诊，1=院前
        /// </summary>
        [Comment("系统标识: 0=急诊，1=院前")]
        public EPlatformType PlatformType { get; set; }

        /// <summary>
        /// 患者唯一标识
        /// </summary>
        [Comment("患者唯一标识")]
        public Guid PIID { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary>
        [Comment("患者Id")]
        [StringLength(20)]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>
        [Comment("患者名称")]
        [StringLength(30)]
        public string PatientName { get; set; }

        /// <summary>
        /// 医嘱编码
        /// </summary>
        [Comment("医嘱编码")]
        [Required, StringLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 医嘱名称
        /// </summary>
        [Comment("医嘱名称")]
        [Required, StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        ///  医嘱项目分类编码
        /// </summary>
        [Comment("医嘱项目分类编码")]
        [Required, StringLength(20)]
        public string CategoryCode { get; set; }

        /// <summary>
        /// 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
        /// </summary>
        [Comment("医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)")]
        [Required, StringLength(20)]
        public string CategoryName { get; set; }

        /// <summary>
        /// 是否补录
        /// </summary>
        [Comment("是否补录")]
        public bool IsBackTracking { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        [Comment("处方号")]
        [StringLength(20)]
        public string PrescriptionNo { get; set; }

        /// <summary>
        /// 医嘱号
        /// </summary> 
        [Comment("医嘱号")]
        [Required, StringLength(20)]
        public string RecipeNo { get; set; }

        /// <summary>
        /// 医嘱子号（同组下参数修改）
        /// </summary>
        [Comment("医嘱子号")]
        public int RecipeGroupNo { get; set; } = 1;

        /// <summary>
        /// 开嘱时间
        /// </summary>
        [Comment("开嘱时间")]
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 申请医生编码
        /// </summary>
        [Comment("申请医生编码")]
        [Required, StringLength(20)]
        public string ApplyDoctorCode { get; set; }

        /// <summary>
        /// 申请医生
        /// </summary>
        [Comment("申请医生")]
        [Required, StringLength(50)]
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary>
        [Comment("申请科室编码")]
        [StringLength(50)]
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请科室
        /// </summary>
        [Comment("申请科室")]
        [StringLength(50)]
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 管培生代码
        /// </summary>
        [Comment("管培生代码")]
        [StringLength(20)]
        public string TraineeCode { get; set; }

        /// <summary>
        /// 管培生名称
        /// </summary>
        [Comment("管培生名称")]
        [StringLength(50)]
        public string TraineeName { get; set; }

        /// <summary>
        /// 执行科室编码
        /// </summary>
        [Comment("执行科室编码")]
        [StringLength(20)]
        public string ExecDeptCode { get; set; }

        /// <summary>
        /// 执行科室名称
        /// </summary>
        [Comment("执行科室名称")]
        [StringLength(50)]
        public string ExecDeptName { get; set; }

        /// <summary>
        /// 执行者编码
        /// </summary>
        [Comment("执行者编码")]
        [StringLength(20)]
        public string ExecutorCode { get; set; }

        /// <summary>
        /// 执行者名称
        /// </summary>
        [Comment("执行者名称")]
        [StringLength(50)]
        public string ExecutorName { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        [Comment("执行时间")]
        public DateTime? ExecTime { get; set; }

        /// <summary>
        /// 停嘱医生编码
        /// </summary> 
        [Comment("停嘱医生编码")]
        public string StopDoctorCode { get; set; }

        /// <summary>
        /// 停嘱医生名称
        /// </summary> 
        [Comment("停嘱医生名称")]
        public string StopDoctorName { get; set; }

        /// <summary>
        /// 停嘱时间
        /// </summary> 
        [Comment("停嘱时间")]
        public DateTime? StopDateTime { get; set; }

        /// <summary>
        /// 医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,18=已缴费,19=已退费
        /// </summary>
        [Comment("医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,18=已缴费,19=已退费")]
        public ERecipeStatus Status { get; set; }

        /// <summary>
        /// 付费类型编码
        /// </summary>
        [Comment("付费类型编码")]
        [StringLength(20)]
        public string PayTypeCode { get; set; }

        /// <summary>
        /// 付费类型: 0=自费,1=医保,2=其它
        /// </summary>
        [Comment("付费类型: 0=自费,1=医保,2=其它")]
        public ERecipePayType PayType { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Comment("单位")]
        [StringLength(20)]
        public string Unit { get; set; }

        /// <summary>
        /// 单价
        /// </summary> 
        [Comment("单价")]
        public decimal Price { get; set; }

        /// <summary>
        /// 总费用
        /// </summary>
        [Comment("总费用")]
        public decimal Amount { get; set; }

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
        /// 是否慢性病
        /// </summary>
        [Comment("是否慢性病")]
        public bool? IsChronicDisease { get; set; }

        /// <summary>
        /// 是否打印过
        /// </summary>
        [Comment("是否打印过")]
        public bool IsRecipePrinted { get; set; }

        /// <summary>
        /// HIS医嘱号
        /// </summary>
        [Comment("HIS医嘱号")]
        [StringLength(20)]
        public string HisOrderNo { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>  
        [Comment("临床诊断")]
        public string Diagnosis { get; set; }

        /// <summary>
        /// 位置编码
        /// </summary>
        [Comment("位置编码")]
        [StringLength(20)]
        public string PositionCode { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [Comment("位置")]
        [StringLength(100)]
        public string PositionName { get; set; }

        /// <summary>
        /// 医嘱各项分类: 0=药方项,1=检查项,2=检验项,3=诊疗项
        /// </summary>
        [Comment("医嘱各项分类: 0=药方项,1=检查项,2=检验项,3=诊疗项")]
        public EDoctorsAdviceItemType ItemType { get; set; }

        /// <summary>
        /// 医嘱说明
        /// </summary>
        [Comment("医嘱说明")]
        [StringLength(500)]
        public string Remarks { get; set; }

        /// <summary>
        /// 收费类型编码
        /// </summary>
        [Comment("收费类型编码")]
        [StringLength(20)]
        public string ChargeCode { get; set; }

        /// <summary>
        /// 收费类型名称
        /// </summary>
        [Comment("收费类型名称")]
        [StringLength(50)]
        public string ChargeName { get; set; }

        /// <summary>
        /// 支付状态 , 0=待支付,1=已支付,2=部分支付,3=已退费
        /// </summary>
        [Comment("支付状态 , 0=待支付,1=已支付,2=部分支付,3=已退费")]
        public EPayStatus PayStatus { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        [Comment("发票号")]
        [StringLength(100)]
        public string InvoiceNo { get; set; }

        /// <summary>
        /// 医嘱类型编码
        /// </summary>
        [Comment("医嘱类型编码")]
        [Required, StringLength(20)]
        public string PrescribeTypeCode { get; set; }

        /// <summary>
        /// 医嘱类型：临嘱、长嘱、出院带药等
        /// </summary>
        [Comment("医嘱类型：临嘱、长嘱、出院带药等")]
        [Required, StringLength(20)]
        public string PrescribeTypeName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Comment("开始时间")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Comment("结束时间")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 领量(数量)
        /// </summary>
        [Comment("领量(数量)")]
        public decimal RecieveQty { get; set; }

        /// <summary>
        /// 领量单位
        /// </summary>
        [Comment("领量单位")]
        [StringLength(20)]
        public string RecieveUnit { get; set; }

        /// <summary>
        /// 提交序列号，每次提交只生成一个序列号,0=没有提交序号
        /// </summary>
        [Comment("提交序列号")]
        public int CommitSerialNo { get; set; } = 0;

        /// <summary>
        /// 单号序号，同一个处方单，单号从1开始递增
        /// </summary>
        [Comment("单号序号，同一个处方单，单号从1开始递增")]
        public int SequenceNo { get; set; }

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
        /// 医保统一名称
        /// </summary>
        [Comment("医保统一名称")]
        [StringLength(50)]
        public string MeducalInsuranceName { get; set; }

        /// <summary>
        /// 分方标记
        /// </summary>
        [Comment("分方标记")]
        [StringLength(50)]
        public string PrescriptionFlag { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        [Comment("区域编码")]
        [StringLength(50)]
        public string AreaCode { get; set; }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="status"></param>
        public void UpdateStatus(ERecipeStatus status)
        {
            Status = status;
        }

        /// <summary>
        /// 创建一个提交序列号
        /// </summary>
        /// <param name="commitSerialNo"></param>
        public void CreateCommitSerialNo(int commitSerialNo)
        {
            CommitSerialNo = commitSerialNo;
        }

        #region 医嘱扩展

        /// <summary>
        /// 学名
        /// </summary>
        [StringLength(200)]
        [Comment("学名")]
        public string ScientificName { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        [StringLength(200)]
        [Comment("别名")]
        public string Alias { get; set; }

        /// <summary>
        /// 别名拼音
        /// </summary>
        [StringLength(50)]
        [Comment("别名拼音")]
        public string AliasPyCode { get; set; }

        /// <summary>
        /// 别名五笔码
        /// </summary>
        [StringLength(50)]
        [Comment("别名五笔码")]
        public string AliasWbCode { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary> 
        [StringLength(50)]
        [Comment("拼音码")]
        public string PyCode { get; set; }

        /// <summary>
        /// 五笔
        /// </summary> 
        [StringLength(50)]
        [Comment("五笔")]
        public string WbCode { get; set; }

        /// <summary>
        /// 是否是加收价格 true=是加收价格， false=不是加收价格
        /// </summary>
        [Comment("是否是加收价格 1=是加收价格， 0=不是加收价格")]
        public bool IsAdditionalPrice { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        [Comment("来源")]
        public int SourceType { get; set; }

        /// <summary>
        /// 附加类型
        /// </summary>
        [Comment("附加类型")]
        public int Additional { get; set; }

        /// <summary>
        /// 是否已打印
        /// </summary>
        [Comment("是否已打印")]
        public bool? IsPrint { get; set; }

        /// <summary>
        /// 设置拼音，五笔，别名，别名拼音，别名五笔 属性
        /// </summary>
        /// <param name="pyCode">拼音</param>
        /// <param name="wbCode">五笔</param>
        /// <param name="scientificName">学名</param>
        /// <param name="alias">别名</param>
        /// <param name="aliasPyCode">别名拼音</param>
        /// <param name="aliasWbCode">别名五笔</param>
        public void SetExtendedAttributes(string pyCode, string wbCode, string scientificName, string alias,
            string aliasPyCode, string aliasWbCode)
        {
            //拼音码
            PyCode = pyCode.IsNullOrEmpty() ? PyCode = Name.FirstLetterPY() : pyCode;
            //五笔
            WbCode = wbCode.IsNullOrEmpty() ? WbCode = Name.FirstLetterWB() : wbCode;

            ScientificName = scientificName;
            Alias = alias;
            AliasPyCode = aliasPyCode;
            AliasWbCode = aliasWbCode;
        }

        #endregion

        /// <summary>
        /// 执行
        /// </summary>
        public void Exec(string executorCode, string executorName, DateTime execTime)
        {
            ExecutorCode = executorCode;
            ExecutorName = executorName;
            ExecTime = execTime;
            Status = ERecipeStatus.Executed;
        }

        /// <summary>
        /// 克隆之后需要重置的属性
        /// </summary> 
        public void AfterCloneAndResetProp(Guid id, string applyDeptCode, string applyDeptName, string applyDoctorCode,
            string applyDoctorName, int detailId)
        {
            Id = id;
            Status = ERecipeStatus.Saved; //重置状态
            PayStatus = EPayStatus.NoPayment; //重置支付状态
            IsRecipePrinted = false;
            //ExecDeptCode = String.Empty;
            //ExecDeptName = String.Empty;
            //ExecutorCode = String.Empty;
            //ExecutorName = String.Empty; 
            ExecTime = null;
            StopDateTime = null;
            StopDoctorCode = string.Empty;
            StopDoctorName = string.Empty;
            StartTime = null;
            EndTime = null;
            ApplyTime = DateTime.Now;
            ApplyDeptCode = applyDeptCode;
            ApplyDeptName = applyDeptName;
            ApplyDoctorCode = applyDoctorCode;
            ApplyDoctorName = applyDoctorName;
            DetailId = detailId;
            //Additional = 0;
            SourceType = 5;
            IsPrint = null;
        }

        /// <summary>
        /// 重新设置医嘱号，医嘱组号
        /// </summary>
        public void ReSetRecipeNo(string recipeNo, int recipeGroupNo, Guid? creatorId)
        {
            RecipeNo = recipeNo;
            RecipeGroupNo = recipeGroupNo;
            PrescriptionNo = "";
            base.CreationTime = DateTime.Now;
            base.CreatorId = creatorId;
        }

        /// <summary>
        /// 清除提交产生的信息
        /// </summary>
        public void CleanCommitInfo()
        {
            CommitSerialNo = 0;
            SequenceNo = 0;
        }

        /// <summary>
        /// 更新停嘱信息
        /// </summary>
        public void UpdateStopInfo(string stopDoctorCode, string stopDoctorName, DateTime? stopDateTime)
        {
            StopDoctorCode = stopDoctorCode;
            StopDoctorName = stopDoctorName;
            StopDateTime = stopDateTime;
            Status = ERecipeStatus.Stopped;
        }

        /// <summary>
        /// 第一次更新
        /// </summary> 
        public void UpdatePartial1(
            string applyDoctorCode,
            string applyDoctorName,
            string applyDeptCode,
            string applyDeptName,
            string traineeCode,
            string traineeName,
            bool? isChronicDisease,
            string diagnosis)
        {
            ApplyDoctorCode = applyDoctorCode;
            ApplyDoctorName = applyDoctorName;
            ApplyDeptCode = applyDeptCode;
            ApplyDeptName = applyDeptName;
            TraineeCode = traineeCode;
            TraineeName = traineeName;
            IsChronicDisease = isChronicDisease;
            Diagnosis = diagnosis;
        }

        /// <summary>
        /// 部分字段更新,处理DTO 和实体不一致automapper 的问题
        /// </summary>
        public void UpdatePartial2(DoctorsAdvicePartial obj)
        {
            Code = obj.Code;
            Name = obj.Name;
            PositionCode = obj.PositionCode;
            PositionName = obj.PositionName;
            Unit = obj.Unit.IsNullOrEmpty() ? "次" : obj.Unit;
            Price = obj.Price;
            InsuranceCode = obj.InsuranceCode;
            InsuranceType = obj.InsuranceType;
            PayTypeCode = obj.PayTypeCode;
            PayType = obj.PayType;
            PrescriptionNo = obj.PrescriptionNo;
            RecipeNo = obj.RecipeNo;
            RecipeGroupNo = obj.RecipeGroupNo;
            ApplyTime = obj.ApplyTime.HasValue ? obj.ApplyTime.Value : DateTime.Now;
            CategoryCode = obj.CategoryCode;
            CategoryName = obj.CategoryName;
            IsBackTracking = obj.IsBackTracking;
            IsRecipePrinted = obj.IsRecipePrinted;
            HisOrderNo = obj.HisOrderNo;
            ExecDeptCode = obj.ExecDeptCode;
            ExecDeptName = obj.ExecDeptName;
            Remarks = obj.Remarks;
            ChargeCode = obj.ChargeCode;
            ChargeName = obj.ChargeName;
            PrescribeTypeCode = obj.PrescribeTypeCode;
            PrescribeTypeName = obj.PrescribeTypeName;
            StartTime = obj.StartTime;
            EndTime = obj.EndTime;
            RecieveQty = obj.RecieveQty == 0 ? 1 : obj.RecieveQty;
            RecieveUnit = obj.RecieveUnit.IsNullOrEmpty() ? "次" : obj.RecieveUnit;
        }

        /// <summary>
        /// 更新可变数据操作
        /// </summary>
        public void Update(
            string code,
            string name,
            string categoryCode,
            string categoryName,
            bool isBackTracking,
            string prescriptionNo,
            DateTime applyTime,
            string applyDoctorCode,
            string applyDoctorName,
            string applyDeptCode,
            string applyDeptName,
            string traineeCode,
            string traineeName,
            string payTypeCode,
            ERecipePayType payType,
            decimal price,
            decimal amount, //todo 需要后端算
            string insuranceCode,
            EInsuranceCatalog insuranceType,
            bool? isChronicDisease,
            bool isRecipePrinted,
            string hisOrderNo,
            string diagnosis,
            string positionCode,
            string positionName,
            string remarks,
            string chargeCode,
            string chargeName,
            string prescribeTypeCode,
            string prescribeTypeName,
            DateTime? startTime,
            DateTime? endTime,
            decimal recieveQty,
            string recieveUnit,
            string execDeptCode,
            string execDeptName,
            bool isAdditionalPrice = false)
        {
            Code = code;
            Name = name;
            CategoryCode = categoryCode;
            CategoryName = categoryName;
            IsBackTracking = isBackTracking;
            PrescriptionNo = prescriptionNo;
            ApplyTime = applyTime;
            ApplyDoctorCode = applyDoctorCode;
            ApplyDoctorName = applyDoctorName;
            ApplyDeptCode = applyDeptCode;
            ApplyDeptName = applyDeptName;
            TraineeCode = traineeCode;
            TraineeName = traineeName;
            PayTypeCode = payTypeCode;
            PayType = payType;
            Price = price;
            Amount = amount;
            InsuranceCode = insuranceCode;
            InsuranceType = insuranceType;
            IsChronicDisease = isChronicDisease;
            IsRecipePrinted = isRecipePrinted;
            HisOrderNo = hisOrderNo;
            Diagnosis = diagnosis;
            PositionCode = positionCode;
            PositionName = positionName;
            Remarks = remarks;
            ChargeCode = chargeCode;
            ChargeName = chargeName;
            PrescribeTypeCode = prescribeTypeCode;
            PrescribeTypeName = prescribeTypeName;
            StartTime = startTime;
            EndTime = endTime;
            RecieveQty = recieveQty;
            RecieveUnit = recieveUnit;
            ExecDeptCode = execDeptCode;
            ExecDeptName = execDeptName;
            IsAdditionalPrice = isAdditionalPrice;
        }

        public void ComputePrice()
        {
            Amount = RecieveQty * Price;
        }

        /// <summary>
        /// 构建项目部分
        /// </summary> 
        public void BuildProject(
            string code,
            string name,
            string categoryCode,
            string categoryName,
            bool isBackTracking,
            string prescriptionNo,
            string payTypeCode,
            ERecipePayType payType,
            decimal price,
            string unit,
            string insuranceCode,
            EInsuranceCatalog insuranceType,
            string hisOrderNo,
            string executorCode,
            string executorName,
            string remarks,
            string chargeCode,
            string chargeName,
            DateTime? startTime = null)
        {
            Code = code;
            Name = name;
            CategoryCode = categoryCode;
            CategoryName = categoryName;
            IsBackTracking = isBackTracking;
            PrescriptionNo = prescriptionNo;
            PayTypeCode = payTypeCode;
            PayType = payType;
            Price = price;
            Unit = unit;
            InsuranceCode = insuranceCode;
            InsuranceType = insuranceType;
            HisOrderNo = hisOrderNo;
            Remarks = remarks;
            ChargeCode = chargeCode;
            ChargeName = chargeName;
            ExecutorCode = executorCode;
            ExecutorName = executorName;
            StartTime = startTime;
            ExecTime = null;
        }

        public void SetId(Guid id)
        {
            Id = id;
        }
        public void ReSetAmount(decimal amount)
        {
            Amount = amount;
        }

        /// <summary>
        /// 套餐更新
        /// </summary> 
        public void BuildSetMeal(
            EDoctorsAdviceItemType itemType,
            decimal amount,
            string recipeNo,
            int recipeGroupNo,
            string positionCode,
            string positionName,
            decimal recieveQty,
            string recieveUnit,
            bool isAdditionalPrice = false,
            decimal? price = null
            )
        {
            ItemType = itemType;
            Amount = amount;
            RecipeNo = recipeNo;
            RecipeGroupNo = recipeGroupNo;
            PositionCode = positionCode;
            PositionName = positionName;
            RecieveQty = recieveQty;
            RecieveUnit = recieveUnit;
            IsAdditionalPrice = isAdditionalPrice;
            if (PyCode.IsNullOrEmpty())
            {
                PyCode = Name.FirstLetterPY();
            }

            if (WbCode.IsNullOrEmpty())
            {
                WbCode = Name.FirstLetterWB();
            }
            if (price != null)
            {
                Price = price.Value;
            }
        }

        /// <summary>
        /// 更新组医嘱属性信息，包括 频次，途径，天数，执行科室
        /// </summary>
        public void UpdateGroupAdviceProp(string prescribeTypeCode,
            string prescribeTypeName,
            string execDeptCode,
            string execDeptName)
        {
            PrescribeTypeCode = prescribeTypeCode;
            PrescribeTypeName = prescribeTypeName;
            ExecDeptCode = execDeptCode;
            ExecDeptName = execDeptName;
        }

        /// <summary>
        /// 更新价格和是否是附加项
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="isAdditionalPrice"></param>
        public void UpdateAmount(decimal amount, bool isAdditionalPrice)
        {
            Amount = amount;
            IsAdditionalPrice = isAdditionalPrice;
        }

        /// <summary>
        /// 重新计算领量
        /// </summary>
        /// <returns></returns>
        public void ComputeQty(Prescribe p)
        {

            if (ItemType != EDoctorsAdviceItemType.Prescribe) return;
            if (!p.FrequencyTimes.HasValue) return;
            var num = p.FrequencyTimes.Value * p.LongDays;

            switch (p.Unpack)
            {
                case EMedicineUnPack.RoundByMinUnitAmount: //0

                    if (p.DosageUnit.Trim() == p.DefaultDosageUnit.Trim())
                    {
                        // 剂量单位===默认剂量单位
                        // 数量=Ceiling((次数*Ceiling((开的剂量/规格剂量))/小包装拆零系数)) 
                        RecieveQty = Math.Ceiling((num * Math.Ceiling(p.DosageQty / p.DefaultDosageQty)) / p.SmallPackFactor);
                    }
                    else if (p.DosageUnit.Trim() == Unit.Trim())
                    {
                        // 剂量单位===基本单位
                        // 数量=Ceiling(次数*Ceiling(开的剂量))/小包装拆零系数
                        RecieveQty = Math.Ceiling(num * Math.Ceiling(p.DosageQty) / p.SmallPackFactor);
                    }
                    else if (p.DosageUnit.Trim() == p.BigPackUnit || p.DosageUnit.Trim() == p.SmallPackUnit.Trim())
                    {
                        // 剂量单位===大包装单位
                        // 数量=Ceiling(次数*开的剂量)
                        RecieveQty = Math.Ceiling(num * p.DosageQty);
                    }

                    break;
                case EMedicineUnPack.RoundByPackUnitAmount: //1
                    if (p.DosageUnit.Trim() == p.DefaultDosageUnit.Trim())
                    {
                        // 剂量单位===默认剂量单位
                        // 数量=Ceiling((次数*(开的剂量/规格剂量))/小包装拆零系数)) 
                        RecieveQty = Math.Ceiling((num * (p.DosageQty / p.DefaultDosageQty) / p.SmallPackFactor));
                    }
                    else if (p.DosageUnit.Trim() == Unit.Trim())
                    {
                        // 剂量单位===基本单位
                        // 数量=Ceiling(次数*开的剂量)/小包装拆零系数
                        RecieveQty = Math.Ceiling((num * p.DosageQty) / p.SmallPackFactor);
                    }
                    else if (p.DosageUnit.Trim() == p.BigPackUnit.Trim() || p.DosageUnit.Trim() == p.SmallPackUnit.Trim())
                    {
                        // 剂量单位===大包装单位
                        // 数量=Ceiling(次数*开的剂量) 
                        RecieveQty = Math.Ceiling(num * p.DosageQty);
                    }

                    break;
                case EMedicineUnPack.RoundByMinUnitTime: //2
                    if (p.DosageUnit.Trim() == p.DefaultDosageUnit.Trim())
                    {
                        // 剂量单位===默认剂量单位
                        // 数量= Ceiling(次数*Ceiling(开的剂量/规格剂量)/大包装拆零系数)  
                        RecieveQty = Math.Ceiling(num * Math.Ceiling((Math.Ceiling(p.DosageQty / p.DefaultDosageQty)) / p.BigPackFactor));
                    }
                    else if (p.DosageUnit.Trim() == Unit)
                    {
                        // 剂量单位===基本单位
                        // 数量=Ceiling(次数*Ceiling(开的剂量)/大包装拆零系数)
                        RecieveQty = Math.Ceiling(num * Math.Ceiling((Math.Ceiling(p.DosageQty) / p.BigPackFactor)));
                    }
                    else if (p.DosageUnit.Trim() == p.SmallPackUnit.Trim())
                    {
                        // 剂量单位===小包装单位
                        // 数量=次数*Ceiling(开的剂量)
                        RecieveQty = Math.Ceiling(p.DosageQty) * num;
                    }
                    else if (p.DosageUnit.Trim() == p.BigPackUnit.Trim())
                    {
                        // 剂量单位===大包装单位
                        // 数量=Ceiling(次数*开的剂量*小包装拆零系数/大包装拆零系数) 
                        RecieveQty = Math.Ceiling(((p.SmallPackFactor / p.BigPackFactor) * p.DosageQty) * num);
                    }

                    break;
                case EMedicineUnPack.RoundByPackUnitTime: //3 
                    if (p.DosageUnit.Trim() == p.DefaultDosageUnit.Trim())
                    {
                        // 剂量单位===默认剂量单位
                        // 数量=Ceiling(次数*开的剂量/规格剂量/大包装拆零系数)
                        RecieveQty = Math.Ceiling(num * ((p.DosageQty / p.DefaultDosageQty) / p.BigPackFactor));
                    }
                    else if (p.DosageUnit == Unit)
                    {
                        // 剂量单位===基本单位
                        // 数量=Ceiling(次数*开的剂量/大包装拆零系数)
                        RecieveQty = Math.Ceiling((p.DosageQty / p.BigPackFactor) * num);
                    }
                    else if (p.DosageUnit.Trim() == p.SmallPackUnit.Trim())
                    {
                        // 剂量单位===小包装单位
                        // 数量=Ceiling(次数*开的剂量)
                        RecieveQty = Math.Ceiling(p.DosageQty * num);
                    }
                    else if (p.DosageUnit.Trim() == p.BigPackUnit.Trim())
                    {
                        // 剂量单位===大包装单位
                        // 数量=Ceiling(次数*Ceiling(开的剂量*小包装拆零系数/大包装拆零系数))
                        RecieveQty = Math.Ceiling(Math.Ceiling((p.SmallPackFactor / p.BigPackFactor) * p.DosageQty) * num);
                    }

                    break;
            }

            var hisDosageQty = p.HisDosageQty == 0 ? p.HisDosageQty : p.DefaultDosageQty;

            if (p.DosageForm == "口服液" || p.DosageForm == "糖浆剂" || p.DosageForm == "滴眼剂")
            {
                RecieveQty = Math.Ceiling((p.DosageQty * num) / (hisDosageQty * p.BigPackFactor));
            }
            else
            {
                var recieveQty = Math.Ceiling(p.DosageQty / hisDosageQty) * num;
                RecieveQty = Math.Ceiling(recieveQty / p.BigPackFactor);
            }

            if (p.UsageName == "涂患处")
            {
                RecieveQty = 1;
            }

            Amount = Price * RecieveQty;
        }
    }
}