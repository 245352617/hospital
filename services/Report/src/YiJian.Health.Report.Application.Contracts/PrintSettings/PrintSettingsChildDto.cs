using System;

namespace YiJian.Health.Report.PrintSettings
{
    /// <summary>
    /// 打印设置子项Dto
    /// </summary>
    public class PrintSettingsChildDto
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// his处方号
        /// </summary>
        public string PrescriptionNo { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string MyPrescriptionNo { get; set; }

        /// <summary>
        /// 传参Url
        /// </summary>
        public string ParamUrl { get; set; }

        /// <summary>
        /// 分方途径配置的Id
        /// </summary>
        public Guid? SeparationId { get; set; }

        /// <summary>
        /// 模板id
        /// </summary>
        public Guid TemplateId { get; set; }

        /// <summary>
        /// 打印单据类型编码，映射ReportTypeCode 简单判断Name 返回指定的 命令参数:-1=不处理, 0=获取处方单,1=获取注射单,2=获取输液单,3=获取检验单,4=获取检查单,5=获取处置单,6=获取治疗单,7=获取雾化单,8=获取预防接种单，9=麻醉单
        /// </summary>
        public string Comm { get; set; }

        /// <summary>
        /// 纸张编码
        /// </summary>
        public string PageSizeCode { get; set; }

        /// <summary>
        /// 纸张高度
        /// </summary>
        public decimal PageSizeHeight { get; set; }

        /// <summary>
        /// 纸张宽度
        /// </summary>
        public string PageSizeWidth { get; set; }

        /// <summary>
        /// 是否已打印
        /// </summary>
        public int IsPrint { get; set; }

        /// <summary>
        /// 是否危急处方  是:true ，否:false  默认是true
        /// </summary>
        public bool IsCriticalPrescription { get; set; } = true;

        /// <summary>
        /// 药理  2=麻醉、精一药品  
        /// </summary>
        public bool IsNarcotic { get; set; } = false;

    }
}