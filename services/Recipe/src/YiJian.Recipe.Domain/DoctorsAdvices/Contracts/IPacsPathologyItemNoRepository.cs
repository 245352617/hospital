using Volo.Abp.Domain.Repositories;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 描    述:检查病理小项序号仓储接口
    /// 创 建 人:杨凯
    /// 创建时间:2023/11/29 9:35:03
    /// </summary>
    public interface IPacsPathologyItemNoRepository : IRepository<PacsPathologyItemNo, int>
    {
    }
}
