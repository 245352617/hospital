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
    public class CreateNursingOrderDto : EntityDto<Guid>
    {

        /// <summary>
        /// 护理时间
        /// </summary>
        /// <example></example>
        [Required]
        public DateTime PlanExcuteTime { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        /// <example></example>
        [Required]
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        [Required]
        public string PI_ID { get; set; }

        /// <summary>
        /// 医嘱组号
        /// </summary>
        /// <example></example>
        [Required]
        public string GroupNo { get; set; }

        /// <summary>
        /// 途径
        /// </summary>
        [Required]
        public string Usage { get; set; }

        /// <summary>
        /// 用药途径名称
        /// </summary>
        /// <example></example>
        [Required]
        public string UsageName { get; set; }


        /// <summary>
        /// 剂量
        /// </summary>
        /// <example></example>
        public decimal? Dosage { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        public string DosageUnit { get; set; }

        /// <summary>
        /// 总量
        /// </summary>
        /// <example></example>
        [Required]
        public decimal? TotalDosage { get; set; }

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
        /// 药速
        /// </summary>
        /// <example></example>
        public decimal? DrugSpeed { get; set; }

        /// <summary>
        /// 药速单位
        /// </summary>
        public string DrugSpeedUnit { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        /// <example></example>
        public string DrugState { get; set; }

        /// <summary>
        /// 流速
        /// </summary>
        /// <example></example>
        public decimal? Speed { get; set; }

        /// <summary>
        /// 加推
        /// </summary>
        /// <example></example>
        public decimal? Push { get; set; }

        /// <summary>
        ///是否结算进入量 
        /// </summary>
        [Required]
        public int IfCalcIn { get; set; }

        /// <summary>
        ///是否单次
        /// </summary>
        public bool? Single { get; set; }

        /// <summary>
        /// 护士工号
        /// </summary>
        /// <example></example>
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        /// <example></example>
        public string NurseName { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        /// <example></example>
        public DateTime RecordTime { get; set; }

        /// <summary>
        /// 有效状态（1-有效，0-无效）
        /// </summary>
        /// <example></example>
        public int ValidState { get; set; }

        /// <summary>
        /// 是否统计 0不统计 1统计
        /// </summary>
        public string StatisticState { get; set; }
    }
}
