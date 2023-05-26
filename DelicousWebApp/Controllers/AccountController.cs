using DelicousWebApp.Models;
using DelicousWebApp.ViewModels.AccountVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace DelicousWebApp.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
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
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Register(RegisterVM register)
        {
            if(!ModelState.IsValid) return View(register);

            AppUser user = new AppUser()
            {
                Name=register.Name,
                Surname=register.Surname,
                Email=register.Email,
                UserName=register.UserName
            };
            IdentityResult identityResult = await _userManager.CreateAsync(user,register.Password);
            if (!identityResult.Succeeded)
            {
                foreach(IdentityError? error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(register);
                }
            }

            IdentityResult result = await _userManager.AddToRoleAsync(user, "Admin");
            if (!result.Succeeded)
            {
                foreach (IdentityError? error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(register);
                }
            }

            return RedirectToAction(nameof(Login));
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View(login);

            AppUser user = await _userManager.FindByNameAsync(login.UserName);
            if(user == null) return View(login);

            Microsoft.AspNetCore.Identity.SignInResult result= await _signInManager.PasswordSignInAsync(user,login.Password,true,false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid password or username.");
                return View(login);
            }

            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public async Task< IActionResult> LogOut()
        {
          await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
        #region Create Role 
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role = new IdentityRole()
        //    {
        //        Name="Admin"
        //    };
        //    if (role == null) return View();
        //    await _roleManager.CreateAsync(role);
        //    return Json("OK");
        //}
        #endregion
    }
}
