using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.MasterData.Treats;

namespace YiJian.MasterData.Exams;

/// <summary>
/// 检查明细项 API
/// </summary>
[Authorize]
public class ExamTargetAppService : MasterDataAppService, IExamTargetAppService
{
    private readonly IExamTargetRepository _examTargetRepository;
    private readonly ITreatRepository _treatRepository;
    private bool _isPUK { get; set; }

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="examTargetRepository"></param>
    /// <param name="treatRepository"></param>
    /// <param name="configuration"></param>
    public ExamTargetAppService(IExamTargetRepository examTargetRepository,
        ITreatRepository treatRepository,
          IConfiguration configuration)
    {
        _examTargetRepository = examTargetRepository;
        _treatRepository = treatRepository;
        var hospitalCode = configuration["HospitalInfoConfig:HospitalCode"];
        if (!string.IsNullOrEmpty(hospitalCode) && hospitalCode == "LDC")
            this._isPUK = false;
        else
            this._isPUK = true;
    }

    #endregion constructor

    #region Create

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<int> CreateAsync(ExamTargetCreation input)
    {
        if (await _examTargetRepository.AnyAsync(a => a.TargetCode == input.TargetCode))
        {
            throw new EcisBusinessException(message: "编码已存在");
        }

        var model = ObjectMapper.Map<ExamTargetCreation, ExamTarget>(input);
        var result = await _examTargetRepository.InsertAsync(model);
        return result.Id;
    }

    #endregion Create

    #region Update

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task UpdateAsync(ExamTargetUpdate input)
    {
        var examTarget = await _examTargetRepository.GetAsync(input.Id);
        if (examTarget == null)
        {
            throw new EcisBusinessException(message: "数据不存在");
        }

        examTarget.Modify(name: input.TargetName, // 名称
            projectCode: input.ProjectCode, // 项目编码
            unit: input.TargetUnit, // 单位
            qty: input.Qty, // 数量
            price: input.Price, // 价格
            otherPrice: input.OtherPrice, // 其它价格
            specification: input.Specification, // 规格
            sort: input.Sort, // 排序号
            insureType: input.InsuranceType, // 医保类型
            specialFlag: input.SpecialFlag, // 特殊标识
            isActive: input.IsActive // 是否启用
        );
        await _examTargetRepository.UpdateAsync(examTarget);
    }

    #endregion Update

    #region Get

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ExamTargetData> GetAsync(int id)
    {
        var examTarget = await _examTargetRepository.GetAsync(id);

        return ObjectMapper.Map<ExamTarget, ExamTargetData>(examTarget);
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
        if (!await _examTargetRepository.AnyAsync(a => a.Id == id))
        {
            throw new EcisBusinessException(message: "数据不存在");
        }
        await _examTargetRepository.DeleteAsync(id);
    }

    #endregion Delete

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="proCode">ProjectCode 项目编码</param>
    /// <param name="firstCode">FirstNodeCode 一级目录编码</param>
    /// <param name="filter"></param>
    /// <param name="sorting"></param>
    /// <returns></returns>
    public async Task<ListResultDto<ExamTargetData>> GetListAsync(string proCode,
        string firstCode = null,
        string filter = null,
        string sorting = null)
    {
        var examTargetList = await _examTargetRepository.GetListAsync(proCode, filter, sorting);
        if (!firstCode.IsNullOrEmpty() && examTargetList.Any())
            examTargetList = examTargetList.Where(c => c.FirstNodeCode == firstCode).ToList();

        var codes = examTargetList.GroupBy(c => c.TargetCode).Select(c => c.Key).ToList();
        var treatList = await _treatRepository.GetListAsync(c => codes.Contains(c.TreatCode));
        var list = new List<ExamTargetData>();
        foreach (var item in examTargetList)
        {
            if (!item.IsActive)
                continue;
            Treat treat = treatList.Where(c => c.TreatCode == item.TargetCode).FirstOrDefault();
            if (treat == null)
            {
                continue;
            }
            var model = new ExamTargetData()
            {
                MeducalInsuranceCode = treat != null ? treat.MeducalInsuranceCode : string.Empty,
                YBInneCode = treat != null ? treat.YBInneCode : string.Empty,
                Id = item.Id,
                InsuranceType = item.InsuranceType,
                IsActive = item.IsActive,
                Qty = item.Qty,
                PyCode = item.PyCode,
                OtherPrice = item.OtherPrice,
                Price = item.Price,
                ProjectCode = item.ProjectCode,
                ProjectMerge = item.ProjectMerge,
                ProjectType = item.ProjectType,
                Sort = item.Sort,
                SpecialFlag = item.SpecialFlag,
                Specification = item.Specification,
                TargetCode = item.TargetCode,
                TargetName = item.TargetName,
                TargetUnit = item.TargetUnit,
                WbCode = item.WbCode,
            };
            if (_isPUK)
                model.OtherPrice = treat != null && treat.Additional == true ? (decimal)treat.OtherPrice : model.OtherPrice;
            list.Add(model);
            //只取第一条数据
            //return new ListResultDto<ExamTargetData>(list);
        }
        return new ListResultDto<ExamTargetData>(list);
    }

    #endregion GetList

    #region GetPagedList

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<ExamTargetData>> GetPagedListAsync(GetExamTargetPagedInput input)
    {
        var examTargets = await _examTargetRepository.GetPagedListAsync(
            input.SkipCount,
            input.Size,
            input.Filter,
            input.Sorting);

        var items = ObjectMapper.Map<List<ExamTarget>, List<ExamTargetData>>(examTargets);

        var totalCount = await _examTargetRepository.GetCountAsync(input.Filter);

        var result = new PagedResultDto<ExamTargetData>(totalCount, items.AsReadOnly());

        return result;
    }

    #endregion GetPagedList
}