using System;
using System.Collections.Generic;
using YiJian.Hospitals.Enums;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 历史医嘱信息
    /// </summary>
    public class HistoryDoctorsAdvicesDto
    {
        /// <summary>
        /// 医嘱信息
        /// </summary>
        public MedDetailResultDto MedDetailResultDto { get; set; }
        /// <summary>
        /// 处方信息
        /// </summary>
        public List<PrescribeInfoDto> PrescribeInfoDtos { get; set; }
    }

    /// <summary>
    /// 历史处方信息
    /// </summary>
    public class PrescribeInfoDto
    {
        /// <summary>
        /// 医嘱Id
        /// </summary>
        public Guid? DoctorsAdviceId { get; set; }

        /// <summary>
        /// 开方编码
        /// </summary> 
        public string Code { get; set; }

        /// <summary>
        /// 开方名称
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        /// 开药天数
        /// </summary>  
        public int? LongDays { get; set; }

        /// <summary>
        /// 每次剂量
        /// </summary> 
        public decimal? DosageQty { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary> 
        public string DosageUnit { get; set; }

        /// <summary>
        /// 领量(数量)
        /// </summary> 
        public decimal? RecieveQty { get; set; }

        /// <summary>
        /// 领量单位
        /// </summary>
        public string RecieveUnit { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary>
        public string UsageName { get; set; }

        /// <summary>
        /// 频次
        /// </summary> 
        public string FrequencyName { get; set; }

        /// <summary>
        /// 频次码
        /// </summary>
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 皮试结果:false=阴性 ture=阳性
        /// </summary>
        public bool? SkinTestResult { get; set; }

        /// <summary>
        /// 限制用药标记 “1.限制用药 2.非限制用药” 出现1.限制用药需要弹出 限制费用提示消息
        /// </summary>
        public int LimitType { get; set; }
        /// <summary>
        /// 收费类型
        /// </summary> 
        public ERestrictedDrugs? RestrictedDrugs { get; set; }

        /// <summary>
        /// 医嘱号
        /// </summary> 
        public string RecipeNo { get; set; }

        /// <summary>
        /// 医嘱子号（同组下参数修改）
        /// </summary>
        public int RecipeGroupNo { get; set; } = 1;

        /// <summary>
        /// 加收标志	
        /// </summary>
        public bool? Additional { get; set; }

        /// <summary>
        /// 是否皮试 false=不需要皮试 true=需要皮试
        /// </summary>
        public bool? IsSkinTest { get; set; }

        /// <summary>
        /// 皮试选择结果
        /// </summary>
        public ESkinTestSignChoseResult? SkinTestSignChoseResult { get; set; }

        /// <summary>
        /// 限制性用药标识
        /// </summary>
        public bool? IsLimited { get; set; }

        /// <summary>
        ///  医嘱项目分类编码
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// 处方权
        /// </summary>
        public int? PrescriptionPermission { get; set; }

        /// <summary>
        /// 抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级
        /// </summary> 
        public int? AntibioticLevel { get; set; }

        /// <summary>
        /// 精神药  0非精神药,1一类精神药,2二类精神药
        /// </summary> 
        public int? ToxicLevel { get; set; }

        /// <summary>
        /// 附加类型
        /// </summary>
        public EAdditionalItemType? AdditionalItemsType { get; set; }

        /// <summary>
        /// 单价
        /// </summary>  
        public decimal Price { get; set; }

        /// <summary>
        /// 执行科室名称
        /// </summary> 
        public string ExecDeptName { get; set; }

        /// <summary>
        /// 位置
        /// </summary>  
        public string PositionName { get; set; }

        /// <summary>
        /// 检查目录名称
        /// </summary>
        public string CatalogName { get; set; }

        /// <summary>
        /// 检查部位
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 标本名称
        /// </summary>
        public string SpecimenName { get; set; }

        /// <summary>
        /// 标本容器
        /// </summary> 
        public string ContainerName { get; set; }

        /// <summary>
        /// 药房编码
        /// </summary> 
        public string PharmacyCode { get; set; }

        /// <summary>
        /// 药房
        /// </summary> 
        public string PharmacyName { get; set; }
    }


    /// <summary>
    /// ALLHistoryDoctorsAdvices
    /// </summary>
    public class ALLHistoryDoctorsAdvices
    {
        /// <summary>
        /// ID
        /// </summary> 
        public Guid Id { get; set; }
        /// <summary>
        /// 病人ID
        /// <![CDATA[
        /// 4.5.2 就诊记录、诊断信息回传（his提供、需对接集成平台） patientId
        /// ]]>
        /// </summary> 
        public string PatientId { get; set; }

        /// <summary>
        /// 科室名称 
        /// <![CDATA[
        /// 一级科室名称:4.4.1 科室字典（his提供） deptName
        /// ]]>
        /// </summary> 
        public string DeptName { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary> 
        public string DoctorName { get; set; }

        /// <summary>
        /// His识别号 对应his处方识别（C）、医技序号（Y）  可用于二维码展示等
        /// </summary>
        public string HisNumber { get; set; }

        /// <summary>
        /// 开嘱时间
        /// </summary>
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 缴费
        /// </summary>
        public string IsPay { get; set; }

        /// <summary>
        /// 医嘱Id
        /// </summary>
        public Guid? DoctorsAdviceId { get; set; }

        /// <summary>
        /// 开方编码
        /// </summary> 
        public string Code { get; set; }

        /// <summary>
        /// 开方名称
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        /// 开药天数
        /// </summary>  
        public int? LongDays { get; set; }

        /// <summary>
        /// 每次剂量
        /// </summary> 
        public decimal? DosageQty { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary> 
        public string DosageUnit { get; set; }

        /// <summary>
        /// 领量(数量)
        /// </summary> 
        public decimal? RecieveQty { get; set; }

        private string _recieveUnit;

        /// <summary>
        /// 领量单位
        /// </summary> 
        public string RecieveUnit
        {
            get
            {
                if (CategoryCode == "Lab" || CategoryCode == "Exam")
                {
                    if (_recieveUnit.IsNullOrEmpty()) return "次";
                }
                return _recieveUnit;
            }
            set { _recieveUnit = value; }
        }


        /// <summary>
        /// 单价
        /// </summary>  
        public decimal Price { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary>
        public string UsageName { get; set; }

        /// <summary>
        /// 频次
        /// </summary> 
        public string FrequencyName { get; set; }

        /// <summary>
        /// 频次码
        /// </summary>
        public string FrequencyCode { get; set; }


        /// <summary>
        /// 皮试结果:false=阴性 ture=阳性
        /// </summary>
        public bool? SkinTestResult { get; set; }

        /// <summary>
        /// 限制用药标记 “1.限制用药 2.非限制用药” 出现1.限制用药需要弹出 限制费用提示消息
        /// </summary>
        public int? LimitType { get; set; }
        /// <summary>
        /// 收费类型
        /// </summary> 
        public ERestrictedDrugs? RestrictedDrugs { get; set; }

        /// <summary>
        /// 医嘱号
        /// </summary> 
        public string RecipeNo { get; set; }

        /// <summary>
        /// 医嘱子号（同组下参数修改））
        /// </summary>
        public int? RecipeGroupNo { get; set; } = 1;

        /// <summary>
        /// 加收标志	
        /// </summary>
        public bool? Additional { get; set; }

        /// <summary>
        /// 是否皮试 false=不需要皮试 true=需要皮试
        /// </summary>
        public bool? IsSkinTest { get; set; }

        /// <summary>
        /// 皮试选择结果
        /// </summary>
        public ESkinTestSignChoseResult? SkinTestSignChoseResult { get; set; }

        /// <summary>
        /// 限制性用药标识
        /// </summary>
        public bool? IsLimited { get; set; }

        /// <summary>
        ///  医嘱项目分类编码
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// 处方权
        /// </summary>
        public int? PrescriptionPermission { get; set; }

        /// <summary>
        /// 抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级
        /// </summary> 
        public int? AntibioticLevel { get; set; }

        /// <summary>
        /// 精神药  0非精神药,1一类精神药,2二类精神药
        /// </summary> 
        public int? ToxicLevel { get; set; }

        /// <summary>
        /// 附加类型
        /// </summary>
        public EAdditionalItemType? AdditionalItemsType { get; set; }

        /// <summary>
        /// 执行科室名称
        /// </summary> 
        public string ExecDeptName { get; set; }

        /// <summary>
        /// 位置
        /// </summary>  
        public string PositionName { get; set; }

        /// <summary>
        /// 检查目录名称
        /// </summary>
        public string CatalogName { get; set; }

        /// <summary>
        /// 检查部位
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 标本名称
        /// </summary>
        public string SpecimenName { get; set; }

        /// <summary>
        /// 标本容器
        /// </summary> 
        public string ContainerName { get; set; }

        /// <summary>
        /// 药房编码
        /// </summary> 
        public string PharmacyCode { get; set; }

        /// <summary>
        /// 药房
        /// </summary> 
        public string PharmacyName { get; set; }
    }

}
