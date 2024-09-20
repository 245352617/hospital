using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.DataElements.Entities; 

namespace YiJian.EMR.DataElements.Contracts
{
    /// <summary>
    /// 输入域类型下拉项目
    /// </summary>
    public interface IDataElementDropdownRepository : IRepository<DataElementDropdown, Guid>
    {
        
    }
}
