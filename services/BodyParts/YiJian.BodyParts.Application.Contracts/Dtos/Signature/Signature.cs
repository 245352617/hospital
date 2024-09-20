using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class Signature
    {
        /// <summary>
        /// 签名Id
        /// </summary>
        public Guid SignatureId { get; set; }

        /// <summary>
        /// 签名工号
        public string SignNurseCode { get; set; }

        /// <summary>
        /// 签名名称
        /// </summary>
        public string SignNurseName { get; set; }

        /// <summary>
        /// 签名图片
        /// </summary>
        public string SignImage { get; set; }
    }
}
