using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.XmlHistories.Entities;

namespace YiJian.EMR.XmlHistories.Contracts
{
    /// <summary>
    /// 电子病历留痕
    /// </summary>
    public interface IXmlHistoryRepository : IRepository<XmlHistory, Guid>
    {

    }
}
