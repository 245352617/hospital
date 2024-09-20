using YiJian.BodyParts.Domain.Shared.Const;
using YiJian.BodyParts.Dtos;
using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.IService;
using YiJian.BodyParts.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Guids;

namespace YiJian.BodyParts.Service
{
    /// <summary>
    /// 基础数据服务
    /// </summary>
    public class PhraseAppService : ApplicationService, IPhraseAppService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IIcuParaModuleRepository _icuParaModuleRepository;
        private IIcuParaItemRepository _icuParaItemRepository;
        private IIcuPhraseRepository _icuPhraseRepository;
        private IDictRepository _dictRepository;
        private IDictCanulaPartRepository _dictCanulaPartRepository;
        private IIcuDeptScheduleRepository _icuDeptScheduleRepository;
        private IIcuSysParaRepository _icuSysParaRepository;


        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="guidGenerator"></param>
        /// <param name="httpContextAccessor">参数模板</param>
        /// <param name="icuParaModuleRepository">参数模板</param>
        /// <param name="icuDeptRepository">科室字典</param>
        /// <param name="icuStaffRepository">员工字典</param>
        /// <param name="icuParaItemRepository">参数字典</param>
        /// <param name="icuPhraseRepository">护理记录模板字典</param>
        /// <param name="dictRepository">参数下拉项字典</param>
        /// <param name="dictCanulaPartRepository">人体图设置</param>
        /// <param name="icuStaffGroupRepository">员工科室表</param>
        /// <param name="icuDeptScheduleRepository">科室班次表</param>
        /// <param name="icuSysParaRepository">系统参数</param>

        public PhraseAppService(IGuidGenerator guidGenerator,
            IHttpContextAccessor httpContextAccessor,
            IIcuParaModuleRepository icuParaModuleRepository,
            IIcuParaItemRepository icuParaItemRepository,
            IIcuPhraseRepository icuPhraseRepository,
            IDictRepository dictRepository,
            IDictCanulaPartRepository dictCanulaPartRepository,
            IIcuDeptScheduleRepository icuDeptScheduleRepository,
            IIcuSysParaRepository icuSysParaRepository
            )
        {
            _guidGenerator = guidGenerator;
            _httpContextAccessor = httpContextAccessor;
            _icuParaModuleRepository = icuParaModuleRepository;
            _icuParaItemRepository = icuParaItemRepository;
            _icuPhraseRepository = icuPhraseRepository;
            _dictRepository = dictRepository;
            _dictCanulaPartRepository = dictCanulaPartRepository;
            _icuDeptScheduleRepository = icuDeptScheduleRepository;
            _icuSysParaRepository = icuSysParaRepository;
        }

        /// <summary>
        /// 查询护理记录模板分组
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <param name="staffCode">员工代码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<SelectPhraseDto>>> SelectIcuPhraseGroup([Required] string deptCode, [Required] string staffCode)
        {
            try
            {
                var icuPhrases = await _icuPhraseRepository.Where(s => (s.DeptCode == deptCode || s.StaffCode == staffCode) && s.ParentId == "0" && s.ValidState == 1)
                    .Select(x => new SelectPhraseDto { Id = x.Id, TypeCode = x.TypeCode, TypeName = x.TypeName, DeptCode = x.DeptCode, StaffCode = x.StaffCode, SortNum = x.SortNum })
                    .ToListAsync();

                //处理类型
                icuPhrases.Where(x => x.StaffCode == staffCode).OrderBy(x => x.SortNum).ToList().ForEach(x => { x.Type = "个人"; });
                icuPhrases.Where(x => x.DeptCode == deptCode).OrderBy(x => x.SortNum).ToList().ForEach(x => { x.Type = "科室"; });

                return JsonResult<List<SelectPhraseDto>>.Ok(data: icuPhrases);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<List<SelectPhraseDto>>.Fail(msg: "查询失败，请联系管理员！");
            }
        }

