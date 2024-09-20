using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Sequences.Contracts;
using YiJian.Sequences.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 我的系列号管理器仓储
    /// </summary>
    public class MySequenceRepository : EfCoreRepository<RecipeDbContext, MySequence, Guid>, IMySequenceRepository
    {
        /// <summary>
        /// 我的系列号管理器仓储
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public MySequenceRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 确认表名和字段名获取最新的系列号
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="filedName">字段名</param>
        /// <returns></returns>
        public async Task<int> GetSequenceAsync(string tableName, string filedName)
        {
            var db = await GetDbSetAsync();
            var entity = await db.OrderByDescending(o => o.Id).FirstOrDefaultAsync(w => w.TableName == tableName && w.FiledName == filedName);

            if (entity == null)
            {
                var newEntity = new MySequence(GuidGenerator.Create(), tableName, filedName);
                _ = await InsertAsync(newEntity, autoSave: true);
                return await Task.FromResult(newEntity.Increment);
            }
            else
            {
                entity.Increment += 1;
                var updateEntity = await UpdateAsync(entity, autoSave: true);
                return await Task.FromResult(updateEntity.Increment);
            }
        }

    }
}
