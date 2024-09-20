using DevExpress.Printing.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 打印配置
/// </summary>
namespace YiJian.CardReader.Settings
{
    public class ReportSetting
    {
        /// <summary>
        /// 打印中心Url
        /// </summary>
        public string? PrintCenterUrl { get; set; }

        /// <summary>
        /// 打印端口
        /// </summary>
        public int? PrintPort { get; set; }

        /// <summary>
        /// 打印IP
        /// </summary>
        public string? PrintIp { get; set; }

        /// <summary>
        /// 默认打印机
        /// </summary>
        public string? DefaultPrinter { get; set; }

        /// <summary>
        /// 默认纸张大小
        /// </summary>
        public string? DefaultPaperName { get; set; }

        /// <summary>
        /// 默认的医嘱打印纸张配置
        /// </summary>
        public string? DefaultDoctorsAdvicePaperName { get;set; }

        /// <summary>
        /// 默认的医嘱打印机配置
        /// </summary>
        public string? DefaultDoctorsAdvicePrinter { get; set; }

        /// <summary>
        /// 默认的电子病历打印纸张配置
        /// </summary>
        public string? DefaultEmrPaperName { get; set; }

        /// <summary>
        /// 默认的电子病历打印机配置
        /// </summary>
        public string? DefaultEmrPrinter { get; set; }

        /// <summary>
        /// 默认的贴瓶打印纸张配置
        /// </summary>
        public string? PastingBottlesPaperName { get; set; }


        /// <summary>
        /// 默认的贴瓶打印机配置
        /// </summary>
        public string? PastingBottlesPrinter { get; set; }


        /// <summary>
        /// 默认的腕带打印纸张配置
        /// </summary>
        public string? WristStrapPaperName { get; set; }


        /// <summary>
        /// 默认的腕带打印机配置
        /// </summary>
        public string? WristStrapPrinter { get; set; }

    }
}
