using FreeSql.DataAnnotations;
using JetBrains.Annotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 报卡关联诊断设置
    /// </summary>
    [Description("报卡关联诊断设置")]
    [Table(Name = "Pat_ReportCardRelatedDiagnose")]
    public class ReportCardRelatedDiagnose : FullAuditedAggregateRoot<Guid>
    {
        #region Property
        /// <summary>
        /// 主键 GUID
        /// </summary>
        [Column(IsPrimary = true, Position = 1)]
        [Description("主键 GUID")]
        public override Guid Id { get; protected set; }

        /// <summary>
        /// 报卡Guid
        /// </summary>
        [Description("报卡Guid")]
        [Column(IsNullable = false, Position = 2)]
        public Guid ReportCardID { get; private set; }

        /// <summary>
        /// 诊断代码
        /// </summary>
        [MaxLength(50)]
        [Column(IsNullable = false, Position = 3)]
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        [MaxLength(200)]
        [Column(IsNullable = false, Position = 4)]
        public string DiagnoseName { get; set; }
        #endregion

        #region Methon
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reportCardID">报卡Guid</param>
        /// <param name="diagnoseCode">诊断代码</param>
        /// <param name="diagnoseName">诊断名称</param>
        public ReportCardRelatedDiagnose([NotNull] Guid id, [NotNull] Guid reportCardID, [NotNull] string diagnoseCode, [NotNull] string diagnoseName)
        {
            this.Id = id;
            this.ReportCardID = reportCardID;
            this.DiagnoseCode = diagnoseCode;
            this.DiagnoseName = diagnoseName;
        }

        private ReportCardRelatedDiagnose()
        {
        }
        #endregion
    }
}
