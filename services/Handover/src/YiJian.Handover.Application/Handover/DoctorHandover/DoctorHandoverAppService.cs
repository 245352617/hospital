using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Users;

namespace YiJian.Handover
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 医生交接班API
    /// </summary>
     [Authorize]
    public class DoctorHandoverAppService : HandoverAppService, IDoctorHandoverAppService
    {
        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="doctorHandoverRepository"></param>
        /// <param name="doctorHandoverManager"></param>
        /// <param name="doctorPatientsRepository"></param>
        /// <param name="handoverSettingAppService"></param>
        /// <param name="doctorPatientStatisticRepository"></param>
        public DoctorHandoverAppService(IDoctorHandoverRepository doctorHandoverRepository,
            DoctorHandoverManager doctorHandoverManager, IDoctorPatientsRepository doctorPatientsRepository,
            IShiftHandoverSettingAppService handoverSettingAppService,
            IDoctorPatientStatisticRepository doctorPatientStatisticRepository)
        {
            _doctorHandoverRepository = doctorHandoverRepository;
            _doctorHandoverManager = doctorHandoverManager;
            _doctorPatientsRepository = doctorPatientsRepository;
            _handoverSettingAppService = handoverSettingAppService;
            _doctorPatientStatisticRepository = doctorPatientStatisticRepository;
        }

        #endregion

        #region Create

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Guid> CreateDoctorHandoverAsync(DoctorHandoverCreation input)
        {
            //判断医生是否已交班
            var setting =
                await _handoverSettingAppService.GetShiftHandoverSettingDataAsync(input.ShiftSettingId);
            if (setting == null)
            {
                throw new BusinessException(message: "班次不存在");
            }

            var handover =
                await GetDoctorHandoverAsync(input.HandoverDoctorCode, input.ShiftSettingId, input.HandoverDate);
            if (handover != null)
            {
                throw new BusinessException(message: "医生已交班，无法新增");
            }

            var inputStatistics = input.PatientStatistics;
            var id = GuidGenerator.Create();
            var statistics = new DoctorPatientStatistic(GuidGenerator.Create(), id, inputStatistics.Total,
                inputStatistics.ClassI, inputStatistics.ClassII, inputStatistics.ClassIII, inputStatistics.ClassIV,
                inputStatistics.PreOperation, inputStatistics.ExistingDisease, inputStatistics.OutDept,
                inputStatistics.Rescue, inputStatistics.Visit, inputStatistics.Death, inputStatistics.CPR,
                inputStatistics.Admission);
            var patients = new List<DoctorPatients>();
            foreach (var item in input.DoctorPatients)
            {
                if (patients.Any(a => a.PI_ID == item.PI_ID))
                {
                    continue;
                }

                patients.Add(new DoctorPatients(GuidGenerator.Create(), id, item.PI_ID, item.PatientId, item.VisitNo,
                    item.PatientName, item.Sex, item.SexName, item.Age, item.TriageLevelName, item.DiagnoseName,
                    item.Bed,
                    item.Content,
                    item.Test, item.Inspect, item.Emr, item.InOutVolume, item.VitalSigns, item.Medicine,item.Status));
            }

            var doctorHandover = await _doctorHandoverManager.CreateAsync(input.HandoverDate,
                DateTime.Parse(input.HandoverTime),
                input.HandoverDoctorCode, input.HandoverDoctorName, input.ShiftSettingId,
                input.ShiftSettingName,
                input.OtherMatters, statistics, patients, input.Status);
            return doctorHandover.Id;
        }

        #endregion

        #region Update

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateDoctorHandoverAsync(DoctorHandoverUpdate input)
        {
            var doctorHandover = await (await _doctorHandoverRepository.GetQueryableAsync()).Include(c => c.PatientStatistics)
                .Include(c => c.DoctorPatients).AsNoTracking().FirstOrDefaultAsync(f => f.Id == input.Id);
            if (doctorHandover == null)
            {
                throw new BusinessException(message: "数据不存在");
            }

            doctorHandover.Modify(DateTime.Parse(input.HandoverTime),
                input.HandoverDoctorCode,
                input.HandoverDoctorName,
                input.ShiftSettingId, input.ShiftSettingName, input.OtherMatters, input.Status);
            await _doctorHandoverRepository.UpdateAsync(doctorHandover);
            var inputStatistics = input.PatientStatistics;
            if (inputStatistics != null)
            {
                var statistics = doctorHandover.PatientStatistics;
                if (statistics == null)
                {
                    await _doctorPatientStatisticRepository.InsertAsync(new DoctorPatientStatistic(
                        GuidGenerator.Create(), input.Id, inputStatistics.Total,
                        inputStatistics.ClassI, inputStatistics.ClassII, inputStatistics.ClassIII,
                        inputStatistics.ClassIV,
                        inputStatistics.PreOperation, inputStatistics.ExistingDisease, inputStatistics.OutDept,
                        inputStatistics.Rescue, inputStatistics.Visit, inputStatistics.Death, inputStatistics.CPR,
                        inputStatistics.Admission));
                }
                else
                {
                    statistics.Modify(inputStatistics.Total,
                        inputStatistics.ClassI, inputStatistics.ClassII, inputStatistics.ClassIII,
                        inputStatistics.ClassIV,
                        inputStatistics.PreOperation, inputStatistics.ExistingDisease, inputStatistics.OutDept,
                        inputStatistics.Rescue, inputStatistics.Visit, inputStatistics.Death, inputStatistics.CPR,
                        inputStatistics.Admission);
                    await _doctorPatientStatisticRepository.UpdateAsync(statistics);
                }
            }

            //删除患者信息，重新新增
            await _doctorPatientsRepository.DeleteManyAsync(doctorHandover.DoctorPatients);
            if (input.DoctorPatients != null)
            {
                var patients = new List<DoctorPatients>();
                foreach (var item in input.DoctorPatients)
                {
                    if (patients.Any(a => a.PI_ID == item.PI_ID))
                    {
                        continue;
                    }

                    patients.Add(new DoctorPatients(GuidGenerator.Create(), input.Id, item.PI_ID, item.PatientId,
                        item.VisitNo,
                        item.PatientName, item.Sex, item.SexName, item.Age, item.TriageLevelName, item.DiagnoseName,
                        item.Bed,
                        item.Content,
                        item.Test, item.Inspect, item.Emr, item.InOutVolume, item.VitalSigns, item.Medicine,item.Status));
                }

                await _doctorPatientsRepository.InsertManyAsync(patients);
            }
        }

        #endregion

        #region Get

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="doctorCode">医生编码</param>
        /// <param name="shiftId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<DoctorHandoverData> GetDoctorHandoverAsync([Required] string doctorCode,
            [Required] Guid shiftId, [Required] string date)
        {
            var doctorHandover = await (await _doctorHandoverRepository.GetQueryableAsync())
                .Include(c => c.DoctorPatients)
                .Include(c => c.PatientStatistics).FirstOrDefaultAsync(f =>
                    (f.HandoverDoctorCode == doctorCode || doctorCode == "admin") && f.ShiftSettingId == shiftId &&
                    f.HandoverDate == DateTime.Parse(date));
            var result = ObjectMapper.Map<DoctorHandover, DoctorHandoverData>(doctorHandover);
            if (result != null)
            {
                result.HandoverTime = doctorHandover.HandoverTime.ToString("yyyy-MM-dd HH:mm:ss");
                result.HandoverDate = doctorHandover.HandoverTime.ToString("yyyy-MM-dd");
            }

            return result;
        }

        #endregion

        /// <summary>
        /// 分组查询历史数据
        /// </summary>
        /// <returns></returns>
        public async Task<PagedResultDto<DoctorHandoverStatisticDataList>> GetHistoryListAsync(
            GetDoctorHandoverInput input)
        {
            var handover = await _doctorHandoverRepository.GetListAsync(startDate: input.StartDate, endDate: input.EndDate);
            var group = handover.Where(x => x.Status == 1)
                .GroupBy(g => new
                {
                    g.HandoverDate, g.ShiftSettingName, g.ShiftSettingId
                })
                .Select(s => new DoctorHandoverStatisticDataList()
                {
                    HandoverDate = s.Key.HandoverDate.ToString("yyyy-MM-dd"),
                    ShiftSettingName = s.Key.ShiftSettingName,
                    ShiftSettingId = s.Key.ShiftSettingId,
                    HandoverDoctorName = string.Join(",", s.Select(t => t.HandoverDoctorName).ToList()),
                    Total = s.Sum(t => t.PatientStatistics.Total),
                    ClassI = s.Sum(t => t.PatientStatistics.ClassI),
                    ClassII = s.Sum(t => t.PatientStatistics.ClassII),
                    ClassIII = s.Sum(t => t.PatientStatistics.ClassIII),
                    ClassIV = s.Sum(t => t.PatientStatistics.ClassIV),
                    PreOperation = s.Sum(t => t.PatientStatistics.PreOperation),
                    ExistingDisease = s.Sum(t => t.PatientStatistics.ExistingDisease),
                    OutDept = s.Sum(t => t.PatientStatistics.OutDept),
                    Rescue = s.Sum(t => t.PatientStatistics.Rescue),
                    Death = s.Sum(t => t.PatientStatistics.Death),
                    CPR = s.Sum(t => t.PatientStatistics.CPR),
                    Admission = s.Sum(t => t.PatientStatistics.Admission)
                }).ToList();
            var result = group.OrderByDescending(o => o.HandoverDate)
                .Skip(input.Size * (input.Index - 1))
                .Take(input.Size).ToList();
            var page = new PagedResultDto<DoctorHandoverStatisticDataList>(totalCount:
                group.Count,
                items: result.AsReadOnly());
            return page;
        }

        /// <summary>
        /// 历史数据详情
        /// </summary>
        /// <param name="date"></param>
        /// <param name="shiftId"></param>
        /// <returns></returns>
        public async Task<DoctorHandoverStatisticDataList> GetDetailsAsync(
            [Required] string date,
            [Required] Guid shiftId)
        {
            var handover = await (await _doctorHandoverRepository.GetQueryableAsync())
                .Include(c => c.PatientStatistics)
                .Include(c => c.DoctorPatients)
                .Where(w => w.HandoverDate == DateTime.Parse(date))
                .Where(w => w.ShiftSettingId == shiftId)
                .Where(w => w.Status == 1)
                .ToListAsync();
            var handoverMap = ObjectMapper.Map<List<DoctorHandover>, List<DoctorHandoverData>>(handover);
            var groups = handoverMap
                .GroupBy(g => new
                {
                    g.HandoverDate
                })
                .Select(s => new DoctorHandoverStatisticDataList()
                {
                    HandoverDate = s.Key.HandoverDate.Split(" ")[0],
                    HandoverDoctorName = string.Join(",", s.Select(t => t.HandoverDoctorName).ToList()),
                    Total = s.Sum(t => t.PatientStatistics.Total),
                    ClassI = s.Sum(t => t.PatientStatistics.ClassI),
                    ClassII = s.Sum(t => t.PatientStatistics.ClassII),
                    ClassIII = s.Sum(t => t.PatientStatistics.ClassIII),
                    ClassIV = s.Sum(t => t.PatientStatistics.ClassIV),
                    PreOperation = s.Sum(t => t.PatientStatistics.PreOperation),
                    ExistingDisease = s.Sum(t => t.PatientStatistics.ExistingDisease),
                    OutDept = s.Sum(t => t.PatientStatistics.OutDept),
                    Rescue = s.Sum(t => t.PatientStatistics.Rescue),
                    Death = s.Sum(t => t.PatientStatistics.Death),
                    CPR = s.Sum(t => t.PatientStatistics.CPR),
                    Admission = s.Sum(t => t.PatientStatistics.Admission),
                }).ToList();
            var result = groups.FirstOrDefault();
            if (result != null)
            {
                var patients = new List<HandoverHistoryPatientsDataList>();

                var doctor = new List<DoctorHandoverDetailsData>();
                handoverMap.ForEach(f =>
                {
                    var patientMap = ObjectMapper.Map<List<DoctorPatientsData>, List<HandoverHistoryPatientsDataList>>(
                        f.DoctorPatients);
                    patientMap.ForEach(ff => ff.HandoverDoctorName = f.HandoverDoctorName);
                    patients.AddRange(patientMap);
                    doctor.Add(new DoctorHandoverDetailsData()
                    {
                        HandoverDoctorName = f.HandoverDoctorName,
                        HandoverTime = DateTime.Parse(f.HandoverTime).ToString("yyyy-MM-dd HH:mm:ss"),
                        OtherMatters = f.OtherMatters
                    });
                });
                result.DoctorPatients = patients;
                result.DoctorHandover = doctor;
            }

            return result;
        }

        #region Private Fields

        private readonly IDoctorHandoverRepository _doctorHandoverRepository;
        private readonly DoctorHandoverManager _doctorHandoverManager;
        private readonly IDoctorPatientsRepository _doctorPatientsRepository;
        private readonly IDoctorPatientStatisticRepository _doctorPatientStatisticRepository;
        private readonly IShiftHandoverSettingAppService _handoverSettingAppService;

        #endregion
    }
}