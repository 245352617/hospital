using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YiJian.ECIS.IMService.HubClientContracts
{
    /// <summary>
    /// 叫号中心强类型集线器接口
    /// </summary>
    public interface ICallClient
    {
        /// <summary>
        /// 刷新叫号列表
        /// </summary>
        /// <returns></returns>
        Task CallingQueueChanged();
    }
}
