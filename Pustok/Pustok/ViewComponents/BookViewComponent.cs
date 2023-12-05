using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.DAL;
using Pustok.Models;

namespace Pustok.ViewComponents
{
  
    public class BookViewComponent:ViewComponent
    {
        private AppDbContext _db;

        public BookViewComponent(AppDbContext db)
        {
            _db = db;
        }
        

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Book> books = await _db.Books.Where(x => x.BookTags.Any(x => x.Tag.Name == "BestSeller")).ToListAsync();
            return View(books);
        }
    }
}
