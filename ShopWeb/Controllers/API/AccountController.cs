namespace ShopWeb.Controllers.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShopCommon.Models;
    using ShopWeb.Data;
    using ShopWeb.Helpers;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Atributtes
        private readonly IUserHelper userHelper;
        private readonly ICountryRepository countryRepository;
        private readonly IEmailHelper emailHelper;
        #endregion

        #region Contructors
        public AccountController(IUserHelper userHelper, ICountryRepository countryRepository, IEmailHelper emailHelper)
        {
            this.userHelper = userHelper;
            this.countryRepository = countryRepository;
            this.emailHelper = emailHelper;
        }
        #endregion

        #region MethodosOrActions

         [HttpPost]
         public async Task<IActionResult> PostUser([FromBody] NewUserRequest  request)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad Request."
                });
            }

            var user = await this.userHelper.GetUserByEmailAsync(request.Email);
            if (user != null)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "This Email is already registered."
                });
            }

            var city = await this.countryRepository.GetCityAsync(request.CityId);
            if (city == null)
            {
                return this.BadRequest(new Response
                {
                   IsSuccess = false,
                   Message = "City don't exists.",
                });
            }

            user = new Data.Entities.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                Address = request.Address,
                PhoneNumber = request.Phone,
                CityId = request.CityId,
                City = city
            };

            var result = await this.userHelper.AddUserAsync(user, request.Password);
            if (result != IdentityResult.Success)
            {
                return this.BadRequest(result.Errors.FirstOrDefault().Description);
            }

            var myToken = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);

            var tokenLink = this.Url.Action("ConfirmEmail","Account", new {

               userid = user.Id,
               token = myToken,                     

            },protocol:HttpContext.Request.Scheme);

            this.emailHelper.sendMail(request.Email, "Email confirmation", $"<h1>Email Confirmation</h1>" +
            $"To allow the user, " +
            $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");


            return Ok(new Response
            {

                IsSuccess = true,
                Message = "A Confirmation email was set, Please confirm your account and into the App."

            }); 
        }

        #endregion
    }
}