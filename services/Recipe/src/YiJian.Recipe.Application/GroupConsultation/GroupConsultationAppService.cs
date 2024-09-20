using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;
using YiJian.Documents;
using YiJian.Documents.Dto;
using YiJian.ECIS.ShareModel.Etos.EMRs;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.Recipe;
using YiJian.Recipes.GroupConsultation;
using YiJian.Recipes.InviteDoctor;

namespace YiJian.GroupConsultation
{
    /// <summary>
    /// 会诊管理 API
    /// </summary>
    [Authorize]
    public class GroupConsultationAppService : RecipeAppService, IGroupConsultationAppService
    {
        private readonly GroupConsultationManager _groupConsultationManager;
        private readonly IGroupConsultationRepository _groupConsultationRepository;
        private readonly IInviteDoctorRepository _inviteDoctorRepository;
        private readonly IDoctorSummaryRepository _iDoctorSummaryRepository;
        private readonly DocumentsAppService _documentsAppService;
        private readonly ICapPublisher _capPublisher;
        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groupConsultationRepository"></param>
        /// <param name="groupConsultationManager"></param>
        /// <param name="inviteDoctorRepository"></param>
        /// <param name="capPublisher"></param>
        /// <param name="iDoctorSummaryRepository"></param>
        /// <param name="documentsAppService"></param>
        public GroupConsultationAppService(IGroupConsultationRepository groupConsultationRepository,
            GroupConsultationManager groupConsultationManager, IInviteDoctorRepository inviteDoctorRepository, ICapPublisher capPublisher, IDoctorSummaryRepository iDoctorSummaryRepository, DocumentsAppService documentsAppService)
        {
            _groupConsultationRepository = groupConsultationRepository;
            _groupConsultationManager = groupConsultationManager;
            _inviteDoctorRepository = inviteDoctorRepository;
            _capPublisher = capPublisher;
            _iDoctorSummaryRepository = iDoctorSummaryRepository;
            _documentsAppService = documentsAppService;
        }

        #endregion constructor

