using Microsoft.AspNetCore.Mvc;
using MobileStore_Project.Models;
using System.Diagnostics;

namespace Project_BE_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MobileStoreContext db;
        List<SanPham> products;
        private int PAGE_SIZE = 8;
        public HomeController(ILogger<HomeController> logger, MobileStoreContext db)
        {
            _logger = logger;
            this.db = db;
            products = db.SanPhams.ToList();
        }

        public IActionResult Index()
        {
            ViewBag.PageCount = Math.Round(products.Count() / PAGE_SIZE * 1.0);
            return View(products);
        }

        [Route("/ListProduct")]
        public IActionResult ListProduct(int page = 1)
        {
            ViewBag.CurrPage = page;
            int skip = (page - 1) * PAGE_SIZE < 0 ? 0 : (page - 1) * PAGE_SIZE;
            products = products.Skip(skip).Take(PAGE_SIZE).ToList();
            return PartialView(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("/LoginRegister")]
        public IActionResult LoginRegister()
        {
            return View();
        }
        [Route("/Cart")]
        public IActionResult Cart()
        {
            return View();
        }
        [Route("/ProductInformation")]
        public IActionResult ProductInformation()
        {
            return View();
        }
        [Route("/SearchOrder")]
        public IActionResult SearchOrder()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}