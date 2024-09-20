using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.MasterData;

/// <summary>
/// 检查部位 API
/// </summary>
[Authorize]
public class ExamPartAppService : MasterDataAppService, IExamPartAppService
{
    private readonly IExamPartRepository _examPartRepository;

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="examPartRepository"></param>
    public ExamPartAppService(IExamPartRepository examPartRepository)
    {
        _examPartRepository = examPartRepository;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> CreateAsync(ExamPartCreation input)
    {
        if (await _examPartRepository.AnyAsync(a => a.PartCode == input.PartCode))
        {
            throw new EcisBusinessException(message: "编码已存在");
        }

        var model = ObjectMapper.Map<ExamPartCreation, ExamPart>(input);
        var result = await _examPartRepository.InsertAsync(model);
        return result.Id;
    }

    #endregion Create

    #region Update

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task UpdateAsync(ExamPartUpdate input)
    {
        var examPart = await _examPartRepository.GetAsync(input.Id);
        if (examPart == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        examPart.Modify(partName: input.PartName, // 检查部位名称
            sort: input.Sort
        );
        await _examPartRepository.UpdateAsync(examPart);
    }

    #endregion Update

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ExamPartData> GetAsync(int id)
    {
        var examPart = await _examPartRepository.GetAsync(id);

        return ObjectMapper.Map<ExamPart, ExamPartData>(examPart);
    }

    #endregion Get

    #region Delete

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(int id)
    {
        if (!await _examPartRepository.AnyAsync(a => a.Id == id))
        {
            throw new EcisBusinessException(message: "数据不存在");
        }
        await _examPartRepository.DeleteAsync(id);
    }

    #endregion Delete

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<ExamPartData>> GetListAsync(
        string filter = null,
        string sorting = null)
    {
        var result = await _examPartRepository.GetListAsync(filter, sorting);

        return new ListResultDto<ExamPartData>(
            ObjectMapper.Map<List<ExamPart>, List<ExamPartData>>(result));
    }

    #endregion GetList

    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<ExamPartData>> GetPagedListAsync(GetExamPartPagedInput input)
    {
        var examParts = await _examPartRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Sorting);

        var items = ObjectMapper.Map<List<ExamPart>, List<ExamPartData>>(examParts);

        var totalCount = await _examPartRepository.GetCountAsync(input.Filter);

        var result = new PagedResultDto<ExamPartData>(totalCount, items.AsReadOnly());

        return result;
    }

    #endregion GetPagedList
}