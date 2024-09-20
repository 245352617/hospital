namespace YiJian.Documents.Dto
{
    /// <summary>
    /// 病理条码Dto
    /// </summary>
    public class PacsItemNoDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 检查项目
        /// </summary>
        public string PacsName { get; set; }

        /// <summary>
        /// 标本名称
        /// </summary>
        public string PathologyName { get; set; }

        /// <summary>
        /// 是否已打
        /// </summary>
        public bool IsPrint { get; set; }
    }
}
