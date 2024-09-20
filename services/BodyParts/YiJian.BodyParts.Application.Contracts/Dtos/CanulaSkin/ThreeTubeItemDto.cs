using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 三管监测返回
    /// </summary>
    public class ThreeTubeItemDto
    {
        /// <summary>
        /// 导管分类
        /// </summary>
        /// <example></example>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 导管分类名称
        /// </summary>
        /// <example></example>
        public string ModuleName { get; set; }

        /// <summary>
        /// 导管部位列表
        /// </summary>
        public List<PartStartDto> partStartDto { get; set; }
    }

    /// <summary>
    /// 导管部位Dto
    /// </summary>
    public class PartStartDto
    {
        /// <summary>
        /// 插管部位
        /// </summary>
        /// <example></example>
        public string CanulaPart { get; set; }

        /// <summary>
        /// 插管时间列表
        /// </summary>
        public List<CanulaStartDto> StartTime { get; set; }
    }

    /// <summary>
    /// 插管时间Dto
    /// </summary>
    public class CanulaStartDto
    {
        /// <summary>
        /// 插管时间
        /// </summary>
        public string StartTime { get; set; }
    }
}
