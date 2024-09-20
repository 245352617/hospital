using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 耗材返回Dto
    /// </summary>
    public class DictConsumableDto : EntityDto<Guid>
    {
        /// <summary>
        /// 耗材分类
        /// </summary>
        public string ConsumType { get; set; }

        /// <summary>
        /// 耗材名称
        /// </summary>
        public string ConsumName { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string PinYin { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 耗材编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// HIS编码
        /// </summary>
        public string HisCode { get; set; }
    }
}
