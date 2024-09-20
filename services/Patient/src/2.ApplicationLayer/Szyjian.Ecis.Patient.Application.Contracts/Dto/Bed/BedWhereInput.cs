namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class BedWhereInput
    {
        public string BedAreaCode { get; set; }

        public int Id { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool? IsShow { get; set; }
    }
}