using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.Hospitals.Contracts;
using YiJian.Health.Report.Hospitals.Entities; 

namespace YiJian.Health.Report.Repositories
{
    /// <summary>
    /// 医院的基础信息
    /// </summary>
    public class HospitalInfoRepository : EfCoreRepository<ReportDbContext, HospitalInfo, Guid>, IHospitalInfoRepository
    {
        /// <summary>
        /// 医院的基础信息
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public HospitalInfoRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }
    }
}
