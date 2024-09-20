using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Volo.Abp.Specifications;
using YiJian.ECIS.Call.Domain.CallCenter;

namespace YiJian.ECIS.Call.CallCenter
{
    /// <summary>
    /// 规约：页面查询条件
    /// </summary>
    public class SearchCallInfoSpecification : Specification<CallInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="callingStatus">叫号状态</param> 
        /// <param name="triageDept">科室</param>
        /// <param name="actTriageLevel">分诊级别</param> 
        /// <param name="filter">姓名、门诊号、联系方式</param>
        public SearchCallInfoSpecification(
            ECallingQueryStatus callingStatus = ECallingQueryStatus.All, 
            string triageDept = null,
            string actTriageLevel = null, 
            string filter = null)
        {
            this.CallingStatus = callingStatus;
            this.TriageDept = triageDept;
            this.ActTriageLevel = actTriageLevel; 
            this.Filter = filter;
        }

        /// <summary>
        /// 叫号状态
        /// </summary>
        public ECallingQueryStatus CallingStatus { get; }

        /// <summary>
        /// 科室
        /// </summary>
        public string TriageDept { get; }

        /// <summary>
        /// 分诊级别
        /// </summary>
        public string ActTriageLevel { get; }
         

        /// <summary>
        /// 姓名、门诊号、联系方式
        /// </summary>
        public string Filter { get; }

        public override Expression<Func<CallInfo, bool>> ToExpression()
        {
            return x =>
                // 分诊级别
                (string.IsNullOrEmpty(this.ActTriageLevel) || x.ActTriageLevel == this.ActTriageLevel)
                // 按产品要求，待就诊列表不显示未挂号患者  by: ywlin-2021.11.10
                // 按产品要求，待就诊列表需要显示未挂号患者  by: ywlin-2021.11.12
                // 候诊中（正在就诊、待就诊、未挂号）
                && (this.CallingStatus != ECallingQueryStatus.Waitting)
                // 已就诊
                && (this.CallingStatus != ECallingQueryStatus.Treated)
                // 已过号
                && (this.CallingStatus != ECallingQueryStatus.UntreatedOver)
                // 附带就诊状态查询条件 
                // 姓名、门诊号、联系方式 查询条件
                && (string.IsNullOrEmpty(Filter)
                    || x.PatientName.Contains(Filter)
                    || x.RegisterNo.Contains(Filter));
        }

        //public override Expression<Func<CallInfo, bool>> ToExpression()
        //{
        //    return x =>
        //        // 分诊级别
        //        (string.IsNullOrEmpty(this.ActTriageLevel) || x.ActTriageLevel == this.ActTriageLevel)
        //        // 按产品要求，待就诊列表不显示未挂号患者  by: ywlin-2021.11.10
        //        // 按产品要求，待就诊列表需要显示未挂号患者  by: ywlin-2021.11.12
        //        // 候诊中（正在就诊、待就诊、未挂号）
        //        && (this.CallingStatus != ECallingQueryStatus.Waitting  )
        //        // 已就诊
        //        && (this.CallingStatus != ECallingQueryStatus.Treated )
        //        // 已过号
        //        && (this.CallingStatus != ECallingQueryStatus.UntreatedOver )
        //        // 附带就诊状态查询条件 
        //        // 姓名、门诊号、联系方式 查询条件
        //        && (string.IsNullOrEmpty(Filter)
        //            || x.PatientName.Contains(Filter)
        //            || x.RegisterNo.Contains(Filter));
        //}
    }
}
