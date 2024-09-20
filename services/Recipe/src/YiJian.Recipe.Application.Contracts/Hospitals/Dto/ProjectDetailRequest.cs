using Newtonsoft.Json;
using YiJian.Hospitals.Enums;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 诊疗项目明细节点
    /// </summary>
    public class ProjectDetailRequest
    {
        /// <summary>
        /// 项目明细序号
        /// <![CDATA[
        /// 唯一识别医技诊疗明细单据的号码，用于后期状态修改 
        /// 第三方渠道医技明细表主键
        /// ]]>
        /// </summary>
        [JsonProperty("projectDetailNo")]
        public string ProjectDetailNo { get; set; }

        /// <summary>
        /// 组套ID 
        /// <![CDATA[
        /// groupType=202、203时必填
        /// ]]>
        /// </summary>
        [JsonProperty("groupId")]
        public string GroupId { get; set; }

        /// <summary>
        /// 标本编号
        /// <![CDATA[
        /// 4.4.5检验组套（his提供）  specimenNo 检验必填
        /// ]]>
        /// </summary>
        [JsonProperty("specimenNo")]
        public string SpecimenNo { get; set; }

        /// <summary>
        /// 容器id  4.4.5检验组套（his提供） 
        /// <![CDATA[
        /// 4.4.5检验组套（his提供）  containerId  检验必填
        /// ]]>
        /// </summary>
        [JsonProperty("containerId")]
        public string ContainerId { get; set; }

        /// <summary>
        /// 项目明细id
        /// <![CDATA[ 
        /// 检验：4.4.5检验组套（his提供） groupsId
        /// 检查：4.4.6检查组套（his提供） groupsId
        /// 诊疗材料:4.4.8诊疗材料字典（his提供） projectId
        /// ]]>
        /// </summary>
        [JsonProperty("groupsId")]
        public string GroupsId { get; set; }

        /// <summary>
        /// 项目类型
        /// <![CDATA[
        /// 检验：4.4.5检验组套（his提供）   projectType
        /// 检查：4.4.6检查组套（his提供）   projectType
        /// 诊疗材料:4.4.8诊疗材料字典（his提供）  projectType
        /// ]]>
        /// </summary>
        [JsonProperty("projectType")]
        public string ProjectType { get; set; }

        /// <summary>
        /// 项目归类
        /// <![CDATA[
        /// 检验：4.4.5检验组套（his提供） projectMerge
        /// 检查：4.4.6检查组套（his提供） projectMerge
        /// 诊疗材料:4.4.8诊疗材料字典（his提供）  projectMerge
        /// ]]> 
        /// </summary>
        [JsonProperty("projectMerge")]
        public string ProjectMerge { get; set; }

        /// <summary>
        /// 项目主项
        /// <![CDATA[
        /// 主项:1  次项:0 
        /// ]]>
        /// </summary>
        [JsonProperty("projectMain")]
        public string ProjectMain { get; set; }

        /// <summary>
        /// 项目单价 格式：0.00 
        /// </summary>
        [JsonProperty("projectPrice")]
        public decimal ProjectPrice { get; set; }

        /// <summary>
        /// 项目数量 格式：0.00 
        /// </summary>
        [JsonProperty("projectQuantity")]
        public decimal ProjectQuantity { get; set; }

        /// <summary>
        /// 项目总价
        /// <![CDATA[
        /// 项目数量*项目单价 格式：0.00
        /// ]]>
        /// </summary>
        [JsonProperty("projectTotamount")]
        public decimal ProjectTotamount { get; set; }

        /// <summary>
        /// 组套名称
        /// <![CDATA[
        /// groupType=202、203时必填
        /// ]]>
        /// </summary>
        [JsonProperty("projectName")]
        public string ProjectName { get; set; }

        /// <summary>
        /// 限制标志
        /// <![CDATA[
        /// -1：审批不通过、全自费
        /// 默认值：
        /// 1：记账
        /// 2:地补目录属性记账
        /// 3:重疾目录属性记账
        /// 4：进口属性记账
        /// 5：国产属性记账 
        /// ]]>
        /// </summary>
        [JsonProperty("restrictedDrugs")]
        public ERestrictedDrugs RestrictedDrugs { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [JsonProperty("remarks")]
        public string Remarks { get; set; }

    }

}
