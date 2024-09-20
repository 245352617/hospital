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
    /// 数据元素
    /// </summary>
    public class DataElementItemRepository : EfCoreRepository<EMRDbContext, DataElementItem, Guid>, IDataElementItemRepository
    {
        /// <summary>
        /// 数据元素
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public DataElementItemRepository(IDbContextProvider<EMRDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
