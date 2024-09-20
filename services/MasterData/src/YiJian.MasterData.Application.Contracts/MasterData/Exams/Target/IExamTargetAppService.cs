using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace YiJian.MasterData.Exams;

/// <summary>
/// 检查明细项 API Interface
/// </summary>   
public interface IExamTargetAppService : IApplicationService
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> CreateAsync(ExamTargetCreation input);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateAsync(ExamTargetUpdate input);

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ExamTargetData> GetAsync(int id);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(int id);

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="proCode">ProjectCode 项目编码</param>
    /// <param name="firstCode">FirstNodeCode 一级目录编码</param>
    /// <returns></returns>
    Task<ListResultDto<ExamTargetData>> GetListAsync(string proCode,
        string firstCode = null,
        string filter = null,
        string sorting = null);

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<ExamTargetData>> GetPagedListAsync(GetExamTargetPagedInput input);
}