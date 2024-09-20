using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    public class IcuScoreStandardAndDetailDto : EntityDto<Guid>
    {
        /// <summary>
        /// 父表名称
        /// </summary>
        [CanBeNull] [StringLength(200)] public string Pname { get; set; }
        /// <summary>
        /// 子表名称
        /// </summary>
        [CanBeNull] [StringLength(200)] public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
        /// <summary>
        /// 父表ID
        /// </summary>
        public Guid PId { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        /// <example></example>
        public decimal? Score { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        /// <example></example>
        public string Remark { get; set; }
    }
}
