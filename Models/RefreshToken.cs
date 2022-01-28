using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthServerApp.Models
{
    public class RefreshToken
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 1)]
        public int Id { get; set; }
        public string JwtToken { get; set; }
        public string RefreshJwtToken { get; set; }
        public string ExpirationDate { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        [NotMapped]
        public UserViewModel User { get; set; }

        public void UpdateExpDate() 
        {
            ExpirationDate = DateTime.Now.AddMonths(1).ToString("dd-MM-yyyy");
        }
    }
}
