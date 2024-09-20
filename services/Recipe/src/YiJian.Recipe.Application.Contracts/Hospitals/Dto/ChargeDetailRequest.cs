using Newtonsoft.Json;
using YiJian.Hospitals.Enums;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 处方明细节点
    /// </summary>
    public class ChargeDetailRequest
    {

        /// <summary>
        /// 药品编号
        /// </summary>
        [JsonProperty("drugCode")]
        public string DrugCode { get; set; }

        /// <summary>
        /// 处方明细序号
        /// </summary>
        [JsonProperty("chargeDetailNo")]
        public string ChargeDetailNo { get; set; }

        /// <summary>
        /// 药品产地id
        /// <![CDATA[
        /// 4.3.1 药品库存信息查询（his提供）  firmID
        /// ]]>
        /// </summary>
        [JsonProperty("firmID")]
        public string FirmID { get; set; }

        /// <summary>
        /// 药品数量
        /// </summary>
        [JsonProperty("drugQuantity")]
        public decimal DrugQuantity { get; set; }

        /// <summary>
        /// 药品单价
        /// </summary>
        [JsonProperty("drugPrice")]
        public decimal DrugPrice { get; set; }

        /// <summary>
        /// 药品总金额
        /// </summary>
        [JsonProperty("drugTotamount")]
        public decimal DrugTotamount { get; set; }

        /// <summary>
        /// 给药途径
        /// <![CDATA[
        /// 4.4.12 药品信息查询（HIS提供） drugChannel
        /// ]]>
        /// </summary>
        [JsonProperty("drugChannel")]
        public string DrugChannel { get; set; }

        /// <summary>
        /// 药品用法
        /// <![CDATA[
        /// 4.4.10药品用法字典（his提供） drugUsageDic
        /// ]]>
        /// </summary>
        [JsonProperty("drugUsageDic")]
        public string DrugUsageDic { get; set; }

        /// <summary>
        /// 药品组号
        /// <![CDATA[
        /// 从1开始排列、同组组号相同
        /// ]]>
        /// </summary>
        [JsonProperty("drugGroupNo")]
        public string DrugGroupNo { get; set; }

        /// <summary>
        /// 药房规格
        /// <![CDATA[
        /// 4.3.1 药品库存信息查询（his提供）  pharSpec
        /// ]]>
        /// </summary>
        [JsonProperty("pharSpec")]
        public string PharSpec { get; set; }

        /// <summary>
        /// 药品单位
        /// <![CDATA[
        /// 4.3.1 药品库存信息查询（his提供） PharmUnit
        /// ]]>
        /// </summary>
        [JsonProperty("pharmUnit")]
        public string PharmUnit { get; set; }

        /// <summary>
        /// 药房包装
        /// <![CDATA[
        /// 4.3.1 药品库存信息查询（his提供） packageAmount
        /// ]]>
        /// </summary>
        [JsonProperty("packageAmount")]
        public string PackageAmount { get; set; }

        /// <summary>
        /// 0.不需要皮试  1.需要皮试
        /// </summary>
        [JsonProperty("skinTest")]
        public int SkinTest { get; set; }

        /// <summary>
        /// 每日次数
        /// <![CDATA[
        /// 4.4.11药品频次（his提供） dailyFrequency
        /// ]]>
        /// </summary>
        [JsonProperty("dailyFrequency")]
        public string DailyFrequency { get; set; }

        /// <summary>
        /// 一次剂
        /// <![CDATA[
        /// 中草药默认：1  
        /// ]]>
        /// </summary>
        [JsonProperty("primaryDose")]
        public string PrimaryDose { get; set; }

        /// <summary>
        /// 限制标志
        /// </summary>
        [JsonProperty("restrictedDrugs")]
        public ERestrictedDrugs? RestrictedDrugs { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        /// <summary>
        /// 狂犬疫苗针次
        /// <![CDATA[
        /// 4.4.12 药品信息查询（his提供） drugAttributes = 7时必传
        /// 第一针(2次) :1
        /// 第二针:2
        /// 第三针:3
        /// 第四针:7
        /// 第五针:8
        /// ]]>
        /// </summary>
        [JsonProperty("needleTimes")]
        public int NeedleTimes { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        [JsonProperty("drugName")]
        public string DrugName { get; set; }

        /// <summary>
        /// 处方天数/贴数 
        /// <![CDATA[
        /// 阿拉伯数字、1代表一天
        /// ]]>
        /// </summary>
        [JsonProperty("prescriptionDays")]
        public int PrescriptionDays { get; set; }


    }

}
