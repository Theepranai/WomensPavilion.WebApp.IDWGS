using PixelPlusMedia.Application.Responses;

namespace PixelPlusMedia.Application.Models.Authentication.Role
{
    public class GetRolesResponse : BaseResponse
    {
        public GetRolesResponse() : base() { }
        public List<object> Roles { get; set; }
    }
}
