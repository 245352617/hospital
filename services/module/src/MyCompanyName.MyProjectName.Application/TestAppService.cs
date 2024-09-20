using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyName.MyProjectName
{
    /// <summary>
    /// 测试服务
    /// </summary>
   public class TestAppService: MyProjectNameAppService
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
