using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using YiJian.MasterData.External.LongGang.Teat;
using YiJian.MasterData.Treats;

namespace YiJian.MasterData.MasterData
{
    /// <summary>
    /// 描    述 ：同步诊疗数据
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/6/17 9:29:41
    /// </summary>
    public class TreatClientHandler : MasterDataAppService, IDistributedEventHandler<TreatsEto>,
    ITransientDependency
    {
        private readonly ITreatRepository _treatRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="treatRepository"></param>
        public TreatClientHandler(ITreatRepository treatRepository)
        {
            _treatRepository = treatRepository;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(TreatsEto eventData)
        {
            if (eventData == null) return;
            List<TreatsEto> list = new List<TreatsEto>() { eventData };

            List<Treat> treatList = new List<Treat>();
            foreach (TreatsEto item in list)
            {
                if (string.IsNullOrEmpty(item.ProjectType)) continue;
                if (string.IsNullOrEmpty(item.UseFlag) == true || item.UseFlag == "1") continue;
                treatList.Add(new Treat()
                {
                    TreatCode = item.ProjectId,
                    TreatName = item.ProjectName,
                    CategoryName = item.ProjectTypeName,
                    CategoryCode = item.ProjectType,
                    Price = (decimal)(item.Price ?? 0),
                    OtherPrice = item.Additional == "1" ? decimal.Parse(item.ChargeAmount.ToString("f2")) : 0,
                    Additional = item.Additional == "1",
                    PyCode = item.SpellCode,
                    Unit = item.Unit,
                    WbCode = item.ProjectName.FirstLetterPY(),
                    ProjectMerge = item.ProjectMerge,
                    MeducalInsuranceCode = item.MeducalInsuranceCode,
                    YBInneCode= item.YBInneCode,
                    IsDeleted = string.IsNullOrEmpty(item.UseFlag) == true || item.UseFlag == "1",
                });
            }

            List<Treat> treats = await _treatRepository.ToListAsync();
            List<Treat> updateTreats = new List<Treat>();
            //排除掉已删除的
            List<Treat> addTreats = treatList.Where(x => treats.All(a => a.TreatCode != x.TreatCode))
                .ToList();
            List<Treat> deleteTreats = treats.Where(x => treatList.All(a => a.TreatCode != x.TreatCode))
                .ToList();
            //去掉已删除的项
            treatList.RemoveAll(deleteTreats);
            //去掉新增的项
            treatList.RemoveAll(addTreats);
            treatList.ForEach(x =>
            {
                Treat data = treats.FirstOrDefault(g =>
                    x.TreatCode == g.TreatCode
                    && (g.CategoryName != x.CategoryName ||
                        x.Price != g.Price ||
                        x.Unit != g.Unit ||
                        g.CategoryCode != x.CategoryCode ||
                        x.TreatName != g.TreatName ||
                        x.PyCode != g.PyCode ||
                        x.OtherPrice != g.OtherPrice ||
                        x.ProjectMerge != g.ProjectMerge ||
                        x.IsDeleted != g.IsDeleted ||
                        x.Additional != g.Additional));
                if (data != null)
                {
                    data.Update(x.TreatName, x.Price, x.CategoryCode, x.CategoryName, x.Unit, x.OtherPrice,
                        x.ProjectMerge, x.IsDeleted, x.Additional);
                    updateTreats.Add(data);
                }
            });

            if (addTreats.Any())
            {
                await _treatRepository.InsertManyAsync(addTreats);
            }

            if (updateTreats.Any())
            {
                await _treatRepository.UpdateManyAsync(updateTreats);
            }

            if (deleteTreats.Any())
            {
                await _treatRepository.DeleteManyAsync(deleteTreats);
            }
        }
    }
}
