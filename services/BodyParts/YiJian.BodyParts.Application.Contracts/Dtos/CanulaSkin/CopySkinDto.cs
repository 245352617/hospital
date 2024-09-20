using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 皮肤复制
    /// </summary>
    public class CopySkinDto
    {
        /// <summary>
        /// 皮肤护理记录Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 复制选中：1，复制上一条：2
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 当前登录人
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 当前登录人code
        /// </summary>
        public string Staffcode { get; set; }

        /// <summary>
        /// 签名图片
        /// </summary>
        public string Signature { get; set; } = string.Empty;

    }
}
