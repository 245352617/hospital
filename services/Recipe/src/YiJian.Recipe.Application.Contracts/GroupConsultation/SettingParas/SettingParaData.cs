using System;

namespace YiJian.Recipe
{
    /// <summary>
    /// 会诊配置
    /// </summary>
    public class SettingParaData
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 模式
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// 数值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}