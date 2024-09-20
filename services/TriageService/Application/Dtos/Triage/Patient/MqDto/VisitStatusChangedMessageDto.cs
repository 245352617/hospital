using Newtonsoft.Json;
using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class VisitStatusChangedMessageDto
    {
        public VisitStatusChangedMessageDto(Guid id, EVisitStatus visitStatus, bool isReferral = false)
        {
            this.Id = id;
            this.VisitStatus = visitStatus;
            this.IsReferral = isReferral;
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

        /// <summary>
        /// 首诊医生工号
        /// </summary>
        public string FirstDoctorCode { get; set; }

        /// <summary>
        /// 首诊医生名称
        /// </summary>
        public string FirstDoctorName { get; set; }

        /// <summary>
        /// 最终去向代码
        /// </summary>
        public string LastDirectionCode { get; set; }

        /// <summary>
        /// 最终去向名称
        /// </summary>
        public string LastDirectionName { get; set; }

        /// <summary>
        /// 结束就诊时间
        /// </summary>
        public DateTime? VisitDate { get; set; }

        /// <summary>
        /// 结束就诊时间
        /// </summary>
        public DateTime? FinishVisitTime { get; set; }

        /// <summary>
        /// 是否转诊
        /// </summary>
        public bool IsReferral { get; set; }

        /// <summary>
        /// 流转区域
        /// </summary>
        public string TransferArea { get; set; }

    }
}
