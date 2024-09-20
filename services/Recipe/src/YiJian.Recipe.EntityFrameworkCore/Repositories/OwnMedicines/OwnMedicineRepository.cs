using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.OwnMedicines.Contracts;
using YiJian.OwnMedicines.Entities;
using YiJian.Recipe.EntityFrameworkCore;

namespace YiJian.Recipe.Repositories.OwnMedicines
{
    /// <summary>
    /// 自备药
    /// </summary>
    public class OwnMedicineRepository : EfCoreRepository<RecipeDbContext, OwnMedicine, int>, IOwnMedicineRepository
    {
        /// <summary>
        /// 自备药
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public OwnMedicineRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 查询患者所有的自备药
        /// </summary>
        /// <returns></returns>
        public async Task<List<OwnMedicine>> GetMyAllAsync(Guid piid)
        {
            var db = await GetDbContextAsync();
            var query = db.OwnMedicines.Where(w => w.PIID == piid);
            return await query.ToListAsync();
        }

        /// <summary>
        /// 移除选中的一批自备药
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task RemoveOwnMedicinesAsync(List<int> ids)
        {
            var db = await GetDbContextAsync();
            var ownMedicines = db.OwnMedicines.Where(w => ids.Contains(w.Id)).ToListAsync();
            db.RemoveRange(ownMedicines);
        }

        /// <summary>
        /// 把推送过消息的数据标记为已经推送
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task SetPushAsync(IEnumerable<int> ids)
        {
            var db = await GetDbContextAsync();
            var entities = await db.OwnMedicines.Where(w => ids.Contains(w.Id)).ToListAsync();
            entities.ForEach(x => x.IsPush = true);
            db.OwnMedicines.UpdateRange(entities);
        }

    }
}
