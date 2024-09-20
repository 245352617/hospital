using System; 
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.Hospitals.Entities;

namespace YiJian.Health.Report.Hospitals.Contracts
{
    /// <summary>
    /// 医院的基础信息
    /// </summary>
    public interface IHospitalInfoRepository : IRepository<HospitalInfo, Guid>
    {
    }
}
