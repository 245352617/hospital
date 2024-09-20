using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using YiJian.ECIS.Call.Domain.CallConfig;
using YiJian.ECIS.Domain.Call;
using YiJian.ECIS.ShareModel.Exceptions;
using SamJan.MicroService.PreHospital.TriageService;

namespace YiJian.ECIS.Call.Domain.CallCenter
{
    /// <summary>
    /// 【叫号信息】领域服务
    /// </summary>
    public class CallInfoManager : DomainService
    {
        private readonly ICallInfoRepository _callInfoRepository;
        private readonly BaseConfigManager _baseConfigManager;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IConsultingRoomRepository _consultingRoomRepository;
        private readonly IConsultingRoomRegularRepository _consultingRoomRegularRepository;
        private readonly IDoctorRegularRepository _doctorRegularRepository;

        public CallInfoManager(ICallInfoRepository callInfoRepository,
            BaseConfigManager baseConfigManager,
            IDepartmentRepository departmentRepository,
            IConsultingRoomRepository consultingRoomRepository,
            IConsultingRoomRegularRepository consultingRoomRegularRepository, IDoctorRegularRepository doctorRegularRepository)
        {
            this._callInfoRepository = callInfoRepository;
            this._baseConfigManager = baseConfigManager;
            this._departmentRepository = departmentRepository;
            this._consultingRoomRepository = consultingRoomRepository;
            this._consultingRoomRegularRepository = consultingRoomRegularRepository;
            this._doctorRegularRepository = doctorRegularRepository;
        }

        /// <summary>
        /// 今日挂号总数，当前候诊人数，今日已就诊人数，今日已过号人数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="departmentCode"></param>
        /// <returns>(今日挂号总数，当前候诊人数，今日已就诊人数，今日已过号人数)</returns>
        public async Task<(int, int, int, int)> GetTodayStatisticsAsync(DateTime date, string departmentCode = null)
        {
            var todayCallingCount = await this.GetTodayCallingCountAsync(date, departmentCode);
            var waittingCount = await this.GetWaittingCountAsync(date, departmentCode);
            var todayTreatedCount = await this.GetTodayTreatedCountAsync(date, departmentCode);
            var todayUntreatedOverCount = await this.GetTodayUntreatedOverCountAsync(date, departmentCode);

            return (todayCallingCount, waittingCount, todayTreatedCount, todayUntreatedOverCount);
        }

        /// <summary>
        /// 获取当天叫号患者总数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="departmentCode"></param>
        /// <returns></returns>
        public async Task<int> GetTodayCallingCountAsync(DateTime date, string departmentCode = null)
        {
            return await (await this._callInfoRepository.GetQueryableAsync())
                .Where(x => x.IsShow)
                .WhereIf(!string.IsNullOrEmpty(departmentCode), x => x.TriageDept == departmentCode)
                .Where(x => x.LogDate == date)  // 当天登记的患者
                .CountAsync();
        }

        /// <summary>
        /// 获取当前待就诊患者总数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="departmentCode"></param>
        /// <returns></returns>
        public async Task<int> GetWaittingCountAsync(DateTime date, string departmentCode = null)
        {
            return await (await this._callInfoRepository.GetQueryableAsync())
                .Where(x => x.IsShow)
                .WhereIf(!string.IsNullOrEmpty(departmentCode), x => x.TriageDept == departmentCode)
                .Where(x => x.LogDate == date)  // 当天登记的患者
                .CountAsync();
        }

        /// <summary>
        /// 获取当天已就诊患者总数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="departmentCode"></param>
        /// <returns></returns>
        public async Task<int> GetTodayTreatedCountAsync(DateTime date, string departmentCode = null)
        {
            return await (await this._callInfoRepository.GetQueryableAsync())
                .Where(x => x.IsShow)
                .WhereIf(!string.IsNullOrEmpty(departmentCode), x => x.TriageDept == departmentCode)
                .Where(x => x.LogDate == date)  // 当天登记的患者                                                        
                .Where(x => x.CallStatus == CallStatus.Over)  // 已就诊、出科
                .CountAsync();
        }

