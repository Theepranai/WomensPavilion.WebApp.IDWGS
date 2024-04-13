namespace PixelPlusMedia.Application.Models.Authentication;
public class AuthenticationResponse
{
    public AuthenticationResponse() : base() { }
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;

}
