#region 代码备注
//------------------------------------------------------------------------------
// 创建描述:  11/12/2020 06:39:47
//
// 功能描述:
//
// 修改描述:
//------------------------------------------------------------------------------
#endregion 代码备注
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:医嘱记录表
    /// </summary>
    public class NursingOrderDto : EntityDto<Guid>
    {
        /// <summary>
        /// 护理时间
        /// </summary>
        /// <example></example>
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        public string PI_ID { get; set; }

        /// <summary>
        /// 医嘱组号
        /// </summary>
        /// <example></example>
        public string GroupNo { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        /// <example></example>
        public DateTime? PlanExcuteTime { get; set; }

        /// <summary>
        /// 开立医嘱医生
        /// </summary>
        /// <example></example>
        public string StartDoctorName { get; set; }

        /// <summary>
        /// 是否泵入
        /// </summary>
        /// <example></example>
        public bool IsPump { get; set; }

        /// <summary>
        /// 医嘱类型长、临
        /// </summary>
        /// <example></example>
        public string OrderType { get; set; }

        /// <summary>
        /// 医嘱简称
        /// </summary>
        /// <example></example>
        public string Abbreviation { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        /// <example></example>
        public string ExcuteFlag { get; set; }

        /// <summary>
        /// 总剂量
        /// </summary>
        /// <example></example>
        public string TotalDosage { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        /// <example></example>
        public decimal? Weight { get; set; }

        /// <summary>
        /// 剩余量
        /// </summary>
        /// <example></example>
        public decimal? RestDosage { get; set; }

        /// <summary>
        /// 每次剂量
        /// </summary>
        /// <example></example>
        public string Dosage { get; set; }

        /// <summary>
        /// 每次剂量单位
        /// </summary>
        /// <example></example>
        public string DosageUnit { get; set; }

        /// <summary>
        /// 用药方式（途径）
        /// </summary>
        /// <example></example>
        public string Usage { get; set; }

        /// <summary>
        /// 用药途径名称
        /// </summary>
        /// <example></example>
        public string UsageName { get; set; }

        /// <summary>
        /// 是否单次
        /// </summary>
        public bool? Single { get; set; }

        /// <summary>
        /// 执行频率
        /// </summary>
        /// <example></example>
        public string Frequency { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        /// <example></example>
        public DateTime? ExcuteTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// <example></example>
        public string Remark { get; set; }

        /// <summary>
        ///是否结算进入量 
        /// </summary>
        public int IfCalcIn { get; set; }

        public bool? StopStatus { get; set; }

        /// <summary>
        /// 执行医嘱
        /// </summary>
        public List<OrderDto> orderDtos { get; set; }

        /// <summary>
        /// 每组医嘱执行时间
        /// </summary>
        public List<PlanExcuteTimeDto> excuteTimeList { get; set; }

        /// <summary>
        /// 是否统计 0不统计 1统计
        /// </summary>
        public string StatisticState { get; set; }
    }
}
