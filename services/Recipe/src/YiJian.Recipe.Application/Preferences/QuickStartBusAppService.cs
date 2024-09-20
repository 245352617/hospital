using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Uow;

namespace YiJian.Preferences
{
    /// <summary>
    /// 快速开嘱内容配置（个人偏好设置）
    /// </summary>
    [Authorize]
    public partial class QuickStartAppService
    {
        /// <summary>
        /// 统计快速开嘱使用数量
        /// </summary>
        /// <param name="quickStartMedicineId"></param>
        /// <returns></returns>
        [CapSubscribe("recipe.quickStart.usageCount.increase")]
        public async Task UsageCountAddAsync(Guid quickStartMedicineId)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                var query = await (from a in (await _quickStartAdviceRepository.GetQueryableAsync())
                                   join m in (await _quickStartMedicineRepository.GetQueryableAsync())
                                   on a.Id equals m.QuickStartAdviceId
                                   where m.Id == quickStartMedicineId
                                   select a).FirstOrDefaultAsync();

                if (query == null)
                {
                    await uow.CompleteAsync();
                    return;
                }

                var entity = await (await _quickStartAdviceRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == query.Id);
                entity.INCR();
                await _quickStartAdviceRepository.UpdateAsync(entity);
                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, $"用法使用次数增加异常:{ex.Message}, 更新的参数：{quickStartMedicineId}");
                await uow.RollbackAsync();
                throw;
            }
        }

    }
}
