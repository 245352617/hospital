using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.Characters.Contracts;
using YiJian.EMR.Characters.Entities;
using YiJian.EMR.EntityFrameworkCore;

namespace YiJian.EMR.Repositories
{
    /// <summary>
    /// 通用字符节点内容
    /// </summary>
    public class UniversalCharacterNodesRepository : EfCoreRepository<EMRDbContext, UniversalCharacterNode, int>, IUniversalCharacterNodesRepository
    {
        /// <summary>
        /// 通用字符节点内容
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public UniversalCharacterNodesRepository(IDbContextProvider<EMRDbContext> dbContextProvider) : base(dbContextProvider)
        {
            
        }
         
        /// <summary>
        /// 添加通用符内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public async Task<int> CreateAsync(UniversalCharacterNode model)
        {
            var db = await GetDbContextAsync();
            var val = await  db.UniversalCharacterNodes.AddAsync(model);
            return val.Entity.Id;
        }

        /// <summary>
        /// 更新通用符内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(UniversalCharacterNode model)
        {
            var db = await GetDbContextAsync();
            var val = db.UniversalCharacterNodes.Update(model);
            return val.Entity.Id;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> RemoveAsync(int id)
        {
            var db = await GetDbContextAsync();
            var entity = await db.UniversalCharacterNodes.FirstOrDefaultAsync(w=>w.Id== id);
            var val = db.UniversalCharacterNodes.Remove(entity);
            return val.Entity.Id;
        }
         
    } 
}
