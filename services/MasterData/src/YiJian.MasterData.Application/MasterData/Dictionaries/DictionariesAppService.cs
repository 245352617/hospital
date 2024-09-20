using DotNetCore.CAP;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.MasterData.DictionariesTypes;
using YiJian.MasterData.MasterData.Dictionaries;

namespace YiJian.MasterData;

/// <summary>
/// 字典API
/// </summary>
[Authorize]
public class DictionariesAppService : MasterDataAppService, IDictionariesAppService, ICapSubscribe
{

    private readonly IDictionariesRepository _repository;
    private readonly DictionariesManager _dictionariesManager;
    private readonly IDictionariesTypeAppService _iDictionariesTypeAppService;
    private readonly IDictionariesTypeRepository _dictionariesTypeRepository;

    /// <summary>
    /// 字典API
    /// </summary> 
    public DictionariesAppService(
        IDictionariesRepository repository,
        DictionariesManager dictionariesManager,
        IDictionariesTypeAppService iDictionariesTypeAppService,
        IDictionariesTypeRepository dictionariesTypeRepository)
    {
        _repository = repository;
        _dictionariesManager = dictionariesManager;
        _iDictionariesTypeAppService = iDictionariesTypeAppService;
        _dictionariesTypeRepository = dictionariesTypeRepository;
    }

    /// <summary>
    /// 获取字典列表
    /// </summary>
    /// <param name="input"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns> 
    public async Task<ListResultDto<DictionariesDto>> GetDictionariesListAsync(
        DictionariesWhereInput input, CancellationToken cancellationToken)
    {
        var list = await (await _repository.GetQueryableAsync())
            .WhereIf(!string.IsNullOrEmpty(input.DictionariesTypeCode), w => input.DictionariesTypeCode == w.DictionariesTypeCode)
            .WhereIf(!string.IsNullOrEmpty(input.DictionariesName), w => w.DictionariesName.Contains(input.DictionariesName.Trim()))
            .OrderBy(x => x.Sort)
            .ToListAsync(cancellationToken);
        return new ListResultDto<DictionariesDto>(
            ObjectMapper.Map<List<Dictionaries>, List<DictionariesDto>>(list));
    }

    /// <summary>
    /// 同步字典信息
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [CapSubscribe("masterdata.dictionaries.addorupdate.from.triage.config")]
    [NonAction]
    public async Task SyncSaveDictionariesAsync(CreateOrUpdateDictionariesDto dto)
    {
        using var uow = this.UnitOfWorkManager.Begin();
        //同步的数据属于预检分诊
        await SaveDictionariesAsync(dto, default, 1);
        await uow.CompleteAsync();
    }

    /// <summary>
    /// 新增或者修改实体
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="dataFrom">数据来源：0:急诊，1:预检分诊,2：院前急救</param>
    /// <returns></returns>
    public async Task<DictionariesDto> SaveDictionariesAsync(CreateOrUpdateDictionariesDto dto,
        CancellationToken cancellationToken = default, int dataFrom = 0)
    {
        try
        {
            Dictionaries result;
            var dict = await _repository.FirstOrDefaultAsync(s =>
                    s.DictionariesCode == dto.DictionariesCode &&
                    s.DictionariesTypeCode == dto.DictionariesTypeCode,
                cancellationToken: cancellationToken);
            if (dict == null)
            {
                if (await _repository.AnyAsync(
                        a => a.DictionariesTypeCode == dto.DictionariesTypeCode &&
                             a.DictionariesCode == dto.DictionariesCode, cancellationToken: cancellationToken))
                {
                    throw new EcisBusinessException(message: "编码已存在");
                }
                var model = ObjectMapper.Map<CreateOrUpdateDictionariesDto, Dictionaries>(dto);
                model.Py = model.DictionariesName.FirstLetterPY();
                if (!await _dictionariesTypeRepository.AnyAsync(a => a.DictionariesTypeCode == dto.DictionariesTypeCode, cancellationToken: cancellationToken))
                {
                    await _dictionariesTypeRepository.InsertAsync(new DictionariesTypes.DictionariesType(GuidGenerator.Create(), dto.DictionariesTypeCode, dto.DictionariesTypeName, "", dataFrom), cancellationToken: cancellationToken);
                }
                if (model.Sort == 0)
                {
                    var dictionary = await (await _repository.GetQueryableAsync())
                        .Where(x => x.DictionariesTypeCode == dto.DictionariesTypeCode)
                        .OrderByDescending(o => o.Sort).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                    if (dictionary != null)
                    {
                        model.Sort = dictionary.Sort + 1;
                    }
                    else
                    {
                        model.Sort = 1;
                    }
                }

                result = await _dictionariesManager.InsertAsync(model, cancellationToken);
                return ObjectMapper.Map<Dictionaries, DictionariesDto>(result);
            }

            dict.DictionariesName = dto.DictionariesName;
            dict.Status = dto.Status;
            dict.Sort = dto.Sort;
            dict.Py = dto.DictionariesName.FirstLetterPY();
            dict.IsDefaltChecked = dto.IsDefaltChecked;
            dict.Remark = dto.Remark;
            result = await _dictionariesManager.UpdateAsync(dict, cancellationToken);
            return ObjectMapper.Map<Dictionaries, DictionariesDto>(result);
        }
        catch (Exception e)
        {
            throw new EcisBusinessException(message: e.Message);
        }
    }

