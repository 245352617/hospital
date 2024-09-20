using Newtonsoft.Json;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 医嘱信息节点、回传成功数据 
    /// </summary>
    public class MedDetailResponse
    {
        /// <summary>
        /// 医嘱类型 处方：CF   非处方:YJ
        /// </summary>
        [JsonProperty("medType")]
        public string MedType { get; set; }

        /// <summary>
        /// 渠道识别号  4.5.3医嘱信息回传（his提供、需对接集成平台） chargeDetailNo  projectItemNo
        /// </summary>
        [JsonProperty("channelNumber")]
        public string ChannelNumber { get; set; }

        /// <summary>
        /// His识别号  对应his处方识别（C）、医技序号（Y）  可用于二维码展示等
        /// </summary>
        [JsonProperty("hisNumber")]
        public string HisNumber { get; set; }

        /// <summary>
        /// HIS申请单号 处方：处方号码   医技：申请单id（检验、检查返回）
        /// </summary>
        [JsonProperty("channelNo")]
        public string ChannelNo { get; set; }

        /// <summary>
        /// 支付二维码 深圳市龙岗中心医院微信公众号支付二维码
        /// </summary>
        [JsonProperty("lgzxyyPayurl")]
        public string lgzxyyPayurl { get; set; }

        /// <summary>
        /// 支付二维码 深圳市龙岗健康在线支付二维码
        /// </summary>
        [JsonProperty("lgjkzxPayurl")]
        public string lgjkzxPayurl { get; set; }

        /// <summary>
        /// 性质 用于申请单性质显示
        /// </summary>
        [JsonProperty("medNature")]
        public string MedNature { get; set; }

        /// <summary>
        /// 费别 用于申请单费别显示
        /// </summary>
        [JsonProperty("medFee")]
        public string MedFee { get; set; }
    }
}
