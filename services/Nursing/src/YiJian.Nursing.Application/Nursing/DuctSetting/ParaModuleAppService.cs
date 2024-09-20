using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using YiJian.Nursing.Settings;

namespace YiJian.Nursing
{
    /// <summary>
    /// 表:模块参数 API
    /// </summary>
    [NonUnify]
    [Authorize]
    public class ParaModuleAppService : NursingAppService, IParaModuleAppService
    {
        private readonly ParaModuleManager _paraModuleManager;
        private readonly IParaModuleRepository _paraModuleRepository;
        private readonly IParaItemRepository _paraItemRepository;
        private readonly IDictRepository _dictRepository;

        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraModuleRepository"></param>
        /// <param name="paraModuleManager"></param>
        /// <param name="paraItemRepository"></param>
        /// <param name="dictRepository"></param>
        public ParaModuleAppService(IParaModuleRepository paraModuleRepository
            , ParaModuleManager paraModuleManager
            , IParaItemRepository paraItemRepository
            , IDictRepository dictRepository)
        {
            _paraModuleRepository = paraModuleRepository;
            _paraModuleManager = paraModuleManager;
            _paraItemRepository = paraItemRepository;
            _dictRepository = dictRepository;
        }

        #endregion constructor

        #region Save

        /// <summary>
        /// 新增或修改模块参数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JsonResult> SaveParaModuleInfoAsync(ParaModuleUpdate input)
        {
            if (input.Id == Guid.Empty)
            {
                //获取最大编码
                int sort = input.Sort;
                if (sort <= 0)
                {
                    int maxSort = await (await _paraModuleRepository.GetQueryableAsync()).MaxAsync(x => x.Sort);
                    sort = maxSort + 1;
                }

                var newModuleCode = await (await _paraModuleRepository.GetQueryableAsync()).MaxAsync(s => Convert.ToInt32(s.ModuleCode));
                await _paraModuleManager.CreateAsync((newModuleCode + 1).ToString(), input.ModuleName,
                    input.DisplayName, input.DeptCode, input.ModuleType, input.IsBloodFlow, input.Py, sort,
                    input.IsEnable);
                return JsonResult.Ok();
            }

            var para = await _paraModuleRepository.GetAsync(input.Id);
            if (para == null)
            {
                throw new BusinessException(message: "数据不存在");
            }

            para.Modify(input.ModuleName,
                input.DisplayName, input.IsBloodFlow, input.Py, input.Sort,
                input.IsEnable);
            await _paraModuleRepository.UpdateAsync(para);
            return JsonResult.Ok();
        }

        #endregion Create


        #region Get

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ParaModuleData> GetAsync(Guid id)
        {
            var paraModule = await _paraModuleRepository.GetAsync(id);

            return ObjectMapper.Map<ParaModule, ParaModuleData>(paraModule);
        }

        #endregion Get

        #region Delete

