using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class ReportPermissionRepository : EfCoreRepository<PreHospitalTriageDbContext, ReportPermission, int>, IReportPermissionRepository
    {
        public ReportPermissionRepository(IDbContextProvider<PreHospitalTriageDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<ReportPermission> GetList(string userName)
        {
            return DbContext.ReportPermission.Where(x => x.UserName == userName).ToList();
        }
    }
}
