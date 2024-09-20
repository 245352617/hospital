namespace YiJian.EMR.CloudSign
{
    /// <summary>
    /// 描述：云签配置
    /// 创建人： yangkai
    /// 创建时间：2022/12/19 17:53:12
    /// </summary>
    public class CloudSign
    {
        /// <summary>
        /// 是否开启云签验证
        /// </summary>
        public bool UseCloudSign { get; set; }

        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string BusinessOrgCode { get; set; }

        /// <summary>
        /// CA业务系统编码
        /// </summary>
        public string BusinessSystemCode { get; set; }

        /// <summary>
        /// 业务系统应用ID
        /// </summary>
        public string BusinessSystemAppID { get; set; }

        /// <summary>
        /// 云签证书数字签名接口地址
        /// </summary>
        public string SignData { get; set; }

        /// <summary>
        /// 云签证书数字签名验证接口地址
        /// </summary>
        public string Verify { get; set; }
    }
}
