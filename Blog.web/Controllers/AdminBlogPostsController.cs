using Microsoft.AspNetCore.Mvc;

namespace Blog.web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
