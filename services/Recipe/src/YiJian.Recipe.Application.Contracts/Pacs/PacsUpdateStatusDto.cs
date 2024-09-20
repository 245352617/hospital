namespace YiJian.Recipe.Pacss
{
    /// <summary>
    /// 更新检查申请单状态Dto
    /// </summary>
    public class PacsUpdateStatusDto
    {
        /// <summary>
        /// 执行模式
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        public string Sqdh { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public string Zxsj { get; set; }

        /// <summary>
        /// 执行工号
        /// </summary>
        public string Zxgh { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
    }
}