        /// <summary>
        /// 获取当天已过号患者总数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="departmentCode"></param>
        /// <returns></returns>
        public async Task<int> GetTodayUntreatedOverCountAsync(DateTime date, string departmentCode = null)
        {
            return await (await this._callInfoRepository.GetQueryableAsync())
                .Where(x => x.IsShow)
                .WhereIf(!string.IsNullOrEmpty(departmentCode), x => x.TriageDept == departmentCode)
                .Where(x => x.LogDate == date)  // 当天登记的患者              
                .Where(x => x.CallStatus == CallStatus.Exceed)  // 已过号
                .CountAsync();
        }

        /// <summary>
        /// 获取所在科室
        /// </summary>
        /// <param name="consultingRoomCode">诊室 ID</param>
        /// <param name="doctorId">医生 ID</param>
        /// <returns></returns>
        public async Task<Department> GetDepartmentAsync(string consultingRoomCode, string doctorId)
        {
            // 配置可能保存多个版本，获取最新生效的版本配置
            var config = await this._baseConfigManager.GetCurrentConfigAsync();
            if (config.CurrentCallMode == CallMode.ConsultingRoomRegular)
            {
                // 诊室固定
                var departmentsQuery = from crr in (await this._consultingRoomRegularRepository.GetQueryableAsync())
                                       join cr in (await this._consultingRoomRepository.GetQueryableAsync()) on crr.ConsultingRoomId equals cr.Id
                                       join dpt in (await this._departmentRepository.GetQueryableAsync()) on crr.DepartmentId equals dpt.Id
                                       where cr.Code == consultingRoomCode
                                       select dpt;
                return await departmentsQuery.AnyAsync() ? await departmentsQuery.FirstOrDefaultAsync() : throw new EcisBusinessException(message: "当前诊室未设置诊室固定规则");

            }
            if (config.CurrentCallMode == CallMode.DoctorRegular)
            {
                // 医生变动
                var departmentsQuery = from dr in (await this._doctorRegularRepository.GetQueryableAsync())
                                       join dpt in (await this._departmentRepository.GetQueryableAsync()) on dr.DepartmentId equals dpt.Id
                                       where dr.DoctorId == doctorId
                                       select dpt;
                return await departmentsQuery.AnyAsync() ? await departmentsQuery.FirstOrDefaultAsync() : throw new EcisBusinessException(message: "当前医生未设置医生变动规则");
            }
            else
            {
                throw new EcisBusinessException(message: "未设置当前叫号模式");
            }
        }

        /// <summary>
        /// 召回患者到待就诊
        /// </summary>
        /// <param name="registerNo">registerNo</param>
        /// <returns></returns>
        public async Task<CallInfo> SendBackWaitingAsync(string registerNo)
        {
            // 获取患者
            var callingInfo = await (await this._callInfoRepository.GetQueryableAsync()).Where(x => x.RegisterNo == registerNo)
                .FirstOrDefaultAsync();

            callingInfo.SendBackWaiting();

            await this._callInfoRepository.UpdateAsync(callingInfo);

            return callingInfo;
        }

        /// <summary>
        /// 患者正在就诊
        /// </summary>
        /// <param name="registerNo">registerNo</param>
        /// <returns></returns>
        public async Task<CallInfo> TreatAsync(string registerNo)
        {
            // 获取当前诊室叫号中的患者
            var callingInfo = await (await this._callInfoRepository.GetQueryableAsync())
                .Where(x => x.RegisterNo == registerNo)
                .FirstOrDefaultAsync();
            if (callingInfo == null)
            {
                throw new EcisBusinessException(message: "患者不存在");
            }
            //callingInfo.Treat(input.DoctorId, input.DoctorName, input.ConsultingRoomCode, input.ConsultingRoomName);
            callingInfo.Treat();

            await this._callInfoRepository.UpdateAsync(callingInfo);

            return callingInfo;
        }

