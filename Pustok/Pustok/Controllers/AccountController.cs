using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok.Helpers;
using Pustok.Models;
using Pustok.ViewModels;
using System.Runtime.CompilerServices;

namespace Pustok.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser()
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                Surname = registerVM.Surname,
                UserName = registerVM.Username
            };
            IdentityResult results=await _userManager.CreateAsync(user,registerVM.Password);
            if (!results.Succeeded)
            {
                foreach(var error in results.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View();
                }
            }
            await _userManager.AddToRoleAsync(user,UserRole.Member.ToString());
            await _signInManager.SignInAsync(user, false);
           return RedirectToAction(nameof(Index),"Home");
        }
        public async Task<IActionResult>Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM,string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var exist = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
            if (exist == null)
            {
                exist=await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
                if (exist == null)
                {
                    ModelState.AddModelError("", "Username ve ya Password sehvdir");
                    return View();
                }
            }
            var signInCheck = _signInManager.CheckPasswordSignInAsync(exist, loginVM.Password, true).Result;
            
                if(!signInCheck.Succeeded)
                {
                    ModelState.AddModelError("", "Username ve ya passwordunuz sehvdir");
                    return View();
                }
                if(signInCheck.IsLockedOut)
                {

                    ModelState.AddModelError("", "Biraz sonra yeniden cehd edin");
                    return View();
                }
                await _signInManager.SignInAsync(exist, loginVM.RememberMe);
                if(ReturnUrl != null)
                {
                return RedirectToAction(ReturnUrl);
                }
                       
            
            return RedirectToAction(nameof(Index), "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");

        }

        public async Task<IActionResult>CreateRole()
        {
            foreach(var role in Enum.GetValues(typeof(UserRole))) 
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole()
                    {
                        Name= role.ToString(),

                    });
                }

            }
            return RedirectToAction(nameof(Index), "Home");
        }

            

    }
}
