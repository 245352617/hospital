using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 压疮皮肤参数
    /// </summary>
    public class SkinPartDto
    {
        /// <summary>
        /// 参数代码
        /// </summary>
        public string DictCode { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string DictValue { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// 部位编号
        /// </summary>
        public string PartNumber { get; set; }

        /// <summary>
        /// 是否正面
        /// </summary>
        public bool? Ispositive { get; set; }
    }
}
