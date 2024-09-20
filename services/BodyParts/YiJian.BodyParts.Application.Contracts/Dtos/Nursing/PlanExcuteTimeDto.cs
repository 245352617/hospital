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
    public class PlanExcuteTimeDto : EntityDto<Guid>
    {
        public string Id { get; set; }

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


        public string ExcuteState { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        /// <example></example>
        public DateTime? PlanFinishTime { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public List<IcuNursingOrderDto> nursingOrderDtos { get; set; }
    }
}
