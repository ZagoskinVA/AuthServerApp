using AuthServerApp.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthServerApp.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task UpdateUserAsync(User user);
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task<User> GetUserById(string Id);
        Task<string> GetEmailConfarmationTokenAsync(User user);
    }
}
