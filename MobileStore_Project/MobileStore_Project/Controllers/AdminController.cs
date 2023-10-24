using Microsoft.AspNetCore.Mvc;

namespace Project_BE_Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddNewProduct()
        {
            return View();
        }
    }
}
