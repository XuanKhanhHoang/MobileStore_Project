using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileStore_Project.Models;
using System.Diagnostics;

namespace Project_BE_Web.Controllers
{
    public class HomeController : Controller
    {
        MobileStoreContext db;
        private int PAGE_SIZE = 8;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MobileStoreContext db)
        {
            _logger = logger;
            this.db=db;
    
        }

        public IActionResult Index()
        {
            var products = db.SanPhams.ToList();
            ViewBag.PageCount = Math.Ceiling(1.0 * products.Count / PAGE_SIZE);
            return View(products);
        }

        [Route("/ListProduct")]
        public IActionResult ListProduct(int page = 1)
        {
            var Products = db.SanPhams.ToList();
            ViewBag.CurrPage = page;
            int skip = (page - 1) * PAGE_SIZE < 0 ? 0 : (page - 1) * PAGE_SIZE;
            Products = Products.Skip(skip).Take(PAGE_SIZE).ToList();
            return PartialView("Product",Products);
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

        [Route("/filterByBrand")]
        public IActionResult fiterByBrand(string? idbrand)
        {
            var products = db.SanPhams.Where(p => p.MaNsx == idbrand).ToList();
            if(products.Count == 0)
            {
                return PartialView("ListEmpty");
            }
            
            return PartialView("Product", products);

        }

        [Route("/filterByDiscount")]
        public IActionResult fiterByDiscount()
        {

            var products = db.SanPhams.Where(p => p.KhuyenMai != null).ToList();
            if (products.Count == 0)
            {
                return PartialView("ListEmpty");
            }
            
            return PartialView("Product", products);
           
        }
        [Route("/filterByPrice")]
        public IActionResult fiterByPrice(float minPrice, float? maxPrice)
        {
            minPrice = minPrice * 1000000;
            maxPrice = maxPrice * 1000000;
            var products = db.SanPhams.Where(p => p.Gia >= (decimal)minPrice && (maxPrice == null || p.Gia <= (decimal)maxPrice)).ToList();
            if (products.Count == 0)
            {
                return PartialView("ListEmpty");
            }
           
            return PartialView("Product", products);
           
        }

        [Route("/SortProducts")]
        public async Task<IActionResult> SortProducts(string sortOrder)
        {
            var products = from product in db.SanPhams select product;
            switch (sortOrder)
            {
                case "price_desc":
                    products = products.OrderByDescending(p => p.Gia);
                    break;
                case "price_asc":
                    products = products.OrderBy(p => p.Gia);
                    break;
                case "default":
                    break;
                   
            }
            return PartialView("Product",await products.AsNoTracking().ToListAsync());

        }

        [Route("/Search")]
        public IActionResult Search(string? searchString) 
        {

            
          var  products = db.SanPhams
         .Where(p => p.TenSp.ToUpper().Replace(" ","").Contains(searchString.ToUpper().Replace(" ",""))).ToList();
            if (products.Count == 0)
            {
                return PartialView("ListEmpty");
            }
            
            return PartialView("Product", products);
      
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}