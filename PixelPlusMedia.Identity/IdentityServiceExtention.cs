using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PixelPlusMedia.Application.Contracts.Identity;
using PixelPlusMedia.Application.Models.Authentication;
using PixelPlusMedia.Identity.Models;
using PixelPlusMedia.Identity.Services;
using System.Text;

namespace PixelPlusMedia.Identity;
public static class IdentityServiceExtention
{ 
    public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddDbContext<UserIdentityDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("DBConnectionString"),
            b=>b.MigrationsAssembly(typeof(UserIdentityDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, ApplicationRole>()
        .AddEntityFrameworkStores<UserIdentityDbContext>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Password Setting
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredLength = 8;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.AllowedForNewUsers = true;
            options.User.RequireUniqueEmail = false;
            options.SignIn.RequireConfirmedEmail = false;

            // User settings.
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

        });

        services.AddTransient<IAuthenticationService, AuthenticationService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
            };
        });
        
    }
}
