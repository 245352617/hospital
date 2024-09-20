using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.Recipe
{
    public interface IUserSettingAppService : IApplicationService
    {
        Task<List<UserSettingDto>> GetListAsync(string userName);
        Task SavePersonSettingsAsync(List<UserSettingDto> personSettingDtos);
    }
}