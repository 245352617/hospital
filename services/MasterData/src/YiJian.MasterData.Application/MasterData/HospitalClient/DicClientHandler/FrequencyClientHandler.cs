using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using YiJian.MasterData.External.LongGang.Frequency;

namespace YiJian.MasterData.MasterData
{
    /// <summary>
    /// 描    述 ：同步药品频次
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/6/17 9:49:36
    /// </summary>
    public class FrequencyClientHandler : MasterDataAppService, IDistributedEventHandler<FrequencyEto>,
    ITransientDependency
    {
        private readonly IMedicineFrequencyRepository _frequencyRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="frequencyRepository"></param>
        public FrequencyClientHandler(IMedicineFrequencyRepository frequencyRepository)
        {
            _frequencyRepository = frequencyRepository;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(FrequencyEto eventData)
        {
            if (eventData == null) return;
            List<FrequencyEto> list = new List<FrequencyEto>() { eventData };

            List<MedicineFrequency> frequencyList = new List<MedicineFrequency>();

            foreach (FrequencyEto item in list)
            {
                if (string.IsNullOrEmpty(item.DrugfrequencyCode)) continue;

                frequencyList.Add(new MedicineFrequency()
                {
                    ThirdPartyId = item.DrugfrequencyId,
                    FrequencyCode = item.DrugfrequencyCode,
                    FrequencyName = item.DrugfrequencyName,
                    FullName = item.DrugfrequencyName,
                    Times = int.Parse(item.DailyFrequency == "" ? "0" : item.DailyFrequency),
                    ExecDayTimes = item.ExecutionTime,
                    Sort = int.Parse(item.ArrangementOrder == "" ? "0" : item.ArrangementOrder)
                });
            }

            List<MedicineFrequency> frequency = await _frequencyRepository.GetListAsync(w => !string.IsNullOrEmpty(w.FrequencyCode));

            List<MedicineFrequency> updateFrequency = new List<MedicineFrequency>();
            List<MedicineFrequency> addFrequency = frequencyList.Where(x => frequency.All(a => a.FrequencyCode != x.FrequencyCode))
                .ToList();
            List<MedicineFrequency> deleteFrequency = frequency
                .Where(x => frequencyList.All(a => a.FrequencyCode != x.FrequencyCode))
                .ToList();

            //去掉已删除的项
            frequencyList.RemoveAll(deleteFrequency);
            //去掉新增的项
            frequencyList.RemoveAll(addFrequency);
            frequencyList.ForEach(x =>
            {
                MedicineFrequency data = frequency.FirstOrDefault(g =>
                    g.ThirdPartyId == x.ThirdPartyId
                    && (g.FrequencyCode != x.FrequencyCode
                        || g.FrequencyName != x.FrequencyName
                        || x.FullName != g.FullName
                        || x.Times != g.Times
                        || x.ExecDayTimes != g.ExecDayTimes));
                if (data != null)
                {
                    data.Update(x.FrequencyCode, x.FrequencyName, x.FullName, x.Times, x.ExecDayTimes);
                    updateFrequency.Add(data);
                }
            });

            if (addFrequency.Any())
            {
                await _frequencyRepository.InsertManyAsync(addFrequency);
            }

            if (updateFrequency.Any())
            {
                await _frequencyRepository.UpdateManyAsync(updateFrequency);
            }

            if (deleteFrequency.Any())
            {
                await _frequencyRepository.DeleteManyAsync(deleteFrequency);
            }
        }
    }
}
