using Microsoft.AspNetCore.Mvc;
using MobileStore_Project.Models;

namespace MobileStore_Project.ViewComponents
{
    public class BrandViewComponent:ViewComponent
    {
        MobileStoreContext db;
        List<Nsx> brands;
        public BrandViewComponent(MobileStoreContext _context)
        {
            db = _context;
            brands = db.Nsxes.ToList();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderBrand", brands);
        }
    }
}
