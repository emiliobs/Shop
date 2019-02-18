namespace ShopWeb.Helpers
{
    using Microsoft.AspNetCore.Identity;
    using ShopWeb.Data.Entities;
    using System.Threading.Tasks;
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<IdentityResult> AddUserAsycncAsync(User user, string password);
    }
}
