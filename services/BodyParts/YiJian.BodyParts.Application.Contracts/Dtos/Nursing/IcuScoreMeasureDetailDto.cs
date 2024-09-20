using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:评分措施明细表
    /// </summary>
    public class IcuScoreMeasureDetailDto : EntityDto<Guid>
    {


        /// <summary>
        /// 措施子项名称
        /// </summary>
        /// <example></example>
        [Required] 
        public string Name { get; set; }

        /// <summary>
        /// 措施子项编号
        /// </summary>
        /// <example></example>
        public string Code { get; set; }

        /// <summary>
        /// 措施名称
        /// </summary>
        /// <example></example>
        [Required] 
        public string Pname { get; set; }

        /// <summary>
        /// 措施编号
        /// </summary>
        /// <example></example> 
        public string Pcode { get; set; }

        /// <summary>
        /// 措施Id
        /// </summary>
        public Guid Pid { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int SortNum { get; set; }
    }
}
