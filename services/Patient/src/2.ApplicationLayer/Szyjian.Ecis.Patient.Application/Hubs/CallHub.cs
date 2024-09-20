using DotNetCore.CAP;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.AspNetCore.SignalR;

namespace Szyjian.Ecis.Patient.Application.Hubs
{
    /// <summary>
    /// 叫号服务的即时消息中心
    /// </summary>
    public class CallHub : AbpHub<ICallClient>, ICapSubscribe
    {
        private readonly IHubContext<CallHub> _hubContext;
        private readonly IFreeSql _freeSql;

        public CallHub(IHubContext<CallHub> hubContext, IFreeSql freeSql)
        {
            this._hubContext = hubContext;
            this._freeSql = freeSql;
        }

        /// <summary>
        /// Hub连接
        /// 连接成功后，向当前客户端发送正在叫号中的患者列表
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// 刷新叫号列表
        /// </summary>
        /// <returns></returns>
        [HubMethodName("calling-queue-changed")]
        public async Task RefreshCallingQuequeAsync()
        {
            await Clients.All.CallingQueueChanged();
        }

        /// <summary>
        /// 改变诊室
        /// </summary>
        /// <param name="consultingRoomCode">诊室代码</param>
        /// <param name="doctorId">医生工号（用户名）</param>
        /// <returns></returns>
        [HubMethodName("change-consulting-room")]
        public async Task ChangeConsultingRoomAsync(string consultingRoomCode, string doctorId)
        {
            // 获取当前科室当前要叫号的患者信息
            var callingAdmissionRecord = await _freeSql.Select<AdmissionRecord>()
                .Where(a => a.CallConsultingRoomCode == consultingRoomCode)
                .WhereIf(!string.IsNullOrWhiteSpace(doctorId), a => a.CallingDoctorId == doctorId)
                .Where(x => x.CallStatus == CallStatus.Calling && x.VisitStatus == VisitStatus.待就诊)
                .OrderByPropertyName(nameof(AdmissionRecord.TriageLevel))
                .OrderByPropertyName(nameof(AdmissionRecord.VisitDate), false)
                .FirstAsync();
            if (callingAdmissionRecord != null)
            {
                var eto = new CallingEto
                {
                    DoctorId = callingAdmissionRecord.CallingDoctorId,
                    DoctorName = callingAdmissionRecord.CallingDoctorName,
                    ConsultingRoomCode = callingAdmissionRecord.CallConsultingRoomCode,
                    ConsultingRoomName = callingAdmissionRecord.CallConsultingRoomName,
                    PatientId = callingAdmissionRecord.PatientID,
                    PatientName = callingAdmissionRecord.PatientName,
                    DepartmentCode = callingAdmissionRecord.DeptCode,
                    DepartmentName = callingAdmissionRecord.TriageDeptName,
                    CallingSn = callingAdmissionRecord.CallingSn,
                    LastCalledTime = DateTime.Now,
                };
                await this._hubContext.Clients.Client(this.Context.ConnectionId).SendAsync("Calling", eto);
            }
            else
            {
                await this._hubContext.Clients.Client(this.Context.ConnectionId).SendAsync("Calling", null);
            }
        }
    }
}