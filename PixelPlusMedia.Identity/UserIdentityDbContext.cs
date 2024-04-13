using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PixelPlusMedia.Identity.Models;
namespace PixelPlusMedia.Identity;

internal class UserIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options) : base(options) { }
}
