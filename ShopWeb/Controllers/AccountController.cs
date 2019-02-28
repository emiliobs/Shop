namespace ShopWeb.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShopWeb.Data.Entities;
    using ShopWeb.Helpers;
    using ShopWeb.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class AccountController : Controller
    {

        #region Attributes
        private readonly IUserHelper userHelper;
        #endregion

        #region Constructor
        public AccountController(IUserHelper userHelper)
        {
            this.userHelper = userHelper;
        }
        #endregion

        #region Methods

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Failed to Login.");
            return View(model);
        }

        public async Task<ActionResult> Logout()
        {
            await userHelper.LogoutAsyc();
            return RedirectToAction("Index","Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userHelper.GetUserByEmailAsync(model.Username);

                if (user == null)
                {
                    user = new User()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username,
                    };

                    //aqui creao wel usuario:
                    var result = await userHelper.AddUserAsycncAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        ModelState.AddModelError(string.Empty,"The user couldn't be created.");
                        return View();
                    }

                    //si lo creo de una vez lo logeo..
                    var loginViewModel = new LoginViewModel()
                    {
                      Password = model.Password,
                      RememberMe = false,
                      Username = model.Username,
                    };

                    var result2 = await userHelper.LoginAsync(loginViewModel);

                    if (result2.Succeeded)
                    {
                        return RedirectToAction("Index","Home");
                    }

                    ModelState.AddModelError(string.Empty, "The user could't be login.");

                    return View(model);

                }

                ModelState.AddModelError(string.Empty,"The Username is already registered.");
            
            }

            return View(model);

        }


        #endregion
    }
}
