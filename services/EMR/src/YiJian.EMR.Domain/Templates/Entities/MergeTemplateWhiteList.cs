using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace YiJian.EMR.Templates.Entities
{
    /// <summary>
    /// 合并病历的白名单
    /// </summary>
    [Comment("合并病历的白名单")]
    public class MergeTemplateWhiteList : Entity<int>
    {
        private MergeTemplateWhiteList()
        {

        }

        /// <summary>
        /// 合并病历的白名单
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="templateName"></param>
        public MergeTemplateWhiteList(Guid templateId, string templateName)
        {
            TemplateId = templateId;
            TemplateName = templateName;
        }

        /// <summary>
        /// 模板ID
        /// </summary>
        [Comment("模板ID")]
        public Guid TemplateId { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        [Comment("模板名称")]
        [StringLength(100)]
        public string TemplateName { get; set; }

    }
}
