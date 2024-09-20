using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class IcuPatientStatisticsDto
    {
        /// <summary>
        /// 患者出入情况列表
        /// </summary>
        public GoInOut goInOuts { get; set; }

        /// <summary>
        /// 患者入科来源分布列表
        /// </summary>
        public DeptSource deptSources { get; set; }

        /// <summary>
        /// 患者出科转归分布列表
        /// </summary>
        public OutDeptFate outDeptFates { get; set; }

        /// <summary>
        /// 本科收治床日率列表
        /// </summary>
        public AdmissionRatio admissionRatios { get; set; }

        /// <summary>
        /// 患者重返率列表
        /// </summary>
        public ReEntryRate reEntryRates { get; set; }

        /// <summary>
        /// 死亡率列表
        /// </summary>
        public MortalityRate mortalityRates { get; set; }
    }

    /// <summary>
    /// 患者出入情况
    /// </summary>
    public class GoInOut
    {
        /// <summary>
        /// 时间，年月
        /// </summary>
        public List<string> Time { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public List<int> OutDeptNumber { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public List<int> InDeptNumber { get; set; }

        /// <summary>
        /// 出入科类型，1：在科，0：出科
        /// </summary>
        //public List<int> DeptState { get; set; }
    }

    /// <summary>
    /// 患者入科来源分布
    /// </summary>
    public class DeptSource
    {
        ///// <summary>
        ///// 科室代码
        ///// </summary>
        //public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public List<string> DeptName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public List<int> Number { get; set; }
    }

    /// <summary>
    /// 患者出科转归分布
    /// </summary>
    public class OutDeptFate
    {
        /// <summary>
        /// 百分比
        /// </summary>
        public List<decimal> Percentage { get; set; }

        /// <summary>
        /// 出科转归类型
        /// </summary>
        public List<string> OutTurnoverName { get; set; }
    }

    /// <summary>
    /// 本科收治床日率
    /// </summary>
    public class AdmissionRatio
    {
        /// <summary>
        /// 时间，年月
        /// </summary>
        public List<string> Time { get; set; }

        /// <summary>
        /// 率值
        /// </summary>
        public List<string> RateValue { get; set; }

        /// <summary>
        /// 总床日数
        /// </summary>
        public List<decimal> NumberValue { get; set; }

        /// <summary>
        /// 比率，用于前端展示画图
        /// </summary>
        public decimal ViewRatio { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public decimal MaxNum { get; set; }
    }

    /// <summary>
    /// 患者重返率
    /// </summary>
    public class ReEntryRate
    {
        /// <summary>
        /// 时间，年月
        /// </summary>
        public List<string> Time { get; set; }

        /// <summary>
        /// 24百分比
        /// </summary>
        public List<string> Proportion24 { get; set; }

        /// <summary>
        /// 48百分比
        /// </summary>
        public List<string> Proportion48 { get; set; }
    }

    /// <summary>
    /// 死亡率
    /// </summary>
    public class MortalityRate
    {
        /// <summary>
        /// 时间，年月
        /// </summary>
        public List<string> Time { get; set; }

        /// <summary>
        /// 率值
        /// </summary>
        public List<string> RateValue { get; set; }

        /// <summary>
        /// 死亡例数值
        /// </summary>
        public List<string> NumberValue { get; set; }

        /// <summary>
        /// 比率，用于前端展示画图
        /// </summary>
        public decimal ViewRatio { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public decimal MaxNum { get; set; }
    }
}