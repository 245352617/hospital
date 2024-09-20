using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace YiJian.MasterData;

public class DictionariesManager : DomainService
{
    private readonly IDictionariesRepository _repository;

    public DictionariesManager(IDictionariesRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 新增实体
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Dictionaries> InsertAsync(Dictionaries model,
        CancellationToken cancellationToken = default)
    {
        if (await _repository.AnyAsync(t =>
                t.DictionariesCode == model.DictionariesCode &&
                t.DictionariesTypeCode == model.DictionariesTypeCode,
            cancellationToken: cancellationToken))
        {
            throw new Exception("编码已存在");
        }

        return await _repository.InsertAsync(model.SetId(GuidGenerator.Create()),
            cancellationToken: cancellationToken);
    }
    
    /// <summary>
    /// 修改实体
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Dictionaries> UpdateAsync(Dictionaries model,
        CancellationToken cancellationToken = default)
    {
        if (!await _repository.AnyAsync(a=>a.Id==model.Id,cancellationToken))
        {
            throw new Exception("数据不存在");
        }
        if (await _repository.AnyAsync(t =>
                t.DictionariesCode == model.DictionariesCode &&
                t.DictionariesTypeCode == model.DictionariesTypeCode&&t.Id!=model.Id,
            cancellationToken: cancellationToken))
        {
            throw new Exception("编码已存在");
        }
        return await _repository.UpdateAsync(model,
            cancellationToken: cancellationToken);
    }
    /// <summary>
    /// 删除实体
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task DeleteAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        if (!await _repository.AnyAsync(a=>a.Id==id,cancellationToken))
        {
            throw new Exception("数据不存在");
        }
        await _repository.DeleteAsync(id,
            cancellationToken: cancellationToken);
    }


}