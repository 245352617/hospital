using YiJian.BodyParts.Dtos;
using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.IService;
using YiJian.BodyParts.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YiJian.BodyParts.Domain.Shared.Const;
using Volo.Abp.Application.Services;
using Volo.Abp.Guids;
using YiJian.BodyParts.Application.Contracts.Dtos.CanulaSkin;

namespace YiJian.BodyParts.Service
{
    /// <summary>
    /// 患者导管服务
    /// </summary>
    public class PatientCanulaAppService : ApplicationService, IPatientCanulaAppService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGuidGenerator _guidGenerator;
        private IIcuParaItemRepository _icuParaItemRepository;
        private IDictRepository _dictRepository;
        private IIcuParaModuleRepository _icuParaModuleRepository;
        private IIcuDeptScheduleRepository _icuDeptScheduleRepository;
        private IDictCanulaPartRepository _dictCanulaPartRepository;
        private IIcuSysParaRepository _icuSysParaRepository;
        private IIcuNursingEventRepository _icuNursingEvent;
        private IDictSourceRepository _dictSourceRepository;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="guidGenerator"></param>
        /// <param name="icuParaItemRepository">参数</param>
        /// <param name="dictRepository">参数下拉项</param>
        /// <param name="icuParaModuleRepository">模块参数</param>
        /// <param name="dictCanulaPartRepository">人体图字典</param>
        /// <param name="icuSysParaRepository">系统设置</param>
        /// <param name="icuNursingEvent">护理记录</param>
        public PatientCanulaAppService(IHttpContextAccessor httpContextAccessor,
            IGuidGenerator guidGenerator,
            IIcuParaItemRepository icuParaItemRepository,
            IDictRepository dictRepository,
            IIcuParaModuleRepository icuParaModuleRepository,
            IDictCanulaPartRepository dictCanulaPartRepository,
            IIcuSysParaRepository icuSysParaRepository,
            IIcuNursingEventRepository icuNursingEvent,
            IDictSourceRepository dictSourceRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _guidGenerator = guidGenerator;
            _icuParaItemRepository = icuParaItemRepository;
            _dictRepository = dictRepository;
            _icuParaModuleRepository = icuParaModuleRepository;
            _dictCanulaPartRepository = dictCanulaPartRepository;
            _icuSysParaRepository = icuSysParaRepository;
            _icuNursingEvent = icuNursingEvent;
            _dictSourceRepository = dictSourceRepository;
        }

        #region 人体图

