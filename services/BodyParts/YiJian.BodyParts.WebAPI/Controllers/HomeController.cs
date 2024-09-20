using Microsoft.AspNetCore.Mvc;

namespace YiJian.BodyParts.WebAPI
{
    public class HomeController : Controller
    {
        public IActionResult Index() => Redirect("/swagger");
    }
}