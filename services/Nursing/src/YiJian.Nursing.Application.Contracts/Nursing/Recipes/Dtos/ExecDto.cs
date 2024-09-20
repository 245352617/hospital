namespace YiJian.Nursing.Recipes.Dtos
{
    /// <summary>
    /// 描述：执行请求Dto
    /// 创建人： yangkai
    /// 创建时间：2023/3/9 14:38:26
    /// </summary>
    public class ExecDto : BaseRequestDto
    {
        /// <summary>
        /// 备用量
        /// </summary>
        public decimal ReserveDosage { get; set; }

        /// <summary>
        /// 执行入量
        /// </summary>
        public decimal Dosage { get; set; }

        /// <summary>
        /// 余量
        /// </summary>
        public decimal RemainDosage { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 是否废弃
        /// </summary>
        public bool? IsDiscard { get; set; }

        /// <summary>
        /// 签名图片
        /// </summary>
        public string Signature { get; set; } = string.Empty;
    }
}
