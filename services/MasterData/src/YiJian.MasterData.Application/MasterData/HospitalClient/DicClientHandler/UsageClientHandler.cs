using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using YiJian.MasterData.External.LongGang.Usage;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData.MasterData
{
    /// <summary>
    /// 描    述 ：同步药品用法信息
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/6/17 9:42:31
    /// </summary>
    public class UsageClientHandler : MasterDataAppService, IDistributedEventHandler<UsagesEto>,
    ITransientDependency
    {
        private readonly IMedicineUsageRepository _usageRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="usageRepository"></param>
        public UsageClientHandler(IMedicineUsageRepository usageRepository)
        {
            _usageRepository = usageRepository;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(UsagesEto eventData)
        {
            if (eventData == null) return;
            List<UsagesEto> list = new List<UsagesEto>() { eventData };


            List<MedicineUsage> usageList = new List<MedicineUsage>();
            list?.ForEach(x =>
            {
                usageList.Add(new MedicineUsage()
                {
                    UsageCode = x.DrugUsageId,
                    UsageName = x.DrugUsageName,
                    FullName = x.DrugUsageName,
                    PyCode = x.SpellCode,
                    WbCode = x.DrugUsageName.FirstLetterWB(),
                    AddCard = x.AddCard,
                    TreatCode = x.Project
                });
            });

            List<MedicineUsage> usages = await _usageRepository.ToListAsync();
            List<MedicineUsage> updateUsages = new List<MedicineUsage>();
            List<MedicineUsage> addUsages = usageList.Where(x => usages.All(a => a.UsageCode != x.UsageCode))
                .ToList();
            var deleteUsages = usages.Where(x => usageList.All(a => a.UsageCode != x.UsageCode))
                .ToList();
            usageList.ForEach(x =>
            {
                MedicineUsage data = usages.FirstOrDefault(g =>
                    g.UsageCode == x.UsageCode
                    && (g.UsageName != x.UsageName
                        || x.FullName != g.FullName
                        || g.AddCard != x.AddCard
                        || x.TreatCode != g.TreatCode));
                if (data != null)
                {
                    data.Update(x.UsageName, x.FullName, x.AddCard, x.TreatCode);
                    updateUsages.Add(data);
                }
            });
            //去掉已删除的项
            updateUsages.RemoveAll(deleteUsages);
            //去掉新增的项
            updateUsages.RemoveAll(addUsages);
            if (addUsages.Any())
            {
                await _usageRepository.InsertManyAsync(addUsages);
            }

            if (updateUsages.Any())
            {
                updateUsages.ForEach(x =>
                {
                    MedicineUsage data = usageList.FirstOrDefault(s => s.UsageCode == x.UsageCode);
                    if (data != null)
                    {
                        x.UsageName = data.UsageName;
                        x.FullName = data.FullName;
                        x.PyCode = data.FullName;
                        x.WbCode = data.WbCode;
                        x.AddCard = data.AddCard;
                        x.TreatCode = data.TreatCode;
                    }
                });
                await _usageRepository.UpdateManyAsync(updateUsages);
            }

            if (deleteUsages.Any())
            {
                await _usageRepository.DeleteManyAsync(deleteUsages);
            }
        }
    }
}
