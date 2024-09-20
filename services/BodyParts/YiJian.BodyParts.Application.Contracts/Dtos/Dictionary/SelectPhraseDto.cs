using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 按科室员工分组
    /// </summary>
    public class PhraseGroupDto
    {
        /// <summary>
        /// 分组代码
        /// </summary>
        /// <example></example>
        public string TypeCode { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        /// <example></example>
        public string TypeName { get; set; }


        public List<SelectPhraseDto> icuPhraseDto { get; set; }
    }


    /// <summary>
    /// 护理记录模板分组
    /// </summary>
    public class SelectPhraseDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 分组代码
        /// </summary>
        /// <example></example>
        public string TypeCode { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        /// <example></example>
        public string TypeName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        public string DeptCode { get; set; }

        /// <summary>
        /// 员工代码
        /// </summary>
        /// <example></example>
        public string StaffCode { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        /// <example></example>
        public string Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        public List<IcuPhraseDto> icuPhraseDto { get; set; }
    }
}
