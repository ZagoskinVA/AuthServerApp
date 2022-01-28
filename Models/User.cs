using Microsoft.AspNetCore.Identity;

namespace AuthServerApp.Models
{
    public class User : IdentityUser
    {
        public string? ConfarmationCode { get; init; }
        public string? NickName { get; set; }
    }
}
