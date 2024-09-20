using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using YiJian.MasterData.Permissions;

namespace YiJian.MasterData.Sequences;

/// <summary>
/// 序列 API
/// </summary>
[Authorize]
public class SequenceAppService : MasterDataAppService, ISequenceAppService
{
    private readonly SequenceManager _sequenceManager;
    private readonly ISequenceRepository _sequenceRepository;

    #region constructor
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sequenceRepository"></param>
    /// <param name="sequenceManager"></param>
    public SequenceAppService(ISequenceRepository sequenceRepository, SequenceManager sequenceManager)
    {
        _sequenceRepository = sequenceRepository;
        _sequenceManager = sequenceManager;
    }
    #endregion constructor

    #region Create
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // [Authorize(MasterDataPermissions.Sequences.Create)]
    public async Task<int> CreateAsync(SequenceCreation input)
    {
        var sequence = await _sequenceManager.CreateAsync(code: input.Code,   // 编码
            name: input.Name,               // 名称
            value: input.Value,             // 序列值
            format: input.Format,           // 格式
            length: input.Length,           // 序列值长度
            date: input.Date,               // 日期
            memo: input.Memo                // 备注
            );

        return sequence.Id;
    }
    #endregion Create

    #region Update
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(MasterDataPermissions.Sequences.Update)]
    public async Task UpdateAsync(int id, SequenceUpdate input)
    {
        var sequence = await _sequenceRepository.GetAsync(id);

        sequence.Modify(name: input.Name,   // 名称
            value: input.Value,             // 序列值
            format: input.Format,           // 格式
            length: input.Length,           // 序列值长度
            date: input.Date,               // 日期
            memo: input.Memo                // 备注
            );

        await _sequenceRepository.UpdateAsync(sequence);
    }
    #endregion Update

    #region Get
    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(MasterDataPermissions.Sequences.Default)]
    public async Task<SequenceData> GetAsync(int id)
    {
        var sequence = await _sequenceRepository.GetAsync(id);

        return ObjectMapper.Map<Sequence, SequenceData>(sequence);
    }
    #endregion Get

    #region Delete
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(MasterDataPermissions.Sequences.Delete)]
    public async Task DeleteAsync(int id)
    {
        await _sequenceRepository.DeleteAsync(id);
    }
    #endregion Delete

    #region GetList
    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    [Authorize(MasterDataPermissions.Sequences.Default)]
    public async Task<ListResultDto<SequenceData>> GetListAsync(
        string filter = null,
        string sorting = null)
    {
        var result = await _sequenceRepository.GetListAsync(filter, sorting);

        return new ListResultDto<SequenceData>(
            ObjectMapper.Map<List<Sequence>, List<SequenceData>>(result));
    }
    #endregion GetList

    #region GetPagedList
    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(MasterDataPermissions.Sequences.Default)]
    public async Task<PagedResultDto<SequenceData>> GetPagedListAsync(GetSequencePagedInput input)
    {
        var sequences = await _sequenceRepository.GetPagedListAsync(
                input.SkipCount,
                input.Size,
                input.Filter,
                input.Sorting);

        var items = ObjectMapper.Map<List<Sequence>, List<SequenceData>>(sequences);

        var totalCount = await _sequenceRepository.GetCountAsync(input.Filter);

        var result = new PagedResultDto<SequenceData>(totalCount, items.AsReadOnly());

        return result;
    }
    #endregion GetPagedList

    #region GetSequence
    /// <summary>
    /// 获取流水号
    /// </summary>
    [HttpPost("/api/MasterData/sequence/{code}")]
    public async Task<string> GetSequenceAsync(string code)
    {
        //获取流水号
        return await _sequenceManager.GetSequenceAsync(code);
    }
    #endregion GetSequence

    #region GetSequences
    /// <summary>
    /// 获取流水号
    /// </summary>
    [HttpPost("/api/MasterData/sequence/{code}/{count}")]
    public async Task<string[]> GetSequencesAsync(string code, int count)
    {
        //获取流水号
        return await _sequenceManager.GetSequencesAsync(code, count);
    }
    #endregion GetSequences
}
