using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.MasterData;


/// <summary>
/// 检查申请注意事项 API
/// </summary>
[Authorize]
public class ExamNoteAppService : MasterDataAppService, IExamNoteAppService
{
    private readonly IExamNoteRepository _examNoteRepository;

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="examNoteRepository"></param>
    public ExamNoteAppService(IExamNoteRepository examNoteRepository)
    {
        _examNoteRepository = examNoteRepository;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> CreateAsync(ExamNoteCreation input)
    {
        if (await _examNoteRepository.AnyAsync(a => a.NoteCode == input.NoteCode))
        {
            throw new EcisBusinessException(message: "编码已存在");
        }

        var model = ObjectMapper.Map<ExamNoteCreation, ExamNote>(input);
        var result = await _examNoteRepository.InsertAsync(model);
        return result.Id;
    }

    #endregion Create

    #region Update

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task UpdateAsync(ExamNoteUpdate input)
    {
        var examNote = await _examNoteRepository.GetAsync(input.Id);
        if (examNote == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        var model = ObjectMapper.Map<ExamNoteUpdate, ExamNote>(input);

        await _examNoteRepository.UpdateAsync(model);
    }

    #endregion Update

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ExamNoteData> GetAsync(int id)
    {
        var examNote = await _examNoteRepository.GetAsync(id);

        return ObjectMapper.Map<ExamNote, ExamNoteData>(examNote);
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
        if (!await _examNoteRepository.AnyAsync(a => a.Id == id))
        {
            throw new EcisBusinessException(message: "编码已存在");
        }
        await _examNoteRepository.DeleteAsync(id);
    }

    #endregion Delete

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<ExamNoteData>> GetListAsync(
        string filter = null,
        string sorting = null)
    {
        var result = await _examNoteRepository.GetListAsync(filter, sorting);

        return new ListResultDto<ExamNoteData>(
            ObjectMapper.Map<List<ExamNote>, List<ExamNoteData>>(result));
    }

    #endregion GetList

    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<ExamNoteData>> GetPagedListAsync(GetExamNotePagedInput input)
    {
        var examNotes = await _examNoteRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Sorting);

        var items = ObjectMapper.Map<List<ExamNote>, List<ExamNoteData>>(examNotes);

        var totalCount = await _examNoteRepository.GetCountAsync(input.Filter);

        var result = new PagedResultDto<ExamNoteData>(totalCount, items.AsReadOnly());

        return result;
    }

    #endregion GetPagedList
}