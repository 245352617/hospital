using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using YiJian.Hospitals.Enums;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 记录信息
    /// </summary>
    public class Recordinfo
    {
        /// <summary>
        /// 记录类型 
        /// <![CDATA[
        /// 1.诊断 2.就诊记录 3.医嘱
        /// ]]>
        /// </summary>
        [JsonProperty("recordType")]
        [Required]
        public ERecordType RecordType { get; set; }

        /// <summary>
        /// 操作日期/结束就诊日期/暂挂时间 
        /// <![CDATA[
        /// 格式：yyyy-mm-dd hh24:mi:ss recordType = 2，则为结束就诊时间
        /// ]]>
        /// </summary>
        [JsonProperty("operatorDate")]
        [Required]
        public string OperatorDate { get; set; }

        /// <summary>
        /// 记录状态
        /// <![CDATA[
        /// 1.诊断作废/结束就诊（recordType=1/2）  
        /// 2.处方、治疗项目作废 （recordType=3）
        /// 3.检验、检查组套作废（recordType=3）
        /// 4.就诊记录召回(recordType = 2，24小时内可以使用)
        /// ]]>
        /// </summary>
        [JsonProperty("recordState")]
        [Required]
        public int RecordState { get; set; }

        /// <summary>
        /// 记录id
        /// <![CDATA[
        /// recordType=1: 4.5.2 就诊记录、诊断信息回传（his提供、需对接集成平台） diagNo、visSerialNo  
        /// recordType=2：4.5.3医嘱信息回传（his提供、需对接集成平台） prescriptionNo、 projectItemNo
        /// recordType=3：4.5.3医嘱信息回传（his提供、需对接集成平台） groupId
        /// ]]>
        /// </summary>
        [JsonProperty("recordNo")]
        [Required]
        public string RecordNo { get; set; }

        /// <summary>
        /// 记录明细id
        /// <![CDATA[
        /// recordType=2:4.5.3医嘱信息回传（his提供、需对接集成平台） chargeDetailNo、 projectDetailNo
        /// recordType = 3：默认为0
        /// ]]>
        /// </summary>
        [JsonProperty("recordItemNo")]
        [Required]
        public string RecordItemNo { get; set; }

    }


}
