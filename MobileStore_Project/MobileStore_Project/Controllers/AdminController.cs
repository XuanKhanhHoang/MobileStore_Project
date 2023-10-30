using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileStore_Project.Models;

namespace Project_BE_Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private MobileStoreContext db;
        private int PAGE_SIZE = 8;
        public AdminController(ILogger<AdminController> logger, MobileStoreContext db)
        {
            _logger = logger;
            this.db = db;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Roles") == "Admin")
            {
                List<SanPham> lst=db.SanPhams.ToList();
                ViewBag.LstNSX = db.Nsxes.ToList();
                ViewBag.PageCount = Math.Ceiling(1.0 * lst.Count / PAGE_SIZE);
                return View(lst);
            }
            else return RedirectToAction("Login", "Home");

        }
        [Route("/List")]
        public IActionResult List(string id, int page = 1)
        {
            List<SanPham> lst = db.SanPhams.Include(s => s.MaNsxNavigation).ToList();
            if (id != null)
            {
                lst = db.SanPhams.Where(product => product.MaSp == int.Parse(id)).ToList();
                return PartialView("List", lst);
            }
            int skip = (page - 1) * PAGE_SIZE < 0 ? 0 : (page - 1) * PAGE_SIZE;
            lst = lst.Skip(skip).Take(PAGE_SIZE).ToList();
            return PartialView("List", lst);
        }
        public IActionResult AddNewProduct()
        {
            if (HttpContext.Session.GetString("Roles") == "Admin")
            {
                ViewBag.LstNSX = db.Nsxes.ToList();
                return View();
            }
            else return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        public IActionResult AddNewProduct(List<IFormFile> fileList, [Bind("MaNsx,SoLuong,TenSp,CauHinh,MoTa,PhienBan,KhuyenMai,GiaBan,MauSac,AnhSp")] SanPham s)
        {

            
            db.SanPhams.Add(s);
            db.SaveChanges();
            foreach(var file in fileList)
            {
                if (file != null)
                {
                    string fileNameOnlyName = file.FileName;
                    string fileName = Path.GetFileName(fileNameOnlyName);
                    string upLoadedFileDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);

                    var strem = new FileStream(upLoadedFileDir, FileMode.Create);

                    try
                    {
                        file.CopyToAsync(strem);
                    }
                    catch (Exception ex) { }
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult EditAProduct(int? id)
        {
            SanPham sp;
            ViewBag.LstNSX = db.Nsxes.ToList();
            if (db.Find(typeof(SanPham), id) != null)
            {
                sp =(SanPham) db.Find(typeof(SanPham), id);
                ViewBag.NSX= sp.MaNsxNavigation.TenNsx;
                return View("AddNewProduct", sp);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult EditAProduct(List<IFormFile> fileList, [Bind("MaSp,MaNsx,SoLuong,TenSp,CauHinh,MoTa,PhienBan,KhuyenMai,GiaBan,MauSac,AnhSp")] SanPham s)

        {
           
            db.SanPhams.Update(s);
            db.SaveChanges();
            foreach (var file in fileList)
            {
                if (file != null)
                {
                    string fileNameOnlyName = file.FileName;
                    string fileName = Path.GetFileName(fileNameOnlyName);
                    string upLoadedFileDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);

                    var strem = new FileStream(upLoadedFileDir, FileMode.Create);

                    try
                    {
                        file.CopyToAsync(strem);
                    }
                    catch (Exception ex) { }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public bool DellAProduct(int id)

        {
            var entity = db.SanPhams.FirstOrDefault(e => e.MaSp == id);
            if (entity != null)
            {
                db.SanPhams.Remove(entity);
                db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
