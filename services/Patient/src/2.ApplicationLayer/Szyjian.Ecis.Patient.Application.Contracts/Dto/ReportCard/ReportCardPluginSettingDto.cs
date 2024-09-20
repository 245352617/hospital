using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Dtos;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 报卡插件设置
    /// 每次更改报卡插件设置，都会先把老的软删除，然后建立新的
    /// </summary>
    public class ReportCardPluginSettingDto : EntityDto
    {
        #region Property
        /// <summary>
        /// 对接类型
        /// </summary>
        public JointTypeEnum JointType { get; set; }

        /// <summary>
        /// Web设置，模式
        /// </summary>
        public string WebMode { get; set; }

        /// <summary>
        /// Web设置，报卡Url
        /// </summary>
        public string WebReportCardUrl { get; set; }

        /// <summary>
        /// Web设置，报卡Url
        /// </summary>
        public string WebReportCardStatusUrl { get; set; }

        /// <summary>
        /// 桌面程序设置，WebSocket Url
        /// </summary>
        public string ExeWebSocketUrl { get; set; }
        #endregion
    }
}
