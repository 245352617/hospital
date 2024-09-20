namespace YiJian.EMR.CloudSign.Dto
{
    /// <summary>
    /// 描述：
    /// 创建人： yangkai
    /// 创建时间：2022/12/19 14:53:15
    /// </summary>
    public class SigndataDto
    {
        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string BusinessOrgCode { get; set; }

        /// <summary>
        /// 业务系统编码
        /// </summary>
        public string BusinessSystemCode { get; set; }

        /// <summary>
        /// 业务系统应用ID
        /// </summary>
        public string BusinessSystemAppID { get; set; }

        /// <summary>
        /// 业务类型编码
        /// </summary>
        public string BusinessTypeCode { get; set; }

        /// <summary>
        /// 加密口令
        /// </summary>
        public string EncryptedToken { get; set; }

        /// <summary>
        /// 病人id
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 业务系统id
        /// </summary>
        public string BizId { get; set; }

        /// <summary>
        /// 签名原文
        /// </summary>
        public string Base64SourceData { get; set; }

        /// <summary>
        /// 是否进行时间戳签名
        /// </summary>
        public bool WithTsa { get; set; }
    }
}
