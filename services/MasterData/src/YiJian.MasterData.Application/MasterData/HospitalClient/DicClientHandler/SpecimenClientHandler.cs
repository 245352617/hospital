using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using YiJian.MasterData.Labs;

namespace YiJian.MasterData.MasterData
{
    /// <summary>
    /// 描    述 ：标本字典同步
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/6/17 9:59:39
    /// </summary>
    public class SpecimenClientHandler : MasterDataAppService, IDistributedEventHandler<SpecimenEto>,
    ITransientDependency
    {
        private readonly ILabSpecimenRepository _labSpecimenRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="labSpecimenRepository"></param>
        public SpecimenClientHandler(ILabSpecimenRepository labSpecimenRepository)
        {
            _labSpecimenRepository = labSpecimenRepository;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(SpecimenEto eventData)
        {
            if (eventData == null) return;
            List<SpecimenEto> list = new List<SpecimenEto>() { eventData };

            //获取已存在的检验标本信息
            List<LabSpecimen> specimenList = await _labSpecimenRepository.ToListAsync();
            //查询新增的检验标本信息
            List<LabSpecimen> specimen = list.Select(s => new LabSpecimen
            {
                SpecimenCode = s.SpecimenNo,
                SpecimenName = s.SpecimenName,
                PyCode = string.IsNullOrEmpty(s.SpellCode) ? s.SpecimenName.FirstLetterPY() : s.SpellCode,
                WbCode = s.SpecimenName.FirstLetterWB()
            }).ToList();
            //新增的标本
            List<LabSpecimen> addSpecimen = specimen.Where(x => specimenList.All(a => a.SpecimenCode != x.SpecimenCode))
                .ToList();
            //删除的标本
            List<LabSpecimen> deleteSpecimen = specimenList.Where(x => specimen.All(a => a.SpecimenCode != x.SpecimenCode))
                .ToList();
            //修改标本
            List<LabSpecimen> updateSpecimen = new List<LabSpecimen>();
            specimen.RemoveAll(addSpecimen);
            specimen.RemoveAll(deleteSpecimen);
            specimen.ForEach(x =>
            {
                LabSpecimen data = specimenList.FirstOrDefault(g =>
                    x.SpecimenCode == g.SpecimenCode && x.SpecimenName != g.SpecimenName);
                if (data != null)
                {
                    data.Update(x.SpecimenName);
                    updateSpecimen.Add(data);
                }
            });
            if (addSpecimen.Any())
            {
                await _labSpecimenRepository.InsertManyAsync(addSpecimen);
            }

            if (updateSpecimen.Any())
            {
                await _labSpecimenRepository.UpdateManyAsync(updateSpecimen);
            }

            if (deleteSpecimen.Any())
            {
                await _labSpecimenRepository.DeleteManyAsync(deleteSpecimen);
            }
        }
    }
}
