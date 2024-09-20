using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.EntityFrameworkCore;
using YiJian.EMR.Templates.Entities;

namespace YiJian.EMR.Templates.Contracts
{
    /// <summary>
    /// 模板目录结构
    /// </summary>
    public class TemplateCatalogueRepository : EfCoreRepository<EMRDbContext, TemplateCatalogue, Guid>, ITemplateCatalogueRepository
    {
        /// <summary>
        /// 模板目录结构
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public TemplateCatalogueRepository(IDbContextProvider<EMRDbContext> dbContextProvider): base(dbContextProvider)
        {

        }
        
    }
}
