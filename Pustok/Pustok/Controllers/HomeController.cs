using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.DAL;
using Pustok.Models;
using Pustok.ViewModels;

namespace Pustok.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)  
        {
            _db = db;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = _db.Sliders.OrderBy(x=>x.Order).ToList(),
                Features = _db.Features.ToList(),
                DiscountBooks=_db.Books.Include(x=>x.BookImages).Include(x=>x.Category).Include(x=>x.Author).Include(x=>x.BookTags).ThenInclude(x=>x.Tag).Where(x=>x.Discount>0).Take(5).ToList(),
                NewBooks=_db.Books.Include(x => x.BookImages).Include(x => x.Category).Include(x => x.Author).Include(x => x.BookTags).ThenInclude(x => x.Tag).OrderByDescending(x=>x.Id).Take(5).ToList()
            };

            return View(homeVM);
        }
    }
}
