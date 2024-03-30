using LiteEcommerceApi.Helper;
using LiteEcommerceApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LiteEcommerceApi.Services
{
    public class RefreshTokenService : IRefreshTokenServices
    {
        private readonly ApplicationDbContext dbContext;
        private readonly string secertKey;
        public RefreshTokenService(ApplicationDbContext _dbContext, IConfiguration configuration) {
            dbContext = _dbContext;
            secertKey = configuration["ApplicationSettings:JWT_Secret"];
        }
        public async Task<AuthModel> refreshToken(string token)
        {
            var user = await dbContext.Users.SingleOrDefaultAsync(u=> u.RefreshToken == token && u.RefreshTokenExpiry >= DateTime.Now);
            if(user is null) return new AuthModel { Message="Invilad Token"};

            var token_= GenerateJWTToken(user);

            user.RefreshToken=token_;
            user.RefreshTokenExpiry = DateTime.Now.AddDays(10);

            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();

            return new AuthModel {Token=token_};

        }


        public string GenerateJWTToken(User user)
        {

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti , user.UserId.ToString())
            };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(10),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(secertKey)
                        ),
                    SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
