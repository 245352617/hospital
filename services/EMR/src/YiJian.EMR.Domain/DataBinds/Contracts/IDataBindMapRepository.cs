using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.DataBinds.Entities;

namespace YiJian.EMR.DataBinds.Contracts
{
    /// <summary>
    /// 数据绑定的数据字典
    /// </summary>
    public interface IDataBindMapRepository : IRepository<DataBindMap, Guid>
    {
        /// <summary>
        /// 批量更新绑定数据
        /// </summary>
        /// <param name="dataBindContextId"></param> 
        /// <param name="maps"></param>  
        /// <returns></returns>  
        public Task BatchUpdateAsync(Guid dataBindContextId, Dictionary<string, Dictionary<string, string>> maps);
    }
}
