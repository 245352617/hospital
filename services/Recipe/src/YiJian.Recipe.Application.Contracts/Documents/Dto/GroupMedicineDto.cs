namespace YiJian.Documents.Dto
{
    public class GroupMedicineDto
    {
        /// <summary>
        /// 分录ID（排序）
        /// </summary>
        public int EntryId { get; set; }

        /// <summary>
        /// 组号
        /// </summary>
        public string RecipeNo { get; set; } = "";

        /// <summary>
        /// 同组的药品数量
        /// </summary>
        public int MedicineCount { get; set; }
        public decimal GroupAmount { get; set; }
    }
}