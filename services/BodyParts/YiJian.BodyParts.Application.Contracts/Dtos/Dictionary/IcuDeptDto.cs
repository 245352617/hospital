using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:字典-科室字典
    /// </summary>
    public class IcuDeptDto : EntityDto<Guid>
    {
        /// <summary>
        /// 院区代码
        /// </summary>
        public string WardCode { get; set; }

        /// <summary>
        /// 院区名称
        /// </summary>
        public string WardName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        /// <example></example>
        public string DeptName { get; set; }

        /// <summary>
        /// 科室简称
        /// </summary>
        public string DeptRefe { get; set; }

        /// <summary>
        /// 院区代码
        /// </summary>
        /// <example></example>
        public string HosRegionCode { get; set; }

        /// <summary>
        /// 院区名称
        /// </summary>
        /// <example></example>
        public string HosRegionName { get; set; }

        /// <summary>
        /// 是否ICU
        /// </summary>
        public bool? IsICU { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 是否有效(1-有效，0-无效)
        /// </summary>
        /// <example></example> 
        public int ValidState { get; set; } = 1;
    }
}
