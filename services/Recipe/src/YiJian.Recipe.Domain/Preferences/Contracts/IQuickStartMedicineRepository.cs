using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Recipes.Preferences.Entities;

namespace YiJian.Recipe.Preferences.Contracts
{
    /// <summary>
    /// 快速开嘱药品
    /// </summary>
    public interface IQuickStartMedicineRepository : IRepository<QuickStartMedicine, Guid>
    {

    }
}
