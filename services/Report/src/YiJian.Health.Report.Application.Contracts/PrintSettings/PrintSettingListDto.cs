using System;
using System.Collections.Generic;

namespace YiJian.Health.Report.PrintSettings
{
    /// <summary>
    /// 打印设置列表Dto
    /// </summary>
    public class PrintSettingListDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 目录Id
        /// </summary>
        public Guid CataLogId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 打印单据类型编码，映射ReportTypeCode 简单判断Name 返回指定的 命令参数:-1=不处理, 0=获取处方单,1=获取注射单,2=获取输液单,3=获取检验单,4=获取检查单,5=获取处置单,6=获取治疗单,7=获取雾化单,8=获取预防接种单，9=麻醉单，41=检查条码
        /// </summary>
        public string Comm { get; set; }

        /// <summary>
        /// 传参Url
        /// </summary>
        public string ParamUrl { get; set; }

        /// <summary>
        /// 模板类型（FastReport, DevExpress, Html）
        /// </summary>
        public string TempType { get; set; }

        /// <summary>
        /// 打印方式（Web, CLodop, SzyjTray）
        /// </summary>
        public string PrintMethod { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 创建时间，上传时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreationName { get; set; }

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
        /// 布局
        /// </summary>
        public string Layout { get; set; }

        /// <summary>
        /// 是否预览
        /// </summary>
        public bool IsPreview { get; set; }

        /// <summary>
        /// 分方途径配置的Id
        /// </summary>
        public Guid? SeparationId { get; set; }

        /// <summary>
        /// 用法编码
        /// </summary>
        public string UsageCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 单据下级
        /// </summary>
        public List<PrintSettingsChildDto> Child { get; set; }

        /// <summary>
        /// 页面数量
        /// </summary>
        public int PageCount { get; set; }
    }
}