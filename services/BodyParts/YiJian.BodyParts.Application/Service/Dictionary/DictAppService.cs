using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YiJian.BodyParts.Dtos;
using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.IService;
using YiJian.BodyParts.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using Volo.Abp.Application.Services;
using Volo.Abp.Guids;

namespace YiJian.BodyParts.Service
{
    /// <summary>
    /// 基础字典服务
    /// </summary>
    public class DictAppService : ApplicationService, IDictAppService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IIcuParaModuleRepository _icuParaModuleRepository;
        private IIcuParaItemRepository _icuParaItemRepository;
        private IDictRepository _dictRepository;
        private IIcuSysParaRepository _icuSysParaRepository;
        private IDictSourceRepository _dictSourcesRepository;


        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="guidGenerator"></param>
        /// <param name="httpContextAccessor">参数模板</param>
        /// <param name="icuParaModuleRepository">参数模板</param>
        /// <param name="icuParaItemRepository">参数字典</param>
        /// <param name="dictRepository">参数下拉项字典</param>
        /// <param name="icuSysParaRepository"></param>
        /// <param name="dictSourceRepository"></param>

        public DictAppService(IGuidGenerator guidGenerator,
            IHttpContextAccessor httpContextAccessor,
            IIcuParaModuleRepository icuParaModuleRepository,
            IIcuParaItemRepository icuParaItemRepository,
            IDictRepository dictRepository,
            IIcuSysParaRepository icuSysParaRepository,
            IDictSourceRepository dictSourceRepository
            )
        {
            _guidGenerator = guidGenerator;
            _httpContextAccessor = httpContextAccessor;
            _icuParaModuleRepository = icuParaModuleRepository;
            _icuParaItemRepository = icuParaItemRepository;
            _dictRepository = dictRepository;
            _icuSysParaRepository = icuSysParaRepository;
            _dictSourcesRepository = dictSourceRepository;
        }

