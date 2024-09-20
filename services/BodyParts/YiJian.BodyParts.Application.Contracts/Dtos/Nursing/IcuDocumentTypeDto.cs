using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:文书分类
    /// </summary>
    public class IcuDocumentTypeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 文书类别标签
        /// </summary>
        /// <example></example>
        public string Categroy { get; set; }


        /// <summary>
        /// 模板编号,Url参数编码
        /// </summary>
        /// <example></example>
        public string TemplateCode { get; set; }

        /// <summary>
        /// 文书名
        /// </summary>
        /// <example></example>
        public string Name { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        /// <example></example>
        public Guid? Pid { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        /// <example></example>
        public int SortNum { get; set; }
    }

    /// <summary>
    /// 表:文书分类
    /// </summary>
    public class IcuDocument
    {
        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 文书类别标签
        /// </summary>
        /// <example></example>
        public string Categroy { get; set; }      

        /// <summary>
        /// 分类Id
        /// </summary>
        /// <example></example>
        public Guid? Pid { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        /// <example></example>
        public int SortNum { get; set; }

        public List<Template> templates { get; set; }
    }
    /// <summary>
    /// 文书列表
    /// </summary>
    public class Template 
    {
        /// <summary>
        /// 模板编号,Url参数编码
        /// </summary>
        /// <example></example>
        public string TemplateCode { get; set; }

        /// <summary>
        /// 文书名
        /// </summary>
        /// <example></example>
        public string Name { get; set; }
    }
}
