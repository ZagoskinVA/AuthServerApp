using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthServerApp.Models
{
    public class RefreshToken
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public string JwtToken { get; set; }
        public string RefreshJwtToken { get; set; }
        public DateTime ExpirationDate { get; } = DateTime.Today.AddMonths(1);
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
