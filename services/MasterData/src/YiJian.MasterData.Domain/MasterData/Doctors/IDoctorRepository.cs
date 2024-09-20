using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData.MasterData.Doctors;

/// <summary>
/// 医生 仓储接口
/// </summary>
public interface IDoctorRepository : IRepository<Doctor, int>
{
}