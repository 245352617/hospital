using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace YiJian.EMR.Templates.Dto
{
    /// <summary>
    /// 合并病历的白名单
    /// </summary>
    [Comment("合并病历的白名单")]
    public class MergeTemplateWhiteListDto : EntityDto<int?>
    {
        private MergeTemplateWhiteListDto()
        {

        }

        /// <summary>
        /// 合并病历的白名单
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="templateName"></param>
        public MergeTemplateWhiteListDto(Guid templateId, string templateName)
        {
            TemplateId = templateId;
            TemplateName = templateName;
        }

        /// <summary>
        /// 模板ID
        /// </summary> 
        public Guid TemplateId { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary> 
        [StringLength(100)]
        public string TemplateName { get; set; }

    }
}
