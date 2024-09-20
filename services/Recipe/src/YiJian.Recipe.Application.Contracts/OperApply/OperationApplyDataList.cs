using System;

namespace YiJian.Recipe
{
    public class OperationApplyDataList
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 分诊患者id
        /// </summary>
        public Guid PI_Id { get; set; }
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
        /// 拟施手术编码
        /// </summary>
        public string ProposedOperationCode { get; set; }
        /// <summary>
        /// 拟施手术名称
        /// </summary>
        public string ProposedOperationName { get; set; }

        /// <summary>
        /// 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
        /// </summary>
        public OperationApplyStatus Status { get; set; }

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