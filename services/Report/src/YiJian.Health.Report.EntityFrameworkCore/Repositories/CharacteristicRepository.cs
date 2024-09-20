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
    /// 病人特征记录
    /// </summary>
    public class CharacteristicRepository : EfCoreRepository<ReportDbContext, Characteristic, Guid>, ICharacteristicRepository
    {
        /// <summary>
        /// 病人特征记录
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public CharacteristicRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }
    }
}
