using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.EntityFrameworkCore;
using YiJian.EMR.XmlHistories.Contracts;
using YiJian.EMR.XmlHistories.Entities;
using YiJian.EMR.Writes.Entities;
using System.Linq.Dynamic.Core;

namespace YiJian.EMR.Repositories
{
    /// <summary>
    /// 电子病历留痕仓储
    /// </summary>
    public class PatientEmrXmlHistoryRepository : EfCoreRepository<EMRDbContext, XmlHistory, Guid>, IXmlHistoryRepository
    {

        /// <summary>
        /// 电子病历留痕仓储
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public PatientEmrXmlHistoryRepository(IDbContextProvider<EMRDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

    }
}
