using PixelPlusMedia.Application.Responses;

namespace PixelPlusMedia.Application.Models.Authentication.Role
{
    public class RemoveRoleResponse : BaseResponse
    {
        public RemoveRoleResponse() : base() { }
        public string Id { get; set; }
    }
}
