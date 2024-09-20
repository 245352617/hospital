using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Recipes.Preferences.Entities;

namespace YiJian.Recipe.Preferences.Contracts
{
    /// <summary>
    /// 快速开嘱医嘱
    /// </summary>
    public interface IQuickStartAdviceRepository : IRepository<QuickStartAdvice, Guid>
    {

    }
}
