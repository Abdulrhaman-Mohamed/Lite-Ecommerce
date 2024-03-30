using LiteEcommerceApi.Dots;
using LiteEcommerceApi.Models;
using LiteEcommerceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace LiteEcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth auth;
        public AuthController(IAuth _auth)
        {
            auth = _auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUserAynsc(Register register)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Creation = await auth.Registeration(register);

            if(!Creation.IsAuthenticated) return BadRequest(Creation.Message);


            return Ok(Creation.Message);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> LoginAynsc(Login login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Check = await auth.Login(login);
            if (!Check.IsAuthenticated) return BadRequest(Check.Message);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, 
                Expires = DateTime.UtcNow.AddDays(10).ToLocalTime()
            };

            HttpContext.Response.Cookies.Append("RefreshToken", Check.Token, cookieOptions);

            return Ok(Check);

        }

    }
}
