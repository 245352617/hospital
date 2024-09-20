using Microsoft.AspNetCore.Mvc;

namespace SamJan.MicroService.PreHospital.TriageService
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        /// <summary>
        /// 重定向到
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return Redirect("/swagger/index.html");
        }
        
    }
}