    /// <summary>
    /// 同步删除
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [CapSubscribe("masterdata.dictionaries.delete.from.triage.config", Group = "ecis.masterdata")]
    [NonAction]
    public async Task SyncDeleteAsync(string code)
    {
        using var uow = this.UnitOfWorkManager.Begin();
        var dto = await _repository.FirstOrDefaultAsync(x => x.DictionariesCode == code);
        if (dto != null)
            await DeleteDictionariesAsync(new List<Guid> { dto.Id });
        await uow.CompleteAsync();
    }

    /// <summary>
    /// 删除字典
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task DeleteDictionariesAsync(List<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _repository.DeleteManyAsync(ids, cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    /// <summary>
    /// post 方法删除字典
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public Task DelDictionariesAsync(List<Guid> ids,
        CancellationToken cancellationToken = default)
        => DeleteDictionariesAsync(ids, cancellationToken);

    /// <summary>
    /// 分组获取字典
    /// </summary>
    /// <param name="dictionariesTypeCode"></param>
    /// <returns></returns>
    public async Task<Dictionary<string, List<DictionariesDto>>> GetDictionariesGroupAsync(
        string dictionariesTypeCode)
    {
        //先判断类型是否已禁用
        //var code = dictionariesTypeCode;
        //dictionariesTypeCode = String.Join(",",
        //    await (await _dictionariesTypeRepository.GetQueryableAsync()).Where(x => x.Status && code.Contains(x.DictionariesTypeCode))
        //        .Select(s => s.DictionariesTypeCode).ToListAsync());
        // var list = await _repository.GetListAsync(dictionariesTypeCode);
        //list = list.Where(x => x.Status).ToList();
        //var listMap = ObjectMapper.Map<List<Dictionaries>, List<DictionariesDto>>(list);
        //var group = listMap.GroupBy(g => g.DictionariesTypeCode).ToDictionary(k => k.Key, v => v.ToList());
        //return group;

        var codeList = dictionariesTypeCode.Split(',');
        if (codeList.Any())
        {
            var list = (await _repository.GetListAsync(c => codeList.Contains(c.DictionariesTypeCode) && c.Status)).OrderBy(s => s.Sort).ToList();
            var listMap = ObjectMapper.Map<List<Dictionaries>, List<DictionariesDto>>(list);
            var group = listMap.GroupBy(g => g.DictionariesTypeCode).ToDictionary(k => k.Key, v => v.ToList());
            return group;
        }
        else
            return new Dictionary<string, List<DictionariesDto>>();

    }

    /// <summary>
    /// 获取电子病历水印配置信息
    /// </summary> 
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<EmrWatermarkDto> GetEmrWatermarkAsync()
    {
        using var uow = this.UnitOfWorkManager.Begin();
        try
        {
            var dictionaries = await _repository.GetEmrWatermarkAsync();
            var watermarking = new EmrWatermarkDto
            {
                Enabled = (dictionaries.FirstOrDefault(w => w.DictionariesCode == "Enabled") ?? new Dictionaries { IsDefaltChecked = false }).IsDefaltChecked,
                Watermark = new Watermarking
                {
                    Alpha = dictionaries.FirstOrDefault(w => w.DictionariesCode == "Alpha")?.DictionariesName,
                    Angle = dictionaries.FirstOrDefault(w => w.DictionariesCode == "Angle")?.DictionariesName,
                    BackColorValue = dictionaries.FirstOrDefault(w => w.DictionariesCode == "BackColorValue")?.DictionariesName,
                    ColorValue = dictionaries.FirstOrDefault(w => w.DictionariesCode == "ColorValue")?.DictionariesName,
                    DensityForRepeat = dictionaries.FirstOrDefault(w => w.DictionariesCode == "DensityForRepeat")?.DictionariesName,
                    Font = dictionaries.FirstOrDefault(w => w.DictionariesCode == "Font")?.DictionariesName,
                    ImageDataBase64String = dictionaries.FirstOrDefault(w => w.DictionariesCode == "ImageDataBase64String")?.DictionariesName,
                    Repeat = dictionaries.FirstOrDefault(w => w.DictionariesCode == "Repeat")?.DictionariesName,
                    Text = dictionaries.FirstOrDefault(w => w.DictionariesCode == "Text")?.DictionariesName,
                    Type = dictionaries.FirstOrDefault(w => w.DictionariesCode == "Type")?.DictionariesName,
                }
            };

            await uow.CompleteAsync();
            return watermarking;
        }
        catch (Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }

}