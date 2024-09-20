using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class CreateOrUpdateTriageDeviceDto
    {

        ///<summary>
        /// 设备编号
        /// </summary>
        public string DeviceCode { get; set; }
        ///<summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        ///<summary>
        /// 厂家信息
        /// </summary>
        public string FactoryInfo { get; set; }
        ///<summary>
        /// 设备型号
        /// </summary>
        public string DeviceModel { get; set; }
        ///<summary>
        /// 接入方式 ip方式传：IP，串口传：COM
        /// </summary>
        public string AccessMode { get; set; }

        ///<summary>
        /// 设备IP或者串口
        /// </summary>
        public string DeviceIPOrCom { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
