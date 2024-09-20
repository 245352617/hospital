using System;

namespace YiJian.Documents.Dto
{
    /// <summary>
    /// 打印的检验项的基本信息内容
    /// </summary>
    public class LisAdviceDto : BaseAdviceDto
    {
        /// <summary>
        /// 检验类别编码
        /// </summary> 
        public string CatalogCode { get; set; }

        /// <summary>
        /// 检验类别
        /// </summary> 
        public string CatalogName { get; set; }

        /// <summary>
        /// 临床症状
        /// </summary> 
        public string ClinicalSymptom { get; set; }

        /// <summary>
        /// 标本编码
        /// </summary> 
        public string SpecimenCode { get; set; }

        /// <summary>
        /// 标本名称
        /// </summary> 
        public string SpecimenName { get; set; }

        /// <summary>
        /// 标本采集部位编码
        /// </summary> 
        public string SpecimenPartCode { get; set; }

        /// <summary>
        /// 标本采集部位
        /// </summary> 
        public string SpecimenPartName { get; set; }

        /// <summary>
        /// 标本容器代码
        /// </summary> 
        public string ContainerCode { get; set; }

        /// <summary>
        /// 标本容器
        /// </summary> 
        public string ContainerName { get; set; }

        /// <summary>
        /// 标本容器颜色:0=红帽,1=蓝帽,2=紫帽
        /// </summary> 
        public string ContainerColor { get; set; }

        /// <summary>
        /// 标本说明
        /// </summary> 
        public string SpecimenDescription { get; set; }

        /// <summary>
        /// 标本采集时间
        /// </summary> 
        public DateTime? SpecimenCollectDatetime { get; set; }

        /// <summary>
        /// 标本接收时间
        /// </summary> 
        public DateTime? SpecimenReceivedDatetime { get; set; }

        /// <summary>
        /// 出报告时间
        /// </summary> 
        public DateTime? ReportTime { get; set; }

        /// <summary>
        /// 确认报告医生编码
        /// </summary> 
        public string ReportDoctorCode { get; set; }

        /// <summary>
        /// 确认报告医生
        /// </summary> 
        public string ReportDoctorName { get; set; }

        /// <summary>
        /// 报告标识
        /// </summary> 
        public bool HasReportName { get; set; }

        /// <summary>
        /// 是否紧急
        /// </summary> 
        public bool IsEmergency { get; set; }

        /// <summary>
        /// 是否在床旁
        /// </summary> 
        public bool IsBedSide { get; set; }

        /// <summary>
        /// 附加卡片类型  
        /// 15.孕母血清胎儿唐氏综合征筛查申请单(早、中期)
        /// 14.新型冠状病毒RNA检测申请单
        /// 13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单
        /// </summary> 
        public string AddCard { get; set; }

    }

}
