using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Regsiter()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Regsiter(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var result = _userManager.CreateAsync(new ApplicationUser()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email
            }, registerViewModel.Password).Result;

            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                }
                return View(registerViewModel);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
