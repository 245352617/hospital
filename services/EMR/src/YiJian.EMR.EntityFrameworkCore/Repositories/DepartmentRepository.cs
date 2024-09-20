using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.EntityFrameworkCore;
using YiJian.EMR.Templates.Contracts;
using YiJian.EMR.Templates.Entities;

namespace YiJian.EMR.Repositories
{
    /// <summary>
    /// 科室记录名单仓储
    /// </summary>
    public class DepartmentRepository : EfCoreRepository<EMRDbContext, Department, Guid>, IDepartmentRepository
    {
        /// <summary>
        /// 电子病历目录仓储
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public DepartmentRepository(IDbContextProvider<EMRDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
        
    }
}
