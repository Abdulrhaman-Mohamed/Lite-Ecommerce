using System.Text.Json.Serialization;

namespace LiteEcommerceApi.Helper
{
    public class AuthModel
    {
        public string? Message { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public bool IsAuthenticated { get; set; }

        //public DateTime? ExpireOn { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }
    }
}
