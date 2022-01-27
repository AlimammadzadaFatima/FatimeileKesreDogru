using envoBack.Areas.ViewModels;
using envoBack.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace envoBack.Areas.Manage.Controllers
{[Area("manage")]
    public class AccountController : Controller
    {
        private readonly UserManager <AppUser> _user;
        private readonly SignInManager <AppUser> _sign;
        private readonly RoleManager <IdentityRole> _role;
        private readonly DataContext _context;

        public AccountController(UserManager <AppUser> user, SignInManager<AppUser> sign, RoleManager<IdentityRole> role, DataContext context)
        {
            _user = user;
            _sign = sign;
            _role = role;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel adminVM)
        {
            bool isAdmin = await _user.Users.AnyAsync(x => x.IsAdmin);
            if (isAdmin == true)
            {
                AppUser admin = await _user.FindByNameAsync(adminVM.UserName);
                if (admin == null)
                {
                    ModelState.AddModelError("", "Ad ve ya parol yanlisdir");
                    return View(adminVM);
                }
                var result = await _sign.PasswordSignInAsync(admin, adminVM.Password, false, false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Ad ve ya parol yanlisdir");
                    return View(adminVM);
                }
                return RedirectToAction("Index", "Dashboard");

            }
            ModelState.AddModelError("", "Ad ve ya parol yanlisdir");
            return View();
        }

        public async Task <IActionResult> Logout()
        {
            await _sign.SignOutAsync();
            return RedirectToAction("login", "account");
        }
        //public async Task<IActionResult> CreateRole()
        //{
        //    var role1 = await _role.CreateAsync(new IdentityRole("Admin"));
        //    var role2 = await _role.CreateAsync(new IdentityRole("SuperAdmin"));
        //    var role3 = await _role.CreateAsync(new IdentityRole("Member"));

        //    var admin = new AppUser { FullName = "Super Admin", UserName = "SuperAdmin" };
        //    var result = await _user.CreateAsync(admin, "SuperAdmin123");
        //    var resultRole = await _user.AddToRoleAsync(admin, "SuperAdmin");
        //    return Ok(resultRole);
        //} 
    }
}
