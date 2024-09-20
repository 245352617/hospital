using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.EntityFrameworkCore;
using YiJian.EMR.Libs;
using YiJian.EMR.Libs.Entities;

namespace YiJian.EMR.Repositories
{
    /// <summary>
    /// 电子病历目录仓储
    /// </summary>
    public class CatalogueRepository : EfCoreRepository<EMRDbContext, Catalogue, Guid>, ICatalogueRepository
    {
        /// <summary>
        /// 电子病历目录仓储
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public CatalogueRepository(IDbContextProvider<EMRDbContext> dbContextProvider)
          : base(dbContextProvider)
        {
             
        } 
    }
}
