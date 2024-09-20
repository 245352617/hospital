using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.BodyParts.IRepository
{
    /// <summary>
    /// 表:人体图-编号字典
    /// </summary>
    public interface IDictCanulaPartRepository : IRepository<DictCanulaPart, Guid>, IBaseRepository<DictCanulaPart, Guid>
    {
        #region 定义接口
        /// <summary>
        /// 判断人体图部位是否存在
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="partName"></param>
        /// <param name="partNumber"></param>
        /// <returns>true存在 false不存在</returns>
        public Task<bool> CheckExists(string deptCode, string moduleCode, string partName, string partNumber);
        #endregion
    }
}
