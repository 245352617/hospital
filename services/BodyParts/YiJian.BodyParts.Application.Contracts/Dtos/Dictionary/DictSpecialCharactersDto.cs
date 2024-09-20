using System;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:护理记录特殊字符
    /// </summary>
    public class DictSpecialCharactersDto:EntityDto<Guid>
    {
        /// <summary>
        /// 特殊字符内容
        /// </summary>
        public string Character { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 有效状态（1-有效，0-无效）
        /// </summary>
        public int ValidState { get; set; }
    }
}
