using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.DAL;
using Pustok.Models;

namespace Pustok.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private AppDbContext _db;

        public SettingController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var setting = await _db.Settings.ToListAsync();
            return View(setting);
        }
        public async Task<IActionResult> Create()
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> Create(Setting setting)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            bool result=await _db.Settings.AnyAsync(x=>x.Key == setting.Key);   
            if(result==true)
            {
                return View();
            }
            await _db.Settings.AddAsync(setting);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //public async Task<IActionResult> Update(int id)
        //{

        //}

    }
}
