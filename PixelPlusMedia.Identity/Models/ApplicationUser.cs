using Microsoft.AspNetCore.Identity;
namespace PixelPlusMedia.Identity.Models;
public class ApplicationUser : IdentityUser<string>
{
    public string ClientName { get; set; } = string.Empty;

}
