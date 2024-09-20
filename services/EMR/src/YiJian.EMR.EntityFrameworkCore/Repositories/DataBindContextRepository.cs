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
using YiJian.EMR.Writes.Contracts;
using YiJian.EMR.Writes.Entities; 

namespace YiJian.EMR.DataBinds.Contracts
{
    /// <summary>
    /// 数据绑定的上下文
    /// </summary>
    public class DataBindContextRepository : EfCoreRepository<EMRDbContext, DataBindContext, Guid>, IDataBindContextRepository
    {
        /// <summary>
        /// 数据绑定的上下文
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public DataBindContextRepository(IDbContextProvider<EMRDbContext> dbContextProvider)
         : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 获取数据绑定的上下文内容
        /// </summary>
        /// <param name="patientEmrId"></param>
        /// <returns></returns>
        public async Task<DataBindContext> GetDetailAsync(Guid patientEmrId)
        {
            var db = await GetDbContextAsync();
            var entity = await db.DataBindContexts
                .Include(i => i.DataBindMaps)
                .Where(w => w.PatientEmrId == patientEmrId)
                .FirstOrDefaultAsync();
            return entity;
        }
         
    }
}
