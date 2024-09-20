using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using YiJian.ECIS;

namespace YiJian.Recipes
{
    /// <summary>
    /// CA签证
    /// </summary>
    [Comment("CA签证")]
    public class CASignature : Entity<Guid>
    {
        /// <summary>
        /// 签名证书
        /// </summary>
        [Comment("签名证书")]
        [StringLength(200)]
        public string SignCert { get; private set; }

        /// <summary>
        /// 签名值
        /// </summary>
        [Comment("签名值")]
        [StringLength(500)]
        public string SignValue { get; private set; }

        /// <summary>
        /// 时间戳值
        /// </summary>
        [Comment("时间戳值")]
        [StringLength(200)]
        public string TimestampValue { get; private set; }

        /// <summary>
        /// Base64编码格式的签章图片
        /// </summary>
        [Comment("Base64编码格式的签章图片")]
        [StringLength(4000)]
        public string SignFlow { get; private set; }

        #region CASign

        /// <summary>
        /// CA签章
        /// </summary>
        /// <param name="signCert">签名证书</param>
        /// <param name="signValue">签名值</param>
        /// <param name="timestampValue">时间戳值</param>
        /// <param name="signFlow">Base64 编码格式的签章图片</param>
        public void CASign(string signCert, string signValue, string timestampValue, string signFlow)
        {
            SignCert = Check.Length(signCert, nameof(signCert), maxLength: 200);
            SignValue = Check.Length(signValue, nameof(signValue), maxLength: 500);
            TimestampValue = Check.Length(timestampValue, nameof(timestampValue), maxLength: 200);
            SignFlow = Check.Length(signFlow, nameof(signFlow), maxLength: 2000);
        }

        #endregion CASign
    }
}
