using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Crypto.Models;

namespace Crypto.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;

        public AdminController(UserManager<AppUser> mgrUs, IUser us)
        {
            userManager = mgrUs;
        }

        public ViewResult Index() => View(userManager.Users);

        public ViewResult Create() => View();


        [HttpPost]
        public async Task<IActionResult> Create(UserCreationModel UCM)
        {
            
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = UCM.UserName,
                    Email = UCM.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, UCM.Password);

                if (result.Succeeded)
                {

                    return RedirectToAction("Login", "Transaction", new { area = "Transaction" });
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(UCM);
        }
    }
}
