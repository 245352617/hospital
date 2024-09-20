using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.Recipe.EntityFrameworkCore.Repositories
{
    public class UserSettingRepository : RecipeRepositoryBase<UserSetting, Guid>,
        IUserSettingRepository
    {
        public UserSettingRepository(IDbContextProvider<RecipeDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取用户配置信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserSetting>> GetUserSettingsAsync(string username)
        {
            var db = await GetDbSetAsync();
            return await db.AsQueryable().AsNoTracking().Where(x => x.UserName == username).ToListAsync();
        }
    }
}