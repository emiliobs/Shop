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
        private readonly RoleManager<IdentityRole> roleManager;
        #endregion

        #region Costructor
        public UserHelper(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {

            //aqui creao nuevos usuarios:
            this.userManager = userManager;  
            //lo utilizo para logiar y deslogiar.
            this.signInManager = signInManager;
            //Lo ujtilizo para rear los roles:
            this.roleManager = roleManager;
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
        public async Task<IdentityResult> ChangedPasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await userManager.ChangePasswordAsync(user, oldPassword,newPassword);
        }

        public async Task<IdentityResult> UpdateUserAsyc(User user)
        {
           return  await userManager.UpdateAsync(user);
        }

        public async Task<SignInResult> ValidatePasswordAsync(User user, string password)
        {
            return await this.signInManager.CheckPasswordSignInAsync(user, password, false);      
        }

        public async Task CheckRoleAsync(string roleName)
        {
            //aqui verifico el rol:
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole()
                {
                   Name = roleName,
                });
            }
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
             await userManager.AddToRoleAsync(user, roleName);
        }

      
        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await userManager.IsInRoleAsync(user, roleName);
        }


        #endregion
    }
}