        #region Create

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Guid> CreateAsync(GroupConsultationUpdate input)
        {
            try
            {
                var consultation = await (await _groupConsultationRepository.GetQueryableAsync()).Include(i => i.InviteDoctors)
                    .Include(i => i.DoctorSummarys)
                    .FirstOrDefaultAsync(x => x.Id == input.Id);
                var id = GuidGenerator.Create();
                if (input.InviteDoctors == null && input.InviteDoctors.Count <= 0)
                {
                    throw new EcisBusinessException(message: "邀请医生必填");
                }

                if (consultation == null)
                {
                    var inviteList = new List<InviteDoctor>();
                    //院前会诊申请人也算作受邀医生
                    if (input.TypeCode == "PreHospitalFirstAid")
                    {
                        var model = await (await _groupConsultationRepository.GetQueryableAsync()).Include(i => i.InviteDoctors)
                            .FirstOrDefaultAsync(x => x.Status == GroupConsultationStatus.已开始 && x.InviteDoctors.Any(
                                a =>
                                    a.Code == input.ApplyCode && a.CheckInStatus != CheckInStatus.已报到));
                        if (model != null)
                        {
                            throw new EcisBusinessException(message: "您正在会诊中，无法发起", data: model.Id.ToString());
                        }

                        inviteList.Add(new InviteDoctor(GuidGenerator.Create(), id, input.ApplyCode,
                            input.ApplyName, input.ApplyDeptCode, input.ApplyDeptName, CheckInStatus.已报到, DateTime.Now,
                            "", "", input.Mobile));
                    }

                    foreach (var item in input.InviteDoctors)
                    {
                        if (inviteList.Any(x => x.Code == item.Code && item.Code != "not"))
                        {
                            continue;
                        }

                        item.GroupConsultationId = id;
                        item.Id = GuidGenerator.Create();

                        inviteList.Add(ObjectMapper.Map<InviteDoctorUpdate, InviteDoctor>(item));
                    }

                    var applyTime = DateTime.Now;
                    var groupConsultation = await _groupConsultationManager.CreateAsync(pIID: input.PI_ID, // 分诊患者id
                        patientId: input.PatientId, // 患者id
                        typeCode: input.TypeCode, // 会诊类型
                        typeName: input.TypeName,
                        startTime: input.StartTime, // 会诊开始时间
                        status: input.Status, // 会诊状态
                        objectiveCode: input.ObjectiveCode, // 会诊目的编码
                        objectiveContent: input.ObjectiveContent, // 会诊目的内容
                        applyDeptCode: input.ApplyDeptCode, // 申请科室编码
                        applyDeptName: input.ApplyDeptName, // 申请科室名称
                        applyCode: input.ApplyCode, // 申请人编码
                        applyName: input.ApplyName, // 申请人名称
                        mobile: input.Mobile, // 联系方式
                        applyTime: applyTime, // 申请时间
                        place: input.Place, // 地点
                        vitalSigns: input.VitalSigns, // 生命体征
                        test: input.Test, // 检验
                        inspect: input.Inspect, // 检查
                        doctorOrder: input.DoctorOrder, // 医嘱
                        diagnose: input.Diagnose, // 诊断
                        content: input.Content, // 病历摘要
                        summary: input.Summary, // 总结
                        inviteDoctors: inviteList // 会诊邀请医生
                    );
                    //院前推送状态
                    if (input.TypeCode == "PreHospitalFirstAid")
                    {
                        var sync = new SyncTaskConsultationStatusDto()
                        {
                            Status = input.Status,
                            PIId = input.PI_ID
                        };
                        await _capPublisher.PublishAsync("sync.task.consultation.status.from.recipe", sync);
                    }
                    //急诊需要推送时间节点
                    else
                    {
                        //同步会诊申请时间
                        await _capPublisher.PublishAsync("sync.timeAxisRecord.to.patient", new CreateTimeAxisRecordDto()
                        {
                            TimePointCode = 21,
                            Time = applyTime,
                            PI_ID = input.PI_ID
                        });
                        //同步会诊开始时间
                        await _capPublisher.PublishAsync("sync.timeAxisRecord.to.patient", new CreateTimeAxisRecordDto()
                        {
                            TimePointCode = 22,
                            Time = input.StartTime,
                            PI_ID = input.PI_ID
                        });
                        var patientInfo = await _documentsAppService.GetPatientInfoAsync(input.PI_ID);
                        var bindEto = new BindConsultationRecordEto()
                        {
                            VisitNo = patientInfo.VisitNo.ToString(),
                            RegisterSerialNo = patientInfo.RegisterNo,
                            Piid = patientInfo.PI_ID,
                            PatientNo = patientInfo.PatientID,
                            PatientName = patientInfo.PatientName,
                            AdmissionTime = patientInfo.TriageTime,
                            DeptCode = input.ApplyDeptCode,
                            DeptName = input.ApplyDeptName,
                            DoctorCode = input.ApplyCode,
                            DoctorName = input.ApplyName,
                            Data = new ConsultationRecordDataEto(),
                            Diagnosis = patientInfo.DiagnoseName
                        };
                        var patientEto = ObjectMapper.Map<AdmissionRecordDto, PatientInfoEto>(patientInfo);

                        //病情简要包括 主诉+ 现病史 
                        var consultationResume = $"患者因[{patientInfo.NarrationName}]就诊，[{patientInfo.PresentMedicalHistory}]，检查结果是[ ]，目前考虑[ ]，请{input.ApplyDeptName}科会诊协助诊治。" + groupConsultation.Content;

                        var medicalInfoEto = new MedicalInfoEto()
                        {
                            ConsultationApplyTime = groupConsultation.ApplyTime.Value.ToString("yyyy年MM月dd日 HH时mm分"),
                            ConsultationDept = input.JoinDepts(), //groupConsultation.ApplyDeptName, 
                            ConsultationCategory = groupConsultation.TypeName,
                            ConsultationResume = consultationResume,
                            ConsultationTime = groupConsultation.StartTime.ToString("yyyy年MM月dd日 HH时mm分"),
                            ConsultationRecord = groupConsultation.Summary
                        };
                        var inviteDepartments = input.InviteDoctors
                            .Select(s => new { s.DeptCode, s.DeptName })
                            .Distinct()
                            .ToList();
                        Dictionary<string, string> inviteDepartmentDic = new();
                        inviteDepartments.ForEach(x =>
                        {
                            if (!inviteDepartmentDic.ContainsKey(x.DeptCode))
                            {
                                inviteDepartmentDic.Add(x.DeptCode, x.DeptName);
                            }
                        });

                        bindEto.Data.PatientInfo = patientEto;
                        bindEto.Data.MedicalInfo = medicalInfoEto;
                        bindEto.Data.InviteDepartmentDic = inviteDepartmentDic;
                        //同步会诊记录到电子病历
                        await _capPublisher.PublishAsync("yijian.emr.bindConsultationRecord", bindEto);
                    }

                    return groupConsultation.Id;
                }


                if (consultation.Status == GroupConsultationStatus.已完结)
                {
                    throw new EcisBusinessException(message: "已完结无法修改");
                }

                #region 会诊邀请医生

                var inviteDoctor = new List<InviteDoctor>();

                foreach (var item in input.InviteDoctors)
                {
                    if (inviteDoctor.Any(x => x.Code == item.Code))
                    {
                        continue;
                    }

                    if (item.Id == Guid.Empty)
                    {
                        item.GroupConsultationId = id;
                        item.Id = GuidGenerator.Create();
                        inviteDoctor.Add(ObjectMapper.Map<InviteDoctorUpdate, InviteDoctor>(item));
                    }
                    else
                    {
                        var invite = consultation.InviteDoctors.FirstOrDefault(x => x.Id == item.Id);
                        if (invite == null)
                        {
                            item.GroupConsultationId = id;
                            item.Id = GuidGenerator.Create();
                            inviteDoctor.Add(ObjectMapper.Map<InviteDoctorUpdate, InviteDoctor>(item));
                        }
                        else
                        {
                            invite.ModifyDoctor(code: item.Code,
                                name: item.Name,
                                deptCode: item.DeptCode,
                                deptName: item.DeptName,
                                doctorTitle: item.DoctorTitle,
                                checkInStatus: item.CheckInStatus, // 状态，0：已邀请，1：已报到
                                checkInTime: item.CheckInTime, // 报道时间
                                opinion: item.Opinion, item.Mobile // 意见
                            );
                            inviteDoctor.Add(invite);
                        }
                    }
                }

                var deleteDoctor = new List<InviteDoctor>();

                consultation.InviteDoctors.ForEach(x =>
                {
                    if (input.InviteDoctors.All(a => a.Id != x.Id))
                    {
                        deleteDoctor.Add(x);
                    }
                });

                #endregion

                #region 会诊纪要医生

                var doctorSummary = new List<DoctorSummary>();
                foreach (var item in input.DoctorSummarys)
                {
                    if (doctorSummary.Any(x => x.Code == item.Code))
                    {
                        continue;
                    }

                    if (item.Id == Guid.Empty)
                    {
                        item.GroupConsultationId = id;
                        item.Id = GuidGenerator.Create();

                        doctorSummary.Add(ObjectMapper.Map<DoctorSummaryUpdate, DoctorSummary>(item));
                    }
                    else
                    {
                        var summary = consultation.DoctorSummarys.FirstOrDefault(x => x.Id == item.Id);
                        if (summary == null)
                        {
                            item.GroupConsultationId = id;
                            item.Id = GuidGenerator.Create();
                            doctorSummary.Add(ObjectMapper.Map<DoctorSummaryUpdate, DoctorSummary>(item));
                        }
                        else
                        {
                            summary.Modify(code: item.Code,
                                name: item.Name,
                                deptCode: item.DeptCode,
                                deptName: item.DeptName,
                                doctorTitle: item.DoctorTitle,
                                checkInTime: item.CheckInTime, // 报道时间
                                opinion: item.Opinion, item.Mobile // 意见
                            );
                            doctorSummary.Add(summary);
                        }
                    }
                }

                var deleteSummary = new List<DoctorSummary>();
                consultation.DoctorSummarys.ForEach(x =>
                {
                    if (input.DoctorSummarys.All(a => a.Id != x.Id))
                    {
                        deleteSummary.Add(x);
                    }
                });

                #endregion

                consultation.Modify(patientId: input.PatientId, // 患者id
                    typeCode: input.TypeCode, // 会诊类型
                    typeName: input.TypeName,
                    startTime: input.StartTime, // 会诊开始时间
                    status: input.Status, // 会诊状态
                    objectiveCode: input.ObjectiveCode, // 会诊目的编码
                    objectiveContent: input.ObjectiveContent, // 会诊目的内容
                    applyDeptCode: input.ApplyDeptCode, // 申请科室编码
                    applyDeptName: input.ApplyDeptName, // 申请科室名称
                    applyCode: input.ApplyCode, // 申请人编码
                    applyName: input.ApplyName, // 申请人名称
                    mobile: input.Mobile, // 联系方式
                    applyTime: input.ApplyTime, // 申请时间
                    place: input.Place, // 地点
                    vitalSigns: input.VitalSigns, // 生命体征
                    test: input.Test, // 检验
                    inspect: input.Inspect, // 检查
                    doctorOrder: input.DoctorOrder, // 医嘱
                    diagnose: input.Diagnose, // 诊断
                    content: input.Content, // 病历摘要
                    summary: input.Summary, // 总结
                    inviteDoctors: inviteDoctor, // 会诊邀请医生
                    doctorSummarys: doctorSummary // 会诊纪要医生
                );
                await _groupConsultationRepository.UpdateAsync(consultation);
                if (deleteDoctor.Count > 0)
                {
                    await _inviteDoctorRepository.DeleteManyAsync(deleteDoctor);
                }

                if (deleteSummary.Count > 0)
                {
                    await _iDoctorSummaryRepository.DeleteManyAsync(deleteSummary);
                }

                if (input.TypeCode == "PreHospitalFirstAid")
                {
                    var sync = new SyncTaskConsultationStatusDto()
                    {
                        Status = input.Status,
                        PIId = input.PI_ID
                    };
                    await _capPublisher.PublishAsync("sync.task.consultation.status.from.recipe", sync);

                }

                return consultation.Id;
            }
            catch (EcisBusinessException e)
            {
                throw new EcisBusinessException(message: e.Message);
            }
        }

