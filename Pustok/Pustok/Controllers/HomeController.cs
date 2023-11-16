using Microsoft.AspNetCore.Mvc;
using Pustok.DAL;
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
            };

            return View(homeVM);
        }
    }
}
