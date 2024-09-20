using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.EntityFrameworkCore;
using YiJian.EMR.Templates.Contracts;
using YiJian.EMR.Templates.Entities;

namespace YiJian.EMR.Repositories
{
    /// <summary>
    /// 合并模板白名单
    /// </summary>
    public class MergeTemplateWhiteListRepository : EfCoreRepository<EMRDbContext, MergeTemplateWhiteList, int>, IMergeTemplateWhiteListRepository
    {
        /// <summary>
        /// 合并模板白名单
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public MergeTemplateWhiteListRepository(IDbContextProvider<EMRDbContext> dbContextProvider)
         : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 获取所有的白名单列表内容
        /// </summary>
        /// <returns></returns>
        public async Task<IList<MergeTemplateWhiteList>> GetListAsync()
        {
            var db = await GetDbContextAsync();
            var list = await db.MergeTemplateWhiteLists.ToListAsync();
            return list;
        }

        /// <summary>
        /// 添加白名单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(MergeTemplateWhiteList model)
        {
            var db = await GetDbContextAsync();
            var res = await db.MergeTemplateWhiteLists.AddAsync(model);
            return res.Entity.Id;
        }

        /// <summary>
        /// 根据白名单id移除白名单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> RemoveAsync(int id)
        {
            var db = await GetDbContextAsync();
            var entity = await db.MergeTemplateWhiteLists.FirstOrDefaultAsync(w => w.Id == id);
            if (entity == null) return 0; 
            db.MergeTemplateWhiteLists.Remove(entity); 
            return id;
        }
         
    }
}
