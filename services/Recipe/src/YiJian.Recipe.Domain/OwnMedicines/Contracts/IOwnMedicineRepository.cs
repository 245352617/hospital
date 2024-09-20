using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.OwnMedicines.Entities;

namespace YiJian.OwnMedicines.Contracts
{
    /// <summary>
    /// 自备药
    /// </summary>
    public interface IOwnMedicineRepository : IRepository<OwnMedicine, int>
    {
        /// <summary>
        /// 查询患者所有的自备药
        /// </summary>
        /// <returns></returns>
        public Task<List<OwnMedicine>> GetMyAllAsync(Guid piid);

        /// <summary>
        /// 移除选中的一批自备药
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task RemoveOwnMedicinesAsync(List<int> ids);

        /// <summary>
        /// 把推送过消息的数据标记为已经推送
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task SetPushAsync(IEnumerable<int> ids);
    }

}
