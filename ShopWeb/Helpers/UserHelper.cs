namespace ShopWeb.Helpers
{
    using Microsoft.AspNetCore.Identity;
    using ShopWeb.Data.Entities;
    using ShopWeb.Models;
    using System.Threading.Tasks;

    public class UserHelper : IUserHelper
    {
        #region Atributtes
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        #endregion

        #region Costructor
        public UserHelper(UserManager<User> userManager, SignInManager<User> signInManager)
        {

            //aqui creao nuevos usuarios:
            this.userManager = userManager;

            //lo utilizo para logiar y deslogiar.
            this.signInManager = signInManager;
        }
        #endregion

        #region Methods
        public async Task<IdentityResult> AddUserAsycncAsync(User user, string password)
        {
            return await this.userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await this.userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
        }

        public async Task LogoutAsyc()
        {
            await signInManager.SignOutAsync();
        } 
        #endregion
    }
}
