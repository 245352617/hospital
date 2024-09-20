using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.ECIS.Call.CallCenter.Dtos
{
    /// <summary>
    /// 叫号历史查看查询参数
    /// Directory: input
    /// </summary>
    public class GetHistoryInput : PageBase
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginDateTime { get; set; } = null;

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDateTime { get; set; } = null;

        /// <summary>
        /// 分诊科室编码
        /// </summary>
        [StringLength(50, ErrorMessage = "分诊科室编码输入不能超过50位")]
        public string TriageDeptCode { get; set; }

        /// <summary>
        /// 分诊级别编码
        /// </summary>
        [StringLength(50, ErrorMessage = "分诊级别输入不能超过50位")]
        public string ActTriageLevelCode { get; set; }

        /// <summary>
        /// 历史叫号状态（值与就诊状态一致）
        /// 0 = 未挂号
        /// 2 = 过号 （医生已经叫号）
        /// 3 = 已退号 （退挂号）
        /// 5 = 已就诊（就诊区患者）
        /// 6 = 出科（抢救区、留观区患者）
        /// </summary>
        public EHistoryVisitStatus HistoryVisitStatus { get; set; } = EHistoryVisitStatus.All;

        /// <summary>
        /// 费别编码
        /// </summary>
        [StringLength(50, ErrorMessage = "费别编码输入不能超过50位")]
        public string ChargeTypeCode { get; set; }

        /// <summary>
        /// 医生ID
        /// </summary>
        [StringLength(50, ErrorMessage = "医生ID输入不能超过50位")]
        public string DoctorId { get; set; }

        /// <summary>
        /// 姓名、联系方式、就诊ID
        /// </summary>
        [StringLength(50, ErrorMessage = "姓名/联系方式/就诊ID 输入不能超过50位")]
        public string Filter { get; set; }
    }
}
