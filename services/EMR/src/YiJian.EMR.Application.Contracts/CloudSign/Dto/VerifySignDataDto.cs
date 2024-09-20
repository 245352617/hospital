namespace YiJian.EMR.CloudSign.Dto
{
    /// <summary>
    /// 描述：
    /// 创建人： yangkai
    /// 创建时间：2022/12/19 17:31:49
    /// </summary>
    public class VerifySignDataDto
    {
        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string BusinessOrgCode { get; set; }

        /// <summary>
        /// 医生工号
        /// </summary>
        public string RelBizNo { get; set; }

        /// <summary>
        /// base64编码格式证书
        /// </summary>
        public string SignCert { get; set; }

        /// <summary>
        /// 签名原文
        /// </summary>
        public string SourceData { get; set; }

        /// <summary>
        /// 签名值
        /// </summary>
        public string SignedData { get; set; }
    }
}
