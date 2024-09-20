using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.Recipe
{
    public interface ISettingParaAppService : IApplicationService
    {
        Task<List<SettingParaData>> GetListAsync();

        Task<List<SettingParaData>> SetEnableAsync(Guid id);

        Task<List<int>> GetParamAsync();

        Task<List<SettingParaData>> ResetAsync();
    }
}