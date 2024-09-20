using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using YiJian.ECIS;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Etos.NurseExecutes;
using YiJian.Nursing.Settings;

namespace YiJian.Nursing
{
    /// <summary>
    /// 护士导管护理API
    /// </summary>
    [NonUnify]
    [Authorize]
    public class NursingCanulaAppService : NursingAppService, INursingCanulaAppService
    {
        private readonly INursingCanulaRepository _nursingCanulaRepository;
        private readonly IParaModuleRepository _paraModuleRepository;
        private readonly ICanulaPartRepository _canulaPartRepository;
        private readonly ICanulaRepository _canulaRepository;
        private readonly IParaItemRepository _paraItemRepository;
        private readonly ICanulaDynamicRepository _canulaDynamicRepository;
        private readonly IDictRepository _dictRepository;
        private readonly ICapPublisher _capPublisher;

        /// <summary>
        /// 护士护理
        /// </summary>
        /// <param name="nursingCanulaRepository"></param>
        /// <param name="paraModuleRepository"></param>
        /// <param name="canulaPartRepository"></param>
        /// <param name="canulatRepository"></param>
        /// <param name="paraItemRepository"></param>
        /// <param name="capPublisher"></param>
        /// <param name="canulaDynamicRepository"></param>
        /// <param name="dictRepository"></param>
        public NursingCanulaAppService(INursingCanulaRepository nursingCanulaRepository
            , IParaModuleRepository paraModuleRepository
            , ICanulaPartRepository canulaPartRepository
            , ICanulaRepository canulatRepository
            , IParaItemRepository paraItemRepository
            , ICapPublisher capPublisher
            , ICanulaDynamicRepository canulaDynamicRepository
            , IDictRepository dictRepository)
        {
            _nursingCanulaRepository = nursingCanulaRepository;
            _paraModuleRepository = paraModuleRepository;
            _canulaPartRepository = canulaPartRepository;
            _canulaRepository = canulatRepository;
            _paraItemRepository = paraItemRepository;
            _canulaDynamicRepository = canulaDynamicRepository;
            _dictRepository = dictRepository;
            _capPublisher = capPublisher;
        }

