using YiJian.BodyParts.Dtos;
using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using YiJian.BodyParts.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Linq.Expressions;
using YiJian.BodyParts.Domain.Shared.Const;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Volo.Abp.Guids;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Users;

namespace YiJian.BodyParts.Service
{
    /// <summary>
    /// 表:护理记录表
    /// </summary>
    [DbDescription("护理记录表")]
    [Authorize]
    public class IcuNursingEventAppService : ApplicationService, IIcuNursingEventAppService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IIcuNursingEventRepository _icuNursingEventRepository;
        //private readonly IIcuDeptScheduleRepository _icuDeptScheduleRepository;
        private readonly IIcuSysParaRepository _icuSysParaRepository;
        private readonly IIcuSignatureRepository _signatureRepository;

        public IcuNursingEventAppService(IGuidGenerator guidGenerator,
            IIcuNursingEventRepository icuNursingEventRepository,
            //IIcuDeptScheduleRepository icuDeptScheduleRepository,
            IHttpContextAccessor httpContextAccessor,
            IIcuSysParaRepository icuSysParaRepository,
            IIcuSignatureRepository signatureRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _guidGenerator = guidGenerator;
            _icuNursingEventRepository = icuNursingEventRepository;
            //_icuDeptScheduleRepository = icuDeptScheduleRepository;
            _icuSysParaRepository = icuSysParaRepository;
            _signatureRepository = signatureRepository;
        }

