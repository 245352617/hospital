#region 代码备注
//------------------------------------------------------------------------------
// 创建描述:  11/12/2020 07:20:16
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
    /// 表:字典-设备类型
    /// </summary>
    public class IcuDeviceTypeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        /// <example></example>
        [Required] [StringLength(80)] public string DeviceType { get; set; }

        /// <summary>
        /// 厂家
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(50)] public string Producer { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(50)] public string DeviceName { get; set; }

        /// <summary>
        /// 协议
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(50)] public string Protocol { get; set; }

        /// <summary>
        /// 是否有效(1-有效，0-无效)
        /// </summary>
        /// <example></example>
        [Required] public int ValidState { get; set; }
    }
}
