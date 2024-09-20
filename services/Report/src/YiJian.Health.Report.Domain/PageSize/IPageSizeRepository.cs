using System;
using System.Text;
using Volo.Abp.Domain.Repositories;
namespace YiJian.Health.Report
{
    /// <summary>
    /// 纸张大小
    /// </summary>
    public interface IPageSizeRepository : IRepository<PageSize, Guid>
    {

    }
}
