using YiJian.BodyParts.Dtos;
using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.IService;
using YiJian.BodyParts.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Volo.Abp.Guids;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using YiJian.BodyParts.Application.Contracts.Dtos.Dictionary;
using Volo.Abp.Users;
using DocumentFormat.OpenXml.Wordprocessing;
using DotNetCore.CAP;
using Volo.Abp.Uow;
using Minio.DataModel;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.AspNetCore.Authorization;

namespace YiJian.BodyParts.Service
{
    /// <summary>
    /// 患者皮肤管理
    /// </summary>
    [AllowAnonymous]
    public class NuringSkinAppService : ApplicationService, INuringSkinAppService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGuidGenerator _guidGenerator;
        //患者皮肤
        private IIcuNursingSkinRepository _icuNursingSkinRepository;
        private IIcuSkinRepository _icuSkinRepository;
        private IIcuParaModuleRepository _icuParaModuleRepository;
        private IIcuParaItemRepository _icuParaItemRepository;
        private IDictRepository _dictRepository;
        //private IIcuDeptScheduleRepository _icuDeptScheduleRepository;
        private IDictCanulaPartRepository _dictCanulaPartRepository;
        private ISkinDynamicRepository _skinDynamicRepository;
        private IIcuNursingEventRepository _icuNursingEvent;
        private ICapPublisher _capPublisher;


        public NuringSkinAppService(IHttpContextAccessor httpContextAccessor,
            IGuidGenerator guidGenerator,
            IIcuNursingSkinRepository icuNursingSkinRepository,
            IIcuSkinRepository icuSkinRepository,
            IIcuParaModuleRepository icuParaModuleRepository,
            IIcuParaItemRepository icuParaItemRepository,
            IDictRepository dictRepository,
            //IIcuDeptScheduleRepository icuDeptScheduleRepository,
            IDictCanulaPartRepository dictCanulaPartRepository,
            ISkinDynamicRepository skinDynamicRepository,
            IIcuNursingEventRepository icuNursingEvent,
            ICapPublisher CapPublisher)
        {
            _httpContextAccessor = httpContextAccessor;
            _guidGenerator = guidGenerator;
            _icuNursingSkinRepository = icuNursingSkinRepository;
            _icuSkinRepository = icuSkinRepository;
            _icuParaModuleRepository = icuParaModuleRepository;
            _icuParaItemRepository = icuParaItemRepository;
            _dictRepository = dictRepository;
            //_icuDeptScheduleRepository = icuDeptScheduleRepository;
            _dictCanulaPartRepository = dictCanulaPartRepository;
            _skinDynamicRepository = skinDynamicRepository;
            _icuNursingEvent = icuNursingEvent;
            _capPublisher = CapPublisher;
        }

        #region 新版本 动态表格，删除接口与旧版本共用

