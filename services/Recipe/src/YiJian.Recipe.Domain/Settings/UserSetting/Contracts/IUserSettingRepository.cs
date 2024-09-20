using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Recipe
{
    public interface IUserSettingRepository : IRepository<UserSetting, Guid>
    {
        /// <summary>
        /// ��ȡ�û������и���������Ϣ
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<List<UserSetting>> GetUserSettingsAsync(string username);
    }
}