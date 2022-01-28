using System.ComponentModel.DataAnnotations;

namespace AuthServerApp.Models
{
    public class SignInModel
    {
        [Required]
        public string? Email { get; init; }
        [Required]
        public string? Password { get; init; }
    }
}
