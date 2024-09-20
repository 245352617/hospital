using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;
using YiJian.ECIS.Call.Domain.CallCenter;

namespace YiJian.ECIS.Call.CallCenter
{
    /// <summary>
    /// 规约：历史查询
    /// </summary>
    public class HistorySearchCallInfoSpecification : Specification<CallInfo>
    {
        public HistorySearchCallInfoSpecification(DateTime? beginDateTime, DateTime? endDateTime, string filter, string actTriageLevelCode,    
            string triageDeptCode, string chargeTypeCode, string doctorId)
        {
            this.BeginDateTime = beginDateTime;
            this.EndDateTime = endDateTime;
            this.Filter = filter;
            this.ActTriageLevelCode = actTriageLevelCode; 
            this.TriageDeptCode = triageDeptCode;
            this.ChargeTypeCode = chargeTypeCode;
            this.DoctorId = doctorId;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginDateTime { get; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDateTime { get; }

        /// <summary>
        /// 姓名、联系方式、就诊ID
        /// </summary>
        public string Filter { get; }

        /// <summary>
        /// 分诊级别编码
        /// </summary>
        public string ActTriageLevelCode { get; }


        /// <summary>
        /// 分诊科室编码
        /// </summary>
        public string TriageDeptCode { get; }

        /// <summary>
        /// 费别编码
        /// </summary>
        public string ChargeTypeCode { get; }

        /// <summary>
        /// 医生ID
        /// </summary>
        public string DoctorId { get; }

        public override Expression<Func<CallInfo, bool>> ToExpression()
        {
            return x =>
                // 开始时间
                (BeginDateTime == null || x.LogTime >= BeginDateTime.Value)
                // 结束时间
                && (EndDateTime == null || x.LogTime <= EndDateTime.Value)
                // 模糊查询
                && (string.IsNullOrEmpty(Filter)
                    || x.PatientName.Contains(Filter)
                    || x.RegisterNo.Contains(Filter))
                // 分诊级别
                && (string.IsNullOrEmpty(ActTriageLevelCode) || x.ActTriageLevel == ActTriageLevelCode.Trim()) 
                // 科室
                && (string.IsNullOrEmpty(TriageDeptCode) || x.TriageDept == TriageDeptCode.Trim());
        }
    }
}
