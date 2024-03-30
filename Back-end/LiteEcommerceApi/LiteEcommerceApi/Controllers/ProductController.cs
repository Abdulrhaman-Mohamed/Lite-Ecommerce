using LiteEcommerceApi.Dots;
using LiteEcommerceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LiteEcommerceApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct productService;
        private readonly IRefreshTokenServices refreshTokenServices;
        public ProductController(IProduct _productService, IRefreshTokenServices _refreshTokenServices)
        {
            productService = _productService;
            refreshTokenServices = _refreshTokenServices;
        }


        [HttpGet]
        public async Task<IActionResult> getAllProducts() {

            var products = await productService.getProducts();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> addProduct([FromForm] ProductDots product)
        {
            if(!ModelState.IsValid) {
                return BadRequest();
            }

            var Create = await productService.addProductService(product);

            if(Create.Status == 500) return StatusCode(StatusCodes.Status500InternalServerError , Create.Message);

            if(Create.Status == 404) return BadRequest();
            
            return StatusCode(StatusCodes.Status201Created , Create.Message);
        }


        [HttpGet("/testToken")]
        public async Task<IActionResult> CheckToken()
        {
            string? token = HttpContext.Request.Cookies["RefreshToken"];
            var refreshToken = await refreshTokenServices.refreshToken(token);
            if (!refreshToken.Message.IsNullOrEmpty()) return BadRequest(refreshToken.Message);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(10).ToLocalTime()
            };

            HttpContext.Response.Cookies.Append("RefreshToken", refreshToken.Token, cookieOptions);

            return Ok();
        }

    }
}
