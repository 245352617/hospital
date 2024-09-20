using FreeSql.DataAnnotations;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 报卡设置
    /// </summary>
    [Description("报卡设置")]
    [Table(Name = "Config_ReportCard")]
    public class ReportCard : FullAuditedAggregateRoot<Guid>
    {
        #region Property
        /// <summary>
        /// 主键 GUID
        /// </summary>
        [Column(IsPrimary = true, Position = 1)]
        [Description("主键 GUID")]
        public override Guid Id { get; protected set; }

        /// <summary>
        /// 报卡名称
        /// </summary>
        [Description("报卡名称")]
        [MaxLength(50)]
        [Column(IsNullable = false, Position = 2)]
        public string Name { get; private set; }

        /// <summary>
        /// 报卡编码
        /// </summary>
        [Description("报卡编码")]
        [MaxLength(50)]
        [Column(IsNullable = false, Position = 3)]
        public string Code { get; private set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        [Description("是否使用")]
        [Column(IsNullable = false, Position = 4)]
        public bool IsActived { get; private set; }

        /// <summary>
        /// 排序权重，数值越高排序越前，默认0
        /// </summary>
        [Description("排序权重，数值越高排序越前，默认0")]
        [Column(IsNullable = false, Position = 5)]
        public int Sort { get; set; } = 0;

        /// <summary>
        /// 关联的诊断列表
        /// </summary>
        [Navigate(nameof(ReportCardRelatedDiagnose.ReportCardID))]
        public virtual ICollection<ReportCardRelatedDiagnose> RelatedDiagnoseList { get; set; }
        #endregion

        #region Methon
        private ReportCard()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">报卡名称</param>
        /// <param name="code">报卡编码</param>
        /// <param name="isActived">是否使用</param>
        /// <param name="sort">排序</param>
        public ReportCard([NotNull] Guid id, [NotNull] string name, [NotNull] string code, bool isActived = true, int sort = 0)
        {
            this.Id = Check.NotNull(id, nameof(Id));
            this.Name = Check.NotNull(name, nameof(Name));
            this.Code = Check.NotNull(code, nameof(Code));
            this.IsActived = isActived;
            this.Sort = sort;
        }
        #endregion
    }
}