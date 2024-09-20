using System;

namespace YiJian.Recipe
{
    public class OperationApplyData
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 分诊患者id
        /// </summary>
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 患者性别
        /// </summary>
        public string SexName { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNo { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        public string ApplyNum { get; set; }

        /// <summary>
        /// 申请人Id
        /// </summary>
        public string ApplicantId { get; set; }

        /// <summary>
        /// 申请人名称
        /// </summary>
        public string ApplicantName { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 血型
        /// </summary>
        public string BloodType { get; set; }

        /// <summary>
        /// RH阴性阳性
        /// </summary>
        public string RHCode { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 拟施手术编码
        /// </summary>
        public string ProposedOperationCode { get; set; }
        /// <summary>
        /// 拟施手术名称
        /// </summary>
        public string ProposedOperationName { get; set; }
        /// <summary>
        /// 手术等级
        /// </summary>
        public string OperationLevel { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary>
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请科室名称
        /// </summary>
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 手术位置
        /// </summary>
        public string OperationLocation { get; set; }

        /// <summary>
        /// 手术医生编码
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 手术医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 手术助手
        /// </summary>
        public string OperationAssistant { get; set; }

        /// <summary>
        /// 手术计划时间
        /// </summary>
        public DateTime? OperationPlanTime { get; set; }

        /// <summary>
        /// 手术时长
        /// </summary>
        public string OperationDuration { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
        /// </summary>
        public OperationApplyStatus Status { get; set; }

        /// <summary>
        /// 手术类型
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// 术前诊断编码
        /// </summary>
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 术前诊断名称
        /// </summary>
        public string DiagnoseName { get; set; }

    }
}