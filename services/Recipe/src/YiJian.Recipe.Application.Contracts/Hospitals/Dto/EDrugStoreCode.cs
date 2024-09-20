using System.ComponentModel;

namespace YiJian.Hospitals.Dto
{

    /// <summary>
    /// 药房编码码字典 
    /// </summary>
    public enum EDrugStoreCode
    {
        /// <summary>
        /// 1	    门诊西药房
        /// </summary>
        [Description("门诊西药房")]
        C1 = 1,

        /// <summary>
        /// 2	    门诊中药房
        /// </summary>
        [Description("门诊中药房")]
        C2 = 2,

        /// <summary>
        /// 3	    内科中心药房
        /// </summary>
        [Description("内科中心药房")]
        C3 = 3,

        /// <summary>
        /// 4	    急诊药房
        /// </summary>
        [Description("急诊药房")]
        C4 = 4,

        /// <summary>
        /// 5	    感染药房
        /// </summary>
        [Description("感染药房")]
        C5 = 5,

        /// <summary>
        /// 6 	外科中心药房
        /// </summary>
        [Description("外科中心药房")]
        C6 = 6,

        /// <summary>
        /// 7	    应急药房
        /// </summary>
        [Description("应急药房")]
        C7 = 7,

        /// <summary>
        /// 8	    互联网医院药品
        /// </summary>
        [Description("互联网医院药品")]
        C8 = 8,

        /// <summary>
        /// 18	结核病药房
        /// </summary>
        [Description("结核病药房")]
        C18 = 18,

        /// <summary>
        /// 19	6号楼4楼门诊药房
        /// </summary>
        [Description("6号楼4楼门诊药房")]
        C19 = 19,
    }

}