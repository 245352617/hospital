using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;

namespace YiJian.Recipe
{
    /// <summary>
    /// 手术申请
    /// </summary>
    [Comment("手术申请")]
    public class OperationApply : FullAuditedAggregateRoot<Guid>
    {
        public OperationApply Set(Guid id, DateTime? applyTime)
        {
            this.Id = id;
            ApplyTime = applyTime;
            return this;
        }

        /// <summary>
        /// 分诊患者id
        /// </summary>
        [Comment("分诊患者id")]
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        [Comment("患者唯一标识(HIS)")]
        [StringLength(20)]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [Comment("患者姓名")]
        [StringLength(50)]
        public string PatientName { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        [Comment("患者性别")]
        [StringLength(50)]
        public string Sex { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        [Comment("患者性别")]
        [StringLength(50)]
        public string SexName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [Comment("年龄")]
        [StringLength(50)]
        public string Age { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [Comment("身份证号")]
        [StringLength(20)]
        public string IDNo { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Comment("生日")]
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        [Comment("申请单号")]
        [StringLength(20)]
        public string ApplyNum { get; set; }

        /// <summary>
        /// 申请人Id
        /// </summary>
        [Comment("申请人Id")]
        public string ApplicantId { get; set; }

        /// <summary>
        /// 申请人名称
        /// </summary>
        [Comment("申请人名称")]
        [StringLength(50)]
        public string ApplicantName { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        [Comment("申请时间")]
        public DateTime? ApplyTime { get; private set; }

        /// <summary>
        /// 血型
        /// </summary>
        [Comment("血型")]
        [StringLength(50)]
        public string BloodType { get; set; }

        /// <summary>
        /// RH阴性阳性
        /// </summary>
        [Comment("RH阴性阳性")]
        [StringLength(50)]
        public string RHCode { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        [Comment("身高")]
        public decimal Height { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        [Comment("体重")]
        public decimal Weight { get; set; }

        /// <summary>
        /// 拟施手术编码
        /// </summary>
        [Comment("拟施手术编码")]
        [StringLength(1000)]
        public string ProposedOperationCode { get; set; }

        /// <summary>
        /// 拟施手术名称
        /// </summary>
        [Comment("拟施手术名称")]
        [StringLength(4000)]
        public string ProposedOperationName { get; set; }

        /// <summary>
        /// 手术等级
        /// </summary>
        [Comment("手术等级")]
        [StringLength(50)]
        public string OperationLevel { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary>
        [Comment("申请科室编码")]
        [StringLength(50)]
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请科室名称
        /// </summary>
        [Comment("申请科室名称")]
        [StringLength(50)]
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 手术位置
        /// </summary>
        [Comment("手术位置")]
        [StringLength(50)]
        public string OperationLocation { get; set; }

        /// <summary>
        /// 手术医生编码
        /// </summary>
        [Comment("手术医生编码")]
        [StringLength(50)]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 手术医生名称
        /// </summary>
        [Comment("手术医生名称")]
        [StringLength(50)]
        public string DoctorName { get; set; }

        /// <summary>
        /// 手术助手
        /// </summary>
        [Comment("手术助手")]
        [StringLength(4000)]
        public string OperationAssistant { get; set; }

        /// <summary>
        /// 手术计划时间
        /// </summary>
        [Comment("手术计划时间")]
        public DateTime? OperationPlanTime { get; set; }

        /// <summary>
        /// 手术时长
        /// </summary>
        [Comment("手术时长")]
        public string OperationDuration { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Comment("备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
        /// </summary>
        [Comment("手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回")]
        public OperationApplyStatus Status { get; set; }

        /// <summary>
        /// 手术申请提交日期
        /// </summary>
        [Comment("手术申请提交日期")]
        public DateTime? SubmitDate { get; set; }

        /// <summary>
        /// 手术类型
        /// </summary>
        [Comment("手术类型")]
        [StringLength(50)]
        public string OperationType { get; set; }

        /// <summary>
        /// 术前诊断编码
        /// </summary>
        [Comment("术前诊断编码")]
        [StringLength(200)]
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 术前诊断名称
        /// </summary>
        [Comment("术前诊断名称")]
        [StringLength(1000)]
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 麻醉医生
        /// </summary>
        [Comment("麻醉医生")]
        [StringLength(50)]
        public string Anesthesiologist { get; set; }

        /// <summary>
        /// 麻醉助手
        /// </summary>
        [Comment("麻醉助手")]
        [StringLength(50)]
        public string AnesthesiologistAssistant { get; set; }

        /// <summary>
        /// 巡回护士
        /// </summary>
        [Comment("巡回护士")]
        [StringLength(50)]
        public string TourNurse { get; set; }

        /// <summary>
        /// 器械护士
        /// </summary>
        [Comment("器械护士")]
        [StringLength(50)]
        public string InstrumentNurse { get; set; }

        /// <summary>
        /// 麻醉方式编码
        /// </summary>
        [Comment("麻醉方式编码")]
        [StringLength(50)]
        public string AnaestheticCode { get; set; }

        /// <summary>
        /// 麻醉方式名称
        /// </summary>
        [Comment("麻醉方式名称")]
        [StringLength(50)]
        public string AnaestheticName { get; set; }

        /// <summary>
        /// 手术台次
        /// </summary>
        [Comment("手术台次")]
        public int Sequence { get; set; }

        #region constructor

        /// <summary>
        /// 分诊患者id构造器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pIId">分诊患者id</param>
        /// <param name="patientId">患者唯一标识(HIS)</param>
        /// <param name="patientName">患者姓名</param>
        /// <param name="sex">患者性别</param>
        /// <param name="sexName"></param>
        /// <param name="age">年龄</param>
        /// <param name="iDNo">身份证号</param>
        /// <param name="birthDay">生日</param>
        /// <param name="applyNum">申请单号</param>
        /// <param name="applicantId">申请人Id</param>
        /// <param name="applicantName">申请人名称</param>
        /// <param name="applyTime">申请时间</param>
        /// <param name="bloodType">血型</param>
        /// <param name="rHCode">RH阴性阳性</param>
        /// <param name="height">身高</param>
        /// <param name="weight">体重</param>
        /// <param name="proposedOperationCode">拟施手术编码</param>
        /// <param name="proposedOperationName">拟施手术名称</param>
        /// <param name="operationLevel">手术等级</param>
        /// <param name="applyDeptCode">申请科室编码</param>
        /// <param name="applyDeptName">申请科室名称</param>
        /// <param name="operationLocation">手术位置</param>
        /// <param name="doctorCode">手术医生编码</param>
        /// <param name="doctorName">手术医生名称</param>
        /// <param name="operationAssistant">手术助手</param>
        /// <param name="operationPlanTime">手术计划时间</param>
        /// <param name="operationDuration">手术时长</param>
        /// <param name="remark">备注</param>
        /// <param name="status">手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回</param>
        /// <param name="submitDate">手术申请提交日期</param>
        /// <param name="operationType">手术类型</param>
        /// <param name="diagnoseCode">术前诊断编码</param>
        /// <param name="diagnoseName">术前诊断名称</param>
        /// <param name="anesthesiologist">麻醉医生</param>
        /// <param name="anesthesiologistAssistant">麻醉助手</param>
        /// <param name="tourNurse">巡回护士</param>
        /// <param name="instrumentNurse">器械护士</param>
        /// <param name="anaestheticCode">麻醉方式编码</param>
        /// <param name="anaestheticName">麻醉方式名称</param>
        /// <param name="sequence">手术台次</param>
        public OperationApply(Guid id,
            Guid pIId, // 分诊患者id
            string patientId, // 患者唯一标识(HIS)
            string patientName, // 患者姓名
            string sex, // 患者性别
            string sexName,
            string age, // 年龄
            string iDNo, // 身份证号
            DateTime? birthDay, // 生日
            string applyNum, // 申请单号
            string applicantId, // 申请人Id
            string applicantName, // 申请人名称
            DateTime? applyTime, // 申请时间
            string bloodType, // 血型
            string rHCode, // RH阴性阳性
            decimal height, // 身高
            decimal weight, // 体重
            string proposedOperationCode, // 拟施手术编码
            string proposedOperationName, // 拟施手术名称
            string operationLevel, // 手术等级
            string applyDeptCode, // 申请科室编码
            string applyDeptName, // 申请科室名称
            string operationLocation, // 手术位置
            string doctorCode, // 手术医生编码
            string doctorName, // 手术医生名称
            string operationAssistant, // 手术助手
            DateTime? operationPlanTime, // 手术计划时间
            string operationDuration, // 手术时长
            string remark, // 备注
            OperationApplyStatus status, // 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
            DateTime? submitDate, // 手术申请提交日期
            string operationType, // 手术类型
            string diagnoseCode, // 术前诊断编码
            string diagnoseName, // 术前诊断名称
            string anesthesiologist, // 麻醉医生
            string anesthesiologistAssistant, // 麻醉助手
            string tourNurse, // 巡回护士
            string instrumentNurse, // 器械护士
            string anaestheticCode, // 麻醉方式编码
            string anaestheticName, // 麻醉方式名称
            int sequence // 手术台次
        ) : base(id)
        {
            //分诊患者id
            PI_Id = pIId;
            //患者唯一标识(HIS)
            PatientId = Check.Length(patientId, "患者唯一标识(HIS)", OperationApplyConsts.MaxPatientIdLength);

            //患者姓名
            PatientName = Check.Length(patientName, "患者姓名", OperationApplyConsts.MaxPatientNameLength);

            //患者性别
            Sex = Check.Length(sex, "患者性别", OperationApplyConsts.MaxSexLength);
            SexName = sexName;
            //年龄
            Age = Check.Length(age, "年龄", OperationApplyConsts.MaxAgeLength);

            //身份证号
            IDNo = Check.Length(iDNo, "身份证号", OperationApplyConsts.MaxIDNoLength);

            //生日
            BirthDay = birthDay;
            Modify(
                bloodType, // 血型
                rHCode, // RH阴性阳性
                height, // 身高
                weight, // 体重
                proposedOperationCode, // 拟施手术编码
                proposedOperationName, // 拟施手术名称
                operationLevel, // 手术等级
                applyDeptCode, // 申请科室编码
                applyDeptName, // 申请科室名称
                operationLocation, // 手术位置
                doctorCode, // 手术医生编码
                doctorName, // 手术医生名称
                operationAssistant, // 手术助手
                operationPlanTime, // 手术计划时间
                operationDuration, // 手术时长
                remark, // 备注
                status, // 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
                operationType, // 手术类型
                diagnoseCode, // 术前诊断编码
                diagnoseName
            );
            //麻醉医生
            Anesthesiologist = Check.Length(anesthesiologist, "麻醉医生", OperationApplyConsts.MaxAnesthesiologistLength);

            //麻醉助手
            AnesthesiologistAssistant = Check.Length(anesthesiologistAssistant, "麻醉助手",
                OperationApplyConsts.MaxAnesthesiologistAssistantLength);

            //巡回护士
            TourNurse = Check.Length(tourNurse, "巡回护士", OperationApplyConsts.MaxTourNurseLength);

            //器械护士
            InstrumentNurse = Check.Length(instrumentNurse, "器械护士", OperationApplyConsts.MaxInstrumentNurseLength);

            //麻醉方式编码
            AnaestheticCode = Check.Length(anaestheticCode, "麻醉方式编码", OperationApplyConsts.MaxAnaestheticCodeLength);

            //麻醉方式名称
            AnaestheticName = Check.Length(anaestheticName, "麻醉方式名称", OperationApplyConsts.MaxAnaestheticNameLength);

            //申请单号
            ApplyNum = Check.Length(applyNum, "申请单号", OperationApplyConsts.MaxApplyNumLength);

            //申请人Id
            ApplicantId = Check.Length(applicantId, "申请人Id", OperationApplyConsts.MaxApplicantIdLength);

            //申请人名称
            ApplicantName = Check.Length(applicantName, "申请人名称", OperationApplyConsts.MaxApplicantNameLength);

            //申请时间
            ApplyTime = applyTime;
            //手术台次
            Sequence = sequence;

            //手术申请提交日期
            SubmitDate = submitDate;
        }

        #endregion

        #region Modify

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="bloodType">血型</param>
        /// <param name="rHCode">RH阴性阳性</param>
        /// <param name="height">身高</param>
        /// <param name="weight">体重</param>
        /// <param name="proposedOperationCode">拟施手术编码</param>
        /// <param name="proposedOperationName">拟施手术名称</param>
        /// <param name="operationLevel">手术等级</param>
        /// <param name="applyDeptCode">申请科室编码</param>
        /// <param name="applyDeptName">申请科室名称</param>
        /// <param name="operationLocation">手术位置</param>
        /// <param name="doctorCode">手术医生编码</param>
        /// <param name="doctorName">手术医生名称</param>
        /// <param name="operationAssistant">手术助手</param>
        /// <param name="operationPlanTime">手术计划时间</param>
        /// <param name="operationDuration">手术时长</param>
        /// <param name="remark">备注</param>
        /// <param name="status">手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回</param>
        /// <param name="operationType">手术类型</param>
        /// <param name="diagnoseCode">术前诊断编码</param>
        /// <param name="diagnoseName">术前诊断名称</param>
        public void Modify(
            string bloodType, // 血型
            string rHCode, // RH阴性阳性
            decimal height, // 身高
            decimal weight, // 体重
            string proposedOperationCode, // 拟施手术编码
            string proposedOperationName, // 拟施手术名称
            string operationLevel, // 手术等级
            string applyDeptCode, // 申请科室编码
            string applyDeptName, // 申请科室名称
            string operationLocation, // 手术位置
            string doctorCode, // 手术医生编码
            string doctorName, // 手术医生名称
            string operationAssistant, // 手术助手
            DateTime? operationPlanTime, // 手术计划时间
            string operationDuration, // 手术时长
            string remark, // 备注
            OperationApplyStatus status, // 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
            string operationType, // 手术类型
            string diagnoseCode, // 术前诊断编码
            string diagnoseName // 术前诊断名称
        )
        {

            //血型
            BloodType = Check.Length(bloodType, "血型", OperationApplyConsts.MaxBloodTypeLength);

            //RH阴性阳性
            RHCode = Check.Length(rHCode, "RH阴性阳性", OperationApplyConsts.MaxRHCodeLength);

            //身高
            Height = height;

            //体重
            Weight = weight;

            //拟施手术编码
            ProposedOperationCode = Check.Length(proposedOperationCode, "拟施手术编码",
                OperationApplyConsts.MaxProposedOperationCodeLength);

            //拟施手术名称
            ProposedOperationName = Check.Length(proposedOperationName, "拟施手术名称",
                OperationApplyConsts.MaxProposedOperationNameLength);

            //手术等级
            OperationLevel = Check.Length(operationLevel, "手术等级", OperationApplyConsts.MaxOperationLevelLength);

            //申请科室编码
            ApplyDeptCode = Check.Length(applyDeptCode, "申请科室编码", OperationApplyConsts.MaxApplyDeptCodeLength);

            //申请科室名称
            ApplyDeptName = Check.Length(applyDeptName, "申请科室名称", OperationApplyConsts.MaxApplyDeptNameLength);

            //手术位置
            OperationLocation =
                Check.Length(operationLocation, "手术位置", OperationApplyConsts.MaxOperationLocationLength);

            //手术医生编码
            DoctorCode = Check.Length(doctorCode, "手术医生编码", OperationApplyConsts.MaxDoctorCodeLength);

            //手术医生名称
            DoctorName = Check.Length(doctorName, "手术医生名称", OperationApplyConsts.MaxDoctorNameLength);

            //手术助手
            OperationAssistant =
                Check.Length(operationAssistant, "手术助手", OperationApplyConsts.MaxOperationAssistantLength);

            //手术计划时间
            OperationPlanTime = operationPlanTime;

            //手术时长
            OperationDuration =
                Check.Length(operationDuration, "手术时长", OperationApplyConsts.MaxOperationDurationLength);

            //备注
            Remark = Check.Length(remark, "备注", OperationApplyConsts.MaxRemarkLength);

            //手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
            Status = status;


            //手术类型
            OperationType = Check.Length(operationType, "手术类型", OperationApplyConsts.MaxOperationTypeLength);

            //术前诊断编码
            DiagnoseCode = Check.Length(diagnoseCode, "术前诊断编码", OperationApplyConsts.MaxDiagnoseCodeLength);

            //术前诊断名称
            DiagnoseName = Check.Length(diagnoseName, "术前诊断名称", OperationApplyConsts.MaxDiagnoseNameLength);
        }

        #endregion

        public void Sync(
            string anesthesiologist, // 麻醉医生
            string anesthesiologistAssistant, // 麻醉助手
            string tourNurse, // 巡回护士
            string instrumentNurse, // 器械护士
            OperationApplyStatus status, // 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
            string anaestheticCode, // 麻醉方式编码
            string anaestheticName, // 麻醉方式名称
            int sequence // 手术台次

            )
        {
            //麻醉医生
            Anesthesiologist = Check.Length(anesthesiologist, "麻醉医生", OperationApplyConsts.MaxAnesthesiologistLength);

            //麻醉助手
            AnesthesiologistAssistant = Check.Length(anesthesiologistAssistant, "麻醉助手",
                OperationApplyConsts.MaxAnesthesiologistAssistantLength);

            //巡回护士
            TourNurse = Check.Length(tourNurse, "巡回护士", OperationApplyConsts.MaxTourNurseLength);

            //器械护士
            InstrumentNurse = Check.Length(instrumentNurse, "器械护士", OperationApplyConsts.MaxInstrumentNurseLength);

            //麻醉方式编码
            AnaestheticCode = Check.Length(anaestheticCode, "麻醉方式编码", OperationApplyConsts.MaxAnaestheticCodeLength);

            //麻醉方式名称
            AnaestheticName = Check.Length(anaestheticName, "麻醉方式名称", OperationApplyConsts.MaxAnaestheticNameLength);

            //手术台次
            Sequence = sequence;

            //手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
            Status = status;


        }
        #region constructor

        private OperationApply()
        {
        }

        #endregion
    }
}