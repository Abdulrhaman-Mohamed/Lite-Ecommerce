using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LiteEcommerceApi.Dots
{
    public class Register
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordPropertyText]
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}")]
        public string Password { get; set; }
    }
}
