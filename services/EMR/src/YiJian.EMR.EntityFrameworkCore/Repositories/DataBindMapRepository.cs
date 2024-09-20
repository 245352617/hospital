using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Uow;
using YiJian.EMR.DataBinds.Entities;
using YiJian.EMR.EntityFrameworkCore;

namespace YiJian.EMR.DataBinds.Contracts
{
    /// <summary>
    /// 数据绑定的数据字典
    /// </summary>
    public class DataBindMapRepository : EfCoreRepository<EMRDbContext, DataBindMap, Guid>, IDataBindMapRepository
    {
        /// <summary>
        /// 数据绑定的上下文
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public DataBindMapRepository(IDbContextProvider<EMRDbContext> dbContextProvider)
         : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 批量更新绑定数据
        /// </summary>
        /// <param name="dataBindContextId"></param> 
        /// <param name="maps"></param>  
        /// <returns></returns>    
        public async Task BatchUpdateAsync(Guid dataBindContextId, Dictionary<string, Dictionary<string, string>> maps)
        {
            if (maps == null || maps.Count == 0) return;
            if (dataBindContextId == Guid.Empty) return;

            var db = await GetDbContextAsync(); 

            var entities = await db.DataBindMaps.Where(w=>w.DataBindContextId == dataBindContextId).ToListAsync();
            db.DataBindMaps.RemoveRange(entities);
             
            var data = new List<DataBindMap>();
            foreach (var datasource in maps)
            {
                foreach (var path in datasource.Value)
                {
                    data.Add(new DataBindMap(GuidGenerator.Create(), datasource.Key, path.Key, path.Value, dataBindContextId));
                } 
            } 
            await db.DataBindMaps.AddRangeAsync(data); 
        }

    }
}
