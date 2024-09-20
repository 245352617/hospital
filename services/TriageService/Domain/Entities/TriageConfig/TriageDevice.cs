using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SamJan.MicroService.PreHospital.Core.BaseEntities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class TriageDevice : BaseEntity<Guid>
    {
        public TriageDevice SetId(Guid id)
        {
            Id = id;
            return this;
        }

        ///<summary>
        /// 设备编号
        /// </summary>
        [StringLength(50)]
        [Description("设备编号")]
        public string DeviceCode { get; set; }

        ///<summary>
        /// 设备名称
        /// </summary>
        [StringLength(100)]
        [Description("设备名称")]
        public string DeviceName { get; set; }

        ///<summary>
        /// 厂家信息
        /// </summary>
        [StringLength(100)]
        [Description("厂家信息")]
        public string FactoryInfo { get; set; }

        ///<summary>
        /// 设备型号
        /// </summary>
        [StringLength(100)]
        [Description("设备型号")]
        public string DeviceModel { get; set; }

        ///<summary>
        /// 接入方式 ip方式传：IP，串口传：COM
        /// </summary>
        [StringLength(50)]
        [Description("接入方式")]
        public string AccessMode { get; set; }

        ///<summary>
        /// 设备IP或者串口
        /// </summary>
        [StringLength(100)]
        [Description("设备IP或者串口")]
        public string DeviceIPOrCom { get; set; }
    }
}