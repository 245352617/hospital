using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace YiJian.ECIS.Call.Domain.CallConfig
{
    /// <summary>
    /// 基础设置种子数据
    /// </summary>
    public class BaseConfigDataContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IBaseConfigRepository _baseConfigRepository;

        public BaseConfigDataContributor(IBaseConfigRepository baseConfigRepository)
        {
            this._baseConfigRepository = baseConfigRepository;
        }

        /// <summary>
        /// 播种
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await this._baseConfigRepository.CountAsync() <= 0)
            {
                // 默认配置
                // 诊室固定、次日生效、生效时间08:00
                var defaultConfig = new BaseConfig(CallMode.ConsultingRoomRegular, RegularEffectTime.Tomorrow, 8, 0);
                defaultConfig.EditFriendlyReminder("III级病情严重于IV级，III级优先，请耐心等待叫号；（如病情加重请立即联系前台护士）");

                await this._baseConfigRepository.InsertAsync(defaultConfig);
            }
        }
    }
}
