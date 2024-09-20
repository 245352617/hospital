using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nito.Disposables.Internals;

namespace YiJian.Handover
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp.Application.Dtos;
    using Volo.Abp.Application.Services;
    using Volo.Abp.DependencyInjection;
    using Microsoft.AspNetCore.Authorization;
    using YiJian.Handover.Permissions;

    /// <summary>
    /// 护士交班 API
    /// </summary>
    [Authorize]
    public class NurseHandoverAppService : HandoverAppService, INurseHandoverAppService
    {
        private readonly NurseHandoverManager _nurseHandoverManager;
        private readonly INurseHandoverRepository _nurseHandoverRepository;

        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nurseHandoverRepository"></param>
        /// <param name="nurseHandoverManager"></param>
        public NurseHandoverAppService(INurseHandoverRepository nurseHandoverRepository,
            NurseHandoverManager nurseHandoverManager)
        {
            _nurseHandoverRepository = nurseHandoverRepository;
            _nurseHandoverManager = nurseHandoverManager;
        }

        #endregion constructor

        #region Create

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        // [Authorize(HandoverPermissions.NurseHandovers.Create)]
        public async Task<Guid> SaveNurseHandoverAsync(NurseHandoverCreation input)
        {
            var nurse = await (await _nurseHandoverRepository.GetQueryableAsync()).FirstOrDefaultAsync(f =>
                f.ShiftSettingId == input.ShiftSettingId && f.HandoverDate == DateTime.Parse(input.HandoverDate) &&
                f.PI_ID == input.PI_ID);
            if (nurse == null)
            {
                var nurseHandover = await _nurseHandoverManager.CreateAsync(pIID: input.PI_ID, // triage分诊患者id
                    patientId: input.PatientId, // 患者id
                    visitNo: input.VisitNo, // 就诊号
                    patientName: input.PatientName, // 患者姓名
                    sex: input.Sex, // 性别编码
                    sexName: input.SexName, // 性别名称
                    age: input.Age, // 年龄
                    triageLevel: input.TriageLevel, // 分诊级别
                    triageLevelName: input.TriageLevelName, // 分诊级别名称
                    areaCode: input.AreaCode, // 区域编码
                    areaName: input.AreaName, // 区域名称
                    inDeptTime: input.InDeptTime, // 入科时间
                    diagnoseName: input.DiagnoseName, // 诊断
                    bed: input.Bed, // 床号
                    isNoThree: input.IsNoThree, // 是否三无
                    criticallyIll: input.CriticallyIll, // 是否病危
                    content: input.Content, // 交班内容
                    test: input.Test, // 检验
                    inspect: input.Inspect, // 检查
                    emr: input.Emr, // 电子病历
                    inOutVolume: input.InOutVolume, // 出入量
                    vitalSigns: input.VitalSigns, // 生命体征
                    medicine: input.Medicine, // 药物
                    latestStatus: input.LatestStatus, // 最新现状
                    background: input.Background, // 背景
                    assessment: input.Assessment, // 评估
                    proposal: input.Proposal, // 建议
                    devices: input.Devices, // 设备
                    handoverTime: DateTime.Parse(input.HandoverTime), // 交班时间
                    handoverNurseCode: input.HandoverNurseCode, // 交班护士编码
                    handoverNurseName: input.HandoverNurseName, // 交班护士名称
                    successionNurseCode: input.SuccessionNurseCode, // 接班护士编码
                    successionNurseName: input.SuccessionNurseName, // 接班护士名称
                    handoverDate: DateTime.Parse(input.HandoverDate), // 交班日期
                    shiftSettingId: input.ShiftSettingId, // 班次id
                    shiftSettingName: input.ShiftSettingName, // 班次名称
                    status: input.Status, // 交班状态，0：未提交，1：提交交班
                    creationCode: input.CreationCode, // 创建人编码
                    creationName: input.CreationName, // 创建人名称
                    totalPatient: input.TotalPatient, dutyNurseName: input.DutyNurseName // 查询的全部患者
                );

                return nurseHandover.Id;
            }
            else
            {
                nurse.Modify(patientId: input.PatientId, // 患者id
                    visitNo: input.VisitNo, // 就诊号
                    patientName: input.PatientName, // 患者姓名
                    sex: input.Sex, // 性别编码
                    sexName: input.SexName, // 性别名称
                    age: input.Age, // 年龄
                    triageLevel: input.TriageLevel, // 分诊级别
                    triageLevelName: input.TriageLevelName, // 分诊级别名称
                    areaCode: input.AreaCode, // 区域编码
                    areaName: input.AreaName, // 区域名称
                    inDeptTime: input.InDeptTime, // 入科时间
                    diagnoseName: input.DiagnoseName, // 诊断
                    bed: input.Bed, // 床号
                    isNoThree: input.IsNoThree, // 是否三无
                    criticallyIll: input.CriticallyIll, // 是否病危
                    content: input.Content, // 交班内容
                    test: input.Test, // 检验
                    inspect: input.Inspect, // 检查
                    emr: input.Emr, // 电子病历
                    inOutVolume: input.InOutVolume, // 出入量
                    vitalSigns: input.VitalSigns, // 生命体征
                    medicine: input.Medicine, // 药物
                    latestStatus: input.LatestStatus, // 最新现状
                    background: input.Background, // 背景
                    assessment: input.Assessment, // 评估
                    proposal: input.Proposal, // 建议
                    devices: input.Devices, // 设备
                    handoverTime: DateTime.Parse(input.HandoverTime), // 交班时间
                    handoverNurseCode: input.HandoverNurseCode, // 交班护士编码
                    handoverNurseName: input.HandoverNurseName, // 交班护士名称
                    successionNurseCode: input.SuccessionNurseCode, // 接班护士编码
                    successionNurseName: input.SuccessionNurseName, // 接班护士名称
                    handoverDate: DateTime.Parse(input.HandoverDate), // 交班日期
                    shiftSettingId: input.ShiftSettingId, // 班次id
                    shiftSettingName: input.ShiftSettingName, // 班次名称
                    status: input.Status, // 交班状态，0：未提交，1：提交交班
                    creationCode: input.CreationCode, // 创建人编码
                    creationName: input.CreationName, // 创建人名称
                    totalPatient: input.TotalPatient, dutyNurseName: input.DutyNurseName // 查询的全部患者
                );
                await _nurseHandoverRepository.UpdateAsync(nurse);
                return nurse.Id;
            }
        }

        #endregion Create


        #region Get

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="shiftId"></param>
        /// <param name="handoverDate"></param>
        /// <param name="creationCode"></param>
        /// <returns></returns>
        // [Authorize(HandoverPermissions.NurseHandovers.Default)]
        public async Task<List<NurseHandoverData>> GetNurseHandoverAsync([Required] Guid shiftId,
            [Required] string handoverDate, [Required] string creationCode)
        {
            var nurseHandover = await (await _nurseHandoverRepository.GetQueryableAsync()).Where(f =>
                    f.ShiftSettingId == shiftId && f.HandoverDate == DateTime.Parse(handoverDate) &&
                    (creationCode == "admin" || f.CreationCode == creationCode))
                .ToListAsync();
            var result = ObjectMapper.Map<List<NurseHandover>, List<NurseHandoverData>>(nurseHandover);
            result.ForEach(x => x.HandoverDate = x.HandoverDate.Split(' ')?[0]);
            return result;
        }

        #endregion Get

        #region Delete

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // [Authorize(HandoverPermissions.NurseHandovers.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _nurseHandoverRepository.DeleteAsync(id);
        }

        #endregion Delete

        #region GetList

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        // [Authorize(HandoverPermissions.NurseHandovers.Default)]
        public async Task<PagedResultDto<NurseHandoverDataList>> GetListAsync(GetNurseHandoverPagedInput input)
        {
            var nurseHandovers = await _nurseHandoverRepository.GetListAsync(
                input.Sorting, input.StartDate, input.EndDate);
            var nurseList = nurseHandovers.Where(x => x.Status == 1);
            var group = nurseList.GroupBy(g => new { g.HandoverDate, g.ShiftSettingName, g.ShiftSettingId })
                .Select(s => new NurseHandoverDataList()
                {
                    HandoverDate = s.Key.HandoverDate.ToString("yyyy-MM-dd"),
                    ShiftSettingName = s.Key.ShiftSettingName,
                    ShiftSettingId = s.Key.ShiftSettingId,
                    HandoverNurseName =
                        string.Join(",", s.GroupBy(x => x.HandoverNurseName).Select(x => x.Key).ToList()),
                    SuccessionNurseName = string.Join(",",
                        s.GroupBy(x => x.SuccessionNurseName).Select(x => x.Key).ToList()),
                    ObservationTotal = s.Count(c => c.AreaCode == "ObservationArea"),
                    ObservationI = s.Count(c =>
                        c.AreaCode == "ObservationArea" && c.TriageLevel == "TriageLevel_001"),
                    ObservationII = s.Count(c =>
                        c.AreaCode == "ObservationArea" && c.TriageLevel == "TriageLevel_002"),
                    ObservationIII = s.Count(c =>
                        c.AreaCode == "ObservationArea" && c.TriageLevel == "TriageLevel_003"),
                    ObservationIV = s.Count((c =>
                        c.AreaCode == "ObservationArea" &&
                        (c.TriageLevel == "TriageLevel_004" || c.TriageLevel == "TriageLevel_005"))),
                    RescueTotal = s.Count(c => c.AreaCode == "RescueArea"),
                    RescueI = s.Count(c =>
                        c.AreaCode == "RescueArea" && c.TriageLevel == "TriageLevel_001"),
                    RescueII = s.Count(c =>
                        c.AreaCode == "RescueArea" && c.TriageLevel == "TriageLevel_002"),
                    RescueIII = s.Count(c =>
                        c.AreaCode == "RescueArea" && c.TriageLevel == "TriageLevel_003"),
                    RescueIV = s.Count(c =>
                        c.AreaCode == "RescueArea" &&
                        (c.TriageLevel == "TriageLevel_004" || c.TriageLevel == "TriageLevel_005")),
                }).ToList();
            var result = group.OrderByDescending(o => o.HandoverDate)
                .Skip(input.Size * (input.Index - 1))
                .Take(input.Size).ToList();
            return new PagedResultDto<NurseHandoverDataList>(
                group.Count,
                items: result.AsReadOnly());
        }

        #endregion GetList

        #region 导入上班次

        /// <summary>
        /// 导入上个班次
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> ImportUpDeviceAsync(GetNurseHandoverInput input)
        {
            var nurseHandover = await (await _nurseHandoverRepository.GetQueryableAsync())
                                    .Where(x => x.CreationCode == input.CreationCode &&
                                                x.HandoverDate == DateTime.Parse(input.HandoverDate) &&
                                                x.ShiftSettingId != input.ShiftId && x.PI_ID == input.PI_ID)
                                    .OrderByDescending(o => o.CreationTime)
                                    .FirstOrDefaultAsync() ??
                                await (await _nurseHandoverRepository.GetQueryableAsync())
                                    .Where(x => x.CreationCode == input.CreationCode &&
                                                x.HandoverDate != DateTime.Parse(input.HandoverDate) &&
                                                x.PI_ID == input.PI_ID)
                                    .OrderByDescending(o => o.CreationTime)
                                    .FirstOrDefaultAsync();
            return nurseHandover?.Devices ?? "";
        }

        #endregion

        #region GetDetails

        /// <summary>
        /// 获取交班详情
        /// </summary>
        /// <param name="date"></param>
        /// <param name="shiftId"></param>
        /// <returns></returns>
        public async Task<StatisticsData> GetDetailsAsync([Required] string date,
            [Required] Guid shiftId)
        {
            var handover = await (await _nurseHandoverRepository.GetQueryableAsync())
                .Where(x => x.HandoverDate == DateTime.Parse(date) && x.ShiftSettingId == shiftId && x.Status == 1)
                .ToListAsync();
            var list = ObjectMapper.Map<List<NurseHandover>, List<NurseHandoverData>>(handover);
            if (list is { Count: > 0 })
            {
                var statistics = new StatisticsData
                {
                    TotalPatient = list.FirstOrDefault().TotalPatient,
                    Already = list.Count,
                    I = list.Count(c => c.TriageLevel == "TriageLevel_001"),
                    II = list.Count(c => c.TriageLevel == "TriageLevel_002"),
                    III = list.Count(c => c.TriageLevel == "TriageLevel_003"),
                    IV = list.Count(c =>
                        (c.TriageLevel == "TriageLevel_004" || c.TriageLevel == "TriageLevel_005")),
                };
                statistics.Not = statistics.TotalPatient - statistics.Already;

                statistics.NursePatients = new List<NurseHandoverData>();
                statistics.NursePatients.AddRange(list);
                return statistics;
            }

            return new StatisticsData();
        }

        #endregion
    }
}