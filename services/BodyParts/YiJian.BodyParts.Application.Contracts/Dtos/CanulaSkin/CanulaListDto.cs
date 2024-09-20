using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 返回导管护理列
    /// </summary>
    public class CanulaListDto
    {
        /// <summary>
        /// 动态列表
        /// </summary>
        public List<CanulaItemDtos> CanulaItemDto { get; set; }
    }

    /// <summary>
    /// 导管返回参数
    /// </summary>
    public class CanulaItemDtos
    {
        /// <summary>
        /// 项目代码
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 项目值
        /// </summary>
        public string ParaValue { get; set; }
    }
}

