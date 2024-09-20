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
    /// 护理单
    /// </summary>
    public class NursingDocumentRepository : EfCoreRepository<ReportDbContext, NursingDocument, Guid>, INursingDocumentRepository
    {
        /// <summary>
        /// 护理单
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public NursingDocumentRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
         : base(dbContextProvider)
        { 
        }

    }
}