        #endregion Create


        #region 邀请医生

        /// <summary>
        /// 邀请医生
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Guid> InvitationAsync(InviteDoctorUpdate input)
        {
            var doctor = await (await _inviteDoctorRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                x.GroupConsultationId == input.GroupConsultationId && x.Code == CurrentUser.FindClaimValue("name"));
            if (doctor == null)
            {
                var mapDoctor = ObjectMapper.Map<InviteDoctorUpdate, InviteDoctor>(input);
                doctor = await _inviteDoctorRepository.InsertAsync(mapDoctor);
            }
            else
            {
                doctor.Modify(checkInStatus: input.CheckInStatus, // 状态，0：已邀请，1：已报到,2:已离开
                    checkInTime: input.CheckInTime, // 报道时间
                    opinion: input.Opinion// 意见
                );
                await _inviteDoctorRepository.UpdateAsync(doctor);
            }

            return doctor.Id;
        }

        #endregion

        #region 会诊总结-院前使用

        /// <summary>
        /// 会诊总结
        /// </summary>
        /// <param name="groupConsultationId">会诊id</param>
        /// <param name="summary">总结信息</param>
        /// <returns></returns>
        /// <exception cref="EcisBusinessException"></exception>
        public async Task<bool> SummaryAsync(Guid groupConsultationId, string summary)
        {
            try
            {
                var dto = await _groupConsultationRepository.GetAsync(groupConsultationId);
                if (dto == null)
                {
                    throw new EcisBusinessException(message: "会诊不存在");
                }

                dto.Summary = summary;
                await _groupConsultationRepository.UpdateAsync(dto);
                return true;
            }
            catch (Exception e)
            {
                throw new EcisBusinessException(message: e.Message);
            }
        }

