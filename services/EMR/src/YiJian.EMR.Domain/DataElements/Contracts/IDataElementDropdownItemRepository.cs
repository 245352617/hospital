using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.DataElements.Entities; 

namespace YiJian.EMR.DataElements.Contracts
{
    /// <summary>
    /// 元素下拉项
    /// </summary>
    public interface IDataElementDropdownItemRepository : IRepository<DataElementDropdownItem, Guid>
    {
        
    }
}
