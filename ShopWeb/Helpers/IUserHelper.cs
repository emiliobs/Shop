namespace ShopWeb.Helpers
{
    using Microsoft.AspNetCore.Identity;
    using ShopWeb.Data.Entities;
    using ShopWeb.Models;
    using System.Threading.Tasks;
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<IdentityResult> AddUserAsycncAsync(User user, string password);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsyc();
    }
}
