using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiJian.ChargeBills.Dto;
using YiJian.DoctorsAdvices;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.Recipe;
using YiJian.Recipes.DoctorsAdvices.Contracts;

namespace YiJian.ChargeBills
{
    /// <summary>
    /// 收费记录单
    /// </summary>
    public class ChargeBillAppService : RecipeAppService, IChargeBillAppService
    {
        private readonly IDoctorsAdviceRepository _doctorsAdviceRepository;
        private readonly IPrescribeRepository _prescribeRepository;
        private readonly ILogger<DoctorsAdviceAppService> _logger;

        /// <summary>
        /// 收费记录单
        /// </summary>
        /// <param name="doctorsAdviceRepository"></param>
        /// <param name="prescribeRepository"></param>
        /// <param name="logger"></param>
        public ChargeBillAppService(
            IDoctorsAdviceRepository doctorsAdviceRepository,
            IPrescribeRepository prescribeRepository,
            ILogger<DoctorsAdviceAppService> logger)
        {
            _doctorsAdviceRepository = doctorsAdviceRepository;
            _prescribeRepository = prescribeRepository;
            _logger = logger;
        }

        /// <summary>
        /// 院前收费单
        /// </summary>
        /// <param name="piid"></param>
        /// <returns></returns>
        public async Task<PreBillDto> PreBillAsync(Guid piid)
        {
            var data = new PreBillDto();
            var statusSubmit = new List<ERecipeStatus>{
                ERecipeStatus.Submitted,
                ERecipeStatus.Confirmed,
                //ERecipeStatus.Stopped, 争议状态
                ERecipeStatus.Executed
            };

            var query = await (await _doctorsAdviceRepository.GetQueryableAsync())
                .Where(w => w.PIID == piid && w.PlatformType == EPlatformType.PreHospital)
                .Select(s => new
                {
                    Name = s.Name,
                    ChargeCode = s.ChargeCode,
                    ChargeName = s.ChargeName,
                    ItemType = s.ItemType,
                    Price = s.Amount,
                    Status = s.Status
                })
                .ToListAsync();

            var list = query.Where(w => statusSubmit.Contains(w.Status)).ToList();  //已提交的记录
            var group = list.GroupBy(g => g.ChargeCode);

            foreach (var item in group)
            {
                var first = item.FirstOrDefault();
                var category = new PreBillCategoryDto
                {
                    ChargeCode = first.ChargeCode,
                    ChargeName = first.ChargeName
                };

                var bills = item.ToList();
                foreach (var bill in bills)
                {
                    var adviceBill = new AdviceBillsDto
                    {
                        ItemType = bill.ItemType,
                        Name = bill.Name,
                        Price = bill.Price,
                        Status = bill.Status
                    };
                    category.AdviceBill.Add(adviceBill);
                }

                data.BillCategory.Add(category);
            }

            var statusNoSubmit = new List<ERecipeStatus>{
                ERecipeStatus.Saved,
                ERecipeStatus.Rejected,
            };

            data.SubmitTotal = list.Sum(s => s.Price);
            data.NoSubmitTotal = query.Where(w => statusNoSubmit.Contains(w.Status)).Sum(s => s.Price);

            return data;
        }

        /// <summary>
        /// 急诊收费单
        /// </summary>
        /// <param name="piid">患者唯一标识</param>
        /// <returns></returns>
        public async Task<BillDto> BillAsync(Guid piid)
        {
            var query = await (
                from d in (await _doctorsAdviceRepository.GetQueryableAsync())
                    .Where(w => w.PIID == piid && w.PlatformType == EPlatformType.EmergencyTreatment)
                join p in (await _prescribeRepository.GetQueryableAsync())
                on d.Id equals p.DoctorsAdviceId into temp
                from pr in temp.DefaultIfEmpty()
                select new
                {
                    Status = d.Status,
                    Price = d.Price,
                    MaterialPrice = pr != null ? pr.MaterialPrice.Value : 0,
                }).ToListAsync();

            var status = new List<ERecipeStatus> { ERecipeStatus.Submitted, ERecipeStatus.Confirmed, ERecipeStatus.Executed };
            var data = new BillDto
            {
                RowCount = query.Count(),
                NoSubmitRowCount = query.Where(w => w.Status == ERecipeStatus.Saved).Count(),
                SubmitRowCount = query.Where(w => w.Status == ERecipeStatus.Submitted).Count(),
                ExecRowCount = query.Where(w => w.Status == ERecipeStatus.Executed).Count(),
                StopRowCount = query.Where(w => w.Status == ERecipeStatus.Stopped).Count(),
                ObsRowCount = query.Where(w => w.Status == ERecipeStatus.Cancelled).Count(),
                NoSubmitAmount = query.Where(w => w.Status == ERecipeStatus.Saved).Sum(s => s.Price),
                NoSubmitConsumablesAmount = query.Where(w => w.Status == ERecipeStatus.Saved).Sum(s => s.MaterialPrice),
                SubmitAmount = query.Where(w => status.Contains(w.Status)).Sum(s => s.Price),
                SubmitConsumablesAmount = query.Where(w => status.Contains(w.Status)).Sum(s => s.MaterialPrice)
            };
            return await Task.FromResult(data);
        }

    }
}
