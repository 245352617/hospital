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
    /// 输入域类型下拉项目
    /// </summary>
    public class DataElementDropdownRepository : EfCoreRepository<EMRDbContext, DataElementDropdown, Guid>, IDataElementDropdownRepository
    {
        /// <summary>
        /// 输入域类型下拉项目
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public DataElementDropdownRepository(IDbContextProvider<EMRDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