        #region 皮肤记录主表
        /// <summary>
        /// 根据条件查询皮肤模块
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<IcuSkinParaModuleDto>>> SelectParaModuleList(string deptCode)
        {
            if (string.IsNullOrWhiteSpace(deptCode))
            {
                return JsonResult<List<IcuSkinParaModuleDto>>.RequestParamsIsNull(msg: "请传入科室代码！");
            }
            try
            {
                var moduleList = await _icuParaModuleRepository.Where(s => s.DeptCode == deptCode && s.ModuleType == "SKIN"
                        && s.IsEnable == true && s.ValidState == 1)
                    .OrderBy(x => x.SortNum).ToListAsync();
                var moduleCodeList = moduleList.Select(s => s.ModuleCode);

                var stageParaCodeList = await _icuParaItemRepository.Where(p => moduleCodeList.Contains(p.ModuleCode) && p.ParaName == "压疮分期" && p.ValidState == 1).Select(s => s.ParaCode).Distinct().ToListAsync();
                var stageDictList = await _dictRepository.Where(p => moduleCodeList.Contains(p.ModuleCode) && stageParaCodeList.Contains(p.ParaCode) && p.ValidState == 1).ToListAsync();

                var moduleDtoList = ObjectMapper.Map<List<IcuParaModule>, List<IcuSkinParaModuleDto>>(moduleList);

                return JsonResult<List<IcuSkinParaModuleDto>>.Ok(data: moduleDtoList);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<List<IcuSkinParaModuleDto>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 根据条件查询压疮列表
        /// </summary>
        /// <param name="PI_ID">患者id</param>
        /// <param name="Finished">使用标志：（false护理中，true已好转）</param>
        /// <param name="State">状态：（0发生时间，1结束时间）</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<NursingSkinDto>>> SelectNursingSkins(string PI_ID, bool Finished, int? State, DateTime? StartTime, DateTime? EndTime)
        {
            try
            {
                //多条件拼接扩展
                Expression<Func<IcuNursingSkin, bool>> predicateNC = PredicateBuilder.True<IcuNursingSkin>();

                predicateNC = predicateNC.And(s => s.ValidState == 1 && s.Finished == Finished);

                if (!string.IsNullOrEmpty(PI_ID))
                {
                    predicateNC = predicateNC.And(s => s.PI_ID == PI_ID);
                }
                //发生时间
                if (State != null && State == 0 && StartTime != null && StartTime.HasValue)
                {
                    predicateNC = predicateNC.And(S => S.NurseTime >= StartTime);
                }
                if (State != null && State == 0 && EndTime != null && EndTime.HasValue)
                {
                    predicateNC = predicateNC.And(S => S.NurseTime <= EndTime);
                }
                //结束时间
                if (State != null && State == 1 && StartTime != null && StartTime.HasValue)
                {
                    predicateNC = predicateNC.And(S => S.FinishTime >= StartTime);
                }
                if (State != null && State == 1 && EndTime != null && EndTime.HasValue)
                {
                    predicateNC = predicateNC.And(S => S.FinishTime <= EndTime);
                }

                var skinList = await (from s in _icuNursingSkinRepository.Where(predicateNC)
                                      join c in _dictCanulaPartRepository.Where(x => x.IsDeleted == false) on
                                      new { s.ModuleCode, CanulaPart = s.PressPart } equals new { c.ModuleCode, CanulaPart = c.PartName } into cc
                                      from c in cc.DefaultIfEmpty()
                                      select new NursingSkinDto
                                      {
                                          Id = s.Id,
                                          PI_ID = s.PI_ID,
                                          PressPart = s.PressPart,
                                          NurseTime = s.NurseTime,
                                          Days = s.FinishTime == null || !s.FinishTime.HasValue ? DateTime.Now.Subtract(Convert.ToDateTime(s.NurseTime.ToShortDateString())).Days + 1
                                            : s.FinishTime.Value.Subtract(Convert.ToDateTime(s.NurseTime.ToShortDateString())).Days + 1,
                                          ModuleCode = s.ModuleCode,
                                          PressType = s.PressType,
                                          PressStage = s.PressStage,
                                          PressArea = s.PressArea,
                                          PressColor = s.PressColor,
                                          PressSmell = s.PressSmell,
                                          NurseId = s.NurseId,
                                          NurseName = s.NurseName,
                                          PartNumber = c.PartNumber
                                      }).OrderByDescending(x => x.NurseTime).ToListAsync();

                return JsonResult<List<NursingSkinDto>>.Ok(data: skinList);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<List<NursingSkinDto>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 结束皮肤护理
        /// </summary>
        /// <param name="nursingSkinDto"></param>
        /// <param name="State">0结束，1取消结束</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<JsonResult> UpdateNursingSkinInfo(NursingSkinDto nursingSkinDto, SkinStateEnum State = SkinStateEnum.其他)
        {
            if (State == SkinStateEnum.其他)
            {
                return JsonResult.RequestParamsIsNull(msg: "请输入正确的操作状态！");
            }
            try
            {
                var nursingSkin = nursingSkinDto.Id == Guid.Empty ? null : await _icuNursingSkinRepository.FindAsync(s => s.Id == nursingSkinDto.Id && s.ValidState == 1);

                if (nursingSkin != null)
                {
                    if (State == SkinStateEnum.结束)
                    {
                        if (nursingSkinDto.FinishTime != null && nursingSkinDto.FinishTime.HasValue && nursingSkinDto.FinishTime <= nursingSkin.NurseTime)
                        {
                            return JsonResult.Write(code: 201, msg: "结束时间小于发生时间，请重新输入结束时间！", null);
                        }
                        nursingSkin.Finished = true;
                        nursingSkin.FinishTime = nursingSkinDto.FinishTime != null && nursingSkinDto.FinishTime.HasValue ? nursingSkinDto.FinishTime : DateTime.Now;
                        await _icuNursingSkinRepository.UpdateAsync(nursingSkin);
                        var skinRecordEto = new SkinRecordEto()
                        {
                            Signature = nursingSkinDto.Signature,
                            CanulaRecord = "皮肤护理结束",
                            NursingCanulaId = nursingSkin.Id,
                            OperateTime = Convert.ToDateTime(nursingSkin.NurseTime),
                            EventType = 4,
                            PiId = Guid.Parse(nursingSkin.PI_ID)
                        };
                        await WriteToNursingRecordAsync(skinRecordEto);

                        return JsonResult.Ok(msg: "结束皮肤护理！");
                    }
                    else
                    {
                        //判断是否有同部位皮肤护理
                        var finder = await _icuNursingSkinRepository.FindAsync(s => s.PI_ID == nursingSkin.PI_ID
                            && s.ModuleCode == nursingSkin.ModuleCode && s.PressPart == nursingSkin.PressPart
                            && s.Finished == false && s.ValidState == 1);

                        if (finder != null)
                        {
                            return JsonResult.Write(code: 201, msg: "已有在用同类型导管！", null);
                        }

                        //判断是否是最近一次皮肤护理,只能恢复最近一次的皮肤护理
                        var newCanula = await _icuNursingSkinRepository.Where(s => s.PI_ID == nursingSkin.PI_ID
                            && s.ModuleCode == nursingSkin.ModuleCode && s.PressPart == nursingSkin.PressPart && s.ValidState == 1)
                            .OrderByDescending(x => x.NurseTime).FirstOrDefaultAsync();

                        if (newCanula.Id != nursingSkin.Id)
                        {
                            return JsonResult.Write(code: 201, msg: "只能恢复最近一次皮肤护理！", null);
                        }

                        nursingSkin.Finished = false;
                        nursingSkin.FinishTime = null;
                        await _icuNursingSkinRepository.UpdateAsync(nursingSkin);

                        await DeleteNursingRecordAsync(new List<Guid>() { nursingSkin.Id }, 4);

                        return JsonResult.Ok(msg: "取消结束成功！");
                    }
                }
                else { return JsonResult.DataNotFound(msg: "要结束护理的皮肤部位不存在！"); }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        #endregion

        #region 皮肤属性项
        /// <summary>
        /// 查询皮肤属性项
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="PI_ID">患者id</param>
        /// <param name="ModuleCode">模块代码</param>
        /// <param name="deptCode">科室代码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<NursingSkinDto>> SelectNursingSkinInfo([Required] Guid Id, [Required] string PI_ID, [Required] string ModuleCode, [Required] string deptCode)
        {
            try
            {
                //获取皮肤主表
                var skinCanula = await (from s in _icuParaModuleRepository.Where(s => s.ModuleCode == ModuleCode)
                                        join c in _icuNursingSkinRepository.Where(s => s.Id == Id && s.Finished == false && s.ValidState == 1)
                                        on s.ModuleCode equals c.ModuleCode into cc
                                        from c in cc.DefaultIfEmpty()
                                        select new NursingSkinDto()
                                        {
                                            Id = c.Id,
                                            PI_ID = PI_ID,
                                            NurseTime = c.NurseTime,
                                            ModuleCode = s.ModuleCode,
                                            PressType = s.ModuleName,
                                            PressArea = c.PressArea,
                                            NurseId = c.NurseId,
                                            NurseName = c.NurseName,
                                            CanulaRecord = c.CanulaRecord
                                        }).FirstOrDefaultAsync();


                //多条件拼接扩展
                Expression<Func<IcuParaItem, bool>> predicatePI = PredicateBuilder.True<IcuParaItem>();
                predicatePI = predicatePI.And(s => s.ValidState == 1 && s.IsEnable == true && s.DeptCode == deptCode && s.ModuleCode == ModuleCode && s.GroupName == "皮肤属性");

                //多条件拼接扩展
                Expression<Func<SkinDynamic, bool>> predicateCD = PredicateBuilder.True<SkinDynamic>();
                if (skinCanula != null)
                {
                    predicateCD = predicateCD.And(s => s.CanulaId == skinCanula.Id);
                }

                //获取所有皮肤属性项目
                var skinItemDtos = await (from s in _icuParaItemRepository.Where(predicatePI)
                                          join c in _skinDynamicRepository.Where(predicateCD) on s.ParaCode equals c.ParaCode into cc
                                          from c in cc.DefaultIfEmpty()
                                          orderby s.SortNum
                                          select new CanulaItemDto
                                          {
                                              Id = c.Id,
                                              ParaCode = s.ParaCode,
                                              ParaName = s.DisplayName,
                                              DictFlag = s.DictFlag,
                                              SortNum = s.SortNum,
                                              GroupName = s.GroupName,
                                              Style = s.Style,
                                              DataSource = string.IsNullOrEmpty(c.ParaValue) ? s.DataSource : c.ParaValue,
                                              //管道项下拉框字典值
                                              Dicts = s.DictFlag == "Y"
                                                  ? _dictCanulaPartRepository.Where(x => x.ModuleCode == ModuleCode && x.IsDeleted == false)
                                                  .OrderBy(x => x.SortNum).Select(x => new Dicts
                                                  {
                                                      DictCanulaPartId = x.Id,
                                                      DictId = s.ParaCode,
                                                      DictName = s.ParaName,
                                                      DictValue = x.PartName,
                                                      SortNum = x.SortNum
                                                  }).ToList()
                                                  : _dictRepository.Where(x => x.ModuleCode == ModuleCode && x.ParaCode == s.ParaCode && x.ValidState == 1)
                                                  .OrderBy(x => x.SortNum).Select(x => new Dicts
                                                  {
                                                      DictCanulaPartId = x.Id,
                                                      DictId = x.ParaCode,
                                                      DictName = x.ParaName,
                                                      DictCode = x.DictCode,
                                                      DictValue = x.DictValue,
                                                      SortNum = x.SortNum
                                                  }).ToList()
                                          }).ToListAsync();

                skinCanula.CanulaItemDto = skinItemDtos;
                return JsonResult<NursingSkinDto>.Ok(data: skinCanula);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<NursingSkinDto>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 新增或修改皮肤属性
        /// </summary>
        /// <param name="neworupdate">新增：new，修改：update</param>
        /// <param name="nursingSkin"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CreateNursingSkin(string neworupdate, NursingSkinDto nursingSkin)
        {
            if (string.IsNullOrWhiteSpace(nursingSkin.PI_ID) || string.IsNullOrWhiteSpace(nursingSkin.ModuleCode))
            {
                return JsonResult.RequestParamsIsNull(msg: "请输入患者id、模块代码！");
            }
            if (neworupdate != "new" && neworupdate != "update")
            {
                return JsonResult.RequestParamsIsNull(msg: "请输入新增、修改标志！");
            }
            try
            {

                //查询模块列表
                var moduleInfo = await _icuParaModuleRepository.FindAsync(s => s.ModuleCode == nursingSkin.ModuleCode && s.ValidState == 1);
                string modulename = moduleInfo.ModuleName;

                string source = null;
                string part = null;
                string stage = null;
                string color = null;
                string smell = null;
                string exudateColor = null;
                string exudateAmount = null;
                foreach (var canulaItemDto in nursingSkin.CanulaItemDto)
                {
                    switch (canulaItemDto.ParaName)
                    {
                        case "来源":
                            source = canulaItemDto.DataSource;
                            break;
                        case "外观颜色":
                            color = canulaItemDto.DataSource;
                            break;
                        case "渗出液气味":
                            smell = canulaItemDto.DataSource;
                            break;
                        case "渗出液颜色":
                            exudateColor = canulaItemDto.DataSource;
                            break;
                        case "渗出液量":
                            exudateAmount = canulaItemDto.DataSource;
                            break;
                        default:
                            break;
                    }
                    if (canulaItemDto.ParaName.Contains("部位"))
                    {
                        part = canulaItemDto.DataSource;
                    }
                    if (canulaItemDto.ParaName.Contains("分期"))
                    {
                        stage = canulaItemDto.DataSource;
                    }
                }
                //查询最近一次皮肤护理
                var oldSkin = await _icuNursingSkinRepository.Where(s => s.PI_ID == nursingSkin.PI_ID && s.ModuleCode == nursingSkin.ModuleCode
                    && s.PressPart == part && s.ValidState == 1).OrderByDescending(s => s.NurseTime).FirstOrDefaultAsync();


                if (neworupdate == "new") //新建皮肤
                {
                    var finder = await _icuNursingSkinRepository.FindAsync(s => s.PI_ID == nursingSkin.PI_ID
                        && s.ModuleCode == nursingSkin.ModuleCode
                        && s.PressPart == part
                        && s.Finished == false && s.ValidState == 1);

                    if (finder == null)
                    {
                        Guid guid = _guidGenerator.Create();
                        IcuNursingSkin icuNursingSkin = new IcuNursingSkin(guid)
                        {
                            PI_ID = nursingSkin.PI_ID,
                            NurseTime = nursingSkin.NurseTime,
                            ModuleCode = nursingSkin.ModuleCode,
                            PressType = modulename,
                            PressPart = part,
                            PressSource = source,
                            PressStage = stage,
                            PressColor = color,
                            PressSmell = smell,
                            ExudateColor = exudateColor,
                            ExudateAmount = exudateColor,
                            PressArea = nursingSkin.PressArea,
                            NurseId = nursingSkin.NurseId,
                            NurseName = nursingSkin.NurseName,
                            CanulaRecord = nursingSkin.CanulaRecord,
                            Finished = false,
                            ValidState = 1
                        };

                        //定义动态属性
                        List<SkinDynamic> createSkins = new List<SkinDynamic>();
                        foreach (CanulaItemDto canulaItem in nursingSkin.CanulaItemDto)
                        {
                            createSkins.Add(new SkinDynamic(_guidGenerator.Create())
                            {
                                CanulaId = guid,
                                GroupName = canulaItem.GroupName,
                                ParaCode = canulaItem.ParaCode,
                                ParaName = canulaItem.ParaName,
                                ParaValue = canulaItem.DataSource
                            });
                        }

                        await _icuNursingSkinRepository.InsertAsync(icuNursingSkin);
                        await _skinDynamicRepository.CreateRangeAsync(createSkins);
                        var skinRecordEto = new SkinRecordEto()
                        {
                            Signature = nursingSkin.Signature,
                            CanulaRecord = nursingSkin.CanulaRecord,
                            NursingCanulaId = icuNursingSkin.Id,
                            OperateTime = Convert.ToDateTime(nursingSkin.NurseTime),
                            EventType = 3,
                            PiId = Guid.Parse(nursingSkin.PI_ID)
                        };
                        await WriteToNursingRecordAsync(skinRecordEto);
                        return JsonResult.Ok();
                    }
                    else
                    {
                        return JsonResult.Write(code: 201, msg: "已有在用同部位的皮肤护理！", null);
                    }
                }
                else //修改皮肤
                {
                    var finder = await _icuNursingSkinRepository.FindAsync(s => s.Id == nursingSkin.Id && s.PI_ID == nursingSkin.PI_ID
                        && s.Finished == false && s.ValidState == 1);

                    if (finder.ModuleCode != nursingSkin.ModuleCode)
                    {
                        return JsonResult.Write(code: 201, msg: "皮肤类型不可修改！", null);
                    }

                    //定义未修改前的发生时间、压疮部位
                    DateTime? oldStartTime = finder.NurseTime;
                    string oldPart = finder.PressPart;

                    //查询是否是同类型皮肤护理
                    var pressPart = await _icuNursingSkinRepository.FindAsync(s => s.PI_ID == nursingSkin.PI_ID
                        && s.ModuleCode == nursingSkin.ModuleCode
                        && s.PressPart == part
                        && s.Finished == false && s.ValidState == 1 && s.Id != finder.Id);

                    if (pressPart != null && pressPart.PressPart == part) { return JsonResult.Write(code: 201, msg: "已有在护理部位,不能更换护理部位！", null); }

                    /*编辑未更换部位，第一次发生时间不小于入科时间，第二次及以上发生时间不小于上一次结束时间
                      编辑更换部位，第一次发生时间不小于入科时间，第二次及以上发生时间不小于上一次结束时间*/
                    //查询是否有前一次皮肤护理
                    var skin = await _icuNursingSkinRepository.Where(s => s.PI_ID == nursingSkin.PI_ID && s.Id == finder.Id && s.ModuleCode == nursingSkin.ModuleCode
                                    && s.PressPart == part && s.ValidState == 1).OrderByDescending(s => s.NurseTime).FirstOrDefaultAsync();

                    if (oldPart != part && oldSkin != null)
                    {
                        if (nursingSkin.NurseTime < oldSkin.FinishTime)
                        {
                            return JsonResult.Write(code: 201, msg: "发生时间小于上一次结束时间，请重新输入发生时间！", null);
                        }
                    }
                    if (oldPart == part && skin != null)
                    {
                        if (nursingSkin.NurseTime < skin.FinishTime)
                        {
                            return JsonResult.Write(code: 201, msg: "发生时间小于上一次结束时间，请重新输入发生时间！", null);
                        }
                    }

                    if (finder != null)
                    {
                        //修改管道主表信息
                        finder.PI_ID = nursingSkin.PI_ID;
                        finder.NurseTime = nursingSkin.NurseTime;
                        finder.ModuleCode = nursingSkin.ModuleCode;
                        finder.PressType = modulename;
                        finder.PressPart = part;
                        finder.PressSource = source;
                        finder.PressStage = stage;
                        finder.PressColor = color;
                        finder.PressSmell = smell;
                        finder.ExudateColor = exudateColor;
                        finder.ExudateAmount = exudateColor;
                        finder.PressArea = nursingSkin.PressArea;
                        finder.NurseId = nursingSkin.NurseId;
                        finder.NurseName = nursingSkin.NurseName;
                        finder.CanulaRecord = nursingSkin.CanulaRecord;

                        //定义动态属性
                        List<SkinDynamic> createSkins = new List<SkinDynamic>();
                        List<SkinDynamic> updateSkins = new List<SkinDynamic>();
                        SkinDynamic skinDynamic = new SkinDynamic();
                        var skinDynamics = await _skinDynamicRepository.AsNoTracking().Where(s => s.CanulaId == finder.Id).ToListAsync();
                        foreach (CanulaItemDto canulaItem in nursingSkin.CanulaItemDto)
                        {
                            //新增动态参数
                            if (canulaItem.Id == Guid.Empty)
                            {
                                createSkins.Add(new SkinDynamic(_guidGenerator.Create())
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
                                skinDynamic = skinDynamics.Where(s => s.Id == canulaItem.Id).FirstOrDefault();
                                skinDynamic.GroupName = canulaItem.GroupName;
                                skinDynamic.ParaCode = canulaItem.ParaCode;
                                skinDynamic.ParaName = canulaItem.ParaName;
                                skinDynamic.ParaValue = canulaItem.DataSource;
                                updateSkins.Add(skinDynamic);
                            }
                        }
                        await _icuNursingSkinRepository.UpdateAsync(finder);

                        if (createSkins.Any())
                        {
                            await _skinDynamicRepository.CreateRangeAsync(createSkins);
                        }
                        await _skinDynamicRepository.UpdateRangeAsync(updateSkins);
                        await WriteToNursingRecordAsync(new SkinRecordEto()
                        {
                            Signature = nursingSkin.Signature,
                            CanulaRecord = nursingSkin.CanulaRecord,
                            EventType = 3,
                            NursingCanulaId = finder.Id,
                            OperateTime = Convert.ToDateTime(nursingSkin.NurseTime),
                            PiId = Guid.Parse(nursingSkin.PI_ID)
                        });
                        return JsonResult.Ok();
                    }
                    else
                    {
                        return JsonResult.DataNotFound();
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        #endregion

        #region 皮肤观察项
        /// <summary>
        /// 查询皮肤列表
        /// </summary>
        /// <param name="SkinId">皮肤列表Id</param>
        /// <param name="state">状态：0：今天（按班次），1：自定义（自然日）</param>
        /// <param name="deptCode">科室代码</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<CanulaListDto>>> SelectSkins(Guid SkinId, int state, string deptCode, DateTime? startTime, DateTime? endTime)
        {
            try
            {
                //获取模块代码
                var nursingSkin = await _icuNursingSkinRepository.FindAsync(x => x.Id == SkinId);
                string PI_ID = nursingSkin.PI_ID;
                string modulecode = nursingSkin.ModuleCode;

                Expression<Func<IcuSkin, bool>> predicateIC = PredicateBuilder.True<IcuSkin>();
                predicateIC = predicateIC.And(s => s.SkinId == nursingSkin.Id);

                //已结束的默认按自然日查询
                if (nursingSkin.Finished == true) state = 1;

                //按自然日查询
                if (state == 1 && startTime != null && startTime.HasValue)
                {
                    predicateIC = predicateIC.And(s => s.NurseTime >= startTime);
                }
                if (state == 1 && endTime != null && endTime.HasValue)
                {
                    predicateIC = predicateIC.And(s => s.NurseTime <= endTime);
                }

                //定义返回值
                List<CanulaListDto> skinDtos = new List<CanulaListDto>();

                //获取皮肤主表
                var skins = await _icuSkinRepository.Where(predicateIC).OrderByDescending(x => x.NurseTime)
                    .Select(s => new SkinDto()
                    {
                        Id = s.Id,
                        NurseTime = s.NurseTime.ToString("yyyy-MM-dd HH:mm"),
                        PressArea = s.PressArea,
                        NurseName = s.NurseName
                    }).ToListAsync();

                //多条件拼接扩展
                Expression<Func<IcuParaItem, bool>> predicatePI = PredicateBuilder.True<IcuParaItem>();
                predicatePI = predicatePI.And(s => s.ValidState == 1 && s.IsEnable == true && s.DeptCode == deptCode
                    && s.ModuleCode == modulecode && s.GroupName == "皮肤观察");

                //生成标题列
                var skinItems = await _icuParaItemRepository.Where(predicatePI).OrderBy(s => s.SortNum)
                    .Select(s => new CanulaItemDtos
                    {
                        ParaCode = s.ParaCode,
                        ParaName = s.ParaName
                    }).ToListAsync();

                skinDtos.Add(new CanulaListDto() { CanulaItemDto = DynamicItem().Union(skinItems).ToList() });

                foreach (SkinDto skin in skins)
                {

                    //多条件拼接扩展
                    Expression<Func<SkinDynamic, bool>> predicateCD = PredicateBuilder.True<SkinDynamic>();
                    predicateCD = predicateCD.And(s => s.CanulaId == skin.Id);

                    //获取所有皮肤属性项目
                    var skinItemDtos = await (from s in _icuParaItemRepository.Where(predicatePI)
                                              join c in _skinDynamicRepository.Where(predicateCD) on s.ParaCode equals c.ParaCode into cc
                                              from c in cc.DefaultIfEmpty()
                                              orderby s.SortNum
                                              select new CanulaItemDtos
                                              {
                                                  ParaCode = s.ParaCode,
                                                  ParaName = s.DisplayName,
                                                  ParaValue = c.ParaValue,
                                              }).ToListAsync();

                    skinDtos.Add(new CanulaListDto() { CanulaItemDto = Dynamic(skin).Union(skinItemDtos).ToList() });
                }

                return JsonResult<List<CanulaListDto>>.Ok(data: skinDtos);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<List<CanulaListDto>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 查询皮肤观察项
        /// </summary>
        /// <param name="SkinId">皮肤列表ID</param>
        /// <param name="NurseTime">观察时间</param>
        /// <param name="deptCode">科室编码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<SkinDto>> SelectSkin(Guid SkinId, DateTime NurseTime, string deptCode)
        {
            try
            {
                //获取模块代码
                var nursingSkin = await _icuNursingSkinRepository.FindAsync(x => x.Id == SkinId);
                string PI_ID = nursingSkin.PI_ID;
                string modulecode = nursingSkin.ModuleCode;

                //获取皮肤主表
                var skin = await _icuSkinRepository.Where(s => s.SkinId == SkinId && s.NurseTime == NurseTime)
                    .Select(s => new SkinDto()
                    {
                        Id = s.Id,
                        SkinId = SkinId,
                        NurseTime = s.NurseTime.ToString("yyyy-MM-dd HH:mm"),
                        PressArea = s.PressArea,
                        NurseId = s.NurseId,
                        NurseName = s.NurseName,
                        CanulaRecord = s.CanulaRecord
                    }).FirstOrDefaultAsync();

                if (string.IsNullOrWhiteSpace(deptCode))
                {
                    return JsonResult<SkinDto>.DataNotFound(msg: "此患者无科室或无此患者信息，请查证后再查询！");
                }

                //多条件拼接扩展
                Expression<Func<IcuParaItem, bool>> predicatePI = PredicateBuilder.True<IcuParaItem>();
                predicatePI = predicatePI.And(s => s.ValidState == 1 && s.IsEnable == true && s.DeptCode == deptCode
                    && s.ModuleCode == modulecode && s.GroupName == "皮肤观察");

                //多条件拼接扩展
                Expression<Func<SkinDynamic, bool>> predicateCD = PredicateBuilder.True<SkinDynamic>();
                if (skin != null)
                {
                    predicateCD = predicateCD.And(s => s.CanulaId == skin.Id);
                }
                else
                {
                    predicateCD = predicateCD.And(s => s.CanulaId == null);
                }

                //获取所有皮肤观察项目
                var skinItemDtos = await (from s in _icuParaItemRepository.Where(predicatePI)
                                          join c in _skinDynamicRepository.Where(predicateCD) on s.ParaCode equals c.ParaCode into cc
                                          from c in cc.DefaultIfEmpty()
                                          orderby s.SortNum
                                          select new CanulaItemDto
                                          {
                                              Id = c.Id,
                                              ParaCode = s.ParaCode,
                                              ParaName = s.DisplayName,
                                              SortNum = s.SortNum,
                                              GroupName = s.GroupName,
                                              Style = s.Style,
                                              DataSource = string.IsNullOrEmpty(c.ParaValue) ? s.DataSource : c.ParaValue,
                                              //管道项下拉框字典值
                                              Dicts = _dictRepository.Where(x => x.ModuleCode == modulecode && x.ParaCode == s.ParaCode && x.ValidState == 1)
                                                  .OrderBy(x => x.SortNum).Select(x => new Dicts
                                                  {
                                                      DictId = x.ParaCode,
                                                      DictName = x.ParaName,
                                                      DictCode = x.DictCode,
                                                      DictValue = x.DictValue,
                                                      SortNum = x.SortNum
                                                  }).ToList()
                                          }).ToListAsync();



                if (skin != null)
                {
                    skin.CanulaItemDto = skinItemDtos;
                    return JsonResult<SkinDto>.Ok(data: skin);
                }
                else
                {
                    //获取上一次皮肤数据
                    var lastSkin = await _icuSkinRepository.Where(s => s.SkinId == SkinId).OrderByDescending(s => s.NurseTime).FirstOrDefaultAsync();
                    SkinDto skinDto = new SkinDto()
                    {
                        SkinId = SkinId,
                        PressArea = lastSkin == null ? nursingSkin.PressArea : lastSkin.PressArea,
                        CanulaItemDto = skinItemDtos
                    };
                    return JsonResult<SkinDto>.Ok(data: skinDto);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<SkinDto>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 查询皮肤观察项
        /// </summary>
        /// <param name="SkinId">皮肤列表ID</param>
        /// <param name="NurseTime">观察时间</param>
        /// <param name="deptCode">科室编码</param>
        /// <param name="Id">皮肤观察护理ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<SkinDto>> SelectSkin(Guid SkinId, DateTime NurseTime, string deptCode, Guid? Id)
        {
            try
            {
                //获取模块代码
                var nursingSkin = await _icuNursingSkinRepository.FindAsync(x => x.Id == SkinId);
                string PI_ID = nursingSkin.PI_ID;
                string modulecode = nursingSkin.ModuleCode;

                //获取皮肤主表
                var skin = await _icuSkinRepository.Where(s => s.Id == Id && s.SkinId == SkinId && s.NurseTime == NurseTime)
                    .Select(s => new SkinDto()
                    {
                        Id = s.Id,
                        SkinId = SkinId,
                        NurseTime = s.NurseTime.ToString("yyyy-MM-dd HH:mm"),
                        PressArea = s.PressArea,
                        NurseId = s.NurseId,
                        NurseName = s.NurseName,
                        CanulaRecord = s.CanulaRecord
                    }).FirstOrDefaultAsync();

                if (string.IsNullOrWhiteSpace(deptCode))
                {
                    return JsonResult<SkinDto>.DataNotFound(msg: "此患者无科室或无此患者信息，请查证后再查询！");
                }

                //多条件拼接扩展
                Expression<Func<IcuParaItem, bool>> predicatePI = PredicateBuilder.True<IcuParaItem>();
                predicatePI = predicatePI.And(s => s.ValidState == 1 && s.IsEnable == true && s.DeptCode == deptCode
                    && s.ModuleCode == modulecode && s.GroupName == "皮肤观察");

                //多条件拼接扩展
                Expression<Func<SkinDynamic, bool>> predicateCD = PredicateBuilder.True<SkinDynamic>();
                if (skin != null)
                {
                    predicateCD = predicateCD.And(s => s.CanulaId == skin.Id);
                }
                else
                {
                    predicateCD = predicateCD.And(s => s.CanulaId == null);
                }

                //获取所有皮肤观察项目
                var skinItemDtos = await (from s in _icuParaItemRepository.Where(predicatePI)
                                          join c in _skinDynamicRepository.Where(predicateCD) on s.ParaCode equals c.ParaCode into cc
                                          from c in cc.DefaultIfEmpty()
                                          orderby s.SortNum
                                          select new CanulaItemDto
                                          {
                                              Id = c.Id,
                                              ParaCode = s.ParaCode,
                                              ParaName = s.DisplayName,
                                              SortNum = s.SortNum,
                                              GroupName = s.GroupName,
                                              Style = s.Style,
                                              DataSource = string.IsNullOrEmpty(c.ParaValue) ? s.DataSource : c.ParaValue,
                                              //管道项下拉框字典值
                                              Dicts = _dictRepository.Where(x => x.ModuleCode == modulecode && x.ParaCode == s.ParaCode && x.ValidState == 1)
                                                  .OrderBy(x => x.SortNum).Select(x => new Dicts
                                                  {
                                                      DictId = x.ParaCode,
                                                      DictName = x.ParaName,
                                                      DictCode = x.DictCode,
                                                      DictValue = x.DictValue,
                                                      SortNum = x.SortNum
                                                  }).ToList()
                                          }).ToListAsync();



                if (skin != null)
                {
                    skin.CanulaItemDto = skinItemDtos;
                    return JsonResult<SkinDto>.Ok(data: skin);
                }
                else
                {
                    //获取上一次皮肤数据
                    var lastSkin = await _icuSkinRepository.Where(s => s.SkinId == SkinId).OrderByDescending(s => s.NurseTime).FirstOrDefaultAsync();
                    SkinDto skinDto = new SkinDto()
                    {
                        SkinId = SkinId,
                        PressArea = lastSkin == null ? nursingSkin.PressArea : lastSkin.PressArea,
                        CanulaItemDto = skinItemDtos
                    };
                    return JsonResult<SkinDto>.Ok(data: skinDto);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<SkinDto>.Fail(msg: ex.Message);
            }
        }


        /// <summary>
        /// 新增或修改皮肤观察
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult<string>> CreateSkin(SkinDto skin)
        {
            try
            {
                //检验时间合法性
                IcuNursingSkin nursingCanula = await _icuNursingSkinRepository.FindAsync(x => x.Id == skin.SkinId);
                string errorMsg = null;

                var finder = await _icuSkinRepository.FindAsync(s => s.Id == skin.Id);
                if (finder == null)
                {
                    Guid guid = _guidGenerator.Create();
                    IcuSkin icuSkin = new IcuSkin(guid)
                    {
                        SkinId = skin.SkinId,
                        NurseTime = skin.NurseTime2,
                        PressArea = skin.PressArea,
                        NurseId = skin.NurseId,
                        NurseName = skin.NurseName,
                        CanulaRecord = skin.CanulaRecord
                    };
                    var entity = await _icuSkinRepository.InsertAsync(icuSkin);

                    //定义动态属性
                    List<SkinDynamic> createSkins = new List<SkinDynamic>();
                    foreach (CanulaItemDto skinItem in skin.CanulaItemDto)
                    {
                        createSkins.Add(new SkinDynamic(_guidGenerator.Create())
                        {
                            CanulaId = guid,
                            GroupName = skinItem.GroupName,
                            ParaCode = skinItem.ParaCode,
                            ParaName = skinItem.ParaName,
                            ParaValue = skinItem.DataSource
                        });
                    }
                    await _skinDynamicRepository.CreateRangeAsync(createSkins);
                    await WriteToNursingRecordAsync(new SkinRecordEto()
                    {
                        Signature = skin.Signature,
                        CanulaRecord = skin.CanulaRecord,
                        NursingCanulaId = entity.Id,
                        OperateTime = Convert.ToDateTime(skin.NurseTime),
                        PiId = Guid.Parse(nursingCanula.PI_ID)
                    });
                    return JsonResult<string>.Ok();
                }
                else
                {
                    //修改皮肤记录主表
                    finder.NurseId = skin.NurseId;
                    finder.NurseName = skin.NurseName;
                    finder.NurseTime = skin.NurseTime2;
                    finder.PressArea = skin.PressArea;
                    finder.CanulaRecord = skin.CanulaRecord;
                    await _icuSkinRepository.UpdateAsync(finder);

                    //定义动态属性
                    List<SkinDynamic> createSkins = new List<SkinDynamic>();
                    List<SkinDynamic> updateSkins = new List<SkinDynamic>();
                    SkinDynamic skinDynamic = new SkinDynamic();
                    var skinDynamics = await _skinDynamicRepository.AsNoTracking().Where(s => s.CanulaId == finder.Id).ToListAsync();
                    foreach (CanulaItemDto skinItem in skin.CanulaItemDto)
                    {
                        //新增动态参数
                        if (skinItem.Id == Guid.Empty)
                        {
                            createSkins.Add(new SkinDynamic(_guidGenerator.Create())
                            {
                                CanulaId = finder.Id,
                                GroupName = skinItem.GroupName,
                                ParaCode = skinItem.ParaCode,
                                ParaName = skinItem.ParaName,
                                ParaValue = skinItem.DataSource
                            });
                        }
                        else
                        {
                            skinDynamic = skinDynamics.Where(s => s.Id == skinItem.Id).FirstOrDefault();
                            skinDynamic.GroupName = skinItem.GroupName;
                            skinDynamic.ParaCode = skinItem.ParaCode;
                            skinDynamic.ParaName = skinItem.ParaName;
                            skinDynamic.ParaValue = skinItem.DataSource;
                            updateSkins.Add(skinDynamic);
                        }
                    }

                    if (createSkins.Any())
                    {
                        await _skinDynamicRepository.CreateRangeAsync(createSkins);
                    }
                    await _skinDynamicRepository.UpdateRangeAsync(updateSkins);

                    await WriteToNursingRecordAsync(new SkinRecordEto()
                    {
                        Signature = skin.Signature,
                        CanulaRecord = skin.CanulaRecord,
                        NursingCanulaId = finder.Id,
                        OperateTime = Convert.ToDateTime(skin.NurseTime),
                        PiId = Guid.Parse(nursingCanula.PI_ID)
                    });
                    return JsonResult<string>.Ok();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<string>.Fail(data: ex.Message + ex.StackTrace);
            }
        }


        /// <summary>
        /// 复制一条数据
        /// </summary>
        /// <param name="copySkinDto">复制dto</param>
        /// <returns></returns>
        [HttpPost]
        //[Route("api/YiJian.BodyParts/NuringSkin/CopySkin")]
        public async Task<JsonResult<string>> CopySkin(CopySkinDto copySkinDto)
        {
            try
            {
                DateTime NurseTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

                //检验时间合法性
                string PI_ID = string.Empty;
                if (copySkinDto.State == 1)
                {
                    var icuSkin = await _icuSkinRepository.FindAsync(s => s.Id == copySkinDto.Id);
                    if (icuSkin == null)
                    {
                        return JsonResult<string>.DataNotFound();
                    }
                    PI_ID = _icuNursingSkinRepository.FindAsync(x => x.Id == icuSkin.SkinId).Result.PI_ID;
                }
                if (copySkinDto.State == 2)
                {
                    PI_ID = _icuNursingSkinRepository.FindAsync(x => x.Id == copySkinDto.Id).Result?.PI_ID;
                    if (string.IsNullOrWhiteSpace(PI_ID))
                    {
                        return JsonResult<string>.DataNotFound();
                    }
                }
                string errorMsg = null;

                //复制上一条
                if (copySkinDto.State == 2)
                {
                    var icuSkin = await _icuSkinRepository.Where(s => s.SkinId == copySkinDto.Id).OrderByDescending(x => x.NurseTime).FirstOrDefaultAsync(); ;
                    copySkinDto.Id = icuSkin.Id;
                }

                //获取皮肤主表
                var skin = await _icuSkinRepository.FindAsync(s => s.Id == copySkinDto.Id);

                if (skin == null)
                {
                    return JsonResult<string>.Fail(500, errorMsg, null);
                }

                Guid skinId = _guidGenerator.Create();
                //复制主护理记录表
                IcuSkin Skin = new IcuSkin(skinId)
                {
                    SkinId = skin.SkinId,
                    NurseTime = NurseTime,
                    PressArea = skin.PressArea,
                    NurseId = copySkinDto.Staffcode,
                    NurseName = copySkinDto.StaffName,
                    CanulaRecord = skin.CanulaRecord,
                };

                //多条件拼接扩展
                Expression<Func<SkinDynamic, bool>> predicateCD = PredicateBuilder.True<SkinDynamic>();
                predicateCD = predicateCD.And(s => s.CanulaId == skin.Id);

                //获取所有皮肤属性项目
                var skinItemDtos = await _skinDynamicRepository.Where(predicateCD).ToListAsync();

                //复制附表
                List<SkinDynamic> skinDynamics = new List<SkinDynamic>();
                foreach (SkinDynamic skinDynamic in skinItemDtos)
                {
                    skinDynamics.Add(new SkinDynamic(_guidGenerator.Create())
                    {
                        CanulaId = skinId,
                        GroupName = skinDynamic.GroupName,
                        ParaCode = skinDynamic.ParaCode,
                        ParaName = skinDynamic.ParaName,
                        ParaValue = skinDynamic.ParaValue
                    });
                }
                await _icuSkinRepository.InsertAsync(Skin);
                await _skinDynamicRepository.CreateRangeAsync(skinDynamics);

                await WriteToNursingRecordAsync(new SkinRecordEto()
                {
                    Signature = copySkinDto.Signature,
                    CanulaRecord = skin.CanulaRecord,
                    NursingCanulaId = skinId,
                    OperateTime = Skin.NurseTime,
                    PiId = Guid.Parse(PI_ID)
                });
                return JsonResult<string>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<string>.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        #endregion

        #endregion

        #region 旧版本 固定表格

        /// <summary>
        /// 根据参数名称获取参数字典（旧）
        /// </summary>
        /// <param name="paraname"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<SkinPartDto>>> SeleteSkinDictList(string paraname)
        {
            try
            {
                if (paraname == "压疮部位")
                {
                    var dictList = await _dictCanulaPartRepository.Where(x => x.DeptCode == "system" && x.ModuleCode == "ycbw").Select(x => new SkinPartDto { DictValue = x.PartName, PartNumber = x.PartNumber }).ToListAsync();

                    return JsonResult<List<SkinPartDto>>.Ok(data: dictList);
                }
                else
                {
                    var dictList = await (from s in _icuParaModuleRepository.Where(x => x.DeptCode == "system" && x.ModuleName == "皮肤" && x.ValidState == 1)
                                          join c in _icuParaItemRepository.Where(x => x.ParaName == paraname && x.ValidState == 1) on s.ModuleCode equals c.ModuleCode
                                          join d in _dictRepository.Where(x => x.ValidState == 1) on new { c.ModuleCode, c.ParaCode } equals new { d.ModuleCode, d.ParaCode }
                                          select new SkinPartDto { DictCode = d.DictCode, DictValue = d.DictValue, IsDefault = d.IsDefault }).ToListAsync();

                    return JsonResult<List<SkinPartDto>>.Ok(data: dictList);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<List<SkinPartDto>>.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 查询压疮（旧）
        /// </summary>
        /// <param name="PI_ID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<IcuNursingSkinDto>>> SelectNursingSkinList([Required] string PI_ID)
        {
            try
            {
                var parts = await _dictCanulaPartRepository.Where(x => x.DeptCode == "system" && x.ModuleCode == "ycbw").Select(x => new { x.PartName, x.PartNumber }).ToListAsync();

                var nursingSkins = await _icuNursingSkinRepository.Where(a => a.PI_ID == PI_ID && a.ValidState == 1).ToListAsync();

                var nursingSkinDtos = ObjectMapper.Map<List<IcuNursingSkin>, List<IcuNursingSkinDto>>(nursingSkins);
                foreach (IcuNursingSkinDto icuNursingSkinDto in nursingSkinDtos)
                {
                    icuNursingSkinDto.NurseTime = Convert.ToDateTime(icuNursingSkinDto.NurseTime.ToString("G"));
                    icuNursingSkinDto.Days = DateTime.Now.Subtract(icuNursingSkinDto.NurseTime).Days + 1;
                    icuNursingSkinDto.PartNumber = parts.Where(x => x.PartName == icuNursingSkinDto.PressPart).Select(x => x.PartNumber).FirstOrDefault();
                }

                return JsonResult<List<IcuNursingSkinDto>>.Ok(data: nursingSkinDtos);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<List<IcuNursingSkinDto>>.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 新增或修改压疮（旧）
        /// </summary>
        /// <param name="nursingSkinDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CreateNursingSkinInfo(CreateUpdateIcuNursingSkinDto nursingSkinDto)
        {
            try
            {
                IcuNursingSkin icuNursingSkin;

                var finder = nursingSkinDto.Id == Guid.Empty ? null : await _icuNursingSkinRepository.FindAsync(a => a.Id == nursingSkinDto.Id && a.ValidState == 1);

                if (finder != null)
                {
                    icuNursingSkin = ObjectMapper.Map(nursingSkinDto, finder);
                    await _icuNursingSkinRepository.UpdateAsync(icuNursingSkin);
                    return JsonResult.Ok();
                }
                else
                {
                    nursingSkinDto.Id = _guidGenerator.Create();
                    icuNursingSkin = ObjectMapper.Map<CreateUpdateIcuNursingSkinDto, IcuNursingSkin>(nursingSkinDto);
                    icuNursingSkin.ValidState = 1;

                    await _icuNursingSkinRepository.InsertAsync(icuNursingSkin, true);
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
        /// 删除压疮
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        //[Route("api/YiJian-BodyParts/NuringSkin/DeleteNursingSkinInfo")]
        public async Task<JsonResult> DeleteNursingSkinInfo(Guid id)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                var nursingSkin = id == Guid.Empty ? null : await _icuNursingSkinRepository.FindAsync(s => s.Id == id && s.ValidState == 1);
                var skinids = new List<Guid>();
                if (nursingSkin != null)
                {
                    nursingSkin.ValidState = 0;
                    await _icuNursingSkinRepository.UpdateAsync(nursingSkin);
                    var icuSkinList = _icuSkinRepository.Where(s => s.SkinId == id).ToList();
                    if (icuSkinList.Any())
                    {
                        icuSkinList.ForEach(async p =>
                        {
                            await _icuSkinRepository.DeleteAsync(p);
                        });
                        skinids = icuSkinList.Select(p => p.Id).ToList();
                        skinids.Add(id);
                    }
                    skinids.Add(id);
                    await DeleteNursingRecordAsync(skinids);
                    await uow.SaveChangesAsync();
                    await uow.CompleteAsync();
                    return JsonResult.Ok();
                }
                else { return JsonResult.DataNotFound("要删除的压疮部位不存在！"); }
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 查询压疮观察护理（旧）
        /// </summary>
        /// <param name="SkinId"></param>
        /// <param name="state">状态：0：今天（按班次），1：自定义（自然日）</param>
        /// <param name="ScheduleCode">班次代码</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        [HttpGet]
        //[Route("api/YiJian.BodyParts/NuringSkin/SelectSkinList")]
        public async Task<JsonResult<List<IcuSkinDto>>> SelectSkinList(Guid SkinId, int state, string ScheduleCode, DateTime? startTime, DateTime? endTime)
        {
            try
            {
                //获取患者流水号
                var nursingSkin = SkinId == Guid.Empty ? null : await _icuNursingSkinRepository.FindAsync(x => x.Id == SkinId);

                if (nursingSkin == null) { return JsonResult<List<IcuSkinDto>>.Ok(); }

                Expression<Func<IcuSkin, bool>> predicateIS = PredicateBuilder.True<IcuSkin>();
                predicateIS = predicateIS.And(s => s.SkinId == SkinId);

                //按自然日查询
                if (state == 1 && startTime != null && startTime.HasValue)
                {
                    predicateIS = predicateIS.And(s => s.NurseTime >= startTime);
                }
                if (state == 1 && endTime != null && endTime.HasValue)
                {
                    predicateIS = predicateIS.And(s => s.NurseTime <= endTime);
                }

                ////按班次查询
                //if (state == 0)
                //{
                //    DateTime scheduleTime = await _icuDeptScheduleRepository.CurrentTime(nursingSkin.PI_ID);
                //    List<DateTime> dateTimes = await _icuDeptScheduleRepository.ScheduleTimes(nursingSkin.PI_ID, ScheduleCode, scheduleTime);
                //    predicateIS = predicateIS.And(s => s.NurseTime >= dateTimes[0] && s.NurseTime <= dateTimes[1]);
                //}
                var skins = await _icuSkinRepository.Where(predicateIS).ToListAsync();

                var skinDtos = ObjectMapper.Map<List<IcuSkin>, List<IcuSkinDto>>(skins);

                return JsonResult<List<IcuSkinDto>>.Ok(data: skinDtos);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult<List<IcuSkinDto>>.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 新增或修改压疮观察护理（旧）
        /// </summary>
        /// <param name="skinDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CreateSkinInfo(CreateUpdateIcuSkinDto skinDto)
        {
            try
            {
                IcuSkin icuSkin;

                var finder = skinDto.Id == Guid.Empty ? null : await _icuSkinRepository.FindAsync(a => a.Id == skinDto.Id);

                if (finder != null)
                {
                    icuSkin = ObjectMapper.Map(skinDto, finder);
                    await _icuSkinRepository.UpdateAsync(icuSkin);
                    return JsonResult.Ok();
                }
                else
                {
                    skinDto.Id = _guidGenerator.Create();
                    icuSkin = ObjectMapper.Map<CreateUpdateIcuSkinDto, IcuSkin>(skinDto);

                    await _icuSkinRepository.InsertAsync(icuSkin, true);
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
        /// 复制一条皮肤护理数据（旧）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route("api/YiJian.BodyParts/NuringSkin/CopySkinInfo")]
        public async Task<JsonResult> CopySkinInfo(Guid id)
        {
            try
            {
                IcuSkin icuSkin;

                var finder = id == Guid.Empty ? null : await _icuSkinRepository.FindAsync(a => a.Id == id);

                if (finder != null)
                {
                    var icuSkinDto = ObjectMapper.Map<IcuSkin, CreateUpdateIcuSkinDto>(finder);
                    icuSkinDto.Id = _guidGenerator.Create();
                    icuSkin = ObjectMapper.Map<CreateUpdateIcuSkinDto, IcuSkin>(icuSkinDto);
                    icuSkin.NurseTime = DateTime.Now;
                    await _icuSkinRepository.InsertAsync(icuSkin, true);
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
        /// 删除压疮观察护理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        //[Route("api/YiJian-BodyParts/NuringSkin/DeleteSkinInfo")]
        public async Task<JsonResult> DeleteSkinInfo(Guid id)
        {
            try
            {
                var skin = id == Guid.Empty ? null : await _icuSkinRepository.FindAsync(s => s.Id == id);

                if (skin != null)
                {
                    await _icuSkinRepository.DeleteAsync(skin);
                    await DeleteNursingRecordAsync(new List<Guid>() { skin.Id });
                    return JsonResult.Ok();
                }
                else { return JsonResult.DataNotFound("要删除的压疮观察记录不存在！"); }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        #endregion

        /// <summary>
        /// 皮肤记录列表方法
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        private static List<CanulaItemDtos> Dynamic(SkinDto skin)
        {
            List<CanulaItemDtos> canulaItemDtos = new List<CanulaItemDtos>();
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "Id",
                ParaName = "主键",
                ParaValue = skin.Id.ToString(),
            });
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "NurseTime",
                ParaName = "观察时间",
                ParaValue = skin.NurseTime.ToString(),
            });
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "NurseName",
                ParaName = "观察人",
                ParaValue = skin.NurseName,
            });
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "PressArea",
                ParaName = "大小(cm)",
                ParaValue = skin.PressArea,
            });
            return canulaItemDtos;
        }

        /// <summary>
        /// 皮肤记录标题方法
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
                ParaName = "观察时间",
            });
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "NurseName",
                ParaName = "观察人",
            });
            canulaItemDtos.Add(new CanulaItemDtos()
            {
                ParaCode = "PressArea",
                ParaName = "大小(cm)"
            });
            return canulaItemDtos;
        }


        /// <summary>
        /// 根据模块名称查询模块code
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<string>> SelectParaModuleCode([Required] string moduleName)
        {
            throw new Exception("11");
            var paraModule = await _icuParaModuleRepository.FirstOrDefaultAsync(a => a.ModuleName.Contains(moduleName));
            return JsonResult<string>.Ok(data: paraModule.ModuleCode);
        }

        /// <summary>
        /// 同步皮肤记录到护理记录单
        /// </summary>
        /// <param name="skinRecordEto"></param>
        /// <returns></returns>
        private async Task WriteToNursingRecordAsync(SkinRecordEto skinRecordEto)
        {
            if (skinRecordEto == null)
            {
                return;
            }
            else if (string.IsNullOrEmpty(skinRecordEto.CanulaRecord))
            {
                await DeleteNursingRecordAsync(new List<Guid>() { skinRecordEto.NursingCanulaId });
                return;
            }
            skinRecordEto.OperateCode = CurrentUser.FindClaimValue("name");
            skinRecordEto.OperateName = CurrentUser.FindClaimValue("fullName");
            await _capPublisher.PublishAsync("nursingrecord.to.nursingreport", skinRecordEto);
        }

        /// <summary>
        /// 删除护理记录单记录
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        private async Task DeleteNursingRecordAsync(IEnumerable<Guid> guids, int? eventType = null)
        {
            List<SkinRecordEto> skinRecordEtos = new List<SkinRecordEto>();
            skinRecordEtos.AddRange(guids.Select(p =>
            {
                return new SkinRecordEto() { NursingCanulaId = p, EventType = eventType };
            }));
            await _capPublisher.PublishAsync("deleterecord.to.nursingreport", skinRecordEtos);
        }
    }
}