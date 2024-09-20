using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 途径列表Dto
    /// </summary>
    public class OrderExcuteListDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 途径分组
        /// </summary>
        public string NursingType { get; set; }

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
        /// 执行状态
        /// </summary>
        /// <example></example>
        public string ExcuteFlag { get; set; }

        /// <summary>
        /// 医嘱是否超过24小时
        /// </summary>
        /// <example></example>
        public bool IsPause { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        /// <example></example>
        public DateTime? PlanExcuteTime { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public List<PlanExcuteTimeListDto> excuteTimeList { get; set; }

        /// <summary>
        /// 医嘱类型长、临
        /// </summary>
        /// <example></example>
        public string OrderType { get; set; }

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
        /// 是否手工录入(手工补录)
        /// </summary>
        /// <example></example>
        public int IfManualInput { get; set; }

        /// <summary>
        /// 用药方式（途径）
        /// </summary>
        /// <example></example>
        public string Usage { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        /// <example></example>
        public string Frequency { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// <example></example>
        public string Remark { get; set; }

        /// <summary>
        /// 下嘱时间
        /// </summary>
        /// <example></example>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 停嘱时间
        /// </summary>
        /// <example></example>
        public DateTime? StopTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        /// <example></example>
        public DateTime? ExcuteTime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        /// <example></example>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 余/总量(ml)
        /// </summary>
        /// <example></example>
        public string TotalDosage { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        /// <example></example>
        public string Speed { get; set; }

        /// <summary>
        /// 是否更新
        /// </summary>
        public bool? HasUpdate { get; set; }

        /// <summary>
        /// 医嘱执行记录
        /// </summary>
        public List<NursingOrderListDto> nursingOrderDtos { get; set; }
    }
}
