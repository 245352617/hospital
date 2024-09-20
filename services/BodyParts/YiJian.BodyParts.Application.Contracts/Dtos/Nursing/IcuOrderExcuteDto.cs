#region 代码备注
//------------------------------------------------------------------------------
// 创建描述:  11/05/2020 08:37:46
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
    /// 表:医嘱执行表
    /// </summary>
    public class IcuOrderExcuteDto : EntityDto<Guid>
    {

        /// <summary>
        /// 护理时间
        /// </summary>
        /// <example></example>
        [Required] public DateTime NurseTime { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        [Required] [StringLength(20)] public string PI_ID { get; set; }

        /// <summary>
        /// 拆分时间
        /// </summary>
        /// <example></example>
        public DateTime? ConversionTime { get; set; }

        /// <summary>
        /// 医嘱组号
        /// </summary>
        /// <example></example>
        [Required] [StringLength(30)] public string GroupNo { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        /// <example></example>
        public DateTime? PlanExcuteTime { get; set; }

        /// <summary>
        /// 预计执行时间长
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string PlanExcuteHours { get; set; }

        /// <summary>
        /// 开立医嘱医生
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string StartDoctor { get; set; }

        /// <summary>
        /// 医嘱类型长、临
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string OrderType { get; set; }

        /// <summary>
        /// 医嘱类别 (A 西药, E治疗, I 消耗品,  H 护理 C 检验 ,D检查 F 手术 G 麻醉 , K 床位 )
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string OrderClass { get; set; }

        /// <summary>
        /// 医嘱号（his中记录唯一值）
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(40)] public string OrderId { get; set; }

        /// <summary>
        /// 医嘱项目代码
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(300)] public string ItemCode { get; set; }

        /// <summary>
        /// 医嘱内容
        /// </summary>
        /// <example></example>
        [Required] [StringLength(300)] public string OrderText { get; set; }

        /// <summary>
        /// 医嘱简称
        /// </summary>
        /// <example></example>
        [Required] [StringLength(100)] public string Abbreviation { get; set; }

        /// <summary>
        /// 总剂量
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string TotalDosage { get; set; }

        /// <summary>
        /// 每次剂量
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string Dosage { get; set; }

        /// <summary>
        /// 每次剂量单位
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string DosageUnit { get; set; }

        /// <summary>
        /// 用药方式（途径）
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string Usage { get; set; }

        /// <summary>
        /// 执行频率
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string Frequency { get; set; }

        /// <summary>
        /// 频率次数
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string FreqCount { get; set; }

        /// <summary>
        /// 频率间隔
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string FreqInterval { get; set; }

        /// <summary>
        /// 频率间隔单位
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string FreqIntervalUnit { get; set; }

        /// <summary>
        /// 执行方式PC/PDA
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string ExcuteType { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string ExcuteFlag { get; set; }

        /// <summary>
        /// 执行护士
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string ExcuteNurseCode { get; set; }

        /// <summary>
        /// 执行护士名称
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(20)] public string ExcuteNurseName { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        /// <example></example>
        public DateTime? ExcuteTime { get; set; }

        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 完成护士
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string EndNurse { get; set; }

        /// <summary>
        /// 终止标志(Y-是 ,N-否)
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(10)] public string StopFlag { get; set; }

        /// <summary>
        /// 终止护士
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string StopNurseCode { get; set; }

        /// <summary>
        /// 终止护士名称
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(20)] public string StopNurseName { get; set; }

        /// <summary>
        /// 终止原因
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string StopWhy { get; set; }

        /// <summary>
        /// 瓶签
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(60)] public string LabelId { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string DeptCode { get; set; }

        /// <summary>
        /// 病区
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(30)] public string WardCode { get; set; }

        /// <summary>
        /// 记录类型(N-Normal 普通药品,S-Special 特殊药品)
        /// </summary>
        /// <example></example>
        [Required] [StringLength(20)] public string RecordType { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        /// <example></example>
        [Required] public DateTime RecordTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(600)] public string Remark { get; set; }

        /// <summary>
        /// 删除原因
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(600)] public string DeleteReason { get; set; }

        /// <summary>
        /// 是否计算到入量
        /// </summary>
        /// <example></example>
        public int IfCalcIn { get; set; }


        /// <summary>
        /// 排序编号
        /// </summary>
        /// <example></example>
        [Required] public int SortNum { get; set; }

        /// <summary>
        /// 有效标志性 ( 0 : Stop; 1 : InUse; 2 : discard )
        /// </summary>
        /// <example></example>
        [Required] public int ValidState { get; set; }
    }
}
