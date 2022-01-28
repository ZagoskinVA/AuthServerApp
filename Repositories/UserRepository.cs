using AuthServerApp.Interfaces;
using AuthServerApp.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthServerApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> GetUserById(string Id)
        {
            return await _userManager.FindByIdAsync(Id);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task<string> GetEmailConfarmationTokenAsync(User user) 
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
    }
}
