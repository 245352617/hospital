using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.DataElements.Entities; 

namespace YiJian.EMR.DataElements.Contracts
{
    /// <summary>
    /// 数据元
    /// </summary>
    public interface IDataElementRepository : IRepository<DataElement, Guid>
    {
        
    }
}
