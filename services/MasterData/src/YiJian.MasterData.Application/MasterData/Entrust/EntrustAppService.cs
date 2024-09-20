using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.MasterData.DictionariesType;

namespace YiJian.MasterData;

/// <summary>
/// 嘱托配置API
/// </summary>
[Authorize]
public class EntrustAppService : MasterDataAppService, IEntrustAppService
{
    private readonly IEntrustRepository _entrustRepository;
    private readonly IMedicineFrequencyRepository _frequencyRepository;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="entrustRepository"></param>
    /// <param name="frequencyRepository"></param>
    public EntrustAppService(IEntrustRepository entrustRepository
        , IMedicineFrequencyRepository frequencyRepository)
    {
        _entrustRepository = entrustRepository;
        _frequencyRepository = frequencyRepository;
    }

    /// <summary>
    /// 保存嘱托配置
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<Guid> SaveEntrustAsync(EntrustCreateOrUpdateDto dto)
    {
        using var uow = UnitOfWorkManager.Begin();
        MedicineFrequency frequency = await _frequencyRepository.FirstOrDefaultAsync(x => x.FrequencyCode == "ST");
        if (dto.Id == Guid.Empty)
        {
            Random random = new Random();
            int code = random.Next(100000000, 999999999);
            int sort = dto.Sort;
            if (dto.Sort == 0)
            {
                Entrust maxSort = await (await _entrustRepository.GetQueryableAsync()).OrderByDescending(o => o.Sort).FirstOrDefaultAsync();
                sort = maxSort == null ? 1 : maxSort.Sort + 1;
            }
            else
            {
                if (await _entrustRepository.AnyAsync(a => a.Sort == dto.Sort))
                {
                    List<Entrust> entrustList = await (await _entrustRepository.GetQueryableAsync())
                        .Where(x => x.Sort >= dto.Sort)
                        .ToListAsync();
                    if (entrustList.Any())
                    {
                        entrustList.ForEach(x => { x.ModifySort(x.Sort + 1); });
                        await _entrustRepository.UpdateManyAsync(entrustList);
                    }
                }
            }

            Guid id = GuidGenerator.Create();
            Entrust model = new Entrust(id, code.ToString(), dto.Name, dto.PrescribeTypeCode,
                dto.PrescribeTypeName, "ST", frequency != null ? frequency.FrequencyName : "", dto.RecieveQty, dto.RecieveUnit, sort);
            Entrust result = await _entrustRepository.InsertAsync(model);
            await uow.CompleteAsync();
            return result.Id;
        }

        Entrust entrust = await (await _entrustRepository.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (entrust == null)
        {
            throw new EcisBusinessException(message: "数据不存在，无法修改");
        }

        //当修改的序号小于原来的需要，区间内的序号都要加1
        if (dto.Sort < entrust.Sort)
        {
            List<Entrust> entrustList = await (await _entrustRepository.GetQueryableAsync())
                .Where(x => x.Sort >= dto.Sort && x.Sort <= entrust.Sort && x.Id != dto.Id).ToListAsync();
            if (entrustList.Any())
            {
                entrustList.ForEach(x => { x.ModifySort(x.Sort + 1); });
                await _entrustRepository.UpdateManyAsync(entrustList);
            }
        }

        //当修改的需要大于原来的需要，区间内的序号都要减1
        if (dto.Sort > entrust.Sort)
        {
            List<Entrust> entrustList = await (await _entrustRepository.GetQueryableAsync())
                .Where(x => x.Sort <= dto.Sort && x.Sort >= entrust.Sort && x.Id != dto.Id).ToListAsync();
            if (entrustList.Any())
            {
                entrustList.ForEach(x => { x.ModifySort(x.Sort - 1); });
                await _entrustRepository.UpdateManyAsync(entrustList);
            }
        }

        entrust.Modify(dto.Name, dto.PrescribeTypeCode, dto.PrescribeTypeName, dto.RecieveQty, dto.RecieveUnit, dto.Sort);
        await _entrustRepository.UpdateAsync(entrust);
        await uow.CompleteAsync();
        return entrust.Id;
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="EcisBusinessException"></exception>
    public async Task<string> DeleteEntrustAsync(Guid id)
    {
        if (!await (await _entrustRepository.GetQueryableAsync()).AsNoTracking().AnyAsync(x => x.Id == id))
        {
            throw new EcisBusinessException(message: "数据不存在，无法修改");
        }

        await _entrustRepository.DeleteAsync(id);
        return "成功";
    }

    /// <summary>
    /// 嘱托列表
    /// </summary>
    /// <param name="filter">编码，名称或拼音码</param>
    /// <returns></returns>
    public async Task<List<EntrustDto>> GetEntrustListAsync(string filter)
    {
        List<Entrust> entrustList = await (await _entrustRepository.GetQueryableAsync())
            .WhereIf(!string.IsNullOrEmpty(filter),
                x => x.Code.Contains(filter) || x.Name.Contains(filter) || x.PyCode.Contains(filter))
            .OrderBy(o => o.Sort)
            .ToListAsync();
        return ObjectMapper.Map<List<Entrust>, List<EntrustDto>>(entrustList);
    }

    /// <summary>
    /// 嘱托分页列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<EntrustDto>> GetEntrustPageAsync(GetEntrustPagedInput input)
    {
        var entrustList = await (await _entrustRepository.GetQueryableAsync())
            .WhereIf(!string.IsNullOrEmpty(input.Filter),
                x => x.Code.Contains(input.Filter) || x.Name.Contains(input.Filter) ||
                     x.PyCode.Contains(input.Filter))
            .OrderBy(o => o.Sort)
            .ToListAsync();

        var entrustMap = ObjectMapper.Map<List<Entrust>, List<EntrustDto>>(entrustList
            .Skip(input.Size * (input.Index - 1))
            .Take(input.Size).ToList());
        var pageResult = new PagedResultDto<EntrustDto>(entrustList.Count,
            entrustMap.AsReadOnly());
        return pageResult;
    }
}