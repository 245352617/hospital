using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace YiJian.MasterData;

public interface IOperationAppService : IApplicationService
{
    /// <summary>
    /// 手术字典查询分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<OperationDto>> GetOperationPagedListAsync(OperationInput input);
}