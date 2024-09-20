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
    public class UsageOrderExcuteDto : EntityDto<Guid>
    {
        /// <summary>
        /// 护理类型(对应护理记录的类型)
        /// </summary>
        /// <example></example>
        public string NursingType { get; set; }

        public List<OrderExcuteDto> OrderExcuteDtos { get; set; }
    }
}
