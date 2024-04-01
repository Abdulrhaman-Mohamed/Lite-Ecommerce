using LiteEcommerceApi.Dots;
using LiteEcommerceApi.Helper;
using LiteEcommerceApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LiteEcommerceApi.Services
{
    public class Auth : IAuth
    {
        private readonly string secertKey;
        private readonly ApplicationDbContext dbContext;
        public Auth(IConfiguration configuration, ApplicationDbContext _dbContext)
        {
            secertKey = configuration["ApplicationSettings:JWT_Secret"];
            dbContext = _dbContext;
            ;
        }


        public async Task<AuthModel> Login(Login model)
        {
            var user = await dbContext.Users.Where(user => user.Email == model.Email).FirstOrDefaultAsync();
            if (user is null) return new AuthModel { Message = "Invilad credential" };

            var hashPassword = HashPassword(model.Password);
            if (hashPassword != user.Password) return new AuthModel { Message = "Invilad credential" };



            var token_ = GenerateJWTToken(user);
            user.RefreshToken= token_;
            user.RefreshTokenExpiry= DateTime.Now.AddDays(10);

            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();

            

            return new AuthModel { Message = "Login Successfully", IsAuthenticated = true, Email = user.Email, Username = user.Username, Token = token_ , RefreshTokenExpiration = DateTime.Now.AddDays(10) };


        }

        public async Task<AuthModel> Registeration(Register model)
        {

            if (await dbContext.Users.Where(user => user.Email == model.Email).FirstOrDefaultAsync() is not null) return new AuthModel { Message = "Email is used Before" };

            if (await dbContext.Users.Where(user => user.Username == model.Username).FirstOrDefaultAsync() is not null) return new AuthModel { Message = "Username is used Before" };

            var User = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = HashPassword(model.Password),
                LastLogin = DateTime.Now,
            };

            await dbContext.Users.AddAsync(User);

            await dbContext.SaveChangesAsync();

            return new AuthModel { Message = "Account is Created Successfully", IsAuthenticated = true };
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


        public string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
        }




    }
}
