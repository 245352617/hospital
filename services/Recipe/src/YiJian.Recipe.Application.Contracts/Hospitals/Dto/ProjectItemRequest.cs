using Newtonsoft.Json;
using System.Collections.Generic;

namespace YiJian.Hospitals.Dto
{

    /// <summary>
    /// 诊疗项目外层节点
    /// </summary>
    public class ProjectItemRequest
    {
        /// <summary>
        /// 执行科室
        /// <![CDATA[
        /// 本科室：0
        /// ]]>
        /// </summary>
        [JsonProperty("executeDeptCode")]
        public string ExecuteDeptCode { get; set; }

        /// <summary>
        /// 急诊标志 0.非危急   1.危急
        /// </summary>
        [JsonProperty("emergencySign")]
        public int EmergencySign { get; set; }

        /// <summary>
        /// 项目序号
        /// 唯一识别医技诊疗单据的号码，用于后期状态修改,
        /// 第三方渠道医技表主键.
        /// 检验:departmentCode(执行科室) 、specimenNo（样本类型）、containerId（容器id）相同时，项目序号相同。
        /// 检查:departmentCode(执行科室)相同时，项目序号相同。
        /// 诊疗/材料:本次提交的所有诊疗、材料可为一个项目序号
        /// 特检单据：单独一张申请单、项目序号不同
        /// 急诊单据:单独一张申请单、项目序号不同
        /// </summary>
        [JsonProperty("projectItemNo")]
        public string ProjectItemNo { get; set; }

        /// <summary>
        /// 开单时间 (格式：YYYY-MM-DD HH24:MI:SS)
        /// </summary>
        [JsonProperty("billingDate")]
        public string BillingDate { get; set; }

        /// <summary>
        /// 特检标志 默认为:0 , 0:非特检  1:特检
        /// </summary>
        [JsonProperty("specialType")]
        public int SpecialType { get; set; }


        /// <summary>
        /// 诊疗项目明细节点
        /// </summary>
        [JsonProperty("projectDetail")]
        public List<ProjectDetailRequest> ProjectDetail { get; set; }

    }

}
