using LiteEcommerceApi.Dots;
using LiteEcommerceApi.Helper;

namespace LiteEcommerceApi.Services
{
    public interface IRefreshTokenServices
    {
        public Task<AuthModel> refreshToken(string token);
    }
}
