using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.MasterData.Exams;
using YiJian.MasterData.Treats;

namespace YiJian.MasterData.Labs;

/// <summary>
/// 检验明细项 API
/// </summary>
[Authorize]
public class LabTargetAppService : MasterDataAppService, ILabTargetAppService
{
    private readonly ILabTargetRepository _labTargetRepository;
    private readonly ITreatRepository _treatRepository;

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="labTargetRepository"></param>
    /// <param name="treatRepository"></param>
    public LabTargetAppService(ILabTargetRepository labTargetRepository,
       ITreatRepository treatRepository)
    {
        _labTargetRepository = labTargetRepository;
        _treatRepository = treatRepository;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> CreateAsync(LabTargetCreation input)
    {
        if (await (await _labTargetRepository.GetQueryableAsync()).AnyAsync(a => a.TargetCode == input.TargetCode))
        {
            throw new EcisBusinessException(message: "编码已存在");
        }

        var model = ObjectMapper.Map<LabTargetCreation, LabTarget>(input);
        var result = await _labTargetRepository.InsertAsync(model);
        return result.Id;
    }

    #endregion Create

    #region Update

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task UpdateAsync(LabTargetUpdate input)
    {
        var labTarget = await _labTargetRepository.GetAsync(input.Id);
        if (labTarget == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }
        labTarget.Modify(name: input.TargetName, // 名称
            sort: input.Sort, // 排序号
            unit: input.TargetUnit, // 数量
            qty: input.Qty,
            price: input.Price, // 价格
            insureType: input.InsuranceType, // 医保类型
            isActive: input.IsActive // 是否启用
        );
        await _labTargetRepository.UpdateAsync(labTarget);
    }

    #endregion Update

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<LabTargetData> GetAsync(int id)
    {
        var labTarget = await _labTargetRepository.GetAsync(id);

        return ObjectMapper.Map<LabTarget, LabTargetData>(labTarget);
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
        if (!await (await _labTargetRepository.GetQueryableAsync()).AnyAsync(a => a.Id == id))
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        await _labTargetRepository.DeleteAsync(id);
    }

    #endregion Delete

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<LabTargetData>> GetListAsync(string catalogAndProjectCode, string proCode,
        string filter = null,
        string sorting = null)
    {
        var labList = await _labTargetRepository.GetListAsync(catalogAndProjectCode, proCode, filter, sorting);
        var codes = labList.GroupBy(c => c.TargetCode).Select(c => c.Key).ToList();
        var treatList = await _treatRepository.GetListAsync(c => codes.Contains(c.TreatCode));
        var list =new List<LabTargetData>();
        foreach (var item in labList)
        {
            if (!item.IsActive)
                continue;
            Treat treat = treatList.Where(c => c.TreatCode == item.TargetCode).FirstOrDefault();
            list.Add(new LabTargetData()
            {
                MeducalInsuranceCode = treat != null ? treat.MeducalInsuranceCode : string.Empty,
                YBInneCode = treat != null ? treat.YBInneCode : string.Empty,
                Id = item.Id,
                InsuranceType = item.InsuranceType,
                IsActive = item.IsActive,
                Qty = item.Qty,
                PyCode = item.PyCode,
                Price = item.Price,
                ProjectCode = item.ProjectCode,
                ProjectMerge = item.ProjectMerge,
                ProjectType = item.ProjectType,
                Sort = item.Sort,
                TargetCode = item.TargetCode,
                TargetName = item.TargetName,
                TargetUnit = item.TargetUnit,
                WbCode = item.WbCode,
                CatalogAndProjectCode = item.CatalogAndProjectCode,
            });
        }
        return new ListResultDto<LabTargetData>(list);
    }

    #endregion GetList
    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<LabTargetData>> GetPagedListAsync(GetLabTargetPagedInput input)
    {
        var labTargets = await _labTargetRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Sorting);

        var items = ObjectMapper.Map<List<LabTarget>, List<LabTargetData>>(labTargets);

        var totalCount = await _labTargetRepository.GetCountAsync(input.Filter);

        var result = new PagedResultDto<LabTargetData>(totalCount, items.AsReadOnly());

        return result;
    }

    #endregion GetPagedList
}