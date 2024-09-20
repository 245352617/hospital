using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Nursing.Config
{
    /// <summary>
    /// 描述：护士站通用配置表
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 11:10:01
    /// </summary>
    [Comment("护士站通用配置表")]
    public class NursingConfig : CreationAuditedEntity<Guid>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        public NursingConfig(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 键名
        /// </summary>
        [Comment("键名")]
        public string Key { get; set; }

        /// <summary>
        /// 键值
        /// </summary>
        [Comment("键值")]
        public string Value { get; set; }

        /// <summary>
        /// 额外信息
        /// </summary>
        [Comment("额外信息")]
        public string Extra { get; set; }

        /// <summary>
        /// 用户名（对指定用户生效）
        /// </summary>
        [Comment("用户名（对指定用户生效）")]
        public string NurseCode { get; set; }
    }
}
