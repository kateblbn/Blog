using Microsoft.AspNetCore.Mvc;

namespace Blog.web.Controllers
{
    public class AdminTagController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
