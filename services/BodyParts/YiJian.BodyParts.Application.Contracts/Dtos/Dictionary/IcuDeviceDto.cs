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
    /// 表:字典-设备表
    /// </summary>
    public class IcuDeviceDto : EntityDto<Guid>
    {


        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(20)] public string DeptCode { get; set; }

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
        /// 
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(40)] public string DeviceVendor { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        /// <example></example>
        [Required] [StringLength(20)] public string DeviceType { get; set; }

        /// <summary>
        /// 设备ip
        /// </summary>
        /// <example></example>
        [Required] [StringLength(50)] public string DeviceIp { get; set; }

        /// <summary>
        /// 端口类型
        /// </summary>
        /// <example></example>
        [CanBeNull]  public string PortType { get; set; }

        /// <summary>
        /// 记录日期
        /// </summary>
        /// <example></example>
        [CanBeNull]
        public DateTime? RecordTime { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        /// <example></example>
        [CanBeNull] public string BarCode { get; set; }

        /// <summary>
        /// 通道
        /// </summary>
        /// <example></example>
        [CanBeNull] public decimal? Channel { get; set; }

        /// <summary>
        /// 设备SN号
        /// </summary>
        /// <example></example>
        [CanBeNull]  public string Sn { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// <example></example>
        [CanBeNull] public string Remark { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        /// <example></example>
        [CanBeNull] public int? SortNum { get; set; }

        /// <summary>
        /// 是否有效(1-有效，0-无效)
        /// </summary>
        /// <example></example>
        [Required] public int ValidState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CanBeNull] public bool IsConnection { get; set; }
    }
}
