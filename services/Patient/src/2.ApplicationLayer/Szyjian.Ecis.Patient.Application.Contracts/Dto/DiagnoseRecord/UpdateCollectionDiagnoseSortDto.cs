namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class UpdateCollectionDiagnoseSortDto
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 自增序号
        /// </summary>
        public int PD_ID { get; set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        public string AddUserCode { get; set; }
    }
}