using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using YiJian.EMR.DataElements.Entities;
using YiJian.EMR.EntityFrameworkCore;
using YiJian.EMR.DataElements.Contracts;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.EMR.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class DataElementRepository : EfCoreRepository<EMRDbContext, DataElement, Guid>, IDataElementRepository
    {
        /// <summary>
        /// 数据元集合根
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public DataElementRepository(IDbContextProvider<EMRDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