        #endregion

        #region 已参加会诊的医生

        /// <summary>
        /// 已参加会诊的医生
        /// </summary>
        /// <returns></returns>
        public async Task<List<InviteDoctorData>> GetJoinAsync()
        {
            var group = await (await _groupConsultationRepository.GetQueryableAsync()).Include(i => i.InviteDoctors)
                .Where(x => x.Status == GroupConsultationStatus.已开始)
                .ToListAsync();
            var map =
                ObjectMapper.Map<List<Recipes.GroupConsultation.GroupConsultation>, List<GroupConsultationData>>(group);
            var doctors = new List<InviteDoctorData>();
            map.ForEach(x =>
            {
                var inviteDoctor = x.InviteDoctors.FindAll(f => f.CheckInStatus == CheckInStatus.已报到);
                doctors.AddRange(inviteDoctor);
            });
            return doctors;
        }

        #endregion

        #region 院前医生报到

        /// <summary>
        /// 院前医生报到
        /// </summary>
        /// <param name="checkInTime">报到时间</param>
        /// <exception cref="EcisBusinessException"></exception>
        public async Task DoctorCheckInAsync(DateTime? checkInTime)
        {
            var group = await (await _groupConsultationRepository.GetQueryableAsync()).Include(i => i.InviteDoctors)
                .Where(x => x.Status == GroupConsultationStatus.已开始 && x.InviteDoctors.Any(a =>
                    a.Code == CurrentUser.FindClaimValue("name") && a.CheckInStatus == CheckInStatus.已邀请))
                .ToListAsync();
            var invite =
                await (await _inviteDoctorRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                    x.Code == CurrentUser.FindClaimValue("name") &&
                    string.Join(",", group.Select(s => s.Id)).Contains(x.GroupConsultationId.ToString()) &&
                    x.CheckInStatus == CheckInStatus.已邀请);
            if (invite == null)
            {
                throw new EcisBusinessException(message: "该医生未参加会诊");
            }
            invite.Modify(
                checkInStatus: CheckInStatus.已报到, // 状态，0：已邀请，1：已报到,2:已离开
                checkInTime: checkInTime == null ? DateTime.Now : checkInTime, // 报道时间
                opinion: invite.Opinion// 意见
            );
            await _inviteDoctorRepository.UpdateAsync(invite);
        }

