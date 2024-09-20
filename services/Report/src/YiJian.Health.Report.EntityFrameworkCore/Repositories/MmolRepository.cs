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
    public class MmolRepository : EfCoreRepository<ReportDbContext, Mmol, Guid>, IMmolRepository
    {
        /// <summary>
        /// 指尖血糖 mmol/L
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public MmolRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }
        
    }
}
