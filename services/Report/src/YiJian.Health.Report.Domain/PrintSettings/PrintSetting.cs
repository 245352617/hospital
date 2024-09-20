using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Health.Report.PrintSettings
{
    /// <summary>
    /// 打印设置
    /// </summary>
    [Comment("打印设置")]
    public class PrintSetting : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cataLogId"></param>
        /// <param name="name"></param>
        /// <param name="paramUrl"></param>
        /// <param name="sort"></param>
        /// <param name="status"></param>
        /// <param name="tempContent"></param>
        /// <param name="creationName"></param>
        /// <param name="code"></param>
        /// <param name="pageSizeCode"></param>
        /// <param name="layout"></param>
        /// <param name="isPreview"></param>
        /// <param name="tempType"></param>
        /// <param name="printMethod"></param>
        /// <param name="reportTypeCode"></param>
        public PrintSetting(Guid id, Guid cataLogId, string name, string paramUrl, int sort, bool status,
            string tempContent, string creationName, string code, string pageSizeCode,  string layout, bool isPreview, string tempType, string printMethod, string reportTypeCode, string remark)
        {
            Id = id;
            CataLogId = cataLogId;
            CreationName = creationName;
            Code = code;
            Update(name, paramUrl, sort, status, tempContent, pageSizeCode, layout,
                isPreview, tempType, printMethod,reportTypeCode,remark);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paramUrl"></param>
        /// <param name="sort"></param>
        /// <param name="status"></param>
        /// <param name="tempContent"></param>
        /// <param name="pageSizeCode"></param>
        /// <param name="layout"></param>
        /// <param name="isPreview"></param>
        /// <param name="tempType"></param>
        /// <param name="printMethod"></param>
        /// <param name="reportTypeCode"></param>
        public void Update(string name, string paramUrl, int sort, bool status, string tempContent, string pageSizeCode,
             string layout, bool isPreview, string tempType, string printMethod,string reportTypeCode, string remark)
        {
            Name = name;
            ParamUrl = paramUrl;
            Sort = sort;
            TempContent = tempContent;
            PageSizeCode = pageSizeCode;
            Layout = layout;
            TempType = tempType;
            PrintMethod = printMethod;
            ReportTypeCode = reportTypeCode;
            Remark = remark;
            SetIsPreview(isPreview);
            SetStatus(status);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="status"></param>
        public void SetStatus(bool status)
        {
            Status = status;
        }

        /// <summary>
        /// 修改预览
        /// </summary>
        /// <param name="isPreview"></param>
        public void SetIsPreview(bool isPreview)
        {
            IsPreview = isPreview;
        }

        /// <summary>
        /// 目录Id
        /// </summary>
        [Comment("目录Id")]
        public Guid CataLogId { get; private set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Comment("编码")]
        [StringLength(50)]
        public string Code { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Comment("名称")]
        [StringLength(100)]
        public string Name { get; private set; }

        /// <summary>
        /// 传参Url
        /// </summary>
        [Comment("传参Url")]
        [StringLength(200)]
        public string ParamUrl { get; private set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        public int Sort { get; private set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Comment("状态")]
        public bool Status { get; private set; }

        /// <summary>
        /// 模板类型（FastReport, DevExpress, Html）
        /// </summary>
        [Comment("模板类型")]
        public string TempType { get; private set; }

        /// <summary>
        /// 打印方式（Web, CLodop, SzyjTray）
        /// </summary>
        [Comment("打印方式")]
        public string PrintMethod { get; private set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        [Comment("模板内容")]
        [XmlText]
        public string TempContent { get; private set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Comment("创建人名称")]
        [StringLength(100)]
        public string CreationName { get; private set; }

        /// <summary>
        /// 纸张编码
        /// </summary>
        [Comment("纸张编码")]
        [StringLength(50)]
        public string PageSizeCode { get; private set; }

        /// <summary>
        /// 布局
        /// </summary>
        [Comment("布局")]
        [StringLength(20)]
        public string Layout { get; private set; }

        /// <summary>
        /// 是否预览
        /// </summary>
        [Comment("是否预览")]
        public bool IsPreview { get; private set; }
        /// <summary>
        /// 单据类型编码
        /// </summary>
        [Comment("单据类型编码")]
        [StringLength(50)]
        public string ReportTypeCode { get;private set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        [Comment("备注")]
        [StringLength(500)]
        public string Remark { get;private set; }
    }
}