        #endregion

        #region 院前医生离开

        /// <summary>
        /// 院前医生离开
        /// </summary>
        /// <exception cref="EcisBusinessException"></exception>
        public async Task DoctorLeaveAsync()
        {
            var group = await (await _groupConsultationRepository.GetQueryableAsync()).Include(i => i.InviteDoctors)
                .Where(x => x.Status == GroupConsultationStatus.已开始 && x.InviteDoctors.Any(a =>
                    a.Code == CurrentUser.FindClaimValue("name") && a.CheckInStatus == CheckInStatus.已报到))
                .ToListAsync();
            var invite =
                await (await _inviteDoctorRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                    x.Code == CurrentUser.FindClaimValue("name") &&
                    group.Select(s => s.Id).ToList().Contains(x.GroupConsultationId) &&
                    x.CheckInStatus == CheckInStatus.已报到);
            if (invite == null)
            {
                throw new EcisBusinessException(message: "该医生未参加会诊");
            }

            invite.Modify(
                checkInStatus: CheckInStatus.已离开, // 状态，0：已邀请，1：已报到,2:已离开
                checkInTime: invite.CheckInTime, // 报道时间
                opinion: invite.Opinion // 意见
            );
            await _inviteDoctorRepository.UpdateAsync(invite);

        }

        #endregion

        #region 会诊意见

