using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.EntityFrameworkCore;
using YiJian.EMR.Props.Contracts;
using YiJian.EMR.Props.Entities;

namespace YiJian.EMR.Repositories
{
    /// <summary>
    /// 电子病历属性
    /// </summary>
    public class CategoryPropertyRepository : EfCoreRepository<EMRDbContext, CategoryProperty, Guid>, ICategoryPropertyRepository
    {
        public CategoryPropertyRepository(IDbContextProvider<EMRDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
