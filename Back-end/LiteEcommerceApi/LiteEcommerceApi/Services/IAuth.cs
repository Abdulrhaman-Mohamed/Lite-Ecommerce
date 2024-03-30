using LiteEcommerceApi.Dots;
using LiteEcommerceApi.Helper;
using LiteEcommerceApi.Models;
using Microsoft.Win32;

namespace LiteEcommerceApi.Services
{
    public interface IAuth
    {
        public Task<AuthModel> Registeration(Register model);

        public Task<AuthModel> Login(Login model);
    }
}
