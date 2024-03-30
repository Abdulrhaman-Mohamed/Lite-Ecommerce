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
        public string Password { get; set; }
    }
}
