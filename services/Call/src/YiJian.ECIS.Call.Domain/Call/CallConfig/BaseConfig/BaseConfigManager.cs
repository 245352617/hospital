using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using YiJian.ECIS.Call;
using YiJian.ECIS.Call.Domain.CallConfig;

namespace YiJian.ECIS.Domain.Call
{
    public class BaseConfigManager : DomainService
    {
        private readonly IBaseConfigRepository _baseConfigRepository;

        public BaseConfigManager(IBaseConfigRepository baseConfigRepository)
        {
            this._baseConfigRepository = baseConfigRepository;
        }

        /// <summary>
        /// 获取当前配置
        /// </summary>
        /// <returns></returns>
        public async Task<BaseConfig> GetCurrentConfigAsync()
        {
            //var config = this._baseConfigRepository.OrderByDescending(x => x.CreationTime).FirstOrDefault();
            var config = await this._baseConfigRepository.GetLastConfigAsync();
            if (config is null)
            {// 基础配置未初始化数据
                //throw new EcisBusinessException(CallErrorCodes.BaseConfigHasNotInitial);
                return new BaseConfig
                {
                    CurrentCallMode = CallMode.None,
                    CurrentUpdateNoHour = 8,
                    CurrentUpdateNoMinute = 0,
                    FriendlyReminder = "III级病情严重于IV级，III级优先，请耐心等待叫号；（如病情加重请立即联系前台护士）"
                };
            }
            // 配置更新（当配置次日生效的规则时，判断已到设置日期的翌日，则更新）
            DateTime nextDate = new DateTime(config.CreationTime.Year, config.CreationTime.Month, config.CreationTime.Day, config.TomorrowUpdateNoHour, config.TomorrowUpdateNoMinute, 0).AddDays(1);
            if (config.RegularEffectTime == RegularEffectTime.Tomorrow && DateTime.Now >= nextDate)
            {
                // 使新配置生效
                return await this.UpdateConfigAsync(config.TomorrowCallMode, RegularEffectTime.Immediate, config.TomorrowUpdateNoHour, config.TomorrowUpdateNoMinute);
            }

            return config;
        }

        /// <summary>
        /// 获取明日生效的配置
        /// </summary>
        /// <returns></returns>
        public async Task<BaseConfig> GetTomorrowConfigAsync()
        {
            //var config = this._baseConfigRepository.OrderByDescending(x => x.CreationTime).FirstOrDefault();
            var config = await this._baseConfigRepository.GetLastConfigAsync();
            if (config is null)
            {// 基础配置未初始化数据
                //throw new EcisBusinessException(CallErrorCodes.BaseConfigHasNotInitial);
                return new BaseConfig
                {
                    CurrentCallMode = CallMode.None,
                    CurrentUpdateNoHour = 8,
                    CurrentUpdateNoMinute = 0,
                };
            }
            // 当配置次日生效的规则时，使用次日的模式及时间
            if (config.RegularEffectTime == RegularEffectTime.Tomorrow)
            {
                return new BaseConfig
                {
                    CurrentCallMode = config.TomorrowCallMode,
                    CurrentUpdateNoHour = config.TomorrowUpdateNoHour,
                    CurrentUpdateNoMinute = config.TomorrowUpdateNoMinute,
                };
            }

            return config;
        }

        /// <summary>
        /// 获取今日更新号码时间
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetTodayBeginAsync()
        {
            var config = await this.GetCurrentConfigAsync();
            // 当前时间大于叫号设置的更新号码时间，时间计入当天
            // 当前时间小于叫号设置的更新号码时间，时间计入前一天
            var date = DateTime.Now.Hour >= config.CurrentUpdateNoHour
                       && DateTime.Now.Minute >= config.CurrentUpdateNoMinute ? DateTime.Today : DateTime.Today.AddDays(-1);
            // 当天开始时间
            DateTime todayBegin = new(date.Year, date.Month, date.Day, config.CurrentUpdateNoHour, config.CurrentUpdateNoMinute, 0);

            return todayBegin;
        }

        /// <summary>
        /// 获取明日更新号码时间
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetTomorrowBeginAsync()
        {
            var date = await this.GetTodayBeginAsync();
            var tomorrowConfig = await this.GetTomorrowConfigAsync();
            // 次日开始时间
            DateTime tomorrowBegin = new(date.AddDays(1).Year,
                                         date.AddDays(1).Month,
                                         date.AddDays(1).Day,
                                         tomorrowConfig.CurrentUpdateNoHour,
                                         tomorrowConfig.CurrentUpdateNoMinute,
                                         0);

            return tomorrowBegin;
        }

        /// <summary>
        /// 更新配置
        /// 不应该直接修改当前配置，应该保存旧版本并增加新的配置版本
        /// </summary>
        /// <returns></returns>
        public async Task<BaseConfig> UpdateConfigAsync(CallMode callMode, RegularEffectTime regularEffectTime, ushort updateNoHour, ushort updateNoMinute)
        {
            var lastConfig = await this._baseConfigRepository.GetLastConfigAsync();
            BaseConfig newConfig;
            // 立即生效：修改当前模式、次日模式等
            // 次日生效：不修改当前模式、修改次日模式
            if (regularEffectTime == RegularEffectTime.Immediate)
            {
                newConfig = new BaseConfig
                {
                    TomorrowCallMode = callMode,
                    TomorrowUpdateNoHour = updateNoHour,
                    TomorrowUpdateNoMinute = updateNoMinute,
                    CurrentCallMode = callMode,
                    CurrentUpdateNoHour = updateNoHour,
                    CurrentUpdateNoMinute = updateNoMinute,
                    RegularEffectTime = regularEffectTime,
                };
            }
            else
            {
                newConfig = new BaseConfig()
                {
                    CurrentCallMode = lastConfig?.CurrentCallMode ?? CallMode.None,  // 考虑首次配置未初始化数据
                    CurrentUpdateNoHour = lastConfig?.CurrentUpdateNoHour ?? 8,  // 考虑首次配置未初始化数据
                    CurrentUpdateNoMinute = lastConfig?.CurrentUpdateNoMinute ?? 0,  // 考虑首次配吹未初始化数据
                    TomorrowCallMode = callMode,
                    TomorrowUpdateNoHour = updateNoHour,
                    TomorrowUpdateNoMinute = updateNoMinute,
                    RegularEffectTime = regularEffectTime,
                };
            }

            return await _baseConfigRepository.InsertAsync(newConfig);
        }
    }
}
