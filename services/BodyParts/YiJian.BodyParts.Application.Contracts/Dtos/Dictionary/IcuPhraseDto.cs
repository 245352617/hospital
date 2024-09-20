using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:常用语模板
    /// </summary>
    public class IcuPhraseDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 类型代码
        /// </summary>
        /// <example></example>
        public string TypeCode { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        /// <example></example>
        public string TypeName { get; set; }

        /// <summary>
        /// 上级编号
        /// </summary>
        /// <example></example>
        public string ParentId { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        public string DeptCode { get; set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        /// <example></example>
        public string PhraseText { get; set; }

        /// <summary>
        /// 是否有效(1-有效，0-无效)
        /// </summary>
        /// <example></example>
        public int ValidState { get; set; }
    }

    /// <summary>
    /// 表:常用模板补充字段
    /// </summary>
    public class IcuPhraseReplenishDto : IcuPhraseDto
    {
        /// <summary>
        /// 员工代码
        /// </summary>
        public string StaffCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
    }
}
