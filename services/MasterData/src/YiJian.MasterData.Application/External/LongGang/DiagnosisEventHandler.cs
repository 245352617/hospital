using DotNetCore.CAP;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using YiJian.MasterData.External.LongGang.Diagnoses;

namespace YiJian.MasterData.External.LongGang;

public class DiagnosisEventHandler : MasterDataAppService, ILongGangHospitalEventHandler, IDistributedEventHandler<List<DiagnoseEto>>,
              ITransientDependency
{

    public ICapPublisher CapPublisher { get; set; }
    public async Task HandleEventAsync(List<DiagnoseEto> eventData)
    {
        await CapPublisher.PublishAsync("sync.diagnose.from.masterdata", eventData);
    }
}
