using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LiteEcommerceApi.Models
{
    [Index(nameof(Email),IsUnique =true)]
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        
        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime LastLogin { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiry { get; set; }
    }
}
