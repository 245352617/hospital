using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 列表配置 Dto
    /// </summary>
    public class IcuTableConfigDto : EntityDto<Guid>
    {
        /// <summary>
        /// 类型：医嘱列表 = 1
        /// </summary>
        public ParaTypeEnum ParaType { get; set; }

        /// <summary>
        /// 字段
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 宽度（px）
        /// </summary>
        public decimal? Width { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 显示位置：center = 0,left = 1,right = 2
        /// </summary>
        public string DisplayPosition { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
