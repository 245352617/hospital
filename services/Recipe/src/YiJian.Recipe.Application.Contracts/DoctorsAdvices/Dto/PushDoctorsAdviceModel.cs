namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 分方之后的信息和医院HIS返回的单号信息的信息
    /// </summary>
    public class PushDoctorsAdviceModel
    {
        /// <summary>
        /// 分方之后的信息和医院HIS返回的单号信息的信息
        /// </summary>
        /// <param name="medType"></param>
        /// <param name="channelNumber"></param>
        /// <param name="hisNumber"></param>
        public PushDoctorsAdviceModel(string medType, string channelNumber, string hisNumber)
        {
            MedType = medType;
            ChannelNumber = channelNumber;
            HisNumber = hisNumber;
        }


        /// <summary>
        /// 医嘱类型 处方：CF   非处方:YJ
        /// </summary> 
        public string MedType { get; set; }

        /// <summary>
        /// 渠道识别号 （自己的）
        /// </summary> 
        public string ChannelNumber { get; set; }

        /// <summary>
        /// His识别号 （HIS的）  对应his处方识别（C）、医技序号（Y） 
        /// </summary> 
        public string HisNumber { get; set; }

    }

}
