namespace Szyjian.Ecis.Patient.Domain.Shared
{
    /// <summary>
    /// 远程服务配置BaseAddress
    /// </summary>
    public class RemoteServices
    {
        /// <summary>
        /// 字典服务BaseAddress
        /// </summary>
        public RemoteBaseAddress Default { get; set; }

        /// <summary>
        /// 患者服务BaseAddress
        /// </summary>
        public RemoteBaseAddress Patient { get; set; }

        /// <summary>
        /// 医院的服务BaseAddress
        /// </summary>
        public RemoteBaseAddress Hospital { get; set; }
        /// <summary>
        /// 云签BaseAddress
        /// </summary>
        public RemoteBaseAddress StampBase { get; set; }
        /// <summary>
        /// 分诊
        /// </summary>
        public RemoteBaseAddress Triage { get; set; }
        /// <summary>
        /// 叫号系统
        /// </summary>
        public RemoteBaseAddress Call { get; set; }

        /// <summary>
        /// 是否是龙岗
        /// </summary>
        public bool LDC { get; set; }

        /// <summary>
        /// DDP服务地址
        /// </summary>
        public RemoteBaseAddress DDP { get; set; }

    }

    /// <summary>
    /// 服务地址信息
    /// </summary>
    public class RemoteBaseAddress
    {
        /// <summary>
        /// URL
        /// </summary>
        public string BaseUrl { get; set; }
    }

    /// <summary>
    /// 科室信息
    /// </summary>
    public class DeptInfoDto
    {
        /// <summary>
        /// 分诊科室代码
        /// </summary>
        public string TriageConfigCode { get; set; }

        /// <summary>
        /// 医院的科室代码
        /// </summary>
        public string HisConfigCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string TriageConfigName { get; set; }
    }
}
