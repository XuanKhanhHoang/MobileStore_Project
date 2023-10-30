using Microsoft.AspNetCore.Mvc;
using MobileStore_Project.Models;

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
