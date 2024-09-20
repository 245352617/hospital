using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;
using YiJian.ECIS.Call.Domain.CallCenter;

namespace YiJian.ECIS.Call.CallCenter
{
    /// <summary>
    /// 处理叫号事件
    /// </summary>
    public class CallingEventHandler : ILocalEventHandler<CallingEvent>, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICallingRecordRepository _callingRecordRepository;

        public CallingEventHandler(IGuidGenerator guidGenerator, ICallingRecordRepository callingRecordRepository)
        {
            this._guidGenerator = guidGenerator;
            this._callingRecordRepository = callingRecordRepository;
        }

        public async Task HandleEventAsync(CallingEvent eventData)
        {
            var callingRecord = new CallingRecord(
                _guidGenerator.Create(),
                eventData.CallInfoId,
                eventData.DoctorId,
                eventData.DoctorName,
                eventData.ConsultingRoomCode,
                eventData.ConsultingRoomName);
            // 添加叫号记录
            await this._callingRecordRepository.InsertAsync(callingRecord);
        }
    }
}
