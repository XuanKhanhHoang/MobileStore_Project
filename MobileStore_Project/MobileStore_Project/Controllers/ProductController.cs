using Microsoft.AspNetCore.Mvc;

namespace Project_BE_Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
