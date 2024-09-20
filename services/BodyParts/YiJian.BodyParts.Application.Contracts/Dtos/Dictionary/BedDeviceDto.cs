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
    public class BedDeviceDto : EntityDto<Guid>
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
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 设备厂商
        /// </summary>
        public string DeviceVendor { get; set; }

        /// <summary>
        /// 设备厂商
        /// </summary>
        public string DeviceIp { get; set; }

        /// <summary>
        /// 床位号码
        /// </summary>
        public string BedNum { get; set; }
        

        /// <summary>
        /// 条码编号
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }


        /// <summary>
        /// 排序
        /// </summary>
        [CanBeNull]
        public int sortNum { get; set; }

        /// <summary>
        /// 端口号码
        /// </summary>
        [CanBeNull]
        public string PortNum { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        [CanBeNull]
        public DateTime? RecordTime { get; set; }

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
        /// 有效标志(1-有效，0-无效)
        /// </summary>
        public int ValidState { get; set; }
    }
}
