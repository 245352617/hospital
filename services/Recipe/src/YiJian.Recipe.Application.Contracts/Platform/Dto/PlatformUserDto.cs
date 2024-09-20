namespace YiJian.Platform
{
    /// <summary>
    /// 平台用户
    /// </summary>
    public class PlatformUserDto
    {
        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        /// <summary>
        /// 签名，base64
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 职务（主任、副主任...）
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 职级（高级、中级、初级）
        /// </summary>
        public string TechnicalTitle { get; set; }
    }
}
