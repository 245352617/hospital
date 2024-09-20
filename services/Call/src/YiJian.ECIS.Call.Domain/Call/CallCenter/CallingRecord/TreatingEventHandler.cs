using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace YiJian.ECIS.Call.CallCenter
{
    /// <summary>
    /// 处理接诊事件
    /// </summary>
    public class TreatingEventHandler : ILocalEventHandler<TreatedEvent>, ITransientDependency
    {
        private readonly ICallingRecordRepository _callingRecordRepository;

        public TreatingEventHandler(ICallingRecordRepository callingRecordRepository)
        {
            this._callingRecordRepository = callingRecordRepository;
        }

        public async Task HandleEventAsync(TreatedEvent eventData)
        {
            // 获取叫号中的叫号记录
            var callingRecord = await (await this._callingRecordRepository.GetQueryableAsync())
                .Where(x => x.CallInfoId == eventData.CallInfoId)
                .Where(x => x.TreatStatus == 0)
                .OrderByDescending(x => x.CreationTime)
                .FirstOrDefaultAsync();
            //非空判断
            if (callingRecord == null)
                return;
            // 接诊
            callingRecord.Treated();
            callingRecord.ConsultingRoomCode = eventData.ConsultingRoomCode;
            callingRecord.ConsultingRoomName = eventData.ConsultingRoomName;
            callingRecord.DoctorId = eventData.DoctorId;
            callingRecord.DoctorName = eventData.DoctorName;

            await this._callingRecordRepository.UpdateAsync(callingRecord);
        }
    }
}
