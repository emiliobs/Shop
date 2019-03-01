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

        public async Task<IActionResult> ChangeUser()
        {
            var user = await userHelper.GetUserByEmailAsync(User.Identity.Name);
            var model = new ChangeUserViewModel();
            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                                  
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userHelper.GetUserByEmailAsync(User.Identity.Name);
                if (user  != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    var response = await userHelper.UpdateUserAsyc(user);
                    if (response.Succeeded)
                    {
                        ViewBag.UserMessage = "User Update!";
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,response.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User no Found.");
                }

                
            }

            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userHelper.GetUserByEmailAsync(User.Identity.Name);
                if (user != null)
                {
                    var result = await userHelper.ChangedPasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usero found.");
                }
            }

            return View(model);
        }




        #endregion
    }
}
