using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call.CallCenter.Etos
{
    /// <summary>
    /// 同步患者信息
    /// </summary>
    public class SyncVisitStatusEto
    {
        public SyncVisitStatusEto(string id, EVisitStatus visitStatus)
        {
            this.Id = id;
            this.VisitStatus = visitStatus;
        }

        /// <summary>
        /// 患者 ID
        /// </summary>
        public string Id { get; set; }

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
        /// 流转区域
        /// </summary>
        public string TransferArea { get; set; }
    }

    public class SyncVisitStatusV2Eto
    {


        /// <summary>
        /// 患者 ID
        /// </summary>
        public string Id { get; set; }

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
        /// 挂号序号
        /// </summary>
        public string RegisterNo { get; set; }
    }
}