        /// <summary>
        /// 会诊意见
        /// </summary>
        /// <param name="opinion">会诊意见</param>
        /// <exception cref="EcisBusinessException"></exception>
        public async Task OpinionAsync(string opinion)
        {
            var group = await (await _groupConsultationRepository.GetQueryableAsync()).Include(i => i.InviteDoctors)
                .Where(x => x.Status == GroupConsultationStatus.已开始 &&
                            x.InviteDoctors.Any(a =>
                                a.Code == CurrentUser.FindClaimValue("name") && a.CheckInStatus == CheckInStatus.已报到))
                .ToListAsync();
            var consultationId = string.Join(",", group.Select(s => s.Id).ToList());

            var invite =
                await (await _inviteDoctorRepository.GetQueryableAsync()).FirstOrDefaultAsync(x =>
                    x.Code == CurrentUser.FindClaimValue("name") &&
                    consultationId.Contains(x.GroupConsultationId.ToString()) && x.CheckInStatus == CheckInStatus.已报到);

            if (invite == null)
            {
                throw new EcisBusinessException(message: "该医生未参加会诊");
            }

            invite.Modify(
                checkInStatus: invite.CheckInStatus, // 状态，0：已邀请，1：已报到
                checkInTime: invite.CheckInTime, // 报道时间
                opinion: opinion // 意见
            );
            await _inviteDoctorRepository.UpdateAsync(invite);
        }

        #endregion

        #region 会诊意见

        /// <summary>
        /// 会诊意见-详情页面填写   
        /// </summary>
        /// <param name="opinion">会诊意见</param>
        /// <param name="inviteId">邀请医生id</param>
        /// <exception cref="EcisBusinessException"></exception>
        public async Task OpinionAsync(Guid inviteId, string opinion)
        {
            var invite =
                await _inviteDoctorRepository.GetAsync(inviteId);
            if (invite == null)
            {
                throw new EcisBusinessException(message: "该医生未参加会诊");
            }

            invite.Modify(
                checkInStatus: invite.CheckInStatus, // 状态，0：已邀请，1：已报到
                checkInTime: invite.CheckInTime, // 报道时间
                opinion: opinion // 意见
            );
            await _inviteDoctorRepository.UpdateAsync(invite);
        }

        #endregion

        #region 结束会诊

        /// <summary>
        /// 结束会诊
        /// </summary>
        /// <param name="groupConsultationId">会诊id</param>
        /// <exception cref="EcisBusinessException"></exception>
        public async Task FinishAsync(Guid groupConsultationId)
        {
            var consultation = await (await _groupConsultationRepository.GetQueryableAsync()).Include(i => i.InviteDoctors)
                .FirstOrDefaultAsync(x => x.Id == groupConsultationId);

            if (consultation == null)
            {
                throw new EcisBusinessException(message: "没有该会诊");
            }
            if (CurrentUser.FindClaimValue("name") != consultation.ApplyCode)
            {
                throw new EcisBusinessException(message: "您不是发起人，不能结束会诊");
            }
            if (consultation.ApplyCode != CurrentUser.FindClaimValue("name"))
            {
                throw new EcisBusinessException(message: "您无法结束会诊");
            }
            consultation.Finish(GroupConsultationStatus.已完结);
            await _groupConsultationRepository.UpdateAsync(consultation);
            var inviteList =
               await (await _inviteDoctorRepository.GetQueryableAsync()).Where(x => x.GroupConsultationId == consultation.Id).ToListAsync();
            inviteList.ForEach(x =>
            {
                if (x.Code == CurrentUser.FindClaimValue("name"))
                {
                    x.Modify(
                        checkInStatus: CheckInStatus.已离开, // 状态，0：已邀请，1：已报到,2:已离开
                        checkInTime: x.CheckInTime, // 报道时间
                        opinion: x.Opinion // 意见
                        );
                }
                else
                {
                    if (x.CheckInStatus == CheckInStatus.已邀请)
                    {
                        x.Modify(
                            checkInStatus: CheckInStatus.未报到, // 状态，0：已邀请，1：已报到,2:已离开
                            checkInTime: x.CheckInTime, // 报道时间
                            opinion: x.Opinion // 意见
                            );
                    }
                    if (x.CheckInStatus == CheckInStatus.已报到)//发起人离开，邀请人也要离开
                    {
                        x.Modify(
                          checkInStatus: CheckInStatus.已离开, // 状态，0：已邀请，1：已报到,2:已离开
                          checkInTime: x.CheckInTime, // 报道时间
                          opinion: x.Opinion // 意见
                          );
                    }
                }
            });
            await _inviteDoctorRepository.UpdateManyAsync(inviteList);


            if (consultation.TypeCode == "PreHospitalFirstAid")
            {
                var sync = new SyncTaskConsultationStatusDto()
                {
                    Status = GroupConsultationStatus.已完结,
                    PIId = consultation.PI_ID
                };
                await _capPublisher.PublishAsync("sync.task.consultation.status.from.recipe", sync);
            }
        }

