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
    public class CreateBedDeviceDto : EntityDto<Guid>
    {
        /// <summary>
        /// 设备代码
        /// </summary>
        public string DeviceCode { get; set; }


        /// <summary>
        /// 设备名称
        /// </summary>
        /// <example></example>
        public string DeviceName { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        /// <example></example>
        public string DisplayName { get; set; }

        /// <summary>
        /// 设备类别
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <example></example>
        public string DeviceVendor { get; set; }

        /// <summary>
        /// 设备ip
        /// </summary>
        /// <example></example>
        public string DeviceIp { get; set; }

        /// <summary>
        /// 设备协议
        /// </summary>
        /// <example></example>
        public string Protocol { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

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
        /// 排序号码
        /// </summary> 
        public int SortNum { get; set; }

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
    }
}
