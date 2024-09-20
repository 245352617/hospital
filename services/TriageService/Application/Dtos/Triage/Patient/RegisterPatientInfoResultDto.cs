using System.Collections.Generic;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 挂号患者信息列表
    /// </summary>
    public class RegisterPatientInfoResultDto
    {
        public RegisterPatientInfoResultDto()
        {

        }

        /// <summary>
        /// 列表信息
        /// </summary>
        public IEnumerable<RegisterPatientInfoDto> Items { get; set; }

        /// <summary>
        /// 挂号患者总数（未分页）
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 挂号总数
        /// </summary>
        public int TotalRegisterCount { get; set; }

        /// <summary>
        /// 当前就诊总数
        /// </summary>
        public int TotalTreatingCount { get; set; }

        /// <summary>
        /// 候诊中总数
        /// </summary>
        public int TotalWaitingCount { get; set; }

        /// <summary>
        /// 已就诊总数
        /// </summary>
        public int TotalTreatedCount { get; set; }

        /// <summary>
        /// 退号总数
        /// </summary>
        public int TotalRegisterRefundCount { get; set; }
    }
}