        #endregion

        #region Get

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // [Authorize(RecipePermissions.GroupConsultations.Default)]
        public async Task<GroupConsultationData> GetAsync(Guid id)
        {
            var groupConsultation = await (await _groupConsultationRepository.GetQueryableAsync()).Include(i => i.InviteDoctors)
                .Include(i => i.DoctorSummarys)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (groupConsultation == null)
            {
                throw new EcisBusinessException(message: "数据不存在");
            }

            var map =
                ObjectMapper.Map<Recipes.GroupConsultation.GroupConsultation, GroupConsultationData>(groupConsultation);
            map.IsApplyDoctor = map.ApplyCode == CurrentUser.FindClaimValue("name");
            return map;
        }

        #endregion Get

        #region Delete

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // [Authorize(RecipePermissions.GroupConsultations.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var groupConsultation = await (await _groupConsultationRepository.GetQueryableAsync()).Include(i => i.InviteDoctors)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (groupConsultation == null)
            {
                throw new EcisBusinessException(message: "数据不存在");
            }

            if (groupConsultation.Status != GroupConsultationStatus.已取消)
            {
                throw new EcisBusinessException(message: "无法删除");
            }

            await _groupConsultationRepository.DeleteAsync(groupConsultation);
        }

        #endregion Delete

        #region GetList 院前使用

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pIId">患者id</param>
        /// <param name="typeCode">会诊类型编码</param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<ListResultDto<GroupConsultationData>> GetListAsync(string pIId, string typeCode, GroupConsultationStatus status = GroupConsultationStatus.全部)
        {
            var consultations =
                await _groupConsultationRepository.GetListAsync(pIId, null, typeCode, status);
            var map = ObjectMapper.Map<List<Recipes.GroupConsultation.GroupConsultation>, List<GroupConsultationData>>(
                consultations);
            map.ForEach(x =>
            {
                x.IsApplyDoctor =
                    x.ApplyCode == CurrentUser.FindClaimValue("name");
            });
            return new ListResultDto<GroupConsultationData>(map);
        }

        #endregion GetList

