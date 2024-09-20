using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.ECIS.Call.Domain.CallConfig
{
    /// <summary>
    /// 【叫号基础设置】领域实体
    /// </summary>
    //[Table("CallBaseConfig")]
    public class BaseConfig : AuditedAggregateRoot<int>
    {
        /// <summary>
        /// 设置的 当前叫号模式
        /// </summary>
        [Required]
        [Comment("当前叫号模式")]
        public virtual CallMode TomorrowCallMode { get; internal set; }

        /// <summary>
        /// 设置的 模式生效时间
        /// </summary>
        [Required]
        public virtual RegularEffectTime RegularEffectTime { get; internal set; }

        /// <summary>
        /// 设置的 每日更新号码时间（小时）（0-23）
        /// </summary>
        [Required, ValueRange(0, 23)]
        public virtual ushort TomorrowUpdateNoHour { get; internal set; }

        /// <summary>
        /// 设置的 每日更新号码时间（分钟）（0-59）
        /// </summary>
        [Required]
        public virtual ushort TomorrowUpdateNoMinute { get; internal set; }

        /// <summary>
        /// 当前叫号模式
        /// </summary>
        public virtual CallMode CurrentCallMode { get; internal set; }

        /// <summary>
        /// 当前的 每日更新号码时间（小时）（0-23）
        /// </summary>
        public virtual ushort CurrentUpdateNoHour { get; internal set; }

        /// <summary>
        /// 当前的 每日更新号码时间（分钟）（0-59）
        /// </summary>
        public virtual ushort CurrentUpdateNoMinute { get; internal set; }

        /// <summary>
        /// 温馨提醒（大屏叫号端）
        /// </summary>
        [MaxLength(1000)]
        public virtual string FriendlyReminder { get; internal set; }

        internal BaseConfig()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="callMode">当前叫号模式</param>
        /// <param name="regularEffectTime">模式生效时间</param>
        /// <param name="updateNoHour">每日更新号码时间（小时）（0-23）</param>
        /// <param name="updateNoMinute">每日更新号码时间（分钟）（0-59）</param>
        public BaseConfig([NotNull] CallMode callMode, [NotNull] RegularEffectTime regularEffectTime, ushort updateNoHour, ushort updateNoMinute)
            : this(regularEffectTime)
        {
            this.SetCallMode(callMode, updateNoHour, updateNoMinute);
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="regularEffectTime">模式生效时间</param>
        public BaseConfig([NotNull] RegularEffectTime regularEffectTime)
        {
            this.RegularEffectTime = Check.NotNull(regularEffectTime, nameof(RegularEffectTime));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="callMode">当前叫号模式</param>
        /// <param name="updateNoHour">每日更新号码时间（小时）（0-23）</param>
        /// <param name="updateNoMinute">每日更新号码时间（分钟）（0-59）</param>
        public BaseConfig EditCallMode([NotNull] CallMode callMode, ushort updateNoHour, ushort updateNoMinute)
        {
            this.SetCallMode(callMode, updateNoHour, updateNoMinute);

            return this;
        }

        /// <summary>
        /// 修改生效时间
        /// </summary>
        /// <param name="regularEffectTime"></param>
        /// <returns></returns>
        public BaseConfig EditRegularEffectTime([NotNull] RegularEffectTime regularEffectTime)
        {
            this.RegularEffectTime = Check.NotNull(regularEffectTime, nameof(RegularEffectTime));

            return this;
        }

        /// <summary>
        /// 修改友情提醒
        /// </summary>
        /// <param name="friendlyReminder"></param>
        /// <returns></returns>
        public BaseConfig EditFriendlyReminder([NotNull] string friendlyReminder)
        {
            this.FriendlyReminder = friendlyReminder;
            return this;
        }

        private void SetCallMode([NotNull] CallMode callMode, ushort updateNoHour, ushort updateNoMinute)
        {
            Check.NotNull(callMode, nameof(TomorrowCallMode));
            if (updateNoHour > 23 || updateNoHour < 0)
            {
                throw new EcisBusinessException(CallErrorCodes.HourInvalid);
            }
            if (updateNoMinute > 59 || updateNoMinute < 0)
            {
                throw new EcisBusinessException(CallErrorCodes.MinuteInvalid);
            }

            if (this.RegularEffectTime == RegularEffectTime.Immediate)
            {
                this.CurrentCallMode = callMode;
                this.CurrentUpdateNoHour = updateNoHour;
                this.CurrentUpdateNoMinute = updateNoMinute;
            }
            else
            {
                this.TomorrowCallMode = callMode;
                this.TomorrowUpdateNoHour = updateNoHour;
                this.TomorrowUpdateNoMinute = updateNoMinute;
            }
        }
    }
}
