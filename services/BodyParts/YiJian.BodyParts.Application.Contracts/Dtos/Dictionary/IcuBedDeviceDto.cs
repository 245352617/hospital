using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 病床设备模板
    /// </summary>
    public class IcuBedDeviceDto : EntityDto<Guid>
    {
        /// <summary>
        /// 设备代码
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// 设备类别
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 床位号码
        /// </summary>
        public string BedNum { get; set; }

        /// <summary>
        /// 端口号码
        /// </summary>
        [CanBeNull]
        public string PortNum { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        [CanBeNull]
        public DateTime RecordTime { get; set; }

        /// <summary>
        /// 操作人编号
        /// </summary>
        [CanBeNull]
        public string OperatorCode { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        [CanBeNull]
        public string OperatorName { get; set; }

        /// <summary>
        /// 删除操作人编号
        /// </summary>
        [CanBeNull]
        public string DelOperatorCode { get; set; }

        /// <summary>
        /// 删除操作人姓名
        /// </summary>
        [CanBeNull]
        public string DelOperatorName { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [CanBeNull]
        public DateTime? DelTime { get; set; }

        /// <summary>
        /// 有效标志(1-有效，0-无效)
        /// </summary>
        public int ValidState { get; set; }
    }
}