        /// <summary>
        /// 分页获取分诊记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<GroupConsultationData>> GetPageListAsync(GetGroupConsultationPagedInput input)
        {
            var list = (await _groupConsultationRepository.GetQueryableAsync()).Include(i => i.InviteDoctors)
                .WhereIf(input.PI_ID != Guid.Empty, x => x.PI_ID == input.PI_ID)
                .WhereIf(!string.IsNullOrWhiteSpace(input.PatientId), x => x.PatientId == input.PatientId)
                .WhereIf(!string.IsNullOrWhiteSpace(input.ApplyDeptCode), x => x.ApplyDeptCode == input.ApplyDeptCode)
                .WhereIf(!string.IsNullOrWhiteSpace(input.ApplyCode), x => x.ApplyCode == input.ApplyCode)
                .WhereIf(input.Status != GroupConsultationStatus.全部, x => x.Status == input.Status)
                .WhereIf(input.StartTime != null, x => x.StartTime >= input.StartTime)
                .WhereIf(input.EndTime != null, x => x.StartTime <= input.EndTime)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Code), x => x.InviteDoctors.Any(a => a.Code == input.Code))
                .WhereIf(!string.IsNullOrWhiteSpace(input.DeptCode),
                    x => x.InviteDoctors.Any(a => a.DeptCode == input.DeptCode))
                .Where(x => x.TypeCode == "PreHospitalFirstAid")
                .OrderByDescending(o => o.ApplyTime);
            var count = await list.CountAsync();
            var page = await list.Skip((input.Index - 1) * input.Size).Take(input.Size).ToListAsync();
            var map =
                ObjectMapper.Map<List<Recipes.GroupConsultation.GroupConsultation>, List<GroupConsultationData>>(
                    page);
            var result = new PagedResultDto<GroupConsultationData>(count, map.AsReadOnly());
            return result;
        }

        /// <summary>
        /// 急诊同步会诊意见
        /// </summary>
        /// <param name="doctor"></param>
        /// <returns></returns>
        /// <exception cref="EcisBusinessException"></exception>
        [CapSubscribe("sync.groupconsultationstatus.to.recipeservice")]
        public async Task<List<InviteDoctorData>> SyncGetOpinionAsync(List<InviteDoctorUpdate> doctor)
        {
            try
            {
                var doctors = await (await _inviteDoctorRepository.GetQueryableAsync()).Where(x => x.GroupConsultationId == doctor.FirstOrDefault().Id).ToListAsync();
                if (doctors == null)
                {
                    throw new EcisBusinessException(message: "会诊不存在");
                }

                doctors.ForEach(x =>
                {
                    x.CheckInStatus = CheckInStatus.已报到;
                    x.CheckInTime = DateTime.Now;
                    x.Opinion = "测试手动同步";
                });
                await _inviteDoctorRepository.UpdateManyAsync(doctors);
                var mapDoctor = ObjectMapper.Map<List<InviteDoctor>, List<InviteDoctorData>>(doctors);
                return mapDoctor;
            }
            catch (Exception e)
            {
                throw new EcisBusinessException(message: e.Message);
            }
        }
        /// <summary>
        /// 急诊同步会诊意见
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="EcisBusinessException"></exception>
        public async Task<List<DoctorSummaryData>> SyncOpinionAsync(Guid id)
        {
            try
            {
                var doctors = await (await _iDoctorSummaryRepository.GetQueryableAsync()).Where(x => x.GroupConsultationId == id).ToListAsync();
                if (doctors == null)
                {
                    throw new EcisBusinessException(message: "会诊不存在");
                }

                doctors.ForEach(x =>
                {
                    x.CheckInTime = DateTime.Now;
                    x.Opinion = "测试手动同步";
                });
                await _iDoctorSummaryRepository.UpdateManyAsync(doctors);
                var mapDoctor = ObjectMapper.Map<List<DoctorSummary>, List<DoctorSummaryData>>(doctors);
                return mapDoctor;
            }
            catch (Exception e)
            {
                throw new EcisBusinessException(message: e.Message);
            }
        }
        #region GetList 急诊使用

        /// <summary>
        /// 获取列表-急诊使用
        /// </summary>
        /// <param name="pIId">患者id</param>
        /// <returns></returns>
        public async Task<ListResultDto<GroupConsultationData>> GetListByEcisAsync(string pIId)
        {
            var consultations = await _groupConsultationRepository.GetListAsync(pIId: pIId);
            var map = ObjectMapper.Map<List<Recipes.GroupConsultation.GroupConsultation>, List<GroupConsultationData>>(
                consultations);
            return new ListResultDto<GroupConsultationData>(map);
        }

        #endregion GetList

        /// <summary>
        /// 获取最新一条会诊记录
        /// </summary>
        /// <param name="applyCode"></param>
        /// <param name="code"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<GroupConsultationData> GetNewestAsync(string applyCode, string code,
            GroupConsultationStatus status)
        {
            var data = await (await _groupConsultationRepository.GetQueryableAsync()).Include(i => i.InviteDoctors)
                .WhereIf(!string.IsNullOrWhiteSpace(applyCode), x => x.ApplyCode == applyCode)
                .WhereIf(status != GroupConsultationStatus.全部, x => x.Status == status)
                .WhereIf(!string.IsNullOrWhiteSpace(code), x => x.InviteDoctors.Any(a => a.Code == code))
                .Where(x => x.TypeCode == "PreHospitalFirstAid")
                .OrderByDescending(o => o.CreationTime).FirstOrDefaultAsync();
            var map = ObjectMapper.Map<Recipes.GroupConsultation.GroupConsultation, GroupConsultationData>(data);
            return map;
        }




    }
}