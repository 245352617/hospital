using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace YiJian.Nursing.Controllers
{
    /// <summary>
    /// 主页控制器
    /// </summary>
    public class HomeController : AbpController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}
