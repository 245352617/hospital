using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 描    述:检查病理小项仓储接口
    /// 创 建 人:杨凯
    /// 创建时间:2023/11/24 14:35:52
    /// </summary>
    public interface IPacsPathologyItemRepository : IRepository<PacsPathologyItem, Guid>
    {
    }
}
