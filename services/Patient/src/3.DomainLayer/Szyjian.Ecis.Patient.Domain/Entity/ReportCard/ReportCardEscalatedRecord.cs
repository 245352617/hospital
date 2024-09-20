using FreeSql.DataAnnotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 报卡上报记录表
    /// </summary>
    [Table(Name = "Pat_ReportCardEscalatedRecord")]
    public class ReportCardEscalatedRecord : FullAuditedEntity<Guid>
    {
        #region Property
        /// <summary>
        /// 主键 GUID
        /// </summary>
        [Column(IsPrimary = true, Position = 1)]
        [Description("主键 GUID")]
        public override Guid Id { get; protected set; }

        /// <summary>
        /// 患者id
        /// </summary>
        [Description("患者id")]
        [Column(IsNullable = false, Position = 2)]
        public string PatientID { get; private set; }

        /// <summary>
        /// 患者分诊id
        /// </summary>
        [Description("患者分诊id")]
        [Column(IsNullable = false, Position = 3)]
        public Guid PIID { get; private set; }

        /// <summary>
        /// 对应报卡编码
        /// </summary>
        [Description("对应报卡编码")]
        [MaxLength(50)]
        [Column(IsNullable = false, Position = 4)]
        public string ReportCardCode { get; private set; }

        /// <summary>
        /// 对应报卡名
        /// </summary>
        [Description("对应报卡名")]
        [MaxLength(50)]
        [Column(IsNullable = false, Position = 4)]
        public string ReportCardName { get; private set; }

        /// <summary>
        /// 是否已上报
        /// </summary>
        [Description("是否已上报")]
        [Column(IsNullable = false, Position = 6)]
        public bool IsEscalated { get; private set; }
        #endregion

        #region Methon
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="PatientID"></param>
        /// <param name="piid">患者分诊id</param>
        /// <param name="reportCardCode">对应报卡编码</param>
        /// <param name="reportCardName"></param>
        /// <param name="isEscalated">是否已上报</param>
        public ReportCardEscalatedRecord(Guid id, string PatientID, Guid piid, string reportCardCode, string reportCardName, bool isEscalated = true)
        {
            this.Id = id;
            this.PatientID = PatientID;
            this.PIID = piid;
            this.ReportCardCode = reportCardCode;
            this.ReportCardName = reportCardName;
            this.IsEscalated = isEscalated;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="PatientID"></param>
        /// <param name="piid">患者分诊id</param>
        /// <param name="reportCardCode">对应报卡编码</param>
        /// <param name="reportCardName"></param>
        /// <param name="creatorId">创建人ID</param>
        /// <param name="isEscalated">是否已上报</param>
        public ReportCardEscalatedRecord(Guid id, string PatientID, Guid piid, string reportCardCode, string reportCardName, Guid? creatorId, bool isEscalated = true)
        {
            this.Id = id;
            this.PatientID = PatientID;
            this.PIID = piid;
            this.ReportCardCode = reportCardCode;
            this.ReportCardName = reportCardName;
            this.IsEscalated = isEscalated;
            this.CreationTime = DateTime.Now;
            this.CreatorId = creatorId;
        }
        #endregion
    }
}