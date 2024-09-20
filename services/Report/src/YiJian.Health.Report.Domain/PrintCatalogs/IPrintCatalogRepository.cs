using System;
using System.Text;
using Volo.Abp.Domain.Repositories;
namespace YiJian.Health.Report.PrintCatalogs
{
    /// <summary>
    /// 打印目录
    /// </summary>
    public interface IPrintCatalogRepository : IRepository<PrintCatalog, Guid>
    {

    }
}