        /// <summary>
        /// 根据条件查询插管列表
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult<List<NursingCanulaDto>>> SelectNursingCanulaListAsync(NursingCanulaInput input)
        {
            try
            {
                //多条件拼接扩展
                Expression<Func<NursingCanula, bool>> predicateNc = s => s.UseFlag == input.UseFlag;

                if (!string.IsNullOrEmpty(input.PI_ID))
                {
                    predicateNc = predicateNc.And(s => s.PI_ID == Guid.Parse(input.PI_ID));
                }

                //插管时间
                if (input.State is 0 && input.StartTime != null)
                {
                    predicateNc = predicateNc.And(s => s.StartTime >= input.StartTime);
                }

                if (input.State is 0 && input.EndTime != null)
                {
                    predicateNc = predicateNc.And(s => s.StartTime <= input.EndTime);
                }

                //拔管时间
                if (input.State is 1 && input.StartTime != null)
                {
                    predicateNc = predicateNc.And(s => s.StopTime >= input.StartTime);
                }

                if (input.State is 1 && input.EndTime != null)
                {
                    predicateNc = predicateNc.And(s => s.StopTime <= input.EndTime);
                }

                var canulaList = await (from s in (await _nursingCanulaRepository.GetQueryableAsync()).Where(predicateNc)
                                        join c in (await _canulaPartRepository.GetQueryableAsync()) on
                                            new { s.ModuleCode, s.CanulaPart } equals new
                                            { c.ModuleCode, CanulaPart = c.PartName } into cc
                                        from c in cc.DefaultIfEmpty()
                                        join e in (await _paraModuleRepository.GetQueryableAsync()) on s.ModuleCode equals e.ModuleCode
                                        select new NursingCanulaDto
                                        {
                                            Id = s.Id,
                                            PI_ID = s.PI_ID,
                                            ModuleCode = s.ModuleCode,
                                            ModuleName = s.ModuleName,
                                            CanulaName = s.CanulaName,
                                            CanulaPart = s.CanulaPart,
                                            StartTime = s.StartTime == null || !s.StartTime.HasValue ? null : s.StartTime,
                                            CanulaNumber = s.CanulaNumber,
                                            CanulaPosition = s.CanulaPosition,
                                            CanulaWay = s.CanulaWay,
                                            CanulaLength = s.CanulaLength,
                                            DoctorId = s.DoctorId,
                                            DoctorName = s.DoctorName,
                                            DrawReason = s.DrawReason,
                                            StopTime = s.StopTime,
                                            PartNumber = string.IsNullOrWhiteSpace(c.PartNumber) ? "33" : c.PartNumber,
                                            DisplayName = e.DisplayName,
                                            CanulaRecord = s.CanulaRecord
                                        }).OrderByDescending(s => s.StartTime).ToListAsync();

                //计算插管天数和持续天数
                foreach (NursingCanulaDto nursingCanulaDto in canulaList)
                {
                    /*插管天数 本次插管开始算起，有插管时间以插管时间为起始时间，插管时间不详以入科时间为起始时间，有拔管时间到拔管时间结束；没有拔管时间的，如果病人在科，计算到当天，如果病人已出科，则以出科时间为拔管时间*/
                    DateTime? startTime = nursingCanulaDto.StartTime;
                    if (startTime == null)
                    {
                        startTime = input.InDeptTime;
                    }

                    DateTime? stopTime = nursingCanulaDto.StopTime;
                    if (stopTime == null)
                    {
                        if (input.VisitStatus == 4) // 在科
                        {
                            // 原代码逻辑中，无拔管时间时，计算公式为：当前时间.Subtract(startTime.Date).Days + 1
                            // 为保持原有逻辑，此处的stopTime应 + 1
                            stopTime = DateTime.Now.AddDays(1);
                        }
                        else
                        {
                            stopTime = input.OutDeptTime;
                        }
                    } // 无拔管时间
                    else // 有拔管时间
                    {
                        if (input.VisitStatus == 6) // 不在科
                        {
                            if (stopTime > input.OutDeptTime)
                            {
                                stopTime = input.OutDeptTime;
                            }
                        }
                    }

                    nursingCanulaDto.Days = stopTime == null ? 0 : stopTime.Value.Subtract(startTime.Value.Date).Days;

                    // 插管当天拔管算持续天数一天，第二天插管持续天数也算一天
                    if (nursingCanulaDto.Days == 0)
                    {
                        nursingCanulaDto.Days = 1;
                    }
                }

                return JsonResult<List<NursingCanulaDto>>.Ok(data: canulaList);
            }
            catch (Exception ex)
            {
                // Log.Error(ex, ex.Message);
                return JsonResult<List<NursingCanulaDto>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 查询管道列表
        /// </summary>
        /// <param name="canulaId">导管列表Id</param>
        /// <param name="state"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<JsonResult<List<CanulaListDto>>> SelectCanulaListAsync(Guid canulaId, int state,
            DateTime? startTime, DateTime? endTime)
        {
            try
            {
                //获取模块代码
                var nursingCanula = await (await _nursingCanulaRepository.GetQueryableAsync()).FirstOrDefaultAsync(x => x.Id == canulaId);
                if (nursingCanula == null)
                {
                    return JsonResult<List<CanulaListDto>>.Ok(data: null);
                }

                string modulecode = nursingCanula.ModuleCode;
                Expression<Func<Canula, bool>> predicateIc = s => s.CanulaId == canulaId;
                //已拔管的默认按自然日查询
                if (nursingCanula.UseFlag == "N")
                {
                    state = 1;
                }

                //按自然日查询
                if (state == 1 && startTime != null)
                {
                    predicateIc = predicateIc.And(s => s.NurseTime >= startTime);
                }

                if (state == 1 && endTime != null)
                {
                    predicateIc = predicateIc.And(s => s.NurseTime <= endTime);
                }

                //定义返回值
                var canulaDtos = new List<CanulaListDto>();

                //获取管道主表
                var canulas = await (await _canulaRepository.GetQueryableAsync()).Where(predicateIc)
                    .OrderByDescending(x => x.NurseTime)
                    .Select(s => new CanulaDto()
                    {
                        Id = s.Id,
                        NurseTime = s.NurseTime,
                        CanulaWay = s.CanulaWay,
                        CanulaLength = s.CanulaLength,
                        NurseName = s.NurseName
                    }).ToListAsync();


                //多条件拼接扩展
                Expression<Func<ParaItem, bool>> predicatePi = s => s.IsEnable
                                                                    && s.ModuleCode == modulecode &&
                                                                    s.GroupName == "管道观察" &&
                                                                    s.DeptCode != "system";
                //生成标题列
                var canulaItems = await (await _paraItemRepository.GetQueryableAsync()).Where(predicatePi).OrderBy(s => s.Sort)
                    .Select(s => new CanulaItemDtos
                    {
                        ParaCode = s.ParaCode,
                        ParaName = s.DisplayName
                    }).ToListAsync();

                canulaDtos.Add(new CanulaListDto() { CanulaItemDto = DynamicItem().Union(canulaItems).ToList() });

                foreach (CanulaDto canula in canulas)
                {
                    //多条件拼接扩展
                    Expression<Func<CanulaDynamic, bool>> predicateCd = s => s.CanulaId == canula.Id;

                    //获取所有管道属性项目
                    var canulaItemDtos = await (from s in (await _paraItemRepository.GetQueryableAsync()).Where(predicatePi)
                                                join c in (await _canulaDynamicRepository.GetQueryableAsync()).Where(predicateCd) on s.ParaCode equals c.ParaCode into cc
                                                from c in cc.DefaultIfEmpty()
                                                orderby s.Sort
                                                select new CanulaItemDtos
                                                {
                                                    ParaCode = s.ParaCode,
                                                    ParaName = s.DisplayName,
                                                    ParaValue = c.ParaValue,
                                                }).ToListAsync();

                    canulaDtos.Add(new CanulaListDto()
                    { CanulaItemDto = Dynamic(canula).Union(canulaItemDtos).ToList() });
                }

                return JsonResult<List<CanulaListDto>>.Ok(data: canulaDtos);
            }
            catch (Exception ex)
            {
                // Log.Error(ex, ex.Message);
                return JsonResult<List<CanulaListDto>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 查询管道属性项
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="pI_ID">患者id</param>
        /// <param name="moduleCode">模块代码</param>
        /// <returns></returns>
        public async Task<JsonResult<NursingCanulaDto>> SelectNursingCanulaInfoAsync([Required] Guid id, [Required] Guid pI_ID, [Required] string moduleCode)
        {
            try
            {
                //获取管道主表
                var query = from s in (await _paraModuleRepository.GetQueryableAsync()).Where(s => s.ModuleCode == moduleCode)
                            join c in (await _nursingCanulaRepository.GetQueryableAsync()).Where(s => s.Id == id && s.UseFlag == "Y")
                                on s.ModuleCode equals c.ModuleCode into cc
                            from c in cc.DefaultIfEmpty()
                            select new NursingCanulaDto()
                            {
                                Id = c == null ? Guid.Empty : c.Id,
                                PI_ID = pI_ID,
                                ModuleCode = s.ModuleCode,
                                ModuleName = s.ModuleName,
                                CanulaName = string.IsNullOrWhiteSpace(c.CanulaName) ? s.ModuleName : c.CanulaName,
                                StartTime = c == null ? null : c.StartTime,
                                CanulaNumber = c == null ? 0 : c.CanulaNumber,
                                CanulaPosition = c == null ? "" : c.CanulaPosition,
                                CanulaWay = c == null ? "" : c.CanulaWay,
                                CanulaLength = c == null ? "" : c.CanulaLength,
                                DoctorId = c == null ? "" : c.DoctorId,
                                DoctorName = c == null ? "" : c.DoctorName,
                                NurseId = c == null ? "" : c.NurseId,
                                NurseName = c == null ? "" : c.NurseName,
                                NurseTime = c == null ? null : c.NurseTime,
                                DrawReason = c == null ? "" : c.DrawReason,
                                StopTime = c == null ? null : c.StopTime,
                                DisplayName = s.DisplayName,
                                CanulaPart = c == null ? null : c.CanulaPart,
                                CanulaRecord = c == null ? null : c.CanulaRecord
                            };

                var nursingCanula = await query.FirstOrDefaultAsync();

                //多条件拼接扩展
                Expression<Func<ParaItem, bool>> predicatePi = s => s.IsEnable && s.ModuleCode == moduleCode && s.GroupName == "管道属性";

                //多条件拼接扩展
                var canulaPart = await (await _canulaPartRepository.GetQueryableAsync()).Where(x => x.ModuleCode == moduleCode).ToListAsync();
                var dict = await (await _dictRepository.GetQueryableAsync()).Where(x => x.ModuleCode == moduleCode).ToListAsync();
                //获取所有管道属性项目
                var canulaItemDtos = await (from s in (await _paraItemRepository.GetQueryableAsync()).Where(predicatePi)
                                            join c in (await _canulaDynamicRepository.GetQueryableAsync()).Where(s => s.CanulaId == nursingCanula.Id)
                                            on s.ParaCode equals c.ParaCode
                                            into cc
                                            from c in cc.DefaultIfEmpty()
                                            orderby s.Sort
                                            select new CanulaItemDto
                                            {
                                                Id = c == null ? Guid.Empty : c.Id,
                                                ParaCode = s.ParaCode,
                                                ParaName = s.DisplayName,
                                                DictFlag = s.DictFlag,
                                                Sort = s.Sort,
                                                GroupName = s.GroupName,
                                                Style = s.Style,
                                                DataSource = string.IsNullOrEmpty(c.ParaValue) ? s.DataSource : c.ParaValue,
                                            }).ToListAsync();

                canulaItemDtos.ForEach(s =>
                {
                    //管道项下拉框字典值
                    s.Dicts = s.DictFlag == "Y"
                        ? canulaPart.OrderBy(x => x.Sort).Select(x => new Dicts
                        {
                            DictId = s.ParaCode,
                            DictName = s.ParaName,
                            DictValue = x.PartName,
                            Sort = x.Sort
                        }).ToList()
                        : dict.Where(x => x.ParaCode == s.ParaCode)
                            .OrderBy(x => x.Sort).Select(x => new Dicts
                            {
                                DictId = x.ParaCode,
                                DictName = x.ParaName,
                                DictCode = x.DictCode,
                                DictValue = x.DictValue,
                                Sort = x.Sort
                            }).ToList();
                });
                nursingCanula.CanulaItemDto = canulaItemDtos;
                return JsonResult<NursingCanulaDto>.Ok(data: nursingCanula);
            }
            catch (Exception ex)
            {
                return JsonResult<NursingCanulaDto>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 新增或修改管道属性
        /// </summary>
        /// <param name="neworupdate">新增：new，修改：update</param>
        /// <param name="nursingCanula"></param>
        /// <returns></returns>
        public async Task<JsonResult> CreateNursingCanulaInfoAsync(string neworupdate, NursingCanulaDto nursingCanula)
        {
            if (neworupdate != "new" && neworupdate != "update")
            {
                return JsonResult.RequestParamsIsNull(msg: "请输入新增、修改标志！");
            }

            if (!nursingCanula.StartTime.HasValue)
            {
                return JsonResult.RequestParamsIsNull(msg: "置管时间不能为空");
            }

            try
            {
                //查询模块列表
                var moduleInfo = await _paraModuleRepository.FindAsync(s => s.ModuleCode == nursingCanula.ModuleCode);
                string modulename = moduleInfo.ModuleName;

                //获取插管部位、插管地点
                string location = null;
                string part = null;
                foreach (var canulaItemDto in nursingCanula.CanulaItemDto)
                {
                    if (canulaItemDto.ParaName == "置管地点")
                    {
                        location = canulaItemDto.DataSource;
                    }

                    if (canulaItemDto.ParaName == "插管部位")
                    {
                        part = canulaItemDto.DataSource;
                    }
                }

                CanulaRecordEto canulaRecordEto = new CanulaRecordEto();
                canulaRecordEto.PiId = nursingCanula.PI_ID;
                canulaRecordEto.EventType = EEventType.Insert;
                canulaRecordEto.CanulaRecord = nursingCanula.CanulaRecord;
                canulaRecordEto.OperateTime = nursingCanula.StartTime.Value;
                canulaRecordEto.Signature = nursingCanula.Signature;

                if (neworupdate == "new") //新建导管
                {
                    var finder = await _nursingCanulaRepository.FindAsync(s => s.PI_ID == nursingCanula.PI_ID
                        && s.ModuleCode == nursingCanula.ModuleCode
                        && s.CanulaPart == part
                        && s.UseFlag == "Y");

                    if (finder == null)
                    {
                        var oldCanula = await (await _nursingCanulaRepository.GetQueryableAsync()).Where(s => s.PI_ID == nursingCanula.PI_ID
                             && s.ModuleCode == nursingCanula.ModuleCode
                             && s.CanulaPart == part).OrderByDescending(s => s.CanulaNumber).FirstOrDefaultAsync();

                        Guid guid = GuidGenerator.Create();
                        var icuNursingCanula = new NursingCanula(guid)
                        {
                            PI_ID = nursingCanula.PI_ID,
                            StartTime = nursingCanula.StartTime,
                            ModuleCode = nursingCanula.ModuleCode,
                            ModuleName = modulename,
                            CanulaName = nursingCanula.CanulaName,
                            CanulaNumber = oldCanula == null ? 1 : oldCanula.CanulaNumber + 1,
                            CanulaPosition = location,
                            CanulaPart = part,
                            CanulaWay = nursingCanula.CanulaWay,
                            CanulaLength = nursingCanula.CanulaLength,
                            DoctorId = nursingCanula.DoctorId,
                            DoctorName = nursingCanula.DoctorName,
                            NurseId = nursingCanula.NurseId,
                            NurseName = nursingCanula.NurseName,
                            NurseTime = DateTime.Now,
                            TubeDrawState = TubeDrawStateEnum.其他,
                            UseFlag = "Y",
                            CanulaRecord = nursingCanula.CanulaRecord
                        };

                        //定义动态属性
                        var createCanulas = new List<CanulaDynamic>();
                        foreach (CanulaItemDto canulaItem in nursingCanula.CanulaItemDto)
                        {
                            createCanulas.Add(new CanulaDynamic(GuidGenerator.Create())
                            {
                                CanulaId = guid,
                                GroupName = canulaItem.GroupName,
                                ParaCode = canulaItem.ParaCode,
                                ParaName = canulaItem.ParaName,
                                ParaValue = canulaItem.DataSource
                            });
                        }

                        await _nursingCanulaRepository.InsertAsync(icuNursingCanula);
                        if (createCanulas.Any()) await _canulaDynamicRepository.InsertManyAsync(createCanulas);
                        canulaRecordEto.NursingCanulaId = icuNursingCanula.Id;
                        await WriteToNursingRecordAsync(canulaRecordEto);

                        return JsonResult.Ok();
                    }

                    return JsonResult.Write(code: 201, msg: "已有在用相同导管！", null);
                }
                else //修改导管
                {
                    if (!nursingCanula.StartTime.HasValue)
                    {
                        return JsonResult.Write(code: 201, msg: "插管时间为空，请重新输入插管时间！", null);
                    }

                    var finder = await _nursingCanulaRepository.FindAsync(s => s.Id == nursingCanula.Id && s.UseFlag == "Y");

                    if (finder == null)
                    {
                        return JsonResult.Write(code: 201, msg: "数据不存在！", null);
                    }

                    //定义未修改前的置管时间、插管部位
                    // DateTime? oldStartTime = finder.StartTime;
                    string oldPart = finder.CanulaPart;
                    // string oldCanulaName = finder.CanulaName;

                    //插管部位有变更
                    NursingCanula canulaPart = null;
                    if (oldPart != part)
                    {
                        //查询相同部位是否有同类型导管
                        canulaPart = await (await _nursingCanulaRepository.GetQueryableAsync()).Where(s => s.PI_ID == nursingCanula.PI_ID
                            && s.ModuleCode == nursingCanula.ModuleCode
                            && s.CanulaPart == part).OrderByDescending(s => s.CanulaNumber).FirstOrDefaultAsync();

                        if (canulaPart != null && canulaPart.UseFlag == "Y")
                        {
                            return JsonResult.Write(code: 201, msg: "相同部位已经有同类型的导管在使用中", null);
                        }

                        if (canulaPart != null && nursingCanula.StartTime < canulaPart.StopTime)
                        {
                            return JsonResult.Write(code: 201, msg: "插管时间小于上一次拔管时间，请重新输入插管时间！", null);
                        }
                    }


                    //修改管道主表信息
                    finder.StartTime = nursingCanula.StartTime;
                    finder.CanulaName = nursingCanula.CanulaName;
                    finder.CanulaPosition = location;
                    if (oldPart != part)
                    {
                        finder.CanulaNumber = canulaPart == null ? 1 : canulaPart.CanulaNumber + 1;
                    }

                    finder.CanulaPart = part;
                    finder.CanulaWay = nursingCanula.CanulaWay;
                    finder.CanulaLength = nursingCanula.CanulaLength;
                    finder.DoctorId = nursingCanula.DoctorId;
                    finder.DoctorName = nursingCanula.DoctorName;
                    finder.NurseId = nursingCanula.NurseId;
                    finder.NurseName = nursingCanula.NurseName;
                    finder.TubeDrawState = TubeDrawStateEnum.其他;
                    finder.CanulaRecord = nursingCanula.CanulaRecord;

                    //定义动态属性
                    var createCanulas = new List<CanulaDynamic>();
                    var updateCanulas = new List<CanulaDynamic>();
                    var canulaDynamics = await (await _canulaDynamicRepository.GetQueryableAsync()).AsNoTracking()
                        .Where(s => s.CanulaId == finder.Id).ToListAsync();
                    foreach (CanulaItemDto canulaItem in nursingCanula.CanulaItemDto)
                    {
                        //新增动态参数
                        if (canulaItem.Id == Guid.Empty)
                        {
                            createCanulas.Add(new CanulaDynamic(GuidGenerator.Create())
                            {
                                CanulaId = finder.Id,
                                GroupName = canulaItem.GroupName,
                                ParaCode = canulaItem.ParaCode,
                                ParaName = canulaItem.ParaName,
                                ParaValue = canulaItem.DataSource
                            });
                        }
                        else
                        {
                            var canulaDynamic = canulaDynamics.FirstOrDefault(s => s.Id == canulaItem.Id);
                            if (canulaDynamic != null)
                            {
                                canulaDynamic.GroupName = canulaItem.GroupName;
                                canulaDynamic.ParaCode = canulaItem.ParaCode;
                                canulaDynamic.ParaName = canulaItem.ParaName;
                                canulaDynamic.ParaValue = canulaItem.DataSource;
                                updateCanulas.Add(canulaDynamic);
                            }
                        }
                    }

                    await _nursingCanulaRepository.UpdateAsync(finder);

                    if (createCanulas.Any())
                    {
                        await _canulaDynamicRepository.InsertManyAsync(createCanulas);
                    }

                    await _canulaDynamicRepository.UpdateManyAsync(updateCanulas);
                    canulaRecordEto.NursingCanulaId = finder.Id;
                    await WriteToNursingRecordAsync(canulaRecordEto);

                    return JsonResult.Ok();
                }
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 同步导管记录到护理记录单
        /// </summary>
        /// <param name="canulaRecordEto"></param>
        /// <returns></returns>
        private async Task WriteToNursingRecordAsync(CanulaRecordEto canulaRecordEto)
        {
            if (canulaRecordEto == null)
            {
                return;
            }

            if (canulaRecordEto.EventType.HasValue && canulaRecordEto.EventType == EEventType.Insert)
            {
                canulaRecordEto.CanulaRecord = "插管：" + canulaRecordEto.CanulaRecord;
            }

            canulaRecordEto.OperateCode = CurrentUser.UserName;
            canulaRecordEto.OperateName = CurrentUser.FindClaimValue("fullName");



            await _capPublisher.PublishAsync("nursingrecord.to.nursingreport", canulaRecordEto);
        }

        /// <summary>
        /// 拔管/换管/取消拔管
        /// </summary>
        /// <param name="inDeptTime"></param>
        /// <param name="nursingCanulaDto"></param>
        /// <param name="state">0拔管，1换管，2取消拔管</param>
        /// <returns></returns>
        public async Task<JsonResult> UpdateNursingCanulaInfoAsync(NursingCanulaDto nursingCanulaDto, DateTime? inDeptTime, TubeDrawStateEnum state = TubeDrawStateEnum.其他)
        {
            if (state == TubeDrawStateEnum.其他)
            {
                return JsonResult.RequestParamsIsNull(msg: "请输入正确的操作状态！");
            }

            try
            {
                var nursingCanula = nursingCanulaDto.Id == Guid.Empty ? null : await _nursingCanulaRepository.FindAsync(s => s.Id == nursingCanulaDto.Id);

                if (nursingCanula == null)
                {
                    return JsonResult.DataNotFound(msg: "要修改的导管不存在！");
                }

                if (state == TubeDrawStateEnum.拔管 || state == TubeDrawStateEnum.换管)
                {
                    if (nursingCanula.StartTime != null)
                    {
                        //拔管时间小于或等于插管时间
                        if (nursingCanulaDto.StopTime != null && nursingCanulaDto.StopTime <= nursingCanula.StartTime)
                        {
                            return JsonResult.Write(code: 201, msg: "拔管时间小于或等于插管时间，请重新输入拔管时间！", null);
                        }
                    }
                    else
                    {
                        //拔管时间小于入科时间
                        if (nursingCanulaDto.StopTime != null && nursingCanulaDto.StopTime <= inDeptTime)
                        {
                            return JsonResult.Write(code: 201, msg: "拔管时间小于入科时间，请重新输入拔管时间！", null);
                        }
                    }

                    nursingCanula.DrawReason = nursingCanulaDto.DrawReason;
                    nursingCanula.StopTime = nursingCanulaDto.StopTime != null
                        ? nursingCanulaDto.StopTime
                        : DateTime.Now;
                    nursingCanula.TubeDrawState = state;
                    nursingCanula.UseFlag = "N";
                    await _nursingCanulaRepository.UpdateAsync(nursingCanula);

                    CanulaRecordEto canulaRecordEto = new CanulaRecordEto();
                    canulaRecordEto.PiId = nursingCanula.PI_ID;
                    canulaRecordEto.NursingCanulaId = nursingCanula.Id;
                    canulaRecordEto.EventType = EEventType.Pluck;

                    canulaRecordEto.CanulaRecord = string.Format("拔管：{0}({1});", nursingCanula.CanulaName, nursingCanula.DrawReason);
                    canulaRecordEto.OperateTime = nursingCanula.StopTime.Value;
                    canulaRecordEto.Signature = nursingCanulaDto.Signature;
                    await WriteToNursingRecordAsync(canulaRecordEto);

                    return JsonResult.Ok(msg: "拔管成功！");
                }
                else
                {
                    //判断是否有同部位在用导管
                    var finder = await _nursingCanulaRepository.FindAsync(s => s.PI_ID == nursingCanula.PI_ID && s.ModuleCode == nursingCanula.ModuleCode && s.CanulaPart == nursingCanula.CanulaPart && s.UseFlag == "Y");

                    if (finder != null)
                    {
                        return JsonResult.Write(code: 201, msg: "已有在用相同导管！", null);
                    }

                    //判断是否是最近一次插管,只能恢复最近一次的插管记录
                    var newCanula = await (await _nursingCanulaRepository.GetQueryableAsync()).Where(s => s.PI_ID == nursingCanula.PI_ID && s.ModuleCode == nursingCanula.ModuleCode && s.CanulaPart == nursingCanula.CanulaPart).OrderByDescending(x => x.CanulaNumber).FirstOrDefaultAsync();

                    if (newCanula.CanulaNumber != nursingCanula.CanulaNumber)
                    {
                        return JsonResult.Write(code: 201, msg: "只能恢复最近一次插管！", null);
                    }

                    nursingCanula.DrawReason = null;
                    nursingCanula.StopTime = null;
                    nursingCanula.TubeDrawState = state;
                    nursingCanula.UseFlag = "Y";
                    await _nursingCanulaRepository.UpdateAsync(nursingCanula);

                    await DeleteNursingRecordAsync(new List<Guid>() { nursingCanula.Id }, EEventType.Pluck);
                    return JsonResult.Ok(msg: "取消拔管成功！");
                }
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 删除一条导管
        /// </summary>
        /// <param name="guid">主键</param>
        /// <returns></returns>
        public async Task<JsonResult> DeleteNursingCanulaInfoAsync(Guid guid)
        {
            try
            {
                NursingCanula nursingCanula = await _nursingCanulaRepository.FirstOrDefaultAsync(s => s.Id == guid && s.UseFlag == "Y");
                if (nursingCanula != null)
                {
                    List<Guid> ids = new List<Guid>();
                    List<Canula> canulas = await (await _canulaRepository.GetQueryableAsync()).Where(x => x.CanulaId == nursingCanula.Id).ToListAsync();
                    if (canulas.Any())
                    {
                        ids.AddRange(canulas.Select(x => x.Id));
                        await _canulaRepository.DeleteManyAsync(canulas);
                    }

                    ids.Add(nursingCanula.Id);
                    await _nursingCanulaRepository.DeleteAsync(nursingCanula);

                    await DeleteNursingRecordAsync(ids, EEventType.Insert);
                    return JsonResult.Ok();
                }

                return JsonResult.DataNotFound("要删除的导管不存在！");
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 删除护理记录单记录
        /// </summary>
        /// <param name="guids"></param>
        /// <param name="eventType"></param>
        /// <returns></returns>
        private async Task DeleteNursingRecordAsync(IEnumerable<Guid> guids, EEventType? eventType = null)
        {
            List<CanulaRecordEto> canulaRecordEtos = new List<CanulaRecordEto>();
            foreach (var guid in guids)
            {
                CanulaRecordEto canulaRecordEto = new CanulaRecordEto() { NursingCanulaId = guid, EventType = eventType };
                canulaRecordEtos.Add(canulaRecordEto);
            }

            await _capPublisher.PublishAsync("deleterecord.to.nursingreport", canulaRecordEtos);
        }

        /// <summary>
        /// 皮肤记录标题方法
        /// </summary>
        /// <returns></returns>
        private static List<CanulaItemDtos> DynamicItem()
        {
            var canulaItemDtos = new List<CanulaItemDtos>
            {
                new CanulaItemDtos()
                {
                    ParaCode = "Id",
                    ParaName = "主键",
                },
                new CanulaItemDtos()
                {
                    ParaCode = "NurseTime",
                    ParaName = "观察时间",
                },
                new CanulaItemDtos()
                {
                    ParaCode = "NurseName",
                    ParaName = "观察人",
                },
                new CanulaItemDtos()
                {
                    ParaCode = "PressArea",
                    ParaName = "大小(cm)"
                }
            };
            return canulaItemDtos;
        }

        /// <summary>
        /// 导管记录列表方法
        /// </summary>
        /// <param name="canula"></param>
        /// <returns></returns>
        private static List<CanulaItemDtos> Dynamic(CanulaDto canula)
        {
            var canulaItemDtos = new List<CanulaItemDtos>
            {
                new CanulaItemDtos()
                {
                    ParaCode = "Id",
                    ParaName = "主键",
                    ParaValue = canula.Id.ToString(),
                },
                new CanulaItemDtos()
                {
                    ParaCode = "NurseTime",
                    ParaName = "时间",
                    ParaValue = canula.NurseTime.ToString(CultureInfo.InvariantCulture),
                },
                // canulaItemDtos.Add(new CanulaItemDtos()
                // {
                //     ParaCode = "ScheduleCode",
                //     ParaName = "班次",
                //     ParaValue = canula.ScheduleCode,
                // });
                new CanulaItemDtos()
                {
                    ParaCode = "NurseName",
                    ParaName = "记录人",
                    ParaValue = canula.NurseName,
                },
                new CanulaItemDtos()
                {
                    ParaCode = "CanulaLength",
                    ParaName = "深度(cm)",
                    ParaValue = string.IsNullOrWhiteSpace(canula.CanulaLength)
                    ? null
                    : canula.CanulaLength + "(" + canula.CanulaWay + ")",
                }
            };
            return canulaItemDtos;
        }

        /// <summary>
        /// 查询管道观察项
        /// </summary>
        /// <param name="canulaId">导管列表ID</param>
        /// <param name="nurseTime">观察时间</param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<JsonResult<CanulaDto>> SelectCanulaInfoAsync(Guid canulaId, DateTime nurseTime, string itemId = "")
        {
            try
            {
                //获取模块代码
                var nursingCanula = await _nursingCanulaRepository.FindAsync(x => x.Id == canulaId);
                string modulecode = nursingCanula.ModuleCode;
                //获取管道主表
                var canula = await (await _canulaRepository.GetQueryableAsync()).Where(s =>
                        !string.IsNullOrEmpty(itemId)
                            ? s.Id == Guid.Parse(itemId)
                            : s.CanulaId == canulaId && s.NurseTime == nurseTime)
                    .Select(s => new CanulaDto()
                    {
                        Id = s.Id,
                        CanulaId = canulaId,
                        NurseTime = s.NurseTime,
                        NurseId = s.NurseId,
                        NurseName = s.NurseName,
                        CanulaWay = s.CanulaWay,
                        CanulaLength = s.CanulaLength,
                        CanulaRecord = s.CanulaRecord,
                    }).FirstOrDefaultAsync();

                //获取所有管道属性项目
                var canulaItemDtos = await (from s in (await _paraItemRepository.GetQueryableAsync()).Where(s => s.IsEnable
                        && s.ModuleCode == modulecode &&
                        s.GroupName == "管道观察")
                                            join c in (await _canulaDynamicRepository.GetQueryableAsync()).Where(s => canula != null
                                                ? s.CanulaId == canula.Id
                                                : s.CanulaId == Guid.Empty) on s.ParaCode equals c.ParaCode into cc
                                            from c in cc.DefaultIfEmpty()
                                            orderby s.Sort
                                            select new CanulaItemDto
                                            {
                                                Id = c == null ? Guid.Empty : c.Id,
                                                ParaCode = s.ParaCode,
                                                ParaName = s.DisplayName,
                                                Sort = s.Sort,
                                                GroupName = s.GroupName,
                                                Style = s.Style,
                                                DataSource = string.IsNullOrEmpty(c.ParaValue) ? s.DataSource : c.ParaValue,
                                            }).ToListAsync();
                var dict = await (await _dictRepository.GetQueryableAsync()).Where(x => x.ModuleCode == modulecode).ToListAsync();
                canulaItemDtos.ForEach(s =>
                {
                    //管道项下拉框字典值
                    s.Dicts = dict.Where(x => x.ParaCode == s.ParaCode)
                        .OrderBy(x => x.Sort).Select(x => new Dicts
                        {
                            DictId = x.ParaCode,
                            DictName = x.ParaName,
                            DictCode = x.DictCode,
                            DictValue = x.DictValue,
                            Sort = x.Sort
                        }).ToList();
                });
                if (canula != null)
                {
                    canula.CanulaItemDto = canulaItemDtos;
                    return JsonResult<CanulaDto>.Ok(data: canula);
                }

                //获取上一次管道数据
                var lastCanula = await (await _canulaRepository.GetQueryableAsync()).Where(s => s.CanulaId == canulaId)
                    .OrderByDescending(s => s.NurseTime).FirstOrDefaultAsync();
                var canulaDto = new CanulaDto()
                {
                    CanulaId = canulaId,
                    CanulaWay = lastCanula == null ? nursingCanula.CanulaWay : lastCanula.CanulaWay,
                    CanulaLength = lastCanula == null ? nursingCanula.CanulaLength : lastCanula.CanulaLength,
                    CanulaItemDto = canulaItemDtos
                };
                return JsonResult<CanulaDto>.Ok(data: canulaDto);
            }
            catch (Exception ex)
            {
                return JsonResult<CanulaDto>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 新增或修改管道观察
        /// </summary>
        /// <param name="canula"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult<string>> CreateCanulaInfoAsync(CanulaDto canula)
        {
            try
            {
                CanulaRecordEto canulaRecordEto = new CanulaRecordEto();
                canulaRecordEto.PiId = canula.PI_ID;
                canulaRecordEto.CanulaRecord = canula.CanulaRecord;
                canulaRecordEto.OperateTime = canula.NurseTime2;
                canulaRecordEto.Signature = canula.Signature;

                Canula oldCanula = await _canulaRepository.FirstOrDefaultAsync(s => s.Id == canula.Id);
                if (oldCanula == null)
                {
                    Guid guid = GuidGenerator.Create();
                    var icuCanula = new Canula(guid)
                    {
                        CanulaId = canula.CanulaId,
                        NurseTime = canula.NurseTime2,
                        CanulaWay = canula.CanulaWay,
                        CanulaLength = canula.CanulaLength,
                        NurseId = canula.NurseId,
                        NurseName = canula.NurseName,
                        CanulaRecord = canula.CanulaRecord
                    };
                    await _canulaRepository.InsertAsync(icuCanula);

                    //定义动态属性
                    var createCanulas = new List<CanulaDynamic>();
                    foreach (CanulaItemDto canulaItem in canula.CanulaItemDto)
                    {
                        createCanulas.Add(new CanulaDynamic(GuidGenerator.Create())
                        {
                            CanulaId = guid,
                            GroupName = canulaItem.GroupName,
                            ParaCode = canulaItem.ParaCode,
                            ParaName = canulaItem.ParaName,
                            ParaValue = canulaItem.DataSource
                        });
                    }

                    await _canulaDynamicRepository.InsertManyAsync(createCanulas);
                    canulaRecordEto.NursingCanulaId = guid;
                    await WriteToNursingRecordAsync(canulaRecordEto);

                    return JsonResult<string>.Ok();
                }
                else
                {
                    //修改导管记录主表
                    oldCanula.NurseId = canula.NurseId;
                    oldCanula.NurseName = canula.NurseName;
                    oldCanula.NurseTime = canula.NurseTime2;
                    oldCanula.CanulaWay = canula.CanulaWay;
                    oldCanula.CanulaLength = canula.CanulaLength;
                    oldCanula.CanulaRecord = canula.CanulaRecord;
                    await _canulaRepository.UpdateAsync(oldCanula);

                    //定义动态属性
                    var createCanulas = new List<CanulaDynamic>();
                    var updateCanulas = new List<CanulaDynamic>();
                    var canulaDynamics = await (await _canulaDynamicRepository.GetQueryableAsync()).AsNoTracking()
                        .Where(s => s.CanulaId == oldCanula.Id).ToListAsync();
                    foreach (CanulaItemDto canulaItem in canula.CanulaItemDto)
                    {
                        //新增动态参数
                        if (canulaItem.Id == Guid.Empty)
                        {
                            createCanulas.Add(new CanulaDynamic(GuidGenerator.Create())
                            {
                                CanulaId = oldCanula.Id,
                                GroupName = canulaItem.GroupName,
                                ParaCode = canulaItem.ParaCode,
                                ParaName = canulaItem.ParaName,
                                ParaValue = canulaItem.DataSource
                            });
                        }
                        else
                        {
                            var canulaDynamic = canulaDynamics.FirstOrDefault(s => s.Id == canulaItem.Id);
                            if (canulaDynamic != null)
                            {
                                canulaDynamic.GroupName = canulaItem.GroupName;
                                canulaDynamic.ParaCode = canulaItem.ParaCode;
                                canulaDynamic.ParaName = canulaItem.ParaName;
                                canulaDynamic.ParaValue = canulaItem.DataSource;
                                updateCanulas.Add(canulaDynamic);
                            }
                        }
                    }

                    if (createCanulas.Any())
                    {
                        await _canulaDynamicRepository.InsertManyAsync(createCanulas);
                    }

                    await _canulaDynamicRepository.UpdateManyAsync(updateCanulas);
                    canulaRecordEto.NursingCanulaId = oldCanula.Id;
                    await WriteToNursingRecordAsync(canulaRecordEto);

                    return JsonResult<string>.Ok();
                }
            }
            catch (Exception ex)
            {
                return JsonResult<string>.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 删除一条导管记录
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns></returns>
        public async Task<JsonResult> DeleteCanulaInfoAsync(Guid id)
        {
            try
            {
                //获取管道主表
                var canula = await _canulaRepository.FindAsync(s => s.Id == id);

                if (canula != null)
                {
                    //获取所有管道属性项目
                    var canulaItemDtos =
                        await (await _canulaDynamicRepository.GetQueryableAsync()).Where(s => s.CanulaId == canula.Id).ToListAsync();

                    await _canulaRepository.DeleteAsync(canula);
                    await _canulaDynamicRepository.DeleteManyAsync(canulaItemDtos);
                    await DeleteNursingRecordAsync(new List<Guid>() { canula.Id });
                    return JsonResult.Ok();
                }

                return JsonResult.DataNotFound();
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 复制一条数据
        /// </summary>
        /// <param name="copyCanulaDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult<string>> CopyCanulaInfoAsync(CopyCanulaDto copyCanulaDto)
        {
            try
            {
                int state = copyCanulaDto.State;
                Guid id = copyCanulaDto.Id;
                if (state == 1)
                {
                    var icuCanula = await _canulaRepository.FindAsync(s => s.Id == id);
                    if (icuCanula == null)
                    {
                        return JsonResult<string>.DataNotFound();
                    }
                }

                if (state == 2)
                {
                    if (!await (await _nursingCanulaRepository.GetQueryableAsync()).AnyAsync(x => x.Id == id))
                    {
                        return JsonResult<string>.DataNotFound();
                    }
                }

                //获取当前登录人
                string staffcode = CurrentUser.FindClaimValue("name");
                string staffName = CurrentUser.FindClaimValue("fullName");

                //复制上一条,更换id
                if (state == 2)
                {
                    var id1 = id;
                    var icuCanula = await (await _canulaRepository.GetQueryableAsync()).Where(s => s.CanulaId == id1)
                        .OrderByDescending(x => x.NurseTime).FirstOrDefaultAsync();
                    if (icuCanula == null)
                    {
                        return JsonResult<string>.Fail(msg: "无上一条记录");
                    }

                    id = icuCanula.Id;
                }

                //获取管道主表
                var canula = await _canulaRepository.FindAsync(s => s.Id == id);

                if (canula == null)
                {
                    return JsonResult<string>.DataNotFound();
                }

                Guid canulaId = GuidGenerator.Create();
                //复制主护理记录表
                var canulaCopy = new Canula(canulaId)
                {
                    CanulaId = canula.CanulaId,
                    NurseTime = Convert.ToDateTime(DateTime.Now.ToString("G")),
                    CanulaWay = canula.CanulaWay,
                    CanulaLength = canula.CanulaLength,
                    NurseId = staffcode,
                    NurseName = staffName,
                    CanulaRecord = canula.CanulaRecord,
                };

                //获取所有管道属性项目
                var canulaItemDtos = await (await _canulaDynamicRepository.GetQueryableAsync()).Where(s => s.CanulaId == canula.Id).ToListAsync();

                //复制附表
                var canulaDynamics = new List<CanulaDynamic>();
                foreach (CanulaDynamic canulaDynamic in canulaItemDtos)
                {
                    canulaDynamics.Add(new CanulaDynamic(GuidGenerator.Create())
                    {
                        CanulaId = canulaId,
                        GroupName = canulaDynamic.GroupName,
                        ParaCode = canulaDynamic.ParaCode,
                        ParaName = canulaDynamic.ParaName,
                        ParaValue = canulaDynamic.ParaValue
                    });
                }

                await _canulaRepository.InsertAsync(canulaCopy);
                await _canulaDynamicRepository.InsertManyAsync(canulaDynamics);

                CanulaRecordEto canulaRecordEto = new CanulaRecordEto();
                canulaRecordEto.PiId = copyCanulaDto.PI_ID;
                canulaRecordEto.NursingCanulaId = canulaCopy.Id;
                canulaRecordEto.CanulaRecord = canulaCopy.CanulaRecord;
                canulaRecordEto.OperateTime = canulaCopy.NurseTime;
                canulaRecordEto.Signature = copyCanulaDto.Signature;
                await WriteToNursingRecordAsync(canulaRecordEto);

                return JsonResult<string>.Ok();
            }
            catch (Exception ex)
            {
                return JsonResult<string>.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 全景视图-导管列表
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<NursingCanula>> GetAllViewNursingCanulaListAsync(Guid PID, DateTime? StartTime, DateTime? EndTime, CancellationToken cancellationToken = default)
        {
            try
            {
                var nursingCanulaList = await (await this._nursingCanulaRepository.GetQueryableAsync()).AsNoTracking()
                .Where(w => w.PI_ID == PID)
                .WhereIf(StartTime.HasValue, w => w.StartTime >= Convert.ToDateTime(StartTime))
                .WhereIf(EndTime.HasValue, w => w.StopTime <= Convert.ToDateTime(EndTime))
                .OrderBy(p => p.CreationTime).ToListAsync(cancellationToken);

                return nursingCanulaList;
                //_log.LogInformation("Get admissionRecord by id success");
                //return RespUtil.Ok<List<NursingCanula>>(data: nursingCanulaList);

            }
            catch// (Exception ex)
            {
                return null;
                //_log.LogError("Get admissionRecord by id error.ErrorMsg:{Msg}", e);
                //return RespUtil.InternalError<List<NursingCanula>>(extra: ex.Message);
            }
        }
    }
}