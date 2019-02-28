namespace ShopWeb.Controllers
{
    using Microsoft.AspNetCore.Mvc;
   
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

        #endregion
    }
}