        #region 模块参数
        /// <summary>
        /// 根据条件查询模块参数
        /// </summary>
        /// <param name="deptCode">科室代码(系统项目:system)</param>
        /// <param name="moduleType">模块类型:(CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO，RR：呼吸监测)</param>
        /// <param name="query">名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<IcuParaModuleDto>>> GetParaModuleList(string deptCode, string moduleType, string query)
        {
            if (string.IsNullOrWhiteSpace(deptCode))
            {
                return JsonResult<List<IcuParaModuleDto>>.RequestParamsIsNull(msg: "请传入科室代码！");
            }
            //如果不是系统参数，模块类型不能为空
            if (deptCode != "system" && string.IsNullOrWhiteSpace(moduleType))
            {
                return JsonResult<List<IcuParaModuleDto>>.RequestParamsIsNull(msg: "请传入模块类型！");
            }
            try
            {
                var moduleList = await _icuParaModuleRepository.Where(s => s.DeptCode == deptCode && s.ValidState == 1)
                    .WhereIf(!string.IsNullOrWhiteSpace(moduleType), s => s.ModuleType == moduleType)
                    .WhereIf(!string.IsNullOrWhiteSpace(query), s => s.ModuleName.Contains(query) || s.Enname.Contains(query))
                    .OrderBy(x => x.SortNum).ToListAsync();

                var moduleDtoList = ObjectMapper.Map<List<IcuParaModule>, List<IcuParaModuleDto>>(moduleList);
                return JsonResult<List<IcuParaModuleDto>>.Ok(data: moduleDtoList);

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<List<IcuParaModuleDto>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 新增或修改模块参数
        /// </summary>
        /// <param name="moduleDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SaveParaModuleInfo(CreateUpdateIcuParaModuleDto moduleDto)
        {
            if (string.IsNullOrWhiteSpace(moduleDto.ModuleName))
            {
                return JsonResult.RequestParamsIsNull(msg: "请传入模块名称！");
            }
            try
            {
                //如果不是系统参数，模块类型不能为空
                if (moduleDto.DeptCode != "system" && string.IsNullOrWhiteSpace(moduleDto.ModuleType))
                {
                    return JsonResult.RequestParamsIsNull(msg: "请传入模块类型！");
                }

                IcuParaModule icuParaModule;

                var finder = string.IsNullOrWhiteSpace(moduleDto.ModuleCode) ? null : await _icuParaModuleRepository.FindAsync(a => a.ModuleCode == moduleDto.ModuleCode && a.ValidState == 1);

                if (finder != null)
                {
                    finder.ModuleName = moduleDto.ModuleName;
                    finder.DisplayName = moduleDto.DisplayName;
                    finder.SortNum = moduleDto.SortNum;
                    finder.IsEnable = moduleDto.IsEnable;
                    finder.IsBloodflow = moduleDto.IsBloodflow;
                    finder.RiskLevel = moduleDto.RiskLevel;
                    finder.PartNumber = moduleDto.PartNumber;
                    await _icuParaModuleRepository.UpdateAsync(finder);
                    return JsonResult.Ok();
                }
                else
                {
                    moduleDto.Id = _guidGenerator.Create();
                    //获取最大编码
                    int max = 1;
                    var codeList = await _icuParaModuleRepository.Select(s => s.ModuleCode).ToArrayAsync();
                    if (codeList.Length > 0)
                    {
                        //转int 获取最大编码
                        int[] array = Array.ConvertAll<string, int>(codeList, s => int.Parse(s));
                        max = array.Max() + 1;
                    }
                    moduleDto.ModuleCode = max.ToString();
                    icuParaModule = ObjectMapper.Map<CreateUpdateIcuParaModuleDto, IcuParaModule>(moduleDto);
                    icuParaModule.ValidState = 1;
                    icuParaModule.Enname = SpellCode.GetSpellCode(moduleDto.ModuleName);

                    await _icuParaModuleRepository.InsertAsync(icuParaModule, true);
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
        /// 根据模块名称查询模块code
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<string>> SelectParaModuleCode([Required] string moduleName)
        {
            var paraModule = await _icuParaModuleRepository.FirstOrDefaultAsync(a => a.ModuleName.Contains(moduleName));
            return JsonResult<string>.Ok(data: paraModule.ModuleCode);
        }

        /// <summary>
        /// 修改模块排序
        /// </summary>
        /// <param name="icuParaModuleDtos"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<JsonResult> UpdateParaModuleSort(List<CreateUpdateIcuParaModuleDto> icuParaModuleDtos)
        {
            try
            {
                //定义需要修改的列表
                List<IcuParaModule> icuParaModules = new List<IcuParaModule>();

                //查询所有需要排序的参数列表
                var guids = icuParaModuleDtos.Select(s => s.Id).ToArray();
                var paraModules = await _icuParaModuleRepository.Where(s => guids.Contains(s.Id)).AsNoTracking().ToListAsync();

                //排序
                foreach (CreateUpdateIcuParaModuleDto icuParaModuleDto in icuParaModuleDtos)
                {
                    var idx = icuParaModuleDtos.IndexOf(icuParaModuleDto);
                    icuParaModuleDto.SortNum = (idx + 1) * 10;

                    var finder = paraModules.Find(s => s.Id == icuParaModuleDto.Id);
                    //映射
                    icuParaModules.Add(ObjectMapper.Map(icuParaModuleDto, finder));
                }

                if (icuParaModules.Any())
                {
                    await _icuParaModuleRepository.UpdateRangeAsync(icuParaModules);
                    return JsonResult.Ok();
                }
                else
                {
                    return JsonResult.DataNotFound(msg: "暂无数据！");
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 删除模块参数(仅用于删除科室项目模块)
        /// </summary>
        /// <param name="moduleCode">模块代码</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<JsonResult> DeleteParaModuleInfo(string moduleCode)
        {
            if (string.IsNullOrWhiteSpace(moduleCode))
            {
                return JsonResult.RequestParamsIsNull(msg: "请输入模块代码！");
            }
            try
            {
                var finder = await _icuParaModuleRepository.FirstOrDefaultAsync(s => s.ModuleCode == moduleCode && s.ValidState == 1);
                if (finder != null)
                {
                    if (finder.DeptCode == "system")
                    {
                        return JsonResult.RequestParamsIsNull(msg: "系统项目模块不能删除！");
                    }

                    var guids = await _icuParaItemRepository.Where(s => s.ModuleCode == moduleCode && s.ValidState == 1).Select(s => s.Id).ToArrayAsync();
                    //定义批量删除
                    List<IcuParaItem> icuParaItems = new List<IcuParaItem>();
                    List<Dict> dicts = new List<Dict>();
                    string message = string.Empty;

                    foreach (Guid guid in guids)
                    {
                        var icuParaItem = guid == Guid.Empty ? null : _icuParaItemRepository.Where(a => a.Id == guid && a.ValidState == 1).AsNoTracking().FirstOrDefault();

                        if (finder != null)
                        {
                            //查询参数字典
                            var dictItems = await _dictRepository.Where(s => s.ParaCode == icuParaItem.ParaCode && s.ModuleCode == icuParaItem.ModuleCode && s.ValidState == 1).AsNoTracking().ToListAsync();
                            //查询是否有患者使用过参数 TODO:XUWEI
                            //var icuPatientParas = await _icuPatientParaRepository.Where(s => s.ParaCode == icuParaItem.ParaCode && s.ModuleCode == icuParaItem.ModuleCode).CountAsync();
                            //if (icuPatientParas > 0)
                            //{
                            //    message += icuParaItem.ParaName + "，";
                            //}
                            //else
                            //{
                            //    icuParaItem.ValidState = 0;
                            //    icuParaItems.Add(icuParaItem);

                            //    //删除参数字典
                            //    foreach (Dict dict in dictItems)
                            //    {
                            //        dict.ValidState = 0;
                            //    }
                            //    dicts.AddRange(dictItems);
                            //}
                        }
                    }
                    if (string.IsNullOrWhiteSpace(message))
                    {
                        //删除模块
                        finder.ValidState = 0;
                        await _icuParaModuleRepository.UpdateAsync(finder);
                        //删除参数列表
                        await _icuParaItemRepository.UpdateRangeAsync(icuParaItems);
                        //删除参数字典列表
                        if (dicts.Any())
                        {
                            await _dictRepository.UpdateRangeAsync(dicts);
                        }
                        return JsonResult.Ok(msg: "删除成功！");
                    }
                    else
                    {
                        return JsonResult.Ok(msg: message + "已经有患者在使用，不能删除，请选择停用！");
                    }
                }
                else
                {
                    return JsonResult.DataNotFound(msg: "暂无数据！");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }
        #endregion

        #region 科室参数
        /// <summary>
        /// 根据条件获取参数项目列表
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="moduleType">模块类型:(CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO，RR：呼吸监测)</param>
        /// <param name="query">项目名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<IcuParaItemDto>>> GetIcuParaItemList([Required(ErrorMessage = "部门编码不允许为空")] string deptCode, string moduleCode, string moduleType, string query)
        {
            //如果不是系统参数，模块类型不能为空
            if (deptCode != "system" && string.IsNullOrWhiteSpace(moduleType))
            {
                return JsonResult<List<IcuParaItemDto>>.RequestParamsIsNull(msg: "请传入模块类型！");
            }
            try
            {
                //多条件拼接扩展
                Expression<Func<IcuParaModule, bool>> predicatePM = PredicateBuilder.True<IcuParaModule>();

                predicatePM = predicatePM.And(s => s.DeptCode == deptCode && s.ValidState == 1);

                if (!string.IsNullOrWhiteSpace(moduleType))
                {
                    predicatePM = predicatePM.And(s => s.ModuleType == moduleType);
                }

                //查询参数
                Expression<Func<IcuParaItem, bool>> predicatePI = PredicateBuilder.True<IcuParaItem>();

                predicatePI = predicatePI.And(s => s.ValidState == 1);

                if (!string.IsNullOrWhiteSpace(moduleCode))
                {
                    predicatePI = predicatePI.And(s => s.ModuleCode == moduleCode);
                }

                if (!string.IsNullOrWhiteSpace(query))
                {
                    predicatePI = predicatePI.And(s => s.ParaName.Contains(query));
                }

                var paraItemList = await (from s in _icuParaModuleRepository.Where(predicatePM)
                                          join c in _icuParaItemRepository.Where(predicatePI) on s.ModuleCode equals c.ModuleCode
                                          orderby c.GroupName descending, c.SortNum
                                          select c).ToListAsync();


                var paraItemDtoList = ObjectMapper.Map<List<IcuParaItem>, List<IcuParaItemDto>>(paraItemList);
                return JsonResult<List<IcuParaItemDto>>.Ok(data: paraItemDtoList);

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<List<IcuParaItemDto>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 新增或修改系统项目
        /// </summary> 
        /// <param name="itemDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SaveParaItemInfo(CreateUpdateIcuParaItemDto itemDto)
        {
            if (string.IsNullOrWhiteSpace(itemDto.ParaName))
            {
                return JsonResult.RequestParamsIsNull(msg: "请输入参数名称！");
            }
            try
            {
                IcuParaItem paraItem;

                //获取关联项目
                var properties = await _icuParaItemRepository.Where(s => s.DeptCode == itemDto.DeptCode && s.ModuleCode == itemDto.ModuleCode).ToListAsync();

                var finder = itemDto.Id == Guid.Empty ? null : await _icuParaItemRepository.FindAsync(a => a.Id == itemDto.Id && a.ValidState == 1);

                if (finder != null)
                {
                    //同步科室项目属性
                    var paraItems = await _icuParaItemRepository.Where(x => x.ParaCode == finder.ParaCode && x.ValidState == 1).ToListAsync();
                    if (finder.DeptCode == "system")
                    {
                        //判断是否已有同名参数
                        int count = await _icuParaItemRepository.Where(x => x.DeptCode == "system" && x.Id != itemDto.Id && x.ParaName == itemDto.ParaName && x.ValidState == 1).CountAsync();
                        if (count > 0)
                        {
                            return JsonResult.RequestParamsIsNull(msg: "已有相同名称参数，请检查在用参数！");
                        }
                        foreach (IcuParaItem icuParaItem in paraItems)
                        {
                            if (itemDto.ParaName != null) icuParaItem.ParaName = itemDto.ParaName;
                            if (itemDto.UnitName != null) icuParaItem.UnitName = itemDto.UnitName;
                            if (itemDto.Style != null) icuParaItem.Style = itemDto.Style;
                            if (itemDto.ValueType != null) icuParaItem.ValueType = itemDto.ValueType;
                            if (itemDto.ColorParaCode != null)
                            {
                                icuParaItem.ColorParaCode = itemDto.ColorParaCode;
                                icuParaItem.ColorParaName = properties.Where(s => s.ParaCode == itemDto.ColorParaCode).Select(s => s.ParaName).FirstOrDefault();
                            }
                            if (itemDto.PropertyParaCode != null)
                            {
                                icuParaItem.PropertyParaCode = itemDto.PropertyParaCode;
                                icuParaItem.PropertyParaName = properties.Where(s => s.ParaCode == itemDto.PropertyParaCode).Select(s => s.ParaName).FirstOrDefault();
                            }
                            if (itemDto.DeviceParaType != null) icuParaItem.DeviceParaType = itemDto.DeviceParaType;
                            if (itemDto.DeviceParaCode != null) icuParaItem.DeviceParaCode = itemDto.DeviceParaCode;
                        }
                        await _icuParaItemRepository.UpdateRangeAsync(paraItems);
                    }
                    //同步更新患者显示名称
                    if (itemDto.DisplayName != null && finder.DisplayName != itemDto.DisplayName)
                    {
                        finder.DisplayName = itemDto.DisplayName;
                        //定义需要修改的患者显示名称 TODO:XUWEI
                        //List<IcuPatientPara> paras = new List<IcuPatientPara>();
                        //if (finder.DeptCode == "system")
                        //{
                        //    paras = await _icuPatientParaRepository.Where(x => x.ParaCode == finder.ParaCode && !paraItems.Select(s => s.DeptCode).Contains(x.DeptCode)).ToListAsync();
                        //}
                        //else
                        //{
                        //    paras = await _icuPatientParaRepository.Where(x => x.DeptCode == finder.DeptCode && x.ParaCode == finder.ParaCode).ToListAsync();
                        //}
                        //foreach (IcuPatientPara para in paras)
                        //{
                        //    if (itemDto.DisplayName != null) para.DisplayName = itemDto.DisplayName;
                        //}
                        //await _icuPatientParaRepository.UpdateRangeAsync(paras);
                    }

                    if (itemDto.ModuleCode != null) finder.ModuleCode = itemDto.ModuleCode;
                    if (itemDto.ScoreCode != null) finder.ScoreCode = itemDto.ScoreCode;
                    if (itemDto.DecimalDigits != null) finder.DecimalDigits = itemDto.DecimalDigits;
                    if (itemDto.MaxValue != null) finder.MaxValue = itemDto.MaxValue;
                    if (itemDto.MinValue != null) finder.MinValue = itemDto.MinValue;
                    if (itemDto.HighValue != null) finder.HighValue = itemDto.HighValue;
                    if (itemDto.LowValue != null) finder.LowValue = itemDto.LowValue;
                    if (itemDto.Threshold != null && itemDto.Threshold.HasValue)
                    {
                        finder.Threshold = (bool)itemDto.Threshold;
                    }
                    if (itemDto.SortNum != 0) finder.SortNum = itemDto.SortNum;
                    if (itemDto.IsEnable != null && itemDto.IsEnable.HasValue && finder.DeptCode != "system")
                    {
                        finder.IsEnable = (bool)itemDto.IsEnable;
                    }

                    if (itemDto.DeviceTimePoint != null) finder.DeviceTimePoint = itemDto.DeviceTimePoint;

                    //新增参数字典
                    List<Dict> newDicts = new List<Dict>();
                    List<Dict> upDicts = new List<Dict>();

                    if (itemDto.createUpdateDictDtos != null)
                    {
                        if (itemDto.createUpdateDictDtos.Count > 0)
                        {
                            //获取最大编码
                            int maxDict = 1;
                            var maxCode = await _dictRepository.Select(s => s.DictCode).ToArrayAsync();
                            if (maxCode.Length > 0)
                            {
                                int[] array = Array.ConvertAll<string, int>(maxCode, s => int.Parse(s));
                                maxDict = array.Max() + 1;
                            }

                            //定义默认值
                            string dataSource = null;
                            foreach (CreateUpdateDictDto dictDto in itemDto.createUpdateDictDtos)
                            {
                                var dictfinder = dictDto.Id == Guid.Empty || dictDto.Id == null ? null : await _dictRepository.FindAsync(s => s.Id == dictDto.Id && s.ValidState == 1);
                                if (dictfinder != null)
                                {
                                    dictfinder.ModuleCode = dictDto.ModuleCode;
                                    dictfinder.DictValue = dictDto.DictValue;
                                    dictfinder.DictDesc = dictDto.DictDesc;
                                    dictfinder.IsDefault = dictDto.IsDefault;
                                    dictfinder.SortNum = itemDto.createUpdateDictDtos.IndexOf(dictDto) + 1;
                                    upDicts.Add(dictfinder);
                                }
                                else
                                {
                                    newDicts.Add(new Dict(_guidGenerator.Create())
                                    {
                                        DeptCode = itemDto.DeptCode,
                                        ModuleCode = itemDto.ModuleCode,
                                        ParaCode = itemDto.ParaCode,
                                        ParaName = itemDto.ParaName,
                                        DictCode = maxDict.ToString(),
                                        DictValue = dictDto.DictValue,
                                        DictDesc = dictDto.DictDesc,
                                        IsDefault = dictDto.IsDefault,
                                        SortNum = itemDto.createUpdateDictDtos.IndexOf(dictDto) + 1,
                                        ValidState = 1,
                                        IsEnable = true
                                    });
                                    maxDict++;
                                }
                                //修改默认值
                                if (dictDto.IsDefault == true)
                                {
                                    dataSource = dictDto.DictValue;
                                }
                            }
                            finder.DataSource = dataSource;
                            if (upDicts.Any())
                            {
                                await _dictRepository.UpdateRangeAsync(upDicts);
                            }
                            if (newDicts.Any())
                            {
                                await _dictRepository.CreateRangeAsync(newDicts);
                            }

                            //删除参数
                            List<Dict> deDicts = new List<Dict>();
                            var dicts = await _dictRepository.Where(s => s.DeptCode == finder.DeptCode && s.ModuleCode == finder.ModuleCode
                                    && s.ParaCode == finder.ParaCode && s.ValidState == 1).ToListAsync();
                            foreach (Dict dict in dicts)
                            {
                                if (!upDicts.Contains(dict) && !newDicts.Contains(dict))
                                {
                                    dict.ValidState = 0;
                                    deDicts.Add(dict);
                                }
                            }
                            if (dicts.Any())
                            {
                                await _dictRepository.UpdateRangeAsync(deDicts);
                            }
                        }
                        else if (itemDto.createUpdateDictDtos.Count < 1)
                        {
                            var dicts = await _dictRepository.Where(s => s.DeptCode == finder.DeptCode && s.ModuleCode == finder.ModuleCode
                                    && s.ParaCode == finder.ParaCode && s.ValidState == 1).ToListAsync();
                            foreach (Dict dict in dicts)
                            {
                                dict.ValidState = 0;
                            }
                            if (dicts.Any())
                            {
                                await _dictRepository.UpdateRangeAsync(dicts);
                            }
                        }

                        //修改
                        await _icuParaItemRepository.UpdateAsync(finder);
                    }
                    return JsonResult.Ok();
                }
                else
                {
                    if (itemDto.DeptCode != "system")
                    {
                        return JsonResult.RequestParamsIsNull(msg: "非系统参数不能新增参数，只能导入！");
                    }

                    //判断是否已有同名参数
                    int count = await _icuParaItemRepository.Where(x => x.DeptCode == "system" && x.ParaName == itemDto.ParaName && x.ValidState == 1).CountAsync();
                    if (count > 0)
                    {
                        return JsonResult.RequestParamsIsNull(msg: "已添加相同名称参数！");
                    }

                    //获取最大编码
                    int max = 0;
                    var codeList = await _icuParaItemRepository.Select(s => s.ParaCode).ToArrayAsync();
                    if (codeList.Length > 0)
                    {
                        int[] array = Array.ConvertAll<string, int>(codeList, s => int.Parse(s));
                        max = array.Max();
                    }

                    itemDto.Id = _guidGenerator.Create();
                    itemDto.ParaCode = (max + 1).ToString();
                    itemDto.ColorParaName = properties.Where(s => s.ParaCode == itemDto.ColorParaCode).Select(s => s.ParaName).FirstOrDefault();
                    itemDto.PropertyParaName = properties.Where(s => s.ParaCode == itemDto.PropertyParaCode).Select(s => s.ParaName).FirstOrDefault();
                    paraItem = ObjectMapper.Map<CreateUpdateIcuParaItemDto, IcuParaItem>(itemDto);
                    paraItem.IsEnable = true;
                    paraItem.ValidState = 1;

                    //新增参数字典
                    List<Dict> newDicts = new List<Dict>();

                    if (itemDto.createUpdateDictDtos != null)
                    {
                        //获取最大编码
                        int maxDict = 1;
                        var maxCode = await _dictRepository.Select(s => s.DictCode).ToArrayAsync();
                        if (maxCode.Length > 0)
                        {
                            int[] array = Array.ConvertAll<string, int>(maxCode, s => int.Parse(s));
                            maxDict = array.Max() + 1;
                        }

                        //定义默认值
                        string dataSource = null;
                        foreach (CreateUpdateDictDto dictDto in itemDto.createUpdateDictDtos)
                        {
                            Dict dict;
                            dictDto.Id = _guidGenerator.Create();
                            dictDto.ParaCode = itemDto.ParaCode;//参数代码
                            dictDto.DictCode = maxDict.ToString();
                            dict = ObjectMapper.Map<CreateUpdateDictDto, Dict>(dictDto);
                            dict.SortNum = itemDto.createUpdateDictDtos.IndexOf(dictDto) + 1;
                            dict.ValidState = 1;
                            dict.IsEnable = true;
                            newDicts.Add(dict);
                            maxDict++;

                            if (dictDto.IsDefault == true)
                            {
                                dataSource = dictDto.DictValue;
                            }
                        }

                        paraItem.DataSource = dataSource;
                    }
                    //新增
                    await _icuParaItemRepository.InsertAsync(paraItem);
                    if (newDicts.Any())
                    {
                        await _dictRepository.CreateRangeAsync(newDicts);
                    }
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
        /// 查询批量导入参数
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleType">模块类型:(CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO，RR：呼吸监测)</param>
        /// <param name="groupName">导管(管道属性、管道观察),皮肤(皮肤属性、皮肤观察)观察项和出入量传null</param>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<ModuleListDto>>> SelectModuleList([Required] string deptCode, [Required] string moduleType, string moduleCode, string groupName)
        {
            try
            {
                //查询系统参数
                var sysParas = await _icuParaItemRepository.Where(x => x.DeptCode == "system" && x.ValidState == 1).ToListAsync();

                //查询科室参数
                var deptModules = await _icuParaModuleRepository.Where(x => x.DeptCode == deptCode && x.ModuleType == moduleType && x.ValidState == 1)
                    .WhereIf(moduleType == "CANULA", x => x.ModuleCode == moduleCode)
                    .WhereIf(moduleType == "SKIN", x => x.ModuleCode == moduleCode).ToListAsync();

                var deptParas = await _icuParaItemRepository.Where(x => x.DeptCode == deptCode && deptModules.Select(x => x.ModuleCode).Contains(x.ModuleCode)
                    && x.GroupName == groupName && x.ValidState == 1).Select(x => x.ParaCode).ToListAsync();

                //去掉已经导入的参数
                if (deptParas.Count > 0)
                {
                    for (int i = sysParas.Count - 1; i >= 0; i--)
                    {
                        if (deptParas.Contains(sysParas[i].ParaCode))
                        {
                            sysParas.Remove(sysParas[i]);
                        }
                    }
                }
                Log.Information(sysParas.Count.ToString());

                //查询模块
                var array = sysParas.Select(s => s.ModuleCode).Distinct().ToArray();

                var moduleList = await _icuParaModuleRepository.Where(s => array.Contains(s.ModuleCode) && s.ValidState == 1).OrderBy(x => x.SortNum)
                    .Select(s => new ModuleListDto() { ParaCode = s.ModuleCode, ParaName = s.ModuleName }).ToListAsync();

                foreach (ModuleListDto moduleListDto in moduleList)
                {
                    moduleListDto.paraItemListDtos = sysParas.Where(x => x.ModuleCode == moduleListDto.ParaCode).OrderBy(x => x.SortNum)
                            .Select(x => new ParaItemListDto { ParaCode = x.ParaCode, ParaName = x.ParaName }).ToList();
                }

                return JsonResult<List<ModuleListDto>>.Ok(data: moduleList);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<List<ModuleListDto>>.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 批量导入参数
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="groupName">导管(管道属性、管道观察),皮肤(皮肤属性、皮肤观察)观察项和出入量传null</param>
        /// <param name="paraCodes">选择的参数列表</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SaveParaItemList([Required] string deptCode, [Required] string moduleCode, string groupName, List<string> paraCodes)
        {
            try
            {
                //查询所有参数
                var propers = _icuParaItemRepository
                    .Where(s => paraCodes.Contains(s.ParaCode) && s.DeptCode == "system" && s.ValidState == 1)
                    .Select(s => s.ColorParaCode)
                    .Concat(_icuParaItemRepository
                    .Where(s => paraCodes.Contains(s.ParaCode) && s.DeptCode == "system" && s.ValidState == 1)
                    .Select(s => s.PropertyParaCode)).ToList();

                paraCodes = paraCodes.Union(propers).ToList();

                //查询要导入的参数列表
                var paraItems = await _icuParaItemRepository.Where(s => paraCodes.Contains(s.ParaCode) && s.DeptCode == "system" && s.ValidState == 1).ToListAsync();

                //查询要导入的参数字典列表
                var dicts = await _dictRepository.Where(s => paraCodes.Contains(s.ParaCode) && s.DeptCode == "system" && s.ValidState == 1).ToListAsync();

                //获取排序最大值
                var sortMax = await _icuParaItemRepository.Where(s => s.DeptCode == deptCode && s.ModuleCode == moduleCode && s.ValidState == 1)
                    .WhereIf(!string.IsNullOrWhiteSpace(groupName), s => s.GroupName == groupName)
                    .OrderByDescending(s => s.SortNum).Select(s => s.SortNum)?.FirstOrDefaultAsync();

                //映射
                var paraItemDtos = ObjectMapper.Map<List<IcuParaItem>, List<IcuParaItemDto>>(paraItems);
                foreach (IcuParaItemDto paraItemDto in paraItemDtos)
                {
                    paraItemDto.Id = _guidGenerator.Create();
                    paraItemDto.DeptCode = deptCode;
                    paraItemDto.ModuleCode = moduleCode;
                    paraItemDto.GroupName = groupName;
                    paraItemDto.SortNum = sortMax + 10;
                    sortMax += 10;
                }
                //映射
                var icuParaItems = ObjectMapper.Map<List<IcuParaItemDto>, List<IcuParaItem>>(paraItemDtos);

                //参数下拉框字典
                var dictDtos = ObjectMapper.Map<List<Dict>, List<DictDto>>(dicts);
                foreach (DictDto dictDto in dictDtos)
                {
                    dictDto.Id = _guidGenerator.Create();
                    dictDto.DeptCode = deptCode;
                    dictDto.ModuleCode = moduleCode;
                }
                var dictItems = ObjectMapper.Map<List<DictDto>, List<Dict>>(dictDtos);

                if (icuParaItems.Any() || dictItems.Any())
                {
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
        /// 修改参数排序
        /// </summary>
        /// <param name="icuParaItemDtos"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<JsonResult> UpdateParaItemSort(List<CreateUpdateIcuParaItemDto> icuParaItemDtos)
        {
            try
            {
                //定义需要修改的列表
                List<IcuParaItem> icuParaItems = new List<IcuParaItem>();

                //查询所有需要排序的参数列表
                var guids = icuParaItemDtos.Select(s => s.Id).ToArray();
                var paraItems = await _icuParaItemRepository.Where(s => guids.Contains(s.Id)).AsNoTracking().ToListAsync();

                //排序
                foreach (CreateUpdateIcuParaItemDto icuParaItemDto in icuParaItemDtos)
                {
                    var idx = icuParaItemDtos.IndexOf(icuParaItemDto);
                    icuParaItemDto.SortNum = (idx + 1) * 10;

                    var finder = paraItems.Find(s => s.Id == icuParaItemDto.Id);
                    //映射
                    icuParaItems.Add(ObjectMapper.Map(icuParaItemDto, finder));
                }

                if (icuParaItems.Any())
                {
                    await _icuParaItemRepository.UpdateRangeAsync(icuParaItems);
                    return JsonResult.Ok();
                }
                else
                {
                    return JsonResult.DataNotFound(msg: "暂无数据！");
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 删除一条参数(删除系统项目、观察项、出入量、导管、皮肤、ECMO血液净化、PICCO)
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<JsonResult> DeleteParaItemInfo(Guid guid)
        {
            try
            {
                var finder = guid == Guid.Empty ? null : await _icuParaItemRepository.FindAsync(a => a.Id == guid && a.ValidState == 1);

                if (finder != null)
                {
                    //查询参数字典
                    var dicts = await _dictRepository.Where(s => s.ParaCode == finder.ParaCode && s.ModuleCode == finder.ModuleCode && s.ValidState == 1).ToListAsync();
                    if (finder.DeptCode == "system")
                    {
                        var icuParaItems = await _icuParaItemRepository.Where(s => s.ParaCode == finder.ParaCode
                            && s.DeptCode != "system").CountAsync();

                        //查询观察项和出入量参数是否有使用 TODO:XUWEI
                        //var icuPatientParas = await _icuPatientParaRepository.Where(s => s.ParaCode == finder.ParaCode).CountAsync();

                        //if (icuParaItems > 0 || icuPatientParas > 0)
                        //{
                        //    return JsonResult.Ok(msg: "该项目已经使用，不能删除！");
                        //}
                        //else
                        //{
                        //删除一条参数
                        finder.ValidState = 0;
                        await _icuParaItemRepository.UpdateAsync(finder);

                        //删除参数字典列表
                        foreach (Dict dict in dicts)
                        {
                            dict.ValidState = 0;
                        }
                        if (dicts.Any())
                        {
                            await _dictRepository.UpdateRangeAsync(dicts);
                        }
                        return JsonResult.Ok(msg: "删除成功！");
                        //}
                    }
                    else
                    {
                        finder.ValidState = 0;
                        await _icuParaItemRepository.UpdateAsync(finder);

                        //删除参数字典
                        foreach (Dict dict in dicts)
                        {
                            dict.ValidState = 0;
                        }
                        if (dicts.Any())
                        {
                            await _dictRepository.UpdateRangeAsync(dicts);
                        }
                        return JsonResult.Ok(msg: "删除成功！");
                        //}
                    }
                }
                else
                {
                    return JsonResult.DataNotFound(msg: "数据不存在！");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 批量删除(仅观察项和出入量可以批量删除)(未判断模块，暂不使用)
        /// </summary>
        /// <param name="guids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> DeleteParaItemList(List<Guid> guids)
        {
            try
            {
                //定义批量删除
                List<IcuParaItem> icuParaItems = new List<IcuParaItem>();
                List<Dict> dicts = new List<Dict>();
                string message = string.Empty;

                foreach (Guid guid in guids)
                {
                    var finder = guid == Guid.Empty ? null : _icuParaItemRepository.Where(a => a.Id == guid && a.ValidState == 1).AsNoTracking().FirstOrDefault();

                    if (finder != null)
                    {
                        //查询参数字典
                        var dictItems = await _dictRepository.Where(s => s.ParaCode == finder.ParaCode && s.ModuleCode == finder.ModuleCode && s.ValidState == 1).AsNoTracking().ToListAsync();
                        //查询是否有患者使用过参数 TODO:XUWEI
                        //var icuPatientParas = await _icuPatientParaRepository.Where(s => s.ParaCode == finder.ParaCode && s.DeptCode == finder.DeptCode).CountAsync();
                        //if (icuPatientParas > 0)
                        //{
                        //    message += finder.ParaName + "，";
                        //}
                        //else
                        //{
                            finder.ValidState = 0;
                            icuParaItems.Add(finder);

                            //删除参数字典
                            foreach (Dict dict in dictItems)
                            {
                                dict.ValidState = 0;
                            }
                            dicts.AddRange(dictItems);
                        //}
                    }
                }
                if (string.IsNullOrWhiteSpace(message))
                {
                    //删除参数列表
                    await _icuParaItemRepository.UpdateRangeAsync(icuParaItems);
                    //删除参数字典列表
                    if (dicts.Any())
                    {
                        await _dictRepository.UpdateRangeAsync(dicts);
                    }
                    return JsonResult.Ok(msg: "删除成功！");
                }
                else
                {
                    return JsonResult.Ok(msg: message + "已经有患者在使用，不能删除，请选择停用！");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }


        #endregion

        #region 参数字典项目
        /// <summary>
        /// 根据模块代码、参数代码查询参数字典列表
        /// </summary>
        /// <param name="ModuleCode">模块代码</param>
        /// <param name="ParaCode">参数代码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<DictDto>>> SelectDictList(string ModuleCode, string ParaCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ModuleCode) || string.IsNullOrWhiteSpace(ParaCode))
                {
                    return JsonResult<List<DictDto>>.RequestParamsIsNull(msg: "请输入模块代码、参数代码！");
                }

                var dicts = await _dictRepository.Where(s => s.ModuleCode == ModuleCode && s.ParaCode == ParaCode && s.ValidState == 1)
                    .OrderBy(s => s.SortNum).ToListAsync();


                var dictDtos = ObjectMapper.Map<List<Dict>, List<DictDto>>(dicts);
                return JsonResult<List<DictDto>>.Ok(data: dictDtos);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<List<DictDto>>.Fail(msg: ex.Message);
            }
        }
        #endregion

    
    }
}