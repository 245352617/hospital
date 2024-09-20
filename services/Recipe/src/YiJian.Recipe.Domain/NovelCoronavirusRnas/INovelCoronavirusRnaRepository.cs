using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Recipe;

namespace YiJian.Recipes
{
    /// <summary>
    /// 新冠rna检测申请
    /// </summary>
    public interface INovelCoronavirusRnaRepository : IRepository<NovelCoronavirusRna, Guid>
    {

    }
}