        /// <summary>
        /// 删除模块参数(仅用于删除科室项目模块)
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        public async Task<JsonResult> DeleteParaModuleInfoAsync(string moduleCode)
        {
            if (string.IsNullOrWhiteSpace(moduleCode))
            {
                return JsonResult.RequestParamsIsNull(msg: "请输入模块代码！");
            }

            try
            {
                var finder = await _paraModuleRepository.FindAsync(s => s.ModuleCode == moduleCode);
                if (finder != null)
                {
                    var paraItemList = await (await _paraItemRepository.GetQueryableAsync())
                        .Where(s => s.ModuleCode == moduleCode && s.DeptCode != "system")
                        .ToArrayAsync();
                    //定义批量删除
                    List<ParaItem> icuParaItems = new List<ParaItem>();
                    List<Dict> dicts = new List<Dict>();
                    string message = string.Empty;

                    foreach (var icuParaItem in paraItemList)
                    {
                        //查询参数字典
                        var dictItems = await (await _dictRepository.GetQueryableAsync())
                            .Where(s => s.ParaCode == icuParaItem.ParaCode &&
                                        s.ModuleCode == icuParaItem.ModuleCode && s.DeptCode != "system").AsNoTracking()
                            .ToListAsync();
                        //查询是否有患者使用过参数
                        // var icuPatientParas = await _icuPatientParaRepository.Where(s =>
                        //         s.ParaCode == icuParaItem.ParaCode && s.ModuleCode == icuParaItem.ModuleCode)
                        //     .CountAsync();
                        // if (icuPatientParas > 0)
                        // {
                        //     message += icuParaItem.ParaName + "，";
                        // }
                        // else
                        // {
                        icuParaItems.Add(icuParaItem);
                        dicts.AddRange(dictItems);
                        // }
                    }

                    if (string.IsNullOrWhiteSpace(message))
                    {
                        //删除模块
                        await _paraModuleRepository.DeleteAsync(finder);
                        //删除参数列表
                        await _paraItemRepository.DeleteManyAsync(icuParaItems);
                        //删除参数字典列表
                        if (dicts.Any())
                        {
                            await _dictRepository.DeleteManyAsync(dicts);
                        }

                        return JsonResult.Ok(msg: "删除成功！");
                    }

                    return JsonResult.Ok(msg: message + "已经有患者在使用，不能删除，请选择停用！");
                }

                return JsonResult.DataNotFound(msg: "暂无数据！");
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        #endregion Delete

        /// <summary>
        /// 根据条件查询模块参数
        /// </summary>
        /// <param name="moduleType">模块类型:(CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO)</param>
        /// <param name="query">名称</param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        public async Task<JsonResult<List<ParaModuleData>>> GetParaModuleListAsync(string moduleType, string query, int isEnabled = -1)
        {
            //如果不是系统参数，模块类型不能为空
            if (string.IsNullOrWhiteSpace(moduleType))
            {
                return JsonResult<List<ParaModuleData>>.RequestParamsIsNull(msg: "请传入模块类型！");
            }

            try
            {
                var moduleList = await _paraModuleRepository.GetListAsync(moduleType, query);
                var moduleDtoList = ObjectMapper.Map<List<ParaModule>, List<ParaModuleData>>(moduleList
                    .WhereIf(isEnabled != -1, x => x.IsEnable == Convert.ToBoolean(isEnabled))
                    .Where(x => x.DeptCode != "system").ToList());
                return JsonResult<List<ParaModuleData>>.Ok(data: moduleDtoList);
            }
            catch (Exception ex)
            {
                return JsonResult<List<ParaModuleData>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 查询批量导入参数
        /// </summary>
        /// <param name="moduleType">模块类型:(CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO)</param>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<ParaItemListDto>>> SelectModuleListAsync([Required] string moduleType, string moduleCode)
        {
            try
            {
                //查询系统参数
                var sysParas = await (await _paraItemRepository.GetQueryableAsync()).Where(x => x.DeptCode == "system").ToListAsync();

                //查询科室参数
                var deptModules = await _paraModuleRepository.GetListAsync(moduleType, moduleCode: moduleCode);

                var deptParas = await (await _paraItemRepository.GetQueryableAsync()).Where(x => x.ModuleCode == moduleCode).Select(x => x.ParaCode).ToListAsync();

                //去掉已经导入的参数
                if (deptParas.Any())
                {
                    sysParas.RemoveAll(x => deptParas.Contains(x.ParaCode));
                }

                var paraItemListDtos = sysParas.OrderBy(x => x.Sort)
                    .Select(x => new ParaItemListDto { ParaCode = x.ParaCode, ParaName = x.ParaName }).ToList();

                return JsonResult<List<ParaItemListDto>>.Ok(data: paraItemListDtos);
            }
            catch (Exception ex)
            {
                return JsonResult<List<ParaItemListDto>>.Fail(data: ex.Message + ex.StackTrace);
            }
        }
    }
}