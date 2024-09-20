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

namespace YiJian.Health.Report.Repositories
{
    /// <summary>
    /// 动态字段名字描述
    /// </summary>
    public class DynamicFieldRepository : EfCoreRepository<ReportDbContext, DynamicField, Guid>, IDynamicFieldRepository
    {
        /// <summary>
        /// 动态字段名字描述
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public DynamicFieldRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
        }
    }
}
