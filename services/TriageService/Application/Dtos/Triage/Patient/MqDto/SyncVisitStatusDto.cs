using System;
using System.Collections.Generic;
using System.Text;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class SyncVisitStatusDto
    {
        public SyncVisitStatusDto(Guid id, EVisitStatus visitStatus)
        {
            this.Id = id;
            this.VisitStatus = visitStatus;
        }

        /// <summary>
        /// 患者 ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 就诊状态
        /// 0 = 未挂号
        /// 1 = 待就诊
        /// 2 = 过号 （医生已经叫号）
        /// 3 = 已退号 （退挂号）
        /// 4 = 正在就诊
        /// 5 = 已就诊（就诊区患者）
        /// 6 = 出科（抢救区、留观区患者）
        /// </summary>
        public EVisitStatus VisitStatus { get; set; }
    }
}
