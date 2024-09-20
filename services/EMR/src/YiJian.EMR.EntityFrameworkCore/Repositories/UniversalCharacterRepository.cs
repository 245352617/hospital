using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.Characters.Contracts;
using YiJian.EMR.Characters.Entities;
using YiJian.EMR.EntityFrameworkCore;

namespace YiJian.EMR.Repositories
{
    /// <summary>
    /// 通用字符节分类
    /// </summary>
    public class UniversalCharacterRepository : EfCoreRepository<EMRDbContext, UniversalCharacter, int>, IUniversalCharacterRepository
    {
        /// <summary>
        /// 通用字符节分类
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public UniversalCharacterRepository(IDbContextProvider<EMRDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }


        /// <summary>
        /// 获取所有通用字符
        /// </summary>
        /// <returns></returns> 
        public async Task<List<UniversalCharacter>> GetAllAsync()
        {
            var db = await GetDbContextAsync();
            return await db.UniversalCharacters.Include(i=>i.Nodes).ToListAsync();
        }

        /// <summary>
        /// 获取所有通用字符分类，不包含内容
        /// </summary>
        /// <returns></returns> 
        public async Task<List<UniversalCharacter>> GetCategoriesAsync()
        {
            var db = await GetDbContextAsync(); 
            return await db.UniversalCharacters.AsNoTracking().ToListAsync(); 
        }

        /// <summary>
        /// 获取指定目录下的所有字符内容
        /// </summary>
        /// <returns></returns> 
        public async Task<UniversalCharacter> GetNodesByIdAsync(int id)
        {
            var db = await GetDbContextAsync();
            return await db.UniversalCharacters.Include(i=>i.Nodes).FirstOrDefaultAsync(w=>w.Id== id);
        }

        /// <summary>
        /// 创建分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> CreateAsync(UniversalCharacter model)
        {
            var db = await GetDbContextAsync();
            var val =  await db.UniversalCharacters.AddAsync(model);
            return val.Entity.Id;
        }

        /// <summary>
        /// 更新分类内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(UniversalCharacter model)
        {
            var db = await GetDbContextAsync();
            var val = db.UniversalCharacters.Update(model);
            return val.Entity.Id;
        }

        /// <summary>
        /// 移除分类内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> RemoveAsync(int id)
        {
            var db = await GetDbContextAsync();
            var entity = await db.UniversalCharacters.FirstOrDefaultAsync(w => w.Id == id);
            var val =  db.UniversalCharacters.Remove(entity);
            return val.Entity.Id;
        } 

    }  
}
