using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.ECIS.Call.CallConfig.Dtos
{
    /// <summary>
    /// 【排队号规则】创建 DTO
    /// direction：input
    /// </summary>
    [Serializable]
    public class SerialNoRuleCreation
    {
        /// <summary>
        /// 科室id
        /// </summary>
        [Required(ErrorMessage = "科室id必填")]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// 开头字母
        /// </summary>
        [Required(ErrorMessage = "开头字母必填")]
        [RegularExpression(@"^\w{1,3}$", ErrorMessage = "开头字母由1~3位字母或数字组成")]
        public string Prefix { get; set; }

        /// <summary>
        /// 流水号位数
        /// </summary>
        [Required(ErrorMessage = "位数必填")]
        [Range(0, 8, ErrorMessage = "位数最大长度8字符")]
        public ushort SerialLength { get; set; }
    }
}
