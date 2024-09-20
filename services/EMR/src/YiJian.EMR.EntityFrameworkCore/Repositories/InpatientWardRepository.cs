using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.EntityFrameworkCore;
using YiJian.EMR.Libs;
using YiJian.EMR.Templates.Entities;

namespace YiJian.EMR.Templates.Contracts
{
    /// <summary>
    /// 病区
    /// </summary>
    public class InpatientWardRepository : EfCoreRepository<EMRDbContext, InpatientWard, Guid>, IInpatientWardRepository
    {
        /// <summary>
        /// 病区
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public InpatientWardRepository(IDbContextProvider<EMRDbContext> dbContextProvider)
         : base(dbContextProvider)
        {

        }
    }
}