        /// <summary>
        /// 查询当前科室是否展示人体图
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> SelectShowHumanBody([Required] string deptCode)
        {
            try
            {
                string paraValue = await _icuSysParaRepository.GetParaValue("D", deptCode, "HumanBody_Show");
                return JsonResult.Ok(data: paraValue);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取人体图部位分布列表
        /// </summary>
        /// <param name="bodyType">导管：1，皮肤：2</param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<DictCanulaPartDto>>> DictCanulaPartList(int bodyType, string deptCode)
        {
            try
            {
                string moduleType = bodyType == 1 ? "CANULA" : "SKIN";
                var paraModule = await _icuParaModuleRepository.Where(x => x.ModuleType == moduleType && x.ValidState == 1).ToListAsync();
                List<string> modules = paraModule.Select(x => x.ModuleCode).ToList();

                var canulaParts = await _dictCanulaPartRepository.Where(x => x.DeptCode == deptCode && modules.Contains(x.ModuleCode) && x.IsDeleted == false)
                    .OrderBy(x => x.SortNum).ToListAsync();

                var partDtos = ObjectMapper.Map<List<DictCanulaPart>, List<DictCanulaPartDto>>(canulaParts);

                partDtos.ForEach(x => x.ModuleName = paraModule.Find(s => s.ModuleCode == x.ModuleCode).ModuleName);

                return JsonResult<List<DictCanulaPartDto>>.Ok(data: partDtos);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return JsonResult<List<DictCanulaPartDto>>.Fail(msg: $"系统异常，请联系管理员！");
            }
        }

        /// <summary>
        /// 获取拔管原因
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> DictCanulaReason()
        {
            try
            {
                var CanulaBgyy = await _dictSourceRepository.Where(x => x.ModuleCode == "Canula_bgyy" && x.IsEnable == true).OrderBy(x => x.SortNum)
                    .Select(x => new { x.ParaName }).ToListAsync();

                return JsonResult.Ok(data: CanulaBgyy);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return JsonResult.Fail(msg: $"系统异常，请联系管理员！");
            }
        }


        /// <summary>
        /// 新增或编辑部位
        /// </summary>
        /// <param name="dictCanula"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CreateCanulaPartInfo(DictCanulaPartDto dictCanula)
        {
            try
            {
                DictCanulaPart dictCanulaPart;
                DictCanulaPart finder = dictCanula.Id == Guid.Empty ? null : await _dictCanulaPartRepository.FindAsync(x => x.Id == dictCanula.Id && x.IsDeleted == false);

                //判断是否有同名部位
                int count = await _dictCanulaPartRepository.Where(x => x.Id != dictCanula.Id && x.ModuleCode == dictCanula.ModuleCode && x.PartName == dictCanula.PartName && x.IsDeleted == false).CountAsync();
                if (count > 0)
                {
                    return JsonResult.Write(201, "该导管下已有同名部位！", null);
                }

                if (finder != null)
                {
                    dictCanulaPart = dictCanula.NotNullModel(finder);
                    await _dictCanulaPartRepository.UpdateAsync(dictCanulaPart);
                    return JsonResult.Ok();
                }
                else
                {
                    dictCanula.Id = _guidGenerator.Create();
                    dictCanulaPart = ObjectMapper.Map<DictCanulaPartDto, DictCanulaPart>(dictCanula);
                    dictCanulaPart.IsDeleted = false;
                    await _dictCanulaPartRepository.InsertAsync(dictCanulaPart);
                    return JsonResult.Ok();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return JsonResult.Fail(msg: "系统异常，请联系管理员！");
            }
        }

        /// <summary>
        /// 删除一条人体图部位
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<JsonResult> DeleteNoticeInfo([Required] Guid Id)
        {
            try
            {
                var finder = Id == Guid.Empty ? null : await _dictCanulaPartRepository.FindAsync(x => x.Id == Id && x.IsDeleted == false);
                if (finder != null)
                {
                    finder.IsDeleted = true;
                    await _dictCanulaPartRepository.UpdateAsync(finder);
                    return JsonResult.Ok();
                }
                else
                {
                    return JsonResult.DataNotFound();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return JsonResult.Fail(msg: "系统异常，请联系管理员！");
            }
        }

        /// <summary>
        /// 通过身体类型，把一个科室对应的人体图部位同步到另一个科室
        /// </summary>
        /// <param name="bodyType">身体类型 1：</param>
        /// <param name="sourceDeptCode"></param>
        /// <param name="targetDeptCode"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SyncCanulaPartAsync(int bodyType, string sourceDeptCode, string targetDeptCode)
        {
            if (bodyType.ToString().IsNullOrWhiteSpace() || sourceDeptCode.IsNullOrWhiteSpace() || targetDeptCode.IsNullOrWhiteSpace())
            {
                Log.Error($"同步人体图部位失败，参数错误，bodyType、sourceDeptCode和targetDeptCode都不能为空");
                return JsonResult.Fail();
            }

            //获取人体图中要插入的数据
            {
                var moduleType = bodyType == 1 ? "CANULA" : "SKIN";
                //查询源科室对应的所有部位
                var copyList = await (from canula in _dictCanulaPartRepository.AsNoTracking()
                                      join module in _icuParaModuleRepository.AsNoTracking() on new { canula.ModuleCode, canula.DeptCode } equals new { module.ModuleCode, module.DeptCode }
                                      where !canula.IsDeleted && module.ModuleType == moduleType && module.ValidState == 1
                                          && canula.DeptCode == sourceDeptCode
                                      select new CanulaModuleDto
                                      {
                                          ModuleCode = canula.ModuleCode,
                                          ModuleName = module.ModuleName,
                                          PartName = canula.PartName,
                                          PartNumber = canula.PartNumber,
                                          SortNum = canula.SortNum,
                                          IsEnable = canula.IsEnable,
                                          IsDeleted = canula.IsDeleted
                                      }).ToListAsync();


                foreach (var item in copyList)
                {
                    var moduleCode = await _icuParaModuleRepository.GetModuleCodeAsync(targetDeptCode, item.ModuleName, moduleType);
                    //如果对应模块不存在，就直接跳过这条数据
                    if (moduleCode.IsNullOrWhiteSpace())
                    {
                        continue;
                    }
                    var checkIsExist = await _dictCanulaPartRepository.CheckExists(targetDeptCode, moduleCode, item.PartName, item.PartNumber);
                    //如果目标科室不存在对应部位，才更新数据
                    if (!checkIsExist)
                    {
                        var newDictCanula = await _dictCanulaPartRepository.FirstOrDefaultAsync(f => f.DeptCode == targetDeptCode && f.ModuleCode == moduleCode && f.PartName == item.PartName && f.PartNumber == item.PartNumber);
                        var isAdd = newDictCanula == null;//是否新增
                        newDictCanula = newDictCanula ?? new DictCanulaPart(_guidGenerator.Create());

                        newDictCanula.DeptCode = targetDeptCode;
                        newDictCanula.ModuleCode = moduleCode;
                        newDictCanula.PartName = item.PartName;
                        newDictCanula.PartNumber = item.PartNumber;
                        newDictCanula.SortNum = item.SortNum;
                        newDictCanula.IsEnable = item.IsEnable;
                        newDictCanula.IsDeleted = item.IsDeleted;

                        if (isAdd)
                            await _dictCanulaPartRepository.InsertAsync(newDictCanula);
                        else
                            await _dictCanulaPartRepository.UpdateAsync(newDictCanula);

                        //addedCanulaList.Add(newDictCanula);//不需要用这些临时变量了，如果需要更新科室项目维护中的数据，需要使用
                    }
                }
            }

            /* 
             * 当前需求7105，已确认只处理部位表的数据，科室项目等关联表不管
             * 以下代码是同步科室项目维护中的数据-只写了处理流程，未完成全部实现
             */
            {
                //var addedCanulaList = new List<DictCanulaPart>();//人体图中要处理的数据
                //var addedModuleList = new List<IcuParaModule>();//导管类型要处理的数据
                //var addedItemList = new List<IcuParaItem>();//导管对应项目要处理的数据
                //var addedDictList = new List<Dict>();//项目对应参数要处理的数据
                ////获取科室项目-导管/皮肤对应详情  IcuParaModule  IcuParaItem  Dict
                //{
                //    foreach(var item in addedCanulaList) {
                //        var oldModules = _icuParaModuleRepository.Where(p => p.DeptCode == sourceDeptCode && p.ModuleCode == item.ModuleCode);
                //        foreach (var module in oldModules) {
                //            //把表中不存在的模块也复制一份
                //            {

                //            }

                //            var oldItems = _icuParaItemRepository.Where(p => p.DeptCode == sourceDeptCode && p.ModuleCode == item.ModuleCode);
                //            foreach (var tmpItem in oldItems) {
                //                //把表中不存在的项目也复制一份
                //                {

                //                }

                //                var oldDict = _dictRepository.Where(p => p.ModuleCode == module.ModuleCode && p.ParaCode == tmpItem.ParaCode);
                //                foreach(var tmpDict in oldDict) {
                //                    //把表中不存在的字典（选项）也复制一份
                //                }
                //            }
                //        }
                //    }
                //}

                ////获取科室模块中的项目--IcuParaItem
                //{
                //    foreach (var item in addedModuleList) {
                //    }
                //}

                ////事务操作，如果失败就全部回滚
                //{

                //}
            }

            return JsonResult.Ok();
        }


        #endregion 人体图
     
        /// <summary>
        /// 班次时间字典--每个时间对应班次代码
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetScheduleCodeByDeptCode(string deptCode)
        {
            //根据部门查询班次时间
            var scheduleList = _icuDeptScheduleRepository.Where(s => s.DeptCode == deptCode).ToList();
            Dictionary<string, string> dicScheduleCode = new Dictionary<string, string>();
            foreach (IcuDeptSchedule icuDeptSchedule in scheduleList)
            {
                string[] strTime = icuDeptSchedule.Period.Split(',');
                for (int i = 0; i < strTime.Length; i++)
                {
                    //if (strTime[i].Length == 1)
                    //    strTime[i] = "0" + strTime[i];
                    if (!dicScheduleCode.ContainsKey(strTime[i]))
                        dicScheduleCode.Add(strTime[i], icuDeptSchedule.ScheduleCode);
                }
            }
            return dicScheduleCode;
        }

        /// <summary>
        /// 导管记录列表方法
        /// </summary>
        /// <param name="canula"></param>
        /// <returns></returns>
        private static List<CanulaItemDtos> Dynamic(CanulaDto canula)
        {
            List<CanulaItemDtos> canulaItemDtos = new List<CanulaItemDtos>();
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "Id",
                ParaName = "主键",
                ParaValue = canula.Id.ToString(),
            });
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "NurseTime",
                ParaName = "时间",
                ParaValue = canula.NurseTime.ToString("yyyy-MM-dd HH:mm"),
            });
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "ScheduleCode",
                ParaName = "班次",
                ParaValue = canula.ScheduleCode,
            });
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "NurseName",
                ParaName = "记录人",
                ParaValue = canula.NurseName,
            });
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "CanulaLength",
                ParaName = "深度(cm)",
                ParaValue = string.IsNullOrWhiteSpace(canula.CanulaLength) ? null : canula.CanulaLength + "(" + canula.CanulaWay + ")",
            });
            return canulaItemDtos;
        }

        /// <summary>
        /// 导管记录标题方法
        /// </summary>
        /// <returns></returns>
        private static List<CanulaItemDtos> DynamicItem()
        {
            List<CanulaItemDtos> canulaItemDtos = new List<CanulaItemDtos>();
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "Id",
                ParaName = "主键",
            });
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "NurseTime",
                ParaName = "时间",
            });
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "ScheduleCode",
                ParaName = "班次",
            });
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "NurseName",
                ParaName = "记录人",
            });
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "CanulaLength",
                ParaName = "深度(cm)"
            });
            return canulaItemDtos;
        }


        /// <summary>
        /// 三管监测护理记录
        /// </summary>
        private class CanulaNursing
        {
            public DateTime NurseTime;
            public string ParaName;
            public string ParaValue;
            public string NurseName;
        }
    }
}