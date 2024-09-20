using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.Handover
{
    /// <summary>
    /// 测试服务
    /// </summary>
   public class TestAppService: HandoverAppService
    {
        /// <summary>
        /// Test方法
        /// </summary>
        /// <returns></returns>
        public async Task GetTestAsync()
        {
            await Task.CompletedTask;
        }
    }
}