        /// <summary>
        /// 新增或修改护理记录模板
        /// </summary>
        /// <param name="icuPhraseDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CreatePhraseGroup(CreateUpdateIcuPhraseDto icuPhraseDto)
        {
            if ((string.IsNullOrWhiteSpace(icuPhraseDto.DeptCode) && string.IsNullOrWhiteSpace(icuPhraseDto.StaffCode)) || (!string.IsNullOrWhiteSpace(icuPhraseDto.DeptCode) && !string.IsNullOrWhiteSpace(icuPhraseDto.StaffCode)))
            {
                return JsonResult.Ok(msg: "个人和科室只能选其一！");
            }
            try
            {
                IcuPhrase icuPhrase;

                var finder = icuPhraseDto.Id == Guid.Empty ? null : await _icuPhraseRepository.FindAsync(a => a.Id == icuPhraseDto.Id && a.ValidState == 1);

                if (finder != null)
                {
                    //映射
                    icuPhrase = ObjectMapper.Map(icuPhraseDto, finder);
                    icuPhrase.ParentId = "0";
                    await _icuPhraseRepository.UpdateAsync(icuPhrase);
                    return JsonResult.Ok();
                }
                else
                {
                    icuPhraseDto.Id = _guidGenerator.Create();

                    //获取最大编码
                    int max = 1;
                    var codeList = await _icuPhraseRepository.Where(s => s.ParentId == "0").Select(s => s.TypeCode).ToArrayAsync();
                    if (codeList.Length > 0)
                    {
                        //转int 获取最大编码
                        int[] array = Array.ConvertAll<string, int>(codeList, s => int.Parse(s));
                        max = array.Max() + 1;

                        int length = Math.Abs(max).ToString().Length;
                        if (length == 1)
                        {
                            icuPhraseDto.TypeCode = "00" + max.ToString();
                        }
                        else if (length == 2)
                        {
                            icuPhraseDto.TypeCode = "0" + max.ToString();
                        }
                        else
                        {
                            icuPhraseDto.TypeCode = max.ToString();
                        }
                    }
                    else
                    {
                        icuPhraseDto.TypeCode = "001";
                    }

                    icuPhrase = ObjectMapper.Map<CreateUpdateIcuPhraseDto, IcuPhrase>(icuPhraseDto);
                    icuPhrase.ParentId = "0";
                    icuPhrase.ValidState = 1;
                    await _icuPhraseRepository.InsertAsync(icuPhrase, true);
                    return JsonResult.Ok();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 删除护理记录模板
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task<JsonResult<string>> DeletePhraseInfo(Guid guid)
        {
            try
            {
                //获取当前登录用户信息
                string staffcode = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;

                var finder = guid == Guid.Empty ? null : await _icuPhraseRepository.FindAsync(a => a.Id == guid && a.ValidState == 1);

                if (finder != null)
                {
                    #region 验证修改权限
                    AuthorityEnum f = await _icuSysParaRepository.GetNursingAuthority(staffcode, finder.StaffCode);
                    if (f == AuthorityEnum.session过期)
                    {
                        return JsonResult<string>.Fail(500, "session过期，请重新登录！", null);
                    }
                    if (f == AuthorityEnum.您无权限修改)
                    {
                        return JsonResult<string>.Fail(500, "您无权限修改！", null);
                    }
                    #endregion

                    finder.ValidState = 0;
                    await _icuPhraseRepository.UpdateAsync(finder);
                }

                return JsonResult<string>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message + ex.StackTrace);
                return JsonResult<string>.Fail();
            }
        }

        /// <summary>
        /// 查询护理记录模板
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <param name="staffCode">员工代码</param>
        /// <param name="query">模板名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<PhraseGroupDto>>> SelectIcuPhraseList([Required] string deptCode, [Required] string staffCode, string query)
        {
            try
            {
                //定义返回参数
                List<PhraseGroupDto> phraseGroups = new List<PhraseGroupDto>();

                //模板分类
                List<SelectPhraseDto> phraseList = new List<SelectPhraseDto>();

                if (!string.IsNullOrWhiteSpace(query))
                {
                    //获取护理模板列表
                    var icuPhrases = await _icuPhraseRepository
                        .Where(s => (s.StaffCode == staffCode || s.DeptCode == deptCode) && s.PhraseText.Contains(query) && s.ParentId != "0" && s.ValidState == 1).ToListAsync();

                    var array = icuPhrases.Select(s => s.ParentId).Distinct().ToArray();

                    //查询模板分类
                    phraseList = await _icuPhraseRepository.Where(s => (s.StaffCode == staffCode || s.DeptCode == deptCode) && array.Contains(s.TypeCode) && s.ParentId == "0" && s.ValidState == 1)
                        .Select(s => new SelectPhraseDto()
                        {
                            TypeCode = s.TypeCode,
                            TypeName = s.TypeName,
                            StaffCode = s.StaffCode,
                            DeptCode = s.DeptCode,
                            SortNum = s.SortNum
                        }).ToListAsync();
                }
                else
                {
                    //查询模板分类
                    phraseList = await _icuPhraseRepository.Where(s => (s.StaffCode == staffCode || s.DeptCode == deptCode) && s.ParentId == "0" && s.ValidState == 1)
                       .Select(s => new SelectPhraseDto()
                       {
                           TypeCode = s.TypeCode,
                           TypeName = s.TypeName,
                           StaffCode = s.StaffCode,
                           DeptCode = s.DeptCode,
                           SortNum = s.SortNum
                       }).ToListAsync();
                }

                //查询模板内容
                foreach (SelectPhraseDto phrase in phraseList)
                {
                    var phrases = _icuPhraseRepository.Where(s => (s.StaffCode == staffCode || s.DeptCode == deptCode) && s.ParentId == phrase.TypeCode && s.ValidState == 1)
                        .WhereIf(!string.IsNullOrWhiteSpace(query), s => s.PhraseText.Contains(query)).OrderBy(s => s.SortNum).ToList();
                    var phraseDtos = ObjectMapper.Map<List<IcuPhrase>, List<IcuPhraseDto>>(phrases);
                    phrase.icuPhraseDto = phraseDtos;
                }

                phraseGroups.Add(new PhraseGroupDto { TypeName = "个人模板", icuPhraseDto = phraseList.Where(x => x.StaffCode == staffCode).OrderBy(x => x.SortNum).ToList() });
                phraseGroups.Add(new PhraseGroupDto { TypeName = "科室模板", icuPhraseDto = phraseList.Where(x => x.DeptCode == deptCode).OrderBy(x => x.SortNum).ToList() });

                return JsonResult<List<PhraseGroupDto>>.Ok(data: phraseGroups);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<List<PhraseGroupDto>>.Fail(msg: "查询失败，请联系管理员！");
            }
        }

        /// <summary>
        /// 新增或修改护理记录模板
        /// </summary>
        /// <param name="icuPhraseDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CreatePhraseInfo(CreateUpdateIcuPhraseDto icuPhraseDto)
        {
            if ((string.IsNullOrWhiteSpace(icuPhraseDto.DeptCode) && string.IsNullOrWhiteSpace(icuPhraseDto.StaffCode)) || (!string.IsNullOrWhiteSpace(icuPhraseDto.DeptCode) && !string.IsNullOrWhiteSpace(icuPhraseDto.StaffCode)))
            {
                return JsonResult.Ok(msg: "个人和科室只能选其一！");
            }
            try
            {
                //创建、修改护理记录模板
                bool f = await _icuPhraseRepository.CreatePhraseInfo(icuPhraseDto);

                //重新排序
                if (icuPhraseDto.ParentId != "0")
                {
                    if (f == true)
                    {
                        var views = await _icuPhraseRepository.Where(x => x.DeptCode == icuPhraseDto.DeptCode && x.ParentId == icuPhraseDto.ParentId).OrderBy(x => x.SortNum).ToListAsync();
                        foreach (var view in views)
                        {
                            var idx = views.IndexOf(view);
                            view.SortNum = (idx + 1) * 10;
                        }
                        await _icuPhraseRepository.UpdateRangeAsync(views);
                    }
                }

                return JsonResult.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 复制护理记录模板,从一个科室到另一个科室
        /// </summary>
        /// <param name="oldDeptCode">源科室代码</param>
        /// <param name="newDeptCode">目标科室代码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> CopyPhraseInfo([Required] string oldDeptCode, [Required] string newDeptCode)
        {
            try
            {
                List<IcuPhrase> oldIcuPhrase = await _icuPhraseRepository.Where(s => s.DeptCode == oldDeptCode && s.ValidState == 1).ToListAsync();
                var existcuPhrase = await _icuPhraseRepository.Where(s => s.DeptCode == newDeptCode && s.ValidState == 1).Select(x => new { x.TypeCode, x.ParentId }).ToListAsync();

                List<IcuPhraseReplenishDto> oldIcuPhraseDto = ObjectMapper.Map<List<IcuPhrase>, List<IcuPhraseReplenishDto>>(oldIcuPhrase);
                oldIcuPhraseDto.ForEach(x => { x.Id = _guidGenerator.Create(); x.DeptCode = newDeptCode; });

                List<IcuPhraseReplenishDto> newIcuPhraseDto = new List<IcuPhraseReplenishDto>();
                foreach (var item in oldIcuPhraseDto)
                {
                    var finder = existcuPhrase.Find(x => x.TypeCode == item.TypeCode && x.ParentId == item.ParentId);
                    if (finder == null)
                    {
                        newIcuPhraseDto.Add(item);
                    }
                }

                List<IcuPhrase> icuPhrase = ObjectMapper.Map<List<IcuPhraseReplenishDto>, List<IcuPhrase>>(newIcuPhraseDto);
                await _icuPhraseRepository.CreateRangeAsync(icuPhrase);

                return JsonResult.Ok();

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        #region 基础配置接口
        /// <summary>
        /// 将一个科室的参数导入另一科室
        /// </summary>
        /// <param name="oldDeptCode">源科室</param>
        /// <param name="newDeptCode">目标科室</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SaveCanulaItemList([Required] string oldDeptCode, [Required] string newDeptCode)
        {
            try
            {
                List<IcuParaModuleDto> moduleDtos = new List<IcuParaModuleDto>();
                List<IcuParaItemDto> paraItemDtos = new List<IcuParaItemDto>();
                List<DictDto> dictDtos = new List<DictDto>();

                //查询将要导入的源科室参数
                var oldModules = await _icuParaModuleRepository.AsNoTracking().Where(x => x.DeptCode == oldDeptCode && x.ValidState == 1).ToListAsync();
                var oldModuleDtos = ObjectMapper.Map<List<IcuParaModule>, List<IcuParaModuleDto>>(oldModules);

                var oldItems = await _icuParaItemRepository.AsNoTracking().Where(x => oldModules.Select(s => s.ModuleCode).Contains(x.ModuleCode) && x.ValidState == 1).ToListAsync();
                var oldItemDtos = ObjectMapper.Map<List<IcuParaItem>, List<IcuParaItemDto>>(oldItems);

                var oldDicts = await _dictRepository.AsNoTracking().Where(x => oldModules.Select(s => s.ModuleCode).Contains(x.ModuleCode) && x.ValidState == 1).ToListAsync();
                var oldDictDtos = ObjectMapper.Map<List<Dict>, List<DictDto>>(oldDicts);

                //查询目标科室已有数据
                var newModules = await _icuParaModuleRepository.Where(x => x.DeptCode == newDeptCode && x.ValidState == 1).ToListAsync();
                var newItems = await _icuParaItemRepository.Where(x => x.DeptCode == newDeptCode && x.ValidState == 1).ToListAsync();

                //生成新科室数据
                //获取最大编码
                int max = 1;
                var codeList = await _icuParaModuleRepository.Select(s => s.ModuleCode).ToArrayAsync();
                if (codeList.Length > 0)
                {
                    //转int 获取最大编码
                    int[] array = Array.ConvertAll<string, int>(codeList, s => int.Parse(s));
                    max = array.Max() + 1;
                }
                foreach (IcuParaModuleDto paraModule in oldModuleDtos)
                {
                    //获取模块代码，并判断是否存在该模块参数
                    string newModuleCode = String.Empty;
                    var finder = newModules.Find(x => x.ModuleName == paraModule.ModuleName && x.ModuleType == paraModule.ModuleType);
                    if (finder != null)
                    {
                        newModuleCode = finder.ModuleCode;

                        //添加参数
                        var paraItems = oldItemDtos.Where(x => x.ModuleCode == paraModule.ModuleCode).ToList();
                        var newParaItems = newItems.Where(x => x.ModuleCode == newModuleCode).ToList();
                        foreach (IcuParaItemDto paraItemDto in paraItems)
                        {
                            //参数在模块下不存在时添加参数及参数字典
                            var finderPara = newParaItems.Find(x => x.ParaCode == paraItemDto.ParaCode);
                            if (finderPara == null)
                            {
                                paraItemDto.Id = _guidGenerator.Create();
                                paraItemDto.DeptCode = newDeptCode;
                                paraItemDto.ModuleCode = newModuleCode;
                                paraItemDtos.Add(paraItemDto);

                                var dicts = oldDictDtos.Where(x => x.ModuleCode == paraModule.ModuleCode && x.ParaCode == paraItemDto.ParaCode).ToList();
                                foreach (DictDto dictDto in dicts)
                                {
                                    dictDto.Id = _guidGenerator.Create();
                                    dictDto.DeptCode = newDeptCode;
                                    dictDto.ModuleCode = newModuleCode;
                                    dictDtos.Add(dictDto);
                                }
                            }
                        }
                    }
                    else
                    {
                        newModuleCode = max.ToString();

                        //添加参数
                        var paraItems = oldItemDtos.Where(x => x.ModuleCode == paraModule.ModuleCode).ToList();
                        foreach (IcuParaItemDto paraItemDto in paraItems)
                        {
                            paraItemDto.Id = _guidGenerator.Create();
                            paraItemDto.DeptCode = newDeptCode;
                            paraItemDto.ModuleCode = newModuleCode;
                            paraItemDtos.Add(paraItemDto);
                        }

                        var dicts = oldDictDtos.Where(x => x.ModuleCode == paraModule.ModuleCode).ToList();
                        foreach (DictDto dictDto in dicts)
                        {
                            dictDto.Id = _guidGenerator.Create();
                            dictDto.DeptCode = newDeptCode;
                            dictDto.ModuleCode = newModuleCode;
                            dictDtos.Add(dictDto);
                        }

                        //添加模块
                        paraModule.Id = _guidGenerator.Create();
                        paraModule.DeptCode = newDeptCode;
                        paraModule.ModuleCode = newModuleCode;
                        moduleDtos.Add(paraModule);
                        max++;
                    }
                }
                //映射
                var modules = ObjectMapper.Map<List<IcuParaModuleDto>, List<IcuParaModule>>(moduleDtos);
                var icuParaItems = ObjectMapper.Map<List<IcuParaItemDto>, List<IcuParaItem>>(paraItemDtos);
                var dictItems = ObjectMapper.Map<List<DictDto>, List<Dict>>(dictDtos);

                if (modules.Any() || icuParaItems.Any())
                {
                    await _icuParaModuleRepository.CreateRangeAsync(modules);
                    await _icuParaItemRepository.CreateRangeAsync(icuParaItems);
                    await _dictRepository.CreateRangeAsync(dictItems);
                }

                return JsonResult.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }


        /// <summary>
        /// 获取科室班次列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="type">科室班次类型，如观察项：0，出入量：1 血液净化：2，ECMO：3，PICCO：4</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<IcuDeptScheduleDto>>> GetDeptScheduleList(string deptCode, DeptScheduleTypeEnum type)
        {
            try
            {
                var scheduleList = await _icuDeptScheduleRepository.Where(s => s.DeptCode == deptCode && s.Type == type).OrderBy(x => x.SortNum).ToListAsync();
                var resultData = ObjectMapper.Map<List<IcuDeptSchedule>, List<IcuDeptScheduleDto>>(scheduleList);
                foreach (var re in resultData)
                {
                    re.StartTime = int.Parse(re.StartTime?.Split(':')[0]) + "";
                    re.EndTime = int.Parse(re.EndTime?.Split(':')[0]) + "";
                }
                return JsonResult<List<IcuDeptScheduleDto>>.Ok(data: resultData);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<List<IcuDeptScheduleDto>>.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 新增科室班次信息
        /// </summary>
        /// <param name="deptScheduleConfigDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> AddDeptScheduleInfo(DeptScheduleConfigDto deptScheduleConfigDto)
        {
            try
            {
                // 查询新增的班次是否存在，若存在则提示；亦或者设置班次名称为唯一键
                var isHaveScheduleName = await _icuDeptScheduleRepository.Where(s => s.ScheduleName == deptScheduleConfigDto.ScheduleName && s.DeptCode == deptScheduleConfigDto.DeptCode && s.Type == deptScheduleConfigDto.Type).FirstOrDefaultAsync();
                if (isHaveScheduleName != null)
                {
                    return JsonResult.Fail(msg: "班次名称已存在！");
                }

                // 处理是否跨天数据转换
                var isKt = deptScheduleConfigDto.Days == true ? "2" : "1";

                // 写入
                IcuDeptSchedule icuDeptSchedule = new IcuDeptSchedule(_guidGenerator.Create())
                {
                    DeptCode = deptScheduleConfigDto.DeptCode,
                    ScheduleCode = deptScheduleConfigDto.ScheduleCode,
                    ScheduleName = deptScheduleConfigDto.ScheduleName,
                    StartTime = GetTime(deptScheduleConfigDto.StartTime, 1, (int)deptScheduleConfigDto.ScheduleTimeTypeEnum),
                    EndTime = GetTime(deptScheduleConfigDto.EndTime, 2, (int)deptScheduleConfigDto.ScheduleTimeTypeEnum),
                    Period = deptScheduleConfigDto.Period,
                    Days = isKt,
                    Hours = deptScheduleConfigDto.Hours,
                    SortNum = deptScheduleConfigDto.SortNum,
                    Type = deptScheduleConfigDto.Type,
                    ScheduleTimeTypeEnum = deptScheduleConfigDto.ScheduleTimeTypeEnum
                };
                await _icuDeptScheduleRepository.InsertAsync(icuDeptSchedule);

                return JsonResult.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 编辑科室班次信息
        /// </summary>
        /// <param name="deptScheduleConfigDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<JsonResult> PutDeptScheduleInfo(DeptScheduleConfigDto deptScheduleConfigDto)
        {
            try
            {
                var scheduleInfo = await _icuDeptScheduleRepository.Where(s => s.Id == deptScheduleConfigDto.Id).FirstOrDefaultAsync();

                if (scheduleInfo != null)
                {
                    scheduleInfo.DeptCode = deptScheduleConfigDto.DeptCode;
                    scheduleInfo.ScheduleCode = deptScheduleConfigDto.ScheduleCode;
                    scheduleInfo.ScheduleName = deptScheduleConfigDto.ScheduleName;
                    scheduleInfo.StartTime = GetTime(deptScheduleConfigDto.StartTime, 1, (int)deptScheduleConfigDto.ScheduleTimeTypeEnum);
                    scheduleInfo.EndTime = GetTime(deptScheduleConfigDto.EndTime, 2, (int)deptScheduleConfigDto.ScheduleTimeTypeEnum);
                    scheduleInfo.Period = deptScheduleConfigDto.Period;
                    scheduleInfo.Days = deptScheduleConfigDto.Days == true ? "2" : "1";
                    scheduleInfo.Hours = deptScheduleConfigDto.Hours;
                    scheduleInfo.SortNum = deptScheduleConfigDto.SortNum;
                    scheduleInfo.Type = deptScheduleConfigDto.Type;
                    scheduleInfo.ScheduleTimeTypeEnum = deptScheduleConfigDto.ScheduleTimeTypeEnum;

                    await _icuDeptScheduleRepository.UpdateAsync(scheduleInfo);
                }
                else
                {
                    return JsonResult.Fail(data: deptScheduleConfigDto, msg: "无效的数据，更新失败！");
                }
                return JsonResult.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 删除科室班次信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<JsonResult> DelDeptScheduleInfo(Guid id)
        {
            try
            {
                var scheduleInfo = await _icuDeptScheduleRepository.Where(s => s.Id == id).FirstOrDefaultAsync();
                if (scheduleInfo != null)
                {
                    await _icuDeptScheduleRepository.DeleteAsync(scheduleInfo);
                }
                else
                {
                    return JsonResult.Fail(data: id, msg: "无效的ID值，删除失败！");
                }
                return JsonResult.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        #endregion

        /// <summary>
        /// 根据小时返回时间
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="type">1:开始时间，2：结束时间</param>
        /// <param name="scheduleType">1:前闭后开，2：前开后闭</param>
        /// <returns></returns>
        private string GetTime(string hours, int type, int scheduleType)
        {
            string minute = hours.Length == 1 ? $"0{hours}" : $"{hours}";
            if (scheduleType == 1)
            {
                if (type == 1)
                    return $"{minute}:00";
                else
                    return $"{minute}:59";
            }
            else if (scheduleType == 2)
            {
                if (type == 1)
                    return $"{minute}:01";
                else
                    return $"{minute}:00";
            }
            else
            {
                throw new ArgumentException($"计算班次开始时间或结束时间错误，无效的班次类型【>>[{scheduleType}]<< 1:前闭后开，2：前开后闭】");
            }
        }
    }

    public class StaffGroup
    {
        public string StaffCode { get; set; }
        public string DeptCode { get; set; }
    }
}
