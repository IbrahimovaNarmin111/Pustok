using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok.DAL;
using Pustok.Models;
using Pustok.ViewModels;
using System.Security.Claims;

namespace Pustok.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public CartController(AppDbContext db,UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> AddBasket(int id)
        {
            
            Book book=await _db.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null) return NotFound();
            if(User.Identity.IsAuthenticated)
            {
                AppUser user=await _userManager.Users.Include(x=>x.BasketItems).FirstOrDefaultAsync(x=>x.Id==User.FindFirstValue(ClaimTypes.NameIdentifier));
                BasketItem basketItem = user.BasketItems.FirstOrDefault(x => x.BookId == id);
                if(basketItem is not null)
                {
                    basketItem.Count++;
                }
                else
                {
                    user.BasketItems.Add(new BasketItem
                    {
                        Count=1,
                        BookId=id
                    });
                }
                await _db.SaveChangesAsync();
            }

            List<BookCookieVM> basket= new List<BookCookieVM>();
            if (Request.Cookies["Basket"] is null)
            {
                BookCookieVM bookCookieVM = new BookCookieVM()
                {
                    Id = id,
                    Count = 1
                };
                basket.Add(bookCookieVM);
            }
            else
            {
                basket = JsonConvert.DeserializeObject<List<BookCookieVM>>(Request.Cookies["Basket"]);
                var exist=basket.FirstOrDefault(x => x.Id == id);
                if(exist != null)
                {
                    exist.Count += 1;
                }
                else
                {
                    BookCookieVM bookCookieVM = new BookCookieVM()
                    {
                        Id = id,
                        Count = 1
                    };
                    basket.Add(bookCookieVM);
                }

            }
           var json=JsonConvert.SerializeObject(basket);
            Response.Cookies.Append("Basket", json);
            return RedirectToAction(nameof(Index), "Home");


           

        }
        public async Task<IActionResult> RemoveBasket(int id)
        {
            string json = Request.Cookies["Basket"];
            if(json is not null)
            {
                List<BookCookieVM> basket = JsonConvert.DeserializeObject<List<BookCookieVM>>(json);
                BookCookieVM book=basket.FirstOrDefault(x=>x.Id == id);
                if(book is not null)
                {
                    basket.Remove(book);
                }
                Response.Cookies.Append("Basket", JsonConvert.SerializeObject(basket));
            }
            return RedirectToAction(nameof(Index), "Home");
        }

    }
}