        #region 服务接口实现
        /// <summary>
        /// 根据条件查询护理记录
        /// </summary>
        /// <param name="PI_ID">患者id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult<List<IcuNursingEventDto>>> SelectIcuNursingEventList([Required] string PI_ID)
        {
            ////班次日期为空默认为当前班次日期、班次代码
            //if (ScheduleTime == null || ScheduleTime == DateTime.MinValue)
            //{
            //    ScheduleTime = await _icuDeptScheduleRepository.CurrentTime(PI_ID);
            //}

            //List<DateTime> dateTimes = await _icuDeptScheduleRepository.ScheduleTimes(PI_ID, ScheduleCode, ScheduleTime);

            List<IcuNursingEvent> events = await _icuNursingEventRepository.GetIcuNursingEvents(PI_ID);

            var eventDtos = ObjectMapper.Map<List<IcuNursingEvent>, List<IcuNursingEventDto>>(events);

            return JsonResult<List<IcuNursingEventDto>>.Ok(data: eventDtos);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="icuNursingEventDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CreateIcuNursingEventInfo(CreateUpdateIcuNursingEventDto icuNursingEventDto)
        {
            try
            {
                //校验工号
                if (string.IsNullOrWhiteSpace(icuNursingEventDto.NurseCode))
                {
                    return JsonResult.Fail("请检查记录人！", null);
                }

                //校验签名
                if (string.IsNullOrWhiteSpace(icuNursingEventDto.Signature))
                {
                    return JsonResult.Fail("签名为空，请重新保存或重新登录！", null);
                }

                //判断是否再次新增护理记录,记录时间是否大于出科时间
                int count = await _icuNursingEventRepository.Where(x => x.PI_ID == icuNursingEventDto.PI_ID && x.NurseTime == icuNursingEventDto.RecordTime2 && x.ValidState == 1).CountAsync();
                if (icuNursingEventDto.IsPopup == false && count > 0)
                {
                    return JsonResult.Write(code: 202, "已有相同护理时间的记录，是否再次新增！", null);
                }

                //签名
                Signature signature = new Signature()
                {
                    SignNurseCode = icuNursingEventDto.NurseCode,
                    SignNurseName = icuNursingEventDto.NurseName,
                    SignImage = icuNursingEventDto.Signature
                };
                Guid guid = await _signatureRepository.GetSignatureId(signature);

                icuNursingEventDto.Id = _guidGenerator.Create();
                icuNursingEventDto.AuditState = 0;
                IcuNursingEvent icuNursingEvent = ObjectMapper.Map<CreateUpdateIcuNursingEventDto, IcuNursingEvent>(icuNursingEventDto);
                icuNursingEvent.SignatureId = guid;

                await _icuNursingEventRepository.InsertAsync(icuNursingEvent, true);

                return JsonResult.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 审核护理记录
        /// </summary>
        /// <param name="icuNursingEventDtos"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<JsonResult> UpdateIcuNursingEventList(List<CreateUpdateIcuNursingEventDto> icuNursingEventDtos)
        {
            try
            {


                List<IcuNursingEvent> icuNursingEvents = new List<IcuNursingEvent>();
                foreach (CreateUpdateIcuNursingEventDto icuNursingEventDto in icuNursingEventDtos)
                {
                    var finder = icuNursingEventDto.Id == Guid.Empty ? null : await _icuNursingEventRepository.FindAsync(a => a.Id == icuNursingEventDto.Id && a.ValidState == 1);
                    if (finder != null)
                    {
                        IcuNursingEvent icuNursingEvent;

                        //取消审核
                        if (icuNursingEventDto.AuditState == 2)
                        {
                            icuNursingEventDto.AuditNurseCode = null;
                            icuNursingEventDto.AuditNurseName = null;
                            icuNursingEventDto.AuditTime = null;
                            icuNursingEventDto.AuditState = 0;
                            icuNursingEventDto.AuditSignatureId = null;

                            //删除签名
                            await _signatureRepository.Remove(finder.AuditSignatureId.Value);
                        }

                        //映射
                        icuNursingEvent = ObjectMapper.Map(icuNursingEventDto, finder);

                        //审核
                        if (icuNursingEventDto.AuditState == 1)
                        {
                            //签名
                            Signature signature = new Signature()
                            {
                                SignNurseCode = icuNursingEventDto.AuditNurseCode,
                                SignNurseName = icuNursingEventDto.AuditNurseName,
                                SignImage = icuNursingEventDto.Signature
                            };
                            Guid guid = await _signatureRepository.GetSignatureId(signature);
                            icuNursingEvent.AuditSignatureId = guid;
                        }

                        //修改班次时间
                        icuNursingEvent.NurseDate = icuNursingEventDto.RecordTime2.Date;
                        icuNursingEvent.NurseTime = icuNursingEventDto.RecordTime2;
                        icuNursingEvent.RecordTime = DateTime.Now;
                        icuNursingEvents.Add(icuNursingEvent);
                    }
                }
                if (icuNursingEvents.Any())
                {
                    await _icuNursingEventRepository.UpdateRangeAsync(icuNursingEvents);
                    return JsonResult.Ok();
                }
                else
                {
                    return JsonResult.DataNotFound();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 修改护理记录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<JsonResult<string>> UpdateNursingEvent(CreateUpdateIcuNursingEventDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                Logger.LogError("Id is null or empty");
                return JsonResult<string>.Fail(500, "参数错误：Id字段缺失！", null);
            }

            //1、校验重复护理记录：判断是否再次新增护理记录
            int count = await _icuNursingEventRepository.Where(x => x.Id != dto.Id && x.PI_ID == dto.PI_ID && x.NurseTime == dto.RecordTime2 && x.ValidState == 1).CountAsync();
            if (dto.IsPopup == false && count > 0)
            {
                return JsonResult<string>.Write(code: 202, "已有相同护理时间的记录，是否再次新增！", null);
            }

            string errorMsg = null;

            //3、获取当前登录用户信息
            string currentStaffcode = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;

            var nursingEvent = await _icuNursingEventRepository.AsNoTracking().FirstOrDefaultAsync(a => a.Id == dto.Id && a.ValidState == 1);
            if (nursingEvent != null)
            {
                nursingEvent.Context = dto.Context;
                nursingEvent.SkinDescription = dto.SkinDescription;
                nursingEvent.Measure = dto.Measure;
                //选择记录时间
                nursingEvent.NurseDate = dto.RecordTime2.Date;
                nursingEvent.NurseTime = dto.RecordTime2;
                //实际记录时间
                nursingEvent.RecordTime = DateTime.Now;

                #region 验证修改权限
                AuthorityEnum f = await _icuSysParaRepository.GetNursingAuthority(currentStaffcode, nursingEvent.NurseCode);
                if (f == AuthorityEnum.session过期)
                {
                    return JsonResult<string>.Fail(500, "session过期，请重新登录！", null);
                }
                if (f == AuthorityEnum.您无权限修改)
                {
                    return JsonResult<string>.Fail(500, "您无权限修改！", null);
                }
                #endregion

                await _icuNursingEventRepository.UpdateAsync(nursingEvent);

                //更新记录人
                nursingEvent.NurseCode = dto.NurseCode;
                nursingEvent.NurseName = dto.NurseName;

                //更新签名
                Signature signature = new Signature()
                {
                    SignatureId = nursingEvent.SignatureId.Value,
                    SignNurseCode = nursingEvent.AuditNurseCode,
                    SignNurseName = nursingEvent.AuditNurseName,
                    SignImage = dto.Signature
                };
                Guid guid = await _signatureRepository.GetSignatureId(signature);

                return JsonResult<string>.Ok();
            }
            else
            {
                Logger.LogError($"can not get NursingEvent by id = {dto.Id}");
                return JsonResult<string>.Fail(500, "护量记录不存在！", null);
            }
        }

        /// <summary>
        /// 删除一条护理记录
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<JsonResult> DeleteIcuNursingEventInfo(Guid guid)
        {
            try
            {
                // 获取当前登录用户信息
                string currentStaffcode = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;
                if (string.IsNullOrEmpty(currentStaffcode))
                {
                    Logger.LogError("can not get staffCode from HttpContext");
                    return JsonResult.Fail("session过期，请重新登录！");
                }

                var finder = guid == Guid.Empty ? null : await _icuNursingEventRepository.FindAsync(s => s.Id == guid && s.ValidState == 1);
                if (finder != null)
                {
                    List<string> canEditStaffCodes = await _icuSysParaRepository.getListValue(Constants.paraCodeCanModifyNursingRecordStaffCodes);
                    if (canEditStaffCodes != null && canEditStaffCodes.Count > 0)
                    {
                        // 只有配置了编辑权限的人才可以删除别人的数据，否则只能由本人删除
                        if (canEditStaffCodes.Contains(currentStaffcode) || finder.NurseCode == currentStaffcode)
                        {
                            finder.ValidState = 0;
                            await _icuNursingEventRepository.UpdateAsync(finder);
                        }
                        else
                        {
                            Logger.LogError($"staff {currentStaffcode} can not Permission to delete nursingRecord");
                            return JsonResult.Fail("您无删除权限！");
                        }
                    }
                    else
                    {
                        // 如果没有配置权限，则只能删除自已的数据
                        if (finder.NurseCode != currentStaffcode)
                        {
                            Logger.LogError($"currentStaff [{currentStaffcode}] can not Permission delete others recordEvent");
                            return JsonResult.Fail("您无删除权限！");
                        }
                        else
                        {
                            finder.ValidState = 0;
                            await _icuNursingEventRepository.UpdateAsync(finder);
                        }
                    }

                    //删除签名
                    if (finder.SignatureId != null)
                    {
                        await _signatureRepository.Remove(finder.SignatureId.Value);
                    }

                    return JsonResult.Ok();
                }
                else { return JsonResult.DataNotFound("要删除的护理记录不存在！"); }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return JsonResult.Fail(data: ex.Message + ex.StackTrace);
            }
        }
        #endregion
    }
}
