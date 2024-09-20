using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class ReportCardEditDto : EntityDto<Guid>
    {
        #region Property
        /// <summary>
        /// 报卡名称
        /// </summary>
        [Required, StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 报卡编码
        /// </summary>
        [Required, StringLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsActived { get; set; } = true;

        /// <summary>
        /// 排序权重，数值越高排序越前，默认0
        /// </summary>
        public int Sort { get; set; } = 0;
        #endregion

    }
}
