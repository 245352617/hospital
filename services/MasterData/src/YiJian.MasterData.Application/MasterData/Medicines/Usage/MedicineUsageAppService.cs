using Microsoft.AspNetCore.Authorization;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.MasterData.Permissions;
using YiJian.MasterData.Treats;

namespace YiJian.MasterData.Medicines;

/// <summary>
/// 药品用法字典 API
/// </summary>
//[Authorize]
public class MedicineUsageAppService : MasterDataAppService, IMedicineUsageAppService
{
    private readonly IMedicineUsageRepository _medicineUsageRepository;
    private readonly ITreatRepository _treatRepository;
    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="medicineUsageRepository"></param>
    /// <param name="treatRepository"></param>
    public MedicineUsageAppService(
        IMedicineUsageRepository medicineUsageRepository,
        ITreatRepository treatRepository)
    {
        _medicineUsageRepository = medicineUsageRepository;
        _treatRepository = treatRepository;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>=
    public async Task<int> CreateAsync(MedicineUsageCreation input)
    {
        //编码为空则生成随机数
        if (string.IsNullOrEmpty(input.UsageCode))
        {
            Random random = new Random(20);
            input.UsageCode = random.Next(100000000, 999999999).ToString();
        }

        if (await _medicineUsageRepository.AnyAsync(a => a.UsageCode == input.UsageCode))
        {
            throw new EcisBusinessException(message: "编码已存在");
        }

        var model = ObjectMapper.Map<MedicineUsageCreation, MedicineUsage>(input);
        model.PyCode = input.UsageName.FirstLetterPY();
        model.WbCode = input.UsageName.FirstLetterWB();
        var result = await _medicineUsageRepository.InsertAsync(model);
        return result.Id;
    }

    #endregion Create

    #region Update

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task UpdateAsync(MedicineUsageUpdate input)
    {
        var medicineUsage = await _medicineUsageRepository.GetAsync(input.Id);
        if (medicineUsage == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        medicineUsage.Modify(name: input.UsageName, // 名称
            catalog: input.Catalog, // 分类  1：输液  2：注射  3：治疗  4：服药  10其他
            indexNo: input.Sort, // 排序号
            treatCode: input.TreatCode, // 诊疗项目 描述：一个或多个项目，多个以,隔开
            isActive: input.IsActive, // 是否启用
            fullName: input.FullName,
            remark: input.Remark,
            isSingle: input.IsSingle,
            isEnterCombination: input.IsEnterCombination
        );
        await _medicineUsageRepository.UpdateAsync(medicineUsage);
    }

    #endregion Update

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(MasterDataPermissions.MedicineUsages.Default)]
    public async Task<MedicineUsageData> GetAsync(int id)
    {
        var medicineUsage = await _medicineUsageRepository.GetAsync(id);

        return ObjectMapper.Map<MedicineUsage, MedicineUsageData>(medicineUsage);
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
        var medicineUsage = await _medicineUsageRepository.GetAsync(id);
        if (medicineUsage == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        await _medicineUsageRepository.DeleteAsync(id);
    }

    #endregion Delete

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<MedicineUsageData>> GetListAsync(
        string filter = null,
        string sorting = null, int code = -1)
    {
        var addCard = "";
        switch (code)
        {
            //TODO 注射单等龙岗固定编码
            case 0: //注射单
                addCard = "10";
                break;
            case 1: //输液单
                addCard = "9";
                break;
            case 2: //雾化
                addCard = "8";
                break;
        }

        var result = await _medicineUsageRepository.GetListAsync(filter, sorting, addCard);
        var data = result.OrderBy(o => o.PyCode.Length).ToList();

        return new ListResultDto<MedicineUsageData>(
            ObjectMapper.Map<List<MedicineUsage>, List<MedicineUsageData>>(data));
    }

    #endregion GetList

    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<MedicineUsageData>> GetPagedListAsync(GetMedicineUsagePagedInput input)
    {
        var medicineUsages = await _medicineUsageRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Sorting);

        var items = ObjectMapper.Map<List<MedicineUsage>, List<MedicineUsageData>>(medicineUsages);

        List<MedicineUsageData> list = new();

        var listA = items.WhereIf(!input.Filter.IsNullOrWhiteSpace(), w => w.UsageCode.StartsWith(input.Filter)).ToList();
        var listB = items.WhereIf(!input.Filter.IsNullOrWhiteSpace(), w => w.UsageName.StartsWith(input.Filter)).ToList();
        var listC = items.WhereIf(!input.Filter.IsNullOrWhiteSpace(), w => w.PyCode.StartsWith(input.Filter.ToUpper())).ToList();

        var query = listA.Union(listA).Union(listB).Union(listC).OrderBy(o => o.PyCodeLen).ToList();

        var ids = query.Select(s => s.Id).ToList();
        list.AddRange(query);

        var otherData = items.Where(w => !ids.Contains(w.Id)).OrderBy(o => o.PyCodeLen).ToList();
        list.AddRange(otherData);

        //获取treacode 去除逗号分割
        var codeList = list.GroupBy(o => o.TreatCode).ToList();
        var treatcList = new List<string>();
        foreach (var item in codeList)
        {
            if (!string.IsNullOrEmpty(item.Key))
            {
                foreach (var code in item.Key.Split(","))
                {
                    treatcList.Add(code);
                }
            }
        }

        treatcList = treatcList.Distinct().ToList();
        // 根据treatCode 查询
        if (treatcList.Count > 0)
        {
            var treatList = await _treatRepository.GetListAsync(c => treatcList.Contains(c.TreatCode));
            Dictionary<string, string> treatNameDic = new Dictionary<string, string>();
            foreach (var item in treatList)
                treatNameDic[item.TreatCode] = item.TreatName;

            foreach (var item in list)
                if (!string.IsNullOrWhiteSpace(item.TreatCode))
                {
                    // 这里可能出现code 有但是查不到 Name的情况。 不管了
                    item.TreatName = item.TreatCode.Split(",").Select(c => treatNameDic.ContainsKey(c) ? treatNameDic[c] : string.Empty).Join(",");
                }
        }

        var totalCount = await _medicineUsageRepository.GetCountAsync(input.Filter);
        var result = new PagedResultDto<MedicineUsageData>(totalCount, list.AsReadOnly());

        return result;
    }

    #endregion GetPagedList
}