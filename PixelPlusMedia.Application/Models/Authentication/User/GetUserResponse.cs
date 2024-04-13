namespace PixelPlusMedia.Application.Models.Authentication.User;
public class GetUserResponse 
{
    public GetUserResponse() : base() { }
    public string Id { get; set; }
    public string ClientName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string[] Roles { get; set; }
}
