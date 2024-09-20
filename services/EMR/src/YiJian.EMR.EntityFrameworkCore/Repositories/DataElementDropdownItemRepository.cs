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
    /// 元素下拉项
    /// </summary>
    public class DataElementDropdownItemRepository : EfCoreRepository<EMRDbContext, DataElementDropdownItem, Guid>, IDataElementDropdownItemRepository
    {
        /// <summary>
        /// 元素下拉项
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public DataElementDropdownItemRepository(IDbContextProvider<EMRDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
