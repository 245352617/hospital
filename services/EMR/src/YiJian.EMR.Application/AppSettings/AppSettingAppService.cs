using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Uow;
using YiJian.EMR.ApplicationSettings.Contracts;
using YiJian.EMR.ApplicationSettings.Entities;
using YiJian.EMR.AppSettings.Dto;

namespace YiJian.EMR.AppSettings
{
    /// <summary>
    /// 应用配置
    /// </summary>
    [Authorize]
    public class AppSettingAppService : EMRAppService, IAppSettingAppService
    {
        private readonly IAppSettingRepository _appSettingRepository;

        private readonly ILogger<AppSettingAppService> _logger;
        private readonly IMemoryCache _memoryCache;

        private readonly string DC_IP_SETTING_KEY = "DC_IP_SETTING";

        public AppSettingAppService(
            IAppSettingRepository appSettingRepository,
            ILogger<AppSettingAppService> logger,
            IMemoryCache memoryCache)
        {
            _appSettingRepository = appSettingRepository;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 配置都昌电子病历组件访问的IP
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        [UnitOfWork]
        public async Task<string> IPAsync(IPInfoDto model)
        {
            var entity = await (await _appSettingRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Name == DC_IP_SETTING_KEY);
            if (entity == null)
            {
                _ = await _appSettingRepository.InsertAsync(new AppSetting(GuidGenerator.Create(), DC_IP_SETTING_KEY, model.Uri));
                _memoryCache.Set<string>(DC_IP_SETTING_KEY, model.Uri);
                return model.Uri;
            }

            entity.Data = model.Uri;
            _ = await _appSettingRepository.UpdateAsync(entity);
            _memoryCache.Set<string>(DC_IP_SETTING_KEY, model.Uri);
            return model.Uri;
        }

        /// <summary>
        /// 获取都昌电子病历组件访问的IP
        /// </summary>
        /// <returns></returns>  
        public async Task<string> GetIPAsync()
        {
            var uri = string.Empty;
            bool flag = _memoryCache.TryGetValue<string>(DC_IP_SETTING_KEY, out uri);
            if (flag) return uri;

            var entity = await (await _appSettingRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Name == DC_IP_SETTING_KEY);
            if (entity == null) return "";

            uri = entity.Data;
            _memoryCache.Set(DC_IP_SETTING_KEY, uri);
            return uri;
        }

    }
}