        /// <summary>
        /// 患者已结束就诊
        /// </summary>
        /// <param name="registerNo">registerNo</param>
        /// <returns></returns>
        public async Task<CallInfo> TreatFinishAsync(string registerNo)
        {
            // 获取就诊中的患者
            var callingInfo = await (await this._callInfoRepository.GetQueryableAsync()).Where(x => x.RegisterNo == registerNo)
                .FirstOrDefaultAsync();
            if (callingInfo == null)
            {
                throw new EcisBusinessException(message: "患者不存在");
            }
            callingInfo.TreatFinish(callingInfo.CallingDoctorCode, callingInfo.CallingDoctorName, callingInfo.TriageDept, callingInfo.TriageDeptName);

            await this._callInfoRepository.UpdateAsync(callingInfo);

            return callingInfo;
        }

        /// <summary>
        /// 患者出科
        /// </summary>
        /// <param name="registerNo">registerNo</param>
        /// <returns></returns>
        public async Task<CallInfo> OutDepartmentAsync(string registerNo)
        {
            // 获取就诊中的患者
            var callingInfo = await this._callInfoRepository.FirstOrDefaultAsync(x => x.RegisterNo == registerNo);
            if (callingInfo == null)
            {
                throw new EcisBusinessException(message: "患者不存在");
            }
            callingInfo.OutDepartment();

            await this._callInfoRepository.UpdateAsync(callingInfo);

            return callingInfo;
        }

        /// <summary>
        /// 患者过号处理
        /// </summary>
        /// <param name="registerNo">registerNo</param>
        /// <returns></returns>
        public async Task<CallInfo> UntreatedOverAsync(string registerNo)
        {
            // 获取指定患者患者
            var callingInfo = await (await this._callInfoRepository.GetQueryableAsync()).Where(x => x.RegisterNo == registerNo)
                .FirstOrDefaultAsync();
            if (callingInfo is null)
            {// 患者已结束就诊
                throw new EcisBusinessException(message: "患者信息不存在");
                //throw new EcisBusinessException("Call:patient-is-treated-finish");
            }
            //if (callingInfo.CallStatus == CallStatus.NotYet)
            //{ // 患者未叫号
            //    throw new EcisBusinessException(message: "患者未叫号");
            //    //throw new EcisBusinessException("Call:patient-is-not-call-yet");
            //}
            callingInfo.Exceed();

            await this._callInfoRepository.UpdateAsync(callingInfo);

            return callingInfo;
        }

        /// <summary>
        /// 患者退号处理
        /// </summary>
        /// <param name="registerNo">registerNo</param>
        /// <returns></returns>
        public async Task<CallInfo> RefundNoAsync(string registerNo)
        {
            // 获取指定患者患者
            var callingInfo = await (await this._callInfoRepository.GetQueryableAsync()).Where(x => x.RegisterNo == registerNo)
                .FirstOrDefaultAsync();
            if (callingInfo is null)
            {// 患者已结束就诊
                throw new EcisBusinessException(message: "患者信息不存在");
                //throw new EcisBusinessException("Call:patient-is-treated-finish");
            }

            callingInfo.RegisterRefund();

            await this._callInfoRepository.UpdateAsync(callingInfo);

            return callingInfo;
        }

        /// <summary>
        /// 插入或更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<CallInfo> InsertOrUpdateAsync(CallInfo entity)
        {
            if (await (await this._callInfoRepository.GetQueryableAsync()).AnyAsync(x => x.Id == entity.Id))
            {
                return await this._callInfoRepository.UpdateAsync(entity);
            }
            else
            {
                return await this._callInfoRepository.InsertAsync(entity);
            }
        }

        /// <summary>
        /// 通过患者分诊id或挂号流水号获取记录
        /// </summary>
        /// <param name="triageId">患者分诊id</param>
        /// <param name="registerNo">挂号流水号</param>
        /// <returns></returns>
        public async Task<CallInfo> GetInfoByTriageOrRegisterAsync(Guid triageId, string registerNo = "")
        {
            // 当患者分诊id为空时，不查询数据
            if (triageId == Guid.Empty && string.IsNullOrEmpty(registerNo)) return default;

            var item = await (await this._callInfoRepository.GetQueryableAsync())
                // 挂号流水号不为空
                .WhereIf(triageId == Guid.Empty && !string.IsNullOrEmpty(registerNo), x => x.RegisterNo == registerNo)
                .FirstOrDefaultAsync();

            return item;
        }
    }
}
