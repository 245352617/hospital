using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 病人入科登记确认
    /// </summary>
    public class PatientIndeptDto
    {
        /// <summary>
        /// 床位号
        /// </summary>
        public string BedNum { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string InHosId { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNum { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string IdentityType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 血型
        /// </summary>
        public string BloodType { get; set; }

        /// <summary>
        /// RH血型
        /// </summary>
        public string RhBloodType { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string LinkPhoneNum { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string LinkAddress { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime? InDeptTime { get; set; }

        /// <summary>
        /// 原入科来源取值（0：其他， 1:产房，2：产科，3：门诊，4：急诊科，5：手术室，6：外院转入）
        /// 现入科来源取值 7-入院、5-手术、6-外院转入、8-转入、9-医联体医院转入
        /// </summary>
        public int? InSource { get; set; }

        /// <summary>
        /// 来源科室代码
        /// </summary>
        public string InDeptCode { get; set; }

        /// <summary>
        /// 来源科室名称
        /// </summary>
        public string InDeptName { get; set; }

        /// <summary>
        /// 入科计划（0：非计划转入，1：计划转入）
        /// </summary>
        public string InPlan { get; set; }

        /// <summary>
        /// 转入原因（非计划转入原因：1：缺少病情变化的预警，2：手术因素，3：麻醉因素，
        /// 计划转入原因：4：危及生命的急性器官功能不全，5：具有潜在危及生命的高危因素，6：慢性器官功能不全急性加重，7：围手术期危重患者，0：其他）
        /// </summary>
        public int? InReason { get; set; }

        /// <summary>
        /// 转入标准
        /// </summary>
        public string InStandard { get; set; }

        /// <summary>
        /// 转出标准
        /// </summary>
        public string OutStandard { get; set; }

        /// <summary>
        /// 是否医生确认
        /// </summary>
        public bool? IsDoctorConfirm { get; set; }

        /// <summary>
        /// 重返（0：否，1：24小时重返，2：48小时重返）
        /// </summary>
        public int? InReturn { get; set; }

        /// <summary>
        /// 主管医生
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 主管医生名称
        /// </summary>
        public string DoctorName { get; set; }


        /// <summary>
        /// 专科医生
        /// </summary>
        public string SpDoctorCode { get; set; }

        /// <summary>
        /// 专科医生名称
        /// </summary>
        public string SpDoctorName { get; set; }

        /// <summary>
        /// 责任护士
        /// </summary>
        public string NurseCode { get; set; }

        /// <summary>
        /// 责任护士名称
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 危重程度（0：其他，1：病危，2：病重）
        /// </summary>
        public int? CriticaDegree { get; set; }

        /// <summary>
        /// 护理级别（1：一级护理，2：二级护理，3：三级护理，4.特级护理）
        /// </summary>
        public string NurseGrade { get; set; }

        /// <summary>
        /// 护理类型（0：其他，1：基础护理，2：特殊护理，3：辩证施护）
        /// </summary>
        public int? NurseType { get; set; }

        /// <summary>
        /// 过敏史
        /// </summary>
        public string Allergy { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        public string PreHistory { get; set; }

        /// <summary>
        /// 入科诊断
        /// </summary>
        public string Indiagnosis { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>
        public string ClinicDiagnosis { get; set; }

        /// <summary>
        /// 是否在科（1：在科；0：出科；2，取消入科，3:待入科，4:待出科)
        /// </summary>
        public int InDeptState { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        public DateTime? OutDeptTime { get; set; }

        /// <summary>
        /// 出科转归(1：出院，2：转出，3：死亡，4：转上级医院，5：转医联体医院）
        /// </summary>
        public int? OutTurnover { get; set; }

        /// <summary>
        /// 出科状态(1：恶化，2：好转，3：未愈)
        /// </summary>
        public int? OutState { get; set; }

        /// <summary>
        /// 转出科室
        /// </summary>
        public string OutDeptCode { get; set; }

        /// <summary>
        /// 转出科室名称
        /// </summary>
        public string OutDeptName { get; set; }

        /// <summary>
        /// 出科诊断
        /// </summary>
        public string Outdiagnosis { get; set; }

        /// <summary>
        /// 入科交接护士Id
        /// </summary>
        /// <example></example>
        public string InDeptNurseCode { get; set; }

        /// <summary>
        /// 入科交接护士名称
        /// </summary>
        /// <example></example>
        public string InDeptNurseName { get; set; }

        /// <summary>
        /// 入科交接时间
        /// </summary>
        /// <example></example>
        public DateTime? InDeptTransferTime { get; set; }

        /// <summary>
        /// 出科交接护士Id
        /// </summary>
        /// <example></example>
        public string OutDeptNurseCode { get; set; }

        /// <summary>
        /// 出科交接护士名称
        /// </summary>
        /// <example></example>
        public string OutDeptNurseName { get; set; }

        /// <summary>
        /// 出科交接时间
        /// </summary>
        /// <example></example>
        public DateTime? OutDeptTransferTime { get; set; }

        /// <summary>
        /// 操作护士
        /// </summary>
        public string OperationNurseId { get; set; }

        /// <summary>
        /// 操作护士名称
        /// </summary>
        public string OperationNurseName { get; set; }

        /// <summary>
        /// 病人状态
        /// 1:正常入科  入院/入科转icu实床
        /// 2:紧急入科
        /// 3:虚床转实床
        /// 4.紧急入科后，接收消息更新后
        /// </summary>
        public int? PatState { get; set; }
    }

    /// <summary>
    /// 病人入科登记确认
    /// </summary>
    public class RePatientIndeptDto
    {
        /// <summary>
        /// 床位号
        /// </summary>
        public string BedNum { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string InHosId { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNum { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string IdentityType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 血型
        /// </summary>
        public string BloodType { get; set; }

        /// <summary>
        /// RH血型
        /// </summary>
        public string RhBloodType { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string LinkPhoneNum { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string LinkAddress { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime? InDeptTime { get; set; }

        /// <summary>
        /// 原入科来源取值（0：其他， 1:产房，2：产科，3：门诊，4：急诊科，5：手术室，6：外院转入）
        /// 现入科来源取值 7-入院、5-手术、6-外院转入、8-转入、9-医联体医院转入
        /// </summary>
        public int? InSource { get; set; }

        /// <summary>
        /// 来源科室代码
        /// </summary>
        public string InDeptCode { get; set; }

        /// <summary>
        /// 来源科室名称
        /// </summary>
        public string InDeptName { get; set; }

        /// <summary>
        /// 入科计划（0：非计划转入，1：计划转入）
        /// </summary>
        public int? InPlan { get; set; }

        /// <summary>
        /// 转入原因（非计划转入原因：1：缺少病情变化的预警，2：手术因素，3：麻醉因素，
        /// 计划转入原因：4：危及生命的急性器官功能不全，5：具有潜在危及生命的高危因素，6：慢性器官功能不全急性加重，7：围手术期危重患者，0：其他）
        /// </summary>
        public int? InReason { get; set; }

        /// <summary>
        /// 转入标准
        /// </summary>
        public string InStandard { get; set; }

        /// <summary>
        /// 转出标准
        /// </summary>
        public string OutStandard { get; set; }

        /// <summary>
        /// 是否医生确认
        /// </summary>
        public bool? IsDoctorConfirm { get; set; }

        /// <summary>
        /// 重返（0：否，1：24小时重返，2：48小时重返）
        /// </summary>
        public int? InReturn { get; set; }

        /// <summary>
        /// 主管医生
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 主管医生名称
        /// </summary>
        public string DoctorName { get; set; }


        /// <summary>
        /// 专科医生
        /// </summary>
        public string SpDoctorCode { get; set; }

        /// <summary>
        /// 专科医生名称
        /// </summary>
        public string SpDoctorName { get; set; }

        /// <summary>
        /// 责任护士
        /// </summary>
        public string NurseCode { get; set; }

        /// <summary>
        /// 责任护士名称
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 危重程度（0：其他，1：病危，2：病重）
        /// </summary>
        public int? CriticaDegree { get; set; }

        /// <summary>
        /// 护理级别（1：一级护理，2：二级护理，3：三级护理，4.特级护理）
        /// </summary>
        public int? NurseGrade { get; set; }

        /// <summary>
        /// 护理类型（0：其他，1：基础护理，2：特殊护理，3：辩证施护）
        /// </summary>
        public int? NurseType { get; set; }

        /// <summary>
        /// 过敏史
        /// </summary>
        public string Allergy { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        public string PreHistory { get; set; }

        /// <summary>
        /// 入科诊断
        /// </summary>
        public string Indiagnosis { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>
        public string ClinicDiagnosis { get; set; }

        /// <summary>
        /// 是否在科（1：在科；0：出科；2，取消入科，3:待入科，4:待出科)
        /// </summary>
        public int InDeptState { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        public DateTime? OutDeptTime { get; set; }

        /// <summary>
        /// 出科转归(1：出院，2：转出，3：死亡，4：转上级医院，5：转医联体医院）
        /// </summary>
        public int? OutTurnover { get; set; }

        /// <summary>
        /// 出科状态(1：恶化，2：好转，3：未愈)
        /// </summary>
        public int? OutState { get; set; }

        /// <summary>
        /// 转出科室
        /// </summary>
        public string OutDeptCode { get; set; }

        /// <summary>
        /// 转出科室名称
        /// </summary>
        public string OutDeptName { get; set; }

        /// <summary>
        /// 出科诊断
        /// </summary>
        public string Outdiagnosis { get; set; }

        /// <summary>
        /// 入科交接护士Id
        /// </summary>
        /// <example></example>
        public string InDeptNurseCode { get; set; }

        /// <summary>
        /// 入科交接护士名称
        /// </summary>
        /// <example></example>
        public string InDeptNurseName { get; set; }

        /// <summary>
        /// 入科交接时间
        /// </summary>
        /// <example></example>
        public DateTime? InDeptTransferTime { get; set; }

        /// <summary>
        /// 出科交接护士Id
        /// </summary>
        /// <example></example>
        public string OutDeptNurseCode { get; set; }

        /// <summary>
        /// 出科交接护士名称
        /// </summary>
        /// <example></example>
        public string OutDeptNurseName { get; set; }

        /// <summary>
        /// 出科交接时间
        /// </summary>
        /// <example></example>
        public DateTime? OutDeptTransferTime { get; set; }

        /// <summary>
        /// 操作护士
        /// </summary>
        public string OperationNurseId { get; set; }

        /// <summary>
        /// 操作护士名称
        /// </summary>
        public string OperationNurseName { get; set; }

        /// <summary>
        /// 病人状态
        /// 1:正常入科  入院/入科转icu实床
        /// 2:紧急入科
        /// 3:虚床转实床
        /// 4.紧急入科后，接收消息更新后
        /// </summary>
        public int? PatState { get; set; }
    }
}
