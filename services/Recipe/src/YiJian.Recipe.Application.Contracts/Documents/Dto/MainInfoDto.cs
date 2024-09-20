namespace YiJian.Documents.Dto
{
    public class MainInfoDto
    {
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 分组数量
        /// </summary>
        public int GroupCount { get; set; }

        /// <summary>
        /// 药品数量
        /// </summary>
        public int MedicineCount { get; set; }

        /// <summary>
        /// 处方返回时间
        /// </summary>
        public string ResultTime { get; set; }
    }
}
