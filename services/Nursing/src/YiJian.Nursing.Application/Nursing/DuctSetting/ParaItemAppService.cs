using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Guids;
using YiJian.Nursing.Settings;

namespace YiJian.Nursing
{
    /// <summary>
    /// 表:护理项目表 API
    /// </summary>
    [NonUnify]
    [Authorize]
    public class ParaItemAppService : NursingAppService, IParaItemAppService
    {
        private readonly IParaItemRepository _paraItemRepository;
        private readonly IDictRepository _dictRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IParaModuleRepository _paraModuleRepository;

        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraItemRepository"></param>
        /// <param name="dictRepository"></param>
        /// <param name="guidGenerator"></param>
        /// <param name="paraModuleRepository"></param>
        public ParaItemAppService(IParaItemRepository paraItemRepository
            , IDictRepository dictRepository
            , IGuidGenerator guidGenerator
            , IParaModuleRepository paraModuleRepository)
        {
            _paraItemRepository = paraItemRepository;
            _dictRepository = dictRepository;
            _guidGenerator = guidGenerator;
            _paraModuleRepository = paraModuleRepository;
        }
        #endregion constructor


        /// <summary>
        /// 新增或修改系统项目
        /// </summary> 
        /// <param name="itemDto"></param>
        /// <returns></returns>
        public async Task<JsonResult> SaveParaItemInfoAsync(ParaItemUpdate itemDto)
        {
            if (string.IsNullOrWhiteSpace(itemDto.ParaName))
            {
                return JsonResult.RequestParamsIsNull(msg: "请输入参数名称！");
            }

            try
            {
                //获取关联项目
                var properties = await (await _paraItemRepository.GetQueryableAsync()).Where(s => s.ModuleCode == itemDto.ModuleCode).ToListAsync();

                //获取排序最大值
                var sortMax = properties.OrderByDescending(s => s.Sort).Select(s => s.Sort).FirstOrDefault();

                var finder = itemDto.Id == Guid.Empty
                    ? null
                    : properties.Find(a => a.Id == itemDto.Id);

                if (finder != null)
                {
                    //系统项目要同步更新科室项目
                    if (finder.DeptCode == "system")
                    {
                        var paraItems = await (await _paraItemRepository.GetQueryableAsync()).Where(x => x.ParaCode == finder.ParaCode).ToListAsync();
                        foreach (var icuParaItem in paraItems)
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

                        //同步科室项目
                        await _paraItemRepository.UpdateManyAsync(paraItems);
                    }

                    if (itemDto.ModuleCode != null) finder.ModuleCode = itemDto.ModuleCode;
                    if (itemDto.DisplayName != null) finder.DisplayName = itemDto.DisplayName;
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

                    if (itemDto.Sort != 0) finder.Sort = itemDto.Sort;
                    if (itemDto.IsEnable != null && itemDto.IsEnable.HasValue)
                    {
                        finder.IsEnable = (bool)itemDto.IsEnable;
                    }

                    //换分组重新排序在最末尾
                    if (itemDto.ModuleCode != finder.ModuleCode)
                    {
                        finder.Sort = sortMax + 10;
                    }


                    //新增参数字典
                    List<Dict> newDicts = new List<Dict>();
                    List<Dict> upDicts = new List<Dict>();

                    if (itemDto.CreateUpdateDictDtos != null)
                    {
                        if (itemDto.CreateUpdateDictDtos.Any())
                        {
                            //获取最大编码
                            int maxCode = await (await _dictRepository.GetQueryableAsync()).MaxAsync(s => Convert.ToInt32(s.DictCode)) + 1;
                            foreach (var dictDto in itemDto.CreateUpdateDictDtos)
                            {
                                var doctrine = dictDto.Id == Guid.Empty
                                    ? null
                                    : await _dictRepository.FindAsync(s => s.Id == dictDto.Id);
                                if (doctrine != null)
                                {
                                    doctrine.ModuleCode = dictDto.ModuleCode;
                                    doctrine.DictValue = dictDto.DictValue;
                                    doctrine.DictDesc = dictDto.DictDesc;
                                    doctrine.IsDefault = dictDto.IsDefault;
                                    upDicts.Add(doctrine);
                                }
                                else
                                {
                                    var dict = new Dict(id: _guidGenerator.Create(),
                                        paraCode: itemDto.ParaCode, // 参数代码
                                        paraName: itemDto.ParaName, // 参数名称
                                        dictCode: maxCode.ToString(), // 字典代码
                                        dictValue: dictDto.DictValue, // 字典值
                                        dictDesc: dictDto.DictDesc, // 字典值说明
                                        parentId: "", // 上级代码
                                        dictStandard: dictDto.DictStandard, // 字典标准（国标、自定义）
                                        hisCode: dictDto.HisCode, // HIS对照代码
                                        hisName: dictDto.HisName, // HIS对照
                                        deptCode: finder.DeptCode,
                                        moduleCode: itemDto.ModuleCode, // 模块代码
                                        sort: itemDto.CreateUpdateDictDtos.IndexOf(dictDto) + 1, // 排序
                                        isDefault: dictDto.IsDefault, // 是否默认
                                        isEnable: true // 是否启用
                                    );
                                    newDicts.Add(dict);
                                    maxCode++;
                                }

                                //修改默认值
                                if (dictDto.IsDefault) finder.DataSource = dictDto.DictValue;
                            }

                            if (upDicts.Any())
                            {
                                await _dictRepository.UpdateManyAsync(upDicts);
                            }

                            if (newDicts.Any())
                            {
                                await _dictRepository.InsertManyAsync(newDicts);
                            }

                            //删除参数
                            List<Dict> deDicts = new List<Dict>();
                            var dicts = await (await _dictRepository.GetQueryableAsync()).Where(s =>
                                s.ModuleCode == finder.ModuleCode
                                && s.ParaCode == finder.ParaCode).ToListAsync();
                            foreach (Dict dict in dicts)
                            {
                                if (!upDicts.Contains(dict) && !newDicts.Contains(dict))
                                {
                                    deDicts.Add(dict);
                                }
                            }

                            if (deDicts.Any())
                            {
                                await _dictRepository.DeleteManyAsync(deDicts);
                            }
                        }

                        if (!itemDto.CreateUpdateDictDtos.Any())
                        {
                            var dicts = await (await _dictRepository.GetQueryableAsync()).Where(s => s.ModuleCode == finder.ModuleCode && s.ParaCode == finder.ParaCode).ToListAsync();
                            if (dicts.Any())
                            {
                                await _dictRepository.DeleteManyAsync(dicts);
                            }
                        }

                        //修改
                        await _paraItemRepository.UpdateAsync(finder);
                    }

                    return JsonResult.Ok();
                }
                else //新增参数到末尾
                {
                    //判断是否已有同名参数
                    int count = await (await _paraItemRepository.GetQueryableAsync()).Where(x => x.ParaName == itemDto.ParaName && x.DeptCode == "system").CountAsync();
                    if (count > 0) return JsonResult.RequestParamsIsNull(msg: "已添加相同名称参数！");

                    //获取最大编码
                    int max = await (await _paraItemRepository.GetQueryableAsync()).MaxAsync(s => Convert.ToInt32(s.ParaCode));

                    itemDto.Id = _guidGenerator.Create();
                    itemDto.ParaCode = (max + 1).ToString();
                    itemDto.ColorParaName = properties.Where(s => s.ParaCode == itemDto.ColorParaCode)
                        .Select(s => s.ParaName).FirstOrDefault();
                    itemDto.PropertyParaName = properties.Where(s => s.ParaCode == itemDto.PropertyParaCode)
                        .Select(s => s.ParaName).FirstOrDefault();
                    ParaItem paraItem = ObjectMapper.Map<ParaItemUpdate, ParaItem>(itemDto);
                    paraItem.Sort = sortMax + 10;
                    paraItem.IsEnable = true;

                    //新增参数字典
                    List<Dict> newDicts = new List<Dict>();

                    if (itemDto.CreateUpdateDictDtos != null)
                    {
                        //获取最大编码
                        var maxDict = await (await _dictRepository.GetQueryableAsync()).MaxAsync(s => Convert.ToInt32(s.DictCode)) + 1;

                        foreach (var dictDto in itemDto.CreateUpdateDictDtos)
                        {
                            dictDto.Id = _guidGenerator.Create();
                            dictDto.ParaCode = itemDto.ParaCode; //参数代码
                            dictDto.DictCode = maxDict.ToString();
                            Dict dict = ObjectMapper.Map<DictUpdate, Dict>(dictDto);
                            dict.Sort = itemDto.CreateUpdateDictDtos.IndexOf(dictDto) + 1;
                            dict.DeptCode = paraItem.DeptCode;
                            dict.IsEnable = true;
                            newDicts.Add(dict);
                            maxDict++;
                        }
                    }

                    //新增
                    await _paraItemRepository.InsertAsync(paraItem);
                    if (newDicts.Any())
                    {
                        await _dictRepository.InsertManyAsync(newDicts);
                    }

                    return JsonResult.Ok();
                }
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 根据条件获取参数项目列表
        /// </summary>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="moduleType">模块类型:(CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO)</param>
        /// <param name="query">项目名称</param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public async Task<JsonResult<List<ParaItemData>>> GetParaItemListAsync(string moduleCode, string moduleType, string query, string deptCode)
        {
            //如果不是系统参数，模块类型不能为空
            if (string.IsNullOrWhiteSpace(moduleType))
            {
                return JsonResult<List<ParaItemData>>.RequestParamsIsNull(msg: "请传入模块类型！");
            }

            try
            {
                //多条件拼接扩展
                Expression<Func<ParaModule, bool>> predicatePM = x => true;

                if (!string.IsNullOrWhiteSpace(moduleType))
                {
                    predicatePM = predicatePM.And(s => s.ModuleType == moduleType);
                }

                if (!deptCode.IsNullOrWhiteSpace())
                {
                    predicatePM = predicatePM.And(s => s.DeptCode == deptCode);
                }

                //查询参数
                Expression<Func<ParaItem, bool>> predicatePI = x => true;

                if (!string.IsNullOrWhiteSpace(moduleCode))
                {
                    predicatePI = predicatePI.And(s => s.ModuleCode == moduleCode);
                }

                if (!string.IsNullOrWhiteSpace(query))
                {
                    predicatePI = predicatePI.And(s => s.ParaName.Contains(query));
                }

                var paraItemList = await (from s in (await _paraModuleRepository.GetQueryableAsync()).Where(predicatePM)
                                          join c in (await _paraItemRepository.GetQueryableAsync()).Where(predicatePI) on s.ModuleCode equals c.ModuleCode
                                          orderby c.GroupName descending, c.Sort
                                          select c).ToListAsync();


                var paraItemDtoList = ObjectMapper.Map<List<ParaItem>, List<ParaItemData>>(paraItemList);
                return JsonResult<List<ParaItemData>>.Ok(data: paraItemDtoList);
            }
            catch (Exception ex)
            {
                return JsonResult<List<ParaItemData>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 批量导入参数
        /// </summary>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="groupName">导管(管道属性、管道观察),皮肤(皮肤属性、皮肤观察)观察项和出入量传null</param>
        /// <param name="paraCodes">选择的参数列表</param>
        /// <returns></returns>
        public async Task<JsonResult> SaveParaItemListAsync([Required] string moduleCode,
            [Required] string groupName, List<string> paraCodes)
        {
            try
            {
                //查询所有参数
                var paraItem = await (await _paraItemRepository.GetQueryableAsync()).Where(s => s.ModuleCode == moduleCode && s.GroupName == groupName).ToListAsync();

                var intersectCode = paraItem.Select(x => x.ParaCode).Intersect(paraCodes);
                if (intersectCode.Any())
                {
                    var paraNames = paraItem.Where(x => intersectCode.Contains(x.ParaCode)).Select(x => x.ParaName);
                    return JsonResult.Fail(msg: string.Join(",", paraNames) + "已存在");
                }

                //查询要导入的参数列表
                var paraItems = await (await _paraItemRepository.GetQueryableAsync())
                    .Where(s => paraCodes.Contains(s.ParaCode) && s.DeptCode == "system")
                    .ToListAsync();
                //查询要导入的参数字典列表
                var dicts = await (await _dictRepository.GetQueryableAsync()).Where(s => paraCodes.Contains(s.ParaCode) && s.DeptCode == "system").ToListAsync();

                //获取排序最大值
                var sortMax = await (await _paraItemRepository.GetQueryableAsync()).Where(s => s.ModuleCode == moduleCode)
                    .Where(s => s.GroupName == groupName)
                    .OrderByDescending(s => s.Sort).Select(s => s.Sort)?.FirstOrDefaultAsync();

                //映射
                var paraItemDtos = ObjectMapper.Map<List<ParaItem>, List<ParaItemData>>(paraItems);
                foreach (var paraItemDto in paraItemDtos)
                {
                    paraItemDto.Id = _guidGenerator.Create();
                    paraItemDto.DeptCode = "";
                    paraItemDto.ModuleCode = moduleCode;
                    paraItemDto.GroupName = groupName;
                    paraItemDto.Sort = sortMax + 10;
                    sortMax += 10;
                }

                //映射
                var icuParaItems = ObjectMapper.Map<List<ParaItemData>, List<ParaItem>>(paraItemDtos);

                //参数下拉框字典
                var dictDtos = ObjectMapper.Map<List<Dict>, List<DictData>>(dicts);
                foreach (var dictDto in dictDtos)
                {
                    dictDto.Id = _guidGenerator.Create();
                    dictDto.DeptCode = "";
                    dictDto.ModuleCode = moduleCode;
                }

                var dictItems = ObjectMapper.Map<List<DictData>, List<Dict>>(dictDtos);

                if (icuParaItems.Any())
                {
                    await _paraItemRepository.InsertManyAsync(icuParaItems);
                }

                if (dictItems.Any())
                {
                    await _dictRepository.InsertManyAsync(dictItems);
                }

                return JsonResult.Ok();
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        #region Delete

        /// <summary>
        /// 删除一条参数(删除系统项目、观察项、出入量、导管、皮肤、ECMO血液净化、PICCO)
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task<JsonResult> DeleteParaItemInfoAsync(Guid guid)
        {
            try
            {
                var finder = guid == Guid.Empty
                    ? null
                    : await _paraItemRepository.FindAsync(a => a.Id == guid);

                if (finder != null)
                {
                    // //查询参数字典
                    var dicts = await (await _dictRepository.GetQueryableAsync()).Where(s =>
                            s.ParaCode == finder.ParaCode && s.ModuleCode == finder.ModuleCode)
                        .ToListAsync();
                    //
                    //     //查询导管参数是否有使用
                    //     var canulas =
                    //         await (from s in _icuNursingCanulaRepository.Where(x => x.ModuleCode == finder.ModuleCode)
                    //                 join c in _canulaDynamicRepository.Where(s => s.ParaCode == finder.ParaCode) on s.Id
                    //                     equals c.CanulaId
                    //                 select c)
                    //             .CountAsync();
                    //     var canulaDynamics =
                    //         await (from s in _icuNursingCanulaRepository.Where(x => x.ModuleCode == finder.ModuleCode)
                    //                 join c in _icuCanulaRepository on s.Id equals c.CanulaId
                    //                 join d in _canulaDynamicRepository.Where(s => s.ParaCode == finder.ParaCode) on c.Id
                    //                     equals d.CanulaId
                    //                 select d)
                    //             .CountAsync();
                    //
                    //
                    //     if (icuPatientParas > 0 || canulas > 0 || canulaDynamics > 0)
                    //     {
                    //         return JsonResult.Ok(msg: "该项目已经有患者在使用，不能删除，请选择停用！");
                    //     }
                    //     else
                    //     {  
                    //删除一条参数
                    await _paraItemRepository.DeleteAsync(finder);
                    if (dicts.Any())
                    {
                        await _dictRepository.DeleteManyAsync(dicts);
                    }

                    return JsonResult.Ok(msg: "删除成功！");
                    // }
                }
                else
                {
                    return JsonResult.DataNotFound(msg: "数据不存在！");
                }
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        #endregion Delete
    }
}