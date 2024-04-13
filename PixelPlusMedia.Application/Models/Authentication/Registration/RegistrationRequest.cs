using System.ComponentModel.DataAnnotations;

namespace PixelPlusMedia.Application.Models.Authentication.Registration;

public class RegistrationRequest
{
    [Required]
    public string ClientName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(5)]
    public string UserName { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }
    public string Role { get; set; }
}
