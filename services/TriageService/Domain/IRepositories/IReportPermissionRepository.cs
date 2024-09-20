using System.Collections.Generic;
using Volo.Abp.Domain.Repositories;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IReportPermissionRepository : IRepository<ReportPermission, int>
    {
        List<ReportPermission> GetList(string userName);
    }
}
