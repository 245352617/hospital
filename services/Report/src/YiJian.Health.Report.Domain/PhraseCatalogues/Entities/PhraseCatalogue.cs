using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.Domain.PhraseCatalogues.Entities
{
    /// <summary>
    /// 常用语目录
    /// </summary>
    [Comment("常用语目录")]
    public class PhraseCatalogue : FullAuditedAggregateRoot<int>
    {
        public PhraseCatalogue()
        {

        }

        public PhraseCatalogue(string title, ETemplateType templateType, string belonger, int sort)
        {
            Title = title;
            TemplateType = templateType;
            Belonger = belonger;
            Sort = sort;
        }

        /// <summary>
        /// 目录标题
        /// </summary>
        [Comment("目录标题")]
        [StringLength(200, ErrorMessage = "目录标题不能超过200个字符")]
        public string Title { get; set; }

        /// <summary>
        /// 模板类型，0=通用(全院)，1=科室，2=个人
        /// </summary>
        [Comment("模板类型，0=通用(全院)，1=科室，2=个人")]
        public ETemplateType TemplateType { get; set; }

        /// <summary>
        /// 归属人 
        /// <![CDATA[
        /// 如果 TemplateType=2 归属者为医生Id doctorId, 
        /// 如果 TemplateType=1 归属者为科室id deptid , 
        /// 如果 TemplateType=0 归属者为"hospital"
        /// ]]> 
        /// </summary>
        [Comment("归属人 如果 TemplateType=2 归属者为医生Id doctorId, 如果 TemplateType=1 归属者为科室id deptid , 如果 TemplateType=0 归属者为hospital")]
        [StringLength(50)]
        public string Belonger { get; set; }

        /// <summary>
        /// 排序号码
        /// </summary>
        [Comment("排序号码")]
        public int Sort { get; set; }

        /// <summary>
        /// 常用语短语
        /// </summary> 
        public virtual List<Phrase> Phrases { get; set; } = new List<Phrase>();

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="title"></param> 
        public void Update(string title)
        {
            Title = title;
        }

    }
}
