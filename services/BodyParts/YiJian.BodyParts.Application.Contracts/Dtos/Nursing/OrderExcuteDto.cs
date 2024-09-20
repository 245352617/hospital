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
using System.Data;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:医嘱执行表
    /// </summary>
    public class OrderExcuteDto : EntityDto<Guid>
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
        /// 开立时间/下嘱时间
        /// </summary>
        /// <example></example>
        public DateTime? ConversionTime { get; set; }

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
        /// 预计执行时间长
        /// </summary>
        /// <example></example>
        public string PlanExcuteHours { get; set; }

        /// <summary>
        /// 开立医嘱医生
        /// </summary>
        /// <example></example>
        public string StartDoctor { get; set; }

        /// <summary>
        /// 医嘱类型长、临
        /// </summary>
        /// <example></example>
        public string OrderType { get; set; }

        /// <summary>
        /// 医嘱类别 (A 西药, E治疗, I 消耗品,  H 护理 C 检验 ,D检查 F 手术 G 麻醉 , K 床位 )
        /// </summary>
        /// <example></example>
        public string OrderClass { get; set; }

        /// <summary>
        /// 医嘱号（his中记录唯一值）
        /// </summary>
        /// <example></example>
        public string OrderId { get; set; }

        /// <summary>
        /// 医嘱项目代码
        /// </summary>
        /// <example></example>
        public string ItemCode { get; set; }

        /// <summary>
        /// 医嘱内容
        /// </summary>
        /// <example></example>
        public string OrderText { get; set; }

        /// <summary>
        /// 医嘱简称
        /// </summary>
        /// <example></example>
        public string Abbreviation { get; set; }

        /// <summary>
        /// 总剂量
        /// </summary>
        /// <example></example>
        public string TotalDosage { get; set; }

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
        /// 用法名全称
        /// </summary>
        /// <example></example>
        public string UsageFullName { get; set; }

        /// <summary>
        /// 护理类型(对应护理记录的类型)
        /// </summary>
        /// <example></example>
        public string NursingType { get; set; }

        /// <summary>
        /// 执行频率
        /// </summary>
        /// <example></example>
        public string Frequency { get; set; }

        /// <summary>
        /// 频率次数
        /// </summary>
        /// <example></example>
        public string FreqCount { get; set; }

        /// <summary>
        /// 频率间隔
        /// </summary>
        /// <example></example>
        public string FreqInterval { get; set; }

        /// <summary>
        /// 频率间隔单位
        /// </summary>
        /// <example></example>
        public string FreqIntervalUnit { get; set; }

        /// <summary>
        /// 执行方式PC/PDA
        /// </summary>
        /// <example></example>
        public string ExcuteType { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        /// <example></example>
        public string ExcuteFlag { get; set; }

        /// <summary>
        /// 执行护士
        /// </summary>
        /// <example></example>
        public string ExcuteNurseCode { get; set; }

        /// <summary>
        /// 执行护士名称
        /// </summary>
        /// <example></example>
        public string ExcuteNurseName { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        /// <example></example>
        public DateTime? ExcuteTime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        /// <example></example>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 完成护士
        /// </summary>
        /// <example></example>
        public string EndNurse { get; set; }

        /// <summary>
        /// 终止标志(Y-是 ,N-否)
        /// </summary>
        /// <example></example>
        public string StopFlag { get; set; }

        /// <summary>
        /// 终止护士
        /// </summary>
        /// <example></example>
        public string StopNurseCode { get; set; }

        /// <summary>
        /// 终止护士名称
        /// </summary>
        /// <example></example>
        public string StopNurseName { get; set; }

        /// <summary>
        /// 终止原因
        /// </summary>
        /// <example></example>
        public string StopWhy { get; set; }

        /// <summary>
        /// 瓶签
        /// </summary>
        /// <example></example>
        public string LabelId { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        public string DeptCode { get; set; }

        /// <summary>
        /// 病区
        /// </summary>
        /// <example></example>
        public string WardCode { get; set; }

        /// <summary>
        /// 记录类型(N-Normal 普通药品,S-Special 特殊药品)
        /// </summary>
        /// <example></example>
        public string RecordType { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        /// <example></example>
        public DateTime RecordTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// <example></example>
        public string Remark { get; set; }

        /// <summary>
        /// 删除人
        /// </summary>
        /// <example></example>
        public string DeleteReason { get; set; }

        /// <summary>
        ///是否结算进入量 
        /// </summary>
        public int IfCalcIn { get; set; }

        /// <summary>
        /// 是否单次执行
        /// </summary>
        public bool? Single { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        /// <example></example>
        public int SortNum { get; set; }

        /// <summary>
        /// 有效标志性 ( 0 : Stop; 1 : InUse; 2 : discard )
        /// </summary>
        /// <example></example> 
        public int ValidState { get; set; }

        /// <summary>
        /// 是否手工录入(手工补录)
        /// </summary>
        /// <example></example>
        public int IfManualInput { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public List<PlanExcuteTimeDto> excuteTimeList { get; set; }


        /// <summary>
        /// 医嘱执行记录
        /// </summary>
        public List<IcuNursingOrderDto> nursingOrderDtos { get; set; }

        /// <summary>
        /// 是否更新
        /// </summary>
        public bool? HasUpdate { get; set; }
    }
}
