using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.OwnMediciness.Dto;

namespace YiJian.OwnMediciness
{
    /// <summary>
    /// 自备药
    /// </summary>
    public interface IOwnMedicineAppService : IApplicationService
    {

        /// <summary>
        /// 查询患者所有的自备药
        /// </summary>
        /// <returns></returns>
        public Task<List<OwnMedicineDto>> GetMyAllAsync(Guid piid);

        /// <summary>
        /// 更新我的自备药
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<bool> ModifyOwnMedicinesAsync(ModifyOwnMedicineDto model);

        // /// <summary>
        // /// 移除选中的一批自备药
        // /// </summary>
        // /// <param name="ids"></param>
        // /// <returns></returns>
        // public Task<bool> RemoveOwnMedicinesAsync(List<int> ids);
    }
}
