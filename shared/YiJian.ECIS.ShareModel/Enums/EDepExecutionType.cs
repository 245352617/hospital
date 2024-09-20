using System.Xml.Serialization;

namespace YiJian.ECIS.ShareModel.Enums
{
    /// <summary>
    /// 科室跟踪执行类别
    ///<![CDATA[
    /// 0.不跟踪执行(默认开单科室)              
    /// 1.按固定科室执行(取depExecutionRules字段)
    /// 2.按病人科室执行(默认开单科室)
    /// 3.按病人病区执行（默认开单科室）         
    /// 9.按规则执行（医生选择开单科室、默认为开单科室）
    /// ]]>
    /// </summary>
    public enum EDepExecutionType
    {
        /// <summary>
        /// 0.不跟踪执行(默认开单科室)
        /// </summary>
        [XmlEnum("0")]
        UntracedExec = 0,
        /// <summary>
        /// 1.按固定科室执行(取depExecutionRules字段)
        /// </summary>
        [XmlEnum("1")]
        FixedDeptExec = 1,
        /// <summary>
        /// 2.按病人科室执行(默认开单科室)
        /// </summary>
        [XmlEnum("2")]
        PatientDeptExec = 2,
        /// <summary>
        /// 3.按病人病区执行（默认开单科室)
        /// </summary>
        [XmlEnum("3")]
        PatientWardExec = 3,
        /// <summary>
        /// 9.按规则执行（医生选择开单科室、默认为开单科室）
        /// </summary>
        [XmlEnum("9")]
        RuleExec = 9,
    }
}
