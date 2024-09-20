using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.BodyParts.IRepository
{
    /// <summary>
    /// 表:皮肤主表
    /// </summary>
    public interface IIcuNursingSkinRepository : IRepository<IcuNursingSkin, Guid>, IBaseRepository<IcuNursingSkin, Guid>
    {
    }
}
