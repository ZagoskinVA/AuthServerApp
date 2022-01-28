using AuthServerApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthServerApp.Contexts
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public new DbSet<User>? Users {get; init;}

        public DbSet<RefreshToken> RefreshTokens {get; init;}

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}
