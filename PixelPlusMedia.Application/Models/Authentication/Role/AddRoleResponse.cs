using PixelPlusMedia.Application.Responses;
namespace PixelPlusMedia.Application.Models.Authentication.Role;

public class AddRoleResponse : BaseResponse
{
    public AddRoleResponse() : base() { }
    public string Id { get; set; }
    public string Name { get; set; }
}
