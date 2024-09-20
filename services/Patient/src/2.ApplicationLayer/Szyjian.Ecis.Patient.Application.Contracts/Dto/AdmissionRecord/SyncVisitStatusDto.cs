using System;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class SyncVisitStatusDto
    {
        public SyncVisitStatusDto(Guid id, VisitStatus visitStatus)
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
        public VisitStatus VisitStatus { get; set; }

        /// <summary>
        /// 是否转诊
        /// </summary>
        public bool IsReferral { get; set; }
    }
}
