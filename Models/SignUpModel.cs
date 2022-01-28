using System.ComponentModel.DataAnnotations;

namespace AuthServerApp.Models
{
    public class SignUpModel
    {
        [Required]
        public string? Email { get; init; }
        [Required]
        public string? Password { get; init; }
        [Required]
        [Compare("Password", ErrorMessage = "Эти поля должны совпадать")]
        public string? ConfirmPassword { get; init; }
        [Required]
        public string? Name { get; init; }
    }
}
