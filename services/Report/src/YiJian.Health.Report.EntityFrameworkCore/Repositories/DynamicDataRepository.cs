using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.NursingDocuments;
using YiJian.Health.Report.NursingDocuments.Contracts;
using YiJian.Health.Report.NursingDocuments.Entities;

namespace YiJian.Health.Report.Repositories
{
    /// <summary>
    /// 动态字段数据
    /// </summary>
    public class DynamicDataRepository : EfCoreRepository<ReportDbContext, DynamicData, Guid>, IDynamicDataRepository
    {
        /// <summary>
        /// 动态字段数据
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public DynamicDataRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
            
        } 
    }
}
