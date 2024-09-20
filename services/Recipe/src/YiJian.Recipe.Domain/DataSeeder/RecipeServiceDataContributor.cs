using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Recipe
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class RecipeServiceDataContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly ISettingParaRepository _settingParaRepository;

        public RecipeServiceDataContributor(ISettingParaRepository settingParaRepository)
        {
            _settingParaRepository = settingParaRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await CreateSettingParaAsync();
        }

        public async Task CreateSettingParaAsync()
        {
            if (!await _settingParaRepository.AnyAsync())
            {
                var list = new List<SettingPara>()
                {
                    new SettingPara(Guid.NewGuid(), "模式一   院内会诊平台推送模式；", 0, false,
                        "叙述场景：院内会诊平台主动推送给我们后台，那么同步按钮隐藏，有数据直接显示；"),
                    new SettingPara(Guid.NewGuid(), "模式二    院内会诊平台拉取模式；（显示同步按钮）", 1, false,
                        "叙述场景：需要我们主动去同步拉取医院平台数据，则需要显示同步按钮；"),
                    new SettingPara(Guid.NewGuid(), "模式三    急诊系统手动录入模式；（显示添加按钮）", 2, true,
                        "叙述场景：医院没有会诊平台，数据需要急诊用户录入，则有“添加”按钮；")
                };
                await _settingParaRepository.InsertManyAsync(list);
            }
        }
    }
}