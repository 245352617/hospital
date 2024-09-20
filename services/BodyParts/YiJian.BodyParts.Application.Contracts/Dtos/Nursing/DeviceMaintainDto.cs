#region 代码备注
//------------------------------------------------------------------------------
// 创建描述:  11/10/2020 08:08:45
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
    /// 表:字典-设备病人绑定信
    /// </summary>
    public class DeviceMaintainDto : EntityDto<Guid>
    {


        /// <summary>
        /// 床位
        /// </summary>
        /// <example></example>
        [Required] [StringLength(20)] public string BedNum { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        [Required] [StringLength(20)] public string PI_ID { get; set; }


        /// <summary>
        /// 住院号
        /// </summary>
        /// <example></example>
        [StringLength(20)] public string PatientId { get; set; }

        /// <summary>
        /// 设备代码
        /// </summary>
        /// <example></example>
        [Required] [StringLength(80)] public string DeviceCode { get; set; }

        /// <summary>
        /// 设备代码
        /// </summary>
        /// <example></example>
        [Required] [StringLength(80)] public string DeviceName { get; set; }

        /// <summary>
        /// 设备代码
        /// </summary>
        /// <example></example>
        [Required] [StringLength(80)] public string DisplayName { get; set; }

        /// <summary>
        /// 设备IP
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(80)] public string DeviceIp { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(80)] public string DeviceVendor { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        /// <example></example>
        [Required] public DateTime RecordTime { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        /// <example></example>
        [Required] [StringLength(80)] public string DeviceType { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        /// <example></example>
        [Required] [StringLength(80)] public string Protocol { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(20)] public string DeptCode { get; set; }

        /// <summary>
        /// 是否有效(1-有效，0-无效)
        /// </summary>
        /// <example></example>
        [Required] public int ValidState { get; set; }
    }
}
