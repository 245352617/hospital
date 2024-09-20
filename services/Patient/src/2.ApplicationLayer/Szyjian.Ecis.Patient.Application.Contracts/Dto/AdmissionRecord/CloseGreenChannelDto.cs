namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 关闭绿通
    /// </summary>
    public class CloseGreenChannelDto
    {
        /// <summary>
        /// 患者Id
        /// </summary>
        public int AR_ID { get; set; }

        /// <summary>
        /// 是否开通
        /// </summary>
        public bool IsOpen { get; set; }
    }
}