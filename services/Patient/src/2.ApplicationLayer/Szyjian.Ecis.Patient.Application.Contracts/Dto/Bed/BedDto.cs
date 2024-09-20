namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class BedDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        public string BedAreaCode { get; set; }

        /// <summary>
        /// 床位名称
        /// </summary>
        public string BedName { get; set; }

        /// <summary>
        /// 床位排序
        /// </summary>
        public int BedOrder { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
    }
}