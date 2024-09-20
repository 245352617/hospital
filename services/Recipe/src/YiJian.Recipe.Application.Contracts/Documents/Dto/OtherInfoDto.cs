namespace YiJian.Documents.Dto
{
    /// <summary>
    /// 报表的其他信息，包括二维码，条形码...
    /// </summary>
    public class OtherInfoDto
    {
        /// <summary>
        /// 二维码
        /// </summary>
        public string QRCode1 { get; set; }

        /// <summary>
        /// 条形码
        /// </summary>
        public string Barcode1 { get; set; }


        /// <summary>
        /// 二维码2
        /// </summary>
        public string QRCode2 { get; set; }

        /// <summary>
        /// 条形码2
        /// </summary>
        public string Barcode2 { get; set; }

    }

}
