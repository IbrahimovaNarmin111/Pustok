using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Areas.Admin.ViewModels.Book;
using Pustok.DAL;
using Pustok.Models;

namespace Pustok.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private AppDbContext _db;
       public BookController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Book>Books=await _db.Books.Where(x=>x.IsDeleted==false).Include(x=>x.Author).Include(x=>x.Category).Include(x=>x.BookImages).Include(x=>x.BookTags).ThenInclude(x=>x.Tag).ToListAsync();
            return View(Books);
        }
        public async Task<IActionResult>Create()
        {
            CreateBookVM bookvm = new CreateBookVM();
            bookvm.Authors=await _db.Authors.ToListAsync();
            bookvm.Categories=await _db.Categories.ToListAsync();
            return View(bookvm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookVM bookvm)
        {
            if (bookvm == null) return NotFound();

            if(!ModelState.IsValid)
            {
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                return View(bookvm);
            }
            if(bookvm.AuthorId==0)
            {
                ModelState.AddModelError("AuthorId", "Mutleq yazici secilmelidir");
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                return View(bookvm);
            }
            if (bookvm.CategoryId == 0)
            {
                ModelState.AddModelError("CategoryId", "Mutleq kateqoriya secilmelidir");
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                return View(bookvm);
            }
            if (!await _db.Authors.AnyAsync(x=>x.Id==bookvm.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "This Author doesn't exist");
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                return View(bookvm);
            }
            if (!await _db.Categories.AnyAsync(x => x.Id == bookvm.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "This Category doesn't exist");
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                return View(bookvm);
            }
            Book book = new Book();
            book.Name= bookvm.Name;
            book.Description= bookvm.Description;
            book.Page= bookvm.Page;
            book.CostPrice= bookvm.CostPrice;
            book.SalePrice= bookvm.SalePrice;
            book.Discount= bookvm.Discount;
            book.AuthorId= bookvm.AuthorId;
            book.CategoryId= bookvm.CategoryId;
            book.IsDeleted = false;
            
            
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));   
            
        }
        public async Task<IActionResult>Update(int id)
        {
            Book exist=await _db.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (exist is null) return NotFound();   
            CreateBookVM bookvm = new CreateBookVM();
            bookvm.Name = exist.Name;
            bookvm.Description = exist.Description;
            bookvm.Page = exist.Page;
            bookvm.CostPrice = exist.CostPrice;
            bookvm.SalePrice = exist.SalePrice;
            bookvm.Discount = exist.Discount;
            bookvm.AuthorId = exist.AuthorId;
            bookvm.CategoryId = exist.CategoryId;
            bookvm.Authors = await _db.Authors.ToListAsync();
            bookvm.Categories = await _db.Categories.ToListAsync();





            return View(bookvm);
        }
        

    }
}
