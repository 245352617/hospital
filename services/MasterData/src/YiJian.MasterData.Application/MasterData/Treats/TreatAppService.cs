using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.MasterData.AllItems;
using YiJian.MasterData.MasterData;
using YiJian.MasterData.Treats;

namespace YiJian.MasterData;

/// <summary>
/// 诊疗项目字典 API
/// </summary>
[Authorize]
public class TreatAppService : MasterDataAppService, ITreatAppService
{
    private readonly ITreatRepository _treatRepository;
    private readonly IAllItemByThreePartyAppService _allItemAppService;
    private readonly ITreatGroupRepository _treatGroupRepository;

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="treatRepository"></param>
    /// <param name="allItemAppService"></param>
    /// <param name="treatGroupRepository"></param>
    public TreatAppService(ITreatRepository treatRepository, IAllItemByThreePartyAppService allItemAppService,
        ITreatGroupRepository treatGroupRepository)
    {
        _treatRepository = treatRepository;
        _allItemAppService = allItemAppService;
        _treatGroupRepository = treatGroupRepository;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> CreateAsync(TreatCreation input)
    {
        var model = ObjectMapper.Map<TreatCreation, Treat>(input);
        model.PyCode = input.TreatName.FirstLetterPY();
        var result = await _treatRepository.InsertAsync(model);
        if (result.PlatformType == PlatformType.PreHospital)
        {
            await _allItemAppService.SaveAsync(new AllItemCreation
            {
                Code = input.TreatName,
                Name = input.TreatName,
                CategoryCode = "Treats",
                CategoryName = "诊疗",
                Price = input.Price,
                Unit = input.Unit,
                TypeCode = "PreHospital",
                TypeName = "院前急救 "
            });
        }

        return result.Id;
    }

    #endregion Create

    #region Update

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> UpdateAsync(TreatUpdate input)
    {
        var treat = await (await _treatRepository.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == input.Id);
        if (treat == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        //拿取旧是否急救，旧的是true，新的是否急救为false，就删除该记录
        var oldAid = treat.PlatformType;
        treat.Modify(name: input.Name, // 名称
            price: input.Price, // 单价
            otherPrice: input.OtherPrice, // 其它价格
            categoryCode: input.CategoryCode, // 诊疗处置类别代码
            category: input.CategoryName, // 诊疗处置类别
            specification: input.Specification, // 规格
            unit: input.Unit, // 单位
            frequencyCode: input.FrequencyCode, // 默认频次代码
            execDeptCode: input.ExecDeptCode, // 执行科室代码
            execDept: input.ExecDeptName, // 执行科室
            feeTypeMain: input.FeeTypeMainCode, // 收费大类代码
            feeTypeSub: input.FeeTypeSubCode, // 收费小类代码
            platformType: input.PlatformType
        );
        var result = await _treatRepository.UpdateAsync(treat);
        if (input.PlatformType == PlatformType.PreHospital)
        {
            await _allItemAppService.SaveAsync(new AllItemCreation
            {
                Code = result.TreatCode,
                Name = result.TreatName,
                CategoryCode = "Treats",
                CategoryName = "诊疗",
                Price = input.Price,
                Unit = input.Unit,
                TypeCode = "PreHospital",
                TypeName = "院前急救 "
            });
        }
        else if (oldAid == PlatformType.PreHospital && input.PlatformType != PlatformType.PreHospital)
        {
            await _allItemAppService.DeleteAsync(result.TreatCode, "Treats", "PreHospital");
        }

        return result.Id;
    }

    #endregion Update

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TreatData> GetAsync(int id)
    {
        var treat = await _treatRepository.GetAsync(id);

        return ObjectMapper.Map<Treat, TreatData>(treat);
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
        var treat = await _treatRepository.GetAsync(id);
        if (treat == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        await _treatRepository.DeleteAsync(id);
        if (treat.PlatformType == PlatformType.PreHospital)
        {
            await _allItemAppService.DeleteAsync(treat.TreatCode, "Treats", "PreHospital");
        }
    }

    #endregion Delete

    #region Details

    /// <summary>
    /// 根据编码获取
    /// </summary>
    /// <param name="code"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="EcisBusinessException"></exception>
    public async Task<TreatData> GetDetailsAsync(string code,PlatformType type = PlatformType.PreHospital)
    {
        var treat = await (await _treatRepository.GetQueryableAsync()).FirstOrDefaultAsync(f => f.TreatCode == code);
        if (treat == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        var result = ObjectMapper.Map<Treat, TreatData>(treat);
        
        if (type == PlatformType.PreHospital)
        {
            //院前CategoryCode和字典PreHospitalCategory保持一致
            result.CategoryCode = "Treats";
            result.CategoryName = "诊疗";
        }
        
        return result;
    }

    #endregion

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<TreatData>> GetListAsync(
        string filter = null,
        string sorting = null, PlatformType platformType = PlatformType.All, string categoryCode = null)
    {
        string catalog = "";
        if (!string.IsNullOrEmpty(categoryCode))
        {
            var group = await (await _treatGroupRepository.GetQueryableAsync())
                .Where(t => t.DictionaryCode == categoryCode).ToListAsync();
            catalog = string.Join(",", group.Select(s => s.CatalogCode));
        }

        var result = await _treatRepository.GetListAsync(filter, sorting, platformType, catalog);
        var res = ObjectMapper.Map<List<Treat>, List<TreatData>>(result);
        if (platformType == PlatformType.PreHospital)
        {
            //院前CategoryCode和字典PreHospitalCategory保持一致
            foreach (var item in res)
            {
                item.CategoryCode = "Treats";
                item.CategoryName = "诊疗";
            }
        }
            
        return new ListResultDto<TreatData>(res);
    }

    #endregion GetList

    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<TreatData>> GetPagedListAsync(GetTreatPagedInput input)
    {
        List<string> categories = new List<string>();

        if (input.PlatformType == PlatformType.EmergencyTreatment)
        {
            //根据处置分类获取处置下级列表
            if (!string.IsNullOrEmpty(input.CategoryCode))
            {
                categories = await (await _treatGroupRepository.GetQueryableAsync())
                    .Where(t => t.DictionaryCode == input.CategoryCode)
                    .Select(s => s.CatalogCode).ToListAsync();
                //input.CategoryCode = string.Join(",", group.Select(s => s.CatalogCode));
            }
            categories.Add("nothing"); //防止没有数据查询所有记录，所以添加一个没有的参数 
        }
        var treats = await _treatRepository.GetPagedListAsync(
            categories,
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Sorting,input.PlatformType);

        var items = ObjectMapper.Map<List<Treat>, List<TreatData>>(treats);

        var totalCount = await _treatRepository.GetCountAsync(categories,input.Filter, input.PlatformType);

        var result = new PagedResultDto<TreatData>(totalCount, items.AsReadOnly());

        return result;
    }

    #endregion GetPagedList
    
    #region 修改默认频次

    /// <summary>
    /// 修改默认频次
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> UpdateFrequencyAsync(UpdateFrequencyDto input)
    {
        var treat = await (await _treatRepository.GetQueryableAsync()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == input.Id);
        if (treat == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        //拿取旧是否急救，旧的是true，新的是否急救为false，就删除该记录
        //var oldAid = treat.PlatformType;
        treat.FrequencyCode = input.FrequencyCode;
        //treat.FrequencyName = input.FrequencyName;
        
        var result = await _treatRepository.UpdateAsync(treat);

        return result.Id;
    }

    #endregion
    
}