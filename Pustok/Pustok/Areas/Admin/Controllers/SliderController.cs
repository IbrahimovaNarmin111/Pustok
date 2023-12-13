using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Pustok.DAL;
using Pustok.Helpers;
using Pustok.Models;
using System.Data;
using System.Drawing;

namespace Pustok.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        private AppDbContext _db;
        private IWebHostEnvironment _env;
        public SliderController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _db.Sliders.ToListAsync();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            if(slider.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "image can not be null");
                return View();
            }
            if(!slider.ImageFile.CheckFileType("image/"))
            {
                ModelState.AddModelError("ImageFile", "image must be image type");
                return View();
            }
            if(slider.ImageFile.CheckFileLength(300))
            {
                ModelState.AddModelError("ImageFile", "image must be more than" + 300 + "kb");
                return View();
            }
            slider.Image = slider.ImageFile.CreateFile(_env.WebRootPath, "uploads/slider");
            await _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider dbSlider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSlider == null)
            {
                return BadRequest();
            }
            return View(dbSlider);
        }
        [HttpPost]

        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider dbSlider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbSlider == null)
            {
                return BadRequest();
            }
            if (slider.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "ImageFile can not be null");
                return View();
            }

            if (slider.ImageFile != null)
            {
                if (!slider.ImageFile.CheckFileType("image/"))
                {
                    ModelState.AddModelError("ImageFile", "ImageFile must be image type");
                    return View();
                }
                if (slider.ImageFile.CheckFileLength(300))
                {
                    ModelState.AddModelError("ImageFile", "ImageFile can not be more than" + 300 + "kb");
                    return View();
                }

                dbSlider.Image.DeleteFile(_env.WebRootPath, "uploads/slider");
                dbSlider.Image = slider.ImageFile.CreateFile(_env.WebRootPath, "uploads/slider");


            }
            dbSlider.Title1 = slider.Title1;
            dbSlider.Title2 = slider.Title2;
            dbSlider.Description = slider.Description;
            dbSlider.Order = slider.Order;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Slider slider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);

            if (slider == null)
            {
                return NotFound();
            }

            slider.Image.DeleteFile(_env.WebRootPath, "uploads/slider");

            _db.Sliders.Remove(slider);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
