using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiJian.ECIS.Call.CallConfig.Dtos;
using YiJian.ECIS.Call.Domain.CallConfig;
using YiJian.ECIS.Domain.Call;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.ECIS.Call.CallConfig
{
    /// <summary>
    /// 【基础设置】应用服务
    /// </summary>
    public class BaseConfigAppService : CallAppService, IBaseConfigAppService
    {
        private readonly IBaseConfigRepository _baseConfigRepository;
        private readonly BaseConfigManager _baseConfigManager;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="baseConfigRepository"></param>
        /// <param name="baseConfigManager"></param>
        public BaseConfigAppService(IBaseConfigRepository baseConfigRepository
            , BaseConfigManager baseConfigManager)
        {
            _baseConfigRepository = baseConfigRepository;
            _baseConfigManager = baseConfigManager;
        }

        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <returns></returns>
        public async Task<BaseConfigData> GetAsync()
        {
            // 配置可能保存多个版本，获取最新生效的版本配置
            var config = await _baseConfigManager.GetCurrentConfigAsync();

            return ObjectMapper.Map<BaseConfig, BaseConfigData>(config);
        }

        /// <summary>
        /// 获取温馨提示信息
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetFriendlyReminderAsync()
        {
            var config = await _baseConfigManager.GetCurrentConfigAsync();

            return config.FriendlyReminder;
        }

        /// <summary>
        /// 修改温馨提醒
        /// </summary>
        /// <param name="friendlyReminder"></param>
        /// <returns></returns>
        public async Task UpdateFriendlyReminderAsync(string friendlyReminder)
        {
            var config = await _baseConfigManager.GetCurrentConfigAsync();
            config.EditFriendlyReminder(friendlyReminder);
            if (config.Id > 0)
            {
                await _baseConfigRepository.UpdateAsync(config);
            }
            else
            {
                await _baseConfigRepository.InsertAsync(config);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateAsync(BaseConfigUpdate input)
        {
            List<ushort> hourMinute = new();
            try
            {
                hourMinute = input.UpdateNoTime.Split(":").Select(x => ushort.Parse(x)).ToList();
            }
            catch (Exception)
            {
                Oh.Error("时间格式不正确");
            }
            if (input.UpdateNoTime.Length != 4 && input.UpdateNoTime.Length != 5)
            {
                Oh.Error("时间格式不正确");
            }
            if (hourMinute.Count != 2 || hourMinute[0] < 0 || hourMinute[0] >= 24 || hourMinute[1] < 0 || hourMinute[1] >= 60)
            {
                Oh.Error("时间格式不正确");
            }

            // 使用领域服务的修改
            await _baseConfigManager.UpdateConfigAsync(input.CallMode, input.RegularEffectTime, hourMinute[0], hourMinute[1]);
        }
    }
}
