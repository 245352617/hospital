using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 项目返回参数
    /// </summary>
    public class DoctorConditionItemDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 分组Id
        /// </summary>
        public Guid? Pid { get; set; }

        /// <summary>
        /// 项目分类
        /// </summary>
        public string ParaType { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary> 
        public string UnitName { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
    }
}
