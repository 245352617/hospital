using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.ECIS.Call.CallConfig.Dtos
{
    /// <summary>
    /// 叫号设置【基础设置】修改 DTO
    /// direction：input
    /// </summary>
    [Serializable]
    public class BaseConfigUpdate
    {
        /// <summary>
        /// 当前叫号模式
        /// 1: 诊室固定
        /// 2: 医生变动
        /// </summary>
        [Required(ErrorMessage = "当前叫号模式必填"), Range(0, 2, ErrorMessage = "叫号模式值不符合规范")]
        public CallMode CallMode { get; set; }

        /// <summary>
        /// 模式生效时间
        /// 0: 立即生效（默认）
        /// 1: 次日生效
        /// </summary>
        [Required(ErrorMessage = "模式生效时间必填"), Range(0, 1, ErrorMessage = "模式生效时间值不符合规范")]
        public RegularEffectTime RegularEffectTime { get; set; }

        /// <summary>
        /// 每日更新号码时间
        /// 形如：08:00  或 8:00
        /// </summary>
        [Required(ErrorMessage = "每日更新号码时间必填")]
        //[RegularExpression(@"^([0-1]?[0-9]|2[0-3]):([0-5][0-9])$", ErrorMessage = "每日更新号码时间格式有误")]
        public string UpdateNoTime { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}
