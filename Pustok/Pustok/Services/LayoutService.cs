using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok.DAL;
using Pustok.Models;
using Pustok.ViewModels;
using System.Security.Claims;
using System.Threading;

namespace Pustok.Services
{
    public class LayoutService
    {
        private AppDbContext _db;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<AppUser> _userManager;

        public LayoutService(AppDbContext db, IHttpContextAccessor http, UserManager<AppUser> userManager)
        {
            _db = db;
            _http = http;
            _userManager = userManager;
        }
        public async Task<Dictionary<string,string>> GetSetting()
        {
            var setting = await _db.Settings.ToDictionaryAsync(x=>x.Key,x=>x.Value);
            return setting;
        }
        public async Task<List<BookItemVM>> GetBooks()
        {
            List<BookItemVM> basket= new List<BookItemVM>();
            if (_http.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.Users.Include(x => x.BasketItems).ThenInclude(x=>x.Book).ThenInclude(x => x.BookImages.Where(x => x.IsPrimary == true)).FirstOrDefaultAsync(x => x.Id == _http.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                foreach (var item in user.BasketItems)
                {
                    Book book = _db.Books.Include(x => x.BookImages.Where(x => x.IsPrimary == true)).FirstOrDefault(x => x.Id == item.Id);
                    if (book == null)
                    {
                        continue;
                    }
                    basket.Add(new BookItemVM()
                    {
                        Id = item.Id,
                        Name = book.Name,
                        Price = book.SalePrice - book.Discount,
                        Image = book.BookImages[0].Image,
                        SubTotal = (book.SalePrice - book.Discount) * item.Count,
                        Count = item.Count,


                    });
                }

                await _db.SaveChangesAsync();
            }
            else
            {
                var jsonBasket = _http.HttpContext.Request.Cookies["Basket"];
                if (jsonBasket != null)
                {
                    List<BookCookieVM> bookCookie = JsonConvert.DeserializeObject<List<BookCookieVM>>(jsonBasket);
                    foreach (var item in bookCookie)
                    {
                        Book book = _db.Books.Include(x => x.BookImages.Where(x => x.IsPrimary == true)).FirstOrDefault(x => x.Id == item.Id);
                        if (book == null)
                        {
                            continue;
                        }
                        basket.Add(new BookItemVM()
                        {
                            Id = item.Id,
                            Name = book.Name,
                            Price = book.SalePrice - book.Discount,
                            Image = book.BookImages[0].Image,
                            SubTotal = (book.SalePrice - book.Discount) * item.Count,
                            Count = item.Count,


                        });
                    }

                }
            }

           
            return basket;


        }
    }
}
