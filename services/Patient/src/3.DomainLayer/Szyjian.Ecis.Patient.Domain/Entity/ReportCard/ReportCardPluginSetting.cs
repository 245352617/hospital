using FreeSql.DataAnnotations;
using JetBrains.Annotations;
using System;
using System.ComponentModel;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Domain.Entities.Auditing;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 报卡插件设置
    /// 每次更改报卡插件设置，都会先把老的软删除，然后建立新的
    /// </summary>
    [Table(Name = "Config_ReportCardPlugin")]
    public class ReportCardPluginSetting : FullAuditedEntity<Guid>
    {
        #region Method
        /// <summary>
        /// 构造方法
        /// </summary>
        public ReportCardPluginSetting()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">主键 GUID</param>
        public ReportCardPluginSetting([NotNull] Guid id)
        {
            SetID(id);
        }

        /// <summary>
        /// 设置GUID
        /// </summary>
        public void SetID([NotNull] Guid id)
        {
            this.Id = id;
        }
        #endregion

        #region Property
        /// <summary>
        /// 主键 GUID
        /// </summary>
        [Column(IsPrimary = true, Position = 1)]
        [Description("主键 GUID")]
        public override Guid Id { get; protected set; }

        /// <summary>
        /// 对接类型
        /// </summary>
        [Column(Position = 2)]
        [Description("对接类型")]
        public JointTypeEnum JointType { get; set; }

        /// <summary>
        /// Web设置，模式
        /// </summary>
        [Column(Position = 3)]
        [Description("Web设置，模式")]
        public string WebMode { get; set; }

        /// <summary>
        /// Web设置，报卡Url
        /// </summary>
        [Column(Position = 4)]
        [Description("Web设置，报卡Url")]
        public string WebReportCardUrl { get; set; }

        /// <summary>
        /// Web设置，报卡状态查询Url
        /// </summary>
        [Column(Position = 5)]
        [Description("Web设置，报卡状态查询Url")]
        public string WebReportCardStatusUrl { get; set; }

        /// <summary>
        /// 桌面程序设置，WebSocket Url
        /// </summary>
        [Column(Position = 6)]
        [Description("桌面程序设置，WebSocket Url")]
        public string ExeWebSocketUrl { get; set; }

        /// <summary>
        /// 是否默认配置
        /// </summary>
        [Column(Position = 7)]
        [Description("是否默认配置")]
        public bool IsDefault { get; set; } = false;
        #endregion
    }
}
