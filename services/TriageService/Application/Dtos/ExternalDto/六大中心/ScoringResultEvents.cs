using System;
using System.Collections.Generic;
using Volo.Abp.EventBus;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// /// 预见分诊评分队列
    /// /// </summary>
    [EventName("ScoringResultEvents")]
    public class ScoringResultEvents
    {
        /// <summary>
        /// 病患id
        /// </summary>
        public Guid Pid { get; set; }

        /// <summary>
        /// 选中Id
        /// </summary>
        public List<Guid> DetailIdList { get; set; }

        /// <summary>
        /// 总分
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 评分名称
        /// </summary>
        public string ScoreName { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string Name { get; set; }
    }
}