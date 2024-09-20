using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace YiJian.MasterData;

/// <summary>
/// 医生信息
/// </summary>
public interface IDoctorAppService : IApplicationService
{
    /// <summary>
    /// 获取医生分页信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<DoctorDto>> GetPageListAsync(GetDoctorPagedInput input);
}