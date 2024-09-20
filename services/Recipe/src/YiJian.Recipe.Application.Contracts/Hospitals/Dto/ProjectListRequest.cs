using Newtonsoft.Json;
using System.Collections.Generic;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 诊疗项目外层节点
    /// </summary>
    public class ProjectListRequest
    {
        /// <summary>
        /// 202：检验  203：检查  0：诊疗、材料等…
        /// </summary>
        [JsonProperty("groupType")]
        public string GroupType { get; set; }

        /// <summary>
        /// 体格检查  203时必传
        /// </summary>
        [JsonProperty("projectPe")]
        public string ProjectPe { get; set; }

        /// <summary>
        /// 现病史 203时必传
        /// </summary>
        [JsonProperty("projectPhl")]
        public string ProjectPhl { get; set; }

        /// <summary>
        /// 诊疗项目外层节点
        /// </summary>
        [JsonProperty("projectItem")]
        public List<ProjectItemRequest> ProjectItem { get; set; }

    }

}
