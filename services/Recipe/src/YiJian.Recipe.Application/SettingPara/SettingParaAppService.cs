using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Recipe
{
    /// <summary>
    /// 会诊配置 API 
    /// </summary>
    [Authorize]
    public class SettingParaAppService : RecipeAppService, ISettingParaAppService
    {
        private readonly ISettingParaRepository _settingParaRepository;

        /// <summary>
        /// 会诊配置
        /// </summary>
        /// <param name="settingParaRepository"></param>
        public SettingParaAppService(ISettingParaRepository settingParaRepository)
        {
            _settingParaRepository = settingParaRepository;
        }

        /// <summary>
        /// 获取模式列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<SettingParaData>> GetListAsync()
        {
            var list = await _settingParaRepository.ToListAsync();
            var map = ObjectMapper.Map<List<SettingPara>, List<SettingParaData>>(list);
            return map;
        }

        /// <summary>
        /// 启用模式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<SettingParaData>> SetEnableAsync(Guid id)
        {
            var list = await _settingParaRepository.ToListAsync();
            list.ForEach(x => { x.SetEnable(x.Id == id); });
            await _settingParaRepository.UpdateManyAsync(list);
            var map = ObjectMapper.Map<List<SettingPara>, List<SettingParaData>>(list);
            return map;
        }

        /// <summary>
        /// 获取启用的会诊操作
        /// </summary>
        /// <returns></returns>
        public async Task<List<int>> GetParamAsync()
        {
            var list = await (await _settingParaRepository.GetQueryableAsync()).Where(x => x.IsEnable).ToListAsync();
            return list.Select(s => s.Value).ToList();
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <returns></returns>
        public async Task<List<SettingParaData>> ResetAsync()
        {
            var list = await _settingParaRepository.ToListAsync();
            list.ForEach(x => { x.SetEnable(x.Value == 2); });
            await _settingParaRepository.UpdateManyAsync(list);
            var map = ObjectMapper.Map<List<SettingPara>, List<SettingParaData>>(list);
            return map;
        }
    }
}