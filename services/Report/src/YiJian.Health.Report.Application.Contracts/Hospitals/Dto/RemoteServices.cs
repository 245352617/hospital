using System.Collections.Generic;

namespace YiJian.Health.Report.Hospitals.Dto
{
    /// <summary>
    /// 远程服务配置BaseAddress
    /// </summary>
    public class RemoteServices
    {
        /// <summary>
        /// 字典服务BaseAddress
        /// </summary>
        public BaseAddress Default { get; set; }

        /// <summary>
        /// 患者服务BaseAddress
        /// </summary>
        public BaseAddress Patient { get; set; }

        /// <summary>
        /// 医院的服务BaseAddress
        /// </summary>
        public BaseAddress Hospital { get; set; }
        /// <summary>
        /// 云签BaseAddress
        /// </summary>
        public BaseAddress StampBase { get; set; }
        /// <summary>
        /// 认证中心 BaseAddress
        /// </summary>
        public IEnumerable<BaseAddress> Identity { get; set; }

        /// <summary>
        /// 是否龙岗
        /// </summary>
        public bool LDC { get; set; }
    }

    /// <summary>
    /// 基础地址
    /// </summary>
    public class BaseAddress
    {
        /// <summary>
        /// 基础URL
        /// </summary>
        public string BaseUrl { get; set; }
    }

}
