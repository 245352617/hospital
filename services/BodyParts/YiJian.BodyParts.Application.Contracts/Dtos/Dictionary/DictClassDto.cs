using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:系统分类字典
    /// </summary>
    public class DictClassDto : EntityDto<Guid>
    {


        /// <summary>
        /// 类型
        /// </summary>
        /// <example></example>
        public string ClassType { get; set; }

        /// <summary>
        /// 字典代码
        /// </summary>
        /// <example></example>
        public string ClassCode { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        /// <example></example>
        public string ClassName { get; set; }
    }
}
