
using LiteEcommerceApi.Models;
using LiteEcommerceApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LiteEcommerceApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string policyName = "CorsPolicy";
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: policyName, builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });


            //Connect Database
            //Add Sql Server
            builder.Services.AddDbContext<ApplicationDbContext>(
                o => o.UseSqlServer(
                    builder.Configuration.GetConnectionString("Connection")));

            builder.Services.AddScoped<IAuth, Auth>();
            builder.Services.AddScoped<IProduct, Services.Product>();
            builder.Services.AddScoped<IRefreshTokenServices, Services.RefreshTokenService>();

            //configration JWT
            builder.Services.AddAuthentication(cfg => {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8
                        .GetBytes(builder.Configuration["ApplicationSettings:JWT_Secret"])
                    ),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(policyName);


            app.MapControllers();

            app.Run();
        }
    }
}