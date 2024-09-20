using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 处方外层节点
/// </summary>
[Table("DrugItem")]
public class DrugItemRequest
{
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// 药品类型 0 西药 1 中成药 2 中草药
    /// </summary>
    public EDrugType DrugType { get; set; }

    /// <summary>
    /// 处方号码
    /// </summary>
    public string? PrescriptionNo { get; set; }

    /// <summary>
    /// 药房编号 
    /// <![CDATA[
    /// 药房唯一编号2.1药房编码码字典（字典、写死）
    /// ]]>
    /// </summary>
    public string? Storage { get; set; }

    /// <summary>
    /// 开处方时间 格式：YYYY-MM-DD HH24:MI:SS
    /// </summary>
    public string? PrescriptionDate { get; set; }

    /// <summary>
    /// 处方天数/贴数 
    /// <![CDATA[
    /// 阿拉伯数字、1代表一天
    /// ]]>
    /// </summary>
    public int PrescriptionDays { get; set; }

    /// <summary>
    /// 处方类型  0.普通处方 1.危急处方 2.精神处方  3.麻醉处方
    /// </summary>
    public EPrescriptionType PrescriptionType { get; set; }

    /// <summary>
    /// 代办人姓名
    /// <![CDATA[
    /// prescriptionType = 3（麻醉处方）、代办人姓名必填 
    /// ]]>
    /// </summary>
    public string? AgencyPeopleName { get; set; }

    /// <summary>
    /// 代办人证件
    /// <![CDATA[
    /// prescriptionType = 3（麻醉处方）、代办人证件必填
    /// ]]>
    /// </summary>
    public string? AgencyPeopleCard { get; set; }

    /// <summary>
    /// 代办人性别 prescriptionType = 3（麻醉处方）、1.男  2.女
    /// </summary>
    public EAgencyPeopleSex AgencyPeopleSex { get; set; }
     
    /// <summary>
    /// 代办人年龄 prescriptionType = 3（麻醉处方）、阿拉伯数字、不满一岁按一岁计算
    /// </summary>
    public int AgencyPeopleAge { get; set; }

    /// <summary>
    /// 代办人联系电话 prescriptionType = 3（麻醉处方）、联系电话
    /// </summary>
    public string? AgencyPeopleMobile { get; set; }

    /// <summary>
    /// 草药服法
    /// <![CDATA[
    /// drugType=2时必填、2.5 草药服法 drugAdministration
    /// ]]>
    /// </summary>
    public EDrugAdministration DrugAdministration { get; set; }

    /// <summary>
    /// 草药煎法
    /// <![CDATA[
    /// drugType=2时必填、2.6 草药煎法 drugDecoct
    /// ]]>
    /// </summary>
    public EDrugDecoct DrugDecoct { get; set; }

    /// <summary>
    /// 处方明细节点
    /// </summary>
    public ChargeDetailRequest ChargeDetail { get; set; }

   



}
