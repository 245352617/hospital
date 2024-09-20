using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.Nursing.Config;

namespace YiJian.Nursing
{
    /// <summary>
    /// 描述：读取配置
    /// 创建人： yangkai
    /// 创建时间：2022/10/10 18:23:10
    /// </summary>
    [Authorize]
    public class NursingConfigAppService : NursingAppService
    {
        private readonly INursingConfigRepository _nursingConfigRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="nursingConfigRepository"></param>
        public NursingConfigAppService(INursingConfigRepository nursingConfigRepository)
        {
            _nursingConfigRepository = nursingConfigRepository;
        }

        /// <summary>
        /// 批量创建或更新
        /// </summary>
        /// <param name="nursingConfigList"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<bool> BatchCreateAsync(List<NursingConfig> nursingConfigList)
        {
            if (nursingConfigList == null || !nursingConfigList.Any())
            {
                throw new Exception("请求参数为空");
            }

            foreach (NursingConfig item in nursingConfigList)
            {
                if (string.IsNullOrEmpty(item.Key))
                {
                    throw new Exception("key不能为空");
                }
            }

            foreach (NursingConfig item in nursingConfigList)
            {
                await CreateOrUpdateAsync(item);
            }

            return true;
        }

        /// <summary>
        /// 创建或更新配置
        /// </summary>
        /// <param name="nursingConfig"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<NursingConfig> CreateAsync(NursingConfig nursingConfig)
        {
            if (nursingConfig == null || string.IsNullOrEmpty(nursingConfig.Key))
            {
                throw new Exception("请求参数为空");
            }

            return await CreateOrUpdateAsync(nursingConfig);
        }

        /// <summary>
        /// 创建或更新versionTag配置
        /// </summary>
        /// <param name="nursingConfig"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<NursingConfig> CreateVersionTagAsync(NursingConfig nursingConfig)
        {
            if (nursingConfig == null || nursingConfig.Key != "versionTag")
            {
                throw new Exception("请求参数错误");
            }

            return await CreateOrUpdateAsync(nursingConfig);
        }

        /// <summary>
        /// 获取versionTag配置
        /// </summary>
        /// <param name="nursingConfig"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<NursingConfig> GetVersionTagAsync(NursingConfig nursingConfig)
        {
            if (nursingConfig == null || nursingConfig.Key != "versionTag")
            {
                throw new Exception("请求参数错误");
            }

            return await _nursingConfigRepository.FirstOrDefaultAsync(x => x.Key == nursingConfig.Key);
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="nursingConfig"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<NursingConfig> GetAsync(NursingConfig nursingConfig)
        {
            if (nursingConfig == null || string.IsNullOrEmpty(nursingConfig.Key))
            {
                throw new Exception("请求参数为空");
            }

            if (!string.IsNullOrEmpty(nursingConfig.NurseCode))
            {
                return await _nursingConfigRepository.FirstOrDefaultAsync(x => x.Key == nursingConfig.Key && x.NurseCode == nursingConfig.NurseCode);
            }

            return await _nursingConfigRepository.FirstOrDefaultAsync(x => x.Key == nursingConfig.Key);
        }

        /// <summary>
        /// 批量获取配置
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<List<NursingConfig>> GetListAsync(List<string> keys)
        {
            if (keys == null || !keys.Any())
            {
                throw new Exception("请求参数为空");
            }

            return await _nursingConfigRepository.GetListAsync(x => keys.Contains(x.Key));
        }

        /// <summary>
        /// 创建或更新配置
        /// </summary>
        /// <param name="nursingConfig"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<NursingConfig> CreateOrUpdateAsync(NursingConfig nursingConfig)
        {
            List<NursingConfig> nursingConfigList = await _nursingConfigRepository.GetListAsync(x => x.Key == nursingConfig.Key);
            if (!nursingConfigList.Any())
            {
                return await InsertAsync(nursingConfig);
            }

            if (!string.IsNullOrEmpty(nursingConfig.NurseCode))
            {
                NursingConfig oldNursingConfig = nursingConfigList.FirstOrDefault(x => x.NurseCode == nursingConfig.NurseCode);
                if (oldNursingConfig == null)
                {
                    return await InsertAsync(nursingConfig);
                }
                else
                {
                    oldNursingConfig.Value = nursingConfig.Value;
                    oldNursingConfig.Extra = nursingConfig.Extra;
                    await _nursingConfigRepository.UpdateAsync(oldNursingConfig);
                    return oldNursingConfig;
                }
            }

            if (nursingConfigList.Count > 1)
            {
                throw new Exception("数据异常，请核查配置数据");
            }

            NursingConfig config = nursingConfigList.First();
            config.Value = nursingConfig.Value;
            config.Extra = nursingConfig.Extra;
            await _nursingConfigRepository.UpdateAsync(config);
            return config;
        }

        /// <summary>
        /// 插入配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private async Task<NursingConfig> InsertAsync(NursingConfig config)
        {
            NursingConfig nursingConfig = new NursingConfig(Guid.NewGuid())
            {
                Key = config.Key,
                Value = config.Value,
                Extra = config.Extra,
                NurseCode = config.NurseCode
            };

            await _nursingConfigRepository.InsertAsync(nursingConfig);
            return nursingConfig;
        }
    }
}
