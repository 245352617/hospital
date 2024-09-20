using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 医嘱打印信息
    /// </summary>
    public interface IPrintInfoRepository : IRepository<PrintInfo, Guid>
    {
    }
}
