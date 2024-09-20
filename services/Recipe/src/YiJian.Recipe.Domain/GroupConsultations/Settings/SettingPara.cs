using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.Recipe
{
    /// <summary>
    /// 会诊配置
    /// </summary>
    [Comment("会诊配置")]
    public class SettingPara : Entity<Guid>
    {
        /// <summary>
        /// 模式
        /// </summary>
        [StringLength(100)]
        [Comment("模式")]
        public string Mode { get; set; }

        /// <summary>
        /// 数值
        /// </summary>
        [Comment("数值")]
        public int Value { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Comment("是否启用")]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Comment("备注")]
        [StringLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mode"></param>
        /// <param name="value"></param>
        /// <param name="isEnable"></param>
        /// <param name="remark"></param>
        public SettingPara(Guid id, string mode, int value, bool isEnable, string remark)
        {
            Id = id;
            Mode = mode;
            Value = value;
            IsEnable = isEnable;
            Remark = remark;
        }

        /// <summary>
        /// 启用禁用
        /// </summary>
        /// <param name="isEnable"></param>
        public void SetEnable(bool isEnable)
        {
            IsEnable = isEnable;
        }

        private SettingPara()
        {
        }
    }
}