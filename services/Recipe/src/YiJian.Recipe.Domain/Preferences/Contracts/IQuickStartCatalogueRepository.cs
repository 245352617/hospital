using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Recipes.Preferences.Entities;

namespace YiJian.Recipe.Preferences.Contracts
{
    /// <summary>
    /// 快速开嘱目录
    /// </summary>
    public interface IQuickStartCatalogueRepository : IRepository<QuickStartCatalogue, Guid>
    {

    }
}
