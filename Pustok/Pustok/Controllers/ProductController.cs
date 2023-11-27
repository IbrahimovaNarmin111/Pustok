using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.DAL;
using Pustok.Models;
using Pustok.ViewModels;

namespace Pustok.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;
        public ProductController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            Book currentBook= await _db.Books.Include(a=>a.Author).Include(bi=>bi.BookImages).Include(c=>c.Category).Include(bt=>bt.BookTags).ThenInclude(t=>t.Tag).FirstOrDefaultAsync(b=>b.Id==id);  
            if(currentBook==null) 
            {
                return NotFound();
            }
            List<Book>relatedBooks= await _db.Books.Where(b=>b.CategoryId==currentBook.CategoryId && b.Id!=id ).ToListAsync();
            BookDetailVM bookDetailVM = new BookDetailVM()
            {
                CurrentBook = currentBook,
                RelatedBooks = relatedBooks,
            };
            return View(bookDetailVM);
        }
    }
}
