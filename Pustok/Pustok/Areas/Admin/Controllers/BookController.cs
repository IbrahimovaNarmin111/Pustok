using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Areas.Admin.ViewModels;
using Pustok.Controllers;
using Pustok.DAL;
using Pustok.Helpers;
using Pustok.Models;

namespace Pustok.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private AppDbContext _db;
        private IWebHostEnvironment _env;
        public BookController(AppDbContext db,IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Book> Books = await _db.Books.Where(x => x.IsDeleted == false).Include(x => x.Author).Include(x => x.Category).Include(x => x.BookImages).Include(x => x.BookTags).ThenInclude(x => x.Tag).ToListAsync();
            return View(Books);
        }
        public async Task<IActionResult> Create()
        {
            CreateBookVM bookvm = new CreateBookVM();
            bookvm.Authors = await _db.Authors.ToListAsync();
            bookvm.Categories = await _db.Categories.ToListAsync();
            bookvm.Tags = await _db.Tags.ToListAsync();
            return View(bookvm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookVM bookvm)
        {
            if (bookvm == null) return NotFound();

            if (!ModelState.IsValid)
            {
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                bookvm.Tags = await _db.Tags.ToListAsync();
                return View(bookvm);
            }
            if (bookvm.AuthorId == 0)
            {
                ModelState.AddModelError("AuthorId", "Mutleq yazici secilmelidir");
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                bookvm.Tags = await _db.Tags.ToListAsync();
                return View(bookvm);
            }
            if (bookvm.CategoryId == 0)
            {
                ModelState.AddModelError("CategoryId", "Mutleq kateqoriya secilmelidir");
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                bookvm.Tags = await _db.Tags.ToListAsync();
                return View(bookvm);
            }
            if (!await _db.Authors.AnyAsync(x => x.Id == bookvm.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "This Author doesn't exist");
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                bookvm.Tags = await _db.Tags.ToListAsync();
                return View(bookvm);
            }
            if (!await _db.Categories.AnyAsync(x => x.Id == bookvm.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "This Category doesn't exist");
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                bookvm.Tags = await _db.Tags.ToListAsync();
                return View(bookvm);
            }

            Book book = new Book();
           
            book.Name = bookvm.Name;
            book.Description = bookvm.Description;
            book.Page = bookvm.Page;
            book.CostPrice = bookvm.CostPrice;
            book.SalePrice = bookvm.SalePrice;
            book.Discount = bookvm.Discount;
            book.AuthorId = bookvm.AuthorId;
            book.CategoryId = bookvm.CategoryId;
            book.IsDeleted = false;
            book.BookTags = new List<BookTag>();
            book.BookImages = new List<BookImage>();
         
            if(bookvm.TagIds is not null)
            {
                foreach(int tagId in bookvm.TagIds)
                {
                    Tag tag=await _db.Tags.FirstOrDefaultAsync(x=>x.Id == tagId);
                    if(tag is null)
                    {
                        ModelState.AddModelError("TagIds", "Bele bir tag movcud deyil");
                        bookvm.Authors = await _db.Authors.ToListAsync();
                        bookvm.Categories = await _db.Categories.ToListAsync();
                        bookvm.Tags = await _db.Tags.ToListAsync();
                        return View(bookvm);
                    }
                    BookTag bookTag = new BookTag()
                    {
                        TagId = tagId,
                        Book = book

                    };
                    book.BookTags.Add(bookTag);
                }
            }
           

            await _db.Books.AddAsync(book);

            if (!bookvm.MainPhoto.CheckFileType("image/"))
            {
                ModelState.AddModelError("MainPhoto", "File must be image type");
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                bookvm.Tags = await _db.Tags.ToListAsync();
                return View(bookvm);

            }
            if (bookvm.MainPhoto.CheckFileLength(300))
            {
                ModelState.AddModelError("MainPhoto", "File length should be more than" +300 + "kb");
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                bookvm.Tags = await _db.Tags.ToListAsync();
                return View(bookvm);

            }
            if (!bookvm.HoverPhoto.CheckFileType("image/"))
            {
                ModelState.AddModelError("HoverPhoto", "File must be image type");
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                bookvm.Tags = await _db.Tags.ToListAsync();
                return View(bookvm);

            }
            if (bookvm.HoverPhoto.CheckFileLength(300))
            {
                ModelState.AddModelError("HoverPhoto", "File length should be more than" + 300 + "kb");
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                bookvm.Tags = await _db.Tags.ToListAsync();
                return View(bookvm);

            }

            BookImage mainImage = new BookImage
            {
                IsPrimary = true,
                Image = bookvm.MainPhoto.CreateFile(_env.WebRootPath, "uploads/product"),
                Book = book
            };
            BookImage hoverImage = new BookImage
            {
                IsPrimary = false,
                Image = bookvm.HoverPhoto.CreateFile(_env.WebRootPath, "uploads/product"),
                Book = book
            };
            book.BookImages.Add(mainImage);
            book.BookImages.Add(hoverImage);
            foreach(IFormFile imgFile in bookvm.Photos)
            {
                if (!imgFile.CheckFileType("image/"))
                {
                    continue;
                }
                if (imgFile.CheckFileLength(300))
                {
                    continue;
                }
                BookImage bookImage = new BookImage
                {
                    IsPrimary = null,
                    Image = imgFile.CreateFile(_env.WebRootPath, "uploads/product"),
                    Book = book
                };
                book.BookImages.Add(bookImage);
            }


            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id <= 0) return NotFound();
            Book exist = await _db.Books.Include(x => x.Author).Include(x => x.Category).Include(x=>x.BookImages).Include(x => x.BookTags).ThenInclude(x => x.Tag).FirstOrDefaultAsync(x => x.Id == id);
            if (exist is null) return NotFound();
            UpdateBookVM bookvm = new UpdateBookVM()
            {
                Id=exist.Id,
                Name = exist.Name,
                Description = exist.Description,
                Page = exist.Page,
                CostPrice = exist.CostPrice,
                SalePrice = exist.SalePrice,
                Discount = exist.Discount,
                AuthorId = exist.AuthorId,
                CategoryId = exist.CategoryId,
                TagIds=exist.BookTags.Select(x => x.TagId).ToList(),
                BookImagesVm=new List<BookImageVm>()
            };
            foreach(var item in exist.BookImages)
            {
                BookImageVm bookImageVm = new BookImageVm()
                {
                    Id=item.Id,
                    IsPrimary = item.IsPrimary,
                    Image=item.Image
                };
                bookvm.BookImagesVm.Add(bookImageVm);
            }
            bookvm.Authors = await _db.Authors.ToListAsync();
            bookvm.Categories = await _db.Categories.ToListAsync();
            bookvm.Tags = await _db.Tags.ToListAsync();
            return View(bookvm);
        
        }
        [HttpPost] 
        public async Task<IActionResult> Update(int id,UpdateBookVM bookvm)
        {
            Book exist = await _db.Books.Include(x => x.Author).Include(x => x.Category).Include(X=>X.BookImages).Include(x => x.BookTags).ThenInclude(x => x.Tag).FirstOrDefaultAsync(x => x.Id == id);
            if (exist is null) return NotFound();
            bookvm.Authors = await _db.Authors.ToListAsync();
            if(!ModelState.IsValid)
            {
                bookvm.Authors=await _db.Authors.ToListAsync();
                bookvm.Categories=await _db.Categories.ToListAsync();
                bookvm.Tags = await _db.Tags.ToListAsync();
                return View();
            }
            if(!await _db.Authors.AnyAsync(x=>x.Id==bookvm.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Bele bir author movcud deyil");
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                bookvm.Tags = await _db.Tags.ToListAsync();
                return View();
            }
            if (!await _db.Categories.AnyAsync(x => x.Id == bookvm.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Bele bir category movcud deyil");
                bookvm.Authors = await _db.Authors.ToListAsync();
                bookvm.Categories = await _db.Categories.ToListAsync();
                bookvm.Tags = await _db.Tags.ToListAsync();
                return View();
            }
            foreach (var item in bookvm.TagIds)
            {
                if (!await _db.Tags.AnyAsync(x => x.Id == item))
                {
                    ModelState.AddModelError("TagIds", "Bele tag movcud deyil");
                    bookvm.Authors = await _db.Authors.ToListAsync();
                    bookvm.Categories = await _db.Categories.ToListAsync();
                    bookvm.Tags = await _db.Tags.ToListAsync();
                    return View(bookvm);
                }
            }
            List<int> newSelectTagId = bookvm.TagIds.Where(tagId => !exist.BookTags.Exists(x => x.TagId == tagId)).ToList();
            exist.Name= bookvm.Name;
            exist.Description= bookvm.Description;
            exist.Page= bookvm.Page;
            exist.CostPrice= bookvm.CostPrice;
            exist.SalePrice= bookvm.SalePrice;
            exist.Discount= bookvm.Discount;    
            exist.AuthorId= bookvm.AuthorId;
            exist.CategoryId= bookvm.CategoryId;
            foreach (var newTagId in newSelectTagId)
            {
                BookTag bookTag = new BookTag()
                {
                    TagId = newTagId,
                    BookId = bookvm.Id,
                };
                _db.BookTags.Add(bookTag);
            }
            List<BookTag> removedTag = exist.BookTags.Where(x => !bookvm.TagIds.Contains(x.TagId)).ToList();

            _db.BookTags.RemoveRange(removedTag);
            if(bookvm.MainPhoto!=null)
            {
                if(!bookvm.MainPhoto.CheckFileType("image/")) 
                {
                    ModelState.AddModelError("MainPhoto", "File must be image type");
                    bookvm.Authors = await _db.Authors.ToListAsync();
                    bookvm.Categories = await _db.Categories.ToListAsync();
                    bookvm.Tags = await _db.Tags.ToListAsync();
                    return View(bookvm);

                }
                if (bookvm.MainPhoto.CheckFileLength(300))
                {
                    ModelState.AddModelError("MainPhoto", "File length should be more than" + 300 + "kb");
                    bookvm.Authors = await _db.Authors.ToListAsync();
                    bookvm.Categories = await _db.Categories.ToListAsync();
                    bookvm.Tags = await _db.Tags.ToListAsync();
                    return View(bookvm);

                }
                var existMainPhoto=exist.BookImages.FirstOrDefault(x=>x.IsPrimary==true);
                existMainPhoto.Image.DeleteFile(_env.WebRootPath, "uploads/product");
                exist.BookImages.Remove(existMainPhoto);
                BookImage bookImage = new BookImage()
                {
                    IsPrimary = true,
                    Image = bookvm.MainPhoto.CreateFile(_env.WebRootPath, "uploads/product"),
                    BookId = exist.Id
                };
                exist.BookImages.Add(bookImage);    
            }
            if (bookvm.HoverPhoto != null)
            {
                if (!bookvm.HoverPhoto.CheckFileType("image/"))
                {
                    ModelState.AddModelError("HoverPhoto", "File must be image type");
                    bookvm.Authors = await _db.Authors.ToListAsync();
                    bookvm.Categories = await _db.Categories.ToListAsync();
                    bookvm.Tags = await _db.Tags.ToListAsync();
                    return View(bookvm);

                }
                if (bookvm.MainPhoto.CheckFileLength(300))
                {
                    ModelState.AddModelError("HoverPhoto", "File length should be more than" + 300 + "kb");
                    bookvm.Authors = await _db.Authors.ToListAsync();
                    bookvm.Categories = await _db.Categories.ToListAsync();
                    bookvm.Tags = await _db.Tags.ToListAsync();
                    return View(bookvm);

                }
                var existHoverPhoto = exist.BookImages.FirstOrDefault(x => x.IsPrimary == false);
                existHoverPhoto.Image.DeleteFile(_env.WebRootPath, "uploads/product");
                exist.BookImages.Remove(existHoverPhoto);
                BookImage bookImage = new BookImage()
                {
                    IsPrimary = false,
                    Image = bookvm.HoverPhoto.CreateFile(_env.WebRootPath, "uploads/product"),
                    BookId = exist.Id
                };
                exist.BookImages.Add(bookImage);
            }
            
            List<BookImage> deleteList = exist.BookImages.Where(x => !bookvm.ImageIds.Contains(x.Id) && x.IsPrimary == null).ToList();
            if (deleteList.Count > 0)
            {
                foreach (var item in deleteList)
                {
                    exist.BookImages.Remove(item);
                    item.Image.DeleteFile(_env.WebRootPath, "uploads/product");
                }
            }
            foreach (IFormFile imgFile in bookvm.Photos)
            {
                if (!imgFile.CheckFileType("image/"))
                {
                    continue;
                }
                if (imgFile.CheckFileLength(300))
                {
                    continue;
                }
                BookImage bookImage = new BookImage
                {
                    IsPrimary = null,
                    Image = imgFile.CreateFile(_env.WebRootPath, "uploads/product"),
                    BookId = exist.Id
                };
                exist.BookImages.Add(bookImage);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int id)
        {
            Book book = await _db.Books.Include(x=>x.BookImages).FirstOrDefaultAsync(x => x.Id == id);
            if (book == null) return NotFound();
            foreach(var item in book.BookImages)
            {
                item.Image.DeleteFile(_env.WebRootPath, "uploads/product");
            }
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }





    }

}

    

