using YiJian.BodyParts;
using YiJian.BodyParts.Application;
using YiJian.BodyParts.Application.Contracts;
using YiJian.BodyParts.Application.Contracts.Dtos;
using YiJian.BodyParts.Application.Contracts.Dtos.Nursing;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing
{
    public class PatientIotParaItem
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 设备参数代码
        /// </summary>
        public string DeviceParaCode { get; set; }

        /// <summary>
        /// 设备参数类型（1-测量值，2-设定值）
        /// </summary>
        public string DeviceParaType { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        public string DecimalDigits { get; set; }

        /// <summary>
        /// 值类型
        /// </summary>
        public string ValueType { get; set; }

        /// <summary>
        /// 采集时间点
        /// </summary>
        public string DeviceTimePoint { get; set; }
    }
}
