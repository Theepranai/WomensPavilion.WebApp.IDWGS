using PixelPlusMedia.Application.Responses;
namespace PixelPlusMedia.Application.Models.Authentication.Registration;

public class RegistrationResponse : BaseResponse
{
    public RegistrationResponse() : base() { }
    public string UserId { get; set; }
}
