using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileStore_Project.Models;
using MobileStore_Project.Models.Authentication;
using System.Diagnostics;

namespace Project_BE_Web.Controllers
{
    public class HomeController : Controller
    {
        MobileStoreContext db;
        private int PAGE_SIZE = 10;
        
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
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(products);
        }

        [Route("/ListProduct")]
        public IActionResult ListProduct(int page = 1)
        {
            var products = db.SanPhams.ToList();
            ViewBag.CurrPage = page;
            int skip = (page - 1) * PAGE_SIZE < 0 ? 0 : (page - 1) * PAGE_SIZE;
            products = products.Skip(skip).Take(PAGE_SIZE).ToList();
            return PartialView("Product",products);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        [Route("/Login")]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return View("Login");
            }
            else return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Route("/Login")]
        public IActionResult Login(KhachHang kh)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                var admins = db.Admins.Where(ad => ad.Email.Equals(kh.Email) && ad.Passw.Equals(kh.Passw)).FirstOrDefault();
                if (admins != null)
                {
                    HttpContext.Session.SetString("Roles", "Admin");
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    var khachhang = db.KhachHangs.Where(khachH => khachH.Email.Equals(kh.Email)
                        && khachH.Passw.Equals(kh.Passw)).FirstOrDefault();

                    if (khachhang != null)
                    {

                        HttpContext.Session.SetString("Email", khachhang.Email.ToString());

                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("/Signup")]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Signup")]
        public IActionResult Signup([Bind("Email, Passw, HoTen, SoDt, DiaChi")] KhachHang kh)
        {
            var list = db.KhachHangs.ToList();
            kh.Id = (list[list.Count - 1].Id) + 1;
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Login", "Home");
            }
            return View();

        }

        [Authentication]
        [Route("/Cart")]
        public IActionResult Cart()
        {
            return View();
        }
        [Route("/ProductInformation")]
        public IActionResult ProductInformation(int productID)
        {
            var product = db.SanPhams.SingleOrDefault(p => p.MaSp == productID);
            var imageProduct = db.SanPhams.Where(p => p.MaSp == productID).ToList();
            ViewBag.Image = imageProduct;
            return View(product);
        }
        [Authentication]
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
            var products = db.SanPhams.Where(p => p.Giaban >= (decimal)minPrice && (maxPrice == null || p.Giaban <= (decimal)maxPrice)).ToList();
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
                    products = products.OrderByDescending(p => p.Giaban);
                    break;
                case "price_asc":
                    products = products.OrderBy(p => p.Giaban);
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