using Microsoft.AspNetCore.Mvc;

namespace Blog.web.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
